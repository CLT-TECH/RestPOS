using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Interfaces
{
    public record SaleItem(string Name, int Qty, decimal Price);
    public interface IReceiptPrinterService
    {
        void PrintReceipt(List<SaleItem> items, decimal total);
        void PrintReceiptText(string receiptText);
    }
}
