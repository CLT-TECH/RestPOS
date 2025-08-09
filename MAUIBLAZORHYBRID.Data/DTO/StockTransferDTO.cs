using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class StockTransferDTO
    {
        [JsonPropertyName("stk_Tr_SlNo")]
        public int Stk_Tr_SlNo { get; set; }

        [JsonPropertyName("stk_Tr_Prefix")]
        public string Stk_Tr_Prefix { get; set; }

        [JsonPropertyName("stk_Tr_RefNo")]
        public string Stk_Tr_RefNo { get; set; }

        [JsonPropertyName("stk_Tr_Date")]
        public DateTime Stk_Tr_Date { get; set; }

        [JsonPropertyName("stk_Tr_Time")]
        public DateTime Stk_Tr_Time { get; set; }

        [JsonPropertyName("stock_From_Type")]
        public int Stock_From_Type { get; set; }

        [JsonPropertyName("stock_To_Type")]
        public int Stock_To_Type { get; set; }

        [JsonPropertyName("entry_Branch_ID")]
        public int Entry_Branch_ID { get; set; }

        [JsonPropertyName("entry_Emp_ID")]
        public int Entry_Emp_ID { get; set; }

        [JsonPropertyName("stk_Tr_Notes")]
        public string Stk_Tr_Notes { get; set; }

        [JsonPropertyName("from_Godown_ID")]
        public int From_Godown_ID { get; set; }

        [JsonPropertyName("from_Counter_ID")]
        public int From_Counter_ID { get; set; }

        [JsonPropertyName("to_Godown_ID")]
        public int To_Godown_ID { get; set; }

        [JsonPropertyName("to_Counter_ID")]
        public int To_Counter_ID { get; set; }

        [JsonPropertyName("items")]
        public List<StockTransferItemDTO> Items { get; set; } = new();
    }
}
