using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;

namespace WebProject.Components
{
    public class HP_EquipmentPart_HeatExchange_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HP_EquipmentPart_HeatExchange_Partial(HssDbContext context, HSSController m_c)
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

            ViewBag.HtExchangerEquipTypes = await _context.Dict_HtExchangerEquipTypes.ToListAsync();
			ViewBag.EquipmentMarks = await _context.fnt_GetEquipmentMarkList().ToListAsync();
			ViewBag.EquipmentHpTypes = await _context.Dict_HeatExchangerTypes.ToListAsync();
			ViewBag.EquipmentStageGVSSchemes = await _context.Dict_StageGVSSchemes.ToListAsync();

			var list = await _context.HPAddRemoveHP_HeatExchangerMappsDataViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHP_HeatExchange {data_status},{heat_point_id}").ToListAsync() ?? new List<HPAddRemoveHP_HeatExchangerMappsDataViewModel>();
            list.Add(new HPAddRemoveHP_HeatExchangerMappsDataViewModel() { heat_point_id = heat_point_id, data_status = data_status });
			return View("HP_EquipmentPart_HeatExchange_Partial", list);
		}
    }
}
