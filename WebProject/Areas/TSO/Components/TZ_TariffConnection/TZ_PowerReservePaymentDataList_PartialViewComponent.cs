using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_PowerReservePaymentDataList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public TZ_PowerReservePaymentDataList_PartialViewComponent(HssDbContext context, HSSController m_c)
		{
			_context = context;
			_m_c = m_c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int tso_id)
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}

			List<PowerReservePaymentTable> tz = await _context.PowerReservePaymentTable.FromSqlInterpolated($"exec tarif_zone.sp_GetTZPowerReservePaymentDataList {data_status},{tso_id}").ToListAsync();
			return View("TZ_PowerReservePaymentDataList_Partial", tz);
			//return View("TZ_PowerReservePaymentDataList_Partial");
		}
	}
}
