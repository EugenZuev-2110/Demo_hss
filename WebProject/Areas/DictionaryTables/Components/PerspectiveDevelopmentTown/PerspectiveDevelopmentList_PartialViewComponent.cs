using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Areas.DictionaryTables.Models;

namespace WebProject.Components
{
    public class PerspectiveDevelopmentList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public PerspectiveDevelopmentList_PartialViewComponent(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<PerspectiveDevelopment_MainListViewModel> tz = await _context.PerspectiveDevelopment_MainListViewModel.FromSqlInterpolated
                ($"exec consumers.sp_PerspectiveDevTown_MainDataList").ToListAsync();

			return View("PerspectiveDevelopmentList_Partial", tz ?? new List<PerspectiveDevelopment_MainListViewModel>());
		}
    }
}
