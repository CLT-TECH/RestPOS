using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class BillCashier
    {
        [Key]
        public int BillCashierId { get; set; }

        public DateTime BillCashDate { get; set; } = DateTime.UtcNow;
        public DateTime BillCashTime { get; set; } = DateTime.UtcNow;
        public int PaymentMode { get; set; }

        public decimal TotalAmount { get; set; }

        public int HotBillId { get; set; }
        public int HotBillCashNo { get; set; }

        public string HotBillCashPrefix { get; set; } = string.Empty;

        public string HotBillCashRefNo { get; set; } = null!;
        public bool IsSynced { get; set; } = false;
        public int ServerHotBillCashId { get; set; }
        public virtual HotBillMaster HotBill { get; set; } = null!;
        public virtual BillCashierCancel? CancelInfo { get; set; }

    }
}
