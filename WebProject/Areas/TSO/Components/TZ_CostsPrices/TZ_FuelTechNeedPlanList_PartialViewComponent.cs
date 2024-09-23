using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_FuelTechNeedPlanList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_FuelTechNeedPlanList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int tz_id, int userId)
		{
			var tz_data = new TZFuelTechNeedPlanDataViewModel();
			tz_data.TZFuelTechNeedPlanListViewModel = await _context.TZFuelTechNeedPlanListViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOneFuelTechNeedPlanList {data_status},{perspective_year},{tz_id},{userId}").ToListAsync();
			tz_data.tz_id = tz_id;
			tz_data.data_status = data_status;
			tz_data.perspective_year = perspective_year;
			ViewBag.FuelTypesList = _context.Dict_FuelTypes.ToList();
			ViewBag.PeriodsList = _context.Dict_Periods.Where(x => (x.Id != 3 && data_status == perspective_year) || (x.Id == 3 && data_status < perspective_year)).ToList();
			return View("TZ_FuelTechNeedPlanList_Partial", tz_data);
		}
	}
}
