using EquipLease.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipLease.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<ProductionFacility> ProductionFacilities { get; set; }
    public DbSet<PlacementContract> PlacementContracts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EquipmentType>().HasData(
            EquipmentType.CreateEquipmentType("ET001" ,"Type A", 50),
            EquipmentType.CreateEquipmentType("ET002","Type B", 100)
        );

        modelBuilder.Entity<ProductionFacility>().HasData(
            ProductionFacility.CreateProductionFacility("PF001", "Facility 1", 500 ),
            ProductionFacility.CreateProductionFacility("PF002", "Facility 2", 800 )
        );

        modelBuilder.Entity<PlacementContract>().HasData(
            PlacementContract.CreatePlacementContract("PF001", "ET001", 10),
            PlacementContract.CreatePlacementContract("PF002", "ET002", 5)
        );

        modelBuilder.Entity<EquipmentType>(entity =>
        {
            entity.HasKey(et => et.Code);
            entity.Property(et => et.Code).IsRequired();
            entity.Property(et => et.Name).IsRequired();
            entity.Property(et => et.AreaPerUnit).IsRequired();
        });

        modelBuilder.Entity<ProductionFacility>(entity =>
        {
            entity.HasKey(pf => pf.Code);
            entity.Property(pf => pf.Code).IsRequired();
            entity.Property(pf => pf.Name).IsRequired();
            entity.Property(pf => pf.StandardArea).IsRequired();
        });

        modelBuilder.Entity<PlacementContract>(entity =>
        {
            entity.HasKey(pc => pc.Id);
            entity.HasOne(pc => pc.ProductionFacility)
                .WithMany()
                .HasForeignKey(pc => pc.ProductionFacilityCode)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(pc => pc.EquipmentType)
                .WithMany()
                .HasForeignKey(pc => pc.EquipmentTypeCode)
                .OnDelete(DeleteBehavior.Restrict);
            entity.Property(pc => pc.EquipmentQuantity).IsRequired();
        });
    }
}
