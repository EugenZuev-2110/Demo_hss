using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_CapitalInvestmentData_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_CapitalInvestmentData_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int tz_id, int userId)
		{
			TZCapitalInvestmentViewModel tz_data = (await _context.TZCapitalInvestmentViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZFinanceCapInvNVVDataOne {data_status},{perspective_year},{tz_id},{userId}").ToListAsync()).FirstOrDefault();
			return View("TZ_CapitalInvestmentData_Partial", tz_data ?? new TZCapitalInvestmentViewModel());
		}
	}
}
