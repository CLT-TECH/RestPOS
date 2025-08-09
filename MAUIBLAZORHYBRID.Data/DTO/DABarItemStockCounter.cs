using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class DABarItemStockCounter
    {
        public int CounterId { get; set; }

        public int BarItemId { get; set; }

        public decimal Stock { get; set; }
    }
}
