using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;

namespace WebProject.Components
{
    public class ConsumersList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public ConsumersList_PartialViewComponent(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int data_status)
        {
            if (data_status == 0)
            {
                data_status = _m_c.GetCurrentDS();
            }

            List<Consumer_MainListUnit> tz = await _context.Consumer_MainListUnit.FromSqlInterpolated
                ($"exec consumers.sp_GetConsumersDataList {data_status}").ToListAsync();

			return View("ConsumersList_Partial", tz ?? new List<Consumer_MainListUnit>());
		}
    }
}
