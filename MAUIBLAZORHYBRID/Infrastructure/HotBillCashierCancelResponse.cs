using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Infrastructure
{
    public class HotBillCashierCancelResponse
    {
        public int HotBillId { get; set; }
        public List<BillCashierCancelResult> Results { get; set; } = new();
    }
}
