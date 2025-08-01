using MAUIBLAZORHYBRID.Data;
using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MAUIBLAZORHYBRID.Components.Pages.POSPage;
using static MudBlazor.Icons.Custom;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public class OtherMasterSyncService
    {
        private readonly AppDbContext _db;

        public OtherMasterSyncService(AppDbContext db)
        {
            _db = db;
        }

        public async Task SaveToLocalDbAsync(OtherMasterDTO mastersothermastersdtos)
        {
            foreach (var category in mastersothermastersdtos.Categories)
            {
                var cat = new Category
                {
                    catId = category.id,
                    catName = category.name
                };

                if (cat.catId > 0  || cat.catId == 0)
                {
                    var existing = await _db.Categories.FindAsync(cat.catId);
                    if (existing == null)
                    {
                        await _db.Categories.AddAsync(cat);
                        await _db.SaveChangesAsync();
                    }
                        

                    foreach (var subcatogory in category.SubCategories)
                    {

                        var subcat = new SubCategory
                        {
                            catId = category.id,
                            subCatId = subcatogory.id,
                            subCatName = subcatogory.name
                        };
                        var existingsub = await _db.SubCategories.FindAsync(subcat.subCatId);
                        if (existingsub == null)
                        {

                            await _db.SubCategories.AddAsync(subcat);
                            await _db.SaveChangesAsync();

                        }
                    }
                }
            }

            foreach (var unit in mastersothermastersdtos.Units)
            {
                var unitnew = new Unit
                {
                    unitId = unit.id,
                    unitName = unit.name,
                };
                if (unitnew.unitId >= 0)
                {
                    var existingunit = await _db.Units.FindAsync(unitnew.unitId);
                    if (existingunit == null)
                    {
                        await _db.Units.AddAsync(unitnew);
                        await _db.SaveChangesAsync();

                    }
                }
            }
            foreach (var diningDto in mastersothermastersdtos.DiningSpaces)
            {
                var dining = new DiningSpace
                {
                    diningSpaceId = diningDto.Id,
                    diningSpaceName = diningDto.Name,
                };

                var existingdining = await _db.DiningSpaces.FindAsync(dining.diningSpaceId);

                if (existingdining == null)
                {
                    _db.DiningSpaces.Add(dining);
                    await _db.SaveChangesAsync();
                }
            }
            foreach (var objtable in mastersothermastersdtos.Tables)
            {
                var table = new Table
                {
                    tableId = objtable.id,
                    tableName = objtable.name,
                    noOfSeats = objtable.noofseats
                };

                var existingdining = await _db.Tables.FindAsync(objtable.id);

                if (existingdining == null)
                {
                    _db.Tables.Add(table);
                    await _db.SaveChangesAsync();
                }
            }
            foreach (var objdstable in mastersothermastersdtos.DiningSpaceTables)
            {
                var dstable = new TableDiningSpace
                {
                    Id = objdstable.id,
                    tableId = objdstable.tableid,
                    diningspaceId = objdstable.diningspaceid,
                    branchId = objdstable.branchid,
                };

                var existingdining = await _db.TablesDiningSpaces.FindAsync(objdstable.id);

                if (existingdining == null)
                {
                    _db.TablesDiningSpaces.Add(dstable);
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
