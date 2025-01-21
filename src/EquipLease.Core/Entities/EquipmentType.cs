namespace EquipLease.Core.Entities;

public class EquipmentType
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public int AreaPerUnit { get; private set; }

    private EquipmentType(string code, string name, int areaPerUnit)
    {
        Code = code;
        Name = name;
        AreaPerUnit = areaPerUnit;
    }

    public static EquipmentType CreateEquipmentType(string code, string name, int areaPerUnit)
    {
        return new EquipmentType(code, name, areaPerUnit);
    }
}