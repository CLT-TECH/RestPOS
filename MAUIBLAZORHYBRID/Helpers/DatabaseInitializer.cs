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
        private const string DbInitKey = "DbInitializedv0.2.1";

        public static async Task EnsureDatabaseInitializedAsync(AppDbContext dbContext)
        {
            if (!Preferences.ContainsKey(DbInitKey))
            {
                    await dbContext.Database.EnsureDeletedAsync();
                    await dbContext.Database.MigrateAsync();
                    Preferences.Set(DbInitKey, true); // Mark as initialized
            }
        }



    }
}
