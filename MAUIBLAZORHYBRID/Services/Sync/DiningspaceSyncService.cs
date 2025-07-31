using MAUIBLAZORHYBRID.Data;
using MAUIBLAZORHYBRID.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MAUIBLAZORHYBRID.Components.Pages.Posneewnew;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public class DiningspaceSyncService
    {
        private readonly AppDbContext _db;

        public DiningspaceSyncService(AppDbContext db)
        {
            _db = db;
        }
        public async Task SyncAsync(string json)
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (root.ValueKind != JsonValueKind.Array)
                return;
            foreach (var element in root.EnumerateArray())
            {
                // Manually extract fields
                var id = element.GetProperty("diningSpaceId").GetInt32();
                var name = element.GetProperty("diningSpaceName").GetString();
                var branchId = element.GetProperty("branchId").GetInt32();

                var existing = await _db.DiningSpaces.FindAsync(id);
                if (existing == null)
                {
                    _db.DiningSpaces.Add(new DiningSpace
                    {
                        diningSpaceId = id,
                        diningSpaceName = name ?? "",
                    });
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}
