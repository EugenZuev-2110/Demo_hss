using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_TaxesCostsData_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_TaxesCostsData_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int tz_id, int userId)
		{
			TZTaxesCostsViewModel tz_data = _context.TZTaxesCostsViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZTaxesCostsDataOne {data_status},{perspective_year},{tz_id},{userId}").ToList().FirstOrDefault();
			return View("TZ_TaxesCostsData_Partial", tz_data ?? new TZTaxesCostsViewModel());
		}
	}
}
