using Microsoft.UI.Windowing;
using System.Diagnostics;
using Microsoft.Maui.Storage;
using MAUIBLAZORHYBRID.Helpers;

using Microsoft.EntityFrameworkCore;
using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Services;




#if WINDOWS
using Microsoft.Maui.Platform;
using MAUIBLAZORHYBRID.Platforms.Windows;
#endif

namespace MAUIBLAZORHYBRID
{
    public partial class App : Application
    {
        private readonly BackgroundDataService _backgroundDataService;
        public App(AppDbContext dbContext, BackgroundDataService backgroundDataService)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                File.WriteAllText(
                    Path.Combine(FileSystem.AppDataDirectory, "crash.log"),
                    e.ExceptionObject.ToString());
            };

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

            InitializeDatabase(dbContext);

            _backgroundDataService = backgroundDataService;

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            try
            {
                var isRegistered = await SecureStorage.GetAsync("IsAppRegistered");
                if (isRegistered == "true")
                {
                    _backgroundDataService.StartBackgroundTasks();
                }
                else
                {
                    Debug.WriteLine("App not registered. Background service not started.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error checking registration: {ex}");
            }
        }

        //protected override void OnSleep()
        //{
        //    base.OnSleep();

        //    try
        //    {
        //        // Stop background services when app sleeps
        //        _backgroundDataService.StopBackgroundTasks();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        protected override async void OnResume()
        {
            base.OnResume();

            try
            {
                var isRegistered = await SecureStorage.GetAsync("IsAppRegistered");
                if (isRegistered == "true")
                {
                    _backgroundDataService.StartBackgroundTasks();
                }
                else
                {
                    Debug.WriteLine("App not registered. Background service not started.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error resuming: {ex}");
            }
        }



        private async void InitializeDatabase(AppDbContext dbContext)
        {
            try
            {
                // Run initialization synchronously on background thread
                await Task.Run(async () =>
                {
                    await DatabaseInitializer.EnsureDatabaseInitializedAsync(dbContext);
                });
            }
            catch (Exception ex)
            {
                MainPage = new ContentPage
                {
                    Content = new Label { Text = $"Fatal Error: Db initialisation failed {ex.Message}" }
                };

            }
        }

      

        private void HandleGlobalException(Exception ex)
        {
            Debug.WriteLine($"Global exception handler: {ex}");
            // Consider writing to a separate crash log file here
        }


    }
}
