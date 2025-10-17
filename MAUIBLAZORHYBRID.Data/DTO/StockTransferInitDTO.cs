using MAUIBLAZORHYBRID.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class StockTransferInitDTO
    {
        public List<BillStation> Counters { get; set; } = new();
        public List<GodownMaster> Godowns { get; set; } = new();
        public List<BillItem> BillItems { get; set; } = new();
        public List<VWItemParentChild> VWParentItemChilds { get; set; } = new();
        public List<BarItem> barItems { get; set; } = new();

    }
}
