using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.StandardConsumptionHeat
{
	public class BuildingCharacteristics_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public BuildingCharacteristics_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId, int data_status, int PurporseTypeBuild = 1)
		{

			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}
			var buildCharact  = await _context.BuildingCharacteristicsViewModels.FromSqlInterpolated($"exec consumers.sp_GetBuildingCharacteristicsList {data_status}, {PurporseTypeBuild}").ToListAsync();
			
			return View("BuildingCharacteristics_Partial", buildCharact);
		}
	}
}
