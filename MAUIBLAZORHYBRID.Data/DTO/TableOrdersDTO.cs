using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class TableOrdersDTO
    {
        public List<OrderDetailsDTO> Orders { get; set; } = new();
    }
}
