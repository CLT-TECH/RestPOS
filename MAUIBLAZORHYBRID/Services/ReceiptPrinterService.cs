using MAUIBLAZORHYBRID.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Font = System.Drawing.Font;

namespace MAUIBLAZORHYBRID.Services
{
    public class ReceiptPrinterService:IReceiptPrinterService
    {
        private  string _printerName;
        private readonly int _lineWidth;

        public ReceiptPrinterService(string printerName = null, int lineWidth = 32)
        {
            //_printerName = printerName ?? new PrinterSettings().PrinterName; // default printer


            _printerName = Preferences.Get("SavedPrinterName", null);

            _lineWidth = lineWidth;
        }



        public void PrintReceipt(List<SaleItem> items, decimal total)
        {


            var f = new ReceiptFormatter(_lineWidth);
            string receipt = "";

            receipt += f.Center("Test Print") + "\n";
            receipt += "Date: " + DateTime.UtcNow.ToString("dd-MM-yyyy") +
                       "  Time: " + DateTime.UtcNow.ToString("hh:mm tt") + "\n";
            receipt += f.Line() + "\n";
            receipt += "Item              Qty  Price\n";
            receipt += f.Line() + "\n";

            foreach (var item in items)
            {
                receipt += f.FormatItem(item.Name, item.Qty, item.Price) + "\n";
            }

            receipt += f.Line() + "\n";
            receipt += f.FormatItem("TOTAL", 0, total) + "\n";
            receipt += f.Line() + "\n";
            receipt += f.Center("Thank you! Visit again") + "\n";

            // Print
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = _printerName;
            pd.PrintPage += (sender, e) =>
            {
                Font font = new Font("Consolas", 10, FontStyle.Regular);
                e.Graphics.DrawString(receipt, font, Brushes.Black, new System.Drawing.PointF(0, 0));
            };
            pd.Print();
        }
        public void PrintReceiptText(string receipt)
        {



            PrintDocument pd = new PrintDocument();
            if (!string.IsNullOrEmpty(_printerName))
            {
                try
                {
                    pd.PrinterSettings.PrinterName = _printerName;

                    pd.PrintPage += (sender, e) =>
                    {
                        Font font = new Font("Consolas", 10, FontStyle.Regular);
                        e.Graphics.DrawString(receipt, font, Brushes.Black, new System.Drawing.PointF(0, 0));
                    };

                    pd.Print(); // 🖨️ Silent print (no dialog)
                    return;
                }
                catch (Exception ex)
                {
                    // If something goes wrong (printer missing, etc.), fall back to dialog
                    Console.WriteLine($"Printer error: {ex.Message}");
                }
            }

            using (PrintDialog printDlg = new PrintDialog())
            {
                printDlg.Document = pd;
                if (printDlg.ShowDialog() == DialogResult.OK)
                {
                    _printerName = printDlg.PrinterSettings.PrinterName;

                    // Save selected printer for future use
                    Preferences.Set("SavedPrinterName", printDlg.PrinterSettings.PrinterName);

                    pd.PrinterSettings = printDlg.PrinterSettings;

                    pd.Print(); // print now
                }
            }


            //pd.PrinterSettings.PrinterName = _printerName;
            //pd.PrintPage += (sender, e) =>
            //{
            //    Font font = new Font("Consolas", 10, FontStyle.Regular);
            //    e.Graphics.DrawString(receipt, font, Brushes.Black, new System.Drawing.PointF(0, 0));
            //};
            //using (PrintDialog printDlg = new PrintDialog())
            //{
            //    printDlg.Document = pd;
            //    if (printDlg.ShowDialog() == DialogResult.OK)
            //    {
            //        pd.PrinterSettings = printDlg.PrinterSettings;
            //        pd.Print();
            //    }
            //}
        }

    }

    public class ReceiptFormatter
    {
        private readonly int _lineWidth;

        public ReceiptFormatter(int lineWidth = 32) // 32 chars for 58mm printer
        {
            _lineWidth = lineWidth;
        }

        public string Center(string text)
        {
            int spaces = Math.Max(0, (_lineWidth - text.Length) / 2);
            return new string(' ', spaces) + text;
        }

        public string Line() => new string('-', _lineWidth);

        public string FormatItem(string name, int qty, decimal price)
        {
            string qtyStr = qty.ToString();
            string priceStr = price.ToString("F2");

            int nameWidth = _lineWidth - (qtyStr.Length + priceStr.Length + 2);
            return name.PadRight(nameWidth) + qtyStr.PadLeft(2) + " " + priceStr;
        }
    }

}
