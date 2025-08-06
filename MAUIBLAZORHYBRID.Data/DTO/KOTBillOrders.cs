using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class KOTBillOrders
    {
            public int KOTId { get; set; } = 0;
            public List<KOTBillOrderDetailsDTO> Items { get; set; } = new();
    }
}
