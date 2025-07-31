using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class BillItemUnit
    {
        [Key]
        public int itemUnitId { get; set; }
        public int itemId { get; set; }

        [ForeignKey(nameof(Unit))]
        public int unitId { get; set; }
        public Unit Unit { get; set; }
        public BillItem Item { get; set; }
    }
}
