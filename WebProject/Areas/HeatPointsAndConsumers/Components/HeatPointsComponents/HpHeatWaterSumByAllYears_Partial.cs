using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;

namespace WebProject.Components
{
    public class HpHeatWaterSumByAllYears_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HpHeatWaterSumByAllYears_Partial(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int heat_point_id, int data_status)
        {
            if (data_status == 0)
            {
                data_status = _m_c.GetCurrentDS();
            }

			List<HP_NameListUnit> list = await _context.HP_NameListUnit.FromSqlInterpolated
                ($"exec heat_points.sp_GetHpHeatWaterSumByAllYears {heat_point_id},{data_status}").ToListAsync();
			return View("HpHeatWaterSumByAllYears_Partial", list);
		}
    }
}
