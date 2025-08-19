using MAUIBLAZORHYBRID.Config;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.DTO;
using MAUIBLAZORHYBRID.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public class SyncService(
    DiningspaceSyncService diningSpaceSync,
    HttpClient http,
    MasterDataSyncService masterDataSyncService,
    OtherMasterSyncService otherMasterSyncService,
    ItemDataSyncService itemDataSyncService,
    AppState appState,
    ILogger<SyncService> logger
) : ISyncService
    {
        private readonly DiningspaceSyncService _diningSpaceSync = diningSpaceSync;
        private readonly HttpClient _http = http;
        private readonly MasterDataSyncService _masterDataSyncService = masterDataSyncService;
        private readonly OtherMasterSyncService _otherMasterSyncService = otherMasterSyncService;
        private readonly ItemDataSyncService _itemDataSyncService = itemDataSyncService;
        private readonly AppState _appState = appState;

        private readonly ILogger<SyncService> _logger= logger;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task SyncAllAsync(IProgress<SyncResultDTO>? progress = null, CancellationToken ct = default)
        {
            var masters = await SyncBranchesWithMasters(ct);
            progress?.Report(masters);

            var other = await SyncOtherMasters(ct);
            progress?.Report(other);

            var items = await SyncItemData(ct);
            progress?.Report(items);

            var itemRel = await SyncItemParentChildData(ct);
            progress?.Report(itemRel);

            var barStock = await SyncBarItemCounterStock(ct);
            progress?.Report(barStock);
        }



        public async Task<SyncResultDTO> SyncBranchesWithMasters(CancellationToken ct = default)
        {
            _logger.LogInformation("Starting sync: Branches with Masters");
            return await RetryHelper.RetryOnTransientErrors(async () =>
            {
                var sw = Stopwatch.StartNew();
            var result = new SyncResultDTO { Source = "masters" };

            try
            {
                var response = await _http.GetAsync(ApiEndpoints.AppSync.BranchesWithMasters(_appState.BranchId),ct);

                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}";
                    result.ErrorCode = response.StatusCode switch
                    {
                        System.Net.HttpStatusCode.BadRequest => "ClientError",
                        System.Net.HttpStatusCode.Unauthorized => "AuthError",
                        System.Net.HttpStatusCode.Forbidden => "AuthError",
                        System.Net.HttpStatusCode.NotFound => "NotFound",
                        >= System.Net.HttpStatusCode.InternalServerError => "ServerError",
                        _ => "HttpError"
                    };
                    return result;
                }

                    var dtoList = await response.Content.ReadFromJsonAsync<List<MasterDTOS>>(_jsonOptions, ct);
                    if (dtoList is not null)
                    {
                        await _masterDataSyncService.SaveToLocalDbAsync(dtoList, ct);
                        result.CountDownloaded = dtoList?.Count??0;
                    }


                    result.Success = true;
                result.Message = "Sync successful.";
            }
            catch (HttpRequestException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                result.ErrorCode = "NetworkError";
            }
            catch (DbUpdateException ex)
            {
                result.Success = false;
                result.Message = "Database update failed: " + ex.Message;
                result.Exception = ex;
                result.ErrorCode = "DbError";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                result.ErrorCode = "UnknownError";
            }
            finally
            {
                result.DurationMs = (int)sw.ElapsedMilliseconds;
            }

                if (result.Success)
                {
                    _logger.LogInformation("Finished sync: Branches with Masters - {Count} downloaded in {Duration}ms",
                        result.CountDownloaded, result.DurationMs);
                }
                else
                {
                    _logger.LogError(result.Exception,
                        "Sync failed: Branches with Masters - ErrorCode={ErrorCode}, Message={Message}",
                        result.ErrorCode, result.Message);
                }
                return result;
            });
        }

        public async Task<SyncResultDTO> SyncOtherMasters(CancellationToken ct = default)
        {
            _logger.LogInformation("Starting sync: SyncOtherMasters");

            return await RetryHelper.RetryOnTransientErrors(async () =>
            {
                var sw = Stopwatch.StartNew();
            var result = new SyncResultDTO { Source = "other masters" };

            try
            {
                var response = await _http.GetAsync(ApiEndpoints.AppSync.OtherMasters,ct);

                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}";
                    result.ErrorCode = response.StatusCode switch
                    {
                        System.Net.HttpStatusCode.BadRequest => "ClientError",
                        System.Net.HttpStatusCode.Unauthorized => "AuthError",
                        System.Net.HttpStatusCode.Forbidden => "AuthError",
                        System.Net.HttpStatusCode.NotFound => "NotFound",
                        >= System.Net.HttpStatusCode.InternalServerError => "ServerError",
                        _ => "HttpError"
                    };
                    return result;
                }

                    var dtoList = await response.Content.ReadFromJsonAsync<OtherMasterDTO>(_jsonOptions, ct);
                    if (dtoList is not null)
                    {
                        await _otherMasterSyncService.SaveToLocalDbAsync(dtoList, ct);
                        result.CountDownloaded = dtoList?.Tables?.Count??0+
                        dtoList?.TaxMasters?.Count??0+
                        dtoList?.Units?.Count??0+
                        dtoList?.DiningSpaces?.Count??0+
                        dtoList?.DiningSpaceTables?.Count??0 +
                        dtoList?.Categories?.Count??0;
                    }
                    result.Success = true;
                result.Message = "Sync successful.";
            }
            catch (HttpRequestException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                result.ErrorCode = "NetworkError";
            }
            catch (DbUpdateException ex)
            {
                result.Success = false;
                result.Message = "Database update failed: " + ex.Message;
                result.Exception = ex;
                result.ErrorCode = "DbError";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                result.ErrorCode = "UnknownError";
            }
            finally
            {
                result.DurationMs = (int)sw.ElapsedMilliseconds;
            }

                if (result.Success)
                {
                    _logger.LogInformation("Finished sync: SyncOtherMasters- {Count} downloaded in {Duration}ms",
                        result.CountDownloaded, result.DurationMs);
                }
                else
                {
                    _logger.LogError(result.Exception,
                        "Sync failed: SyncOtherMasters - ErrorCode={ErrorCode}, Message={Message}",
                        result.ErrorCode, result.Message);
                }
                return result;
            });
        }

        public async Task<SyncResultDTO> SyncItemData(CancellationToken ct = default)
        {
            _logger.LogInformation("Starting sync: SyncItemData");
            return await RetryHelper.RetryOnTransientErrors(async () =>
            {
                var sw = Stopwatch.StartNew();
            var result = new SyncResultDTO { Source = "itemdata" };

            try
            {
                var response = await _http.GetAsync(ApiEndpoints.AppSync.ItemData, ct);

                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}";
                    result.ErrorCode = response.StatusCode switch
                    {
                        System.Net.HttpStatusCode.BadRequest => "ClientError",
                        System.Net.HttpStatusCode.Unauthorized => "AuthError",
                        System.Net.HttpStatusCode.Forbidden => "AuthError",
                        System.Net.HttpStatusCode.NotFound => "NotFound",
                        >= System.Net.HttpStatusCode.InternalServerError => "ServerError",
                        _ => "HttpError"
                    };
                    return result;
                }

                var dtoList = await response.Content.ReadFromJsonAsync<ItemSyncDTO>(_jsonOptions, ct);
                if (dtoList is not null)
                {
                    await _itemDataSyncService.SaveToLocalDbAsync(dtoList, ct);
                    result.CountDownloaded = dtoList?.baritems?.Count??0;
                }


                result.Success = true;
                result.Message = "Sync successful.";
            }
            catch (HttpRequestException ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                result.ErrorCode = "NetworkError";
            }
            catch (DbUpdateException ex)
            {
                result.Success = false;
                result.Message = "Database update failed: " + ex.Message;
                result.Exception = ex;
                result.ErrorCode = "DbError";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                result.ErrorCode = "UnknownError";
            }
            finally
            {
                result.DurationMs = (int)sw.ElapsedMilliseconds;
            }
                if (result.Success)
                {
                    _logger.LogInformation("Finished sync: SyncItemData - {Count} downloaded in {Duration}ms",
                        result.CountDownloaded, result.DurationMs);
                }
                else
                {
                    _logger.LogError(result.Exception,
                        "Sync failed: SyncItemData - ErrorCode={ErrorCode}, Message={Message}",
                        result.ErrorCode, result.Message);
                }
                return result;
            });
        }

        public async Task<SyncResultDTO> SyncItemParentChildData(CancellationToken ct = default)
        {
            _logger.LogInformation("Starting sync: SyncItemParentChildData");
            return await RetryHelper.RetryOnTransientErrors(async () =>
            {
                var sw = Stopwatch.StartNew();
            var result = new SyncResultDTO { Source = "itemparentchild" };

            try
            {
                var response = await _http.GetAsync(ApiEndpoints.AppSync.ItemParentChild, ct);

                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}";
                    result.ErrorCode = response.StatusCode switch
                    {
                        System.Net.HttpStatusCode.BadRequest => "ClientError",
                        System.Net.HttpStatusCode.Unauthorized => "AuthError",
                        System.Net.HttpStatusCode.Forbidden => "AuthError",
                        System.Net.HttpStatusCode.NotFound => "NotFound",
                        >= System.Net.HttpStatusCode.InternalServerError => "ServerError",
                        _ => "HttpError"
                    };
                    return result;
                }

                var dtoList = await response.Content.ReadFromJsonAsync<List<ItemParentChildDTO>>(_jsonOptions, ct);
                if (dtoList is not null)
                {
                    await _itemDataSyncService.SaveToLocalDbItemParntChildAsync(dtoList, ct);
                    result.CountDownloaded = dtoList?.Count??0;
                }
                    result.Success = true;
                result.Message = "Sync successful.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                result.ErrorCode = "DbError";
            }
            finally
            {
                result.DurationMs = (int)sw.ElapsedMilliseconds;
            }
                if (result.Success)
                {
                    _logger.LogInformation("Finished sync: SyncItemParentChildData - {Count} downloaded in {Duration}ms",
                        result.CountDownloaded, result.DurationMs);
                }
                else
                {
                    _logger.LogError(result.Exception,
                        "Sync failed: SyncItemParentChildData - ErrorCode={ErrorCode}, Message={Message}",
                        result.ErrorCode, result.Message);
                }
                return result;
            });
        }

        public async Task<SyncResultDTO> SyncBarItemCounterStock(CancellationToken ct = default)
        {
            _logger.LogInformation("Starting sync: SyncBarItemCounterStock");
            return await RetryHelper.RetryOnTransientErrors(async () =>
            {
                var sw = Stopwatch.StartNew();
            var result = new SyncResultDTO { Source = "stockforcounter" };

            try
            {
                var response = await _http.GetAsync(ApiEndpoints.AppSync.StockForCounter(_appState.CounterId),ct);

                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}";
                    result.ErrorCode = response.StatusCode switch
                    {
                        System.Net.HttpStatusCode.BadRequest => "ClientError",
                        System.Net.HttpStatusCode.Unauthorized => "AuthError",
                        System.Net.HttpStatusCode.Forbidden => "AuthError",
                        System.Net.HttpStatusCode.NotFound => "NotFound",
                        >= System.Net.HttpStatusCode.InternalServerError => "ServerError",
                        _ => "HttpError"
                    };
                    return result;
                }

                var dtoList = await response.Content.ReadFromJsonAsync<DAStockDTO>(_jsonOptions, ct);
                if (dtoList is not null)
                {
                        await _itemDataSyncService.SaveToLocalDbBarItemStock(dtoList, ct);
                        result.CountDownloaded = dtoList?.BarItemStock?.Count??0;
                }
                result.Success = true;
                result.Message = "Sync successful.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                result.ErrorCode = "DbError";
            }
            finally
            {
                result.DurationMs = (int)sw.ElapsedMilliseconds;
            }
                if (result.Success)
                {
                    _logger.LogInformation("Finished sync: SyncBarItemCounterStock - {Count} downloaded in {Duration}ms",
                        result.CountDownloaded, result.DurationMs);
                }
                else
                {
                    _logger.LogError(result.Exception,
                        "Sync failed: SyncBarItemCounterStock - ErrorCode={ErrorCode}, Message={Message}",
                        result.ErrorCode, result.Message);
                }
                return result;
            });
        }
    }

}
