using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.StandardConsumptionHeat
{
	public class CalcConsumptionGVS_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public CalcConsumptionGVS_PartialViewComponent(HssDbContext context, HSSController c)
		{
            _context = context;
			_m_c = c;
		}

		public async Task<IViewComponentResult> InvokeAsync(int userId, int data_status, int district_id, int consumptionUnit)
		{
			List<CalcConsumptionGVSViewModel> CalcConsumptionGVS = new();
			try
			{
				if (consumptionUnit == 1)
				{
					CalcConsumptionGVS = await _context.CalcConsumptionGVSViewModels.FromSqlInterpolated($"exec consumers.sp_GetCalcConsumptionHeatingGSV {data_status}, {district_id}").ToListAsync();
				}
				if (consumptionUnit == 2)
				{
					CalcConsumptionGVS = await _context.CalcConsumptionGVSViewModels.FromSqlInterpolated($"exec consumers.sp_GetCalcConsumptionHeatingGSV {data_status}, {district_id}").ToListAsync();
				}
				ViewBag.ConsumptionUnit = consumptionUnit;
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("CalcConsumptionGVS_PartialViewComponent", $"data_status={data_status},district_id={district_id}, consumptionUnit={consumptionUnit}", ex.Message, userId);
			}
			return View("CalcConsumptionGVS_Partial", CalcConsumptionGVS);

		}
	}
}
