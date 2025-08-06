using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Infrastructure;
using MAUIBLAZORHYBRID.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiClient> _logger;

        public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiResponse> PostBillAsync(HotBillMaster bill)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/bills", bill);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<ApiSuccessResponse>();
                    return ApiResponse.Success(content.ServerId);
                }

                var error = await response.Content.ReadAsStringAsync();
                return ApiResponse.Fail($"HTTP {response.StatusCode}: {error}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API call failed");
                return ApiResponse.Fail(ex.Message);
            }
        }
    }
}
