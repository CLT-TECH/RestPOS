using MAUIBLAZORHYBRID.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class KOTInitDTO
    {
        public List<Table> Tables { get; set; } = new();
        public List<TableDiningSpace> TablesDiningSpaces { get; set; } = new();

    }
}
