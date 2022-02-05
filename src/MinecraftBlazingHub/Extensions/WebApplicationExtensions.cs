using Microsoft.EntityFrameworkCore;
using MinecraftApi.Core.Models;
using MinecraftApi.Ef.Models;
using MinecraftApi.Integrations.Models;

namespace MinecraftBlazingHub.Extensions
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
                    if (db != null)
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
        ///// <summary>
        ///// Use this extension method to seed data from known plugins.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="host"></param>
        ///// <returns></returns>
        //public static IHost SeedData<T>(this IHost host) where T : PluginContext
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        try
        //        {
        //            var db = services.GetRequiredService<T>();
        //            if (db != null)
        //            {
        //                var plugins = new List<Plugin>();

        //                plugins.Add(new Plugin(new MinecraftApi.Plugins.Vanilla.MinecraftMainPlugin())); //Plugin defines a constructor that takes IPlugin. We create all the plugins here.
        //                plugins.Add(RoleCommands.LuckyPermsPlugin);
        //                foreach (var plugin in plugins)
        //                {
        //                    var originalPlugin = db.Plugins!.FirstOrDefault(x => x.Name == plugin.Name);
        //                    if (originalPlugin == null)
        //                    {
        //                        db.Plugins?.Add(plugin);
        //                    }
        //                }
        //                //in the future I want to use DI. Define something like IDefinedPlugin, or IReadOnlyPlugin, and then register all the IReadonlyplugins and get the IEnumenrable in here.
        //                db.SaveChanges();


        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            var logger = services.GetRequiredService<ILogger<Program>>();
        //            logger.LogError(ex, "An error occurred while migrating the database.");
        //        }
        //    }
        //    return host;
        //}
    }
}
