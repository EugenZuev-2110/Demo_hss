using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_ExpensesAddValuesData_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_ExpensesAddValuesData_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int tz_id, int userId)
		{
			TZExpensesAddValues_ViewModel tz_data = (await _context.TZExpensesAddValues_ViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZExpensesAddValues_DataOne {data_status},{tz_id},{userId}").ToListAsync()).FirstOrDefault();
			return View("TZ_ExpensesAddValuesData_Partial", tz_data ?? new TZExpensesAddValues_ViewModel());
		}
	}
}
