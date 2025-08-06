using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class BranchTaxSettingsDTO
    {
        public int BranchId { get; set; }

        public int BillingType { get; set; }

        public int ItemType { get; set; }

        public int TaxId { get; set; }

        public decimal TaxPer { get; set; }
    }
}
