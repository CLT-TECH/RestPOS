using MAUIBLAZORHYBRID.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Deactivate
{
    public interface IDeviceStatusService
    {
        Task<bool> IsDeviceActiveAsync(CancellationToken token = default);
    }


    public class DeviceStatusService : IDeviceStatusService
    {
        private readonly HttpClient _httpClient;
        private const string DeactivatedKey = "AppPermanentlyDeactivated";

        public DeviceStatusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<bool> IsPermanentlyDeactivatedAsync()
        {
            var flag = await SecureStorage.GetAsync(DeactivatedKey);
            return !string.IsNullOrEmpty(flag) && flag == "true";
        }

        private async Task SetPermanentlyDeactivatedAsync()
        {
            await SecureStorage.SetAsync(DeactivatedKey, "true");
        }


        public async Task<bool> IsDeviceActiveAsync(CancellationToken token = default)
        {
            if (await IsPermanentlyDeactivatedAsync())
                return false;
            try
            {


                var appId = await SecureStorage.GetAsync("AppMachineId");
                var deviceId = await SecureStorage.GetAsync("AppMachineName");

                if (string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(deviceId))
                    return true; // Cannot validate without IDs

                var request = new HttpRequestMessage(HttpMethod.Get, "api/appsync/status");

                request.Headers.Add("POS-App-Id", appId);
                request.Headers.Add("POS-Device-Name", deviceId);

                var response = await _httpClient.SendAsync(request, token);


                if (response.IsSuccessStatusCode)
                {

                    var result = await response.Content.ReadFromJsonAsync<DeviceStatusResponse>(cancellationToken: token);
                    if (result == null)
                        return false;

                    if (!result.Status) // means inactive
                    {
                        await SetPermanentlyDeactivatedAsync(); // 🔒 lock forever
                        return false;
                    }

                }

                   

                return true; // status == true (active)
            }
            catch
            {
                return true;
            }
        }
    }
}
