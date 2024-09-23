using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TSOList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public TSOList_PartialViewComponent(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, byte only_reg_contract, byte only_liquidate, int userId)
        {
            if (data_status == 0)
            {
                data_status = _m_c.GetCurrentDS();
            }
            if (perspective_year == 0)
            {
                perspective_year = _m_c.GetCurrentYearByDS(data_status);
            }

            List<TSOListViewModel> tso = await _context.TSOListViewModel.FromSqlInterpolated($"exec tso.sp_GetTSOList {data_status},{perspective_year},{only_reg_contract},{only_liquidate},{userId}").ToListAsync();
            //await _context.DisposeAsync();
            return View("TSOList_Partial", tso);
            //return View("TSOList_Partial");
        }
    }
}
