using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class PrintDetailDTO
    {
        public  int ItemId { get; set; }
        public  string ItemName { get; set; }
        public decimal Total { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }

    }
}
