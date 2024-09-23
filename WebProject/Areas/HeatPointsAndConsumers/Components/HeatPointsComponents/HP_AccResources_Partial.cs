using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;

namespace WebProject.Components
{
    public class HP_EquipmentPart_AccResources_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HP_EquipmentPart_AccResources_Partial(HssDbContext context, HSSController m_c)
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

            ViewBag.AccTypes = await _context.Dict_AccResourcesTypes.ToListAsync();
			var list = await _context.HPAddRemove_AccResourcesViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHP_AccResources {data_status},{heat_point_id}").ToListAsync() ?? new List<HPAddRemove_AccResourcesViewModel>();
            list.Add(new HPAddRemove_AccResourcesViewModel() { heat_point_id = heat_point_id, data_status = data_status });
			return View("HP_EquipmentPart_AccResources_Partial", list);
		}
    }
}
