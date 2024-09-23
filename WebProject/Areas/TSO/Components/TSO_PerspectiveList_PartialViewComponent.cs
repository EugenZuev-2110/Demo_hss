using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TSO_PerspectiveList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;

        public TSO_PerspectiveList_PartialViewComponent(HssDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int data_status, int tso_id, int userId)
        {
            var tso_p = new TSOPerspectiveViewModel();
			tso_p.Id = tso_id;
            tso_p.data_status= data_status;

			tso_p.TSOPerspectiveList = await _context.TSOPerspectiveListViewModel.FromSqlInterpolated($"exec tso.sp_GetTSOPerspectiveList {data_status},{tso_id},{userId}").ToListAsync();
            ViewBag.OrgStatusesList = _context.Dict_OrgStatuses.ToList();
			ViewBag.TSOTypesList = _context.Dict_TSOTypes.ToList();
			//await _context.DisposeAsync();
			return View("TSO_PerspectiveList_Partial", tso_p);
            //return View("TSOList_Partial");
        }
    }
}
