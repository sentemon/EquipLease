namespace EquipLease.Core.Entities;

public class PlacementContract
{
    public Guid Id { get; private set; }
    public string ProductionFacilityCode { get; private set; }
    public string EquipmentTypeCode { get; private set; }
    public int EquipmentQuantity { get; private set; }
    public ProductionFacility ProductionFacility { get; private set; }
    public EquipmentType EquipmentType { get; private set; }

#pragma warning disable CS8618 // Empty constructor for EF Core
    private PlacementContract() { }
#pragma warning disable CS8618

    private PlacementContract(string productionFacilityCode, string equipmentTypeCode, int equipmentQuantity)
    {
        if (string.IsNullOrEmpty(productionFacilityCode))
        {
            throw new ArgumentException("Production facility code cannot be empty.", nameof(productionFacilityCode));
        }

        if (string.IsNullOrEmpty(equipmentTypeCode))
        {
            throw new ArgumentException("Equipment type code cannot be empty.", nameof(equipmentTypeCode));
        }

        if (equipmentQuantity <= 0)
        {
            throw new ArgumentException("Equipment quantity must be greater than zero.", nameof(equipmentQuantity));
        }

        Id = Guid.NewGuid();
        ProductionFacilityCode = productionFacilityCode;
        EquipmentTypeCode = equipmentTypeCode;
        EquipmentQuantity = equipmentQuantity;
    }

    public static PlacementContract CreatePlacementContract(string productionFacilityCode, string equipmentTypeCode, int equipmentQuantity)
    {
        return new PlacementContract(productionFacilityCode, equipmentTypeCode, equipmentQuantity);
    }
}