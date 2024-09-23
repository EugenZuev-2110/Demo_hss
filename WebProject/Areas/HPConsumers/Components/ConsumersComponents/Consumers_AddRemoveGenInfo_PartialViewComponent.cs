using DataBaseHSS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Areas.TSO.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.HeatPointsAndConsumers.Components.HeatPointsComponents
{
	public class Consumers_AddRemoveGenInfo_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public Consumers_AddRemoveGenInfo_PartialViewComponent(HssDbContext context, HSSController m_c)
		{
			_context = context;
			_m_c = m_c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int consumer_id)
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}

			var data = (await _context.ConsumerAddRemoveGenInfoDataViewModel.FromSqlInterpolated($"exec consumers.sp_GetConsumerAddRemoveGenInfoDataOne {data_status},{consumer_id}")
				.ToListAsync()).FirstOrDefault() ?? new ConsumerAddRemoveGenInfoDataViewModel();

			ViewBag.OrgOwnerInn = await _context.fnt_GetOrgOwnerInnListByChars("", data_status).ToListAsync();
			ViewBag.Buildings = await _context.Buildings.ToListAsync();

			return View("Consumers_AddRemoveGenInfo_Partial", data);
		}
	}
}
