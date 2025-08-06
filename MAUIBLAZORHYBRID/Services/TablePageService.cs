using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using ZXing;

namespace MAUIBLAZORHYBRID.Services
{
    public class TablePageService
    {
        private readonly AppDbContext _db;

        public TablePageService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<TablePageDTO> GetInitData()
        {

            var conb = _db.Database.GetConnectionString();
            var tablesTask = _db.Tables
                .AsNoTracking()
                .ToListAsync();
            var diningspacesTask = _db.DiningSpaces
                .AsNoTracking()
                .ToListAsync();
            var tablesdiningspacesTask = _db.TablesDiningSpaces
                .AsNoTracking()
                .ToListAsync();
            var nonbilledTableTask = _db.HotKOTTables
                .Where(t => t.HotKOTMaster != null && !t.HotKOTMaster.HotBillAgainstKots.Any())
                .Select(h => h.Tables)   // projection to the Table
                .Distinct()
                .ToListAsync();


            var checklist4 = await _db.HotKOTTables
                .Where(t => t.HotKOTMaster != null && t.HotKOTMaster.HotBillAgainstKots.Any())
                .Select(h => h.Tables)   // projection to the Table
                .Distinct()
                .ToListAsync();

            await Task.WhenAll(tablesTask, diningspacesTask, tablesdiningspacesTask, nonbilledTableTask);

            var result = new TablePageDTO
            {
                Tables = tablesTask.Result,
                DiningSpaces = diningspacesTask.Result,
                TablesDiningSpaces = tablesdiningspacesTask.Result,
                NonBilledTables = nonbilledTableTask.Result
            };
            return result;
        }

        public async Task<TableOrdersDTO> GetOrderDetailsByTable(int tableId)
        {

            var tableNotBilledOrders = await _db.HotKOTMasters
               .Include(u=>u.HotBillAgainstKots)
               .Where(t => t.Tables.HotTabID == tableId)
               .Where(u => !u.HotBillAgainstKots.Any())
               .Select(h =>new OrderDetailsDTO { 
                        OrderNo=h.HotKOTNo.ToString(),
                        OrderedAt=h.HotKOTTime,
                        Items=h.Items.Select(d => new OrderItemDTO
                        {
                            ItemId = d.ItemID,
                            Quantity=d.Qty.ToString("F0"),
                            ItemName= d.UnitID == 0
                            ? _db.BillItems
                                .Where(b => b.itemId == d.ItemID)
                                .Select(b => b.itemName)
                                .FirstOrDefault()
                            : _db.ItemParentChilds
                                .Where(i => i.parentItemId == d.ItemID && i.unitId==d.UnitID)
                                .Select(i => i.childItemname)
                                .FirstOrDefault()
                        }).ToList()
               })       
               .ToListAsync();


            return new TableOrdersDTO { Orders = tableNotBilledOrders };
        }
    }
}
