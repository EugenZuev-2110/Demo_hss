using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_ExpensesCAData_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_ExpensesCAData_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int tz_id, int userId)
		{
			TZExpenses_IPHS_CA_ViewModel tz_data = (await _context.TZExpenses_IPHS_CA_ViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZExpensesIpHsCa_DataOne {data_status},{tz_id},{2},{userId}").ToListAsync()).FirstOrDefault();
			return View("TZ_ExpensesCAData_Partial", tz_data ?? new TZExpenses_IPHS_CA_ViewModel());
		}
	}
}
