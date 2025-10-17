using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.DTO
{
    public class PrintMasterDTO
    {

        public string BranchName { get; set; }
        public string DocNo { get; set; }
        public DateTime DocDate { get; set; }
        public DateTime DocTime { get; set; }
        public decimal Total { get; set; }
        public decimal TaxTotal { get; set; }
        public  List<PrintDetailDTO> PrintDetails { get; set; } = new ();
    }
}
