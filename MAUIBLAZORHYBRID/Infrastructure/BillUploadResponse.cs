using MAUIBLAZORHYBRID.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Infrastructure
{
    public class BillUploadResponse
    {
        public int ServerBillId { get; set; }
        public List<BillCashierResultDTO> ServerCashIDs { get; set; } = new();
        public DateTime ProcessedTime { get; set; }
    }
}
