using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MAUIBLAZORHYBRID.Services
{
    public class StockReportService
    {
        private readonly AppDbContext _db;
        private readonly AppState _appState;

        public StockReportService(AppDbContext db, AppState appState)
        {
            _db = db;
            _appState = appState;
        }

        public async Task<Result<List<StockReportDTO>>> GetCounterStockAsync(int counterId)
        {
            try
            {
                var stock = await _db.BarItems
                 .Select(barItem => new StockReportDTO
                 {
                     ItemId = barItem.BarItemId,
                     ItemName = barItem.BarItemName,
                     Stock = barItem.BarItemStockCounters
                         .Where(c => c.CounterId == counterId)
                         .Select(c => Convert.ToInt32(c.Stock))
                         .FirstOrDefault()
                 })
                 .AsNoTracking()
                 .ToListAsync();

                return Result<List<StockReportDTO>>.Success(stock);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCounterStockAsync failed: {ex.Message}");
                return Result<List<StockReportDTO>>.Failure("Failed to load counter stock.");
            }
        }

        // Base godown stock
        public async Task<Result<List<StockReportDTO>>> GetGodownStockAsync(int godownId)
        {
            try
            {
                var stock = await _db.BarItems
                 .Select(barItem => new StockReportDTO
                 {
                     ItemId = barItem.BarItemId,
                     ItemName = barItem.BarItemName,
                     Stock = barItem.BarItemGodownStocks
                         .Where(g => g.GodownId == godownId)
                         .Sum(g => (int?)g.Stock) ?? 0  // Handle null with nullable sum
                 })
                 .AsNoTracking()
                 .ToListAsync();

                return Result<List<StockReportDTO>>.Success(stock);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetGodownStockAsync failed: {ex.Message}");
                return Result<List<StockReportDTO>>.Failure("Failed to load godown stock.");
            }
        }

        // Pending transfers (not synced)
        public async Task<Result<List<PendingStockDTO>>> GetPendingTransfersAsync()
        {
            try
            {
                var transfers = await _db.StockTransfers
                      .Include(t => t.CancelInfo)
                    .Where(t => !t.IsSynced && t.CancelInfo == null && t.FromType==1)
                    .SelectMany(t => t.StockTransferDetails)
                    .GroupBy(d => new { d.MainBarItemId, d.UnitId })
                    .Select(g => new
                    {
                        g.Key.MainBarItemId,
                        g.Key.UnitId,
                      Quantity = (decimal)g.Sum(x => (double)x.Quantity)
                    })
                    .ToListAsync();

                var barItems = await _db.BarItems
                    .Select(b => new
                    {
                        b.BarItemId,
                        b.MainBarItemID,
                        b.BarItemBaseUnitId
                    })
                    .ToListAsync();

                var barItemMap = barItems.ToDictionary(
    key => (key.MainBarItemID, key.BarItemBaseUnitId),
    val => val.BarItemId
);

                var groupedTransfers = transfers
                    .Select(g => new PendingStockDTO
                    {
                        MainBarItemId = barItemMap.TryGetValue((g.MainBarItemId, g.UnitId), out var id) ? id : 0,
                        Quantity = g.Quantity
                    })
                    .Where(r => r.MainBarItemId != 0)
                    .ToList();

                return Result<List<PendingStockDTO>>.Success(groupedTransfers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPendingTransfersAsync failed: {ex.Message}");
                return Result<List<PendingStockDTO>>.Failure("Failed to load pending transfers.");
            }
        }

        public async Task<Result<List<PendingStockDTO>>> GetPendingTransfersToGodownAsync()
        {
            try
            {
                var transfers = await _db.StockTransfers
                      .Include(t => t.CancelInfo)
                    .Where(t => !t.IsSynced && t.CancelInfo == null && t.ToType == 1 && t.ToGodownId == _appState.GodownId)
                    .SelectMany(t => t.StockTransferDetails)
                    .GroupBy(d => new { d.MainBarItemId, d.UnitId })
                    .Select(g => new
                    {
                        g.Key.MainBarItemId,
                        g.Key.UnitId,
                        Quantity = (decimal)g.Sum(x => (double)x.Quantity)
                    })
                    .ToListAsync();

                var barItems = await _db.BarItems
                    .Select(b => new
                    {
                        b.BarItemId,
                        b.MainBarItemID,
                        b.BarItemBaseUnitId
                    })
                    .ToListAsync();

                var barItemMap = barItems.ToDictionary(
    key => (key.MainBarItemID, key.BarItemBaseUnitId),
    val => val.BarItemId
);

                var groupedTransfers = transfers
                    .Select(g => new PendingStockDTO
                    {
                        MainBarItemId = barItemMap.TryGetValue((g.MainBarItemId, g.UnitId), out var id) ? id : 0,
                        Quantity = g.Quantity
                    })
                    .Where(r => r.MainBarItemId != 0)
                    .ToList();

                return Result<List<PendingStockDTO>>.Success(groupedTransfers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPendingTransfersAsync failed: {ex.Message}");
                return Result<List<PendingStockDTO>>.Failure("Failed to load pending transfers.");
            }
        }





        public async Task<Result<List<PendingStockDTO>>> GetCancelledPendingSyncAsync()
        {
            try
            {
                var transfers = await _db.StockTransfers
                    .Include(t => t.StockTransferDetails)
                    .Include(t => t.CancelInfo)
                    .Where(t =>
                        t.IsSynced &&                    // transfer already synced
                        t.CancelInfo != null &&          // has cancel info
                        !t.CancelInfo.IsSynced &&          // cancel not yet synced
                        t.FromType==1
                    )
                    .SelectMany(t => t.StockTransferDetails)
                    .GroupBy(d => new { d.MainBarItemId, d.UnitId })
                    .Select(g => new
                    {
                        g.Key.MainBarItemId,
                        g.Key.UnitId,
                      Quantity = (decimal)g.Sum(x => (double)x.Quantity)
                    })
                    .ToListAsync();

                var barItems = await _db.BarItems
                    .Select(b => new
                    {
                        b.BarItemId,
                        b.MainBarItemID,
                        b.BarItemBaseUnitId
                    })
                    .ToListAsync();

                var barItemMap = barItems.ToDictionary(
    key => (key.MainBarItemID, key.BarItemBaseUnitId),
    val => val.BarItemId
);

                var groupedTransfers = transfers
                    .Select(g => new PendingStockDTO
                    {
                        MainBarItemId = barItemMap.TryGetValue((g.MainBarItemId, g.UnitId), out var id) ? id : 0,
                        Quantity = g.Quantity
                    })
                    .Where(r => r.MainBarItemId != 0)
                    .ToList();




                return Result<List<PendingStockDTO>>.Success(groupedTransfers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCancelledPendingSyncAsync failed: {ex.Message}");
                return Result<List<PendingStockDTO>>.Failure("Failed to load cancelled pending sync stock.");
            }
        }


        public async Task<Result<List<PendingStockDTO>>> GetCancelledPendingSyncToGodownAsync()
        {
            try
            {
                var transfers = await _db.StockTransfers
                    .Include(t => t.StockTransferDetails)
                    .Include(t => t.CancelInfo)
                    .Where(t =>
                        t.IsSynced &&                    // transfer already synced
                        t.CancelInfo != null &&          // has cancel info
                        !t.CancelInfo.IsSynced &&          // cancel not yet synced
                        t.ToType == 1 && t.ToGodownId == _appState.GodownId
                    )
                    .SelectMany(t => t.StockTransferDetails)
                    .GroupBy(d => new { d.MainBarItemId, d.UnitId })
                    .Select(g => new
                    {
                        g.Key.MainBarItemId,
                        g.Key.UnitId,
                        Quantity = (decimal)g.Sum(x => (double)x.Quantity)
                    })
                    .ToListAsync();

                var barItems = await _db.BarItems
                    .Select(b => new
                    {
                        b.BarItemId,
                        b.MainBarItemID,
                        b.BarItemBaseUnitId
                    })
                    .ToListAsync();

                var barItemMap = barItems.ToDictionary(
    key => (key.MainBarItemID, key.BarItemBaseUnitId),
    val => val.BarItemId
);

                var groupedTransfers = transfers
                    .Select(g => new PendingStockDTO
                    {
                        MainBarItemId = barItemMap.TryGetValue((g.MainBarItemId, g.UnitId), out var id) ? id : 0,
                        Quantity = g.Quantity
                    })
                    .Where(r => r.MainBarItemId != 0)
                    .ToList();




                return Result<List<PendingStockDTO>>.Success(groupedTransfers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCancelledPendingSyncAsync failed: {ex.Message}");
                return Result<List<PendingStockDTO>>.Failure("Failed to load cancelled pending sync stock.");
            }
        }

        public async Task<Result<List<PendingStockDTO>>> GetPendingInwardsAsync()
        {
            try
            {
                // Load BillItemUnits into a dictionary first
                var billUnits = await _db.BillItemUnits
                    .Where(b => b.unitId != 10) // Exclude unitId = 10
                    .GroupBy(b => b.itemId)
                    .Select(g => g.First())
                    .ToDictionaryAsync(b => b.itemId, b => b.caseContains);

                // Load StockInwardDetails first
                var inwardDetails = await _db.StockInwardMasters
                    .Where(i => !i.IsSynced)
                    .SelectMany(i => i.StockInwardDetails)
                    .ToListAsync(); // now in memory

                // Compute pending stock using LINQ-to-Objects
                var inwards = inwardDetails
                .GroupBy(d => d.BarItemId) // Group by BarItemId only
                .Select(g =>
                {
                    var quantity = g.Sum(x =>
                    {
                        // Check if item exists in billUnits (using only itemId)
                        billUnits.TryGetValue(x.BarItemId, out var caseContains);

                        // If unitId is 10, use caseContains value, else use 1
                        if (x.UnitId == 10)
                        {
                            // Use the caseContains value from billUnits
                            if (caseContains == 0) caseContains = 1;
                            return x.InwardQty * caseContains;
                        }
                        else
                        {
                            // For unitId != 10, use multiplier of 1
                            return x.InwardQty * 1;
                        }
                    });

                    return new PendingStockDTO
                    {
                        MainBarItemId = g.Key, // Just the BarItemId
                        Quantity = quantity
                    };
                })
                .ToList();


                return Result<List<PendingStockDTO>>.Success(inwards);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPendingInwardsAsync failed: {ex.Message}");
                return Result<List<PendingStockDTO>>.Failure("Failed to load pending inwards.");
            }
        }

        public async Task<Result<List<PendingStockDTO>>> GetPendingTransfersAsyncByCounter(int counterId)
        {
            try
            {
                var transfers = await _db.StockTransfers
                      .Include(t => t.CancelInfo)
                    .Where(t => !t.IsSynced && t.ToCounterId == counterId && t.CancelInfo == null)
                    .SelectMany(t => t.StockTransferDetails)
 .GroupBy(d => new { d.MainBarItemId, d.UnitId })
                    .Select(g => new
                    {
                        g.Key.MainBarItemId,
                        g.Key.UnitId,
                      Quantity = (decimal)g.Sum(x => (double)x.Quantity)
                    })
                    .ToListAsync();

                var barItems = await _db.BarItems
                    .Select(b => new
                    {
                        b.BarItemId,
                        b.MainBarItemID,
                        b.BarItemBaseUnitId
                    })
                    .ToListAsync();

                var barItemMap = barItems.ToDictionary(
    key => (key.MainBarItemID, key.BarItemBaseUnitId),
    val => val.BarItemId
);

                var groupedTransfers = transfers
                    .Select(g => new PendingStockDTO
                    {
                        MainBarItemId = barItemMap.TryGetValue((g.MainBarItemId, g.UnitId), out var id) ? id : 0,
                        Quantity = g.Quantity
                    })
                    .Where(r => r.MainBarItemId != 0)
                    .ToList();

                return Result<List<PendingStockDTO>>.Success(groupedTransfers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPendingTransfersAsync failed: {ex.Message}");
                return Result<List<PendingStockDTO>>.Failure("Failed to load pending transfers.");
            }
        }


        public async Task<Result<List<PendingStockDTO>>> GetCancelledPendingSyncByCounterAsync(int counterId)
        {
            try
            {
                var transfers = await _db.StockTransfers
                    .Include(t => t.StockTransferDetails)
                    .Include(t => t.CancelInfo)
                    .Where(t =>
                        t.IsSynced &&                   // master already synced
                        t.ToCounterId == counterId &&    // for specific counter
                        t.CancelInfo != null &&          // has cancel record
                        !t.CancelInfo.IsSynced           // cancel not yet synced
                    )
                    .SelectMany(t => t.StockTransferDetails)
                    .GroupBy(d => new { d.MainBarItemId, d.UnitId })
                    .Select(g => new
                    {
                        g.Key.MainBarItemId,
                        g.Key.UnitId,
                      Quantity = (decimal)g.Sum(x => (double)x.Quantity)
                    })
                    .ToListAsync();

                var barItems = await _db.BarItems
                    .Select(b => new
                    {
                        b.BarItemId,
                        b.MainBarItemID,
                        b.BarItemBaseUnitId
                    })
                    .ToListAsync();

                var barItemMap = barItems.ToDictionary(
    key => (key.MainBarItemID, key.BarItemBaseUnitId),
    val => val.BarItemId
);

                var groupedTransfers = transfers
                    .Select(g => new PendingStockDTO
                    {
                        MainBarItemId = barItemMap.TryGetValue((g.MainBarItemId, g.UnitId), out var id) ? id : 0,
                        Quantity = g.Quantity
                    })
                    .Where(r => r.MainBarItemId != 0)
                    .ToList();

                return Result<List<PendingStockDTO>>.Success(groupedTransfers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCancelledPendingSyncByCounterAsync failed: {ex.Message}");
                return Result<List<PendingStockDTO>>.Failure("Failed to load cancelled pending sync transfers.");
            }
        }



        public async Task<Result<List<PendingStockDTO>>> GetPendingBillsAsync()
        {
            try
            {
                //var bills = await _db.HotBillMasters
                //    .Where(b => !b.IsSynced)
                //    .SelectMany(b => b.HotBillItemDetails)
                //    .Join(
                //        _db.ItemParentChilds,                  // join with parent-child mapping
                //        bill => bill.ItemId,                   // child
                //        ipc => ipc.parentItemId,                     // parent item id
                //        (bill, ipc) => new { bill, ipc }
                //    )
                //    .GroupBy(x => x.ipc.parentItemId)          // group by MainBarItemId
                //    .Select(g => new PendingStockDTO
                //    {
                //        MainBarItemId = g.Key,
                //        Quantity = (decimal)g.Sum(x => (double)x.bill.Qty)
                //    })
                //    .ToListAsync();


                var billss = await _db.HotBillMasters
                    .Where(b => !b.IsSynced)
                    .SelectMany(b => b.HotBillItemDetails)
                    .Join(
                        _db.BarItems,
                        bill => new { bill.ItemId, bill.UnitId },
                        bar => new { ItemId = bar.MainBarItemID, UnitId = bar.BarItemBaseUnitId },
                        (bill, bar) => new { bill, bar }
                    )
                    .GroupBy(x => new { x.bill.ItemId, x.bill.UnitId })
                    .Select(g => new PendingStockDTO
                    {
                        MainBarItemId = g.First().bar.BarItemId,
                        Quantity = (decimal)g.Sum(x => (double)x.bill.Qty)
                    })
                    .ToListAsync();

                return Result<List<PendingStockDTO>>.Success(billss);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPendingBillsAsync failed: {ex.Message}");
                return Result<List<PendingStockDTO>>.Failure("Failed to load pending bills.");
            }
        }

        public async Task<Result<List<Unit>>> GetUnitsAsync()
        {
            try
            {
                var units = await _db.Units
                    .Where(u=>u.unitId>0 && u.unitId!=10)
                    .AsNoTracking()
                    .ToListAsync();

                return Result<List<Unit>>.Success(units);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUnitsAsync failed: {ex.Message}");
                return Result<List<Unit>>.Failure("Failed to load units.");
            }
        }
        public async Task<Result<List<BillItemUnit>>> GetBillItemUnitsAsync()
        {
            try
            {
                var billItemUnits = await _db.BillItemUnits
                    .Where(bu => bu.itemId > 0 && bu.unitId > 0) // optional filter
                    .AsNoTracking()
                    .ToListAsync();

                return Result<List<BillItemUnit>>.Success(billItemUnits);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetBillItemUnitsAsync failed: {ex.Message}");
                return Result<List<BillItemUnit>>.Failure("Failed to load bill item units.");
            }
        }



        public async Task<Result<List<BillItem>>> GetBillItemsAsync()
        {
            try
            {
                var billItems = await _db.BillItems
                    .Where(b => b.itemType == 2)
                    .AsNoTracking()
                    .ToListAsync();

                return Result<List<BillItem>>.Success(billItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetBillItemsAsync failed: {ex.Message}");
                return Result<List<BillItem>>.Failure("Failed to load bill items.");
            }
        }

        public async Task<Result<List<VWItemParentChild>>> GetItemParentChildAsync()
        {
            try
            {
                var mappings = await _db.ItemParentChilds
                    .AsNoTracking()
                    .ToListAsync();

                return Result<List<VWItemParentChild>>.Success(mappings);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetItemParentChildAsync failed: {ex.Message}");
                return Result<List<VWItemParentChild>>.Failure("Failed to load item parent-child mappings.");
            }
        }
    }





    }
