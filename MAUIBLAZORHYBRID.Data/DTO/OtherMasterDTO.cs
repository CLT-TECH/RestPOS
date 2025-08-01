using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class OtherMasterDTO
    {
        public List<CategoryDTO> Categories { get; set; } = new();
        public List<UnitDTO> Units { get; set; } = new();
        public List<DiningSpaceDTO> DiningSpaces { get; set; } = new();
        public List<TablesDTO> Tables { get; set; } = new();
        public List<DiningSpaceTablesDTO> DiningSpaceTables { get; set; } = new();

    }
}
