using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_InputEnergyDataList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_InputEnergyDataList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int tz_id, int data_status, int userId)
		{
			List<TZInOutEnergyPlanListViewModel> tz_in = await _context.TZInOutEnergyPlanListViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZInputEnergyPerspectiveList {data_status},{tz_id},{userId}").ToListAsync();
			return View("TZ_InputEnergyDataList_Partial", tz_in);
		}
	}
}
