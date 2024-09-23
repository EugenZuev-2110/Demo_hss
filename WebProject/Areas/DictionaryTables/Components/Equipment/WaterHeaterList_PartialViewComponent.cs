using DataBaseHSS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class WaterHeaterList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public WaterHeaterList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var WaterHeater = await _context.WaterHeaterViewmodels.FromSqlInterpolated($"exec [dictionary].[sp_GetWaterHeater]").ToListAsync();

			return View("WaterHeaterList_Partial", WaterHeater);
		}
	}
}
