using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MinecraftApi.Core.Models;
using MinecraftApi.Core.Models.Commands;
using MinecraftApi.Core.Models.Integrations;
using MinecraftApi.Core.Models.Minecraft.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftApi.Ef.Models
{
    /// <summary>
    /// Base db context for use in other specific infrastructure projects.
    /// </summary>
    public class PluginContext : DbContext
    {
        /// <summary>
        /// Configuration used.
        /// </summary>
        protected readonly DatabaseConfigurationOptions _databaseConfiguration = new DatabaseConfigurationOptions();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseConfigurationOptions"></param>
        public PluginContext(IOptions<DatabaseConfigurationOptions> databaseConfigurationOptions)
        {
            _databaseConfiguration = databaseConfigurationOptions.Value;
        }
        /// <summary>
        /// PluginContext for configuring externally
        /// </summary>
        /// <param name="options"></param>
        public PluginContext(DbContextOptions options) : base(options)
        {
        }



        /// <summary>
        /// Plugins for the EF Core base.
        /// </summary>
        public DbSet<Plugin> Plugins => Set<Plugin>();
        /// <summary>
        /// Commands db set.
        /// </summary>
        public DbSet<Command> Commands => Set<Command>();
        /// <summary>
        /// Arguments to store in the database
        /// </summary>
        public DbSet<SavedArgument> Arguments => Set<SavedArgument>();
        /// <summary>
        /// Set of arguments used in previous command execution.
        /// </summary>
        public DbSet<RanArgument> RanArguments => Set<RanArgument>();
        /// <summary>
        /// Raw commands, only strings.
        /// </summary>
        public DbSet<BaseRanCommand> BaseRanCommands => Set<BaseRanCommand>();
        /// <summary>
        /// Full commands ran
        /// </summary>
        public DbSet<RanCommand> RanCommands => Set<RanCommand>();
        /// <summary>
        /// Players set
        /// </summary>
        public DbSet<MinecraftPlayer> MinecraftPlayers => Set<MinecraftPlayer>();
        /// <summary>
        /// Link requests.
        /// </summary>
        public DbSet<LinkRequest> LinkRequests => Set<LinkRequest>();
        /// <summary>
        /// The OAUTH2 tokens saved.
        /// </summary>
        public DbSet<Token> Tokens => Set<Token>();
        /// <summary>
        /// The linked players (MC + External Id).
        /// </summary>
        public DbSet<LinkedPlayer> LinkedPlayers => Set<LinkedPlayer>();
        /// <summary>
        /// Set of roles to awards to players.
        /// </summary>
        public DbSet<Role> Roles => Set<Role>();
        /// <summary>
        /// Set of linked players with roles.
        /// </summary>
        public DbSet<LinkedPlayerRole> LinkedPlayerRoles => Set<LinkedPlayerRole>();


        /// <inheritdoc/>
        /// All the configuration is done here to avoid the models from having attributes depending on EF Core. I want the models to be POCO as much as possible.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Plugin>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Command>()
                .HasIndex(p => p.Prefix)
                .IsUnique();
            modelBuilder.Entity<MinecraftPlayer>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<LinkedPlayer>()
                .HasIndex(p => p.PlayerId)
                .IsUnique();
            modelBuilder.Entity<Token>()
                .HasIndex(p => p.LinkedPlayerId)
                .IsUnique();
        }
    }
}
