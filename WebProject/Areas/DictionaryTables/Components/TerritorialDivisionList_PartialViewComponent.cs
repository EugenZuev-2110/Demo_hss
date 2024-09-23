using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.TSO.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components
{
    public class TerritorialDivisionList_PartialViewComponent: ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

		public TerritorialDivisionList_PartialViewComponent(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
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

            var terrDivision = new TerritorialDivisionMainViewModel();

			terrDivision.TerritorialDivision = await _context.TerritorialDivisionViewModels.FromSqlInterpolated($"exec dictionary.GetTerritorialDivisionList {data_status},{userId}").ToListAsync();
			terrDivision.TerritorialDivisionPopulationList = await _context.TerritorialDivisionPopulationDataOneViewModels.FromSqlInterpolated($"exec dictionary.GetTerritorialDivisionPopulationDataList {data_status},{userId}").ToListAsync();
            ViewBag.PerspectiveYears = await _context.PerspectiveYears.Where(x => x.data_status == data_status).Select(x => new { x.perspective_year }).ToListAsync();

			//List<TerritorialDivisionViewModel> terrDivisionList = await _context.TerritorialDivisionViewModels.FromSqlInterpolated($"exec dictionary.GetTerritorialDivisionList {data_status},{userId}").ToListAsync();
			//foreach(var terrDivision in  terrDivisionList)
			//{
			//	terrDivision.TerritorialDivisionPopulationList = await _context.TerritorialDivisionPopulationDataOneViewModels.FromSqlInterpolated($"exec dictionary.GetTerritorialDivisionPopulationDataOne {terrDivision.Id},{data_status},{userId}").ToListAsync();
			//}

			return View("TerritorialDivisionList_Partial", terrDivision);
        }
    }
}
