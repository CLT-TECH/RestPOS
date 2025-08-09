using Blazored.LocalStorage;
using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.Seed;
using MAUIBLAZORHYBRID.Services;
using MAUIBLAZORHYBRID.Services.Interfaces;
using MAUIBLAZORHYBRID.Services.Mappers;
using MAUIBLAZORHYBRID.Services.Sync;
using MAUIBLAZORHYBRID.Services.Upload;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
                });

            //var dbContext = builder.Services.GetService<AppDbContext>();
            //dbContext?.Database.Migrate(); 

            builder.Services.AddTransient<ISyncService, SyncService>();

            builder.Services.AddTransient<DiningspaceSyncService>();

            builder.Services.AddTransient<MasterDataSyncService>();
            builder.Services.AddTransient<OtherMasterSyncService>();
            builder.Services.AddTransient<ItemDataSyncService>();


            builder.Services.AddSingleton<IErrorBoundaryLogger, MauiErrorBoundaryLogger>();

            builder.Services.AddMauiBlazorWebView();

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

            // 2) Apply migrations on startup
            using (var scope = app.Services.CreateScope())
            {
                // Resolve the factory…
                var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AppDbContext>>();

                // Create a context instance and migrate
                using var db = factory.CreateDbContext();
                db.Database.Migrate();

                //#if DEBUG
                //MainSeeder.Seed(db); // Only during development/testing
                //#endif
            }

            return app;
        }
    }
}
