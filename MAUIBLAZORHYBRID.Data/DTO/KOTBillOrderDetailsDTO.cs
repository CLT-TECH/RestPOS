using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class KOTBillOrderDetailsDTO
    {
        public int ItemId { get; set; } = 0;
        public int UnitId { get; set; } = 0;
        public string ItemName { get; set; } = "";
        public string Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
