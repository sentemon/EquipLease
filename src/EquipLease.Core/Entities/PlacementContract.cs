namespace EquipLease.Core.Entities;

public class PlacementContract
{
    public Guid Id { get; private set; }
    public Guid ProductionFacilityId { get; private set; }
    public Guid EquipmentTypeId { get; private set; }
    public int EquipmentQuantity { get; private set; }
    public ProductionFacility ProductionFacility { get; private set; }
    public EquipmentType EquipmentType { get; private set; }
    
    private PlacementContract() { }

    private PlacementContract(Guid productionFacilityId, Guid equipmentTypeId, int equipmentQuantity)
    {
        if (productionFacilityId == Guid.Empty)
        {
            throw new ArgumentException("Production facility Id cannot be empty.", nameof(productionFacilityId));
        }

        if (equipmentTypeId == Guid.Empty)
        {
            throw new ArgumentException("Equipment type ID cannot be empty.", nameof(equipmentTypeId));
        }

        if (equipmentQuantity <= 0)
        {
            throw new ArgumentException("Equipment quantity must be greater than zero.", nameof(equipmentQuantity));
        }

        ProductionFacilityId = productionFacilityId;
        EquipmentTypeId = equipmentTypeId;
        EquipmentQuantity = equipmentQuantity;
    }

    public static PlacementContract CreatePlacementContract(Guid productionFacilityId, Guid equipmentTypeId, int equipmentQuantity)
    {
        return new PlacementContract(productionFacilityId, equipmentTypeId, equipmentQuantity);
    }
}