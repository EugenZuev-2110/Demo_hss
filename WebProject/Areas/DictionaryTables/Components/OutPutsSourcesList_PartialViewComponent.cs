using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components
{
	public class OutPutsSourcesList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public OutPutsSourcesList_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}

		public async Task<IViewComponentResult> InvokeAsync(int data_status, int userId)
		{
			if (data_status == 0)
			{
					data_status = _m_c.GetCurrentDS();
			}
			var output = await _context.OutPutsSourcesListViewModels.FromSqlInterpolated($"exec sources.sp_GetOutPutsSourcesList {data_status}").ToListAsync();

			return View("OutPutsSourcesList_Partial", output);
		}
	}
}
