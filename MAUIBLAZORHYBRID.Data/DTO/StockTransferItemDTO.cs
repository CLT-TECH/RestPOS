using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class StockTransferItemDTO
    {
            [JsonPropertyName("main_Bar_Item_ID")]
            public int Main_Bar_Item_ID { get; set; }

            [JsonPropertyName("unit_ID")]
            public int Unit_ID { get; set; }

            [JsonPropertyName("tr_Qty")]
            public decimal TR_Qty { get; set; }

            [JsonPropertyName("stk_Tr_Det_Notes")]
            public string? Stk_Tr_Det_Notes { get; set; }
    }
}
