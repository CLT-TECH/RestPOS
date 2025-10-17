using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    
    public class HotBillCashierDTO
    {
        [JsonPropertyName("paymode")]
        public int Paymode { get; set; }
        [JsonPropertyName("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("hotBillCashNo")]
        public int HotBillCashNo { get; set; }

        [JsonPropertyName("hotBillCashPrefix")]
        public string HotBillCashPrefix { get; set; } = string.Empty;

        [JsonPropertyName("hotBillCashRefNo")]
        public string HotBillCashRefNo { get; set; } = null!;

    }
}
