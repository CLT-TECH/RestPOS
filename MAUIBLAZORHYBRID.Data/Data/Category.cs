using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class Category
    {
        [Key]
        public int catId { get; set; }
        public string? catName { get; set; }
        public int catType { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
