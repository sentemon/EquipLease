﻿// <auto-generated />
using System;
using EquipLease.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EquipLease.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250119180113_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EquipLease.Core.Entities.EquipmentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AreaPerUnit")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EquipmentTypes");
                });

            modelBuilder.Entity("EquipLease.Core.Entities.PlacementContract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EquipmentQuantity")
                        .HasColumnType("int");

                    b.Property<Guid>("EquipmentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductionFacilityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentTypeId");

                    b.HasIndex("ProductionFacilityId");

                    b.ToTable("PlacementContracts");
                });

            modelBuilder.Entity("EquipLease.Core.Entities.ProductionFacility", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StandardArea")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProductionFacilities");
                });

            modelBuilder.Entity("EquipLease.Core.Entities.PlacementContract", b =>
                {
                    b.HasOne("EquipLease.Core.Entities.EquipmentType", "EquipmentType")
                        .WithMany()
                        .HasForeignKey("EquipmentTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("EquipLease.Core.Entities.ProductionFacility", "ProductionFacility")
                        .WithMany()
                        .HasForeignKey("ProductionFacilityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EquipmentType");

                    b.Navigation("ProductionFacility");
                });
#pragma warning restore 612, 618
        }
    }
}
