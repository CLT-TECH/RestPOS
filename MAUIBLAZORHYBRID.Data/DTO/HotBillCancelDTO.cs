using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class HotBillCancelDTO
    {
        public int HotBillId { get; set; }
        public DateTime CancelDate { get; set; }
        public DateTime CancelTime { get; set; }
        public string CancelReason { get; set; } = string.Empty;
        public int CancelledBy { get; set; }
    }
}
