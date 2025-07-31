using MAUIBLAZORHYBRID.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class TablePageDTO
    {
        public List<Table> Tables { get; set; } = new();
        public List<DiningSpace> DiningSpaces { get; set; } = new();
        public List<TableDiningSpace> TablesDiningSpaces { get; set; } = new();
    }
}
