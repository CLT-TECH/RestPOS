using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Infrastructure
{
    public class BillCashierCancelResult
    {
        public int BillCashierId { get; set; }
        public int CancelId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
