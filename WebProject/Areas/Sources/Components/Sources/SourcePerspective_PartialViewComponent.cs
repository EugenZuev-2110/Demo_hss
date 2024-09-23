using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Areas.Sources.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.Sources.Components.Sources
{
	public class SourcePerspective_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public SourcePerspective_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status = 0, int source_id = 0, string action_for = "")
		{
				if (data_status == 0)
				{
					data_status = _m_c.GetCurrentDS();
				}

				var source_p = (await _context.SourcePerspectiveViewModels.FromSqlInterpolated($"exec [sources].[sp_GetSourceImplementSchemeParam] {data_status}, {source_id}").ToListAsync())
				?? new List<SourcePerspectiveViewModel>();
            
				ViewBag.SourceStatus = await _context.Dict_SourceStatuses.ToListAsync();
				ViewBag.HSSList = await _context.HeatSupplySystems.Select(x => new { x.hss_id, x.unom_hss }).ToListAsync();
				ViewBag.OrgList = await _context.fnt_GetOrgOwnerInnListByChars("", data_status).ToListAsync();
				ViewBag.District = await _context.fnt_GetDistrictRegionList().ToListAsync();
				ViewBag.TSOList = await _context.fnt_GetOrgList(data_status, 1).ToListAsync();
				source_p[0].source_id = source_id;
                return View("SourcePerspective_Partial", source_p);
		}
	}
}
