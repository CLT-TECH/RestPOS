using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIBLAZORHYBRID.Data.DTO;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public class MasterDataSyncService
    {
        private readonly AppDbContext _db;

        public MasterDataSyncService(AppDbContext db)
        {
            _db = db;
        }

        public async Task SaveToLocalDbAsync(List<MasterDTOS> branchDtos)
        {
          
            foreach (var branchDto in branchDtos)
            {
                var branch = new BranchMaster
                {
                    branchId = branchDto.Id,
                    branchName = branchDto.Name
                };

                if (branchDto.Id > 0)
                {

                    var existing = await _db.BranchMasters.FindAsync(branch.branchId);

                    if (existing == null)
                    {
                        await _db.BranchMasters.AddAsync(branch);
                        await _db.SaveChangesAsync();

                    }

                    

                    foreach (var billstatDto in branchDto.BillStation)
                    {
                        var billstation = new BillStation
                        {
                            billStationId = billstatDto.Id,
                            billStationName = billstatDto.Name,
                            branchId = branchDto.Id
                        };

                        var existingsatation = await _db.BillStations.FindAsync(billstation.billStationId);

                        if (existingsatation == null)
                        {
                        _db.BillStations.Add(billstation);
                            await _db.SaveChangesAsync();

                        }
                    }
                }
            }

        }
    }
}
