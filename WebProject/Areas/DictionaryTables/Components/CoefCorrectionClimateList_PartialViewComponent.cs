using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components
{
	public class CoefCorrectionClimateList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public CoefCorrectionClimateList_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int userId)
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}
			if (perspective_year == 0)
			{
				perspective_year = _m_c.GetCurrentYearByDS(data_status);
			}

			var coefCorr = new CoefCorrectionClimateMainViewModel();

			coefCorr.coefCorrectionPerspective = await _context.CoefCorrectionClimatePerspectiveViewModels.FromSqlInterpolated($"exec [dictionary].[sp_GetCoefCorrectionСlimatePerspectiveList] {data_status}").ToListAsync();
			coefCorr.coefCorrection = await _context.CoefCorrectionClimateViewModels.FromSqlInterpolated($"exec [dictionary].[sp_GetCoefCorrectionСlimateList]").ToListAsync();
			ViewBag.PerspectiveYears = await _context.PerspectiveYears.Where(x => x.data_status == data_status).Select(x => new { x.perspective_year }).ToListAsync();

			return View("CoefCorrectionClimateList_Partial", coefCorr);
		}
	}
}
