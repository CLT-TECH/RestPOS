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
        public string? Message { get; set; }
        public string? Source { get; set; }
        public Exception? Exception { get; set; }
    }
}
