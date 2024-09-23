using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TZ_OutputTransferEnergyDataList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;

        public TZ_OutputTransferEnergyDataList_PartialViewComponent(HssDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int tz_id, int data_status, int userId)
        {
			List<TZOutputTransferEnergyPlanListViewModel> tz_in = await _context.TZOutputTransferEnergyPlanListViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOutputTransferEnergyPerspectiveList {data_status},{tz_id},{userId}").ToListAsync();
			return View("TZ_OutputTransferEnergyDataList_Partial", tz_in);
        }
    }
}
