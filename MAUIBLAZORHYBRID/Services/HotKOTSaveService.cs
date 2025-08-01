using MAUIBLAZORHYBRID.Data;
using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Infrastructure;
using MAUIBLAZORHYBRID.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class HotKOTSaveService : IHotKOTSaveService
    {
        private readonly AppDbContext _dbContext;

        public HotKOTSaveService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<HotKOT>> SaveHotKOTAsync(HotKOT hotKOT)
        {
            try
            {
                if (hotKOT == null)
                    return Result<HotKOT>.Failure("HotKOTMaster is null");

                // Check if the record exists locally by AppKOTId
                var existing = await _dbContext.HotKOTMasters
                    .Include(h => h.Tables)
                    .Include(h => h.Items)
                    .FirstOrDefaultAsync(h => h.AppKOTId == hotKOT.AppKOTId);

                if (existing == null)
                {
                    // New record - add with all children
                    _dbContext.HotKOTMasters.Add(hotKOT);
                }
                else
                {
                    return Result<HotKOT>.Failure($"HotKOTMaster with AppKOTId {hotKOT.AppKOTId} already exists.");

                    //// Existing record - update scalar props
                    //_dbContext.Entry(existing).CurrentValues.SetValues(hotKOT);

                    //// Update child collections:

                    //// Update Tables
                    //foreach (var table in hotKOT.Tables)
                    //{
                    //    var existingTable = existing.Tables.FirstOrDefault(t => t.AppKOTTableId == table.AppKOTTableId);
                    //    if (existingTable == null)
                    //    {
                    //        existing.Tables.Add(table);
                    //    }
                    //    else
                    //    {
                    //        _dbContext.Entry(existingTable).CurrentValues.SetValues(table);
                    //    }
                    //}

                    //// Update Items
                    //foreach (var item in hotKOT.Items)
                    //{
                    //    var existingItem = existing.Items.FirstOrDefault(i => i.AppKOTItemId == item.AppKOTItemId);
                    //    if (existingItem == null)
                    //    {
                    //        existing.Items.Add(item);
                    //    }
                    //    else
                    //    {
                    //        _dbContext.Entry(existingItem).CurrentValues.SetValues(item);
                    //    }
                    //}
                }

                await _dbContext.SaveChangesAsync();

                return Result<HotKOT>.Success(hotKOT);
            }
            catch (Exception ex)
            {
                // Log error as needed
                return Result<HotKOT>.Failure($"Error saving HotKOT: {ex.Message}");
            }
        }
    }

}
