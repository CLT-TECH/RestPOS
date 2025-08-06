using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class TaxMasterDTO
    {
        public int TaxId { get; set; }

        public string? TaxCode { get; set; }

        public string? TaxName { get; set; }

        public int TaxGroupId { get; set; }

        public int TaxCalcId { get; set; }

        public int ApplicableType { get; set; }
    }
}
