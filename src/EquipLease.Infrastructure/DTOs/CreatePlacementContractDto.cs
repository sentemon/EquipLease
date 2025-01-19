namespace EquipLease.Infrastructure.DTOs;

public record CreatePlacementContractDto(
    string ProductionFacilityCode,
    string EquipmentTypeCode,
    int EquipmentQuantity
);