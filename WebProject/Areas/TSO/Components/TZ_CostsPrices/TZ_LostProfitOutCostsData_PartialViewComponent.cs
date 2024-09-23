using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_LostProfitOutCostsData_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_LostProfitOutCostsData_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int tz_id, int userId)
		{
			TZLostProfitOutCostsViewModel tz_data = (await _context.TZLostProfitOutCostsViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZLostRevenueNVVDataOne {data_status},{perspective_year},{tz_id},{userId}").ToListAsync()).FirstOrDefault();
			return View("TZ_LostProfitOutCostsData_Partial", tz_data ?? new TZLostProfitOutCostsViewModel());
		}
	}
}
