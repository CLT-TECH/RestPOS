using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class HOTKotBilled
    {
        public int id { get; set; }

        public int HOTKotBillID { get; set; }

        [ForeignKey(nameof(HotKOTMaster))]
        public int HotKOTId { get; set; }
        public HotKOT? HotKOTMaster { get; set; }
    }
}
