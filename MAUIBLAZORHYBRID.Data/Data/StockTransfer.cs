using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class StockTransfer
    {
        [Key]
        public int Id { get; set; }
        public int ServerTransferId { get; set; }  // Optional: if synced to server
        public string Prefix { get; set; }
        public int StkTrSlNo { get; set; }
        public string RefNo { get; set; } = string.Empty;
        public DateTime TransferDate { get; set; }
        public DateTime TransferTime { get; set; }
        public int FromType { get; set; } // 1: Godown, 2: Counter
        public int ToType { get; set; }   // 1: Godown, 2: Counter
        public int FromGodownId { get; set; }
        public int FromCounterId { get; set; }
        public int ToGodownId { get; set; }
        public int ToCounterId { get; set; }
        public int BranchId { get; set; }
        public int EmployeeId { get; set; }
        public string Notes { get; set; } = string.Empty;
        public bool IsSynced { get; set; } = false;
        public virtual ICollection<StockTransferItem> StockTransferDetails { get; set; } = new List<StockTransferItem>();
        public virtual StockTransferCancel? CancelInfo { get; set; }

    }
}
