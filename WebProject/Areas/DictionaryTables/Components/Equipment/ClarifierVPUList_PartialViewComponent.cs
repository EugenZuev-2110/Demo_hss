using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class ClarifierVPUList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public ClarifierVPUList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var Clarifier = await _context.Equipments.Where(x => x.equip_type_id == 10).Select(x => new ClarifierVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity, volume = x.volume, diameter = x.diameter }).ToListAsync();

			return View("ClarifierVPUList_Partial", Clarifier);
		}
	}
}
