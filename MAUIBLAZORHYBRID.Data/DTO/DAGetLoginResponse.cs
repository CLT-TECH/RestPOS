using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class DAGetLoginResponse
    {
        public bool Success { get; set; }
        public List<BillStationDTO>? Counters { get; set; } 

    }
}
