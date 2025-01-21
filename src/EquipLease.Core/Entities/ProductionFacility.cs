namespace EquipLease.Core.Entities;

public class ProductionFacility
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public int StandardArea { get; private set; }

    private ProductionFacility(string code, string name, int standardArea)
    {
        Code = code;
        Name = name;
        StandardArea = standardArea;
    }

    public static ProductionFacility CreateProductionFacility(string code, string name, int standardArea)
    {
        return new ProductionFacility(code, name, standardArea);
    }
}