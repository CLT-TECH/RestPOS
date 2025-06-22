using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class SubCategory
    {
            [Key]
            public int subCatId { get; set; }
            public string subCatName { get; set; } = null!;

            [ForeignKey(nameof(Category))]
            public int catId { get; set; }
            public Category Category { get; set; }

            public ICollection<Item> Items { get; set; }
    }
}
