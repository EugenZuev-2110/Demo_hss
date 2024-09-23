using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;

namespace WebProject.Components
{
    public class HP_Automatization_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HP_Automatization_Partial(HssDbContext context, HSSController m_c)
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

			ViewBag.AutomaticTypes = await _context.fnt_GetAutomaticList().ToListAsync();

			var item = (await _context.HPAddRemove_AutomatizationViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHP_AutomatizationList {data_status},{heat_point_id}").ToListAsync()).FirstOrDefault() ?? new HPAddRemove_AutomatizationViewModel();
			return View("HP_Automatization_Partial", item);
		}
    }
}
