using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;

namespace WebProject.Components
{
    public class TZ_TariffHotWaterDataList_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public TZ_TariffHotWaterDataList_PartialViewComponent(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int data_status, int perspective_year, int userId)
        {
            if (data_status == 0)
            {
                data_status = _m_c.GetCurrentDS();
            }
            if (perspective_year == 0)
            {
                perspective_year = _m_c.GetCurrentYearByDS(data_status);
            }

			List<TZTariffHotWaterTabModel> tz = await _context.TZTariffHotWaterTabModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZTariffsDataList {data_status},{perspective_year},{userId}").ToListAsync();
			return View("TZ_TariffHotWaterDataList_Partial", tz);
			//return View("TZ_TariffDataList_Partial");
		}
    }
}
