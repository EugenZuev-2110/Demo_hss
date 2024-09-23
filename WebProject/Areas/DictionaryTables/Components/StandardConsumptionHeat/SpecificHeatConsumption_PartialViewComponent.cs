using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Components.StandardConsumptionHeat
{
	public class SpecificHeatConsumption_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public SpecificHeatConsumption_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int userId, int data_status, int district_id, int consumptionUnit)
		{
			List<CalcConsumptionViewModel> CalcConsumptionHeating = new();
			try
			{
				if (consumptionUnit == 1)
				{
					CalcConsumptionHeating = await _context.CalcConsumptionViewModels.FromSqlInterpolated($"exec consumers.sp_GetCalcConsumptionHeating1mList {data_status}, {district_id}").ToListAsync();
				}
				if (consumptionUnit == 2)
				{
					CalcConsumptionHeating = await _context.CalcConsumptionViewModels.FromSqlInterpolated($"exec consumers.sp_CalcConsumptionHeatingYear {data_status}, {district_id}").ToListAsync();
				}
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("SpecificHeatConsumption_PartialViewComponent", $"data_status={data_status},district_id={district_id}, consumptionUnit={consumptionUnit}", ex.Message, userId);
			}
			return View("SpecificHeatConsumption_Partial", CalcConsumptionHeating);
		}
	}
}
