using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class StockInwardDocDTO
    {
        public int SLNO { get; set; }
        public string Prefix { get; set; }
        public string DocNo => $"{Prefix}{SLNO}";
    }
}
