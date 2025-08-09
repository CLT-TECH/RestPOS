using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class SyncHistory
    {
        public int Id { get; set; }
        public DateTime LastSyncTime { get; set; }
        public string SyncType { get; set; } // "Initial", "Manual", etc.
    }
}
