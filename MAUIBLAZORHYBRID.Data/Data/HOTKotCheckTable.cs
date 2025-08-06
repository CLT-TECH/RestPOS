using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class HOTKotCheckTable
    {
        [Key]
        public int AppKOTTableId { get; set; }  // Local primary key

        [ForeignKey(nameof(Tables))]
        public int HotTabID { get; set; }
        public Table Tables { get; set; }

        // Foreign key referencing local master key
        public int AppKOTId { get; set; }

        [ForeignKey(nameof(AppKOTId))]
        public HotKOT HotKOTMaster { get; set; }
    }
}
