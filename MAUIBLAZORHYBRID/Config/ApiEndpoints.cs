using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Config
{
    public class ApiEndpoints
    {
        public static class AppSync
        {
            public const string ItemData = "api/AppSync/itemdata";
            public const string OtherMasters = "api/AppSync/othermasters";
            public const string Masters = "api/AppSync/masters";
            public const string ItemParentChild = "api/AppSync/itemparentchild";
            public const string BarItemStock = "api/AppSync/baritemcounterstock";
            public static string BranchesWithMasters(string machineID) =>
           $"api/AppSync/branches-with-masters/{machineID}";

            public static string StockForCounter(int counterId) =>
            $"api/AppSync/stockforcounter/{counterId}";

            public static string StockForGodown(int godownId) =>
            $"api/AppSync/stockforgodown/{godownId}";
        }
    }
}
