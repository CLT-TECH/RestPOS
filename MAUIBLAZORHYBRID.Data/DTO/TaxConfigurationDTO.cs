using MAUIBLAZORHYBRID.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class TaxConfigurationDTO
    {
        public List<TaxMaster> TaxMsters { get; set; } = new();
        public List<BranchTaxSetting> TaxSettings { get; set; } = new();
    }
}
