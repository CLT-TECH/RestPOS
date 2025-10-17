using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Infrastructure
{
    public class BillCashierAloneResponse
    {
        public int HotBillCashAppId { get; set; }
        public int? PaymentType { get; set; }   // e.g. "CASH", "CARD", etc.
        public int Hot_Bill_Cash_ID { get; set; }  // returned from SP
    }
}
