using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_MethodIndexationNVVData_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_MethodIndexationNVVData_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int tz_id, int userId)
		{
			TZMethodIndexationViewModel tz_data = (await _context.TZMethodIndexationViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZMethodIndexationNVVDataOne {data_status},{perspective_year},{tz_id},{userId}").ToListAsync()).FirstOrDefault();
			return View("TZ_MethodIndexationNVVData_Partial", tz_data ?? new TZMethodIndexationViewModel());
		}
	}
}
