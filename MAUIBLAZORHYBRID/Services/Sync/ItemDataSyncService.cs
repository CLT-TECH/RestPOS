using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MAUIBLAZORHYBRID.Services.Sync
{
    public class ItemDataSyncService
    {
        private readonly AppDbContext _db;

        public ItemDataSyncService(AppDbContext db)
        {
            _db = db;
        }

        public async Task SaveToLocalDbAsync(ItemSyncDTO itemdata)
        {
            _db.ChangeTracker.Clear();
            var icount = 0;
            foreach (var item in itemdata.items)
            {

                var categoryresult = await _db.Categories
                     .Include(c => c.SubCategories)
                     .FirstOrDefaultAsync(c => c.catId == item.catid);
                var itemresult = new BillItem
                {
                    itemId = item.id,
                    itemName = item.name,
                    itemType = item.type,
                    CatId = item.catid,
                    category= categoryresult
                };

                if (itemresult.itemId > 0 && item.catid>0)
                {
                    var existingitem = await _db.BillItems.FindAsync(itemresult.itemId);
                    if (existingitem == null)
                    {
                        await _db.BillItems.AddAsync(itemresult);
                        await _db.SaveChangesAsync();

                        icount++;
                    }

                }
            }

            _db.ChangeTracker.Clear();

            foreach (var itemunit in itemdata.itemunits)
            {
                var unitmaster = await _db.Units.FindAsync(itemunit.unitid);
                var itembyid = await _db.BillItems.FindAsync(itemunit.itemid);

                if (itembyid != null && unitmaster != null)
                {
                    var itemunitresult = new BillItemUnit
                    {
                        itemUnitId = itemunit.id,
                        itemId = itemunit.itemid,
                        unitId = itemunit.unitid,
                        Unit = unitmaster,
                        Item = itembyid
                    };

                    if (itemunitresult.itemUnitId > 0)
                    {
                        var existingitemunit = await _db.BillItemUnits.FindAsync(itemunitresult.itemUnitId);
                        if (existingitemunit == null)
                        {
                            await _db.BillItemUnits.AddAsync(itemunitresult);
                            await _db.SaveChangesAsync();


                        }

                    }
                }
            }

            _db.ChangeTracker.Clear();

            foreach (var itemrate in itemdata.diningspacerates)
            {
                var diningspaceresult = await _db.DiningSpaces.FindAsync(itemrate.diningspaceid);
                var itemresult = await _db.BillItems.FindAsync(itemrate.itemid);
                if (diningspaceresult != null && itemresult != null)
                {
                    var itemrateresult = new DiningSpaceItemRate
                    {
                        id = itemrate.id,
                        itemId = itemrate.itemid,
                        diningSpaceId = itemrate.diningspaceid,
                        itemRate = itemrate.rate,
                        diningSpace = diningspaceresult,
                        item = itemresult
                    };

                    if (itemrateresult.id > 0)
                    {
                        var existingitemrate = await _db.DiningSpaceItemRates.FindAsync(itemrateresult.id);
                        if (existingitemrate == null)
                        {
                            await _db.DiningSpaceItemRates.AddAsync(itemrateresult);
                            await _db.SaveChangesAsync();

                        }
                    }
                }
            }

        }

        public async Task SaveToLocalDbItemParntChildAsync(List<ItemParentChildDTO>  itemdata)
        {
            _db.ChangeTracker.Clear();
            foreach (var item in itemdata)
            {
                var unitmaster = await _db.Units.FindAsync(item.unitid);
                var categoryresult = await _db.Categories.FindAsync(item.catid);

                var itemresult = new VWItemParentChild
                {
                    parentItemId = item.parentitemid,
                    childItemId = item.childitemid,
                    parentItemname = item.parentitemname,
                    parentItemcode = item.parentitemcode,
                    childItemname = item.childitemname,
                    childItemcode = item.childitemcode,
                    itemtype = item.itemtype,
                    unitId = item.unitid,
                    Unit = unitmaster,
                    CatId = item.catid,
                    category = categoryresult
                };
                
                await _db.ItemParentChilds.AddAsync(itemresult);
                await _db.SaveChangesAsync();
            }


        }
    }
}
