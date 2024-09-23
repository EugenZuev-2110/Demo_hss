using DataBaseHSS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.TSO.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.HeatPointsAndConsumers.Components.HeatPointsComponents
{
	public class HP_AddRemoveGenInfo_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public HP_AddRemoveGenInfo_PartialViewComponent(HssDbContext context, HSSController m_c)
		{
			_context = context;
			_m_c = m_c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int heat_point_id)
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}

			var data = (await _context.HPAddRemoveGenInfoDataViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHpAddRemoveGenInfoDataOne {data_status},{heat_point_id}")
				.ToListAsync()).FirstOrDefault() ?? new HPAddRemoveGenInfoDataViewModel { add_remove_gen_info_hp_type_id = 1};

			ViewBag.OrgOwnerInn = await _context.fnt_GetOrgOwnerInnListByChars("", data_status).ToListAsync();
			ViewBag.HpLocations = await _context.fnt_GetHpLocations().ToListAsync();
			ViewBag.HpTypeLocations = await _context.fnt_GetHpTypeLocations().ToListAsync();
			ViewBag.HpDistricts = await _context.fnt_GetHpDistricts().ToListAsync();
			ViewBag.TariffZones = await _context.fnt_GetTZUnomList(data_status).ToListAsync();

			return View("HP_AddRemoveGenInfo_Partial", data);
		}
	}
}
