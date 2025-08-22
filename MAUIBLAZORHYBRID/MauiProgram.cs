using Blazored.LocalStorage;
using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Services;
using MAUIBLAZORHYBRID.Services.Deactivate;
using MAUIBLAZORHYBRID.Services.Interfaces;
using MAUIBLAZORHYBRID.Services.Mappers;
using MAUIBLAZORHYBRID.Services.Sync;
using MAUIBLAZORHYBRID.Services.Upload;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using MudBlazor.Services;
using MudExtensions.Services;

namespace MAUIBLAZORHYBRID
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                }).ConfigureLifecycleEvents(events =>
                {
                #if WINDOWS
                    events.AddWindows(windows => windows
                        .OnWindowCreated(window =>
                        {
                            window.ExtendsContentIntoTitleBar = true;
                            var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                            var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
                            if (appWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter p)
                            {
                                p.Maximize();
                            }

                        }));
                    #endif
                });

            //var dbContext = builder.Services.GetService<AppDbContext>();
            //dbContext?.Database.Migrate(); 

            

            builder.Services.AddTransient<ISyncService, SyncService>();

            builder.Services.AddTransient<DiningspaceSyncService>();
            builder.Services.AddTransient<MasterDataSyncService>();
            builder.Services.AddTransient<OtherMasterSyncService>();
            builder.Services.AddTransient<ItemDataSyncService>();


            builder.Services.AddSingleton<IErrorBoundaryLogger, MauiErrorBoundaryLogger>();

            builder.Services.AddSingleton<IPreferences>(Preferences.Default);
           
            builder.Logging.AddDebug();

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<DeviceStateService>();
            builder.Services.AddSingleton<IDeviceStatusService, DeviceStatusService>();


            builder.Services.AddSingleton<LoginService>();
            builder.Services.AddSingleton<AppState>();
            builder.Services.AddSingleton<TaxCalculationService>();


            builder.Services.AddScoped<PosPageService>();
            builder.Services.AddScoped<TablePageService>();
            builder.Services.AddScoped<KOTService>();
            builder.Services.AddScoped<KOTBillService>();
            builder.Services.AddScoped<StockTransferService>();
            

            builder.Services.AddScoped<IHotKOTSaveService, HotKOTSaveService>();
            builder.Services.AddScoped<IHotBillSaveService, HotBillSaveService>();
            builder.Services.AddScoped<IStockTransferSaveService, StockTransferSaveService>();

            builder.Services.AddSingleton<IApiClient, ApiClient>();
            builder.Services.AddScoped<IDataUploadService, DataUploadService>();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddMudExtensions();
            builder.Services.AddMudServices();

            builder.Services.AddSingleton<BackgroundDataService>();


#if DEBUG

            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddDbContextFactory<AppDbContext>(options =>
                    options.UseSqlite(DbConfig.ConnectionString));
            builder.Services.AddSingleton<LoginService>();
            builder.Services.AddSingleton<SessionService>();
            builder.Services.AddSingleton<LoadingService>();
            builder.Services.AddSingleton<MappingService>();

            builder.Services.AddScoped(sp =>
            new HttpClient
            {
                //BaseAddress = new Uri("http://localhost:5108") // Your API base URL
                BaseAddress = new Uri("https://hotelerp.azurewebsites.net") // Your API base URL
            });
                //BaseAddress = new Uri("http://hotelerp.azurewebsites.net") // Your API base URL


            var app = builder.Build();

            
            return app;
        }
    }
}
