namespace EquipLease.Infrastructure.DTOs;

public record PlacementContractDto(
    string ProductionFacilityName,
    string EquipmentTypeName,
    int EquipmentQuantity
);