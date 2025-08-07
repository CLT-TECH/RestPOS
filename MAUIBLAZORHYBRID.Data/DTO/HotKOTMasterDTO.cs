using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class HotKOTMasterDTO
    {
        [JsonPropertyName("hot_KOT_Type")]
        public int Hot_KOT_Type { get; set; }

        [JsonPropertyName("hot_KOT_Prefix")]
        public string Hot_KOT_Prefix { get; set; }

        [JsonPropertyName("hot_KOT_No")]
        public int Hot_KOT_No { get; set; }

        [JsonPropertyName("hot_KOT_Ref_No")]
        public string Hot_KOT_Ref_No { get; set; }

        [JsonPropertyName("hot_KOT_Date")]
        public DateTime Hot_KOT_Date { get; set; }

        [JsonPropertyName("hot_KOT_Time")]
        public DateTime Hot_KOT_Time { get; set; }

        [JsonPropertyName("bearer_ID")]
        public int Bearer_ID { get; set; }

        [JsonPropertyName("no_Of_Guests")]
        public int No_Of_Guests { get; set; }

        [JsonPropertyName("hot_Kot_Amt")]
        public decimal Hot_Kot_Amt { get; set; }

        [JsonPropertyName("hot_KOT_Notes")]
        public string Hot_KOT_Notes { get; set; }

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

        [JsonPropertyName("items")]
        public List<HotKOTItemDetailDTO> Items { get; set; } = new();

        [JsonPropertyName("tables")]
        public List<HotKOTTableDTO> Tables { get; set; } = new();
    }
}
