using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Areas.DictionaryTables.Models;

namespace WebProject.Areas.DictionaryTables.Components.PerspectiveDevelopmentTown
{
    public class PerspectiveDevelopment_GenInfo_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public PerspectiveDevelopment_GenInfo_PartialViewComponent(HssDbContext context, HSSController m_c)
        {
            _context = context; 
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int dev_prog_id)
        {
			var item = (await _context.PerspectiveDevelopment_GenInfoViewModel.FromSqlInterpolated($"exec consumers.sp_GetPerspectiveDevelopmentGenInfoDataOne {dev_prog_id}").ToListAsync()).FirstOrDefault()
                ?? new PerspectiveDevelopment_GenInfoViewModel { dev_prog_id = dev_prog_id };

            ViewBag.PerspectiveDevelopmentList = await _context.Dict_DevProgTypes.ToListAsync();
            ViewBag.LayerList = await _context.Layers.ToListAsync();

            return View("PerspectiveDevelopment_GenInfo_Partial", item);
		}
    }
}
