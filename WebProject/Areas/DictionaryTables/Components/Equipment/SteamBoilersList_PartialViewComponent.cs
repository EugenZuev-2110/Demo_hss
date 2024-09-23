using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class SteamBoilersList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public SteamBoilersList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var SteamBoilers = await _context.Equipments.Where(x => x.equip_type_id == 3).Select(x => new SteamBoilersViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, capacity = x.capacity, inst_heat_power = x.inst_heat_power, norm_service_life = x.norm_service_life, kpd = x.kpd, fresh_steam_pressure = x.fresh_steam_pressure, fresh_steam_temp = x.fresh_steam_temp }).ToListAsync();

			return View("SteamBoilersList_Partial", SteamBoilers);
		}
	}
}
