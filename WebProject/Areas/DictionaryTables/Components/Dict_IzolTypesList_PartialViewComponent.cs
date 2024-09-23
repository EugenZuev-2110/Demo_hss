using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components
{
	public class Dict_IzolTypesList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public Dict_IzolTypesList_PartialViewComponent(HssDbContext context, HSSController c)
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
			var izol = await _context.Dict_IzolTypes.ToListAsync();
			return View("Dict_IzolTypesList_Partial", izol);

		}
	}
}
