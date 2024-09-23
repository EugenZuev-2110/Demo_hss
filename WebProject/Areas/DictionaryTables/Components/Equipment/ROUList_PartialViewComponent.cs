using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class ROUList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public ROUList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{

			var rou = await _context.Equipments.Where(x => x.equip_type_id == 6).Select(x => new ROUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity, fresh_steam_pressure = x.fresh_steam_pressure, fresh_steam_temp = x.fresh_steam_temp }).ToListAsync();

			return View("ROUList_Partial", rou);
		}
	}

}
