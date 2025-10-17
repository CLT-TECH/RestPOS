using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Data.DTO;
using MAUIBLAZORHYBRID.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Mappers
{
    public class MappingService
    {
        public BillMasterDTO MapToBillMasterDTO(HotBillMaster bill)
        {
            return new BillMasterDTO
            {
                Hot_Bill_Type = bill.HotBillType,
                Hot_Bill_Prefix = bill.HotBillPrefix,
                Hot_Bill_No = bill.HotBillNo,
                Bearer_ID = bill.BearerId,
                Hot_Bill_Item_Total = bill.HotBillItemTotal,
                Hot_Bill_Tax_Total = bill.HotBillTaxTotal,
                B4Round_Amt = bill.B4roundAmt,
                Round_Need = bill.RoundNeed,
                RoundOff_Val = bill.RoundOffVal,
                Hot_Bill_NeT_Amt = bill.HotBillNeTAmt,
                Cashier_Found = bill.CashierFound,
                Hot_Bill_Notes = bill.HotBillNotes,
                App_Machine_ID = bill.AppMachineId,
                Branch_ID = bill.BranchId,
                Dining_Space_ID = bill.DiningSpaceId,
                Entered_Emp_ID = bill.EnteredEmpId,
                Counter_ID = bill.CounterId,
                Customer_Mobile = bill.CustomerMobile,
                Hot_Bill_Date =bill.HotBillDate,
                Hot_Bill_Time=bill.HotBillTime,
                Items = bill.HotBillItemDetails.Select(MapToItemDetailDTO).ToList(),
                KOTs = bill.HotBillAgainstKots.Select(MapToKotDTO).ToList(),
                TaxDetails = bill.HotBillTaxDetails.Select(MapToTaxDetailDTO).ToList(),
                BillCashiers = bill.BillCashiers.Select(MapToBillCahierDTO).ToList()
                
            };
        }

        public HotBillItemDetailDTO MapToItemDetailDTO(HotBillItemDetail item)
        {
            return new HotBillItemDetailDTO
            {
                Hot_Bill_ID = item.HotBillId,
                Item_ID = item.ItemId,
                BarCode = item.BarCode,
                Qty = item.Qty,
                Unit_ID = item.UnitId,
                Det_Rate = item.DetRate,
                Det_Amt = item.DetAmt,
                Det_Disc_Per = item.DetDiscPer,
                Det_Disc_Amt = item.DetDiscAmt,
                Det_Gross_Amt = item.DetGrossAmt,
                Det_Tax_Per = item.DetTaxPer,
                Det_Tax_Amt = item.DetTaxAmt,
                Det_Net_Amt = item.DetNetAmt
            };
        }

        public HotBillTaxDetailDTO MapToTaxDetailDTO(HotBillTaxDetail tax)
        {
            return new HotBillTaxDetailDTO
            {
                TaX_ID = tax.TaXId,
                Taxable_Amt = tax.TaxableAmt,
                Tax_Per = tax.TaxPer,
                Tax_Amt = tax.TaxAmt
            };
        }
        public HotBillAgainstKOTDTO MapToKotDTO(HotBillAgainstKot kot)
        {
            return new HotBillAgainstKOTDTO
            {
                Hot_KOT_ID = kot.HotKot.ServerKOTId
            };
        }

        public HotKOTMasterDTO MapToHotKOTMasterDTO(HotKOT kot)
        {
            return new HotKOTMasterDTO
            {
                Hot_KOT_Type = kot.HotKOTType,
                Hot_KOT_Prefix = kot.HotKOTPrefix,
                Hot_KOT_No = kot.HotKOTNo,
                Hot_KOT_Ref_No = kot.HotKOTRefNo,
                Hot_KOT_Date = kot.HotKOTDate,
                Hot_KOT_Time = kot.HotKOTTime,
                Bearer_ID = kot.BearerID,
                No_Of_Guests = kot.NoOfGuests,
                Hot_Kot_Amt = kot.HotKotAmt,
                Hot_KOT_Notes = kot.HotKOTNotes,
                App_Machine_ID = kot.AppMachineID,
                Branch_ID = kot.BranchID,
                Dining_Space_ID = kot.DiningSpaceID,
                Entered_Emp_ID = kot.EnteredEmpID,
                Counter_ID = kot.CounterID,
                Items = kot.Items.Select(MapToHotKOTItemDetailDTO).ToList(),
                Tables = kot.Tables == null
                        ? new List<HotKOTTableDTO>()
                        : new List<HotKOTTableDTO>
                        {
                            new() { Hot_Tab_ID = kot.Tables.HotTabID
                        }}
            };


        }

        private HotKOTItemDetailDTO MapToHotKOTItemDetailDTO(HotKOTItemDetail item)
        {
            return new HotKOTItemDetailDTO
            {
                Hot_KOT_ID = item.HotKOTId,
                Item_ID = item.ItemID,
                BarCode = item.BarCode,
                Qty = item.Qty,
                Unit_ID = item.UnitID,
                Det_Rate = item.DetRate,
                Det_Amt = item.DetAmt,
                Det_Disc_Per = item.DetDiscPer,
                Det_Disc_Amt = item.DetDiscAmt,
                Det_Gross_Amt = item.DetGrossAmt,
                Det_Tax_Per = item.DetTaxPer,
                Det_Tax_Amt = item.DetTaxAmt,
                Det_Net_Amt = item.DetNetAmt,
                Hot_Kot_Item_Notes = item.HotKotItemNotes
            };
        }
        public StockTransferDTO MapToStockTransferDTO(StockTransfer transfer)
        {
            return new StockTransferDTO
            {
                Stk_Tr_SlNo=transfer.StkTrSlNo,
                Stk_Tr_Prefix = transfer.Prefix,
                Stk_Tr_RefNo = transfer.RefNo,
                Stk_Tr_Date = transfer.TransferDate,
                Stk_Tr_Time = transfer.TransferTime,
                Stock_From_Type = transfer.FromType,
                Stock_To_Type = transfer.ToType,
                Entry_Branch_ID = transfer.BranchId,
                Entry_Emp_ID = transfer.EmployeeId,
                Stk_Tr_Notes = transfer.Notes,
                From_Godown_ID = transfer.FromGodownId,
                From_Counter_ID = transfer.FromCounterId,
                To_Godown_ID = transfer.ToGodownId,
                To_Counter_ID = transfer.ToCounterId,
                Items = transfer.StockTransferDetails.Select(d => new StockTransferItemDTO
                {
                    Main_Bar_Item_ID = d.MainBarItemId,
                    Unit_ID = d.UnitId,
                    TR_Qty = d.Quantity,
                    Stk_Tr_Det_Notes = d.Notes ?? ""
                }).ToList()
            };
        }

        public StockInwardDTO MapToStockInwardDTO(StockInwardMaster master)
        {
            return new StockInwardDTO
            {
                StockInwardDocNo = master.StockInwardDocNo,
                StockInwardSlNo = master.StockInwardSlNo,
                StockInwardPrefix = master.StockInwardPrefix,
                StockInwardRefNo = master.StockInwardRefNo,
                StockInwardDate = master.StockInwardDate,
                StockInwardTime = master.StockInwardTime,
                StockInwardNotes = master.StockInwardNotes,
                BranchId = master.BranchId,
                LoginEmpId = master.LoginEmpId,
                VendorId = master.VendorId,
                StockInwardDetails = master.StockInwardDetails.Select(d => new StockInwardDetailsDTO
                {
                    BarItemId = d.BarItemId,
                    UnitId = d.UnitId,
                    InwardQty = d.InwardQty
                }).ToList()
            };
        }

        public HotBillCashierDTO MapToBillCahierDTO(BillCashier billcash)
        {
            return new HotBillCashierDTO
            {
                Paymode = billcash.PaymentMode,
                TotalAmount = billcash.TotalAmount,
                HotBillCashPrefix = billcash.HotBillCashPrefix,
                HotBillCashNo = billcash.HotBillCashNo,
                HotBillCashRefNo = billcash.HotBillCashRefNo
            };
        }


        public StockTransferCancelDTO MapToStockTransferCancelDTO(StockTransferCancel master)
        {
            return new StockTransferCancelDTO
            {
                StkTrId = master.StockTransfer!.ServerTransferId,
                CancelledBy=master.CancelledByEmpId,
                CancelDate = master.CancelDate,
                CancelReason = master.CancelReason,
                CancelTime = master.CancelTime

            };

        }
        public BillCashierCancelDTO MapToBillCashierCancelDTO(BillCashierCancel master)
        {
            return new BillCashierCancelDTO
            {
                HotBillCashId = master.BillCashier!.ServerHotBillCashId,
                CancelReason = master.CancelReason,
                CancelDate = master.CancelDate,
                CancelTime = master.CancelTime,
                CancelledBy = master.CancelledByEmpId
            };

        }

        public HotBillCancelDTO MapToHotBillCancelDTO(HotBillCancel master)
        {
            return new HotBillCancelDTO
            {
                HotBillId = master.HotBill!.ServerHotBillId,
                CancelReason = master.CancelReason,
                CancelDate = master.CancelDate,
                CancelTime = master.CancelTime,
                CancelledBy = master.CancelledByEmpId
            };

        }
        

        public HotBillCashierCancelRequest MapToBillCashierCancelAllDTO(int hotBillId, BillCashierCancel master)
        {
            return new HotBillCashierCancelRequest
            {
                HotBillId = hotBillId,
                CashierCancels = new List<BillCashierCancelDTO>
                {
                    new BillCashierCancelDTO
                    {
                        HotBillCashId = master.BillCashierId,
                        CancelReason = master.CancelReason,
                        CancelDate = master.CancelDate,
                        CancelTime = master.CancelTime,
                        CancelledBy = master.CancelledByEmpId
                    }
                }
            };
        }

    }
}
