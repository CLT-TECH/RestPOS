using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class BranchTaxSetting
    {
        [Key]
        public int id { get; set; }
        public int BranchId { get; set; }

        public int BillingType { get; set; }

        public int ItemType { get; set; }

        public int TaxId { get; set; }

        public decimal TaxPer { get; set; }

        public virtual BranchMaster Branch { get; set; } = null!;

    }
}
