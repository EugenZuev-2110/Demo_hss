using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class PumpsList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public PumpsList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var pumps = await _context.Equipments.Where(x => x.equip_type_id == 8).Select(x => new PumpsViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, manufacturer = x.manufacturer, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity, pump_press = x.pump_press }).ToListAsync();

			return View("PumpsList_Partial", pumps);
		}
	}
}
