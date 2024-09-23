using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class DeaeratorsList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public DeaeratorsList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var deaerators = await _context.Equipments.Where(x => x.equip_type_id == 13).Select(x => new ReverseOsmosisInstalVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity}).ToListAsync();

			return View("DeaeratorsList_Partial", deaerators);
		}
	}
}
