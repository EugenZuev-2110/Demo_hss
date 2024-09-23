using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TZ_OutputTransferEnergySourcesListData_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;

        public TZ_OutputTransferEnergySourcesListData_PartialViewComponent(HssDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int tz_id, int data_status, int perspective_year, int userId)
        {
			List<TZOutputTransferEnergySourcesListDataViewModel> tz_in = await _context.TZOutputTransferEnergySourcesListDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOutputTransferEnergySourcesList {data_status},{perspective_year},{tz_id},{userId}").ToListAsync();
			return View("TZ_OutputTransferEnergySourcesListData_Partial", tz_in);
        }
    }
}
