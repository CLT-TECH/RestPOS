using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class HotBillTaxDetail
    {
            [Key]
            public int HotBillTaxId { get; set; }

            public int HotBillId { get; set; }

            public int TaXId { get; set; }

            public decimal TaxableAmt { get; set; }

            public decimal TaxPer { get; set; }

            public decimal TaxAmt { get; set; }

            public virtual HotBillMaster HotBill { get; set; } = null!;

            public virtual TaxMaster TaX { get; set; } = null!;
    }
}
