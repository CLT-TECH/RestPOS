using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Helpers
{
    public class PasswordManager
    {
        private const string PasswordKey = "App-Auth-Key";

        // Save password securely
        public static async Task SavePasswordAsync(string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            await SecureStorage.SetAsync(PasswordKey, hash);
        }

        // Check entered password
        public static async Task<bool> VerifyPasswordAsync(string enteredPassword)
        {
            var storedHash = await SecureStorage.GetAsync(PasswordKey);
            if (string.IsNullOrEmpty(storedHash))
                return false;

            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }

        // Check if password exists (for first setup)
        public static async Task<bool> HasPasswordAsync()
        {
            var storedHash = await SecureStorage.GetAsync(PasswordKey);
            return !string.IsNullOrEmpty(storedHash);
        }

        // Optional: reset
        public static void ClearPassword()
        {
            SecureStorage.Remove(PasswordKey);
        }
    }
}
