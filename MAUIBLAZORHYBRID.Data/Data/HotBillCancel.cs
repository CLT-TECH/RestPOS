using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class HotBillCancel
    {
        [Key]
        public int Id { get; set; }

        public int HotBillId { get; set; }               // The sale being cancelled
        public string CancelReason { get; set; } = string.Empty;
        public DateTime CancelDate { get; set; } = DateTime.UtcNow;
        public DateTime CancelTime { get; set; } = DateTime.UtcNow;
        public int CancelledByEmpId { get; set; }
        public int BranchId { get; set; }

        public bool IsSynced { get; set; } = false;
        public int HotBillCancelServerId { get; set; } = 0;

        // Navigation
        public virtual HotBillMaster? HotBill { get; set; }
    }
}
