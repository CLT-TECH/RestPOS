using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class StockReportLegacyDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public Dictionary<int, int> UnitStocks { get; set; } = new();
        public Dictionary<int, int> BottleStocks { get; set; } = new();
    }
}
