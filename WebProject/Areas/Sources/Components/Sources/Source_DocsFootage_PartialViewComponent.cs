using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.Sources.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.Sources.Components.Sources
{
    public class Source_DocsFootage_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public Source_DocsFootage_PartialViewComponent(HssDbContext context, HSSController c)
        {
            _context = context;
            _m_c = c;
        }

        public async Task<IViewComponentResult> InvokeAsync(int data_status, int source_id)
        {
            if (data_status == 0)
            {
                data_status = _m_c.GetCurrentDS();
            }

            ViewBag.DocumentTypes = await _context.DocumentTypes.ToListAsync();
            var host = HttpContext.Request.Host.Value;

            if (host.Contains("localhost"))
                host = "http://" + host;
            else
                host = "https://" + host;

            ViewBag.Host = host + "/Docs/SourceDocs/";
            var list = await _context.Source_DocsFootageViewModels.FromSqlInterpolated($"exec sources.sp_GetSource_DocsFootage {data_status},{source_id}").ToListAsync() ?? new List<Source_DocsFootageViewModel>();
            list.Add(new Source_DocsFootageViewModel() { source_id = source_id, data_status = data_status });
            return View("Source_DocsFootage_Partial", list);
        }
    }
}
