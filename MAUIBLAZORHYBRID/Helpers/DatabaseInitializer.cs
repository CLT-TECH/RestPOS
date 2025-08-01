﻿using MAUIBLAZORHYBRID.Data;
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
        private const string DbInitKey = "DbInitializedversion5";

        public static async Task EnsureDatabaseInitializedAsync(AppDbContext dbContext)
        {
            if (!Preferences.ContainsKey(DbInitKey))
            {
                // Delete + re-create/migrate only on first run
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.MigrateAsync();

                Preferences.Set(DbInitKey, true); // Mark as initialized
            }
        }

    }
}
