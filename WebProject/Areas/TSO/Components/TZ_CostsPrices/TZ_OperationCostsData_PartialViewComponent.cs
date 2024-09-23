using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
	public class TZ_OperationCostsData_PartialViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(int tz_id, int data_status, int perspective_year, int userId)
		{
			TZViewModel tz = new TZViewModel()
			{
				tz_id = tz_id,
				data_status = data_status,
				perspective_year = perspective_year
			};
			return View("TZ_OperationCostsData_Partial", tz);
		}
	}
	
}
