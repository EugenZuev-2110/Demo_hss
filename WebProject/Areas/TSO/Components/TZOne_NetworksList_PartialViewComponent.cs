using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TZOne_NetworksList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;

        public TZOne_NetworksList_PartialViewComponent(HssDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int tz_id, int data_status, int perspective_year, int userId)
        {
			List<TZOneNetworksDataListViewModel> tz = await _context.TZOneNetworksDataListViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOneNetworksDataList {tz_id},{data_status},{perspective_year},{userId}").ToListAsync();
			return View("TZOne_NetworksList_Partial", tz);
        }
    }
}
