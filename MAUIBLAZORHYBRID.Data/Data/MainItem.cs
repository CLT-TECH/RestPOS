using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class MainItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int MainItemType { get; set; }

        // Navigation: One main item has many items
        public List<Item> Items { get; set; } = new();

        public override string ToString() => Name;

    }
}
