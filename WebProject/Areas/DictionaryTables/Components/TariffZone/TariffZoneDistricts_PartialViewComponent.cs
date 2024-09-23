using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.TariffZone
{
	public class TariffZoneDistricts_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TariffZoneDistricts_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int tz_id, int perspective_year, int userId)
		{
			var tariffZoneDistrict = new TariffZoneDistrictViewModal();
			try
			{
				 tariffZoneDistrict.TariffZoneDistrictList = await _context.DistrictViewModals.FromSqlInterpolated($"select * from tarif_zone.GetTarifZoneDistricts({perspective_year},{data_status}, {tz_id},{userId})").ToListAsync();
				tariffZoneDistrict.tz_id = tz_id;
				tariffZoneDistrict.data_status = data_status;
			}
			catch (Exception ex) { }
			//ViewBag.Action_for = action_for;
			return View("TariffZoneDistricts_Partial", tariffZoneDistrict);
		}
	}
}
