using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Components.Data
{
    public class ItemOrder
    {
        public int ItemId { get; set; }
        public int ItemType { get; set; }
        public int UnitID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Amount => Price * Quantity;
    }
}
