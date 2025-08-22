using MAUIBLAZORHYBRID.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class DAGodownStockDTO
    {
        public List<DABarItemStockGodown> BarItemStock { get; set; } = new();
    }
}
