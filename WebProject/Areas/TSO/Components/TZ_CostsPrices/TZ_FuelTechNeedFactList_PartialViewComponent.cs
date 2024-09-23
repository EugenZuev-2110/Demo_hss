using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_FuelTechNeedFactList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_FuelTechNeedFactList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int tz_id, int data_status, int userId)
		{
			var tz_data = new TZFuelTechNeedFactDataViewModel();
			tz_data.TZFuelTechNeedFactListViewModel = await _context.TZFuelTechNeedFactListViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOneFuelTechNeedFactList {data_status},{tz_id},{userId}").ToListAsync();
			tz_data.tz_id = tz_id;
			tz_data.data_status = data_status;

			ViewBag.FuelTypesList = _context.Dict_FuelTypes.ToList();
			ViewBag.PeriodsList = _context.Dict_Periods.Where(x => x.Id != 3).ToList();
			return View("TZ_FuelTechNeedFactList_Partial", tz_data);
		}
	}
}
