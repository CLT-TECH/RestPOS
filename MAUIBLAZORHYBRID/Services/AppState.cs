using MAUIBLAZORHYBRID.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class AppState
    {
        public int BranchId { get; set; } 
        public int LoggedInUserId { get; set; }
        public int MachineId { get; set; } 
        public int CounterId { get; set; } 
        public int BearerId { get; set; } = 1;
        public int GodownId { get; set; } 
        public string BranchName { get; set; }
        public string AppUserName { get; set; }
        public int defaultBranchGodown { get; set; }


        private readonly AppDbContext _db;

        //public int BranchId { get; private set; }
        //public int LoggedInUserId { get; private set; }
        //public int MachineId { get; private set; }
        //public int CounterId { get; private set; }
        //public int BearerId { get; private set; }

        public AppState(AppDbContext db)
        {
            _db = db;
        }

        public async Task LoadAsync()
        {
            // Example: Get machine id from SecureStorage
            var machineIdStr = await SecureStorage.GetAsync("AppMachineId");
            MachineId = int.TryParse(machineIdStr, out var mId) ? mId : 0;

            // Example: Load branch/counter/user from DB
            BranchId = await _db.BranchMasters
                .Select(b => b.branchId)
                .FirstOrDefaultAsync();

            BranchName = await _db.BranchMasters
               .Select(b => b.branchName)
               .FirstOrDefaultAsync()??"";

            CounterId = await _db.BranchMasters
                .Select(c => c.CounterId)
                .FirstOrDefaultAsync();

            GodownId = await _db.BranchMasters
              .Select(c => c.GodownId)
              .FirstOrDefaultAsync();


            defaultBranchGodown = await _db.BranchMasters
              .Select(c => c.BranchGodownId)
              .FirstOrDefaultAsync();

            var AppManagerID=await SecureStorage.GetAsync("AppManagerID");
            var AppUsernameValue = await SecureStorage.GetAsync("AppUsername");


            
            LoggedInUserId = Convert.ToInt32(AppManagerID);
            AppUserName = AppUsernameValue;
        }
    }
}
