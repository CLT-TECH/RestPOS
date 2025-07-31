using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class BillMasterDTO
    {
        [JsonPropertyName("hot_Bill_Type")]
        public int Hot_Bill_Type { get; set; }

        [JsonPropertyName("hot_Bill_Prefix")]
        public string Hot_Bill_Prefix { get; set; } = string.Empty;

        [JsonPropertyName("hot_Bill_No")]
        public int Hot_Bill_No { get; set; }

        [JsonPropertyName("bearer_ID")]
        public int Bearer_ID { get; set; }

        [JsonPropertyName("hot_Bill_Item_Total")]
        public decimal Hot_Bill_Item_Total { get; set; }

        [JsonPropertyName("hot_Bill_Tax_Total")]
        public decimal Hot_Bill_Tax_Total { get; set; }

        [JsonPropertyName("b4Round_Amt")]
        public decimal B4Round_Amt { get; set; }

        [JsonPropertyName("round_Need")]
        public int Round_Need { get; set; }

        [JsonPropertyName("roundOff_Val")]
        public decimal RoundOff_Val { get; set; }

        [JsonPropertyName("hot_Bill_NeT_Amt")]
        public decimal Hot_Bill_NeT_Amt { get; set; }

        [JsonPropertyName("cashier_Found")]
        public int Cashier_Found { get; set; }

        [JsonPropertyName("hot_Bill_Notes")]
        public string Hot_Bill_Notes { get; set; } = string.Empty;

        [JsonPropertyName("app_Machine_ID")]
        public int App_Machine_ID { get; set; }

        [JsonPropertyName("branch_ID")]
        public int Branch_ID { get; set; }

        [JsonPropertyName("dining_Space_ID")]
        public int Dining_Space_ID { get; set; }

        [JsonPropertyName("entered_Emp_ID")]
        public int Entered_Emp_ID { get; set; }

        [JsonPropertyName("counter_ID")]
        public int Counter_ID { get; set; }

        [JsonPropertyName("customer_Mobile")]
        public string Customer_Mobile { get; set; } = string.Empty;

        [JsonPropertyName("items")]
        public List<HotBillItemDetailDTO> Items { get; set; } = new();
    }
}
