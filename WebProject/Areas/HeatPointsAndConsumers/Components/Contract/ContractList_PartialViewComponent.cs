using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;


namespace WebProject.Areas.HeatPointsAndConsumers.Components.Contract
{
	public class ContractList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public ContractList_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}

		public async Task<IViewComponentResult> InvokeAsync(int data_status, byte only_liquidate)
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}
			
			List<ContractListViewModel> contract = await _context.ContractListViewModels.FromSqlInterpolated
				($"exec consumers.sp_GetContractsList {data_status}, {only_liquidate}").ToListAsync() ?? new List<ContractListViewModel>();
			
			return View("ContractList_Partial", contract);
		}
	}
}
