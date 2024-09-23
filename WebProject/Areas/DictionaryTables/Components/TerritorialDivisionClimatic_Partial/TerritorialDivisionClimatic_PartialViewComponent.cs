using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.TerritorialDivisionClimatic_Partial
{
	public class TerritorialDivisionClimatic_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TerritorialDivisionClimatic_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int distr_id, int userId, string action_for = "")
		{
			var terrDivisionClimatic = (await _context.TerritorialDivisionClimaticDataOneViewModels.FromSqlInterpolated($"exec dictionary.GetTerritorialDivisionClimaticDataOne {distr_id},{data_status},{userId}")
				.ToListAsync()).FirstOrDefault() ?? new TerritorialDivisionClimaticDataOneViewModel();
			terrDivisionClimatic.Id = distr_id;
			terrDivisionClimatic.data_status = data_status;
			
			ViewBag.Months = _context.Months.ToList();
			ViewBag.Action_for = action_for;
			return View("TerritorialDivisionClimatic_Partial", terrDivisionClimatic);
		}
	}
}
