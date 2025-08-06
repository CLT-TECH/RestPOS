using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace MAUIBLAZORHYBRID.Services
{
    public class KOTBillService
    {
        private readonly AppDbContext _db;

        public KOTBillService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<KOTBillDTO>> GetKOTDetailsByTable(int tableId)
        {
            try
            {

                var tableNotBilledOrders = await _db.HotKOTMasters
                    .Include(u=>u.HotBillAgainstKots)
                   .Where(t => t.Tables.HotTabID == tableId)
                   .Where(u => !u.HotBillAgainstKots.Any())
                   .Select(h => new KOTBillOrders
                   {
                       KOTId = h.HotKOTId,
                       Items = h.Items.Select(d => new KOTBillOrderDetailsDTO
                       {
                           ItemId = d.ItemID,
                           Quantity = d.Qty.ToString("F0"),
                           UnitId = d.UnitID,
                           Price = d.DetRate,
                           ItemName = d.UnitID == 0
                           ? _db.BillItems
                               .Where(b => b.itemId == d.ItemID)
                               .Select(b => b.itemName)
                               .FirstOrDefault()
                           : _db.ItemParentChilds
                               .Where(i => i.parentItemId == d.ItemID && i.unitId == d.UnitID)
                               .Select(i => i.childItemname)
                               .FirstOrDefault()
                       }).ToList()
                   })
                   .ToListAsync();

                var diningSpace = await _db.TablesDiningSpaces
                    .Where(tds => tds.tableId == tableId)
                    .Join(_db.DiningSpaces,
                        tds => tds.diningspaceId,
                        ds => ds.diningSpaceId,
                        (tds, ds) => ds)
                    .FirstOrDefaultAsync();

                var result = new KOTBillDTO
                {
                    Orders = tableNotBilledOrders,
                    DiningSpaces = diningSpace ?? new(),
                };


                return Result<KOTBillDTO>.Success(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"KOT data retreive failed : {ex.Message}");
                return Result<KOTBillDTO>.Failure("Failed to load data. Please try again.");
            }

        }
    }
}
