using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Infrastructure;
using MAUIBLAZORHYBRID.Services.Interfaces;
using MAUIBLAZORHYBRID.Services.Upload;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class DataUploadService: IDataUploadService
    {
        private readonly AppDbContext _dbContext;
        private readonly IApiClient _apiClient;
        private readonly ILogger<DataUploadService> _logger;

        public DataUploadService(AppDbContext dbContext, IApiClient apiClient,
                               ILogger<DataUploadService> logger)
        {
            _dbContext = dbContext;
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<bool> HasPendingUploadsAsync()
        {
            return await _dbContext.HotBillMasters
                .AnyAsync(b => !b.IsSynced);
        }

        public async Task<UploadResult> UploadPendingDataAsync()
        {
            var result = new UploadResult();

            try
            {
                // Get unsynced bills with their items
                var pendingBills = await _dbContext.HotBillMasters
                    .Include(b => b.HotBillItemDetails)
                    .Include(b => b.HotBillTaxDetails)
                    .Where(b => !b.IsSynced)
                    .ToListAsync();

                foreach (var bill in pendingBills)
                {
                    var apiResponse = await _apiClient.PostBillAsync(bill);

                    if (apiResponse.IsSuccess)
                    {
                        bill.IsSynced = true;
                        bill.ServerHotBillId = apiResponse.ServerId??0;
                        result.SuccessCount++;
                    }
                    else
                    {
                        result.FailedCount++;
                        result.Errors.Add($"Bill {bill.HotBillRefNo}: {apiResponse.ErrorMessage}");
                    }
                }

                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during data upload");
                throw;
            }
        }
    }
}
