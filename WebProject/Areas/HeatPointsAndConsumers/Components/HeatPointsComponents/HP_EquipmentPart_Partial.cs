using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;

namespace WebProject.Components
{
    public class HP_EquipmentPart_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HP_EquipmentPart_Partial(HssDbContext context, HSSController m_c)
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
				ViewBag.IsDisabled = string.Empty;

			var item = await _context.HP_TankBatteryAvailables.FirstOrDefaultAsync(n => n.data_status == data_status && n.heat_point_id == heat_point_id) ?? new HP_TankBatteryAvailables();
			return View("HP_EquipmentPart_Partial", item);
		}
    }
}
