using OilManagementMvc.Shared;
using System.ComponentModel;

namespace OilManagementMvc.Models;

public class Recycle : Entity
{
    public CollectPoint? CollectPoint { get; set; }
    public Guid CollectPointId { get; set; }
    public TypeRecycle TypeRecycle { get; set; }
    public int Count { get; set; }
    public UnitType UnitType { get; set; }
    public String Colletor { get; set; }
}

public enum UnitType
{
    [Description("Kilogramas")]
    kilograms = 0,

    [Description("Liters")]
    Liters = 1,

    [Description("Tons")]
    Tons = 2
}

public enum TypeRecycle
{
    [Description("Oil Kitchen")]
    OilKitchen = 0
}