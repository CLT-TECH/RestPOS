using MAUIBLAZORHYBRID.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace MAUIBLAZORHYBRID.Services
{
    public class LoginService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        private readonly ILogger<LoginService> _logger;


        public LoginService(IDbContextFactory<AppDbContext> dbContextFactory, ILogger<LoginService> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> GetWelcomeMessageAsync()
        {
            try
            {
                // Access the raw resource file (ensure welcome.txt is in Resources/Raw and set as MauiAsset)
                using var stream = await FileSystem.OpenAppPackageFileAsync("welcome.txt");
                using var reader = new StreamReader(stream);
                var message = await reader.ReadToEndAsync();

                _logger.LogInformation("Successfully read welcome message from resource file");
                return string.IsNullOrWhiteSpace(message) ? "Welcome to the app!" : message;
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogWarning(ex, "Welcome message file not found");
                return "Welcome to the app! (File not found)";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading welcome message from resource file");
                return "Welcome to the app! (Error)";
            }
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "app.db"
            );


            using var dbContext = _dbContextFactory.CreateDbContext();
            if (await dbContext.Users.AnyAsync(u => u.Username == username))
                return false; // Username taken

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Username = username, Password = hashedPassword };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return false; // User not found

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
