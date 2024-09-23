using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;

namespace WebProject.Components
{
    public class HP_EquipmentPart_Pump_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HP_EquipmentPart_Pump_Partial(HssDbContext context, HSSController m_c)
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

            ViewBag.PumpTypes = await _context.Dict_PumpTypes.ToListAsync();
			ViewBag.PumpMarks = await _context.fnt_GetPumpMarkList().ToListAsync();
			var list = await _context.HPAddRemoveHP_PumpMappsDataViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHP_Pumps {data_status},{heat_point_id}").ToListAsync() ?? new List<HPAddRemoveHP_PumpMappsDataViewModel>();
            list.Add(new HPAddRemoveHP_PumpMappsDataViewModel() { heat_point_id = heat_point_id, data_status = data_status });
			return View("HP_EquipmentPart_Pump_Partial", list);
		}
    }
}
