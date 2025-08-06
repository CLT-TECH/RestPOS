using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class OrderDetailsDTO
    {
        public string OrderNo { get; set; } = "";
        public DateTime OrderedAt { get; set; }
        public string TableName { get; set; } = "";
        public List<OrderItemDTO> Items { get; set; } = new();
    }
}
