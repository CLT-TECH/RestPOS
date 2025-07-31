using MAUIBLAZORHYBRID.DTO;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public interface ISyncService
    {
        Task<SyncResultDTO> SyncBranchesWithMasters();
        Task<SyncResultDTO> SyncOtherMasters();
        Task<SyncResultDTO> SyncItemData();
        Task<SyncResultDTO> SyncItemParentChildData();

    }
}
