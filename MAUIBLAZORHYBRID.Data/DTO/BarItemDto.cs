using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class BarItemDto
    {
        public int BarItemId { get; set; }
        public string BarItemCode { get; set; } = string.Empty;
        public string BarItemName { get; set; } = string.Empty;
        public int BarItemBaseUnitId { get; set; }
        public int BarItemInventoryUnitId { get; set; }
        public int MainBarItem { get; set; }
        public int MainBarItemID { get; set; }
    }
}
