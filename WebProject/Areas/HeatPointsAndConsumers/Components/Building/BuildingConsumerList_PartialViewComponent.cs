using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.HeatPointsAndConsumers.Components.Building
{
    public class BuildingConsumerList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public BuildingConsumerList_PartialViewComponent(HssDbContext context, HSSController c)
        {
            _context = context;
            _m_c = c;
        }

        public async Task<IViewComponentResult> InvokeAsync(int building_id)
        {
            int data_status = _m_c.GetCurrentDS();

            List<BuildingConsumerViewModel> build = await _context.BuildingConsumerViewModels.FromSqlInterpolated
                ($"exec consumers.sp_GetBuildingConsumerList {data_status}, {building_id}").ToListAsync() ?? new List<BuildingConsumerViewModel>();
           
                ViewBag.build = building_id.ToString();
            
                return View("BuildingConsumerList_Partial", build);
        }
    }
}
