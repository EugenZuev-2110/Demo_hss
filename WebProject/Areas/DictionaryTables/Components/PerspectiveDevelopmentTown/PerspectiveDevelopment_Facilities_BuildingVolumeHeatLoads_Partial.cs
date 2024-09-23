using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.PerspectiveDevelopmentTown
{
	public class PerspectiveDevelopment_Facilities_BuildingVolumeHeatLoads_Partial : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

        public PerspectiveDevelopment_Facilities_BuildingVolumeHeatLoads_Partial(HssDbContext context, HSSController m_c)
		{
			_context = context;
			_m_c = m_c;
        }
		public async Task<IViewComponentResult> InvokeAsync( int consumer_id, int dev_prog_id )
		{
			var data_status = _m_c.GetCurrentDS();
            var data = (await _context.PerspectiveDevelopment_Facilities_BuildingVolumeHeatLoadsViewModel.FromSqlInterpolated($"exec consumers.sp_GetPerspectiveDevelopment_Facilities_BuildingVolumeHeatLoads_PartialDataOne {data_status},{consumer_id},{dev_prog_id}")
				.ToListAsync()).FirstOrDefault() ?? new PerspectiveDevelopment_Facilities_BuildingVolumeHeatLoadsViewModel();

            if (consumer_id == 0)
                ViewBag.IsDisabled = "disabled";
            else
                ViewBag.IsDisabled = String.Empty;

            ViewBag.CalcHlDetermMethodList = await _context.Dict_CalcHeatLoadsTypes.ToListAsync();
            ViewBag.CalcAreaTypeList = await _context.Dict_CalcAreaTypes.ToListAsync();

            return View("PerspectiveDevelopment_Facilities_BuildingVolumeHeatLoads_Partial", data);
		}
	}
}
