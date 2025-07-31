using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class DiningSpaceItemRate
    {
        public int id { get; set; }

        [ForeignKey(nameof(Item))]
        public int itemId { get; set; }
        public BillItem item { get; set; }

        [ForeignKey(nameof(DiningSpace))]
        public int diningSpaceId { get; set; }
        public DiningSpace diningSpace { get; set; }
        public decimal itemRate { get; set; }

    }
}
