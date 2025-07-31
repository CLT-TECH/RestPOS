using MAUIBLAZORHYBRID.Data;
using MAUIBLAZORHYBRID.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class PosPageService
    {
        private readonly AppDbContext _db;

        public PosPageService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _db.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.Items)
                .ToListAsync();
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            return await _db.Items
                .Include(u => u.Unit)
                .Include(i => i.SubCategory)
                .ThenInclude(sc => sc.Category)
                .ToListAsync();
        }

        public async Task<List<BillItem>> GetBillItemsAsync()
        {
            return await _db.BillItems
                .Include(u => u.ItemUnits)
                    .ThenInclude(u => u.Unit)
                .Include(i => i.category)
                .Include(r=>r.DiningSpaceItemRates)
                .ToListAsync();
        }
        
        public async Task<List<VWItemParentChild>> GetParentChildAsync()
        {
            return await _db.ItemParentChilds
                .Include(u => u.Unit)
                .Include(i => i.category)
                .ToListAsync();
        }

        public async Task<List<DiningSpaceItemRate>> GetItemRateAsync()
        {
            return await _db.DiningSpaceItemRates
                .ToListAsync();
        }
        //public async Task<List<MainItem>> GetMainItemsAsync()
        //{
        //    return await _db.MainItems
        //        .ToListAsync();
        //}

        public async Task<List<Unit>> GetUnitsAsync()
        {
            return await _db.Units
                .ToListAsync();
        }

        public async Task<List<DiningSpace>> GetDiningSpaces()
        {
            return await _db.DiningSpaces
                .ToListAsync();
        }

        public async Task<List<BillStation>> GetBillStations()
        {
            return await _db.BillStations
                .ToListAsync();
        }
    }
}
