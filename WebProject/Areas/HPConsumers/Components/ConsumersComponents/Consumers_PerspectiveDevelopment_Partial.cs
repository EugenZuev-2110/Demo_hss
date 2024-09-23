using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;

namespace WebProject.Areas.HPConsumers.Models
{
    public class Consumers_PerspectiveDevelopment_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public Consumers_PerspectiveDevelopment_Partial(HssDbContext context, HSSController m_c)
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

			ViewBag.IsDisabled = consumer_id == 0 ? "disabled" : String.Empty;

			var item = await _context.Consumers_PerspectiveDevelopmentViewModel.FromSqlInterpolated($"exec consumers.sp_GetConsumers_PerspectiveDev_MainDataList {data_status},{consumer_id}").ToListAsync()
                ?? new List<Consumers_PerspectiveDevelopmentViewModel>();

            if(item.Count == 0)
            {
                item.Add(new Consumers_PerspectiveDevelopmentViewModel() { data_status = data_status, consumer_id = consumer_id });
                ViewBag.IsEmpty = true;
            }                 

			return View("Consumers_PerspectiveDevelopment_Partial", item);
		}
    }
}
