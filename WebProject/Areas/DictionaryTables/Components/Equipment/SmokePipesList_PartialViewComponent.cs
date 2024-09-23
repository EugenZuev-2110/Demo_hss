using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class SmokePipesList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public SmokePipesList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var smokePipes = await _context.Equipments.Where(x => x.equip_type_id == 16).Select(x => new SmokePipesViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, pipe_heigh_ground_lvl = x.pipe_heigh_ground_lvl, diameter = x.diameter }).ToListAsync();

			return View("SmokePipesList_Partial", smokePipes);
		}
	}
}
