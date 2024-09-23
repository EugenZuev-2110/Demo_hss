using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.StandardConsumptionHeat
{
	public class CoefCalculation_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public CoefCalculation_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId, int data_status)
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}

			var CoefCalculation = (await _context.CalcCoefViewModels.FromSqlInterpolated($"exec consumers.sp_GetCalcCoefList {data_status}")
				.ToListAsync()).FirstOrDefault() ?? new CalcCoefViewModel();

			return View("CoefCalculation_Partial", CoefCalculation);
		}
	}
}
