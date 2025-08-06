using System.IO;
using System;

public static class DbConfig
{
    // Builds the full path and returns the SQLite data-source string.
    public static string ConnectionString
    {
        get
        {
            var appData = Environment
                .GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = Path.Combine(appData, "MAUIHybridApp");
            Directory.CreateDirectory(folder);         // ensure folder exists
            var filePath = Path.Combine(folder, "posapplication.db");
            return $"Data Source={filePath}";
        }
    }
}
