using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;

namespace WebProject.Components
{
    public class HeatPointList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HeatPointList_PartialViewComponent(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
        {
            if (data_status == 0)
            {
                data_status = _m_c.GetCurrentDS();
            }
            if (perspective_year == 0)
            {
                perspective_year = _m_c.GetCurrentYearByDS(data_status);
            }

            List<HP_MainListUnit> tz = await _context.HP_MainListUnit.FromSqlInterpolated
                ($"exec heat_points.sp_GetHeatPointsDataList {data_status},{perspective_year},{hp_type_id},{hp_status_id},{source_id},{tso_id}").ToListAsync();

			return View("HeatPointList_Partial", tz ?? new List<HP_MainListUnit>());
		}
    }
}
