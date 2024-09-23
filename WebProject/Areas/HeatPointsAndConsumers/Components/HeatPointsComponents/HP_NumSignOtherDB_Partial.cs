using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;

namespace WebProject.Components
{
    public class HP_NumSignOtherDB_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HP_NumSignOtherDB_Partial(HssDbContext context, HSSController m_c)
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

			if (heat_point_id == 0)
				ViewBag.IsDisabled = "disabled";
			else
				ViewBag.IsDisabled = String.Empty;

			var item = (await _context.HPAddRemove_NumSignOtherDbViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHpAddRemoveNumSignOtherDbDataOne {data_status},{heat_point_id}").ToListAsync()).FirstOrDefault()
                ?? new HPAddRemove_NumSignOtherDbViewModel { data_status = data_status, heat_point_id = heat_point_id};
			return View("HP_NumSignOtherDB_Partial", item);
		}
    }
}
