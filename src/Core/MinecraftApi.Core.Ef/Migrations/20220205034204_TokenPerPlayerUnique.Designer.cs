﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinecraftApi.Ef.Models.Contexts;

#nullable disable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace MinecraftApi.Ef.Migrations
{
    [DbContext(typeof(SqlContext))]
    [Migration("20220205034204_TokenPerPlayerUnique")]
    partial class TokenPerPlayerUnique
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MinecraftApi.Core.Models.Command", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PluginId")
                        .HasColumnType("bigint");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PluginId");

                    b.HasIndex("Prefix")
                        .IsUnique();

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Commands.BaseRanCommand", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RanTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RawCommand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BaseRanCommands");

                    b.HasDiscriminator<string>("Discriminator").HasValue("BaseRanCommand");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Commands.RanArgument", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long?>("RanCommandId")
                        .HasColumnType("bigint");

                    b.Property<long>("SavedArgumentId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RanCommandId");

                    b.HasIndex("SavedArgumentId");

                    b.ToTable("RanArguments");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Integrations.Token", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AccessTokenGenerationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IntegrationType")
                        .HasColumnType("int");

                    b.Property<long>("LinkedPlayerId")
                        .HasColumnType("bigint");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scope")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TokenType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LinkedPlayerId")
                        .IsUnique();

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.LinkedPlayerRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("AssignedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckRoleBy")
                        .HasColumnType("datetime2");

                    b.Property<long>("LinkedPlayerId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LinkedPlayerId");

                    b.HasIndex("RoleId");

                    b.ToTable("LinkedPlayerRoles");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Minecraft.Players.LinkedPlayer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("LinkedPlayers");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Minecraft.Players.LinkRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Oauth2RequestUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RequestedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UniqueId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("LinkRequests");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.MinecraftPlayer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MinecraftPlayers");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Plugin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Plugins");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<long?>("TierId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.SavedArgument", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("CommandId")
                        .HasColumnType("bigint");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<bool>("Required")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CommandId");

                    b.ToTable("Arguments");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Commands.RanCommand", b =>
                {
                    b.HasBaseType("MinecraftApi.Core.Models.Commands.BaseRanCommand");

                    b.Property<long>("CommandId")
                        .HasColumnType("bigint");

                    b.HasIndex("CommandId");

                    b.HasDiscriminator().HasValue("RanCommand");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Command", b =>
                {
                    b.HasOne("MinecraftApi.Core.Models.Plugin", "Plugin")
                        .WithMany("Commands")
                        .HasForeignKey("PluginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plugin");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Commands.RanArgument", b =>
                {
                    b.HasOne("MinecraftApi.Core.Models.Commands.RanCommand", null)
                        .WithMany("RanArguments")
                        .HasForeignKey("RanCommandId");

                    b.HasOne("MinecraftApi.Core.Models.SavedArgument", "SavedArgument")
                        .WithMany()
                        .HasForeignKey("SavedArgumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SavedArgument");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Integrations.Token", b =>
                {
                    b.HasOne("MinecraftApi.Core.Models.Minecraft.Players.LinkedPlayer", "LinkedPlayer")
                        .WithMany()
                        .HasForeignKey("LinkedPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LinkedPlayer");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.LinkedPlayerRole", b =>
                {
                    b.HasOne("MinecraftApi.Core.Models.Minecraft.Players.LinkedPlayer", "LinkedPlayer")
                        .WithMany()
                        .HasForeignKey("LinkedPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinecraftApi.Core.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LinkedPlayer");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Minecraft.Players.LinkedPlayer", b =>
                {
                    b.HasOne("MinecraftApi.Core.Models.MinecraftPlayer", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Minecraft.Players.LinkRequest", b =>
                {
                    b.HasOne("MinecraftApi.Core.Models.MinecraftPlayer", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.SavedArgument", b =>
                {
                    b.HasOne("MinecraftApi.Core.Models.Command", "Command")
                        .WithMany("Arguments")
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Command");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Commands.RanCommand", b =>
                {
                    b.HasOne("MinecraftApi.Core.Models.Command", "Command")
                        .WithMany()
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Command");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Command", b =>
                {
                    b.Navigation("Arguments");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Plugin", b =>
                {
                    b.Navigation("Commands");
                });

            modelBuilder.Entity("MinecraftApi.Core.Models.Commands.RanCommand", b =>
                {
                    b.Navigation("RanArguments");
                });
#pragma warning restore 612, 618
        }
    }
}
