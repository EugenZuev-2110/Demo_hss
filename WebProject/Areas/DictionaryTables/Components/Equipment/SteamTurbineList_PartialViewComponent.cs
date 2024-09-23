using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class SteamTurbineList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public SteamTurbineList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{

			var SteamTurbine = await _context.Equipments.Where(x => x.equip_type_id == 1).Select(x => new SteamTurbineViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, inst_electric_power = x.inst_electric_power, inst_heat_power = x.inst_heat_power, ihp_heat_selections = x.ihp_heat_selections, ihp_prod_selections = x.ihp_prod_selections, park_resources = x.park_resources, fresh_steam_pressure = x.fresh_steam_pressure, fresh_steam_temp = x.fresh_steam_temp }).ToListAsync();
			
			return View("SteamTurbineList_Partial", SteamTurbine);
		}
	}
}
