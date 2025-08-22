using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class StockTransferService
    {
        private readonly AppDbContext _db;

        public StockTransferService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<StockTransferInitDTO>> GetInitData()
        {
            try
            {
                var billItemTask = _db.BillItems
                .Include(u => u.ItemUnits)
                    .ThenInclude(u => u.Unit)
                .Include(i => i.category)
                .Include(r => r.DiningSpaceItemRates)
                                .AsNoTracking()
                .ToListAsync();

                var countertask = _db.BillStations
                                .AsNoTracking()
                                 .ToListAsync();

                var itemparentchildtask = _db.ItemParentChilds
                .Include(u => u.Unit)
                .Include(i => i.category)
                .ToListAsync();

                var baritemtask = _db.BarItems
                               .Include(d=>d.BarItemGodownStocks)
                              .AsNoTracking()
                               .ToListAsync();

                await Task.WhenAll(billItemTask, countertask, itemparentchildtask, baritemtask);

                var result = new StockTransferInitDTO
                {
                    BillItems=billItemTask.Result,
                    Counters=countertask.Result,
                    VWParentItemChilds=itemparentchildtask.Result,
                    barItems=baritemtask.Result,
                };
                return Result<StockTransferInitDTO>.Success(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetInitData failed: {ex.Message}");
                return Result<StockTransferInitDTO>.Failure("Failed to load data. Please try again.");
            }
        }
    }
}
