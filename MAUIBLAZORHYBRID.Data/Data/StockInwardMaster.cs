using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class StockInwardMaster
    {
        [Key]
        public int StockInwardId { get; set; }

        public int SockInwardServerId { get; set; } = 0;

        public int StockInwardSlNo { get; set; }

        public string StockInwardPrefix { get; set; } = null!;

        public string StockInwardDocNo { get; set; } = null!;

        public string StockInwardRefNo { get; set; } = null!;

        public DateTime StockInwardDate { get; set; }

        public DateTime StockInwardTime { get; set; }

        public DateTime StockInwardSqlDateTime { get; set; }

        public int VendorId { get; set; }

        public string StockInwardNotes { get; set; } = null!;

        public int BranchId { get; set; }

        public int LoginEmpId { get; set; }
        public bool IsSynced { get; set; } = false;

        public virtual BranchMaster Branch { get; set; } = null!;

        public virtual ICollection<StockInwardDetail> StockInwardDetails { get; set; } = new List<StockInwardDetail>();

        public virtual VendorMaster Vendor { get; set; } = null!;
    }
}
