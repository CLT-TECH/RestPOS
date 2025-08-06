using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class MasterDTOS
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<BillStationDTO> BillStation { get; set; } = new();
        public List<BranchTaxSettingsDTO> TaxSetting { get; set; } = new();
    }


    public class BillStationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
