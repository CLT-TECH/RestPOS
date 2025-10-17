using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class StockReportDTO
    {
        public string ItemName { get; set; } = string.Empty;
        public int ItemId { get; set; }
        public int Stock { get; set; }
    }
}
