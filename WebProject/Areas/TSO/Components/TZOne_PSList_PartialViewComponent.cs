using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TZOne_PSList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;

        public TZOne_PSList_PartialViewComponent(HssDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int tz_id, int data_status, int perspective_year, int userId)
        {
			List<TZOnePSDataListViewModel> tz = await _context.TZOnePSDataListViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOnePSDataList {tz_id},{data_status},{perspective_year},{userId}").ToListAsync();
			return View("TZOne_PSList_Partial", tz);
        }
    }
}
