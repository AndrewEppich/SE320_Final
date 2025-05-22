using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace receiptProject.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                bool created = await context.Database.EnsureCreatedAsync();
                
                if (created)
                {
                    logger.LogInformation("Database was created successfully");
                }

                if (await context.Users.AnyAsync())
                {
                    logger.LogInformation("Database already populated, skipping initialization");
                    return;
                }

                logger.LogInformation("Initializing database with SQL script");

                var connectionString =
                    "Server=localhost;Database=ReceiptProject;User=root;Password=CPSC40801;Port=3306;AllowPublicKeyRetrieval=true;SslMode=none;";
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Database connection string is not configured.");
                }


                var parts = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
                foreach (var part in connectionString.Split(';'))
                {
                    var trimmedPart = part.Trim();
                    if (string.IsNullOrEmpty(trimmedPart))
                        continue;
                        
                    var keyValue = trimmedPart.Split('=', 2);
                    if (keyValue.Length != 2)
                        continue;
                        
                    parts[keyValue[0].ToLowerInvariant()] = keyValue[1];
                }
                

                if (!parts.ContainsKey("server") || !parts.ContainsKey("database") || 
                    !parts.ContainsKey("user") || !parts.ContainsKey("password"))
                {
                    logger.LogError("Connection string is missing required parts (server, database, user, or password)");
                    return;
                }

                var sqlScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "receiptProjectBackend", "Data", "receiptProject.sql");

                if (!File.Exists(sqlScriptPath))
                {
                    logger.LogWarning("SQL script not found at path: {Path}", sqlScriptPath);
                    return;
                }

                var mysqlCommand = $"mysql -h {parts["server"]} -u {parts["user"]} -p{parts["password"]} {parts["database"]} < \"{sqlScriptPath}\"";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{mysqlCommand}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                try
                {
                    process.Start();
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    if (process.ExitCode != 0)
                    {
                        logger.LogError("Failed to import SQL script. Error: {Error}", error);
                        throw new Exception($"Failed to import SQL script: {error}");
                    }

                    logger.LogInformation("Database initialized successfully");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error executing MySQL command: {Command}", mysqlCommand);
                    throw;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initializing the database.");
                throw;
            }
        }
    }
} 