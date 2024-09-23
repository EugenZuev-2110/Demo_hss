using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Models;

namespace WebProject.Components
{
    public class Consumers_YearParamsCalcLoad_Partial : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public Consumers_YearParamsCalcLoad_Partial(HssDbContext context, HSSController m_c)
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

			var list = await _context.Consumers_YearParamsCalcLoadViewModel.FromSqlInterpolated
                ($"exec consumers.sp_GetConsumers_YearParamsCalcLoadDataOne {data_status},{consumer_id}").ToListAsync();

            if (consumer_id == 0)
                ViewBag.IsDisabled = "disabled";
            else
                ViewBag.IsDisabled = String.Empty;

			ViewBag.CalcMethodTypes = await _context.Dict_CalcMethodTypes.ToListAsync();
            ViewBag.Statuses = await _context.Dict_Statuses.ToListAsync();
            ViewBag.HpNumberAddressList = await _context.fnt_GetUnomTpAddressListByChars("", data_status).ToListAsync();
            ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
            ViewBag.DevProgrammList = await Task.Run(() => (from dp in _context.DevProgramms
                                      join dpc in _context.DevProgrammsConsumers on dp.dev_prog_id equals dpc.dev_prog_id
                                      where dpc.consumer_id == consumer_id
                                      select new { dp.unom_num, dp.dev_prog_id }));

            return View("Consumers_YearParamsCalcLoad_Partial", list);
		}
    }
}
