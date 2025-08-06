using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Data.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<MainItem> MainItems { get; set; }
        public DbSet<Unit> Units { get; set; }

        public DbSet<BranchMaster> BranchMasters { get; set; }
        public DbSet<BillStation> BillStations { get; set; }
        public DbSet<DiningSpace> DiningSpaces { get; set; }

        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<BillItemUnit> BillItemUnits { get; set; }
        public DbSet<DiningSpaceItemRate> DiningSpaceItemRates { get; set; }
        public DbSet<VWItemParentChild> ItemParentChilds { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableDiningSpace> TablesDiningSpaces { get; set; }
        public DbSet<HotKOT> HotKOTMasters { get; set; }
        public DbSet<HotKOTTable> HotKOTTables { get; set; }
        public DbSet<HotKOTItemDetail> HotKOTItemDetails { get; set; }
        public DbSet<HOTKotBilled> HOTKotBilleds { get; set; }
        public DbSet<HOTKotCheckTable> HOTKotCheckTabless { get; set; }
        public DbSet<BranchTaxSetting> BranchTaxSettings { get; set; }
        public DbSet<TaxMaster> TaxMasters { get; set; }
        public DbSet<HotBillMaster> HotBillMasters { get; set; }
        public DbSet<HotBillItemDetail> HotBillItemDetail { get; set; }
        public DbSet<HotBillTaxDetail> HotBillTaxDetails { get; set; }
        public DbSet<HotBillAgainstKot> HotBillAgainstKots { get; set; }
        
        static AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
