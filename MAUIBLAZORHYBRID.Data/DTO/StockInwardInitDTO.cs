using MAUIBLAZORHYBRID.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class StockInwardInitDTO
    {
        public List<BillItem> BillItems { get; set; } = new();
        public List<VWItemParentChild> VWParentItemChilds { get; set; } = new();
        public List<BarItem> barItems { get; set; } = new();
        public List<Unit> Units { get; set; } = new();
        public List<VendorMaster> Vendors { get; set; } = new();


    }
}
