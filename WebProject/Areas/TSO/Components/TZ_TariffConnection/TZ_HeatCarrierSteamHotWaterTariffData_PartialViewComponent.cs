using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Areas.TSO.Components.TZ_TariffConnection
{
	public class TZ_HeatCarrierSteamHotWaterTariffData_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;

		public TZ_HeatCarrierSteamHotWaterTariffData_PartialViewComponent(HssDbContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int tz_id, int userId)
		{
			var tz_data = (await _context.HeatEnergyTariffSteamHotWater.FromSqlInterpolated($"exec tarif_zone.sp_GetTZTariffsHeatCarrier_FactOne {data_status},{perspective_year},{tz_id},{userId}")
				.ToListAsync()).FirstOrDefault();

			return View("TZ_HeatCarrierSteamHotWaterTariffData_Partial", tz_data ?? new HeatEnergyTariffSteamHotWater());
		}
	}
}
