using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public class ItemDataSyncService(
     IDbContextFactory<AppDbContext> dbFactory,
     ILogger<ItemDataSyncService> logger)
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory = dbFactory;
        private readonly ILogger<ItemDataSyncService> _logger = logger;

        public async Task SaveToLocalDbAsync(ItemSyncDTO itemdata, CancellationToken ct = default)
        {

            await using var db = await _dbFactory.CreateDbContextAsync(ct);
            await using var transaction = await db.Database.BeginTransactionAsync(ct);
            try
            {
                var icount = 0;
                foreach (var item in itemdata.items ?? Enumerable.Empty<ItemMasterDTO>())
                {

                    var categoryresult = await db.Categories
                         .Include(c => c.SubCategories)
                         .FirstOrDefaultAsync(c => c.catId == item.catid, ct);
                    var itemresult = new BillItem
                    {
                        itemId = item.id,
                        itemName = item.name,
                        itemType = item.type,
                        CatId = item.catid,
                        category = categoryresult ?? new()
                    };

                    if (itemresult.itemId > 0 && item.catid > 0)
                    {
                        var existingitem = await db.BillItems.FindAsync(itemresult.itemId, ct);
                        if (existingitem == null)
                        {
                            await db.BillItems.AddAsync(itemresult, ct);
                            icount++;
                        }
                    }
                }


                foreach (var itemunit in itemdata.itemunits ?? Enumerable.Empty<ItemUnitDTO>())
                {
                    var unitmaster = await db.Units.FindAsync(itemunit.unitid, ct);
                    var itembyid = await db.BillItems.FindAsync(itemunit.itemid, ct);

                    if (itembyid != null && unitmaster != null)
                    {
                        var itemunitresult = new BillItemUnit
                        {
                            itemUnitId = itemunit.id,
                            itemId = itemunit.itemid,
                            unitId = itemunit.unitid,
                            Unit = unitmaster,
                            Item = itembyid
                        };

                        if (itemunitresult.itemUnitId > 0)
                        {
                            var existingitemunit = await db.BillItemUnits.FindAsync(itemunitresult.itemUnitId, ct);
                            if (existingitemunit == null)
                            {
                                await db.BillItemUnits.AddAsync(itemunitresult, ct);


                            }

                        }
                    }
                }


                foreach (var itemrate in itemdata.diningspacerates ?? Enumerable.Empty<DiningSpaceItemRateDTO>())
                {
                    var diningspaceresult = await db.DiningSpaces.FindAsync(itemrate.diningspaceid, ct);
                    var itemresult = await db.BillItems.FindAsync(itemrate.itemid, ct);
                    if (diningspaceresult != null && itemresult != null)
                    {
                        var itemrateresult = new DiningSpaceItemRate
                        {
                            id = itemrate.id,
                            itemId = itemrate.itemid,
                            diningSpaceId = itemrate.diningspaceid,
                            itemRate = itemrate.rate,
                            diningSpace = diningspaceresult,
                            item = itemresult
                        };

                        if (itemrateresult.id > 0)
                        {
                            var existingitemrate = await db.DiningSpaceItemRates.FindAsync(itemrateresult.id, ct);
                            if (existingitemrate == null)
                            {
                                await db.DiningSpaceItemRates.AddAsync(itemrateresult, ct);

                            }
                        }
                    }
                }


                foreach (var baritem in itemdata.baritems ?? Enumerable.Empty<BarItem>())
                {
                    var britemresult = await db.BarItems.FirstOrDefaultAsync(b => b.BarItemId == baritem.BarItemId, ct);
                    if (britemresult == null)
                    {
                        var baritemmaster = new BarItem
                        {
                            BarItemId = baritem.BarItemId,
                            BarItemBaseUnitId = baritem.BarItemBaseUnitId,
                            BarItemCode = baritem.BarItemCode,
                            BarItemInventoryUnitId = baritem.BarItemInventoryUnitId,
                            BarItemName = baritem.BarItemName,
                            MainBarItem = baritem.MainBarItem,
                            MainBarItemID = baritem.MainBarItemID
                        };
                        await db.BarItems.AddAsync(baritemmaster, ct);
                    }
                   

                }
                await db.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                db.ChangeTracker.Clear(); // important: clear bad entities so they don't retry
                _logger.LogError(ex, "Error saving ItemDataSyncService ,SaveToLocalDbAsync batch");
                throw;
            }

        }

        public async Task SaveToLocalDbItemParntChildAsync(List<ItemParentChildDTO> itemdata, CancellationToken ct = default)
        {
            await using var db = await _dbFactory.CreateDbContextAsync(ct);
            await using var transaction = await db.Database.BeginTransactionAsync(ct);
            try
            {
                foreach (var item in itemdata ?? Enumerable.Empty<ItemParentChildDTO>())
                {
                    var unitmaster = await db.Units.FindAsync(item.unitid, ct);
                    var categoryresult = await db.Categories.FindAsync(item.catid, ct);

                    var itemresult = new VWItemParentChild
                    {
                        parentItemId = item.parentitemid,
                        childItemId = item.childitemid,
                        parentItemname = item.parentitemname,
                        parentItemcode = item.parentitemcode,
                        childItemname = item.childitemname,
                        childItemcode = item.childitemcode,
                        itemtype = item.itemtype,
                        unitId = item.unitid,
                        Unit = unitmaster,
                        CatId = item.catid,
                        category = categoryresult??new()
                    };

                    await db.ItemParentChilds.AddAsync(itemresult, ct);
                }
                await db.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                db.ChangeTracker.Clear(); // important: clear bad entities so they don't retry
                _logger.LogError(ex, "Error saving ItemDataSyncService ,SaveToLocalDbItemParntChildAsync batch");
                throw;
            }


        }

        public async Task SaveToLocalDbBarItemStock(DAStockDTO stockdata, CancellationToken ct = default)
        {
            await using var db = await _dbFactory.CreateDbContextAsync(ct);
            await using var transaction = await db.Database.BeginTransactionAsync(ct);
            try
            {
                foreach (var item in stockdata.BarItemStock ?? Enumerable.Empty<DABarItemStockCounter>())
                {

                    var barItemExists = await db.BarItems.AnyAsync(b => b.BarItemId == item.BarItemId, ct);
                    var counterExists = await db.BillStations.AnyAsync(c => c.billStationId == item.CounterId, ct);



                    var itemresult = new BarItemStockCounter
                    {
                        BarItemId = item.BarItemId,
                        CounterId = item.CounterId,
                        Stock = item.Stock,
                    };
                    var britemresult = await db.BarItemCounterStocks.FirstOrDefaultAsync(b => b.BarItemId == item.BarItemId, ct);

                    if (britemresult == null)
                    {
                        await db.BarItemCounterStocks.AddAsync(itemresult, ct);
                    }
                    else
                    {
                        britemresult.Stock = item.Stock;
                    }
                }
                await db.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync(ct);
                db.ChangeTracker.Clear();

                // Check if it's a FK violation
                if (dbEx.InnerException != null)
                {
                    var sqlEx = dbEx.InnerException;

                    // For SQL Server, the message contains table/column info
                    var message = sqlEx.Message;
                    _logger.LogError(dbEx, "Foreign key violation detected: {Message}", message);

                    // Optional: parse the message to extract table/column names
                }

                throw; // rethrow if needed
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                db.ChangeTracker.Clear(); // important: clear bad entities so they don't retry
                _logger.LogError(ex, "Error saving ItemDataSyncService ,SaveToLocalDbBarItemStock batch");
                throw;
            }

        }
    }
}
