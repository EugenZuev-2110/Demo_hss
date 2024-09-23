using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.Sources.Components.SourcesEquipments
{
	public class SourcesEquipTurbineList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public SourcesEquipTurbineList_PartialViewComponent(HssDbContext context, HSSController c)
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
            
			var sourceEquip = await _context.SourcesEquipTurbineViewModels.FromSqlInterpolated($"exec sources.sp_GetSourcesEquipTurbineList {data_status}, {perspective_year}, {tz}, {status}, {org}, {type} ").ToListAsync();


            return View("SourcesEquipTurbineList_Partial", sourceEquip);
		}
	}
}
