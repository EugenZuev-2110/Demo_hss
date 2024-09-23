using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_InputEnergyPlanList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_InputEnergyPlanList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int tz_id, int userId)
		{
			List<TZInputEnergyPlanViewModel> tz_l = await _context.TZInputEnergyPlanViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZInputEnergyPlanList {data_status},{perspective_year},{tz_id},{userId}").ToListAsync();

			return View("TZ_InputEnergyPlanList_Partial", tz_l);
		}
	}
}
