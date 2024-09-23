using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;

namespace WebProject.Components
{
    public class HP_HeatPointSwitchDataList_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HP_HeatPointSwitchDataList_Partial(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(string heat_point_id_list, int data_status, int perspective_year_before, int perspective_year_after)
        {
            if (data_status == 0)
            {
                data_status = _m_c.GetCurrentDS();
            }
            if (perspective_year_before == 0)
            {
				perspective_year_before = _m_c.GetCurrentYearByDS(data_status);
            }
			if (perspective_year_after == 0)
			{
				perspective_year_after = _m_c.GetCurrentYearByDS(data_status);
			}

			List<HP_SwitchableUnit> list = await _context.HP_SwitchableUnit.FromSqlInterpolated
                ($"exec heat_points.sp_GetListSwitchHPTable {heat_point_id_list},{data_status},{perspective_year_before},{perspective_year_after}").ToListAsync();
			return View("HP_HeatPointSwitchDataList_Partial", list);
		}
    }
}
