using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using Microsoft.Extensions.Hosting;

namespace WebProject.Components
{
    public class HP_DocsFootage_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public HP_DocsFootage_Partial(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int data_status, int heat_point_id)
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

			ViewBag.Host = host + "/Docs/HeatPointDocs/";
			var list = await _context.HPAddRemove_DocsFootageViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHP_DocsFootage {data_status},{heat_point_id}").ToListAsync() ?? new List<HPAddRemove_DocsFootageViewModel>();
            list.Add(new HPAddRemove_DocsFootageViewModel() { heat_point_id = heat_point_id, data_status = data_status });
			return View("HP_DocsFootage_Partial", list);
		}
    }
}
