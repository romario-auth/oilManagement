using Microsoft.AspNetCore.Mvc.Rendering;
using dto = OilManagementMvc.DtoReturn;

namespace OilManagementMvc.DtoReturn;

public class RecyclesViews
{
    public List<dto.SelectListItemCollectPoint> CollectPoints;
    public SelectListItem[]? TypeRecycle;
    public SelectListItem[]? UnitTypes;

    public RecyclesViews() { }
}
