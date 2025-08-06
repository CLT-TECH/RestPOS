using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class HotBillMaster
    {
            [Key]
            public int HotBillId { get; set; }

            public int HotBillType { get; set; }

            public string HotBillPrefix { get; set; } = null!;

            public int HotBillNo { get; set; }

            public string HotBillRefNo { get; set; } = null!;

            public DateTime HotBillDate { get; set; }

            public DateTime HotBillTime { get; set; }

            public int BearerId { get; set; }

            public decimal HotBillItemTotal { get; set; }

            public decimal HotBillTaxTotal { get; set; }

            public decimal B4roundAmt { get; set; }

            public int RoundNeed { get; set; }

            public decimal RoundOffVal { get; set; }

            public decimal HotBillNeTAmt { get; set; }

            public int CashierFound { get; set; }

            public string HotBillNotes { get; set; } = null!;

            public int AppMachineId { get; set; }

            public int BranchId { get; set; }

            public int DiningSpaceId { get; set; }

            public int EnteredEmpId { get; set; }

            public int CounterId { get; set; }

            public string CustomerMobile { get; set; } = null!;

            public bool IsSynced { get; set; } = false;
            public int ServerHotBillId { get; set; }

            public virtual ICollection<HotBillAgainstKot> HotBillAgainstKots { get; set; } = new List<HotBillAgainstKot>();

            public virtual ICollection<HotBillItemDetail> HotBillItemDetails { get; set; } = new List<HotBillItemDetail>();

            public virtual ICollection<HotBillTaxDetail> HotBillTaxDetails { get; set; } = new List<HotBillTaxDetail>();
    }
}
