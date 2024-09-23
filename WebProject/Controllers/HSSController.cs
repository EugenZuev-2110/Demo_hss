using DataBase.Models;
using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.TSO.Models;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Controllers
{
    //Общий контроллер
    public class HSSController : Controller
    {
        private readonly HssDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string? _dbName;
		private readonly string? _ds;
		private readonly string? _ami;
		private readonly string? _amсi;
		public HSSController(HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
			_httpContextAccessor = httpContextAccessor;
			_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("dbName", out _dbName);
			_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("data_status", out _ds);
			_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("activeMenuItem", out _ami);
			_httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("activeMenuChildItem", out _amсi);
		}

		public IActionResult Home()
		{
			if (_dbName == null)
			{
				_httpContextAccessor.HttpContext.Response.Cookies.Append("dbName", "HeatSupplyScheme");
			}

			return View();
		}

		public IActionResult _MainMenuHSS()
        {
            ViewBag.DataStatusesList = _context.DataStatuses.ToList();
            return PartialView();
        }

		[HttpPost]
		public JsonResult GetActiveCityDS()
		{
			var db_name = _dbName;
			var data_status = _ds;

			if (data_status == null)
			{
				data_status = GetCurrentDS().ToString();
			}

			if (_dbName == "HeatSupplyScheme") 
			{
				db_name = "msk";
			}
			else if (_dbName == "HeatSupplyScheme_Test")
			{
				db_name = "test";
			}

			return Json(new { db_name, data_status });
		}

        [HttpPost]
        public JsonResult GetActiveDS()
        {
            var data_status = _ds;

            if (data_status == null)
            {
                data_status = GetCurrentDS().ToString();
            }

            return Json(new { data_status });
        }

		[HttpGet]
		public JsonResult GetActiveMenuItem()
		{
			return Json(new { item_id = _ami, child_item_id = _amсi });
		}

		[HttpPost]
		public JsonResult SetActiveMenuItem(string parent_id, string menu_item_id)
		{
			_httpContextAccessor.HttpContext.Response.Cookies.Append("activeMenuItem", parent_id.ToString());
			_httpContextAccessor.HttpContext.Response.Cookies.Append("activeMenuChildItem", menu_item_id.ToString());

			return Json(new { });
		}

		[HttpPost]
		public JsonResult ChangeDbName(string db_name)
		{
			string newDbName = string.Empty;

			if (db_name == "msk")
				newDbName = "HeatSupplyScheme";
			else if (db_name == "test")
				newDbName = "HeatSupplyScheme_Test";

			_httpContextAccessor.HttpContext.Response.Cookies.Append("dbName", newDbName);

			return Json(new {  });
		}

		[HttpPost]
		public JsonResult ChangeDS(int data_status)
		{
			_httpContextAccessor.HttpContext.Response.Cookies.Append("data_status", data_status.ToString());

			return Json(new { });
		}

		public IActionResult GetPerspectiveYears(int data_status)
        {
            if (data_status == 0)
            {
                data_status = GetCurrentDS();
            }
            var years = _context.PerspectiveYears.Where(x => x.data_status == data_status).Select(x => new { x.perspective_year, x.perspective_year_dt }).ToList();
            return Json(new { years });
        }

		[NonAction]
		public List<PerspectiveYears> GetPerspectiveYearsList(int data_status)
		{
			if (data_status == 0)
			{
				data_status = GetCurrentDS();
			}
			List<PerspectiveYears> years = _context.PerspectiveYears.Where(x => x.data_status == data_status).ToList();
			return years;
		}

		public IActionResult GetSourcesListByTZ(int tz_id, int data_status)
		{
			List<TZSourcesViewModel> sources = _context.fnt_GetSourcesByTZList(data_status, tz_id).ToList();
			return Json(new { sources });
		}

		[NonAction]
        public int GetCurrentYearByDS(int data_status)
        {
            if (data_status == 0)
                data_status = GetCurrentDS();
            var current_year = _context.PerspectiveYears.Where(x => x.data_status == data_status).Select(x => x.perspective_year).FirstOrDefault();
            return current_year;
        }

		[HttpPost]
		public IActionResult GetUnomPPListByChars(string chars, int data_status)
		{
			List<Values_List> list = _context.fnt_GetUnomPPListByChars(chars ?? "", data_status).ToList();
			return Json(new { list });
		}

		[HttpPost]
		public IActionResult GetUnomTpAddressListByChars(string chars, int data_status)
		{
			List<Values_List> list = _context.fnt_GetUnomTpAddressListByChars(chars ?? "", data_status).ToList();
			return Json(new { list });
		}

		[HttpPost]
		public IActionResult GetConsumerNumberAddressListByChars(string chars, int data_status)
		{
			List<Values_List> list = _context.fnt_GetUnomConsumerAddressListByChars(chars ?? "", data_status).ToList();
			return Json(new { list });
		}

		[HttpPost]
		public IActionResult GetOrgOwnerInnListByChars(string chars, int data_status)
		{
			List<Values_List> list = _context.fnt_GetOrgOwnerInnListByChars(chars ?? "", data_status).ToList();
			return Json(new { list });
		}

		[HttpPost]
		public IActionResult GetHpDistricts()
		{
			List<Values_List> list = _context.fnt_GetHpDistricts().ToList();
			return Json(new { list });
		}

		[HttpPost]
		public IActionResult GetDevPrograms(int consumer_id)
		{
			List<Values_List> list = _context.fnt_GetDevProgramsList(consumer_id).ToList();
			return Json(new { list });
		}

		[HttpPost]
		public IActionResult GetUnomOutputs(int source_id)
		{
			List<Values_List> list = _context.fnt_GetUnomOutputsList(source_id).ToList();
			return Json(new { list });
		}

		[HttpPost]
		public IActionResult GetHPEquipmentByMark(int equip_id)
		{
			var item = _context.fnt_GetHP_EquipmentByMark(equip_id).FirstOrDefault();
			return Json(new { item });
		}

		[HttpPost]
		public IActionResult GetHPPumpByMark(int equip_id)
		{
			var item = _context.fnt_GetHP_PumpByMark(equip_id).FirstOrDefault();
			return Json(new { item });
		}

		[HttpPost]
		public async Task<IActionResult> GetHpDiamHtSupplyIdValue(int data_status,int heat_network_id)
		{
			var list = await _context.fnt_GetHpDiamHtSupplyIdList(data_status, heat_network_id).ToListAsync();
			return Json(new { list });
		}

        [HttpPost]
        public async Task<IActionResult> GetSourceUnomByUnomHp(int data_status, int heat_point_id)
        {
            var list = await _context.fnt_GetSourceUnomByUnomHpList(data_status, heat_point_id).ToListAsync();
            return Json(new { list });
        }

        [HttpPost]
		public IActionResult GetApplName(int consumer_id, int dev_prog_id)
		{
			IndividualFeeApplNameObject appl_obj;
			string appl_name = "Нет записей";
			decimal? individual_fee_one_hl_gvsm_hw = 0;
			try
			{
				appl_obj = _context.fnt_GetApplName(consumer_id, dev_prog_id).FirstOrDefault();
				if(appl_obj != null)
				{
					appl_name = appl_obj.appl_name;
					individual_fee_one_hl_gvsm_hw = appl_obj.individual_fee_one_hl_gvsm_hw;
				}
			}
			catch
			{ }

			return Json(new { appl_name, individual_fee_one_hl_gvsm_hw });
		}

		[NonAction]
        public int GetCurrentDS()
        {
			int data_status;
			bool success = int.TryParse(_ds, out data_status);
			if (success)
			{
				return data_status;
			}
			else
			{
				return data_status = _context.DataStatuses.Where(x => x.is_active == true).Select(x => x.data_status).FirstOrDefault();
			}
		}

		[HttpPost]
		public async Task<JsonResult> FindETOAddToList(string eto_num)
		{
            var eto = await _context.ETO.Where(x => x.unom_eto == eto_num).Select(x=> new { x.eto_id, x.unom_eto }).FirstOrDefaultAsync();

			return Json(new { eto });
		}

		[HttpPost]
		public IActionResult GetRegions()
		{
			List<Region_List> list = _context.fnt_GetRegionsList().ToList();
			return Json(new { list });
		}

		public async Task<IActionResult> GetExploitingOrgList(int data_status)
		{
			if (data_status == 0)
				data_status = GetCurrentDS();

			var list = await _context.fnt_GetTSOName(data_status).ToListAsync();
			list.Insert(0, new TSO_List { tso_id = -1, tso_name = "Все" });

			return Json(new { list });
		}

		public async Task<IActionResult> GetHeatEnergySourceList(int data_status)
		{
			if (data_status == 0)
				data_status = GetCurrentDS();

			var list = await _context.fnt_GetSourcesByTZList(data_status, -1).ToListAsync();
			list.Insert(0, new TZSourcesViewModel { value_id = -1, value_name = "Все" });

			return Json(new { list });
		}

		[HttpPost]
		public async Task<IActionResult> GetHpTypeLocations()
		{
			var list = await _context.fnt_GetHpTypeLocations().ToListAsync();
			list.Insert(0, new Values_List { value_id = -1, value_name = "Все" });
			return Json(new { list });
		}

		[HttpPost]
		public async Task<IActionResult> GetHpStatusesForFilter()
		{
			var list = await _context.fnt_GetHpStatusesForFilter().ToListAsync();
			return Json(new { list });
		}

		[HttpPost]
		public async Task<IActionResult> GetLoadTypesForFilter()
		{
			var list = await _context.Dict_LoadTypes.ToListAsync();
			return Json(new { list });
		}

		[HttpPost]
		public async Task<IActionResult> GetEquipmentMarkTypeList(int equip_id)
		{
			var item = await _context.Equipments.FirstOrDefaultAsync(n => n.equip_type_id == equip_id);
			return Json(new { item });
		}

		//Общее действие для выгрузки шаблонов
		public async Task<ActionResult> DownloadTemplate(int? template_id)
		{
			try
			{
				string host = _httpContextAccessor.HttpContext.Request.Host.Value;

				if (host.Contains("localhost"))
					host = "http://" + host;
				else
					host = "https://" + host;

				if (template_id > 0)
				{
					var template = await _context.UploadsTemplates.Where(x => x.Id == template_id).FirstOrDefaultAsync();

					return Json(new { success = true, filename = template.template_path + template.template_name });
				}
				else
				{
					return Json(new { success = false });
				}
			}
			catch (Exception ex)
			{
                return Json(new { success = false });
            }
		}

		public void ExLog_Save(string? method_name, string? parameters, string? exception, int? user_id)
		{
			try
			{
				var log = new EX_Logs()
				{
					method_name = method_name,
					parameters = parameters,
					exception = exception,
					create_date = DateTime.Now,
					user_id = user_id
				};
				_context.EX_Logs.Add(log);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{

			}
		}
	}
}
