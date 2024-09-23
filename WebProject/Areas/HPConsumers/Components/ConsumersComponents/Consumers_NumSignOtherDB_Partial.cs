using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;

namespace WebProject.Areas.HPConsumers.Models
{
    public class Consumers_NumSignOtherDB_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public Consumers_NumSignOtherDB_Partial(HssDbContext context, HSSController m_c)
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

			var item = (await _context.Consumers_NumSignOtherDBViewModel.FromSqlInterpolated($"exec consumers.sp_GetConsumersAddRemoveNumSignOtherDbDataOne {data_status},{consumer_id}").ToListAsync()).FirstOrDefault()
                ?? new Consumers_NumSignOtherDBViewModel { data_status = data_status, consumer_id = consumer_id };
			return View("Consumers_NumSignOtherDB_Partial", item);
		}
    }
}
