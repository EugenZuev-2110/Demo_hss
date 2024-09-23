using Microsoft.AspNetCore.Mvc;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.Sources.Components.SourcesEquipments
{
	public class OpenSourcesTurbine_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public OpenSourcesTurbine_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status = 0, int source_id = 0, string action_for = "")
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}

			return View("OpenSourcesTurbine_Partial");
		}
	}
}
