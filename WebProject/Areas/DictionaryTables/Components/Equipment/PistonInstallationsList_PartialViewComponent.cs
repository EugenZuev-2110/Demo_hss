using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.Equipment
{
    public class PistonInstallationsList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;

        public PistonInstallationsList_PartialViewComponent(HssDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {

            var Piston = await _context.Equipments.Where(x => x.equip_type_id == 5).Select(x => new PistonInstallationsViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, inst_electric_power = x.inst_electric_power, inst_heat_power = x.inst_heat_power, park_resources = x.park_resources}).ToListAsync();

            return View("PistonInstallationsList_Partial", Piston);
        }
    }
}
