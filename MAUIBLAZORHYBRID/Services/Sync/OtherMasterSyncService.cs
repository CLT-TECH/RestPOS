using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public class OtherMasterSyncService(
    IDbContextFactory<AppDbContext> dbFactory,
    ILogger<OtherMasterSyncService> logger,
    AppState appState)
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory = dbFactory;
        private readonly ILogger<OtherMasterSyncService> _logger = logger;
        private readonly AppState _appState = appState;


        public async Task SaveToLocalDbAsync(OtherMasterDTO mastersothermastersdtos, CancellationToken ct = default)
        {
            await using var db = await _dbFactory.CreateDbContextAsync(ct);
            await using var transaction = await db.Database.BeginTransactionAsync(ct);
            try
            {
                foreach (var category in mastersothermastersdtos.Categories ?? Enumerable.Empty<CategoryDTO>())
                {
                    var cat = new Category
                    {
                        catId = category.id,
                        catName = category.name
                    };

                    var existing = await db.Categories.FindAsync(cat.catId, ct);
                    if (existing == null)
                    {
                        await db.Categories.AddAsync(cat, ct);
                    }
                }

                foreach (var unit in mastersothermastersdtos.Units ?? Enumerable.Empty<UnitDTO>())
                {
                    var unitnew = new Unit
                    {
                        unitId = unit.id,
                        unitName = unit.name,
                    };
                    if (unitnew.unitId >= 0)
                    {
                        var existingunit = await db.Units.FindAsync(unitnew.unitId, ct);
                        if (existingunit == null)
                        {
                            await db.Units.AddAsync(unitnew, ct);
                        }
                    }
                }
                foreach (var diningDto in mastersothermastersdtos.DiningSpaces ?? Enumerable.Empty<DiningSpaceDTO>())
                {
                    var dining = new DiningSpace
                    {
                        diningSpaceId = diningDto.Id,
                        diningSpaceName = diningDto.Name,
                    };

                    var existingdining = await db.DiningSpaces.FindAsync(dining.diningSpaceId, ct);

                    if (existingdining == null)
                    {
                        db.DiningSpaces.Add(dining);
                    }
                }
                foreach (var objtable in mastersothermastersdtos.Tables ?? Enumerable.Empty<TablesDTO>())
                {
                    var table = new Table
                    {
                        tableId = objtable.id,
                        tableName = objtable.name,
                        noOfSeats = objtable.noofseats
                    };


                    var existingdining = await db.Tables.FindAsync(objtable.id, ct);

                    if (existingdining == null)
                    {
                        db.Tables.Add(table);
                    }
                }
                foreach (var objdstable in mastersothermastersdtos.DiningSpaceTables ?? Enumerable.Empty<DiningSpaceTablesDTO>())
                {
                    var dstable = new TableDiningSpace
                    {
                        Id = objdstable.id,
                        tableId = objdstable.tableid,
                        diningspaceId = objdstable.diningspaceid,
                        branchId = objdstable.branchid,
                    };
                    var AppBranchId = await SecureStorage.GetAsync("AppBranchId");

                    int value = int.TryParse(AppBranchId, out var temp) ? temp : 0;

                    if (objdstable.branchid== value)
                    {
                        var existingdining = await db.TablesDiningSpaces.FindAsync(objdstable.id, ct);

                        if (existingdining == null)
                        {
                            db.TablesDiningSpaces.Add(dstable);
                        }

                    }

                   
                }


                foreach (var objTax in mastersothermastersdtos.TaxMasters ?? Enumerable.Empty<TaxMaster>())
                {
                    var tax = new TaxMaster
                    {
                        TaxId = objTax.TaxId,
                        ApplicableType = objTax.ApplicableType,
                        TaxCalcId = objTax.TaxCalcId,
                        TaxCode = objTax.TaxCode,
                        TaxGroupId = objTax.TaxGroupId,
                        TaxName = objTax.TaxName
                    };
                    var existingtax = await db.TaxMasters.FindAsync(objTax.TaxId, ct);
                    if (existingtax == null)
                    {
                        db.TaxMasters.Add(tax);
                    }
                }

                foreach (var obj in mastersothermastersdtos.VendorMasters ?? Enumerable.Empty<VendorMasterDTO>())
                {
                    var vendor = new VendorMaster
                    {
                        VendorId = obj.VendorId,
                        VendorName = obj.VendorName
                    };
                    var existingvendor = await db.VendorMasters.FindAsync(obj.VendorId, ct);
                    if (existingvendor == null)
                    {
                        db.VendorMasters.Add(vendor);
                    }
                }
                await db.SaveChangesAsync(ct);  
                await transaction.CommitAsync(ct);
            }
            catch (Exception ex) {
                await transaction.RollbackAsync(ct);
                db.ChangeTracker.Clear(); // important: clear bad entities so they don't retry
                _logger.LogError(ex, "Error saving OtherMasterSyncService batch");
                throw;
            }
        }
    }
}
