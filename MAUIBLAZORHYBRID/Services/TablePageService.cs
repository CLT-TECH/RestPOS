using MAUIBLAZORHYBRID.Data;
using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace MAUIBLAZORHYBRID.Services
{
    public class TablePageService
    {
        private readonly AppDbContext _db;

        public TablePageService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<TablePageDTO> GetInitData()
        {
            var tablesTask =  _db.Tables
                .AsNoTracking()
                 .ToListAsync();
            var diningspacesTask = _db.DiningSpaces
                .AsNoTracking()
                 .ToListAsync();
            var tablesdiningspacesTask = _db.TablesDiningSpaces
                .AsNoTracking()
                 .ToListAsync();

            await Task.WhenAll(tablesTask, diningspacesTask, tablesdiningspacesTask);

            var result = new TablePageDTO
            {
                Tables = tablesTask.Result,
                DiningSpaces = diningspacesTask.Result,
                TablesDiningSpaces = tablesdiningspacesTask.Result

            };
            return result;
        }

    }
}
