using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
	public class GasTurbineList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public GasTurbineList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync(int userId)
		{
			var GasTurbine = await _context.Equipments.Where(x => x.equip_type_id == 2).Select(x => new GasTurbineViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, inst_electric_power = x.inst_electric_power,  park_resources = x.park_resources, norm_count_start = x.norm_count_start }).ToListAsync();
			
			return View("GasTurbineList_Partial", GasTurbine);
		}
	}
}
