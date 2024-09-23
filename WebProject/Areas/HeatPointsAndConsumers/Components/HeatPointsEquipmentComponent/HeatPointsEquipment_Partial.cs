using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using DataBaseHSS.Models;
using System.Linq;

namespace WebProject.Components
{
    public class HeatPointsEquipment_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HeatPointsEquipment_Partial(HssDbContext context, HSSController m_c)
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

            var model = await _context.HeatPointsEquipment
                .FromSqlInterpolated($"exec heat_points.sp_GetHeatPointsEquipmentsDataList {data_status},{perspective_year},{hp_type_id},{hp_status_id},{source_id},{tso_id}")
                .ToListAsync() ?? new List<HeatPointsEquipment>();

			return View("HeatPointsEquipment_Partial", model);
		}
    }
}
