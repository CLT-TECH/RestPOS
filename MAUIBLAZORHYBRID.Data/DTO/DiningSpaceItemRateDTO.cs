using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class DiningSpaceItemRateDTO
    {
        public int id { get; set; }
        public int itemid { get; set; }
        public int diningspaceid { get; set; }
        public decimal rate { get; set; }
    }
}
