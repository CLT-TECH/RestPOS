using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class HotKOTTable
    {
        [Key]
        public int AppKOTTableId { get; set; }  // Local primary key

        public int HotTabID { get; set; }

        // Foreign key referencing local master key
        public int AppKOTId { get; set; }

        [ForeignKey(nameof(AppKOTId))]
        public HotKOT? HotKOTMaster { get; set; }
    }
}
