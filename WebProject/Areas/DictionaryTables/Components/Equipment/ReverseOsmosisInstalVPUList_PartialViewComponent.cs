using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class ReverseOsmosisInstalVPUList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public ReverseOsmosisInstalVPUList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var reverseOsmosis = await _context.Equipments.Where(x => x.equip_type_id == 11).Select(x => new ReverseOsmosisInstalVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity}).ToListAsync();

			return View("ReverseOsmosisInstalVPUList_Partial", reverseOsmosis);
		}
	}
}
