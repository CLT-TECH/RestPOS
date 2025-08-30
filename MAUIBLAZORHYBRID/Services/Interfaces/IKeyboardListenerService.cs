using SharpHook;
using SharpHook.Data;
using SharpHook.Reactive;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace MAUIBLAZORHYBRID.Services.Interfaces
{
    public interface IKeyboardListenerService
    {
        IObservable<KeyboardEventData> KeyEvents { get; }
        IObservable<string> BarcodeStream { get; }
        void StartListening();
        void StopListening();
    }

    public class KeyboardEventData
    {
        public KeyCode KeyCode { get; set; }
        public string KeyName { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public EventType EventType { get; set; }

        public bool IsCtrlPressed { get; set; }
    }

    public class KeyboardListenerService : IKeyboardListenerService, IDisposable
    {
        private IReactiveGlobalHook? _hook;
        private readonly Subject<KeyboardEventData> _keyEventsSubject;
        private readonly Subject<string> _barcodeSubject;
        private readonly StringBuilder _barcodeBuffer;
        private DateTime _lastKeyPressTime;
        private const int BarcodeTimeoutMs = 300;

        private bool _ctrlPressed = false;

        private bool _isDisposed = false;

        public IObservable<KeyboardEventData> KeyEvents => _keyEventsSubject.AsObservable();
        public IObservable<string> BarcodeStream => _barcodeSubject.AsObservable();

        public KeyboardListenerService()
        {
            _keyEventsSubject = new Subject<KeyboardEventData>();
            _barcodeSubject = new Subject<string>();
            _barcodeBuffer = new StringBuilder();
            _lastKeyPressTime = DateTime.MinValue;
        }

        public void StartListening()
        {
            if (_isDisposed)
                throw new ObjectDisposedException("KeyboardListenerService has been disposed");

            // Clean up existing hook if present
            _hook?.Dispose();

            _hook = new ReactiveGlobalHook();

            _hook.KeyPressed.Merge(_hook.KeyReleased).Subscribe(ProcessKeyboardEvent);
            _hook.KeyPressed.Subscribe(UpdateCtrlState);
            _hook.KeyReleased.Subscribe(UpdateCtrlState);
            _hook.RunAsync();
        }

        public void StopListening()
        {
            _hook?.Dispose();
            _hook = null;
        }

        private void UpdateCtrlState(KeyboardHookEventArgs e)
        {
            var isPressed = e.RawEvent.Type == EventType.KeyPressed;
            if (e.Data.KeyCode is KeyCode.VcLeftControl or KeyCode.VcRightControl)
                _ctrlPressed = isPressed;
        }

        private void ProcessKeyboardEvent(KeyboardHookEventArgs e)
        {
            try
            {
                var data = new KeyboardEventData
                {
                    KeyCode = e.Data.KeyCode,
                    KeyName = e.Data.KeyCode.ToString(),
                    Timestamp = DateTime.Now,
                    EventType = e.RawEvent.Type,
                    IsCtrlPressed = _ctrlPressed
                };

                _keyEventsSubject.OnNext(data);

                if (e.RawEvent.Type == EventType.KeyPressed)
                {
                    HandleSpecialKeys(e.Data.KeyCode);

                    HandleBarcodeInput(e.Data.KeyCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ProcessKeyboardEvent Error:-" + ex.Message);
            }
        }
        private void HandleSpecialKeys(KeyCode keyCode)
        {
            if (keyCode >= KeyCode.VcF1 && keyCode <= KeyCode.VcF12)
            {
                Console.WriteLine($"Function key: {keyCode}");
            }
            else if (_ctrlPressed)
            {
                if (keyCode == KeyCode.VcS) Console.WriteLine("Ctrl+S");
                else if (keyCode == KeyCode.VcEnter || keyCode == KeyCode.VcNumPadEnter) Console.WriteLine("Ctrl+Enter");
                else if (keyCode == KeyCode.VcDelete) Console.WriteLine("Ctrl+Delete");
            }
            else if (keyCode == KeyCode.VcEquals) Console.WriteLine("+");
            else if (keyCode == KeyCode.VcMinus) Console.WriteLine("-");
        }

        private void HandleBarcodeInput(KeyCode keyCode)
        {
            var now = DateTime.Now;
            var elapsed = (now - _lastKeyPressTime).TotalMilliseconds;
            if (elapsed > BarcodeTimeoutMs && _barcodeBuffer.Length > 0)
            {
                Debug.Write("buffer clear");

                _barcodeBuffer.Clear();
            }

            Debug.Write("Code"+ _barcodeBuffer.Length + _barcodeBuffer.ToString());


            if (keyCode == KeyCode.VcEnter || keyCode == KeyCode.VcNumPadEnter)
            {
                Debug.Write("barcode enter");

                if (_barcodeBuffer.Length > 0)
                {
                    Debug.Write("barcode subject");

                    
                    _barcodeSubject.OnNext(_barcodeBuffer.ToString());
                    _barcodeBuffer.Clear();
                }
                return;
            }


            if (keyCode >= KeyCode.VcA && keyCode <= KeyCode.VcZ)
            {
                _barcodeBuffer.Append((char)('a' + (keyCode - KeyCode.VcA)));
            }
            else if (keyCode >= KeyCode.Vc0 && keyCode <= KeyCode.Vc9)
            {
                _barcodeBuffer.Append((char)('0' + (keyCode - KeyCode.Vc0)));
            }
            else 
            {
                _barcodeBuffer.Clear();
            }
                _lastKeyPressTime = now;
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            StopListening();
            _keyEventsSubject?.Dispose();
            _barcodeSubject?.Dispose();
            _isDisposed = true;
        }
    }

}
