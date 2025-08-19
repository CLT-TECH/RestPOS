using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.DTO
{
    public class SyncResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public Exception? Exception { get; set; }
        public int CountDownloaded { get; set; }
        public int CountSaved { get; set; }
        public int CountUpdated { get; set; }
        public int DurationMs { get; set; }
        public string? ErrorCode { get; set; }
    }
}
