using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int itemId { get; set; }
        public string itemName { get; set; } = null!;

        [ForeignKey(nameof(SubCategory))]
        public int subCatId { get; set; }
        public SubCategory SubCategory { get; set; }
        public string? picURL { get; set; }

        public decimal itemRate { get; set; }

        [ForeignKey(nameof(MainItem))]
        public int? MainItemId { get; set; }

        // Navigation property
        public MainItem MainItem { get; set; }


        [ForeignKey(nameof(Unit))]
        public int? unitId { get; set; }

        // Navigation property
        public Unit Unit { get; set; }

    }
}
