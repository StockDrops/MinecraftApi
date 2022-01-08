using Microsoft.EntityFrameworkCore;

namespace MinecraftApi.Api.Extensions
{
    /// <summary>
    /// Extensions for the WebBuilder
    /// </summary>
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Migrates the given database using EF core.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="webApplication"></param>
        /// <returns></returns>
        public static IHost MigrateDatabase<T>(this IHost webApplication) where T : DbContext
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var db = services.GetRequiredService<T>();
                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
            return webApplication;
        }
    }
}
