using EquipLease.Core.Entities;
using EquipLease.Infrastructure.DTOs;
using EquipLease.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EquipLease.Infrastructure.Services;

public class PlacementContractService : IPlacementContractService
{
    private readonly AppDbContext _context;

    public PlacementContractService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PlacementContractDto?> CreatePlacementContractAsync(CreatePlacementContractDto dto)
    {
        var productionFacility = await _context.ProductionFacilities
            .FirstOrDefaultAsync(pf => pf.Code == dto.ProductionFacilityCode);

        var equipmentType = await _context.EquipmentTypes
            .FirstOrDefaultAsync(et => et.Code == dto.EquipmentTypeCode);

        if (productionFacility == null || equipmentType == null)
        {
            return null;
        }

        var usedArea = await _context.PlacementContracts
            .Where(pc => pc.ProductionFacilityId == productionFacility.Id)
            .SumAsync(pc => pc.EquipmentQuantity * pc.EquipmentType.AreaPerUnit);

        var requiredArea = dto.EquipmentQuantity * equipmentType.AreaPerUnit;

        if (usedArea + requiredArea > productionFacility.StandardArea)
        {
            return null;
        }

        var placementContract = PlacementContract.CreatePlacementContract(
            productionFacility.Id,
            equipmentType.Id,
            dto.EquipmentQuantity
        );

        _context.PlacementContracts.Add(placementContract);
        await _context.SaveChangesAsync();

        var resultDto = new PlacementContractDto(
            productionFacility.Name,
            equipmentType.Name,
            placementContract.EquipmentQuantity
        );

        return resultDto;
    }

    public async Task<List<PlacementContractDto>> GetPlacementContractsAsync()
    {
        var contracts = await _context.PlacementContracts
            .Include(pc => pc.ProductionFacility)
            .Include(pc => pc.EquipmentType)
            .ToListAsync();

        return contracts.Select(pc => new PlacementContractDto(
            pc.ProductionFacility.Name,
            pc.EquipmentType.Name,
            pc.EquipmentQuantity)
        ).ToList();
    }
}
