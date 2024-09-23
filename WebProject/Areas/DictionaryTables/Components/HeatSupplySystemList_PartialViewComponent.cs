using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components
{
	public class HeatSupplySystemList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public HeatSupplySystemList_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
				var hss = await _context.HeatSupplySystemViewModels.FromSqlInterpolated($"exec sources.sp_GetHssList").ToListAsync();
				return View("HeatSupplySystemList_Partial", hss);
		}
	}
}
