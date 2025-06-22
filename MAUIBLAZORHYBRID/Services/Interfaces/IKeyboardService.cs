using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Interfaces
{
    public interface IKeyboardService
    {
        event EventHandler<KeyEventArgs> KeyPressed;
        void StartListening();
        void StopListening();
    }

    public class KeyEventArgs : EventArgs
    {
        public string Key { get; }
        public bool CtrlPressed { get; }
        public bool ShiftPressed { get; }
        public bool AltPressed { get; }

        public KeyEventArgs(string key, bool ctrl, bool shift, bool alt)
        {
            Key = key;
            CtrlPressed = ctrl;
            ShiftPressed = shift;
            AltPressed = alt;
        }
    }
}
