using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class HotKOTItemDetail
    {
        [Key]
        public int AppKOTItemId { get; set; }  // Local primary key

        public int ItemID { get; set; }

        [MaxLength(100)]
        public string BarCode { get; set; } = string.Empty;

        [Precision(20, 3)]
        public decimal Qty { get; set; }

        public int UnitID { get; set; }

        [Precision(20, 3)]
        public decimal DetRate { get; set; }

        [Precision(20, 3)]
        public decimal DetAmt { get; set; }

        [Precision(20, 3)]
        public decimal DetDiscPer { get; set; }

        [Precision(20, 3)]
        public decimal DetDiscAmt { get; set; }

        [Precision(20, 3)]
        public decimal DetGrossAmt { get; set; }

        [Precision(20, 3)]
        public decimal DetTaxPer { get; set; }

        [Precision(20, 3)]
        public decimal DetTaxAmt { get; set; }

        [Precision(20, 3)]
        public decimal DetNetAmt { get; set; }

        [MaxLength(900)]
        public string HotKotItemNotes { get; set; } = string.Empty;

        // Foreign key referencing local master key
        public int AppKOTId { get; set; }

        [ForeignKey(nameof(AppKOTId))]
        public HotKOT? HotKOTMaster { get; set; }
    }

}
