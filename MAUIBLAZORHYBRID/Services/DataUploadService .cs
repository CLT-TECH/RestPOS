using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Infrastructure;
using MAUIBLAZORHYBRID.Services.Interfaces;
using MAUIBLAZORHYBRID.Services.Mappers;
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
        private readonly MappingService _mappingService;

        public DataUploadService(AppDbContext dbContext, IApiClient apiClient,
                               ILogger<DataUploadService> logger, MappingService mappingService)
        {
            _dbContext = dbContext;
            _apiClient = apiClient;
            _logger = logger;
            _mappingService = mappingService;
        }

        public async Task<bool> HasPendingUploadsBillsAsync()
        {
            return await _dbContext.HotBillMasters
                .AnyAsync(b => !b.IsSynced);
        }
        public async Task<bool> HasPendingUploadsKOTAsync()
        {
            return await  _dbContext.HotKOTMasters
       .AnyAsync(k => !k.IsSynced);

        }

        public async Task<bool> HasPendingUploadsBillKOTAsync()
        {
            return await _dbContext.HotBillAgainstKots
        .Include(h => h.HotKot)
        .AnyAsync(h => !h.HotKot.IsSynced); ;

        }

        public async Task<UploadResult> UploadPendingKOTsAsync()
        {
            try
            {
                var result = new UploadResult();
                var pendingKOTs = await _dbContext.HotKOTMasters
                    .Include(k => k.Items)
                    .Include(k => k.Tables)
                    .Where(k => !k.IsSynced)
                    .ToListAsync();

                foreach (var kot in pendingKOTs)
                {
                    var kotDto = _mappingService.MapToHotKOTMasterDTO(kot);
                    var apiResponse = await _apiClient.PostKOTAsync(kotDto);

                    if (apiResponse.Success && apiResponse.Data != null)
                    {
                        kot.IsSynced = true;
                        kot.ServerKOTId = apiResponse.Data.ServerKOTId;
                        result.SuccessCount++;
                    }
                    else
                    {
                        result.FailedCount++;
                        var errorMessage = apiResponse.Message;
                        if (apiResponse.Errors?.Any() == true)
                        {
                            errorMessage += " | " + string.Join(" | ",
                                apiResponse.Errors.Select(e => $"{e.Code}: {e.Description}"));
                        }
                        result.Errors.Add($"KOT {kot.HotKOTRefNo}: {errorMessage}");
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

        public async Task<UploadResult> UploadPendingDataAsync()
        {
            var result = new UploadResult();

            try
            {
                // Get unsynced bills with their items
                var pendingBills = await _dbContext.HotBillMasters
                    .Include(b => b.HotBillItemDetails)
                    .Include(b => b.HotBillTaxDetails)
                    .Include(b => b.HotBillAgainstKots)
                    .ThenInclude(d=>d.HotKot)
                    .Where(b => !b.IsSynced)
                    .ToListAsync();

                foreach (var bill in pendingBills)
                {

                    var billDto = _mappingService.MapToBillMasterDTO(bill);

                    var failFalg = 0;

                    if (billDto.Hot_Bill_Type == 1)
                    {
                        if (!(billDto.KOTs != null && billDto.KOTs.Any(k => k.Hot_KOT_ID > 0)))
                        {
                            failFalg = 1;
                        }
                    }
                    if(billDto.Items==null) failFalg = 2;


                    if (failFalg > 0) {

                        result.FailedCount++;
                        var errorMessage1 = "HOT Kot bill not found";
                        if (failFalg==2)
                             errorMessage1 = "HOT  bill detail not found";


                        result.Errors.Add($"Bill {bill.HotBillRefNo}: {errorMessage1}");

                        _logger.LogWarning("Bill upload failed: {ErrorMessage}", errorMessage1);
                    }
                    else
                    {



                        var apiResponse = await _apiClient.PostBillAsync(billDto);

                        if (apiResponse.Success && apiResponse.Data != null)
                        {
                            // Success case
                            bill.IsSynced = true;
                            bill.ServerHotBillId = apiResponse.Data.ServerBillId;
                            result.SuccessCount++;
                        }
                        else
                        {
                            // Error case
                            result.FailedCount++;

                            // Combine all error messages
                            var errorMessage = apiResponse.Message;
                            if (apiResponse.Errors?.Any() == true)
                            {
                                errorMessage += " | " + string.Join(" | ",
                                    apiResponse.Errors.Select(e => $"{e.Code}: {e.Description}"));
                            }

                            result.Errors.Add($"Bill {bill.HotBillRefNo}: {errorMessage}");

                            _logger.LogWarning("Bill upload failed: {ErrorMessage}", errorMessage);
                        }
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

        public async Task<bool> HasPendingUploadsStockTransferAsync()
        {
            return await _dbContext.StockTransfers
                .AnyAsync(st => !st.IsSynced);
        }

        public async Task<UploadResult> UploadPendingStockTransfersAsync()
        {
            var result = new UploadResult();

            try
            {
                var pendingTransfers = await _dbContext.StockTransfers
                    .Include(st => st.StockTransferDetails)
                    .Where(st => !st.IsSynced)
                    .ToListAsync();

                foreach (var transfer in pendingTransfers)
                {
                    var dto = _mappingService.MapToStockTransferDTO(transfer); // You must define this mapping

                    var apiResponse = await _apiClient.PostStockTransferAsync(dto); // Ensure this method is in your IApiClient

                    if (apiResponse.Success && apiResponse.Data != null)
                    {
                        transfer.IsSynced = true;
                        transfer.ServerTransferId = apiResponse.Data.ServerTransferId;
                        result.SuccessCount++;
                    }
                    else
                    {
                        result.FailedCount++;

                        var errorMessage = apiResponse.Message;
                        if (apiResponse.Errors?.Any() == true)
                        {
                            errorMessage += " | " + string.Join(" | ",
                                apiResponse.Errors.Select(e => $"{e.Code}: {e.Description}"));
                        }

                        result.Errors.Add($"Transfer {transfer.RefNo}: {errorMessage}");
                        _logger.LogWarning("Stock transfer upload failed: {ErrorMessage}", errorMessage);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading stock transfers");
                throw;
            }
        }


    }
}
