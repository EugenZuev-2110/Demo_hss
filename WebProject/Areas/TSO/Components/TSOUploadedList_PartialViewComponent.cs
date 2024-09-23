using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TSO_UploadedList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public TSO_UploadedList_PartialViewComponent(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int data_status, int userId)
        {
            if (data_status == 0)
            {
                data_status = _m_c.GetCurrentDS();
            }

            int? batch_id = _context.TestUploadTSO.Where(x => x.data_status == data_status).Max(x => (int?)x.batch_id);

            List<TSOUploadedListViewModel> tso = await _context.TestUploadTSO.Where(x => x.batch_id == batch_id && x.is_uploaded == false)
                .Select(x => new TSOUploadedListViewModel { Id = x.Id, Name = x.Name, INN = x.INN }).ToListAsync();

            //List<TSOUploadedListViewModel> tso = await _context.TSOListViewModel.FromSqlInterpolated($"exec tso.sp_GetTSOList {data_status},{perspective_year},{only_reg_contract},{only_liquidate},{userId}").ToListAsync();
            return View("TSO_UploadedList_Partial", tso);
        }
    }
}
