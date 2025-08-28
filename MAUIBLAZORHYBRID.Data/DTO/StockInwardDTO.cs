using MAUIBLAZORHYBRID.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class StockInwardDTO
    {

        public int StockInwardSlNo { get; set; }

        public string StockInwardPrefix { get; set; } = null!;

        public string StockInwardDocNo { get; set; } = null!;

        public string StockInwardRefNo { get; set; } = null!;

        public DateTime StockInwardDate { get; set; }

        public DateTime StockInwardTime { get; set; }

        public int VendorId { get; set; }

        public string StockInwardNotes { get; set; } = null!;

        public int BranchId { get; set; }

        public int LoginEmpId { get; set; }
        public virtual ICollection<StockInwardDetailsDTO> StockInwardDetails { get; set; } = new List<StockInwardDetailsDTO>();

    }
}
