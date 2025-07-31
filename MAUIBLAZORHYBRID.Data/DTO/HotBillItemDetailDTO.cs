using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class HotBillItemDetailDTO
    {
        [JsonPropertyName("hot_Bill_ID")]
        public int Hot_Bill_ID { get; set; }

        [JsonPropertyName("item_ID")]
        public int Item_ID { get; set; }

        [JsonPropertyName("barCode")]
        public string BarCode { get; set; } = string.Empty;

        [JsonPropertyName("qty")]
        public decimal Qty { get; set; }

        [JsonPropertyName("unit_ID")]
        public int Unit_ID { get; set; }

        [JsonPropertyName("det_Rate")]
        public decimal Det_Rate { get; set; }

        [JsonPropertyName("det_Amt")]
        public decimal Det_Amt { get; set; }

        [JsonPropertyName("det_Disc_Per")]
        public decimal Det_Disc_Per { get; set; }

        [JsonPropertyName("det_Disc_Amt")]
        public decimal Det_Disc_Amt { get; set; }

        [JsonPropertyName("det_Gross_Amt")]
        public decimal Det_Gross_Amt { get; set; }

        [JsonPropertyName("det_Tax_Per")]
        public decimal Det_Tax_Per { get; set; }

        [JsonPropertyName("det_Tax_Amt")]
        public decimal Det_Tax_Amt { get; set; }

        [JsonPropertyName("det_Net_Amt")]
        public decimal Det_Net_Amt { get; set; }
    }
}
