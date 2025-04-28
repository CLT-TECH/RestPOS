using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    internal class LoadingService
    {
        public event Action<int> ProgressChanged;
        private int _progress;

        public int Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                ProgressChanged?.Invoke(value);
            }
        }
    }
}
