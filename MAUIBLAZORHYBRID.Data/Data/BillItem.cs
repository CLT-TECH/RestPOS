using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class BillItem
    {
        [Key]
        public int itemId { get; set; }
        public string itemName { get; set; } = null!;
        public int itemType { get; set; }

        [ForeignKey(nameof(Category))]
        public int CatId { get; set; }
        public Category category { get; set; }

        public ICollection<BillItemUnit>? ItemUnits { get; set; }
        public ICollection<DiningSpaceItemRate>? DiningSpaceItemRates { get; set; }

        public override string ToString() => itemName;
    }
}
