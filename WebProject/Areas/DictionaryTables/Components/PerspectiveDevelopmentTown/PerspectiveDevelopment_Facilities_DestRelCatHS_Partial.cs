using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.PerspectiveDevelopmentTown
{
	public class PerspectiveDevelopment_Facilities_DestRelCatHS_Partial : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public PerspectiveDevelopment_Facilities_DestRelCatHS_Partial(HssDbContext context, HSSController m_c)
		{
			_context = context;
			_m_c = m_c;
		}
		public async Task<IViewComponentResult> InvokeAsync( int consumer_id, int dev_prog_id )
		{
			var data = (await _context.PerspectiveDevelopment_Facilities_DestRelCatHSViewModel.FromSqlInterpolated($"exec consumers.sp_GetPerspectiveDevelopment_Facilities_DestRelCatHSDataOne {consumer_id},{dev_prog_id}")
				.ToListAsync()).FirstOrDefault() ?? new PerspectiveDevelopment_Facilities_DestRelCatHSViewModel();

            if (consumer_id == 0)
                ViewBag.IsDisabled = "disabled";
            else
                ViewBag.IsDisabled = String.Empty;

            ViewBag.ReliabilityCategories = (await _context.Dict_ReliabilityCategories.ToListAsync()).Select(n => new {n.Id, n.rcat_name });
            ViewBag.CalcPurposeTypes = (await _context.Dict_CalcPurposeTypes.ToListAsync()).Select(n => new { n.Id, n.main_purpose_type_id, n.cpurp_type_name });
            ViewBag.ProdSupplyType = (await _context.Dict_ProdSupplyType.ToListAsync()).Select(n => new { n.Id, n.ps_type_name });
            ViewBag.MainPurposeTypes = (await _context.Dict_MainPurposeTypes.ToListAsync()).Select(n => new { n.Id, n.ptype_name });
            ViewBag.Floor = _context.Dict_Floors.Select(x => new Dict_Floors { Id = x.Id, floor_name = x.floor_name }).ToList();

            return View("PerspectiveDevelopment_Facilities_DestRelCatHS_Partial", data);
		}
	}
}
