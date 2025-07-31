using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class VWItemParentChild
    {
        [Key]
        public int Id { get; set; }

        public int childItemId { get; set; }

        public int parentItemId { get; set; }

        [ForeignKey(nameof(Unit))]
        public int unitId { get; set; }
        public Unit? Unit { get; set; }

        public string parentItemcode { get; set; } = null!;

        public string parentItemname { get; set; } = null!;

        public string childItemcode { get; set; } = null!;

        public string childItemname { get; set; } = null!;

        public int itemtype { get; set; }

        [ForeignKey(nameof(Category))]
        public int CatId { get; set; }
        public Category category { get; set; }
    }
}
