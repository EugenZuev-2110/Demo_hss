using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Components
{
    public class UnomInfoViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public UnomInfoViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(string searchText, bool IsAdmin, int userId)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                List<UnomInfoViewModel> unoms = await _context.UnomInfoViewModel.FromSqlInterpolated($"exec sp_GetUnomInfo {searchText ?? ""}").ToListAsync();
                unoms[0].ItsExecutorOrAdmin = IsAdmin || unoms[0].executor_id == userId ? true : false;
                ViewBag.DataStatusesList = await _context.DataStatusesView.Select(x => new { x.DataStatus, x.Ds }).ToListAsync();
                ViewBag.LayersList = await _context.DictLayers.Select(x => new { x.Id, x.layer_name }).ToListAsync();
                return View("UnomInfo", unoms[0]);
            }
            else
            {
                return View("UnomInfo", new UnomInfoViewModel());
            }
            //await _context.DisposeAsync();
            //var unoms = await _context.sp_GetUnomsList(searchText ?? string.Empty).ToListAsync();
            
        }
    }
}
