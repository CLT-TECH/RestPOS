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

            public int HotKOTId { get; set; } 

            public int HotKOTType { get; set; }
            public int HotKOTNo { get; set; }

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

            public bool IsSynced { get; set; } = false;
            public int ServerKOTId { get; set; }   


            public HotKOTTable Tables { get; set; } = new();
            public ICollection<HotKOTItemDetail> Items { get; set; } = new List<HotKOTItemDetail>();
            public ICollection<HOTKotBilled> Billed { get; set; } = new List<HOTKotBilled>();
            public virtual ICollection<HotBillAgainstKot> HotBillAgainstKots { get; set; } = new List<HotBillAgainstKot>();

    }
}
