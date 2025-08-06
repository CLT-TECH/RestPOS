using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Infrastructure;
using MAUIBLAZORHYBRID.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MAUIBLAZORHYBRID.Services
{
    public class HotKOTSaveService : IHotKOTSaveService
    {
        private readonly AppDbContext _dbContext;

        public HotKOTSaveService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<HotKOT>> SaveHotKOTAsync(HotKOT hotKOT)
        {
            try
            {
                if (hotKOT == null)
                    return Result<HotKOT>.Failure("Object cannot be null");

                // Check if the record exists locally by AppKOTId
                var existing = await _dbContext.HotKOTMasters
                    .Include(h => h.Tables)
                    .Include(h => h.Items)
                    .FirstOrDefaultAsync(h => h.HotKOTId == hotKOT.HotKOTId);

                int currentMax = await _dbContext.HotKOTMasters.AsNoTracking()
        .MaxAsync(h => (int?)h.HotKOTNo) ?? 0;
                hotKOT.HotKOTNo = currentMax + 1;
                hotKOT.HotKOTRefNo = (currentMax + 1).ToString();

                await using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    if (existing == null)
                    {
                        _dbContext.HotKOTMasters.Add(hotKOT);
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return Result<HotKOT>.Failure($"KOT with Id {hotKOT.HotKOTId} already exists.");
                    }
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Result<HotKOT>.Success(hotKOT);
                }
                catch (Exception extran)
                {
                    await transaction.RollbackAsync();
                    return Result<HotKOT>.Failure($"Transaction Error");
                }


            }
            catch (Exception ex)
            {
                // Log error as needed
                return Result<HotKOT>.Failure($"Error saving HotKOT: {ex.Message}");
            }
        }
    }

}
