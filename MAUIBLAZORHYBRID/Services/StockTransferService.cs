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
                var godownstask = _db.GodownMasters
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

                await Task.WhenAll(billItemTask, countertask, itemparentchildtask, baritemtask,godownstask);

                var result = new StockTransferInitDTO
                {
                    BillItems=billItemTask.Result,
                    Counters=countertask.Result,
                    VWParentItemChilds=itemparentchildtask.Result,
                    barItems=baritemtask.Result,
                    Godowns= godownstask.Result,
                };
                return Result<StockTransferInitDTO>.Success(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetInitData failed: {ex.Message}");
                return Result<StockTransferInitDTO>.Failure("Failed to load data. Please try again.");
            }
        }


        public async Task<Result<List<StockTransfer>>> GetStockTransferMastersAsync()
        {
            try
            {
                // Fetch only the master records — no includes unless required
                var stockTransferMaster = await _db.StockTransfers
                    .Include(d => d.CancelInfo)
                    .Where(d => d.CancelInfo == null)
                    .AsNoTracking()
                    .OrderByDescending(m => m.TransferDate)
                    .ToListAsync();

                return Result<List<StockTransfer>>.Success(stockTransferMaster);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetStockInwardMastersAsync failed: {ex.Message}");
                return Result<List<StockTransfer>>.Failure("Failed to load stock inward master data. Please try again.");
            }
        }

        public async Task<Result<List<StockTransferItem>>> GetStockTransferDetailsById(int transferID)
        {
            try
            {
                // Load only the detail rows for the given master ID
                var details = await _db.StockTransferItems
                    .Where(d => d.StkTr.Id == transferID)
                    .AsNoTracking()
                    .ToListAsync();

                if (details == null || details.Count == 0)
                    return Result<List<StockTransferItem>>.Failure("No details found for this inward entry.");

                return Result<List<StockTransferItem>>.Success(details);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetStockInwardDetailsById failed: {ex.Message}");
                return Result<List<StockTransferItem>>.Failure("Failed to load inward details. Please try again.");
            }
        }

        public async Task<Result<bool>> SaveStockTransferCancelAsync(StockTransferCancel cancel)
        {
            try
            {
                await _db.StockTransferCancels.AddAsync(cancel);
                await _db.SaveChangesAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SaveStockTransferCancelAsync failed: {ex.Message}");
                return Result<bool>.Failure("Failed to save cancellation. Please try again.");
            }
        }
    }
}
