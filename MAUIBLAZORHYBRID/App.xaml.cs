using Microsoft.UI.Windowing;
using System.Diagnostics;

#if WINDOWS
using Microsoft.Maui.Platform;
using MAUIBLAZORHYBRID.Platforms.Windows;
#endif

namespace MAUIBLAZORHYBRID
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                var exception = (Exception)args.ExceptionObject;
                HandleGlobalException(exception);
            };

            // Handle background thread exceptions
            TaskScheduler.UnobservedTaskException += (sender, args) =>
            {
                HandleGlobalException(args.Exception);
                args.SetObserved();
            };

            MainPage = new MainPage();
        }

        private void HandleGlobalException(Exception ex)
        {
            Debug.WriteLine($"Global exception handler: {ex}");
            // Consider writing to a separate crash log file here
        }

        public void ToggleFullScreen()
        {
            #if WINDOWS
                        var window = Application.Current.Windows[0].Handler.PlatformView as MauiWinUIWindow;
                        window?.ToggleFullScreen();
            #endif
        }

    }
}
