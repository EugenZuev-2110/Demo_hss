using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.TariffZone
{
	public class TariffZoneTsoList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TariffZoneTsoList_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int tz_id, int userId)
		{
			var tariffZoneTosList = new TariffZoneTsoViewModel();
			try
			{
				tariffZoneTosList.TariffZoneTsoList = await _context.TariffZoneTsoViewModals.FromSqlInterpolated($"exec tarif_zone.GetTZTSOPerspective {data_status}, {tz_id}").ToListAsync();
				tariffZoneTosList.tz_id = tz_id;
				tariffZoneTosList.data_status = data_status;
				ViewBag.TsoList = await _context.fnt_GetTSOName(data_status).ToArrayAsync();
			}
			catch (Exception ex) { }
			//ViewBag.Action_for = action_for;
			return View("TariffZoneTsoList_Partial", tariffZoneTosList);
		}
	}
}
