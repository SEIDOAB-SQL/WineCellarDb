﻿// <auto-generated />
using System;
using DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbContext.Migrations.SqlServer
{
    [DbContext(typeof(csMainDbContext.SqlServerDbContext))]
    [Migration("20240514114733_initial_migration")]
    partial class initial_migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.csWine", b =>
                {
                    b.Property<Guid>("WineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("WineCellarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("WineId");

                    b.HasIndex("WineCellarId");

                    b.ToTable("Wines");
                });

            modelBuilder.Entity("Models.csWineCellar", b =>
                {
                    b.Property<Guid>("WineCellarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Seeded")
                        .HasColumnType("bit");

                    b.HasKey("WineCellarId");

                    b.ToTable("WineCellars");
                });

            modelBuilder.Entity("Models.csWine", b =>
                {
                    b.HasOne("Models.csWineCellar", "WineCellar")
                        .WithMany("Wines")
                        .HasForeignKey("WineCellarId");

                    b.Navigation("WineCellar");
                });

            modelBuilder.Entity("Models.csWineCellar", b =>
                {
                    b.Navigation("Wines");
                });
#pragma warning restore 612, 618
        }
    }
}
