using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class HotBillAgainstKOTDTO
    {
        [JsonPropertyName("hot_KOT_ID")]
        public int Hot_KOT_ID { get; set; }
    }
}
