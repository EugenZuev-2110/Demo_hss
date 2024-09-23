using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.HeatPointsAndConsumers.Components.Building
{
	public class BuildingList_PartialViewComponent : ViewComponent
	{
		private readonly HssDbContext _context;
		private readonly HSSController _m_c;

		public BuildingList_PartialViewComponent(HssDbContext context, HSSController c)
		{
			_context = context;
			_m_c = c;
		}

		public async Task<IViewComponentResult> InvokeAsync(int data_status)
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}

			List<BuildingListViewModel> build = await _context.BuildingListViewModels.FromSqlInterpolated
				($"exec consumers.sp_GetBuildingList {data_status}").ToListAsync() ?? new List<BuildingListViewModel>();

			return View("BuildingList_Partial", build);
		}
	}
}
