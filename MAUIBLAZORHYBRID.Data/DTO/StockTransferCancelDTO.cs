using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class StockTransferCancelDTO
    {

        public DateTime CancelDate { get; set; } = DateTime.UtcNow;
        public DateTime CancelTime { get; set; } = DateTime.UtcNow;
        public int StkTrId { get; set; }
        public int CancelledBy { get; set; }
        public string CancelReason { get; set; } = string.Empty;

    }
}
