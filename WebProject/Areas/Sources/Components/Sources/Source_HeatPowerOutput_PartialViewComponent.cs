using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Sources.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.Sources.Components.Sources
{
    public class Source_HeatPowerOutput_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public Source_HeatPowerOutput_PartialViewComponent(HssDbContext context, HSSController c)
        {
            _context = context;
            _m_c = c;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId, int data_status = 0, int source_id = 0)
        {
            try
            {
                var source = await _context.SourceHeatPowerOutputViewModels.FromSqlInterpolated($"exec [sources].[sp_GetSourceHeatPowerOutput] {data_status}, {source_id}").ToListAsync();
                source[0].source_id = source_id;
                source[0].data_status = data_status;
                ViewBag.TempGraph = await _context.TemperatureGraphics.Select(x => new { x.temp_graph_id, x.temp_graph_name }).ToListAsync();
                ViewBag.ChemType = await _context.Dict_HeatOutSchemeTypes.Select(x => new {x.Id, x.scheme_heat_out_type_name}).ToListAsync();
                
                ViewBag.AvailCondensPipe = new List<string> { "Да", "Нет" };
                return View("Source_HeatPowerOutput_Partial", source);
			}
            catch (Exception ex) { return View("Source_HeatPowerOutput_Partial"); }
        }
    }
}
