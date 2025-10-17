using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Interfaces
{
    public interface IShortcutService
    {
        void CreateDesktopShortcut();
        bool ShortcutExists();
    }
}
