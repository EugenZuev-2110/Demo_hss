using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class HWBoilersList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public HWBoilersList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var HWBoilers = await _context.Equipments.Where(x => x.equip_type_id == 4).Select(x => new HWBoilersViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, inst_heat_power = x.inst_heat_power, norm_service_life = x.norm_service_life, kpd = x.kpd, max_temp_out = x.max_temp_out, net_water_consump_min = x.net_water_consump_min, net_water_consump_nom = x.net_water_consump_nom, net_water_consump_max = x.net_water_consump_max }).ToListAsync();

			return View("HWBoilersList_Partial", HWBoilers);
		}
	}
}
