using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class CategoryDTO
    {
        public int id { get; set; }
        public string name { get; set; }

        public List<SubCatgoryDTO>? SubCategories { get; set; } = new();

    }
}
