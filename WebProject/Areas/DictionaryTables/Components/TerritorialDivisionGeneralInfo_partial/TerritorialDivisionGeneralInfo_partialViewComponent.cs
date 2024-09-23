using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Components.TerritorialDivisionGeneralInfo_partial
{
	public class TerritorialDivisionGeneralInfo_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TerritorialDivisionGeneralInfo_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int distr_id, int userId, string action_for = "")
		{
			var terrDivision_h = (await _context.TerritorialDivisionGeneralInfoDataOneViewModels.FromSqlInterpolated($"exec dictionary.GetTerritorialDivisionGeneralInfoDataOne {distr_id},{data_status},{userId}")
				.ToListAsync()).FirstOrDefault() ?? new TerritorialDivisionGeneralInfoDataOneViewModel();

			terrDivision_h.data_status = data_status;

			ViewBag.RegionList = _context.fnt_GetRegionsList().ToList();
			if(action_for == "copy")
				terrDivision_h.Id = 0;
			return View("TerritorialDivisionGeneralInfo_Partial", terrDivision_h);
		}
	}
}
