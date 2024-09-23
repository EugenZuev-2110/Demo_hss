using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TZOne_HPList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;

        public TZOne_HPList_PartialViewComponent(HssDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int tz_id, int data_status, int perspective_year, int userId)
        {
			List<TZOneHPDataListViewModel> tz = await _context.TZOneHPDataListViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOneHPDataList {tz_id},{data_status},{perspective_year},{userId}").ToListAsync();
			return View("TZOne_HPList_Partial", tz);
        }
    }
}
