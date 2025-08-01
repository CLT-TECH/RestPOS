using MAUIBLAZORHYBRID.Data.Data;
using MAUIBLAZORHYBRID.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Interfaces
{
    public interface IHotKOTSaveService
    {
        Task<Result<HotKOT>> SaveHotKOTAsync(HotKOT hotKOT);
    }
}
