using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.Sources.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.Sources.Components.Sources
{
    public class SourceList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public SourceList_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}

		public async Task<IViewComponentResult> InvokeAsync(int userId, int data_status = 0, int perspective_year = 0, int tz = 0, int status = 0, int org = 0, int type = 0)
        {
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}
			if (perspective_year == 0)
			{
				perspective_year = _m_c.GetCurrentYearByDS(data_status);
			}
			try
			{
				var Source = await _context.SourcesViewModels.FromSqlInterpolated($"exec sources.sp_GetSourcesList {data_status}, {perspective_year}, {tz}, {status}, {org}, {type} ").ToListAsync();
				return View("SourceList_Partial", Source);
			}
			catch (Exception ex) { }
            return View("SourceList_Partial");
        }
    }
}
