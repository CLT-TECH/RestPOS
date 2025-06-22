using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class Unit
    {
        [Key]
        public int unitId { get; set; }
        public string unitName { get; set; }

        public override string ToString() => unitName;
    }
}
