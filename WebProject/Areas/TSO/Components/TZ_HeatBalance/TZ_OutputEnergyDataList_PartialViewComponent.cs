using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TZ_OutputEnergyDataList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;

        public TZ_OutputEnergyDataList_PartialViewComponent(HssDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int tz_id, int data_status, int userId)
        {
			List<TZInOutEnergyPlanListViewModel> tz_in = await _context.TZInOutEnergyPlanListViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOutputEnergyPerspectiveList {data_status},{tz_id},{userId}").ToListAsync();
			return View("TZ_OutputEnergyDataList_Partial", tz_in);
        }
    }
}
