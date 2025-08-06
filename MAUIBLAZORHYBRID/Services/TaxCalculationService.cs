using MAUIBLAZORHYBRID.Components.Data;
using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class TaxCalculationService
    {
        public BillTotalDTO CalculateBill(List<ItemOrder> orderedItems, List<BranchTaxSetting> taxSettings)
        {
            var billTotal = new BillTotalDTO();
            billTotal.ItemTotal = orderedItems.Sum(x => x.Amount);

            // Group items by ItemType to apply taxes
            var itemsByType = orderedItems.GroupBy(x => x.ItemType);

            foreach (var group in itemsByType)
            {
                int itemType = group.Key;
                decimal itemTypeTotal = group.Sum(x => x.Amount);

                // Apply all taxes for this ItemType
                var applicableTaxes = taxSettings.Where(t => t.ItemType == itemType).ToList();

                foreach (var tax in applicableTaxes)
                {
                    decimal taxAmount = itemTypeTotal * tax.TaxPer/100;

                    billTotal.TaxDetails.Add(new BillTaxDetailDTO
                    {
                        TaxId = tax.TaxId,
                        TaxPer= tax.TaxPer,
                        TaxableAmount = itemTypeTotal,
                        TaxAmount = taxAmount
                    });
                }
            }

            billTotal.TaxTotal = billTotal.TaxDetails.Sum(x => x.TaxAmount);
            billTotal.TotalAmount = billTotal.ItemTotal + billTotal.TaxTotal;

            return billTotal;
        }
    }
}
