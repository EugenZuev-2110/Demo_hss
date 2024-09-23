using DataBase.Models.TSO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.TSO.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("TSO")]
	public class ObjectsInServiceController : Controller
	{
		private readonly ILogger<ObjectsInServiceController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;
		//private PrincipalContext _ctx = new PrincipalContext(ContextType.Domain);
		public ObjectsInServiceController(ILogger<ObjectsInServiceController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
		{
			_logger = logger;
			_context = context;
			_context2 = context2;
			_httpContextAccessor = httpContextAccessor;
			_user = _httpContextAccessor.HttpContext.User.Identity.Name;
			_hostingEnvironment = hostingEnvironment;
			_m_c = m_c;
			if (_user != null)
			{
				var user = _context2.DictWinUsers.Where(x => x.UserLogin == _user).FirstOrDefault();
				userDisplayName = user.UserName;
				userId = user.Id;
			}
			else
			{
				string host = _httpContextAccessor.HttpContext.Request.Host.Value;
				if (host.Contains("localhost"))
				{
					userId = 1;
					userDisplayName = "Ермошин Виктор Анатольевич";
				}
			}
		}
		public IActionResult TSOObjects()
		{
			return View();
		}

		#region OpenPopups
		//открытие попапа добавления или редактирования дополнительной информации по ТСО
		public IActionResult TSOAdditionalOpen(int id, int data_status)
		{
			var tso = new TSOAdditionalDataViewModel();
			try
			{
				if (id > 0)
				{
					if (_context.TSO_AdditionalValues.Any(x => x.tso_id == id && x.data_status <= data_status && x.is_deleted == false))
					{
                        tso = (from org in _context.Organisations
                               join tso_h in _context.TSO_History on org.org_id equals tso_h.org_id
                               join tso_ad in _context.TSO_AdditionalValues on org.org_id equals tso_ad.tso_id
                               where org.org_id == id && tso_h.is_deleted == false && tso_ad.is_deleted == false
                                   && tso_ad.data_status == (_context.TSO_AdditionalValues.Where(x => x.data_status <= data_status && x.tso_id == id).Max(x => x.data_status))
                                   && tso_h.data_status == (_context.TSO_History.Where(x => x.data_status <= data_status && x.org_id == id).Max(x => x.data_status))
                               select new TSOAdditionalDataViewModel
                               {
                                   tso_id = org.org_id,
                                   fixed_assets_cost_prod = tso_ad.fixed_assets_cost_prod,
                                   fixed_assets_wear = tso_ad.fixed_assets_wear,
                                   fixed_assets_cost_transfer = tso_ad.fixed_assets_cost_transfer,
                                   network_length = tso_ad.network_length,
                                   network_length_replaced = tso_ad.network_length_replaced,
                                   useful_supply_large_consumer = tso_ad.useful_supply_large_consumer,
                                   count_service_public = tso_ad.count_service_public,
                                   count_abonent = tso_ad.count_abonent
                               }).FirstOrDefault();
                    }
				}
				tso.tso_id = id;
				tso.data_status = data_status;

				ViewBag.TSOList = _context.fnt_GetOrgList(data_status, 1).ToList();
			}
			catch (Exception ex)
			{

			}
			return PartialView(tso);
		}

		//открытие попапа добавления или редактирования дополнительной информации по ТСО
		public IActionResult TZObjectsOpen(int id, int data_status, int perspective_year)
		{
			var tz = new TZObjectsDataViewModel();
			try
			{
				if (id > 0)
				{
					List<TZObjectsDataViewModel> tz_l = _context.TZObjectsDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZTSOObjectsDataOne {id},{data_status},{perspective_year},{userId}").ToList();
					if (tz_l.Count > 0)
					{
						tz = tz_l[0];
					}
					else
					{
						tz.tz_id = id;
						tz.data_status = data_status;
						tz.perspective_year = perspective_year;
					}
				}

				ViewBag.TZTerritoryList = _context.fnt_GetTZTerritoryList(id).ToList();
				ViewBag.TZTSOList = _context.fnt_GetTZTSOList(data_status, perspective_year, "all").ToList();
				ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZObjectsOpen", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(tz);
		}
		#endregion

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TSOAdditional_Save(TSOAdditionalDataViewModel model)
		{
			try
			{
				if (model.tso_id > 0)
				{
                    var tso_upd = await _context.TSO_AdditionalValues.Where(x => x.tso_id == model.tso_id && x.data_status == model.data_status).FirstOrDefaultAsync();

                    if (tso_upd != null)
                    {
                        tso_upd.fixed_assets_cost_prod = model.fixed_assets_cost_prod;
                        tso_upd.fixed_assets_wear = model.fixed_assets_wear;
                        tso_upd.fixed_assets_cost_transfer = model.fixed_assets_cost_transfer;
                        tso_upd.network_length = model.network_length;
                        tso_upd.network_length_replaced = model.network_length_replaced;
                        tso_upd.useful_supply_large_consumer = model.useful_supply_large_consumer;
                        tso_upd.count_service_public = model.count_service_public;
                        tso_upd.count_abonent = model.count_abonent;
                        tso_upd.edit_date = DateTime.Now;
                        tso_upd.user_id = userId;
                    }
                    else
                    {
                        var tso_additional_new = new TSO_AdditionalValues()
                        {
                            tso_id = model.tso_id,
                            data_status = model.data_status,
                            fixed_assets_cost_prod = model.fixed_assets_cost_prod,
                            fixed_assets_wear = model.fixed_assets_wear,
                            fixed_assets_cost_transfer = model.fixed_assets_cost_transfer,
                            network_length = model.network_length,
                            network_length_replaced = model.network_length_replaced,
                            useful_supply_large_consumer = model.useful_supply_large_consumer,
                            count_service_public = model.count_service_public,
                            count_abonent = model.count_abonent,
                            is_deleted = false,
                            create_date = DateTime.Now,
                            user_id = userId
                        };
                        await _context.TSO_AdditionalValues.AddAsync(tso_additional_new);
                    }
					await _context.SaveChangesAsync();
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false });
			}
		}

		#region ViewComponents
		//Объекты ТСО в обслуживании - сводные данные
		public ActionResult OnGetCallTSO_SumDataList_PartialViewComponent(int data_status, int perspective_year)
		{
			return ViewComponent("TSO_SumDataList_Partial", new { data_status, perspective_year, userId });
		}
		//Объекты ТСО в обслуживании - источники
		public ActionResult OnGetCallTSO_SourcesList_PartialViewComponent(int data_status, int perspective_year)
		{
			return ViewComponent("TSO_SourcesList_Partial", new { data_status, perspective_year, userId });
		}
		//Объекты ТСО в обслуживании - ТП
		public ActionResult OnGetCallTSO_HPList_PartialViewComponent(int data_status, int perspective_year)
		{
			return ViewComponent("TSO_HPList_Partial", new { data_status, perspective_year, userId });
		}
		//Объекты ТСО в обслуживании - дополнительные данные
		public ActionResult OnGetCallTSO_AdditionalDataList_PartialViewComponent(int data_status, int perspective_year)
		{
			return ViewComponent("TSO_AdditionalDataList_Partial", new { data_status, perspective_year, userId });
		}
		//Данные о ТЗ - перечень источников
		public ActionResult OnGetCallTZOne_SourcesList_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZOne_SourcesList_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Данные о ТЗ - участки сетей
		public ActionResult OnGetCallTZOne_NetworksList_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZOne_NetworksList_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Данные о ТЗ - перечень НПС
		public ActionResult OnGetCallTZOne_PSList_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZOne_PSList_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Данные о ТЗ - перечень ТП
		public ActionResult OnGetCallTZOne_HPList_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZOne_HPList_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		#endregion
	}
}
