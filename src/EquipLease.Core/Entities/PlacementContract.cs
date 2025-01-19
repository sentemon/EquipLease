namespace EquipLease.Core.Entities;

public class PlacementContract
{
    public Guid Id { get; private set; }
    public Guid ProductionFacilityId { get; private set; }
    public Guid EquipmentTypeId { get; private set; }
    public int EquipmentQuantity { get; private set; }

    private PlacementContract(Guid productionFacilityId, Guid equipmentTypeId, int equipmentQuantity)
    {
        ProductionFacilityId = productionFacilityId;
        EquipmentTypeId = equipmentTypeId;
        EquipmentQuantity = equipmentQuantity;
    }

    public static PlacementContract CreatePlacementContract(Guid productionFacilityId, Guid equipmentTypeId, int equipmentQuantity)
    {
        return new PlacementContract(productionFacilityId, equipmentTypeId, equipmentQuantity);
    }
}