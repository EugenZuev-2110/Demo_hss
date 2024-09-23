using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;

namespace WebProject.Areas.HPConsumers.Models
{
    public class Consumers_PerspectiveDev_SubPartial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public Consumers_PerspectiveDev_SubPartial(HssDbContext context, HSSController m_c)
		{
			_context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int data_status, int consumer_id, int dev_prog_id = -1)
        {
            if (data_status == 0)
            {
                data_status = _m_c.GetCurrentDS();
            }

			ViewBag.IsDisabled = consumer_id == 0 ? "disabled" : String.Empty;
            var item = new Consumers_SubPartialYearDynamicViewModel();
			item.Consumers_PerspectiveDev_SubPartialViewModel = (await _context.Consumers_PerspectiveDev_SubPartialViewModel.FromSqlInterpolated($"exec consumers.sp_GetConsumers_PerspectiveDev_SubPartialDataOne {data_status},{consumer_id},{dev_prog_id}").ToListAsync()).FirstOrDefault()
                ?? new Consumers_PerspectiveDev_SubPartialViewModel { data_status = data_status, consumer_id = consumer_id, dev_prog_id = dev_prog_id };

            item.Consumers_PerspectiveDev_YearDynamicViewModel = await _context.Consumers_PerspectiveDev_YearDynamicViewModel.FromSqlInterpolated($"exec consumers.sp_GetConsumers_PerspectiveDev_YearDynamicDataList {data_status},{consumer_id},{dev_prog_id}").ToListAsync();

            if(item.Consumers_PerspectiveDev_YearDynamicViewModel == null)
            {
                item.Consumers_PerspectiveDev_YearDynamicViewModel = new List<Consumers_PerspectiveDev_YearDynamicViewModel>();
                item.Consumers_PerspectiveDev_YearDynamicViewModel.Add(new Consumers_PerspectiveDev_YearDynamicViewModel { data_status = data_status, consumer_id = consumer_id, dev_prog_id = dev_prog_id });
            }                

			return View("Consumers_PerspectiveDev_SubPartial", item);
		}
    }
}
