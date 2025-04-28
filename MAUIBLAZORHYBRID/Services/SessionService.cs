using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Services
{
    public class SessionService
    {
        private Exception _lastError;
        public Exception LastError { get => _lastError; private set => _lastError = value; }
        public string? CurrentUser { get; private set; }
        public event Action OnAuthenticationStateChanged = null;
        public void Login(string username)
        {
            CurrentUser = username;
            OnAuthenticationStateChanged?.Invoke();
        }

        public void Logout()
        {
            CurrentUser = null;
            OnAuthenticationStateChanged?.Invoke();
        }

        public bool IsAuthenticated => !string.IsNullOrEmpty(CurrentUser);
        public void SetLastError(Exception exception)
        {
            LastError = exception;
        }

        public void ClearLastError()
        {
            LastError = null;
        }
    }
}