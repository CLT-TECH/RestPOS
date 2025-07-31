using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public class SyncService : ISyncService
    {
        private readonly DiningspaceSyncService _diningSpaceSync;
        private readonly HttpClient _http;
        private readonly MasterDataSyncService _masterDataSyncService;
        private readonly OtherMasterSyncService _otherMasterSyncService;
        private readonly ItemDataSyncService _itemDataSyncService;


        public SyncService(
            DiningspaceSyncService diningSpaceSync,
            HttpClient http, MasterDataSyncService masterDataSyncService, OtherMasterSyncService otherMasterSyncService, ItemDataSyncService itemDataSyncService)
        {
            _diningSpaceSync = diningSpaceSync;
            _http = http;
            _masterDataSyncService = masterDataSyncService;
            _otherMasterSyncService = otherMasterSyncService;
            _itemDataSyncService = itemDataSyncService;
        }

        public async Task SyncAllAsync()
        {
            await SyncBranchesWithMasters();
            await SyncOtherMasters();
            await SyncItemData();
            await SyncItemParentChildData();
        }

        

        public async Task<SyncResultDTO> SyncBranchesWithMasters()
        {
            var result = new SyncResultDTO { Source = "masters" };

            try
            {
                var response = await _http.GetAsync("api/AppSync/branches-with-masters");

                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}";
                    return result;
                }

                var json = await response.Content.ReadAsStringAsync();
                var dtoList = JsonSerializer.Deserialize<List<MasterDTOS>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (dtoList is not null)
                    await _masterDataSyncService.SaveToLocalDbAsync(dtoList);

                result.Success = true;
                result.Message = "Sync successful.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                return result;
            }

        }

        public async Task<SyncResultDTO> SyncOtherMasters()
        {
            var result = new SyncResultDTO { Source = "other masters" };

            try
            {
                var response = await _http.GetAsync("api/AppSync/othermasters");

                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}";
                    return result;
                }

                var json = await response.Content.ReadAsStringAsync();
                var dtoList = JsonSerializer.Deserialize<OtherMasterDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (dtoList is not null)
                    await _otherMasterSyncService.SaveToLocalDbAsync(dtoList);

                result.Success = true;
                result.Message = "Sync successful.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                return result;
            }

        }

        public async Task<SyncResultDTO> SyncItemData()
        {
            var result = new SyncResultDTO { Source = "itemdata" };

            try
            {
                var response = await _http.GetAsync("api/AppSync/itemdata");

                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}";
                    return result;
                }

                var json = await response.Content.ReadAsStringAsync();
                var dtoList = JsonSerializer.Deserialize<ItemSyncDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (dtoList is not null)
                    await _itemDataSyncService.SaveToLocalDbAsync(dtoList);

                result.Success = true;
                result.Message = "Sync successful.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                return result;
            }

        }

        public async Task<SyncResultDTO> SyncItemParentChildData()
        {
            var result = new SyncResultDTO { Source = "itemparentchild" };

            try
            {
                var response = await _http.GetAsync("api/AppSync/itemparentchild");

                if (!response.IsSuccessStatusCode)
                {
                    result.Success = false;
                    result.Message = $"HTTP {(int)response.StatusCode}: {response.ReasonPhrase}";
                    return result;
                }

                var json = await response.Content.ReadAsStringAsync();
                var dtoList = JsonSerializer.Deserialize<List<ItemParentChildDTO>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (dtoList is not null)
                    await _itemDataSyncService.SaveToLocalDbItemParntChildAsync(dtoList);

                result.Success = true;
                result.Message = "Sync successful.";
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
                return result;
            }

        }
    }

}
