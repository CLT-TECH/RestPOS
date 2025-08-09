using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class BarItemStockCounter
    {
        [Key]
        public int Id { get; set; }
        public int CounterId { get; set; }

        public int BarItemId { get; set; }

        public decimal Stock { get; set; }

        public virtual BarItem BarItem { get; set; } = null!;

        public virtual BillStation Counter { get; set; } = null!;
    }
}
