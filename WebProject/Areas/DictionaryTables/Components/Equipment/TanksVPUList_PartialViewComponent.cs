using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class TanksVPUList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TanksVPUList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var tanks = await _context.Equipments.Where(x => x.equip_type_id == 14).Select(x => new TanksVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, volume = x.volume }).ToListAsync();

			return View("TanksVPUList_Partial", tanks);
		}
	}
}
