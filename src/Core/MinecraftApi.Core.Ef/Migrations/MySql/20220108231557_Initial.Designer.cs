// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinecraftApi.Ef.Models.Contexts;

#nullable disable

namespace MinecraftApi.Ef.Migrations.MySql
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20220108231557_Initial")]
    partial class Initial
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MinecraftApi.Ef.Models.Argument", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Arguments");
                });

            modelBuilder.Entity("MinecraftApi.Ef.Models.Command", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Prefix")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("MinecraftApi.Ef.Models.Plugin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Plugins");
                });
#pragma warning restore 612, 618
        }
    }
}
