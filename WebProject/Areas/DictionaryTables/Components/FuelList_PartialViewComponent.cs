using DataBase.Models.Sources;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Components
{
	public class FuelList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public FuelList_PartialViewComponent(HssDbContext context, HSSController c)
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
			var fuel = await _context.Dict_FuelTypes.ToListAsync();
			return View("FuelList_Partial", fuel);

		}
	}
}
