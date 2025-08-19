using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class BillStation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int billStationId { get; set; }
        public string billStationName { get; set; }

        [ForeignKey(nameof(BranchMaster))]
        public int branchId { get; set; }
        public BranchMaster Branch { get; set; }

    }
}
