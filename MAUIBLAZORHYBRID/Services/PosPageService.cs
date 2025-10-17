using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
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
        private readonly AppState _app;

        public PosPageService(AppDbContext db,AppState app)
        {
            _db = db;
            _app = app;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _db.Categories
                .Where(i=>i.catId!=0)
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

        public async Task<TaxConfigurationDTO> GetTaxConfigurationAsync()
        {
            var taxSettingTask =  _db.BranchTaxSettings
                .ToListAsync();
            var taxMasterTask = _db.TaxMasters
                .ToListAsync();

            await Task.WhenAll(taxSettingTask, taxMasterTask);

            var result = new TaxConfigurationDTO
            {
                TaxMsters = taxMasterTask.Result,
                TaxSettings = taxSettingTask.Result
            };
            return result;
        }

        public async Task<PrintMasterDTO?> GetLastBillForPrintAsync(int BillId)
        {
            int HotBillId = BillId;

            if (!(HotBillId > 0))
            {
                var lastBill = await _db.HotBillMasters
                    .OrderByDescending(b => b.HotBillId)
                    .FirstOrDefaultAsync();



                if (lastBill == null)
                    return null;

                HotBillId = lastBill.HotBillId;
            }

            var Bill = await _db.HotBillMasters
                    .Where(d=>d.HotBillId== HotBillId)
                   .OrderByDescending(b => b.HotBillId)
                   .FirstOrDefaultAsync();

            var details = await _db.HotBillItemDetail
                .Where(d => d.HotBillId == HotBillId)
                .ToListAsync();

            var items = await _db.BillItems
                .ToListAsync();

            var itemsunit = await _db.ItemParentChilds
                .ToListAsync();

            var branchName = await _db.BranchMasters
                .Where(d => d.branchId == _app.BranchId)
                .Select(d => d.branchName)
                .FirstOrDefaultAsync();

            var dto = new PrintMasterDTO
            {
                BranchName = branchName??"",
                DocNo = Bill.HotBillRefNo,
                DocDate = Bill.HotBillDate,
                DocTime = Bill.HotBillTime,
                Total = Bill.HotBillNeTAmt,
                TaxTotal = Bill.HotBillTaxTotal,
                PrintDetails = details.Select(d =>
                {
                    if(d.UnitId>0)
                    {
                        var item = itemsunit.FirstOrDefault(i => i.parentItemId == d.ItemId && i.unitId == d.UnitId);
                        return new PrintDetailDTO
                        {
                            ItemId = d.ItemId,
                            ItemName = item?.childItemname ?? "Unknown",
                            Qty = d.Qty,
                            Total = d.DetAmt,
                            Price = d.DetRate
                        };

                    }
                    else
                    {
                        var item = items.FirstOrDefault(i => i.itemId == d.ItemId && d.UnitId == 0);
                        return new PrintDetailDTO
                        {
                            ItemId = d.ItemId,
                            ItemName = item?.itemName ?? "Unknown",
                            Qty = d.Qty,
                            Total = d.DetAmt,
                            Price = d.DetRate
                        };

                    }
                        
                }).ToList()
            };

            return dto;
        }

        public async Task<Result<List<HotBillMaster>>> GetHotBillListAsync(
    DateTime? fromDate = null,
    DateTime? toDate = null)
        {
            try
            {
                var query = _db.HotBillMasters
                    .AsNoTracking()
                    .Include(b => b.BillCashiers.Where(c => c.CancelInfo == null))
                    .Include(b => b.HotBillItemDetails) 
                    .Include(b=>b.CancelInfo)
                    .Where(d=>d.CancelInfo==null)
                    .OrderByDescending(b => b.HotBillId)
                    .AsQueryable();

                if (fromDate.HasValue)
                    query = query.Where(b => b.HotBillDate >= fromDate.Value);

                if (toDate.HasValue)
                    query = query.Where(b => b.HotBillDate <= toDate.Value);

                var bills = await query.ToListAsync();

                if (bills.Count == 0)
                    return Result<List<HotBillMaster>>.Failure("No sales found in the selected range.");

                return Result<List<HotBillMaster>>.Success(bills);
            }
            catch (Exception ex)
            {
                return Result<List<HotBillMaster>>.Failure($"Error fetching sale history: {ex.Message}");
            }
        }


        public async Task<Result<List<HotBillItemDetail>>> GetHotBillDetail(int HotBillID)
        {
            try
            {
                var query = _db.HotBillItemDetail
                    .Where(b => b.HotBillId == HotBillID)
                    .AsNoTracking()
                    .AsQueryable();


                var bills = await query.ToListAsync();

                if (bills.Count == 0)
                    return Result<List<HotBillItemDetail>>.Failure("No detai found.");

                return Result<List<HotBillItemDetail>>.Success(bills);
            }
            catch (Exception ex)
            {
                return Result<List<HotBillItemDetail>>.Failure($"Error fetching detail: {ex.Message}");
            }
        }

        public async Task<Result<bool>> SaveHotBillCancelAsync(HotBillCancel cancel)
        {
            if (cancel == null)
                return Result<bool>.Failure("Cancel data cannot be null.");

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // 1️⃣ Find the HotBill record to cancel (not already cancelled)
                var hotBill = await _db.HotBillMasters
                    .Include(b => b.BillCashiers)
        .                   ThenInclude(c => c.CancelInfo)
                    .Include(b => b.CancelInfo)
                    .FirstOrDefaultAsync(b => b.HotBillId == cancel.HotBillId && b.CancelInfo == null);


                bool hasPendingCashier = hotBill.BillCashiers.Any(c => c.CancelInfo == null);

                if (hasPendingCashier)
                {
                    return Result<bool>.Failure("Bill has active cashier records that are not cancelled.");
                }

                if (hotBill == null)
                    return Result<bool>.Failure("Hot bill not found or already cancelled.");

                // 2️⃣ Create a new HotBillCancel entry
                var hotBillCancel = new HotBillCancel
                {
                    HotBillId = hotBill.HotBillId,
                    CancelReason = cancel.CancelReason,
                    CancelDate = cancel.CancelDate,
                    CancelTime = cancel.CancelTime,
                    CancelledByEmpId = cancel.CancelledByEmpId,
                    BranchId = cancel.BranchId,
                    IsSynced = false
                };

                await _db.HotBillCancels.AddAsync(hotBillCancel);


                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<bool>.Failure($"Failed to cancel HotBill: {ex.Message}");
            }
        }


        public async Task<Result<bool>> SaveHotBillCashierCancelAsync(HotBillCancel cancel)
        {
            if (cancel == null)
                return Result<bool>.Failure("Cancel data cannot be null.");

            await using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // 1️⃣ Get the HotBillMaster including all related BillCashiers
                var billcashier = await _db.BillCashiers
                    .Include(b => b.CancelInfo)
                    .Where(b=>b.CancelInfo==null)
                    .ToListAsync();

                if (billcashier == null)
                    return Result<bool>.Failure("Hot bill payment not found.");

                foreach (var cashier in billcashier)
                {
                    var cashierCancel = new BillCashierCancel
                    {
                        BillCashierId = cashier.BillCashierId,
                        BillCashierServerId = cashier.ServerHotBillCashId,
                        CancelReason = cancel.CancelReason,
                        CancelDate = cancel.CancelDate,
                        CancelTime = cancel.CancelTime,
                        CancelledByEmpId = cancel.CancelledByEmpId,
                        BranchId = cancel.BranchId,
                        IsSynced = false
                    };

                    await _db.BillCashierCancels.AddAsync(cashierCancel);
                }


                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<bool>.Failure($"Failed to cancel HotBill: {ex.Message}");
            }
        }

        public async Task<Result<HotBillMaster>> AddCashiersToHotBillAsync(int hotBillId, List<BillCashier> newCashiers)
        {
            // 1️⃣ Validate inputs
            if (hotBillId <= 0)
                return Result<HotBillMaster>.Failure("Invalid HotBillId.");

            if (newCashiers == null || newCashiers.Count == 0)
                return Result<HotBillMaster>.Failure("No cashier payments provided.");

            // 2️⃣ Fetch bill
            var bill = await _db.HotBillMasters
                .Include(b => b.BillCashiers)
                .FirstOrDefaultAsync(b => b.HotBillId == hotBillId);

            if (bill == null)
                return Result<HotBillMaster>.Failure($"HotBill with ID {hotBillId} not found.");

            // 3️⃣ Begin transaction
            await using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                // 4️⃣ Get current max cashier number
                int currentMaxCashier = await _db.BillCashiers
                    .AsNoTracking()
                    .MaxAsync(b => (int?)b.HotBillCashNo) ?? 0;

                // 5️⃣ Add new cashier entries
                foreach (var cashier in newCashiers)
                {
                    currentMaxCashier++;
                    cashier.HotBillId = bill.HotBillId;
                    cashier.HotBillCashNo = currentMaxCashier;
                    cashier.HotBillCashPrefix = bill.HotBillPrefix;
                    cashier.HotBillCashRefNo = $"{bill.HotBillPrefix}{currentMaxCashier}";

                    _db.BillCashiers.Add(cashier);
                }

                // 6️⃣ Save and commit
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                // Reload with new cashiers
                await _db.Entry(bill).Collection(b => b.BillCashiers).LoadAsync();

                return Result<HotBillMaster>.Success(bill);
            }
            catch (DbUpdateException dbEx)
            {
                await transaction.RollbackAsync();
                return Result<HotBillMaster>.Failure($"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result<HotBillMaster>.Failure($"Error adding cashiers: {ex.Message}");
            }
        }

        public async Task<Result<List<BillCashier>>> GetHotBillCashierDetailsAsync(int hotBillId)
        {
            try
            {
                if (hotBillId <= 0)
                    return Result<List<BillCashier>>.Failure("Invalid HotBill ID.");

                var billCashiers = await _db.BillCashiers
                    .Include(c => c.CancelInfo)
                    .Where(c => c.HotBillId == hotBillId && c.CancelInfo == null) // ✅ only uncancelled
                    .AsNoTracking()
                    .ToListAsync();

                if (billCashiers.Count == 0)
                    return Result<List<BillCashier>>.Failure("No uncancelled payments found for this bill.");

                return Result<List<BillCashier>>.Success(billCashiers);
            }
            catch (Exception ex)
            {
                return Result<List<BillCashier>>.Failure($"Error fetching cashier details: {ex.Message}");
            }
        }

    }
}
