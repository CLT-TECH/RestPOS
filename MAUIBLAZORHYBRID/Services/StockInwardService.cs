using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MAUIBLAZORHYBRID.Services
{
    public class StockInwardService
    {
        private readonly AppDbContext _db;

        public StockInwardService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Result<StockInwardInitDTO>> GetInitData()
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


                var itemparentchildtask = _db.ItemParentChilds
                .Include(u => u.Unit)
                .Include(i => i.category)
                .ToListAsync();

                var baritemtask = _db.BarItems
                              .AsNoTracking()
                               .ToListAsync();

                var unittask = _db.Units
                              .AsNoTracking()
                               .ToListAsync();
                var vendortask = _db.VendorMasters
                             .AsNoTracking()
                              .ToListAsync();

                await Task.WhenAll(billItemTask, itemparentchildtask, baritemtask,unittask, vendortask);

                var result = new StockInwardInitDTO
                {
                    BillItems = billItemTask.Result,
                    VWParentItemChilds = itemparentchildtask.Result,
                    barItems = baritemtask.Result,
                    Units = unittask.Result,
                    Vendors = vendortask.Result,
                };
                return Result<StockInwardInitDTO>.Success(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetInitData failed: {ex.Message}");
                return Result<StockInwardInitDTO>.Failure("Failed to load data. Please try again.");
            }
        }
        public async Task<Result<StockInwardDocDTO>> GetStockInwardDoc()
        {
            try
            {

                int currentMax = await _db.StockInwardMasters.AsNoTracking()
                  .MaxAsync(s => (int?)s.StockInwardSlNo) ?? 0;


                var result = new StockInwardDocDTO
                {
                    SLNO = currentMax + 1,
                    Prefix = ""
                };
                return Result<StockInwardDocDTO>.Success(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetInitData failed: {ex.Message}");
                return Result<StockInwardDocDTO>.Failure("Failed to load data. Please try again.");
            }
        }

    }
}
