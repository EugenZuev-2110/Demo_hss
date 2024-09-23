using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class ComplexonsVPUList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public ComplexonsVPUList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var complexonsVPU = await _context.Equipments.Where(x => x.equip_type_id == 15).Select(x => new ReverseOsmosisInstalVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity}).ToListAsync();

			return View("ComplexonsVPUList_Partial", complexonsVPU);
		}
	}
}
