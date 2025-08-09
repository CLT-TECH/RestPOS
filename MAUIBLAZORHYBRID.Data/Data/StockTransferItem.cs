using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class StockTransferItem
    {
        [Key]
        public int Id { get; set; }

        public int StockTransferLocalId { get; set; } // Foreign key reference to StockTransfer.LocalId

        public int MainBarItemId { get; set; }
        public int UnitId { get; set; }
        public decimal Quantity { get; set; }

        public string ItemName { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public virtual StockTransfer StkTr { get; set; } = null!;
    }
}
