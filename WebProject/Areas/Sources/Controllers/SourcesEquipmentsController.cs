using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Controllers;
using WebProject.Areas.Sources.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.Sources.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("Sources")]
	public class SourcesEquipmentsController : ModuleBaseController
	{
		public SourcesEquipmentsController(ILogger<TariffZoneController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
			: base(logger, context, context2, httpContextAccessor, hostingEnvironment, m_c) { }

        public async Task<IActionResult> SourcesEquipmentsList()
        {
            int data_status = _m_c.GetCurrentDS();
            ViewBag.Tso = await _context.fnt_GetTSOName(data_status).ToListAsync();
            ViewBag.SourcesType = await _context.fnt_GetSourcesTypeList().ToListAsync();
            return View();
        }

        public async Task<IActionResult> Index()
        {
            int data_status = _m_c.GetCurrentDS();
            ViewBag.Tso = await _context.fnt_GetTSOName(data_status).ToListAsync();
            ViewBag.SourcesType = await _context.fnt_GetSourcesTypeList().ToListAsync();
            return View();
        }

        #region OpenPopups
        public async Task<IActionResult> OpenSourceEquipment(int id, int data_status, string action_for = "")
        {
            ViewBag.Source = await _context.fnt_GetSourcesUnomList(data_status).ToListAsync();
            SourcesOneDataViewModel sourcesOneData = new() { data_status = data_status, source_id = id };
            return PartialView("OpenSourcesEquipment", sourcesOneData);
        }
        #endregion

        #region ViewComponent
        public ActionResult OnGetSourcesEquipViewComponent(int data_status, int perspective_year, int tz, int status, int org, int type)
        {
            return ViewComponent("SourcesEquipList_Partial", new { userId, data_status, perspective_year, tz, status, org, type });
        }
        public ActionResult OnGetSourcesEquipTurbineViewComponent(int data_status, int perspective_year, int tz, int status, int org, int type)
		{
			return ViewComponent("SourcesEquipTurbineList_Partial", new { userId, data_status, perspective_year, tz, status, org, type });
		}
        public ActionResult OnGetSourcesEquipBoilerViewComponent(int data_status, int perspective_year, int tz, int status, int org, int type)
        {
            return ViewComponent("SourcesEquipBoilerList_Partial", new { userId, data_status, perspective_year, tz, status, org, type });
        }
        public ActionResult OnGetSourcesEquipPistonViewComponent(int data_status, int perspective_year, int tz, int status, int org, int type)
        {
            return ViewComponent("SourcesEquipPistonList_Partial", new { userId, data_status, perspective_year, tz, status, org, type });
        }
        public ActionResult OnGetSourcesEquipRouViewComponent(int data_status, int perspective_year, int tz, int status, int org, int type)
        {
            return ViewComponent("SourcesEquipRouList_Partial", new { userId, data_status, perspective_year, tz, status, org, type });
        }
        public ActionResult OnGetSourcesEquipHeaterViewComponent(int data_status, int perspective_year, int tz, int status, int org, int type)
        {
            return ViewComponent("SourcesEquipHeaterList_Partial", new { userId, data_status, perspective_year, tz, status, org, type });
        }
        public ActionResult OnGetSourcesEquipPumpViewComponent(int data_status, int perspective_year, int tz, int status, int org, int type)
        {
            return ViewComponent("SourcesEquipPumpList_Partial", new { userId, data_status, perspective_year, tz, status, org, type });
        }
        public ActionResult OnGetSourcesEquipSmokePipeViewComponent(int data_status, int perspective_year, int tz, int status, int org, int type)
        {
            return ViewComponent("SourcesEquipSmokePipeList_Partial", new { userId, data_status, perspective_year, tz, status, org, type });
        }
        #endregion
    }
}
