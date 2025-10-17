using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class BillCashierDTO
    {
        public int HotBillCashAppId { get; set; }

        public int HotBillId { get; set; }
        public DateTime HotBillDate { get; set; }
        public DateTime HotBillTime { get; set; }
        public int FormType { get; set; }
        public int Paymode { get; set; }
        public decimal TotalAmount { get; set; }

        public int HotBillCashNo { get; set; }
        public string HotBillCashPrefix { get; set; } = string.Empty;
        public string HotBillCashRefNo { get; set; } = string.Empty;

        public int CashierId { get; set; }
        public int BranchId { get; set; }
    }
}
