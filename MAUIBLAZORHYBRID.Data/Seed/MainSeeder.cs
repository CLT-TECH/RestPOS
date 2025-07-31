using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIBLAZORHYBRID.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace MAUIBLAZORHYBRID.Data.Seed
{
    public class MainSeeder
    {
        public static void Seed(AppDbContext dbContext)
        {
            //dbContext.Database.EnsureDeletedAsync();
            //dbContext.Database.MigrateAsync();
            return;

            //dbContext.Items.RemoveRange(dbContext.Items);
            //dbContext.MainItems.RemoveRange(dbContext.MainItems);
            ////dbContext.Categories.RemoveRange(dbContext.Categories);
            //dbContext.SaveChanges();
            //// Only seed if empty
            //if (!dbContext.Categories.Any())
            //{
            //    dbContext.Categories.AddRange(
            //         new Category { catId = 1, catName = "Indian Rice", catType = 1 },
            //         new Category { catId = 2, catName = "Bread", catType = 1 },
            //         new Category { catId = 3, catName = "Snacks", catType = 1 },
            //         new Category { catId = 4, catName = "Chineese", catType = 1 },
            //         new Category { catId = 5, catName = "BEER", catType = 2 },
            //         new Category { catId = 6, catName = "WINE", catType = 2 },
            //         new Category { catId = 7, catName = "SPIRIT", catType = 2 },
            //         new Category { catId = 8, catName = "BEVERAGES", catType = 2 }
            //     );
            //    dbContext.SaveChanges();


            //}

            //if (!dbContext.Units.Any())
            //{ 
            //    dbContext.Units.AddRange(
            //        new Unit { unitId = 11, unitName = "LTR" },
            //        new Unit { unitId = 12, unitName = "HLTR" },
            //        new Unit { unitId = 13, unitName = "QTS" },
            //        new Unit { unitId = 14, unitName = "PTS" },
            //        new Unit { unitId = 15, unitName = "NPS" },
            //        new Unit { unitId = 16, unitName = "PEG" },
            //        new Unit { unitId = 17, unitName = "MINI" },
            //        new Unit { unitId = 18, unitName = "CAN" },
            //        new Unit { unitId = 19, unitName = "650" },
            //        new Unit { unitId = 20, unitName = "SMALL" },
            //        new Unit { unitId = 21, unitName = "2LTR" },
            //        new Unit { unitId = 22, unitName = "ML" },
            //        new Unit { unitId = 23, unitName = "275" },
            //        new Unit { unitId = 50, unitName = "NOS" }
            //    );
            //}
            //if (!dbContext.MainItems.Any())
            //{
            //    dbContext.MainItems.AddRange(
            //        new MainItem { Id = 1, Name = "MORPHEUS XO BLENDED PRM. BRANDY" },
            //        new MainItem { Id = 2, Name = "PAULJOHN SING.MALT NIRVANA WHISKY" },
            //        new MainItem { Id = 3, Name = "CALENTER DELUXE VSOP" }
            //    ); 
            //}
            //if (dbContext.Categories.Any())
            //{


            //    if (!dbContext.SubCategories.Any())
            //    {
            //        dbContext.SubCategories.AddRange(
            //               new SubCategory { subCatId = 1, subCatName = "RED WINE", catId = 6 },
            //               new SubCategory { subCatId = 2, subCatName = "ROSE WINE", catId = 6 },
            //               new SubCategory { subCatId = 3, subCatName = "WHISKY", catId = 7 },
            //               new SubCategory { subCatId = 4, subCatName = "BRANDY", catId = 7 }
            //           );
            //        dbContext.SaveChanges();
            //    }

            //    dbContext.Items.AddRange(
            //        new Item { itemName = "MORPHEUS XO BLENDED PRM. BRANDY LTR", subCatId = 4, picURL = "images/morpheus.webp", unitId=11,itemRate=680,MainItemId=1},
            //        new Item { itemName = "MORPHEUS XO BLENDED PRM. BRANDY QTS", subCatId = 4, picURL = "images/morpheus.webp", unitId = 13, itemRate = 340, MainItemId = 1 },
            //        new Item { itemName = "MORPHEUS XO BLENDED PRM. BRANDY PTS", subCatId = 4, picURL = "images/morpheus.webp", unitId = 14, itemRate = 170, MainItemId = 1 },
            //        new Item { itemName = "MORPHEUS XO BLENDED PRM. BRANDY NPS", subCatId = 4, picURL = "images/morpheus.webp", unitId = 15, itemRate = 90, MainItemId = 1 },
            //        new Item { itemName = "MORPHEUS XO BLENDED PRM. BRANDY PEG", subCatId = 4, picURL = "images/morpheus.webp", unitId = 16, itemRate = 60, MainItemId = 1 },
            //        new Item { itemName = "PAULJOHN SING.MALT NIRVANA WHISKY LTR", subCatId = 3, picURL = "images/Calendar.webp", unitId = 11, itemRate = 1000, MainItemId = 2 },
            //        new Item { itemName = "PAULJOHN SING.MALT NIRVANA WHISKY QTS", subCatId = 3, picURL = "images/Calendar.webp", unitId = 13, itemRate = 500, MainItemId = 2 },
            //        new Item { itemName = "PAULJOHN SING.MALT NIRVANA WHISKY PTS", subCatId = 3, picURL = "images/Calendar.webp", unitId = 14, itemRate = 200, MainItemId = 2 },
            //        new Item { itemName = "PAULJOHN SING.MALT NIRVANA WHISKY SMALL", subCatId = 3, picURL = "images/Calendar.webp", unitId = 20, itemRate = 100, MainItemId = 2 },
            //        new Item { itemName = "PAULJOHN SING.MALT NIRVANA WHISKY 2LTR", subCatId = 3, picURL = "images/Calendar.webp", unitId = 21, itemRate = 2000, MainItemId = 2 },
            //        new Item { itemName = "CALENTER DELUXE VSOP HLTR", subCatId = 4, picURL = "images/pauljohn.webp", unitId = 12, itemRate = 0, MainItemId = 3 },
            //        new Item { itemName = "CALENTER DELUXE VSOP QTS", subCatId = 4, picURL = "images/pauljohn.webp", unitId = 13, itemRate = 0, MainItemId = 3 },
            //        new Item { itemName = "CALENTER DELUXE VSOP PTS", subCatId = 4, picURL = "images/pauljohn.webp", unitId = 14, itemRate = 0, MainItemId = 3 },
            //        new Item { itemName = "CALENTER DELUXE VSOP NPS", subCatId = 4, picURL = "images/pauljohn.webp", unitId = 15, itemRate = 0, MainItemId = 3 },
            //        new Item { itemName = "CALENTER DELUXE VSOP MINI", subCatId = 4, picURL = "images/pauljohn.webp", unitId = 17, itemRate = 0, MainItemId = 3 }
            //    );




            //    dbContext.SaveChanges();

            //}
        }
    }
}
