using Microsoft.UI.Windowing;
using Microsoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRT.Interop;

namespace MAUIBLAZORHYBRID.Platforms.Windows
{
    public static class WindowExtensions
    {
        public static AppWindow? GetAppWindow(this MauiWinUIWindow window)
        {
            var hwnd = WindowNative.GetWindowHandle(window);
            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            return AppWindow.GetFromWindowId(windowId);
        }

        public static void ToggleFullScreen(this MauiWinUIWindow window)
        {
            var appWindow = window.GetAppWindow();
            if (appWindow is null) return;

            if (appWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.SetBorderAndTitleBar(false, false);

                if (presenter.State == OverlappedPresenterState.Maximized)
                {
                    presenter.Restore();
                }
                else
                {
                    presenter.Maximize();
                }
            }
        }

        public static void EnterFullScreen(this MauiWinUIWindow window)
        {
            var appWindow = window.GetAppWindow();
            if (appWindow?.Presenter is OverlappedPresenter presenter)
            {
                presenter.SetBorderAndTitleBar(false, false);
                presenter.Maximize();
            }
        }

        public static void ExitFullScreen(this MauiWinUIWindow window)
        {
            var appWindow = window?.GetAppWindow();
            if (appWindow?.Presenter is OverlappedPresenter presenter)
            {
                presenter.SetBorderAndTitleBar(true, true);
                presenter.Restore();
            }
        }
    }
}
