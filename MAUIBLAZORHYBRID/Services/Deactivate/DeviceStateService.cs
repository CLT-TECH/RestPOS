using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services.Deactivate
{
    public class DeviceStateService
    {
        public event Action<bool> OnDeviceStateChanged;
        private bool _isActive = true;

        public bool IsActive => _isActive;

        public void SetDeviceState(bool isActive)
        {
            if (_isActive != isActive)
            {
                _isActive = isActive;
                OnDeviceStateChanged?.Invoke(_isActive);
            }
        }
    }
}
