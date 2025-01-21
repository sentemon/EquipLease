using EquipLease.Core.Entities;
using EquipLease.Infrastructure.Common;
using EquipLease.Infrastructure.DTOs;
using EquipLease.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EquipLease.Infrastructure.Services;

public class PlacementContractService : IPlacementContractService
{
    private readonly AppDbContext _context;
    private readonly ILogger<PlacementContractService> _logger;

    public PlacementContractService(AppDbContext context, ILogger<PlacementContractService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<PlacementContractDto>> CreatePlacementContractAsync(CreatePlacementContractDto dto)
    {
        _logger.LogInformation("Attempting to create a placement contract.");

        if (dto.EquipmentQuantity <= 0)
        {
            _logger.LogWarning("Invalid equipment quantity: {Quantity}. Must be greater than zero.",
                dto.EquipmentQuantity);
            return Result<PlacementContractDto>.Failure("Equipment quantity must be greater than zero.");
        }

        var productionFacility = await _context.ProductionFacilities
            .FirstOrDefaultAsync(pf => pf.Code == dto.ProductionFacilityCode);

        if (productionFacility == null)
        {
            _logger.LogWarning("Production facility not found: {Code}", dto.ProductionFacilityCode);
            return Result<PlacementContractDto>.Failure("Production facility not found.");
        }

        var equipmentType = await _context.EquipmentTypes
            .FirstOrDefaultAsync(et => et.Code == dto.EquipmentTypeCode);

        if (equipmentType == null)
        {
            _logger.LogWarning("Equipment type not found: {Code}", dto.EquipmentTypeCode);
            return Result<PlacementContractDto>.Failure("Equipment type not found.");
        }

        var usedArea = await _context.PlacementContracts
            .Where(pc => pc.EquipmentTypeCode == productionFacility.Code)
            .SumAsync(pc => pc.EquipmentQuantity * pc.EquipmentType.AreaPerUnit);

        var requiredArea = dto.EquipmentQuantity * equipmentType.AreaPerUnit;

        if (usedArea + requiredArea > productionFacility.StandardArea)
        {
            _logger.LogWarning(
                "Not enough available area in the production facility: {FacilityName}. Needed: {RequiredArea}, Available: {AvailableArea}",
                productionFacility.Name, requiredArea, productionFacility.StandardArea - usedArea);
            return Result<PlacementContractDto>.Failure("Not enough available area in the production facility.");
        }

        var placementContract = PlacementContract.CreatePlacementContract(
            productionFacility.Code,
            equipmentType.Code,
            dto.EquipmentQuantity
        );

        _context.PlacementContracts.Add(placementContract);
        await _context.SaveChangesAsync();

        var resultDto = new PlacementContractDto(
            productionFacility.Name,
            equipmentType.Name,
            placementContract.EquipmentQuantity
        );

        _logger.LogInformation("Placement contract created successfully: {ContractId}", placementContract.Id);

        return Result<PlacementContractDto>.Success(resultDto);
    }

    public async Task<Result<List<PlacementContractDto>>> GetPlacementContractsAsync()
    {
        _logger.LogInformation("Fetching the list of placement contracts.");

        var contracts = await _context.PlacementContracts
            .Include(pc => pc.ProductionFacility)
            .Include(pc => pc.EquipmentType)
            .ToListAsync();

        var result = contracts.Select(pc => new PlacementContractDto(
            pc.ProductionFacility.Name,
            pc.EquipmentType.Name,
            pc.EquipmentQuantity)
        ).ToList();

        _logger.LogInformation("Fetched {Count} placement contracts.", result.Count);

        return Result<List<PlacementContractDto>>.Success(result);
    }
}