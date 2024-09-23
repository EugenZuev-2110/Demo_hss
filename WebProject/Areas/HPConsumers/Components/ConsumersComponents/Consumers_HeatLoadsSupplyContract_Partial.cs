using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.HPConsumers.Components.ConsumersComponents
{
	public class Consumers_HeatLoadsSupplyContract_Partial : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public Consumers_HeatLoadsSupplyContract_Partial(HssDbContext context, HSSController m_c)
		{
			_context = context;
			_m_c = m_c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int data_status, int consumer_id)
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}

			var data = (await _context.Consumers_HeatLoadsSupplyContractDataViewModel.FromSqlInterpolated($"exec consumers.sp_GetConsumers_HeatLoadsSupplyContract {data_status},{consumer_id}")
				.ToListAsync()).FirstOrDefault() ?? new Consumers_HeatLoadsSupplyContractDataViewModel();

            if (consumer_id == 0)
                ViewBag.IsDisabled = "disabled";
            else
                ViewBag.IsDisabled = String.Empty;

            ViewBag.ContractList = _context.fnt_GetSupplyContractsByChars("");

            return View("Consumers_HeatLoadsSupplyContract_Partial", data);
		}
	}
}
