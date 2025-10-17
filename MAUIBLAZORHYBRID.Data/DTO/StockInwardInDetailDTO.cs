using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class StockInwardInDetailDTO
    {
        public string ItemName { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public string ItemUnit { get; set; }
        public int UnitType { get; set; }
    }
}
