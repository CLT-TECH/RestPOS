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
        Task<bool> HasPendingUploadsAsync();
    }
}
