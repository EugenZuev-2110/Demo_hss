using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class NanoFiltrationInstallVPUList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public NanoFiltrationInstallVPUList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var nanoFiltr = await _context.Equipments.Where(x => x.equip_type_id == 12).Select(x => new ReverseOsmosisInstalVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity}).ToListAsync();

			return View("NanoFiltrationInstallVPUList_Partial", nanoFiltr);
		}
	}
}
