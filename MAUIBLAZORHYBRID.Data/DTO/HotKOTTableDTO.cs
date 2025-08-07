using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class HotKOTTableDTO
    {
        [JsonPropertyName("hot_Tab_ID")]
        public int Hot_Tab_ID { get; set; }
    }
}
