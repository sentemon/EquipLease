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
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<EquipmentType>(entity =>
        {
            entity.HasKey(et => et.Id);

            entity.Property(et => et.Id)
                  .ValueGeneratedOnAdd();
            
            entity.Property(et => et.Code)
                  .IsRequired();
            
            entity.Property(et => et.Name)
                  .IsRequired();
            
            entity.Property(et => et.AreaPerUnit)
                  .IsRequired();
        });
        
        modelBuilder.Entity<ProductionFacility>(entity =>
        {
            entity.HasKey(pf => pf.Id);

            entity.Property(pf => pf.Id)
                  .ValueGeneratedOnAdd();
            
            entity.Property(pf => pf.Code)
                  .IsRequired();

            entity.Property(pf => pf.Name)
                  .IsRequired();
            
            entity.Property(pf => pf.StandardArea)
                  .IsRequired();
        });
        
        modelBuilder.Entity<PlacementContract>(entity =>
        {
              entity.HasKey(pc => pc.Id);

              entity.Property(pc => pc.Id)
                    .ValueGeneratedOnAdd();
              
              entity.HasOne(pc => pc.ProductionFacility)
                    .WithMany()
                    .HasForeignKey(pc => pc.ProductionFacilityId)
                    .OnDelete(DeleteBehavior.Restrict);

              entity.HasOne(pc => pc.EquipmentType)
                    .WithMany()
                    .HasForeignKey(pc => pc.EquipmentTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

              entity.Property(pc => pc.EquipmentQuantity)
                    .IsRequired();
        });
    }
}
