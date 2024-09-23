using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components
{
	public class OrganizationList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public OrganizationList_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}

		public async Task<IViewComponentResult> InvokeAsync(int data_status, int userId)
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}
			var org = await _context.OrganizationViewModels.FromSqlInterpolated($"exec org.OrganizationList {data_status}").ToListAsync();

			return View("OrganizationList_Partial", org);
		}
	}
}
