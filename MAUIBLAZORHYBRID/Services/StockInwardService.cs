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

        public async Task<Result<List<StockInwardMaster>>> GetStockInwardMastersAsync()
        {
            try
            {
                // Fetch only the master records — no includes unless required
                var stockInwardMasters = await _db.StockInwardMasters
                    .Include(d=>d.Vendor)
                    .AsNoTracking()
                    .OrderByDescending(m => m.StockInwardDate)
                    .ToListAsync();

                return Result<List<StockInwardMaster>>.Success(stockInwardMasters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetStockInwardMastersAsync failed: {ex.Message}");
                return Result<List<StockInwardMaster>>.Failure("Failed to load stock inward master data. Please try again.");
            }
        }

        public async Task<Result<List<StockInwardInDetailDTO>>> GetStockInwardDetailsById(int inwardId)
        {
            try
            {
                // Load only the detail rows for the given master ID
                var details = await _db.StockInwardDetails
                    .Where(d => d.StockInwardId == inwardId)
                    .Include(d => d.Unit)
                    .Include(d => d.BarItem)
                    .ThenInclude(c=>c.ItemUnits)
                    .AsNoTracking()
                    .Select(d => new StockInwardInDetailDTO
                    {
                        ItemName = d.BarItem.itemName,
                        Qty = d.InwardQty,
                        Unit = d.Unit.unitName,
                        UnitType = d.Unit.unitId,
                        ItemUnit = d.BarItem.ItemUnits != null
                                ? d.BarItem.ItemUnits.Select(u => u.Unit.unitName).FirstOrDefault() ?? string.Empty
                                : string.Empty
                    })
                    .ToListAsync();

                if (details == null || details.Count == 0)
                    return Result<List<StockInwardInDetailDTO>>.Failure("No details found for this inward entry.");

                return Result<List<StockInwardInDetailDTO>>.Success(details);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetStockInwardDetailsById failed: {ex.Message}");
                return Result<List<StockInwardInDetailDTO>>.Failure("Failed to load inward details. Please try again.");
            }
        }

    }
}
