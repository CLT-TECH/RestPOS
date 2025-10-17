using MAUIBLAZORHYBRID.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Helpers
{
    public class DatabaseInitializer
    {
        private const string DbInitKey = "DbInitializedv0.3.3";



        public static async Task EnsureDatabaseInitializedAsync(AppDbContext dbContext)
        {
            if (!Preferences.ContainsKey(DbInitKey))
            {
                SecureStorage.Remove("AppMachineId");
                SecureStorage.Remove("AppMachineName");
                SecureStorage.Remove("AppUsername");
                SecureStorage.Remove("AppPassword");
                SecureStorage.Remove("AppManagerID");
                SecureStorage.Remove("AppBranchId");
                SecureStorage.Remove("IsLoggedIn");
                SecureStorage.Remove("LastLogin");
                SecureStorage.Remove("AppPermanentlyDeactivated");

                await SecureStorage.SetAsync("AppPermanentlyDeactivated", "false");

                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.MigrateAsync();
                Preferences.Set(DbInitKey, true); // Mark as initialized
            }
            else
            {
                await dbContext.Database.MigrateAsync();
            }
        }

    }
}
