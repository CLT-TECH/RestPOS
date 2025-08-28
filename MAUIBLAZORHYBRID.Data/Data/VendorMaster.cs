using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class VendorMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VendorId { get; set; }
        public string VendorCode { get; set; } = string.Empty;
        public string VendorName { get; set; } = string.Empty;
    }
}
