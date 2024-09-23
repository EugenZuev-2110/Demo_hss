using DataBaseHSS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.TerritorialDivisionPopulation_Partial
{
	public class TerritorialDivisionPopulation_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public TerritorialDivisionPopulation_PartialViewComponent(HssDbContext context, HSSController m_c)
		{
			_context = context;
			_m_c = m_c;
	}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int distr_id, int userId)
		{
			var terrDivisionPopulation = new TerritorialDivisionPopulationViewModel();
			terrDivisionPopulation.Id = distr_id;
			terrDivisionPopulation.data_status = data_status;
			
				terrDivisionPopulation.TerritorialDivisionPopulationList = await _context.TerritorialDivisionPopulationDataOneViewModels.FromSqlInterpolated($"exec dictionary.GetTerritorialDivisionPopulationDataOne {distr_id},{data_status},{userId}").ToListAsync();
			
			if(distr_id == 0 || terrDivisionPopulation.TerritorialDivisionPopulationList.Count == 0)
			{
				var p_years =  _m_c.GetPerspectiveYearsList(data_status);
				var TerritorialDivisionPopulationList = new List<TerritorialDivisionPopulationListViewModel>();
				for(int i = 0;  i < p_years.Count ; i++)
				{
					TerritorialDivisionPopulationList.Add(new TerritorialDivisionPopulationListViewModel() { perspective_year = p_years[i].perspective_year, populate_size = null });
				}
				terrDivisionPopulation.TerritorialDivisionPopulationList = TerritorialDivisionPopulationList;
			}
			return View("TerritorialDivisionPopulation_Partial", terrDivisionPopulation);
		}
	}
}
