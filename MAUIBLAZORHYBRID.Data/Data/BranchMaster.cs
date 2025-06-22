using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class BranchMaster
    {
        [Key]
        public  int branchId { get; set; }
        public  string branchName { get; set; }

        public ICollection<DiningSpace> DiningSpaces { get; set; }
        public ICollection<BillStation> BillStations { get; set; }

    }
}
