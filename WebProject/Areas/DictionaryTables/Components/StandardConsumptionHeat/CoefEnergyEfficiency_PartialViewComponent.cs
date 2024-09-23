using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.StandardConsumptionHeat
{
	public class CoefEnergyEfficiency_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public CoefEnergyEfficiency_PartialViewComponent(HssDbContext context, HSSController c)
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
			var CoefEnergyEfficienc = await _context.CoefEnergyEfficiencyViewModels.FromSqlInterpolated($"exec consumers.sp_CoefEnergyEfficiencyList {data_status}").ToListAsync();

			return View("CoefEnergyEfficiency_Partial", CoefEnergyEfficienc);
		}
	}
}
