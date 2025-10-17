using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class StockTransferCancel
    {
        [Key]
        public int Id { get; set; }

        public int StockTransferId { get; set; }     // The transfer being cancelled
        public string CancelReason { get; set; } = string.Empty;
        public DateTime CancelDate { get; set; } = DateTime.UtcNow;
        public DateTime CancelTime { get; set; } = DateTime.UtcNow;
        public int CancelledByEmpId { get; set; }
        public int BranchId { get; set; }

        public bool IsSynced { get; set; } = false;
        public int StkCancelServerId { get; set; } = 0;

        public virtual StockTransfer? StockTransfer { get; set; }
    }
}
