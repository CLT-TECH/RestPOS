using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Infrastructure;
using MAUIBLAZORHYBRID.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class HotBillSaveService:IHotBillSaveService
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<HotBillSaveService> _logger;

        public HotBillSaveService(AppDbContext dbContext, ILogger<HotBillSaveService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Result<HotBillMaster>> SaveHotBillAsync(HotBillMaster billMaster)
        {
            string json = JsonSerializer.Serialize(billMaster, new JsonSerializerOptions
            {
                WriteIndented = true // Makes it pretty-printed
            });

            // 1. Basic validation
            if (billMaster == null)
                return Result<HotBillMaster>.Failure("Bill master cannot be null.");

            if (billMaster.HotBillType < 0)
                return Result<HotBillMaster>.Failure("Bill type must be specified.");

            // 2. Generate bill number if new bill
            // Generate bill number if new bill
            if (billMaster.HotBillNo == 0)
            {
                int currentMax = await _dbContext.HotBillMasters
                    .AsNoTracking()
                    .MaxAsync(b => (int?)b.HotBillNo) ?? 0;

                billMaster.HotBillNo = currentMax + 1;
                billMaster.HotBillRefNo = $"{billMaster.HotBillPrefix}{billMaster.HotBillNo}";


                int currentMaxCashier = await _dbContext.BillCashiers
                    .AsNoTracking()
                    .MaxAsync(b => (int?)b.HotBillCashNo) ?? 0;


                foreach (var cashier in billMaster.BillCashiers)
                {
                    currentMaxCashier++;
                    cashier.HotBillCashNo = currentMaxCashier;
                    cashier.HotBillCashPrefix = "";
                    cashier.HotBillCashRefNo = currentMaxCashier.ToString();
                }

            }
            else
            {
                // Check for existing bill by number (not ID)
                bool billExists = await _dbContext.HotBillMasters
                    .AnyAsync(b => b.HotBillNo == billMaster.HotBillNo &&
                                 b.HotBillPrefix == billMaster.HotBillPrefix);

                if (billExists)
                    return Result<HotBillMaster>.Failure($"Bill {billMaster.HotBillRefNo} already exists.");
            }

            // 3. Start transaction
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (billMaster.HotBillId == 0)
                {
                    _dbContext.HotBillMasters.Add(billMaster);
                }
                else
                {
                    //_dbContext.HotBillMasters.Update(billMaster);
                    await transaction.RollbackAsync();
                    return Result<HotBillMaster>.Failure($"Bill Cannot Be Updated As Of Now");
                }

                // 5. Save changes
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                

                return Result<HotBillMaster>.Success(billMaster);
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                _logger.LogError(dbEx, "Database error saving bill");
                return Result<HotBillMaster>.Failure($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error saving bill");
                return Result<HotBillMaster>.Failure($"Error saving bill: {ex.Message}");
            }
        }
    }
}
