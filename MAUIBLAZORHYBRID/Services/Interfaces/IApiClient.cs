using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Interfaces
{
    public interface IApiClient
    {
        Task<ApiResponse<KOTUploadResponse>> PostKOTAsync(HotKOTMasterDTO kot);
        Task<ApiResponse<BillUploadResponse>> PostBillAsync(BillMasterDTO bill);
        Task<ApiResponse<StockTransferUploadResponse>> PostStockTransferAsync(StockTransferDTO dto);
        Task<ApiResponse<StockInwardUploadResponse>> PostStockInwardAsync(StockInwardDTO dto);

    }
}
