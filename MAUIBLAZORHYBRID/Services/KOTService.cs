using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class KOTService
    {
        private readonly AppDbContext _db;

        public KOTService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<KOTInitDTO>> GetInitData()
        {
            try {
                var tablesTask = _db.Tables
                    .AsNoTracking()
                     .ToListAsync();
                var tablesdiningspacesTask = _db.TablesDiningSpaces
                                .AsNoTracking()
                                 .ToListAsync();


                await Task.WhenAll(tablesTask, tablesdiningspacesTask);

                var result = new KOTInitDTO
                {
                    Tables = tablesTask.Result,
                    TablesDiningSpaces = tablesdiningspacesTask.Result,
                };
                return Result<KOTInitDTO>.Success(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetInitData failed: {ex.Message}");
                return Result<KOTInitDTO>.Failure("Failed to load data. Please try again.");
            }
        }
    }
}
