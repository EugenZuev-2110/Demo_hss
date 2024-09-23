using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.HPConsumers.Components.ConsumersComponents
{
	public class Consumers_DestRelCatHS_Partial : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public Consumers_DestRelCatHS_Partial(HssDbContext context, HSSController m_c)
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

			var data = (await _context.Consumers_DestRelCatHSDataViewModel.FromSqlInterpolated($"exec consumers.sp_GetConsumers_DestRelCatHSDataOne {data_status},{consumer_id}")
				.ToListAsync()).FirstOrDefault() ?? new Consumers_DestRelCatHSDataViewModel();

            if (consumer_id == 0)
                ViewBag.IsDisabled = "disabled";
            else
                ViewBag.IsDisabled = String.Empty;

            ViewBag.ReliabilityCategories = (await _context.Dict_ReliabilityCategories.ToListAsync()).Select(n => new {n.Id, n.rcat_name });
            ViewBag.CalcPurposeTypes = (await _context.Dict_CalcPurposeTypes.ToListAsync()).Select(n => new { n.Id, n.main_purpose_type_id, n.cpurp_type_name });
            ViewBag.ProdSupplyType = (await _context.Dict_ProdSupplyType.ToListAsync()).Select(n => new { n.Id, n.ps_type_name });
            ViewBag.MainPurposeTypes = (await _context.Dict_MainPurposeTypes.ToListAsync()).Select(n => new { n.Id, n.ptype_name });

            return View("Consumers_DestRelCatHS_Partial", data);
		}
	}
}
