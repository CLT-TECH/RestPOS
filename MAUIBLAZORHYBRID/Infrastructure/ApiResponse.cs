using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Infrastructure
{
    public class ApiResponse
    {
        public bool IsSuccess { get; private set; }
        public int? ServerId { get; private set; }
        public string ErrorMessage { get; private set; }

        public static ApiResponse Success(int? serverId) => new() { IsSuccess = true, ServerId = serverId };
        public static ApiResponse Fail(string error) => new() { IsSuccess = false, ErrorMessage = error };
    }
}
