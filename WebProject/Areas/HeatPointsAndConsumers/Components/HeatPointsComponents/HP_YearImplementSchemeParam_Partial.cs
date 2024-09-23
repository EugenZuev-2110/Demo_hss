using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;

namespace WebProject.Components
{
    public class HP_YearImplementSchemeParam_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HP_YearImplementSchemeParam_Partial(HssDbContext context, HSSController m_c)
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

			var list = await _context.HPAddRemoveYearImplementSchemeParamDataViewModel.FromSqlInterpolated
                ($"exec heat_points.sp_GetHpYearImplementSchemeParamDataOne {data_status},{heat_point_id}").ToListAsync();

            if (heat_point_id == 0)
                ViewBag.IsDisabled = "disabled";
            else
                ViewBag.IsDisabled = String.Empty;

			ViewBag.HpTsoList = await _context.fnt_GetTSOName(data_status).ToListAsync();
			ViewBag.HpStatusList = await _context.fnt_GetHpStatusList().ToListAsync();
			ViewBag.HpUnomSourceOutputList = await _context.fnt_GetUnomSourceOutputList(data_status).ToListAsync();
			ViewBag.HpTempHtPlanList = await _context.fnt_GetHpTempHtPlanList().ToListAsync();
			ViewBag.HpTempHtTypeSchemeList = await _context.fnt_GetHpTempHtTypeSchemeList().ToListAsync();
			ViewBag.HpConnectTypesTechList = await _context.fnt_GetHpConnectTypesTechList().ToListAsync();
			ViewBag.HpTempTechPlanList = await _context.fnt_GetHpTempHtPlanList().ToListAsync();
			ViewBag.HpConnectTypesHeatList = await _context.fnt_GetHpConnectTypesHeatList().ToListAsync();
			ViewBag.HpTempHeatPlanList = await _context.fnt_GetHpTempHtPlanList().ToListAsync();
			ViewBag.HpConnectTypesVentList = await _context.fnt_GetHpConnectTypesVentList().ToListAsync();
			ViewBag.HpTempVentPlanList = await _context.fnt_GetHpTempHtPlanList().ToListAsync();
			ViewBag.HpConnectTypesHWList = await _context.fnt_GetHpConnectTypesHWList().ToListAsync();
			ViewBag.HpTempHWPlanList = await _context.fnt_GetHpTempHtPlanList().ToListAsync();

			return View("HP_YearImplementSchemeParam_Partial", list);
		}
    }
}
