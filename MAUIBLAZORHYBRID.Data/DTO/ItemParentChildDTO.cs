using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class ItemParentChildDTO
    {
        public int id { get; set; }

        public int childitemid { get; set; }

        public int parentitemid { get; set; }

        public int unitid { get; set; }

        public string parentitemcode { get; set; } = null!;

        public string parentitemname { get; set; } = null!;

        public string childitemcode { get; set; } = null!;

        public string childitemname { get; set; } = null!;

        public int itemtype { get; set; }

        public int catid { get; set; }
    }
}
