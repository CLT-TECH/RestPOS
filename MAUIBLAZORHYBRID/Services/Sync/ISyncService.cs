using MAUIBLAZORHYBRID.DTO;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public interface ISyncService
    {
        Task<SyncResultDTO> SyncBranchesWithMasters(CancellationToken ct = default);
        Task<SyncResultDTO> SyncOtherMasters(CancellationToken ct = default);
        Task<SyncResultDTO> SyncItemData(CancellationToken ct = default);
        Task<SyncResultDTO> SyncItemParentChildData(CancellationToken ct = default);
        Task<SyncResultDTO> SyncBarItemCounterStock(CancellationToken ct = default);

    }
}
