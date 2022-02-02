﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MinecraftApi.Core.Models;
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
        public DbSet<Plugin>? Plugins { get; set; }
        /// <summary>
        /// Commands db set.
        /// </summary>
        public DbSet<Command>? Commands { get; set; }
        /// <summary>
        /// Arguments to store in the database
        /// </summary>
        public DbSet<Argument>? Arguments { get; set; }
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


        /// <inheritdoc/>
        /// All the configuration is done here to avoid the models from having attributes depending on EF Core. I want the models to be POCO as much as possible.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Plugin>()
                .HasIndex(p => p.Name)
                .IsUnique();
            modelBuilder.Entity<MinecraftPlayer>()
                .HasKey(p => p.Id);

        }
    }
}
