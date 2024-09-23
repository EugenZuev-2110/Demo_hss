using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.StandardConsumptionHeat
{
	public class CoefHeatventHeatLoad_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public CoefHeatventHeatLoad_PartialViewComponent(HssDbContext context, HSSController c)
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

			var CoefHeatventHeatLoad = await _context.CoefRelationHeatVentLoadViewModels.FromSqlInterpolated($"exec consumers.sp_GetCoefRelationHeatVentLoadList {data_status}").ToListAsync();

			return View("CoefHeatventHeatLoad_Partial", CoefHeatventHeatLoad);
		}
	}
}
