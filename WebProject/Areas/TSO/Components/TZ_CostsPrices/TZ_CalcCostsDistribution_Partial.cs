using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_CalcCostsDistribution_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_CalcCostsDistribution_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int tz_id, int userId)
		{
			TZCalcCostsDistributionViewModel tz_data = (await _context.TZCalcCostsDistributionViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZCalcCostsDistributionDataOne {data_status},{perspective_year},{tz_id},{userId}").ToListAsync()).FirstOrDefault();
			return View("TZ_CalcCostsDistribution_Partial", tz_data ?? new TZCalcCostsDistributionViewModel());
		}
	}
}
