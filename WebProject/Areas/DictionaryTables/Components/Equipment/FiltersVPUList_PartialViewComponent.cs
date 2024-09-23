using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class FiltersVPUList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public FiltersVPUList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var filtersVPU = await _context.Equipments.Where(x => x.equip_type_id == 9).Select(x => new FiltersVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity, diameter = x.diameter }).ToListAsync();

			return View("FiltersVPUList_Partial", filtersVPU);
		}
	}
}
