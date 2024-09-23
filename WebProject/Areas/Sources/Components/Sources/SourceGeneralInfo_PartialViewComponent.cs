using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.Sources.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.Sources.Components.Sources
{
	public class SourceGeneralInfo_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public SourceGeneralInfo_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}

		public async Task<IViewComponentResult> InvokeAsync(int data_status = 0, int source_id = 0 , string action_for = "")
		{
			try
			{
				if (data_status == 0)
				{
					data_status = _m_c.GetCurrentDS();
				}

				var source = (await _context.SourceGeneralInfoViewModels.FromSqlInterpolated($"exec [sources].[sp_GetSourceGeneralInfo] {data_status}, {source_id}").ToListAsync()).FirstOrDefault()
				?? new SourceGeneralInfoViewModel();

				source.data_status = data_status;
				ViewBag.SourceType = await _context.fnt_GetSourcesTypeList().ToListAsync();
				ViewBag.TZ = await _context.fnt_GetTZUnomList(data_status).ToListAsync();
				ViewBag.OrgList = await _context.fnt_GetOrgOwnerInnListByChars("", data_status).ToListAsync();
				ViewBag.District = await _context.fnt_GetDistrictRegionList().ToListAsync();
				return View("SourceGeneralInfo_Partial", source);
			}
			catch (Exception ex) { return View("SourceGeneralInfo_Partial"); }
			
		}
	}
}
