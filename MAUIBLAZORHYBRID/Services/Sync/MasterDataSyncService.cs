using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Windows.UI;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public class MasterDataSyncService(
      IDbContextFactory<AppDbContext> dbFactory,
      ILogger<MasterDataSyncService> logger)
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory = dbFactory;
        private readonly ILogger<MasterDataSyncService> _logger = logger;

        public async Task SaveToLocalDbAsync(List<MasterDTOS> branchDtos, CancellationToken ct = default)
        {
            await using var db = await _dbFactory.CreateDbContextAsync(ct);
            await using var transaction = await db.Database.BeginTransactionAsync(ct);
            try
            {
                foreach (var branchDto in branchDtos ?? Enumerable.Empty<MasterDTOS>())
                {
                    var branch = new BranchMaster
                    {
                        branchId = branchDto.Id,
                        branchName = branchDto.Name,
                        CounterId=branchDto.MachineCounterId,
                        GodownId = branchDto.GodownId
                    };

                    if (branchDto.Id > 0)
                    {
                        var existing = await db.BranchMasters.FindAsync(branch.branchId, ct);
                        if (existing == null)
                        {
                            await db.BranchMasters.AddAsync(branch, ct);
                        }
                        else 
                        {
                            existing.BranchGodownId = branchDto.BranchGodownId;
                        }
                            foreach (var billstatDto in branchDto.BillStation ?? Enumerable.Empty<BillStationDTO>())
                            {
                                var billstation = new BillStation
                                {
                                    billStationId = billstatDto.Id,
                                    billStationName = billstatDto.Name,
                                    branchId = branchDto.Id
                                };

                                var existingsatation = await db.BillStations.FindAsync(billstation.billStationId, ct);

                                if (existingsatation == null)
                                {
                                    db.BillStations.Add(billstation);
                                }
                            }

                        foreach (var obj in branchDto.GodownMasters ?? Enumerable.Empty<GodownMasterDTO>())
                        {
                            var godown = new GodownMaster
                            {
                                GodownId = obj.GodownId,
                                GodownName = obj.GodownName,
                            };

                            var existingsatation = await db.GodownMasters.FindAsync(godown.GodownId, ct);

                            if (existingsatation == null)
                            {
                                db.GodownMasters.Add(godown);
                            }
                        }



                        //await db.BranchTaxSettings.ExecuteDeleteAsync(ct);
                        //await db.SaveChangesAsync(ct);

                        foreach (var obj in branchDto.TaxSetting ?? Enumerable.Empty<BranchTaxSettingsDTO>())
                        {
                            var taxSetting = new BranchTaxSetting
                            {
                                BranchId = obj.BranchId,
                                BillingType = obj.BillingType,
                                ItemType = obj.ItemType,
                                TaxId = obj.TaxId,
                                TaxPer = obj.TaxPer
                            };


                            var taxSettingExist = await db.BranchTaxSettings
                               .FirstOrDefaultAsync(t =>
                                   t.BranchId == obj.BranchId &&
                                   t.BillingType == obj.BillingType &&
                                   t.ItemType == obj.ItemType &&
                                   t.TaxId == obj.TaxId);


                            if (taxSettingExist != null)
                            {
                                taxSettingExist.TaxPer = obj.TaxPer;
                            }
                            else
                            {
                                db.BranchTaxSettings.Add(taxSetting);
                            }
                        }
                    }
                }
                await db.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                db.ChangeTracker.Clear(); // important: clear bad entities so they don't retry
                _logger.LogError(ex, "Error saving MasterDataSyncService batch");
                throw;
            }

        }
    }
}
