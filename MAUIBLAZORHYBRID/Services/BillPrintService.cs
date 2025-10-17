using MAUIBLAZORHYBRID.Services.Interfaces;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class BillPrintService
    {
        private readonly IReceiptPrinterService _printer;
        private readonly PosPageService _pos;

        public BillPrintService( IReceiptPrinterService printer, PosPageService pos)
        {
            _printer = printer;
            _pos = pos;
        }

        public async Task PrintLastBillAsync(int BillId)
        {
            var dto = await _pos.GetLastBillForPrintAsync(BillId);



            if (dto == null) throw new Exception("No bill found");

            var f = new ReceiptFormatter(32); // 58mm printer width
            string receipt = "";

            // Header
            receipt += f.Center(dto.BranchName) + "\n";
            receipt += $"Bill#: {dto.DocNo.PadRight(3)}";
            receipt += $"{dto.DocDate:dd-MM-yyyy} : {dto.DocTime.ToLocalTime():hh:mm tt}\n";
            receipt += f.Line() + "\n";

            // Item header
            receipt += "Item Name\n";

            int priceWidth = 9;   // space reserved for Rate
            int qtyWidth = 8;      // space reserved for Qty
            int amountWidth = 10;  // space reserved for Amount

            string headerLine =
                "".PadLeft(5) +
                "Price".PadLeft(priceWidth) +
                "Qty".PadLeft(qtyWidth) +
                "Amount".PadLeft(amountWidth);

            receipt += headerLine + "\n";
            receipt += f.Line() + "\n";
           

            int TotalQty = 0;
            // Details
            foreach (var d in dto.PrintDetails)
            {
                TotalQty += Convert.ToInt16(d.Qty);
                receipt += FormatItemLine(d.ItemName, d.Price, Convert.ToInt16(d.Qty), d.Total) + "\n";
            }

            receipt += f.Line() + "\n";

            // Tax + Total
            receipt += $"Tax: {dto.TaxTotal:F2}\n";
            receipt += f.Line() + "\n";

            //receipt += f.FormatItem("TOTAL", TotalQty, dto.Total) + "\n";

            string footerLine =
                "TOTAL".PadLeft(priceWidth) +
                "".PadLeft(4) +
                TotalQty.ToString().PadLeft(qtyWidth) +
                dto.Total.ToString("F2").PadLeft(amountWidth+1);
            receipt += footerLine + "\n";


            receipt += f.Line() + "\n";
            receipt += f.Center("Thank you! Visit again") + "\n";

            // Print
            _printer.PrintReceiptText(receipt);
        }


        private string FormatItemLine(string itemName, decimal price, int qty, decimal amount)
        {
            int lineWidth = 32; // your receipt width (for 58mm printer)
            int priceWidth = 9;   // space reserved for Rate
            int qtyWidth = 5;      // space reserved for Qty
            int amountWidth = 11;  // space reserved for Amount


            List<string> lines = new();

            string[] words = itemName.Split(' ');
            StringBuilder currentLine = new();
            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + 1 > lineWidth-8)
                {
                    lines.Add(currentLine.ToString().TrimEnd());
                    currentLine.Clear();
                }
                currentLine.Append(word + " ");
            }

            if (currentLine.Length > 0)
                lines.Add(currentLine.ToString().TrimEnd());

            // 2️⃣ Add rate/qty/amount line after item name is fully printed
            string rateQtyAmtLine =
         price.ToString("F2").PadLeft(priceWidth) +
         qty.ToString().PadLeft(qtyWidth) +
         amount.ToString("F2").PadLeft(amountWidth);

            if (lines.Count > 1)
            {
                string secondLineItemPart = lines[1].Length > 7
                    ? lines[1].Substring(0, 7)
                    : lines[1].PadRight(7);  // ✅ pad to ensure spacing consistency

                lines[1] = secondLineItemPart + rateQtyAmtLine;
            }
            else
            {
                // if only one line name, append the rate/qty/amount in a new line
                lines.Add("".PadRight(7) + rateQtyAmtLine);
            }

            // 4️⃣ Join all lines with newline
            return string.Join("\n", lines);
        }
    }
}
