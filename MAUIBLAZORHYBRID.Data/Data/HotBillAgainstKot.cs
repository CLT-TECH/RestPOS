using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class HotBillAgainstKot
    {
        [Key]
        public int HotBillKotId { get; set; }

        public int HotBillId { get; set; }

        public int HotKotId { get; set; }

        public virtual HotBillMaster HotBill { get; set; } = null!;

        public virtual HotKOT HotKot { get; set; } = null!;


    }
}
