using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class StockInwardDetail
    {
        [Key]
        public int StockInwardDetId { get; set; }

        public int StockInwardId { get; set; }

        public int BarItemId { get; set; }

        public decimal InwardQty { get; set; }

        public int UnitId { get; set; }

        public virtual BillItem BarItem { get; set; } = null!;

        public virtual StockInwardMaster StockInward { get; set; } = null!;

        public virtual Unit Unit { get; set; } = null!;
    }
}
