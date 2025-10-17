using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class BranchMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int branchId { get; set; }
        public  string branchName { get; set; }
        public int CounterId { get; set; }
        public int GodownId { get; set; }
        public int BranchGodownId { get; set; }
    }
}
