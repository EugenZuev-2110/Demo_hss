using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.HeatPointsAndConsumers.Components.Contract
{
	public class ContractConsumerList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public ContractConsumerList_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}
		public async Task<IViewComponentResult> InvokeAsync(int contract_id)
		{
			int data_status = _m_c.GetCurrentDS();
			
				List<ContractConsumerListViewModel> contract = await _context.ContractConsumerListViewModels.FromSqlInterpolated
					($"exec consumers.sp_GetContractConsumerList {data_status}, {contract_id}").ToListAsync() ?? new List<ContractConsumerListViewModel>();
				ViewBag.contract = contract_id.ToString();

				return View("ContractConsumerList_Partial", contract);
			
			
		}
	}
}
