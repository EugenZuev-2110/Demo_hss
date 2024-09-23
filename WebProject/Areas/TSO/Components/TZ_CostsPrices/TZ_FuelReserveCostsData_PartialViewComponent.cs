using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_FuelReserveCostsData_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_FuelReserveCostsData_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int tz_id, int userId)
		{
			var tz_data = new TZFuelReserveCostsDataViewModel();

			List<TZFuelReserveCostsDataViewModel> tz_l = await _context.TZFuelReserveCostsDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZFuelReserveCostsDataOne {data_status},{perspective_year},{tz_id},{userId}").ToListAsync();
			if (tz_l.Count > 0)
				tz_data = tz_l[0];

			return View("TZ_FuelReserveCostsData_Partial", tz_data);
		}
	}
}
