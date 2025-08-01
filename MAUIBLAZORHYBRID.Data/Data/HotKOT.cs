using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class HotKOT
    {
            [Key]
            public int AppKOTId { get; set; }  // Local primary key for SQLite

            public int? HotKOTId { get; set; } // Server-side master ID (nullable until synced)

            public int HotKOTType { get; set; }

            [MaxLength(50)]
            public string HotKOTPrefix { get; set; } = string.Empty;

            [MaxLength(100)]
            public string HotKOTRefNo { get; set; } = string.Empty;

            public DateTime HotKOTDate { get; set; }
            public DateTime HotKOTTime { get; set; }

            public int BearerID { get; set; }
            public int NoOfGuests { get; set; }

            [Precision(20, 3)]
            public decimal HotKotAmt { get; set; }

            [MaxLength(500)]
            public string HotKOTNotes { get; set; } = string.Empty;

            public int AppMachineID { get; set; }
            public int BranchID { get; set; }
            public int DiningSpaceID { get; set; }
            public int EnteredEmpID { get; set; }
            public int CounterID { get; set; }

            /// <summary>
            /// Tracks if this master record has been synced to server.
            /// </summary>
            public bool IsSynced { get; set; } = false;

            // Navigation properties
            public ICollection<HotKOTTable> Tables { get; set; } = new List<HotKOTTable>();
            public ICollection<HotKOTItemDetail> Items { get; set; } = new List<HotKOTItemDetail>();

    }
}
