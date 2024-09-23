using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Models;
using WebProject.Areas.HPConsumers.Models;

namespace WebProject.Components
{
    public class Consumers_YearHeatConsumption_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public Consumers_YearHeatConsumption_Partial(HssDbContext context, HSSController m_c)
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

            ViewBag.IsDisabled = consumer_id == 0 ? "disabled" : String.Empty;

            Consumers_YearHeatConsumptionViewModel model = new Consumers_YearHeatConsumptionViewModel();
            model.perspective = await _context.Consumers_YearHeatConsumptionViewModel_Perspective.FromSqlInterpolated
                ($"exec consumers.sp_GetConsumers_YearHeatConsumptionDataOne_Perspective {data_status},{consumer_id}").ToListAsync() ?? new List<Consumers_YearHeatConsumptionViewModel_Perspective>();

            model.fact = (await _context.Consumers_YearHeatConsumptionViewModel_Fact.FromSqlInterpolated
                ($"exec consumers.sp_GetConsumers_YearHeatConsumptionDataOne_Fact {data_status},{consumer_id}").ToListAsync()).FirstOrDefault() ?? new Consumers_YearHeatConsumptionViewModel_Fact ();

            return View("Consumers_YearHeatConsumption_Partial", model);
		}
    }
}
