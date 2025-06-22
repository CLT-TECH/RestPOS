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
        public async Task<List<MainItem>> GetMainItemsAsync()
        {
            return await _db.MainItems
                .ToListAsync();
        }

        public async Task<List<Unit>> GetUnitsAsync()
        {
            return await _db.Units
                .ToListAsync();
        }
    }
}
