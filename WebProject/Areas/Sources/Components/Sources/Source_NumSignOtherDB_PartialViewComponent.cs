using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.Sources.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.Sources.Components.Sources
{
	public class Source_NumSignOtherDB_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public Source_NumSignOtherDB_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}

		public async Task<IViewComponentResult> InvokeAsync(int userId, int data_status = 0, int source_id = 0, int tz = 0, int status = 0, int org = 0, int type = 0)
		{
			var Source = (await _context.S_Histories.Where(x => x.source_id == source_id && x.data_status == data_status).Select( x => new SourceNumSignOtherDBViewModel { muid = x.muid, lotus_id = x.lotus_id, kasu_composite_id = x.kasu_composite_id, kasu_id = x.kasu_id, kasu_map_id = x.kasu_map_id }).ToListAsync()).FirstOrDefault()
				?? new SourceNumSignOtherDBViewModel();
			Source.data_status = data_status;
			Source.source_id = source_id;
			return View("Source_NumSignOtherDB_Partial", Source); 
			
		}
	}
}
