using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Infrastructure;

namespace MAUIBLAZORHYBRID.Services.Interfaces
{
    public interface IStockTransferSaveService
    {
        Task<Result<StockTransfer>> SaveStockTransferAsync(StockTransfer stockTransfer);
    }
}
