using MAUIBLAZORHYBRID.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class ItemSyncDTO
    {
        public List<ItemMasterDTO> items { get; set; } = new();
        public List<ItemUnitDTO> itemunits { get; set; } = new();
        public List<DiningSpaceItemRateDTO> diningspacerates { get; set; } = new();
    }
}
