using MAUIBLAZORHYBRID.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class KOTBillDTO
    {
        public DiningSpace DiningSpaces { get; set; } = new();
        public List<KOTBillOrders> Orders { get; set; } = new();


    }
}
