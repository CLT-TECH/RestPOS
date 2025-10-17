using MAUIBLAZORHYBRID.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Infrastructure
{
    public class HotBillCashierCancelRequest
    {
        public int HotBillId { get; set; }
        public List<BillCashierCancelDTO> CashierCancels { get; set; } = new();
    }
}
