using OilManagementMvc.Shared;

namespace OilManagementMvc.Models;

public class CollectPoint : Entity
{
    public String? Address { get; set; }
    public String? City { get; set; }
    public String? State { get; set; }
    public String ZipCode { get; set; }
    public int Number { get; set; }
    public String Name { get; set; }
    public String? OwnPointCollect { get; set; }
}
