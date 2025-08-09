using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class BarItem
    {
        [Key]
        public int Id { get; set; }
        public int BarItemId { get; set; }
        public string BarItemCode { get; set; } = string.Empty;
        public string BarItemName { get; set; } = string.Empty;
        public int BarItemBaseUnitId { get; set; }
        public int BarItemInventoryUnitId { get; set; }
        public int MainBarItem { get; set; }
        public int MainBarItemID { get; set; }

        public virtual ICollection<BarItemStockCounter> BarItemStockCounters { get; set; } = new List<BarItemStockCounter>();

    }
}
