using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class BillTaxDetailDTO
    {
        public int TaxId { get; set; }
        public decimal TaxableAmount { get; set; } // Amount eligible for this tax
        public decimal TaxPer { get; set; } // Amount eligible for this tax
        public decimal TaxAmount { get; set; }     // Tax calculated (TaxableAmount * TaxPer)
    }
}
