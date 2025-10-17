using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class BillCashierCancel
    {
        [Key]
        public int Id { get; set; }

        public int BillCashierId { get; set; }           // The cashier/payment entry being cancelled
        public int BillCashierServerId { get; set; }           // The cashier/payment entry being cancelled
        public string CancelReason { get; set; } = string.Empty;
        public DateTime CancelDate { get; set; } = DateTime.UtcNow;
        public DateTime CancelTime { get; set; } = DateTime.UtcNow;
        public int CancelledByEmpId { get; set; }
        public int BranchId { get; set; }

        public bool IsSynced { get; set; } = false;
        public int BillCashierCancelServerId { get; set; } = 0;

        public virtual BillCashier? BillCashier { get; set; }
    }
}
