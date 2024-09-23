using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TZ_OutputEnergySourcesListData_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;

        public TZ_OutputEnergySourcesListData_PartialViewComponent(HssDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int tz_id, int data_status, int perspective_year, int userId)
        {
			List<TZOutputEnergySourcesListDataViewModel> tz_in = await _context.TZOutputEnergySourcesListDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOutputEnergySourcesList {data_status},{perspective_year},{tz_id},{userId}").ToListAsync();
			return View("TZ_OutputEnergySourcesListData_Partial", tz_in);
        }
    }
}
