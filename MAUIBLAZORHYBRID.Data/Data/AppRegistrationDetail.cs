using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class AppRegistrationDetail
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string AppMachineName { get; set; } = string.Empty;
        public string machineId { get; set; } = string.Empty;
        public string ManagerId { get; set; } = string.Empty;
        public string BranchId { get; set; } = string.Empty;
    }
}
