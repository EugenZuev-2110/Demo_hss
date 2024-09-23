using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.PerspectiveDevelopmentTown
{
	public class PerspectiveDevelopment_Facilities_MainIndicatorDynamic_Partial : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public PerspectiveDevelopment_Facilities_MainIndicatorDynamic_Partial(HssDbContext context, HSSController m_c)
		{
			_context = context;
			_m_c = m_c;
		}
		public async Task<IViewComponentResult> InvokeAsync( int consumer_id, int dev_prog_id )
		{
			var data = new DevProgrammsLoadsCalculated_List();
            data.AddRange(await _context.DevProgrammsLoadsCalculated.Where(n => n.dev_prog_id == dev_prog_id && n.consumer_id == consumer_id).ToListAsync());

            if (consumer_id == 0)
                ViewBag.IsDisabled = "disabled";
            else
                ViewBag.IsDisabled = String.Empty;

            ViewBag.ReliabilityCategories = (await _context.Dict_ReliabilityCategories.ToListAsync()).Select(n => new {n.Id, n.rcat_name });
            ViewBag.CalcPurposeTypes = (await _context.Dict_CalcPurposeTypes.ToListAsync()).Select(n => new { n.Id, n.main_purpose_type_id, n.cpurp_type_name });
            ViewBag.ProdSupplyType = (await _context.Dict_ProdSupplyType.ToListAsync()).Select(n => new { n.Id, n.ps_type_name });
            ViewBag.MainPurposeTypes = (await _context.Dict_MainPurposeTypes.ToListAsync()).Select(n => new { n.Id, n.ptype_name });
            ViewBag.Floor = await _context.Dict_Floors.Select(x => new Dict_Floors { Id = x.Id, floor_name = x.floor_name }).ToListAsync();

            return View("PerspectiveDevelopment_Facilities_MainIndicatorDynamic_Partial", data);
		}
	}
}
