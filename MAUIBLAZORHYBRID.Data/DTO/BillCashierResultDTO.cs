using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class BillCashierResultDTO
    {
        public int? PaymentType { get; set; }   // e.g. "CASH", "CARD", etc.
        public int Hot_Bill_Cash_ID { get; set; }  // returned from SP
    }
}
