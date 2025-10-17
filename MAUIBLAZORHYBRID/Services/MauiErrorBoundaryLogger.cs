using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Maui.Storage;
using System.Diagnostics;

namespace MAUIBLAZORHYBRID.Services
{
    internal class MauiErrorBoundaryLogger : IErrorBoundaryLogger
    {
        private readonly SessionService _sessionService;

        public MauiErrorBoundaryLogger(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async ValueTask LogErrorAsync(Exception exception)
        {
            try
            {
                Debug.WriteLine($"Error logged: {exception}");
                _sessionService.SetLastError(exception);

                // Platform-safe logging
                await SafeLogToFile(exception);
            }
            catch (Exception loggingEx)
            {
                Debug.WriteLine($"Error logging failed: {loggingEx}");
            }
        }

        private async Task SafeLogToFile(Exception exception)
        {
            try
            {
                string logFilePath = Path.Combine(GetProjectFolder(), "error.log");
                await File.AppendAllTextAsync(logFilePath,
                    $"[{DateTime.UtcNow}] {exception}\n\n");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"File logging failed: {ex}");
            }
        }


        private string GetProjectFolder()
        {
            // Start from the base directory (e.g., bin\Debug\net9.0-windows10.0.19041.0)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Navigate up to the project folder (assuming bin\Debug structure)
            string projectFolder = Directory.GetParent(baseDirectory)?.Parent?.Parent?.Parent?.FullName
                ?? throw new InvalidOperationException("Could not determine project folder.");

            return projectFolder;
        }
    }
}