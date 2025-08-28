using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Interfaces
{
    internal interface IStockInwardSaveService
    {
        Task<Result<StockInwardMaster>> SaveStockInwardAsync(StockInwardMaster stockInward);

    }

    public class StockInwardSaveService : IStockInwardSaveService
    {
        private readonly AppDbContext _dbContext;

        public StockInwardSaveService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<StockInwardMaster>> SaveStockInwardAsync(StockInwardMaster master)
        {
            try
            {
                if (master == null)
                    return Result<StockInwardMaster>.Failure("StockTransfer object cannot be null");

                // Check if the record exists locally by LocalId
                var existing = await _dbContext.StockInwardMasters
                    .Include(s => s.StockInwardDetails)
                    .FirstOrDefaultAsync(s => s.StockInwardId == master.StockInwardId);

               
                await using var transaction = await _dbContext.Database.BeginTransactionAsync();

                try
                {
                    if (existing == null)
                    {
                        _dbContext.StockInwardMasters.Add(master);
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return Result<StockInwardMaster>.Failure($"Transfer with LocalId {master.StockInwardId} already exists.");
                    }

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Result<StockInwardMaster>.Success(master);
                }
                catch (Exception innerEx)
                {
                    await transaction.RollbackAsync();
                    return Result<StockInwardMaster>.Failure("Transaction error: " + innerEx.Message);
                }
            }
            catch (Exception ex)
            {
                return Result<StockInwardMaster>.Failure($"Error saving StockTransfer: {ex.Message}");
            }
        }
    }
}
