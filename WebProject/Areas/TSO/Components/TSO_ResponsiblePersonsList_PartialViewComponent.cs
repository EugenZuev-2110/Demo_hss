using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TSO_ResponsiblePersonsList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;

        public TSO_ResponsiblePersonsList_PartialViewComponent(HssDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int data_status, int tso_id, int userId)
        {
			var tso_resp_pers = new TSOResponsiblePersonsViewModel();
			tso_resp_pers.tso_Id = tso_id;
			tso_resp_pers.data_status = data_status;
			tso_resp_pers.TSOResponsiblePersonsList = await _context.TSOResponsiblePersonsListViewModel.FromSqlInterpolated($"exec tso.sp_GetTSOResponsiblePersonsList {data_status},{tso_id},{userId}").ToListAsync();
            tso_resp_pers.TSOResponsiblePersonsList.Add(new TSOResponsiblePersonsListViewModel());

			return View("TSO_ResponsiblePersonsList_Partial", tso_resp_pers);
        }
    }
}
