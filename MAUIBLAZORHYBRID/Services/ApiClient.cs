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
        private readonly AppState _appState;
        private readonly ILogger<ApiClient> _logger;

        public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger, AppState appState)
        {
            _appState = appState;
            _httpClient = httpClient;

            _logger = logger;
            _appState = appState;
        }
        private void SetMachineHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove("App-Machine-Id");
            _httpClient.DefaultRequestHeaders.Remove("Entry-Counter-Id");
            _httpClient.DefaultRequestHeaders.Remove("App-User-Name");

            if (_appState.MachineId > 0)
                _httpClient.DefaultRequestHeaders.Add("App-Machine-Id", _appState.MachineId.ToString());

            if (_appState.CounterId > 0)
                _httpClient.DefaultRequestHeaders.Add("Entry-Counter-Id", _appState.CounterId.ToString());

            if (_appState.AppUserName != "")
                _httpClient.DefaultRequestHeaders.Add("App-User-Name", _appState.AppUserName);

            
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

                SetMachineHeaders();
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
                var json = JsonSerializer.Serialize(bill, new JsonSerializerOptions
                {
                    WriteIndented = true
                });


                SetMachineHeaders();


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

        public async Task<ApiResponse<StockTransferUploadResponse>> PostStockTransferAsync(StockTransferDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
                {
                    WriteIndented = true
                });


                SetMachineHeaders();

                var response = await _httpClient.PostAsJsonAsync("/api/AppSync/SaveStockTransfer", dto);
                var content = await response.Content.ReadFromJsonAsync<ApiResponse<StockTransferUploadResponse>>();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<StockTransferUploadResponse>
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

                return content!;
            }
            catch (Exception ex)
            {
                return new ApiResponse<StockTransferUploadResponse>
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
    
    
        public async Task<ApiResponse<StockInwardUploadResponse>> PostStockInwardAsync(StockInwardDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                Console.WriteLine(json);


                SetMachineHeaders();

                var response = await _httpClient.PostAsJsonAsync("/api/AppSync/SaveStockInward", dto);



                var content = await response.Content.ReadFromJsonAsync<ApiResponse<StockInwardUploadResponse>>();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<StockInwardUploadResponse>
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

                return content!;
            }
            catch (Exception ex)
            {
                return new ApiResponse<StockInwardUploadResponse>
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


        public async Task<ApiResponse<StkTrCancelResponse>> PostStkTrCancelAsync(StockTransferCancelDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                Console.WriteLine(json); // Optional: For debugging
                SetMachineHeaders();

                var response = await _httpClient.PostAsJsonAsync("/api/AppSync/SaveStockTransferCancel", dto);
                var content = await response.Content.ReadFromJsonAsync<ApiResponse<StkTrCancelResponse>>();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<StkTrCancelResponse>
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

                return content!;
            }
            catch (Exception ex)
            {
                return new ApiResponse<StkTrCancelResponse>
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

        public async Task<ApiResponse<HotBillCancelResponse>> PostHotBillCancelAsync(HotBillCancelDTO dto)
        {
            try
            {
                var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                Console.WriteLine(json); // Optional: Debugging
                SetMachineHeaders();

                var response = await _httpClient.PostAsJsonAsync("/api/AppSync/SaveHotBillCancel", dto);

                var content = await response.Content.ReadFromJsonAsync<ApiResponse<HotBillCancelResponse>>();

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<HotBillCancelResponse>
                    {
                        Success = false,
                        Message = content?.Message ?? "API request failed",
                        Errors = content?.Errors ?? new List<ApiError>
                {
                    new ApiError
                    {
                        Code = $"HTTP_{(int)response.StatusCode}",
                        Description = "Unexpected API error"
                    }
                }
                    };
                }

                return content!;
            }
            catch (Exception ex)
            {
                return new ApiResponse<HotBillCancelResponse>
                {
                    Success = false,
                    Message = "Network error occurred",
                    Errors = new List<ApiError>
            {
                new ApiError
                {
                    Code = "NETWORK_ERROR",
                    Description = ex.Message
                }
            }
                };
            }
        }


        public async Task<ApiResponse<BillCashierCancelResponse>> PostBillCashierCancelAsync(BillCashierCancelDTO dto)
        {
            try
            {
                // 🔹 Optional: log payload for debugging
                var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                Console.WriteLine(json);

                // 🔹 Set headers (same as your other API calls)
                SetMachineHeaders();

                // 🔹 Call your API endpoint
                var response = await _httpClient.PostAsJsonAsync("/api/AppSync/SaveBillCashierCancel", dto);

                // 🔹 Try to deserialize the API response
                var content = await response.Content.ReadFromJsonAsync<ApiResponse<BillCashierCancelResponse>>();

                // 🔹 Handle HTTP-level failure
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<BillCashierCancelResponse>
                    {
                        Success = false,
                        Message = content?.Message ?? "API request failed",
                        Errors = content?.Errors ?? new List<ApiError>
                {
                    new ApiError
                    {
                        Code = $"HTTP_{(int)response.StatusCode}",
                        Description = "Unexpected API error"
                    }
                }
                    };
                }

                return content!;
            }
            catch (Exception ex)
            {
                return new ApiResponse<BillCashierCancelResponse>
                {
                    Success = false,
                    Message = "Network error occurred while cancelling bill cashier",
                    Errors = new List<ApiError>
            {
                new ApiError
                {
                    Code = "NETWORK_ERROR",
                    Description = ex.Message
                }
            }
                };
            }
        }


        public async Task<ApiResponse<List<BillCashierAloneResponse>>> PostBillCashiersAsync(List<BillCashierDTO> dto)
        {
            try
            {
                // Serialize (optional: for debugging/logging)
                var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                // Add headers like App-Machine-Id, Entry-Counter-Id, etc.
                SetMachineHeaders();

                // Send POST request
                var response = await _httpClient.PostAsJsonAsync("/api/AppSync/SaveBillCashiers", dto);
                var content = await response.Content.ReadFromJsonAsync<ApiResponse<List<BillCashierAloneResponse>>>();

                // Handle failure responses
                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResponse<List<BillCashierAloneResponse>>
                    {
                        Success = false,
                        Message = content?.Message ?? "API request failed",
                        Errors = content?.Errors ?? new List<ApiError>
                        {
                            new ApiError
                            {
                                Code = $"HTTP_{(int)response.StatusCode}",
                                Description = "Unexpected API error"
                            }
                        }
                    };
                }

                // Return success content
                return content!;
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<BillCashierAloneResponse>>
                {
                    Success = false,
                    Message = "Network error occurred while saving bill cashier data",
                    Errors = new List<ApiError>
                    {
                        new ApiError
                        {
                            Code = "NETWORK_ERROR",
                            Description = ex.Message
                        }
                    }
                };
            }
        }
    }
}
