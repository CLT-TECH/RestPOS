using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class DALoginResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ManagerId { get; set; }
        public int BranchId { get; set; }
        public List<BillStationDTO>? Counters { get; set; }
    }
}
