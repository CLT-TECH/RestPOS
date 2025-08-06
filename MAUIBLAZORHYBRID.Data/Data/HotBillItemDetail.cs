using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class HotBillItemDetail
    {
        [Key]
        public int HotBillItemId { get; set; }

        public int HotBillId { get; set; }

        public int ItemId { get; set; }

        public string BarCode { get; set; } = null!;

        public decimal Qty { get; set; }

        public int UnitId { get; set; }

        public decimal DetAmt { get; set; }

        public decimal DetDiscPer { get; set; }

        public decimal DetDiscAmt { get; set; }

        public decimal DetGrossAmt { get; set; }

        public decimal DetTaxPer { get; set; }

        public decimal DetTaxAmt { get; set; }

        public decimal DetNetAmt { get; set; }

        public decimal DetRate { get; set; }

        public virtual HotBillMaster HotBill { get; set; } = null!;

    }
}
