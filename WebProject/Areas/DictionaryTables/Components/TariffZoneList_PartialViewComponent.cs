using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components
{
	public class TariffZoneList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public TariffZoneList_PartialViewComponent(HssDbContext context, HSSController m_c)
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

			List<TariffZoneListViewModel> terrDivisionList = await _context.TariffZoneListViewModels.FromSqlInterpolated($"exec tarif_zone.sp_GetTarifZoneList  {data_status},{perspective_year},{userId}").ToListAsync();
			
			return View("TariffZoneList_Partial", terrDivisionList);
		}
	}
}
