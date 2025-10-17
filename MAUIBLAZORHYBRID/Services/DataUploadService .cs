using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.Infrastructure;
using MAUIBLAZORHYBRID.Services.Interfaces;
using MAUIBLAZORHYBRID.Services.Mappers;
using MAUIBLAZORHYBRID.Services.Upload;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Animations;
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
        private readonly AppState _appState;
        private readonly IApiClient _apiClient;
        private readonly ILogger<DataUploadService> _logger;
        private readonly MappingService _mappingService;

        public DataUploadService(AppDbContext dbContext, IApiClient apiClient,
                               ILogger<DataUploadService> logger, MappingService mappingService, AppState appState)
        {
            _dbContext = dbContext;
            _apiClient = apiClient;
            _logger = logger;
            _mappingService = mappingService;
            _appState = appState;
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
                    .Include(b => b.BillCashiers)
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
                                var datacash = apiResponse.Data.ServerCashIDs;
                            foreach(var cashier in bill.BillCashiers)
                            {
                                var type = cashier.PaymentMode;
                                for (var i= 0;i< datacash.Count;i++)
                                {
                                    if (type == datacash[i].PaymentType)
                                    {
                                        cashier.IsSynced = true;
                                        cashier.ServerHotBillCashId = datacash[i].Hot_Bill_Cash_ID;

                                        break;
                                    }
                                }
                            }
                            

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
                        transfer.ServerTransferId = apiResponse.Data.ServerStockTransferId;
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
        public async Task<bool> HasPendingUploadsStockInwardAsync()
        {
            return await _dbContext.StockInwardMasters
                .AnyAsync(st => !st.IsSynced);
        }

        public async Task<UploadResult> UploadPendingStockInwardsAsync()
        {
            var result = new UploadResult();

            try
            {
                var pendingTransfers = await _dbContext.StockInwardMasters
                    .Include(st => st.StockInwardDetails)
                    .Where(st => !st.IsSynced)
                    .ToListAsync();

                foreach (var transfer in pendingTransfers)
                {
                    var dto = _mappingService.MapToStockInwardDTO(transfer); // You must define this mapping

                    var apiResponse = await _apiClient.PostStockInwardAsync(dto); // Ensure this method is in your IApiClient

                    if (apiResponse.Success && apiResponse.Data != null)
                    {
                        transfer.IsSynced = true;
                        transfer.SockInwardServerId = apiResponse.Data.ServerStockInwardId;
                        transfer.StockInwardSqlDateTime = apiResponse.Data.ProcessedTime;
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

                        result.Errors.Add($"Transfer {transfer.StockInwardDocNo}: {errorMessage}");
                        _logger.LogWarning("Stock inward upload failed: {ErrorMessage}", errorMessage);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading stock inward");
                throw;
            }
        }



        public async Task<bool> HasPendingStockTransferCancelAsync()
        {
            return await _dbContext.StockTransferCancels
                  .AnyAsync(stc => stc.StockTransfer!.IsSynced && !stc.IsSynced);
        }

        public async Task<UploadResult> UploadPendingStockTranferCancelsAsync()
        {
            var result = new UploadResult();

            try
            {
                var pendingTransfers = await _dbContext.StockTransferCancels
                    .Include(st=>st.StockTransfer)
                    .Where(st => st.StockTransfer!.IsSynced && !st.IsSynced)
                    .ToListAsync();

                foreach (var transfer in pendingTransfers)
                {
                    var dto = _mappingService.MapToStockTransferCancelDTO(transfer); // You must define this mapping

                    var apiResponse = await _apiClient.PostStkTrCancelAsync(dto); // Ensure this method is in your IApiClient

                    if (apiResponse.Success && apiResponse.Data != null)
                    {
                        transfer.IsSynced = true;
                        transfer.StkCancelServerId = apiResponse.Data.CancelId;
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

                        result.Errors.Add($"Transfer {transfer.StockTransferId}: {errorMessage}");
                        _logger.LogWarning("Stock transfer cacnel failed: {ErrorMessage}", errorMessage);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading stock transfers cancel");
                throw;
            }
        }



        public async Task<bool> HasPendingBillCashierCancelAsync()
        {
            return await _dbContext.BillCashierCancels
                  .AnyAsync(stc => stc.BillCashier!.IsSynced && !stc.IsSynced);
        }

        public async Task<UploadResult> UploadPendingBillCashierCancelsAsync()
        {
            var result = new UploadResult();

            try
            {
                // 1️⃣ Get all unsynced BillCashierCancels
                var pendingCancels = await _dbContext.BillCashierCancels
                    .Include(c => c.BillCashier)
                    .Where(c => c.BillCashier!.IsSynced && !c.IsSynced)
                    .ToListAsync();

                foreach (var cancel in pendingCancels)
                {
                    // 2️⃣ Map entity -> DTO
                    var dto = _mappingService.MapToBillCashierCancelDTO(cancel);

                    // 3️⃣ Send to API
                    var apiResponse = await _apiClient.PostBillCashierCancelAsync(dto);

                    if (apiResponse.Success && apiResponse.Data != null)
                    {
                        cancel.IsSynced = true;
                        cancel.BillCashierCancelServerId = apiResponse.Data.CancelId;
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

                        result.Errors.Add($"BillCashier {cancel.BillCashierId}: {errorMessage}");
                        _logger.LogWarning("Bill cashier cancel failed: {ErrorMessage}", errorMessage);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading bill cashier cancels");
                throw;
            }
        }
   
        public async Task<bool> HasPendingHotBillCancelAsync()
        {
            return await _dbContext.HotBillCancels
                .AnyAsync(hc => hc.HotBill!.IsSynced && !hc.IsSynced);
        }

        public async Task<UploadResult> UploadPendingHotBillCancelsAsync()
        {
            var result = new UploadResult();

            try
            {
                var pendingCancels = await _dbContext.HotBillCancels
                    .Include(hc => hc.HotBill)
                    .Where(hc => hc.HotBill!.IsSynced && !hc.IsSynced)
                    .ToListAsync();

                foreach (var cancel in pendingCancels)
                {
                    var dto = _mappingService.MapToHotBillCancelDTO(cancel);
                    // 🔸 You’ll need to implement this mapper like MapToStockTransferCancelDTO()

                    var apiResponse = await _apiClient.PostHotBillCancelAsync(dto);
                    // 🔸 Ensure this API client method exists (see next section)

                    if (apiResponse.Success && apiResponse.Data != null)
                    {
                        cancel.IsSynced = true;
                        cancel.HotBillCancelServerId = apiResponse.Data.CancelId; // server ID returned
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

                        result.Errors.Add($"HotBill {cancel.HotBillId}: {errorMessage}");
                        _logger.LogWarning("HotBill cancel upload failed: {ErrorMessage}", errorMessage);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading HotBill cancels");
                throw;
            }
        }


        public async Task<bool> HasPendingHotBillCashierAsync()
        {
            var invalidCashiers = await _dbContext.BillCashiers
       .Include(c => c.HotBill)
       .Where(c =>
           (c.BillCashDate == default ) &&
           c.HotBill != null)
       .ToListAsync();

            if (invalidCashiers.Any())
            {
                foreach (var cashier in invalidCashiers)
                {
                    // Try to fix using related HotBill dates first
                    if (cashier.BillCashDate == default)
                        cashier.BillCashDate = cashier.HotBill!.HotBillDate != default
                            ? cashier.HotBill.HotBillDate
                            : DateTime.UtcNow;

                }

                await _dbContext.SaveChangesAsync();
            }

            var invalidCashiersTime = await _dbContext.BillCashiers
       .Include(c => c.HotBill)
       .Where(c =>
           (c.BillCashTime == default) &&
           c.HotBill != null)
       .ToListAsync();

            if (invalidCashiersTime.Any())
            {
                foreach (var cashier in invalidCashiersTime)
                {
                    // Try to fix using related HotBill dates first
                    if (cashier.BillCashTime == default)
                        cashier.BillCashTime = cashier.HotBill!.HotBillTime != default
                            ? cashier.HotBill.HotBillTime
                            : DateTime.UtcNow;

                }

                await _dbContext.SaveChangesAsync();
            }

            var returnvalue= await _dbContext.BillCashiers
                .Include(d=>d.HotBill)
                .AnyAsync(c => c.HotBill!.IsSynced && !c.IsSynced);

            var returnvalue1 = _dbContext.BillCashiers
                .AnyAsync(c => c.HotBill!.IsSynced && !c.IsSynced);

            return await _dbContext.BillCashiers
                .AnyAsync(c => c.HotBill!.IsSynced && !c.IsSynced);
        }

        public async Task<UploadResult> UploadPendingHotBillCashiersAsync()
        {
            var result = new UploadResult();

            try
            {
                var pendingCashiers = await _dbContext.BillCashiers
                    .Include(c => c.HotBill)
                    .Where(c => c.HotBill!.IsSynced && !c.IsSynced)
                    .ToListAsync();




                var dtoList = pendingCashiers
                    .Select(c => new BillCashierDTO
                    {
                        HotBillCashAppId = c.BillCashierId,
                        HotBillId = c.HotBillId,
                        HotBillDate = c.BillCashDate,
                        HotBillTime = c.BillCashTime,
                        FormType = c.HotBill!.HotBillType,
                        Paymode = c.PaymentMode,
                        TotalAmount = c.TotalAmount,
                        HotBillCashNo = c.HotBillCashNo,
                        HotBillCashPrefix = c.HotBillCashPrefix ?? string.Empty,
                        HotBillCashRefNo = c.HotBillCashRefNo ?? string.Empty,
                        CashierId = _appState.LoggedInUserId,
                        BranchId = _appState.BranchId
                    })
                    .ToList();

                    // 🔹 Call the API
                var apiResponse = await _apiClient.PostBillCashiersAsync(dtoList);

                if (apiResponse.Success && apiResponse.Data != null)
                {
                    // 🔸 Mark as synced
                    foreach (var res in apiResponse.Data)
                    {
                        var entity = pendingCashiers.FirstOrDefault(c => c.BillCashierId == res.HotBillCashAppId);
                        if (entity != null)
                        {
                            entity.IsSynced = true;
                            entity.ServerHotBillCashId = res.Hot_Bill_Cash_ID;
                            result.SuccessCount++;
                        }
                    }
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

                    _logger.LogWarning("HotBill cashier upload failed: {ErrorMessage}", errorMessage);
                }

                await _dbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading HotBill cashiers");
                throw;
            }
        }

    }
}
