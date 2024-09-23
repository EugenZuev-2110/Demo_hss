using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_InputEnergyFactList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_InputEnergyFactList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int tz_id, int userId)
		{
			List<TZInputEnergyFactViewModel> tz_l = await _context.TZInputEnergyFactViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZInputEnergyFactList {data_status},{tz_id},{userId}").ToListAsync();

			return View("TZ_InputEnergyFactList_Partial", tz_l);
		}
	}
}
