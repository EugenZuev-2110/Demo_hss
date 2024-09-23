using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_HPStandardizedRatesDataList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public TZ_HPStandardizedRatesDataList_PartialViewComponent(HssDbContext context, HSSController m_c)
		{
			_context = context;
			_m_c = m_c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int userId)
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}

			List<StandardizedRatesTable> tz = await _context.StandardizedRatesTable.FromSqlInterpolated($"exec tarif_zone.sp_GetTZStandardizedRatesDataList {data_status},{userId}").ToListAsync();
			return View("TZ_HPStandardizedRatesDataList_Partial", tz);
			//return View("TZ_HPStandardizedRatesDataList_Partial");
		}
	}
}
