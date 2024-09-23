using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components
{
	public class ActiveZoneETOList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public ActiveZoneETOList_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var activeZoneETO = await _context.ActiveZoneETOViewModels.FromSqlInterpolated($"exec tso.sp_GetETOZoneList").ToListAsync();

			return View("ActiveZoneETOList_Partial", activeZoneETO);
		}
	}
}
