using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class HotBillTaxDetailDTO
    {
        [JsonPropertyName("taX_ID")]
        public int TaX_ID { get; set; }

        [JsonPropertyName("taxable_Amt")]
        public decimal Taxable_Amt { get; set; }

        [JsonPropertyName("tax_Per")]
        public decimal Tax_Per { get; set; }

        [JsonPropertyName("tax_Amt")]
        public decimal Tax_Amt { get; set; }
    }
}
