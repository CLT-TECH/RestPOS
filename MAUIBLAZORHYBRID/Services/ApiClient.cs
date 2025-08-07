using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.Infrastructure;
using MAUIBLAZORHYBRID.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
        public async Task<ApiResponse<KOTUploadResponse>> PostKOTAsync(HotKOTMasterDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
                {
                    WriteIndented = true // Makes the output easier to read
                });

                // You can log or inspect this JSON
                Console.WriteLine(json); //
                var response = await _httpClient.PostAsJsonAsync("/api/AppSync/SaveKOT", dto);
                var content = await response.Content.ReadFromJsonAsync<ApiResponse<KOTUploadResponse>>();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<KOTUploadResponse>
                    {
                        Success = false,
                        Message = content?.Message ?? "API request failed",
                        Errors = content?.Errors ?? new List<ApiError> {
                            new ApiError {
                                Code = $"HTTP_{(int)response.StatusCode}",
                                Description = "Unexpected API error"
                            }
                        }
                    };
                }

                return content;
            }
            catch (Exception ex)
            {
                return new ApiResponse<KOTUploadResponse>
                {
                    Success = false,
                    Message = "Network error occurred",
                    Errors = new List<ApiError> {
                        new ApiError {
                            Code = "NETWORK_ERROR",
                            Description = ex.Message
                        }
                    }
                };
            }
        }

        public async Task<ApiResponse<BillUploadResponse>> PostBillAsync(BillMasterDTO bill)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/AppSync/SaveBill", bill);
                var content = await response.Content.ReadFromJsonAsync<ApiResponse<BillUploadResponse>>();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<BillUploadResponse>
                    {
                        Success = false,
                        Message = content?.Message ?? "API request failed",
                        Errors = content?.Errors ?? new List<ApiError> {
                            new ApiError {
                                Code = $"HTTP_{(int)response.StatusCode}",
                                Description = "Unexpected API error"
                            }
                            }
                    };
                }

                return content;
            }
            catch (Exception ex)
            {
                return new ApiResponse<BillUploadResponse>
                {
                    Success = false,
                    Message = "Network error occurred",
                            Errors = new List<ApiError> {
                        new ApiError {
                            Code = "NETWORK_ERROR",
                            Description = ex.Message
                        }
                    }
                };
            }
        }
    }
}
