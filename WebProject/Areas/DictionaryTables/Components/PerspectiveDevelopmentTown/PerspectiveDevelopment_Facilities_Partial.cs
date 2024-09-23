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
    public class PerspectiveDevelopment_Facilities_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public PerspectiveDevelopment_Facilities_PartialViewComponent(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int dev_prog_id)
        {
            var item = await _context.PerspectiveDevelopment_FacilityListViewModel.FromSqlInterpolated($"exec consumers.sp_GetPerspectiveDev_FacilitiesDataList {dev_prog_id}").ToListAsync();

            if (item == null || item.Count == 0)
                item = new List<PerspectiveDevelopment_FacilityListViewModel> { new PerspectiveDevelopment_FacilityListViewModel { dev_prog_id = dev_prog_id }};

            return View("PerspectiveDevelopment_Facilities_Partial", item);
        }
    }
}
