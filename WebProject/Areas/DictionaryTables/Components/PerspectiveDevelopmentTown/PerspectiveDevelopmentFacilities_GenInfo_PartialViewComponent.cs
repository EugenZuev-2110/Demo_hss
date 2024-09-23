using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Areas.DictionaryTables.Models;

namespace WebProject.Areas.DictionaryTables.Components.PerspectiveDevelopmentTown
{
    public class PerspectiveDevelopmentFacilities_GenInfo_PartialViewComponent : ViewComponent
    {
        private readonly HssDbContext _context;
        private readonly HSSController _m_c;

        public PerspectiveDevelopmentFacilities_GenInfo_PartialViewComponent(HssDbContext context, HSSController m_c)
        {
            _context = context;
            _m_c = m_c;
        }
        public async Task<IViewComponentResult> InvokeAsync(int dev_prog_id, int consumer_id)
        {
            var item = (await _context.PerspectiveDevelopmentFacility_GenInfoViewModel.FromSqlInterpolated($"exec consumers.sp_GetPerspectiveDevelopmentFacilityGenInfoDataOne {dev_prog_id}, {consumer_id}").ToListAsync()).FirstOrDefault()
                ?? new PerspectiveDevelopmentFacility_GenInfoViewModel { dev_prog_id = dev_prog_id, consumer_id = consumer_id };

            var data_status = _m_c.GetCurrentDS();
            ViewBag.DistrictRegionList = await _context.fnt_GetDistrictRegionList().ToListAsync();
            ViewBag.ConsumerNumberAddressList = await _context.fnt_GetUnomConsumerAddressListByCharsForObjDevProg("", consumer_id, item.building_id ?? 0, dev_prog_id, data_status).ToListAsync();
            ViewBag.Buildings = await _context.fnt_GetBildingUnomList(data_status).ToListAsync();

            return View("PerspectiveDevelopmentFacilities_GenInfo_Partial", item);
        }
    }
}
