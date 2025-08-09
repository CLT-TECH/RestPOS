using MAUIBLAZORHYBRID.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Upload
{
    public interface IDataUploadService
    {
        Task<UploadResult> UploadPendingDataAsync();
        Task<UploadResult> UploadPendingKOTsAsync();
        Task<bool> HasPendingUploadsBillsAsync();
        Task<bool> HasPendingUploadsKOTAsync();
        Task<bool> HasPendingUploadsBillKOTAsync();

        Task<UploadResult> UploadPendingStockTransfersAsync();
        Task<bool> HasPendingUploadsStockTransferAsync();
    }
}
