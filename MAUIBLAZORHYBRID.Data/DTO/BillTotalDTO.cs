using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class BillTotalDTO
    {
        public decimal ItemTotal { get; set; }  // Sum of all item amounts (before tax)
        public decimal TaxTotal { get; set; }   // Sum of all taxes
        public decimal TotalAmount { get; set; } // ItemTotal + TaxTotal
        public List<BillTaxDetailDTO> TaxDetails { get; set; } = new List<BillTaxDetailDTO>();
    }
}
