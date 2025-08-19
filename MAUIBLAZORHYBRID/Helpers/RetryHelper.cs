using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIBLAZORHYBRID.Helpers
{
    public class RetryHelper
    {
        public static async Task<T> RetryOnTransientErrors<T>(
          Func<Task<T>> action,
          int maxRetries = 3,
          int baseDelayMs = 500)
        {
            for (int attempt = 1; ; attempt++)
            {
                try
                {
                    return await action();
                }
                catch (HttpRequestException) when (attempt < maxRetries)
                {
                    await Task.Delay(baseDelayMs * attempt);
                }
                catch (DbUpdateException) when (attempt < maxRetries)
                {
                    await Task.Delay(baseDelayMs * attempt);
                }
            }
        }
    }
}
