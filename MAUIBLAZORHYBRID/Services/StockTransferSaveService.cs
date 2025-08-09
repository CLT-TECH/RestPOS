using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Infrastructure;
using MAUIBLAZORHYBRID.Services.Interfaces;
using MAUIBLAZORHYBRID.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class StockTransferSaveService : IStockTransferSaveService
    {
        private readonly AppDbContext _dbContext;

        public StockTransferSaveService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<StockTransfer>> SaveStockTransferAsync(StockTransfer stockTransfer)
        {
            try
            {
                if (stockTransfer == null)
                    return Result<StockTransfer>.Failure("StockTransfer object cannot be null");

                // Check if the record exists locally by LocalId
                var existing = await _dbContext.StockTransfers
                    .Include(s => s.StockTransferDetails)
                    .FirstOrDefaultAsync(s => s.Id == stockTransfer.Id);

                // Auto-generate transfer number if needed
                int currentMax = await _dbContext.StockTransfers.AsNoTracking()
                    .MaxAsync(s => (int?)s.Id) ?? 0;

                stockTransfer.Prefix = "";
                stockTransfer.StkTrSlNo = currentMax + 1;
                stockTransfer.RefNo = (stockTransfer.StkTrSlNo).ToString()+ stockTransfer.Prefix;

                await using var transaction = await _dbContext.Database.BeginTransactionAsync();

                try
                {
                    if (existing == null)
                    {
                        _dbContext.StockTransfers.Add(stockTransfer);
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return Result<StockTransfer>.Failure($"Transfer with LocalId {stockTransfer.Id} already exists.");
                    }

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Result<StockTransfer>.Success(stockTransfer);
                }
                catch (Exception innerEx)
                {
                    await transaction.RollbackAsync();
                    return Result<StockTransfer>.Failure("Transaction error: " + innerEx.Message);
                }
            }
            catch (Exception ex)
            {
                return Result<StockTransfer>.Failure($"Error saving StockTransfer: {ex.Message}");
            }
        }
    }
}
