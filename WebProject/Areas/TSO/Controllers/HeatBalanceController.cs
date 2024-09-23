using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
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
	public class HeatBalanceController : Controller
	{
		private readonly ILogger<HeatBalanceController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;
		//private PrincipalContext _ctx = new PrincipalContext(ContextType.Domain);
		public HeatBalanceController(ILogger<HeatBalanceController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		//Тепловой баланс
		public IActionResult TZHeatBalance()
		{
			return View();
		}

		#region OpenPopups
		//открытие попапа добавления или редактирования по балансу тепловой энергии (производство)
		public IActionResult OpenTZProduction(int id, int data_status, int perspective_year)
		{
			var tz = new TZOneProductionDataViewModel();
			try
			{
				if (id > 0)
				{
					List<TZOneProductionDataViewModel> tz_l = _context.TZOneProductionDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZProductionDataOne {id},{data_status},{perspective_year},{userId}").ToList();
					if (tz_l.Count > 0)
					{
						tz = tz_l[0];
					}
				}
				tz.tz_id = id;
				tz.data_status = data_status;
				tz.perspective_year = perspective_year;

				ViewBag.TZTSOList = _context.fnt_GetTZTSOList(data_status, perspective_year, "not_only_transfer").ToList();
				ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenTZProduction", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(tz);
		}

		//открытие попапа по исходящей тепловой энергии
		public IActionResult OpenTZOutputEnergy(int id, int data_status, int perspective_year)
		{
			var tz = new TZOutputEnergyDataViewModel();
			try
			{
				List<TZOutputEnergyDataViewModel> tz_l = _context.TZOutputEnergyDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOutputEnergyDataOne {id},{data_status},{perspective_year},{userId}").ToList();
				if (tz_l.Count > 0)
				{
					tz = tz_l[0];
				}
				ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenTZOutputEnergy", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(tz);
		}

		//получение информации по исходящей энергии в указанную зону источника 
		public IActionResult OpenTZOutputEnergySourceOne(int source_id, short buy_place_id, int input_tz_id, int output_tz_id, int data_status, int perspective_year)
		{
			var tz = new TZOutputEnergySourceOneDataViewModel();
			try
			{
				if (source_id > 0)
				{
					List<TZOutputEnergySourceOneDataViewModel> tz_l = _context.TZOutputEnergySourceOneDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOutputEnergySourceDataOne {source_id},{buy_place_id},{input_tz_id},{output_tz_id},{data_status},{perspective_year},{userId}").ToList();
					if (tz_l.Count > 0)
					{
						tz = tz_l[0];
						ViewBag.TZSourcesList = _context.fnt_GetSourcesByTZList(data_status, input_tz_id).ToList();
					}
				}
				else
				{
					tz.db_action = 0;
					tz.data_status = data_status;
					tz.output_tz_id = output_tz_id;
					tz.perspective_year = perspective_year;
				}
				
				ViewBag.TZTSOList = _context.fnt_GetTZTSOList(data_status, perspective_year, "not_only_transfer").ToList();
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenTZOutputEnergySourceOne", $"source_id={source_id},buy_place_id={buy_place_id},input_tz_id={input_tz_id},output_tz_id={output_tz_id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(tz);
		}

		//Получение остатка для распределения покупной энергии по источникам
		public IActionResult GetTZOutputEnergySourceOne(int source_id, short buy_place_id, int input_tz_id, int output_tz_id, int data_status, int perspective_year)
		{
			var tz = new TZOutputEnergySourceOneDataViewModel();
			try
			{
				List<TZOutputEnergySourceOneDataViewModel> tz_l = _context.TZOutputEnergySourceOneDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOutputEnergySourceDataOne {source_id},{buy_place_id},{input_tz_id},{output_tz_id},{data_status},{perspective_year},{userId}").ToList();
				if (tz_l.Count > 0)
				{
					tz = tz_l[0];
				}

				return Json(new { success = true, fact_1 = tz_l[0].buy_energy_fact_1, fact_2 = tz_l[0].buy_energy_fact_2, fact_3 = tz_l[0].buy_energy_fact_3,
					plan_1 = tz_l[0].buy_energy_plan_1, plan_2 = tz_l[0].buy_energy_plan_2, plan_3 = tz_l[0].buy_energy_plan_3
				});
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("GetTZOutputEnergySourceOne", $"source_id={source_id},buy_place_id={buy_place_id},input_tz_id={input_tz_id},output_tz_id={output_tz_id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}

		}

		//открытие попапа добавления или редактирования по балансу тепловой энергии (передача)
		public IActionResult OpenTZTransfer(int id, int data_status, int perspective_year)
		{
			var tz = new TZOneTransferDataViewModel();
			try
			{
				if (id > 0)
				{
					List<TZOneTransferDataViewModel> tz_l = _context.TZOneTransferDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZTransferDataOne {id},{data_status},{perspective_year},{userId}").ToList();
					if (tz_l.Count > 0)
					{
						tz = tz_l[0];
					}
				}
				tz.tz_id = id;
				tz.data_status = data_status;
				tz.perspective_year = perspective_year;

				ViewBag.TZTSOList = _context.fnt_GetTZTSOList(data_status, perspective_year, "only_transfer").ToList();
				ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenTZTransfer", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(tz);
		}

		//открытие попапа по транспортируемой тепловой энергии (передача)
		public IActionResult OpenTZOutputTransferEnergy(int id, int data_status, int perspective_year)
		{
			var tz = new TZOutputTransferEnergyDataViewModel();
			try
			{
				List<TZOutputTransferEnergyDataViewModel> tz_l = _context.TZOutputTransferEnergyDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOutputTransferEnergyDataOne {id},{data_status},{perspective_year},{userId}").ToList();
				if (tz_l.Count > 0)
				{
					tz = tz_l[0];
				}
				ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenTZOutputTransferEnergy", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(tz);
		}

		//получение информации по транспортируемой энергии в указанную зону источника 
		public IActionResult OpenTZOutputTransferEnergySourceOne(int source_id, int input_tz_id, int output_tz_id, int data_status, int perspective_year)
		{
			var tz = new TZOutputTransferEnergySourceOneDataViewModel();
			try
			{
				List<TZOutputTransferEnergySourceOneDataViewModel> tz_l = _context.TZOutputTransferEnergySourceOneDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOutputTransferEnergySourceDataOne {source_id},{input_tz_id},{output_tz_id},{data_status},{perspective_year},{userId}").ToList();
				if (tz_l.Count > 0)
				{
					tz = tz_l[0];
				}
				if (source_id > 0)
				{
					ViewBag.TZSourcesList = _context.fnt_GetSourcesByTZList(data_status, input_tz_id).ToList();
				}

				ViewBag.TZTSOList = _context.fnt_GetTZTSOList(data_status, perspective_year, "only_transfer").ToList();
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenTZOutputTransferEnergySourceOne", $"source_id={source_id},input_tz_id={input_tz_id},output_tz_id={output_tz_id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(tz);
		}

		#endregion

		#region SAVE_DATA
		//Сохранение теплового баланса (производство)
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZProduction_Save(TZOneProductionDataViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					var tz_upd_1 = await _context.TZ_BalanceHeatEnergy_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status 
										&& x.report_period_id == 1).FirstOrDefaultAsync();
					if (tz_upd_1 != null)
					{
						tz_upd_1.generate_heat_energy_fact = model.generate_heat_energy_fact_1;
						tz_upd_1.heat_consump_fact = model.heat_consump_fact_1;
						tz_upd_1.release_collect_ownprod_fact = model.release_collect_ownprod_fact_1;
						tz_upd_1.release_collect_household_needs_fact = model.release_collect_household_needs_fact_1;
						tz_upd_1.release_collect_tso_salers_fact = model.release_collect_tso_salers_fact_1;
						tz_upd_1.release_collect_buget_cons_fact = model.release_collect_buget_cons_fact_1;
						tz_upd_1.release_collect_public_fact = model.release_collect_public_fact_1;
						tz_upd_1.release_collect_other_cons_fact = model.release_collect_other_cons_fact_1;
						tz_upd_1.release_collect_ownheatnet_fact = model.release_collect_ownheatnet_fact_1;
						tz_upd_1.heatenrg_loss_heatnet_fact = model.heatenrg_loss_heatnet_fact_1;
						tz_upd_1.useful_release_ownprod_fact = model.useful_release_ownprod_fact_1;
						tz_upd_1.useful_release_household_needs_fact = model.useful_release_household_needs_fact_1;
						tz_upd_1.useful_release_tso_salers_fact = model.useful_release_tso_salers_fact_1;
						tz_upd_1.useful_release_buget_cons_fact = model.useful_release_buget_cons_fact_1;
						tz_upd_1.useful_release_public_fact = model.useful_release_public_fact_1;
						tz_upd_1.useful_release_other_cons_fact = model.useful_release_other_cons_fact_1;
						tz_upd_1.edit_date = DateTime.Now;
						tz_upd_1.user_id = userId;
					}
					else
					{
						var new_tz_bal = GetNewTZBalanceFactModel(model.tz_id, model.data_status, 1, model.generate_heat_energy_fact_1, model.heat_consump_fact_1, 
							model.release_collect_ownprod_fact_1, model.release_collect_household_needs_fact_1, model.release_collect_tso_salers_fact_1,
							model.release_collect_buget_cons_fact_1, model.release_collect_public_fact_1, model.release_collect_other_cons_fact_1, 
							model.release_collect_ownheatnet_fact_1, model.heatenrg_loss_heatnet_fact_1, model.useful_release_ownprod_fact_1, 
							model.useful_release_household_needs_fact_1, model.useful_release_tso_salers_fact_1, model.useful_release_buget_cons_fact_1,
							model.useful_release_public_fact_1, model.useful_release_other_cons_fact_1, null, false);
						await _context.TZ_BalanceHeatEnergy_Fact.AddAsync(new_tz_bal);
					}

					var tz_upd_2 = await _context.TZ_BalanceHeatEnergy_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status 
										&& x.report_period_id == 2).FirstOrDefaultAsync();
					if (tz_upd_2 != null)
					{
						tz_upd_2.generate_heat_energy_fact = model.generate_heat_energy_fact_2;
						tz_upd_2.heat_consump_fact = model.heat_consump_fact_2;
						tz_upd_2.release_collect_ownprod_fact = model.release_collect_ownprod_fact_2;
						tz_upd_2.release_collect_household_needs_fact = model.release_collect_household_needs_fact_2;
						tz_upd_2.release_collect_tso_salers_fact = model.release_collect_tso_salers_fact_2;
						tz_upd_2.release_collect_buget_cons_fact = model.release_collect_buget_cons_fact_2;
						tz_upd_2.release_collect_public_fact = model.release_collect_public_fact_2;
						tz_upd_2.release_collect_other_cons_fact = model.release_collect_other_cons_fact_2;
						tz_upd_2.release_collect_ownheatnet_fact = model.release_collect_ownheatnet_fact_2;
						tz_upd_2.heatenrg_loss_heatnet_fact = model.heatenrg_loss_heatnet_fact_2;
						tz_upd_2.useful_release_ownprod_fact = model.useful_release_ownprod_fact_2;
						tz_upd_2.useful_release_household_needs_fact = model.useful_release_household_needs_fact_2;
						tz_upd_2.useful_release_tso_salers_fact = model.useful_release_tso_salers_fact_2;
						tz_upd_2.useful_release_buget_cons_fact = model.useful_release_buget_cons_fact_2;
						tz_upd_2.useful_release_public_fact = model.useful_release_public_fact_2;
						tz_upd_2.useful_release_other_cons_fact = model.useful_release_other_cons_fact_2;
						tz_upd_2.edit_date = DateTime.Now;
						tz_upd_2.user_id = userId;
					}
					else
					{
						var new_tz_bal = GetNewTZBalanceFactModel(model.tz_id, model.data_status, 2, model.generate_heat_energy_fact_2, model.heat_consump_fact_2,
							model.release_collect_ownprod_fact_2, model.release_collect_household_needs_fact_2, model.release_collect_tso_salers_fact_2,
							model.release_collect_buget_cons_fact_2, model.release_collect_public_fact_2, model.release_collect_other_cons_fact_2,
							model.release_collect_ownheatnet_fact_2, model.heatenrg_loss_heatnet_fact_2, model.useful_release_ownprod_fact_2,
							model.useful_release_household_needs_fact_2, model.useful_release_tso_salers_fact_2, model.useful_release_buget_cons_fact_2,
							model.useful_release_public_fact_2, model.useful_release_other_cons_fact_2, null, false);
						await _context.TZ_BalanceHeatEnergy_Fact.AddAsync(new_tz_bal);
					}

					var tz_upd_3 = await _context.TZ_BalanceHeatEnergy_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
										&& x.report_period_id == 3).FirstOrDefaultAsync();
					if (tz_upd_3 != null)
					{
						tz_upd_3.generate_heat_energy_fact = (model.generate_heat_energy_fact_1 ?? 0) + (model.generate_heat_energy_fact_2 ?? 0);
						tz_upd_3.heat_consump_fact = (model.heat_consump_fact_1 ?? 0) + (model.heat_consump_fact_2 ?? 0);
						tz_upd_3.release_collect_ownprod_fact = (model.release_collect_ownprod_fact_1 ?? 0) + (model.release_collect_ownprod_fact_2 ?? 0);
						tz_upd_3.release_collect_household_needs_fact = (model.release_collect_household_needs_fact_1 ?? 0) + (model.release_collect_household_needs_fact_2 ?? 0);
						tz_upd_3.release_collect_tso_salers_fact = (model.release_collect_tso_salers_fact_1 ?? 0) + (model.release_collect_tso_salers_fact_2 ?? 0);
						tz_upd_3.release_collect_buget_cons_fact = (model.release_collect_buget_cons_fact_1 ?? 0) + (model.release_collect_buget_cons_fact_2 ?? 0);
						tz_upd_3.release_collect_public_fact = (model.release_collect_public_fact_1 ?? 0) + (model.release_collect_public_fact_2 ?? 0);
						tz_upd_3.release_collect_other_cons_fact = (model.release_collect_other_cons_fact_1 ?? 0) + (model.release_collect_other_cons_fact_2 ?? 0);
						tz_upd_3.release_collect_ownheatnet_fact = (model.release_collect_ownheatnet_fact_1 ?? 0) + (model.release_collect_ownheatnet_fact_2 ?? 0);
						tz_upd_3.heatenrg_loss_heatnet_fact = (model.heatenrg_loss_heatnet_fact_1 ?? 0) + (model.heatenrg_loss_heatnet_fact_2 ?? 0);
						tz_upd_3.useful_release_ownprod_fact = (model.useful_release_ownprod_fact_1 ?? 0) + (model.useful_release_ownprod_fact_2 ?? 0);
						tz_upd_3.useful_release_household_needs_fact = (model.useful_release_household_needs_fact_1 ?? 0) + (model.useful_release_household_needs_fact_2 ?? 0);
						tz_upd_3.useful_release_tso_salers_fact = (model.useful_release_tso_salers_fact_1 ?? 0) + (model.useful_release_tso_salers_fact_2 ?? 0);
						tz_upd_3.useful_release_buget_cons_fact = (model.useful_release_buget_cons_fact_1 ?? 0) + (model.useful_release_buget_cons_fact_2 ?? 0);
						tz_upd_3.useful_release_public_fact = (model.useful_release_public_fact_1 ?? 0) + (model.useful_release_public_fact_2 ?? 0);
						tz_upd_3.useful_release_other_cons_fact = (model.useful_release_other_cons_fact_1 ?? 0) + (model.useful_release_other_cons_fact_2 ?? 0);
						tz_upd_3.notes_fact = model.notes_fact;
						tz_upd_3.edit_date = DateTime.Now;
						tz_upd_3.user_id = userId;
					}
					else
					{
						var new_tz_bal = GetNewTZBalanceFactModel(model.tz_id, model.data_status, 3, 
							(model.generate_heat_energy_fact_1 ?? 0) + (model.generate_heat_energy_fact_2 ?? 0),
							(model.heat_consump_fact_1 ?? 0) + (model.heat_consump_fact_2 ?? 0), 
							(model.release_collect_ownprod_fact_1 ?? 0) + (model.release_collect_ownprod_fact_2 ?? 0),
							(model.release_collect_household_needs_fact_2 ?? 0) + (model.release_collect_household_needs_fact_2 ?? 0),
							(model.release_collect_tso_salers_fact_1 ?? 0) + (model.release_collect_tso_salers_fact_2 ?? 0),
							(model.release_collect_buget_cons_fact_1 ?? 0) + (model.release_collect_buget_cons_fact_2 ?? 0),
							(model.release_collect_public_fact_1 ?? 0) + (model.release_collect_public_fact_2 ?? 0),
							(model.release_collect_other_cons_fact_1 ?? 0) + (model.release_collect_other_cons_fact_2 ?? 0),
							(model.release_collect_ownheatnet_fact_1 ?? 0) + (model.release_collect_ownheatnet_fact_2 ?? 0),
							(model.heatenrg_loss_heatnet_fact_1 ?? 0) + (model.heatenrg_loss_heatnet_fact_2 ?? 0),
							(model.useful_release_ownprod_fact_1 ?? 0) + (model.useful_release_ownprod_fact_2 ?? 0),
							(model.useful_release_household_needs_fact_1 ?? 0) + (model.useful_release_household_needs_fact_2 ?? 0),
							(model.useful_release_tso_salers_fact_1 ?? 0) + (model.useful_release_tso_salers_fact_2 ?? 0),
							(model.useful_release_buget_cons_fact_1 ?? 0) + (model.useful_release_buget_cons_fact_2 ?? 0),
							(model.useful_release_public_fact_1 ?? 0) + (model.useful_release_public_fact_2 ?? 0),
							(model.useful_release_other_cons_fact_1 ?? 0) + (model.useful_release_other_cons_fact_2 ?? 0), model.notes_fact, false);
						await _context.TZ_BalanceHeatEnergy_Fact.AddAsync(new_tz_bal);
					}

					var tz_upd_p_1 = await _context.TZ_BalanceHeatEnergy_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
										&& x.perspective_year == model.perspective_year	&& x.report_period_id == 1).FirstOrDefaultAsync();

					if (tz_upd_p_1 != null)
					{
						tz_upd_p_1.generate_heat_energy_plan = model.generate_heat_energy_plan_1;
						tz_upd_p_1.heat_consump_plan = model.heat_consump_plan_1;
						tz_upd_p_1.release_collect_ownprod_plan = model.release_collect_ownprod_plan_1;
						tz_upd_p_1.release_collect_household_needs_plan = model.release_collect_household_needs_plan_1;
						tz_upd_p_1.release_collect_tso_salers_plan = model.release_collect_tso_salers_plan_1;
						tz_upd_p_1.release_collect_buget_cons_plan = model.release_collect_buget_cons_plan_1;
						tz_upd_p_1.release_collect_public_plan = model.release_collect_public_plan_1;
						tz_upd_p_1.release_collect_other_cons_plan = model.release_collect_other_cons_plan_1;
						tz_upd_p_1.release_collect_ownheatnet_plan = model.release_collect_ownheatnet_plan_1;
						tz_upd_p_1.heatenrg_loss_heatnet_plan = model.heatenrg_loss_heatnet_plan_1;
						tz_upd_p_1.useful_release_ownprod_plan = model.useful_release_ownprod_plan_1;
						tz_upd_p_1.useful_release_household_needs_plan = model.useful_release_household_needs_plan_1;
						tz_upd_p_1.useful_release_tso_salers_plan = model.useful_release_tso_salers_plan_1;
						tz_upd_p_1.useful_release_buget_cons_plan = model.useful_release_buget_cons_plan_1;
						tz_upd_p_1.useful_release_public_plan = model.useful_release_public_plan_1;
						tz_upd_p_1.useful_release_other_cons_plan = model.useful_release_other_cons_plan_1;
						tz_upd_p_1.edit_date = DateTime.Now;
						tz_upd_p_1.user_id = userId;
					}
					else
					{
						var new_tz_bal = GetNewTZBalancePlanModel(model.tz_id, model.data_status, model.perspective_year, 1, model.generate_heat_energy_plan_1, 
							model.heat_consump_plan_1, model.release_collect_ownprod_plan_1, model.release_collect_household_needs_plan_1, model.release_collect_tso_salers_plan_1,
							model.release_collect_buget_cons_plan_1, model.release_collect_public_plan_1, model.release_collect_other_cons_plan_1,
							model.release_collect_ownheatnet_plan_1, model.heatenrg_loss_heatnet_plan_1, model.useful_release_ownprod_plan_1,
							model.useful_release_household_needs_plan_1, model.useful_release_tso_salers_plan_1, model.useful_release_buget_cons_plan_1,
							model.useful_release_public_plan_1, model.useful_release_other_cons_plan_1, null);
						await _context.TZ_BalanceHeatEnergy_Plan.AddAsync(new_tz_bal);
					}

					var tz_upd_p_2 = await _context.TZ_BalanceHeatEnergy_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
										&& x.perspective_year == model.perspective_year && x.report_period_id == 2).FirstOrDefaultAsync();

					if (tz_upd_p_2 != null)
					{
						tz_upd_p_2.generate_heat_energy_plan = model.generate_heat_energy_plan_2;
						tz_upd_p_2.heat_consump_plan = model.heat_consump_plan_2;
						tz_upd_p_2.release_collect_ownprod_plan = model.release_collect_ownprod_plan_2;
						tz_upd_p_2.release_collect_household_needs_plan = model.release_collect_household_needs_plan_2;
						tz_upd_p_2.release_collect_tso_salers_plan = model.release_collect_tso_salers_plan_2;
						tz_upd_p_2.release_collect_buget_cons_plan = model.release_collect_buget_cons_plan_2;
						tz_upd_p_2.release_collect_public_plan = model.release_collect_public_plan_2;
						tz_upd_p_2.release_collect_other_cons_plan = model.release_collect_other_cons_plan_2;
						tz_upd_p_2.release_collect_ownheatnet_plan = model.release_collect_ownheatnet_plan_2;
						tz_upd_p_2.heatenrg_loss_heatnet_plan = model.heatenrg_loss_heatnet_plan_2;
						tz_upd_p_2.useful_release_ownprod_plan = model.useful_release_ownprod_plan_2;
						tz_upd_p_2.useful_release_household_needs_plan = model.useful_release_household_needs_plan_2;
						tz_upd_p_2.useful_release_tso_salers_plan = model.useful_release_tso_salers_plan_2;
						tz_upd_p_2.useful_release_buget_cons_plan = model.useful_release_buget_cons_plan_2;
						tz_upd_p_2.useful_release_public_plan = model.useful_release_public_plan_2;
						tz_upd_p_2.useful_release_other_cons_plan = model.useful_release_other_cons_plan_2;
						tz_upd_p_2.edit_date = DateTime.Now;
						tz_upd_p_2.user_id = userId;
					}
					else
					{
						var new_tz_bal = GetNewTZBalancePlanModel(model.tz_id, model.data_status, model.perspective_year, 2, model.generate_heat_energy_plan_2,
							model.heat_consump_plan_2, model.release_collect_ownprod_plan_2, model.release_collect_household_needs_plan_2, model.release_collect_tso_salers_plan_2,
							model.release_collect_buget_cons_plan_2, model.release_collect_public_plan_2, model.release_collect_other_cons_plan_2,
							model.release_collect_ownheatnet_plan_2, model.heatenrg_loss_heatnet_plan_2, model.useful_release_ownprod_plan_2,
							model.useful_release_household_needs_plan_2, model.useful_release_tso_salers_plan_2, model.useful_release_buget_cons_plan_2,
							model.useful_release_public_plan_2, model.useful_release_other_cons_plan_2, null);
						await _context.TZ_BalanceHeatEnergy_Plan.AddAsync(new_tz_bal);
					}

					var tz_upd_p_3 = await _context.TZ_BalanceHeatEnergy_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
										&& x.perspective_year == model.perspective_year && x.report_period_id == 3).FirstOrDefaultAsync();

					if (tz_upd_p_3 != null)
					{
						tz_upd_p_3.generate_heat_energy_plan = (model.generate_heat_energy_plan_1 ?? 0) + (model.generate_heat_energy_plan_2 ?? 0);
						tz_upd_p_3.heat_consump_plan = (model.heat_consump_plan_1 ?? 0) + (model.heat_consump_plan_2 ?? 0);
						tz_upd_p_3.release_collect_ownprod_plan = (model.release_collect_ownprod_plan_1 ?? 0) + (model.release_collect_ownprod_plan_2 ?? 0);
						tz_upd_p_3.release_collect_household_needs_plan = (model.release_collect_household_needs_plan_1 ?? 0) + (model.release_collect_household_needs_plan_2 ?? 0);
						tz_upd_p_3.release_collect_tso_salers_plan = (model.release_collect_tso_salers_plan_1 ?? 0) + (model.release_collect_tso_salers_plan_2 ?? 0);
						tz_upd_p_3.release_collect_buget_cons_plan = (model.release_collect_buget_cons_plan_1 ?? 0) + (model.release_collect_buget_cons_plan_2 ?? 0);
						tz_upd_p_3.release_collect_public_plan = (model.release_collect_public_plan_1 ?? 0) + (model.release_collect_public_plan_2 ?? 0);
						tz_upd_p_3.release_collect_other_cons_plan = (model.release_collect_other_cons_plan_1 ?? 0) + (model.release_collect_other_cons_plan_2 ?? 0);
						tz_upd_p_3.release_collect_ownheatnet_plan = (model.release_collect_ownheatnet_plan_1 ?? 0) + (model.release_collect_ownheatnet_plan_2 ?? 0);
						tz_upd_p_3.heatenrg_loss_heatnet_plan = (model.heatenrg_loss_heatnet_plan_1 ?? 0) + (model.heatenrg_loss_heatnet_plan_2 ?? 0);
						tz_upd_p_3.useful_release_ownprod_plan = (model.useful_release_ownprod_plan_1 ?? 0) + (model.useful_release_ownprod_plan_2 ?? 0);
						tz_upd_p_3.useful_release_household_needs_plan = (model.useful_release_household_needs_plan_1 ?? 0) + (model.useful_release_household_needs_plan_2 ?? 0);
						tz_upd_p_3.useful_release_tso_salers_plan = (model.useful_release_tso_salers_plan_1 ?? 0) + (model.useful_release_tso_salers_plan_2 ?? 0);
						tz_upd_p_3.useful_release_buget_cons_plan = (model.useful_release_buget_cons_plan_1 ?? 0) + (model.useful_release_buget_cons_plan_2 ?? 0);
						tz_upd_p_3.useful_release_public_plan = (model.useful_release_public_plan_1 ?? 0) + (model.useful_release_public_plan_2 ?? 0);
						tz_upd_p_3.useful_release_other_cons_plan = (model.useful_release_other_cons_plan_1 ?? 0) + (model.useful_release_other_cons_plan_2 ?? 0);
						tz_upd_p_3.notes_plan = model.notes_plan;
						tz_upd_p_3.edit_date = DateTime.Now;
						tz_upd_p_3.user_id = userId;
					}
					else
					{
						var new_tz_bal = GetNewTZBalancePlanModel(model.tz_id, model.data_status, model.perspective_year, 3,
							(model.generate_heat_energy_plan_1 ?? 0) + (model.generate_heat_energy_plan_2 ?? 0),
							(model.heat_consump_plan_1 ?? 0) + (model.heat_consump_plan_2 ?? 0),
							(model.release_collect_ownprod_plan_1 ?? 0) + (model.release_collect_ownprod_plan_2 ?? 0),
							(model.release_collect_household_needs_plan_2 ?? 0) + (model.release_collect_household_needs_plan_2 ?? 0),
							(model.release_collect_tso_salers_plan_1 ?? 0) + (model.release_collect_tso_salers_plan_2 ?? 0),
							(model.release_collect_buget_cons_plan_1 ?? 0) + (model.release_collect_buget_cons_plan_2 ?? 0),
							(model.release_collect_public_plan_1 ?? 0) + (model.release_collect_public_plan_2 ?? 0),
							(model.release_collect_other_cons_plan_1 ?? 0) + (model.release_collect_other_cons_plan_2 ?? 0),
							(model.release_collect_ownheatnet_plan_1 ?? 0) + (model.release_collect_ownheatnet_plan_2 ?? 0),
							(model.heatenrg_loss_heatnet_plan_1 ?? 0) + (model.heatenrg_loss_heatnet_plan_2 ?? 0),
							(model.useful_release_ownprod_plan_1 ?? 0) + (model.useful_release_ownprod_plan_2 ?? 0),
							(model.useful_release_household_needs_plan_1 ?? 0) + (model.useful_release_household_needs_plan_2 ?? 0),
							(model.useful_release_tso_salers_plan_1 ?? 0) + (model.useful_release_tso_salers_plan_2 ?? 0),
							(model.useful_release_buget_cons_plan_1 ?? 0) + (model.useful_release_buget_cons_plan_2 ?? 0),
							(model.useful_release_public_plan_1 ?? 0) + (model.useful_release_public_plan_2 ?? 0),
							(model.useful_release_other_cons_plan_1 ?? 0) + (model.useful_release_other_cons_plan_2 ?? 0), model.notes_plan);
						await _context.TZ_BalanceHeatEnergy_Plan.AddAsync(new_tz_bal);
					}

					await _context.SaveChangesAsync();
				}
				

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZProduction_Save", $"id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}


		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZOutputEnergySource_Save(TZOutputEnergySourceOneDataViewModel model)
		{
			try
			{
				if (model.input_tz_id > 0 && model.output_tz_id > 0 && model.source_id > 0 && model.buy_place_id > 0)
				{
					var tz_upd_1 = await _context.TZ_InOutBuyEnergy_Fact.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
										&& x.data_status == model.data_status && x.source_id == model.old_source_id && x.buy_place_id == model.old_buy_place_id
										&& x.report_period_id == 1).FirstOrDefaultAsync();

					var tz_upd_2 = await _context.TZ_InOutBuyEnergy_Fact.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
									&& x.data_status == model.data_status && x.source_id == model.old_source_id && x.buy_place_id == model.old_buy_place_id
									&& x.report_period_id == 2).FirstOrDefaultAsync();

					var tz_upd_3 = await _context.TZ_InOutBuyEnergy_Fact.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
									&& x.data_status == model.data_status && x.source_id == model.old_source_id && x.buy_place_id == model.old_buy_place_id
									&& x.report_period_id == 3).FirstOrDefaultAsync();

					if (tz_upd_3 != null)
					{
						if (model.input_tz_id != model.old_input_tz_id || model.source_id != model.old_source_id || model.buy_place_id != model.old_buy_place_id)
						{
							_context.TZ_InOutBuyEnergy_Fact.Remove(tz_upd_1);
							_context.TZ_InOutBuyEnergy_Fact.Remove(tz_upd_2);
							_context.TZ_InOutBuyEnergy_Fact.Remove(tz_upd_3);
						}
						else
						{
							tz_upd_1.buy_energy_houseneed_fact = model.buy_energy_houseneed_fact_1;
							tz_upd_1.buy_energy_techneed_fact = model.buy_energy_techneed_fact_1;
							tz_upd_1.buy_loss_houseneed_fact = model.buy_loss_houseneed_fact_1;
							tz_upd_1.buy_loss_techneed_fact = model.buy_loss_techneed_fact_1;
							tz_upd_1.edit_date = DateTime.Now;
							tz_upd_1.user_id = userId;

							tz_upd_2.buy_energy_houseneed_fact = model.buy_energy_houseneed_fact_2;
							tz_upd_2.buy_energy_techneed_fact = model.buy_energy_techneed_fact_2;
							tz_upd_2.buy_loss_houseneed_fact = model.buy_loss_houseneed_fact_2;
							tz_upd_2.buy_loss_techneed_fact = model.buy_loss_techneed_fact_2;
							tz_upd_2.edit_date = DateTime.Now;
							tz_upd_2.user_id = userId;

							tz_upd_3.buy_energy_houseneed_fact = (model.buy_energy_houseneed_fact_1 ?? 0) + (model.buy_energy_houseneed_fact_2 ?? 0);
							tz_upd_3.buy_energy_techneed_fact = (model.buy_energy_techneed_fact_1 ?? 0) + (model.buy_energy_techneed_fact_2 ?? 0);
							tz_upd_3.buy_loss_houseneed_fact = (model.buy_loss_houseneed_fact_1 ?? 0) + (model.buy_loss_houseneed_fact_2 ?? 0);
							tz_upd_3.buy_loss_techneed_fact = (model.buy_loss_techneed_fact_1 ?? 0) + (model.buy_loss_techneed_fact_2 ?? 0);
							tz_upd_3.edit_date = DateTime.Now;
							tz_upd_3.user_id = userId;
						}
						
					}
					if (model.input_tz_id != model.old_input_tz_id || model.source_id != model.old_source_id || model.buy_place_id != model.old_buy_place_id)
					{
						var new_tz_bal_f1 = GetNewTZInOutBuyEnergyFactModel(model.input_tz_id, model.output_tz_id, model.source_id, model.buy_place_id,
							model.data_status, 1, model.buy_energy_houseneed_fact_1, model.buy_energy_techneed_fact_1, model.buy_loss_houseneed_fact_1,
							model.buy_loss_techneed_fact_1, false);
						await _context.TZ_InOutBuyEnergy_Fact.AddAsync(new_tz_bal_f1);

						var new_tz_bal_f2 = GetNewTZInOutBuyEnergyFactModel(model.input_tz_id, model.output_tz_id, model.source_id, model.buy_place_id,
							model.data_status, 2, model.buy_energy_houseneed_fact_2, model.buy_energy_techneed_fact_2, model.buy_loss_houseneed_fact_2,
							model.buy_loss_techneed_fact_2, false);
						await _context.TZ_InOutBuyEnergy_Fact.AddAsync(new_tz_bal_f2);

						var new_tz_bal_f3 = GetNewTZInOutBuyEnergyFactModel(model.input_tz_id, model.output_tz_id, model.source_id, model.buy_place_id,
							model.data_status, 3, (model.buy_energy_houseneed_fact_1 ?? 0) + (model.buy_energy_houseneed_fact_2 ?? 0),
							(model.buy_energy_techneed_fact_1 ?? 0) + (model.buy_energy_techneed_fact_2 ?? 0),
							(model.buy_loss_houseneed_fact_1 ?? 0) + (model.buy_loss_houseneed_fact_2 ?? 0),
							(model.buy_loss_techneed_fact_1 ?? 0) + (model.buy_loss_techneed_fact_2 ?? 0), false);
						await _context.TZ_InOutBuyEnergy_Fact.AddAsync(new_tz_bal_f3);
					}

					var tz_upd_p_1 = await _context.TZ_InOutBuyEnergy_Plan.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
										&& x.data_status == model.data_status && x.perspective_year == model.perspective_year && x.source_id == model.old_source_id
										&& x.buy_place_id == model.old_buy_place_id && x.report_period_id == 1).FirstOrDefaultAsync();

					var tz_upd_p_2 = await _context.TZ_InOutBuyEnergy_Plan.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
									&& x.data_status == model.data_status && x.perspective_year == model.perspective_year && x.source_id == model.old_source_id
									&& x.buy_place_id == model.old_buy_place_id && x.report_period_id == 2).FirstOrDefaultAsync();

					var tz_upd_p_3 = await _context.TZ_InOutBuyEnergy_Plan.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
									&& x.data_status == model.data_status && x.perspective_year == model.perspective_year && x.source_id == model.old_source_id
									&& x.buy_place_id == model.old_buy_place_id && x.report_period_id == 3).FirstOrDefaultAsync();

					if (tz_upd_p_3 != null)
					{
						if (model.input_tz_id != model.old_input_tz_id || model.source_id != model.old_source_id || model.buy_place_id != model.old_buy_place_id)
						{
							_context.TZ_InOutBuyEnergy_Plan.Remove(tz_upd_p_1);
							_context.TZ_InOutBuyEnergy_Plan.Remove(tz_upd_p_2);
							_context.TZ_InOutBuyEnergy_Plan.Remove(tz_upd_p_3);
						}
						else
						{
							tz_upd_p_1.buy_energy_houseneed_plan = model.buy_energy_houseneed_plan_1;
							tz_upd_p_1.buy_energy_techneed_plan = model.buy_energy_techneed_plan_1;
							tz_upd_p_1.buy_loss_houseneed_plan = model.buy_loss_houseneed_plan_1;
							tz_upd_p_1.buy_loss_techneed_plan = model.buy_loss_techneed_plan_1;
							tz_upd_p_1.edit_date = DateTime.Now;
							tz_upd_p_1.user_id = userId;

							tz_upd_p_2.buy_energy_houseneed_plan = model.buy_energy_houseneed_plan_2;
							tz_upd_p_2.buy_energy_techneed_plan = model.buy_energy_techneed_plan_2;
							tz_upd_p_2.buy_loss_houseneed_plan = model.buy_loss_houseneed_plan_2;
							tz_upd_p_2.buy_loss_techneed_plan = model.buy_loss_techneed_plan_2;
							tz_upd_p_2.edit_date = DateTime.Now;
							tz_upd_p_2.user_id = userId;

							tz_upd_p_3.buy_energy_houseneed_plan = (model.buy_energy_houseneed_plan_1 ?? 0) + (model.buy_energy_houseneed_plan_2 ?? 0);
							tz_upd_p_3.buy_energy_techneed_plan = (model.buy_energy_techneed_plan_1 ?? 0) + (model.buy_energy_techneed_plan_2 ?? 0);
							tz_upd_p_3.buy_loss_houseneed_plan = (model.buy_loss_houseneed_plan_1 ?? 0) + (model.buy_loss_houseneed_plan_2 ?? 0);
							tz_upd_p_3.buy_loss_techneed_plan = (model.buy_loss_techneed_plan_1 ?? 0) + (model.buy_loss_techneed_plan_2 ?? 0);
							tz_upd_p_3.edit_date = DateTime.Now;
							tz_upd_p_3.user_id = userId;
						}
					}

					if (tz_upd_p_3 == null || model.input_tz_id != model.old_input_tz_id || model.source_id != model.old_source_id || model.buy_place_id != model.old_buy_place_id)
					{
						var new_tz_bal_p_1 = GetNewTZInOutBuyEnergyPlanModel(model.input_tz_id, model.output_tz_id, model.source_id, model.buy_place_id,
							model.data_status, model.perspective_year, 1, model.buy_energy_houseneed_plan_1, model.buy_energy_techneed_plan_1,
							model.buy_loss_houseneed_plan_1, model.buy_loss_techneed_plan_1);
						await _context.TZ_InOutBuyEnergy_Plan.AddAsync(new_tz_bal_p_1);

						var new_tz_bal_p_2 = GetNewTZInOutBuyEnergyPlanModel(model.input_tz_id, model.output_tz_id, model.source_id, model.buy_place_id,
							model.data_status, model.perspective_year, 2, model.buy_energy_houseneed_plan_2, model.buy_energy_techneed_plan_2,
							model.buy_loss_houseneed_plan_2, model.buy_loss_techneed_plan_2);
						await _context.TZ_InOutBuyEnergy_Plan.AddAsync(new_tz_bal_p_2);

						var new_tz_bal_p_3 = GetNewTZInOutBuyEnergyPlanModel(model.input_tz_id, model.output_tz_id, model.source_id, model.buy_place_id,
							model.data_status, model.perspective_year, 3,
							(model.buy_energy_houseneed_plan_1 ?? 0) + (model.buy_energy_houseneed_plan_2 ?? 0),
							(model.buy_energy_techneed_plan_1 ?? 0) + (model.buy_energy_techneed_plan_2 ?? 0),
							(model.buy_loss_houseneed_plan_1 ?? 0) + (model.buy_loss_houseneed_plan_2 ?? 0),
							(model.buy_loss_techneed_plan_1 ?? 0) + (model.buy_loss_techneed_plan_2 ?? 0));
						await _context.TZ_InOutBuyEnergy_Plan.AddAsync(new_tz_bal_p_3);
					}

					await _context.SaveChangesAsync();
				}


				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZOutputEnergySource_Save", $"source_id={model.source_id},buy_place_id={model.buy_place_id},input_tz_id={model.input_tz_id},output_tz_id={model.output_tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}


		//Сохранение теплового баланса (передача)
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZTransfer_Save(TZOneTransferDataViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					var tz_upd_1 = await _context.TZ_BalanceHeatEnergyTransfer_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
									&& x.report_period_id == 1).FirstOrDefaultAsync();

					var tz_upd_2 = await _context.TZ_BalanceHeatEnergyTransfer_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
									&& x.report_period_id == 2).FirstOrDefaultAsync();

					var tz_upd_3 = await _context.TZ_BalanceHeatEnergyTransfer_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
									&& x.report_period_id == 3).FirstOrDefaultAsync();

					var tz_upd_p_1 = await _context.TZ_BalanceHeatEnergyTransfer_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
									&& x.perspective_year == model.perspective_year && x.report_period_id == 1).FirstOrDefaultAsync();

					var tz_upd_p_2 = await _context.TZ_BalanceHeatEnergyTransfer_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
									&& x.perspective_year == model.perspective_year && x.report_period_id == 2).FirstOrDefaultAsync();

					var tz_upd_p_3 = await _context.TZ_BalanceHeatEnergyTransfer_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
									&& x.perspective_year == model.perspective_year && x.report_period_id == 3).FirstOrDefaultAsync();

					if (tz_upd_3 != null)
					{
						tz_upd_1.loss_heatnet_fact = model.loss_heatnet_fact_1;
						tz_upd_1.ownprod_fact = model.ownprod_fact_1;
						tz_upd_1.household_needs_fact = model.household_needs_fact_1;
						tz_upd_1.tso_fact = model.tso_fact_1;
						tz_upd_1.buget_consumers_fact = model.buget_consumers_fact_1;
						tz_upd_1.public_fact = model.public_fact_1;
						tz_upd_1.other_consumers_fact = model.other_consumers_fact_1;
						tz_upd_1.notes_fact = null;
						tz_upd_1.edit_date = DateTime.Now;
						tz_upd_1.user_id = userId;

						tz_upd_2.loss_heatnet_fact = model.loss_heatnet_fact_2;
						tz_upd_2.ownprod_fact = model.ownprod_fact_2;
						tz_upd_2.household_needs_fact = model.household_needs_fact_2;
						tz_upd_2.tso_fact = model.tso_fact_2;
						tz_upd_2.buget_consumers_fact = model.buget_consumers_fact_2;
						tz_upd_2.public_fact = model.public_fact_2;
						tz_upd_2.other_consumers_fact = model.other_consumers_fact_2;
						tz_upd_2.notes_fact = null;
						tz_upd_2.edit_date = DateTime.Now;
						tz_upd_2.user_id = userId;

						tz_upd_3.loss_heatnet_fact = (model.loss_heatnet_fact_1 ?? 0) + (model.loss_heatnet_fact_2 ?? 0);
						tz_upd_3.ownprod_fact = (model.ownprod_fact_1 ?? 0) + (model.ownprod_fact_2 ?? 0);
						tz_upd_3.household_needs_fact = (model.household_needs_fact_1 ?? 0) + (model.household_needs_fact_2 ?? 0); 
						tz_upd_3.tso_fact = (model.tso_fact_1 ?? 0) + (model.tso_fact_2 ?? 0);
						tz_upd_3.buget_consumers_fact = (model.buget_consumers_fact_1 ?? 0) + (model.buget_consumers_fact_2 ?? 0);
						tz_upd_3.public_fact = (model.public_fact_1 ?? 0) + (model.public_fact_2 ?? 0);
						tz_upd_3.other_consumers_fact = (model.other_consumers_fact_1 ?? 0) + (model.other_consumers_fact_2 ?? 0); 
						tz_upd_3.notes_fact = model.notes_fact;
						tz_upd_3.edit_date = DateTime.Now;
						tz_upd_3.user_id = userId;
					}
					else
					{
						var new_tz_bal_f1 = GetNewTZBalanceTransferFactModel(model.tz_id, model.data_status, 1, model.loss_heatnet_fact_1,
							model.ownprod_fact_1, model.household_needs_fact_1, model.tso_fact_1, model.buget_consumers_fact_1, model.public_fact_1,
							model.other_consumers_fact_1, null, false);
						await _context.TZ_BalanceHeatEnergyTransfer_Fact.AddAsync(new_tz_bal_f1);

						var new_tz_bal_f2 = GetNewTZBalanceTransferFactModel(model.tz_id, model.data_status, 2, model.loss_heatnet_fact_2,
							model.ownprod_fact_2, model.household_needs_fact_2, model.tso_fact_2, model.buget_consumers_fact_2, model.public_fact_2,
							model.other_consumers_fact_2, null, false);
						await _context.TZ_BalanceHeatEnergyTransfer_Fact.AddAsync(new_tz_bal_f2);

						var new_tz_bal_f3 = GetNewTZBalanceTransferFactModel(model.tz_id, model.data_status, 3,
							(model.loss_heatnet_fact_1 ?? 0) + (model.loss_heatnet_fact_2 ?? 0),
							(model.ownprod_fact_1 ?? 0) + (model.ownprod_fact_2 ?? 0),
							(model.household_needs_fact_1 ?? 0) + (model.household_needs_fact_2 ?? 0),
							(model.tso_fact_1 ?? 0) + (model.tso_fact_2 ?? 0),
							(model.buget_consumers_fact_1 ?? 0) + (model.buget_consumers_fact_2 ?? 0),
							(model.public_fact_1 ?? 0) + (model.public_fact_2 ?? 0),
							(model.other_consumers_fact_1 ?? 0) + (model.other_consumers_fact_2 ?? 0), model.notes_fact, false);
						await _context.TZ_BalanceHeatEnergyTransfer_Fact.AddAsync(new_tz_bal_f3);
					}

					if (tz_upd_p_3 != null)
					{
						tz_upd_p_1.loss_heatnet_plan = model.loss_heatnet_plan_1;
						tz_upd_p_1.ownprod_plan = model.ownprod_plan_1;
						tz_upd_p_1.household_needs_plan = model.household_needs_plan_1;
						tz_upd_p_1.tso_plan = model.tso_plan_1;
						tz_upd_p_1.buget_consumers_plan = model.buget_consumers_plan_1;
						tz_upd_p_1.public_plan = model.public_plan_1;
						tz_upd_p_1.other_consumers_plan = model.other_consumers_plan_1;
						tz_upd_p_1.notes_plan = null;
						tz_upd_p_1.edit_date = DateTime.Now;
						tz_upd_p_1.user_id = userId;

						tz_upd_p_2.loss_heatnet_plan = model.loss_heatnet_plan_2;
						tz_upd_p_2.ownprod_plan = model.ownprod_plan_2;
						tz_upd_p_2.household_needs_plan = model.household_needs_plan_2;
						tz_upd_p_2.tso_plan = model.tso_plan_2;
						tz_upd_p_2.buget_consumers_plan = model.buget_consumers_plan_2;
						tz_upd_p_2.public_plan = model.public_plan_2;
						tz_upd_p_2.other_consumers_plan = model.other_consumers_plan_2;
						tz_upd_p_2.notes_plan = null;
						tz_upd_p_2.edit_date = DateTime.Now;
						tz_upd_p_2.user_id = userId;

						tz_upd_p_3.loss_heatnet_plan = (model.loss_heatnet_plan_1 ?? 0) + (model.loss_heatnet_plan_2 ?? 0);
						tz_upd_p_3.ownprod_plan = (model.ownprod_plan_1 ?? 0) + (model.ownprod_plan_2 ?? 0);
						tz_upd_p_3.household_needs_plan = (model.household_needs_plan_1 ?? 0) + (model.household_needs_plan_2 ?? 0);
						tz_upd_p_3.tso_plan = (model.tso_plan_1 ?? 0) + (model.tso_plan_2 ?? 0);
						tz_upd_p_3.buget_consumers_plan = (model.buget_consumers_plan_1 ?? 0) + (model.buget_consumers_plan_2 ?? 0);
						tz_upd_p_3.public_plan = (model.public_plan_1 ?? 0) + (model.public_plan_2 ?? 0);
						tz_upd_p_3.other_consumers_plan = (model.other_consumers_plan_1 ?? 0) + (model.other_consumers_plan_2 ?? 0);
						tz_upd_p_3.notes_plan = model.notes_plan;
						tz_upd_p_3.edit_date = DateTime.Now;
						tz_upd_p_3.user_id = userId;
					}
					else
					{
						var new_tz_bal_p1 = GetNewTZBalanceTransferPlanModel(model.tz_id, model.data_status, model.perspective_year, 1, model.loss_heatnet_plan_1,
							model.ownprod_plan_1, model.household_needs_plan_1, model.tso_plan_1, model.buget_consumers_plan_1, model.public_plan_1, 
							model.other_consumers_plan_1, null, false);
						await _context.TZ_BalanceHeatEnergyTransfer_Plan.AddAsync(new_tz_bal_p1);

						var new_tz_bal_p2 = GetNewTZBalanceTransferPlanModel(model.tz_id, model.data_status, model.perspective_year, 2, model.loss_heatnet_plan_2,
							model.ownprod_plan_2, model.household_needs_plan_2, model.tso_plan_2, model.buget_consumers_plan_2, model.public_plan_2,
							model.other_consumers_plan_2, null, false);
						await _context.TZ_BalanceHeatEnergyTransfer_Plan.AddAsync(new_tz_bal_p2);

						var new_tz_bal_p3 = GetNewTZBalanceTransferPlanModel(model.tz_id, model.data_status, model.perspective_year, 3,
							(model.loss_heatnet_plan_1 ?? 0) + (model.loss_heatnet_plan_2 ?? 0),
							(model.ownprod_plan_1 ?? 0) + (model.ownprod_plan_2 ?? 0),
							(model.household_needs_plan_1 ?? 0) + (model.household_needs_plan_2 ?? 0),
							(model.tso_plan_1 ?? 0) + (model.tso_plan_2 ?? 0),
							(model.buget_consumers_plan_1 ?? 0) + (model.buget_consumers_plan_2 ?? 0),
							(model.public_plan_1 ?? 0) + (model.public_plan_2 ?? 0),
							(model.other_consumers_plan_1 ?? 0) + (model.other_consumers_plan_2 ?? 0), model.notes_plan, false);
						await _context.TZ_BalanceHeatEnergyTransfer_Plan.AddAsync(new_tz_bal_p3);
					}

					await _context.SaveChangesAsync();
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZTransfer_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZOutputTransferEnergySource_Save(TZOutputTransferEnergySourceOneDataViewModel model)
		{
			try
			{
				if (model.input_tz_id > 0 && model.output_tz_id > 0 && model.source_id > 0)
				{
					var tz_upd_1 = await _context.TZ_InOutTransferEnergy_Fact.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
										&& x.data_status == model.data_status && x.source_id == model.old_source_id && x.report_period_id == 1).FirstOrDefaultAsync();

					var tz_upd_2 = await _context.TZ_InOutTransferEnergy_Fact.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
									&& x.data_status == model.data_status && x.source_id == model.old_source_id && x.report_period_id == 2).FirstOrDefaultAsync();

					var tz_upd_3 = await _context.TZ_InOutTransferEnergy_Fact.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
									&& x.data_status == model.data_status && x.source_id == model.old_source_id && x.report_period_id == 3).FirstOrDefaultAsync();

					if (tz_upd_3 != null)
					{
						if (model.input_tz_id != model.old_input_tz_id || model.source_id != model.old_source_id)
						{
							_context.TZ_InOutTransferEnergy_Fact.Remove(tz_upd_1);
							_context.TZ_InOutTransferEnergy_Fact.Remove(tz_upd_2);
							_context.TZ_InOutTransferEnergy_Fact.Remove(tz_upd_3);
						}
						else
						{
							tz_upd_1.loss_heatnet_fact = model.source_loss_heatnet_fact_1;
							tz_upd_1.ownprod_fact = model.source_ownprod_fact_1;
							tz_upd_1.household_needs_fact = model.source_household_needs_fact_1;
							tz_upd_1.tso_fact = model.source_tso_fact_1;
							tz_upd_1.buget_consumers_fact = model.source_buget_consumers_fact_1;
							tz_upd_1.public_fact = model.source_public_fact_1;
							tz_upd_1.other_consumers_fact = model.source_other_consumers_fact_1;
							tz_upd_1.edit_date = DateTime.Now;
							tz_upd_1.user_id = userId;

							tz_upd_2.loss_heatnet_fact = model.source_loss_heatnet_fact_2;
							tz_upd_2.ownprod_fact = model.source_ownprod_fact_2;
							tz_upd_2.household_needs_fact = model.source_household_needs_fact_2;
							tz_upd_2.tso_fact = model.source_tso_fact_2;
							tz_upd_2.buget_consumers_fact = model.source_buget_consumers_fact_2;
							tz_upd_2.public_fact = model.source_public_fact_2;
							tz_upd_2.other_consumers_fact = model.source_other_consumers_fact_2;
							tz_upd_2.edit_date = DateTime.Now;
							tz_upd_2.user_id = userId;

							tz_upd_3.loss_heatnet_fact = (model.source_loss_heatnet_fact_1 ?? 0) + (model.source_loss_heatnet_fact_2 ?? 0);
							tz_upd_3.ownprod_fact = (model.source_ownprod_fact_1 ?? 0) + (model.source_ownprod_fact_2 ?? 0);
							tz_upd_3.household_needs_fact = (model.source_household_needs_fact_1 ?? 0) + (model.source_household_needs_fact_2 ?? 0);
							tz_upd_3.tso_fact = (model.source_tso_fact_1 ?? 0) + (model.source_tso_fact_2 ?? 0);
							tz_upd_3.buget_consumers_fact = (model.source_buget_consumers_fact_1 ?? 0) + (model.source_buget_consumers_fact_2 ?? 0);
							tz_upd_3.public_fact = (model.source_public_fact_1 ?? 0) + (model.source_public_fact_2 ?? 0);
							tz_upd_3.other_consumers_fact = (model.source_other_consumers_fact_1 ?? 0) + (model.source_other_consumers_fact_2 ?? 0);
							tz_upd_3.edit_date = DateTime.Now;
							tz_upd_3.user_id = userId;
						}

					}
					if (model.input_tz_id != model.old_input_tz_id || model.source_id != model.old_source_id)
					{
						var new_tz_bal_f1 = GetNewTZInOutTransferEnergyFactModel(model.input_tz_id, model.output_tz_id, model.source_id, model.data_status, 1, 
							model.source_loss_heatnet_fact_1, model.source_ownprod_fact_1, model.source_household_needs_fact_1, model.source_tso_fact_1, 
							model.source_buget_consumers_fact_1, model.source_public_fact_1, model.source_other_consumers_fact_1, false);
						await _context.TZ_InOutTransferEnergy_Fact.AddAsync(new_tz_bal_f1);

						var new_tz_bal_f2 = GetNewTZInOutTransferEnergyFactModel(model.input_tz_id, model.output_tz_id, model.source_id, model.data_status, 2,
							model.source_loss_heatnet_fact_2, model.source_ownprod_fact_2, model.source_household_needs_fact_2, model.source_tso_fact_2,
							model.source_buget_consumers_fact_2, model.source_public_fact_2, model.source_other_consumers_fact_2, false);
						await _context.TZ_InOutTransferEnergy_Fact.AddAsync(new_tz_bal_f2);

						var new_tz_bal_f3 = GetNewTZInOutTransferEnergyFactModel(model.input_tz_id, model.output_tz_id, model.source_id, model.data_status, 3, 
							(model.source_loss_heatnet_fact_1 ?? 0) + (model.source_loss_heatnet_fact_2 ?? 0),
							(model.source_ownprod_fact_1 ?? 0) + (model.source_ownprod_fact_2 ?? 0),
							(model.source_household_needs_fact_1 ?? 0) + (model.source_household_needs_fact_2 ?? 0),
							(model.source_tso_fact_1 ?? 0) + (model.source_tso_fact_2 ?? 0),
							(model.source_buget_consumers_fact_1 ?? 0) + (model.source_buget_consumers_fact_2 ?? 0),
							(model.source_public_fact_1 ?? 0) + (model.source_public_fact_2 ?? 0),
							(model.source_other_consumers_fact_1 ?? 0) + (model.source_other_consumers_fact_2 ?? 0),
							false);
						await _context.TZ_InOutTransferEnergy_Fact.AddAsync(new_tz_bal_f3);
					}

					var tz_upd_p_1 = await _context.TZ_InOutTransferEnergy_Plan.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
										&& x.data_status == model.data_status && x.perspective_year == model.perspective_year && x.source_id == model.old_source_id
										&& x.report_period_id == 1).FirstOrDefaultAsync();

					var tz_upd_p_2 = await _context.TZ_InOutTransferEnergy_Plan.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
									&& x.data_status == model.data_status && x.perspective_year == model.perspective_year && x.source_id == model.old_source_id
									&& x.report_period_id == 2).FirstOrDefaultAsync();

					var tz_upd_p_3 = await _context.TZ_InOutTransferEnergy_Plan.Where(x => x.input_tz_id == model.old_input_tz_id && x.out_tz_id == model.output_tz_id
									&& x.data_status == model.data_status && x.perspective_year == model.perspective_year && x.source_id == model.old_source_id
									&& x.report_period_id == 3).FirstOrDefaultAsync();

					if (tz_upd_p_3 != null)
					{
						if (model.input_tz_id != model.old_input_tz_id || model.source_id != model.old_source_id)
						{
							_context.TZ_InOutTransferEnergy_Plan.Remove(tz_upd_p_1);
							_context.TZ_InOutTransferEnergy_Plan.Remove(tz_upd_p_2);
							_context.TZ_InOutTransferEnergy_Plan.Remove(tz_upd_p_3);
						}
						else
						{
							tz_upd_p_1.loss_heatnet_plan = model.source_loss_heatnet_plan_1;
							tz_upd_p_1.ownprod_plan = model.source_ownprod_plan_1;
							tz_upd_p_1.household_needs_plan = model.source_household_needs_plan_1;
							tz_upd_p_1.tso_plan = model.source_tso_plan_1;
							tz_upd_p_1.buget_consumers_plan = model.source_buget_consumers_plan_1;
							tz_upd_p_1.public_plan = model.source_public_plan_1;
							tz_upd_p_1.other_consumers_plan = model.source_other_consumers_plan_1;
							tz_upd_p_1.edit_date = DateTime.Now;
							tz_upd_p_1.user_id = userId;

							tz_upd_p_2.loss_heatnet_plan = model.source_loss_heatnet_plan_2;
							tz_upd_p_2.ownprod_plan = model.source_ownprod_plan_2;
							tz_upd_p_2.household_needs_plan = model.source_household_needs_plan_2;
							tz_upd_p_2.tso_plan = model.source_tso_plan_2;
							tz_upd_p_2.buget_consumers_plan = model.source_buget_consumers_plan_2;
							tz_upd_p_2.public_plan = model.source_public_plan_2;
							tz_upd_p_2.other_consumers_plan = model.source_other_consumers_plan_2;
							tz_upd_p_2.edit_date = DateTime.Now;
							tz_upd_p_2.user_id = userId;

							tz_upd_p_3.loss_heatnet_plan = (model.source_loss_heatnet_plan_1 ?? 0) + (model.source_loss_heatnet_plan_2 ?? 0);
							tz_upd_p_3.ownprod_plan = (model.source_ownprod_plan_1 ?? 0) + (model.source_ownprod_plan_2 ?? 0);
							tz_upd_p_3.household_needs_plan = (model.source_household_needs_plan_1 ?? 0) + (model.source_household_needs_plan_2 ?? 0);
							tz_upd_p_3.tso_plan = (model.source_tso_plan_1 ?? 0) + (model.source_tso_plan_2 ?? 0);
							tz_upd_p_3.buget_consumers_plan = (model.source_buget_consumers_plan_1 ?? 0) + (model.source_buget_consumers_plan_2 ?? 0);
							tz_upd_p_3.public_plan = (model.source_public_plan_1 ?? 0) + (model.source_public_plan_2 ?? 0);
							tz_upd_p_3.other_consumers_plan = (model.source_other_consumers_plan_1 ?? 0) + (model.source_other_consumers_plan_2 ?? 0);
							tz_upd_p_3.edit_date = DateTime.Now;
							tz_upd_p_3.user_id = userId;
						}
					}

					if (tz_upd_p_3 == null || model.input_tz_id != model.old_input_tz_id || model.source_id != model.old_source_id)
					{
						var new_tz_bal_p_1 = GetNewTZInOutTransferEnergyPlanModel(model.input_tz_id, model.output_tz_id, model.source_id, model.data_status, 
							model.perspective_year, 1, model.source_loss_heatnet_plan_1, model.source_ownprod_plan_1, model.source_household_needs_plan_1, 
							model.source_tso_plan_1, model.source_buget_consumers_plan_1, model.source_public_plan_1, model.source_other_consumers_plan_1, false);
						await _context.TZ_InOutTransferEnergy_Plan.AddAsync(new_tz_bal_p_1);

						var new_tz_bal_p_2 = GetNewTZInOutTransferEnergyPlanModel(model.input_tz_id, model.output_tz_id, model.source_id, model.data_status,
							model.perspective_year, 2, model.source_loss_heatnet_plan_2, model.source_ownprod_plan_2, model.source_household_needs_plan_2,
							model.source_tso_plan_2, model.source_buget_consumers_plan_2, model.source_public_plan_2, model.source_other_consumers_plan_2, false);
						await _context.TZ_InOutTransferEnergy_Plan.AddAsync(new_tz_bal_p_2);

						var new_tz_bal_p_3 = GetNewTZInOutTransferEnergyPlanModel(model.input_tz_id, model.output_tz_id, model.source_id, model.data_status, 
							model.perspective_year, 3,
							(model.source_loss_heatnet_plan_1 ?? 0) + (model.source_loss_heatnet_plan_2 ?? 0),
							(model.source_ownprod_plan_1 ?? 0) + (model.source_ownprod_plan_2 ?? 0),
							(model.source_household_needs_plan_1 ?? 0) + (model.source_household_needs_plan_2 ?? 0),
							(model.source_tso_plan_1 ?? 0) + (model.source_tso_plan_2 ?? 0),
							(model.source_buget_consumers_plan_1 ?? 0) + (model.source_buget_consumers_plan_2 ?? 0),
							(model.source_public_plan_1 ?? 0) + (model.source_public_plan_2 ?? 0),
							(model.source_other_consumers_plan_1 ?? 0) + (model.source_other_consumers_plan_2 ?? 0), false);
						await _context.TZ_InOutTransferEnergy_Plan.AddAsync(new_tz_bal_p_3);
					}

					await _context.SaveChangesAsync();
				}


				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZOutputEnergySource_Save", $"source_id={model.source_id},input_tz_id={model.input_tz_id},output_tz_id={model.output_tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		#endregion

		//Создание модели для добавления новой записи по балансу тепловой энергии (факт)
		[NonAction]
		public TZ_BalanceHeatEnergy_Fact GetNewTZBalanceFactModel(int tz_id, int data_status, short report_period_id, decimal? generate_heat_energy_fact, decimal? heat_consump_fact,	decimal? release_collect_ownprod_fact, 
			decimal? release_collect_household_needs_fact, decimal? release_collect_tso_salers_fact, decimal? release_collect_buget_cons_fact,
			decimal? release_collect_public_fact, decimal? release_collect_other_cons_fact, decimal? release_collect_ownheatnet_fact, decimal? heatenrg_loss_heatnet_fact,
			decimal? useful_release_ownprod_fact, decimal? useful_release_household_needs_fact, decimal? useful_release_tso_salers_fact, decimal? useful_release_buget_cons_fact,
			decimal? useful_release_public_fact, decimal? useful_release_other_cons_fact, string? notes_fact, bool? is_deleted)
		{
			var new_tz_bal = new TZ_BalanceHeatEnergy_Fact()
			{
				tz_id = tz_id,
				data_status = data_status,
				report_period_id = report_period_id,
				generate_heat_energy_fact = generate_heat_energy_fact,
				heat_consump_fact = heat_consump_fact,
				release_collect_ownprod_fact = release_collect_ownprod_fact,
				release_collect_household_needs_fact = release_collect_household_needs_fact,
				release_collect_tso_salers_fact = release_collect_tso_salers_fact,
				release_collect_buget_cons_fact = release_collect_buget_cons_fact,
				release_collect_public_fact = release_collect_public_fact,
				release_collect_other_cons_fact = release_collect_other_cons_fact,
				release_collect_ownheatnet_fact = release_collect_ownheatnet_fact,
				heatenrg_loss_heatnet_fact = heatenrg_loss_heatnet_fact,
				useful_release_ownprod_fact = useful_release_ownprod_fact,
				useful_release_household_needs_fact = useful_release_household_needs_fact,
				useful_release_tso_salers_fact = useful_release_tso_salers_fact,
				useful_release_buget_cons_fact = useful_release_buget_cons_fact,
				useful_release_public_fact = useful_release_public_fact,
				useful_release_other_cons_fact = useful_release_other_cons_fact,
				notes_fact = notes_fact,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		//Создание модели для добавления новой записи по балансу тепловой энергии (производство - план)
		[NonAction]
		public TZ_BalanceHeatEnergy_Plan GetNewTZBalancePlanModel(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? generate_heat_energy_plan, 
			decimal? heat_consump_plan, decimal? release_collect_ownprod_plan, decimal? release_collect_household_needs_plan, decimal? release_collect_tso_salers_plan, 
			decimal? release_collect_buget_cons_plan, decimal? release_collect_public_plan, decimal? release_collect_other_cons_plan, decimal? release_collect_ownheatnet_plan, 
			decimal? heatenrg_loss_heatnet_plan, decimal? useful_release_ownprod_plan, decimal? useful_release_household_needs_plan, decimal? useful_release_tso_salers_plan, 
			decimal? useful_release_buget_cons_plan, decimal? useful_release_public_plan, decimal? useful_release_other_cons_plan, string? notes_plan)
		{
			var new_tz_bal = new TZ_BalanceHeatEnergy_Plan()
			{
				tz_id = tz_id,
				data_status = data_status,
				perspective_year = perspective_year,
				report_period_id = report_period_id,
				generate_heat_energy_plan = generate_heat_energy_plan,
				heat_consump_plan = heat_consump_plan,
				release_collect_ownprod_plan = release_collect_ownprod_plan,
				release_collect_household_needs_plan = release_collect_household_needs_plan,
				release_collect_tso_salers_plan = release_collect_tso_salers_plan,
				release_collect_buget_cons_plan = release_collect_buget_cons_plan,
				release_collect_public_plan = release_collect_public_plan,
				release_collect_other_cons_plan = release_collect_other_cons_plan,
				release_collect_ownheatnet_plan = release_collect_ownheatnet_plan,
				heatenrg_loss_heatnet_plan = heatenrg_loss_heatnet_plan,
				useful_release_ownprod_plan = useful_release_ownprod_plan,
				useful_release_household_needs_plan = useful_release_household_needs_plan,
				useful_release_tso_salers_plan = useful_release_tso_salers_plan,
				useful_release_buget_cons_plan = useful_release_buget_cons_plan,
				useful_release_public_plan = useful_release_public_plan,
				useful_release_other_cons_plan = useful_release_other_cons_plan,
				notes_plan = notes_plan,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		//Создание модели для добавления новой записи по продаже тепловой энергии (производство - факт)
		[NonAction]
		public TZ_InOutBuyEnergy_Fact GetNewTZInOutBuyEnergyFactModel(int input_tz_id, int output_tz_id, int source_id, short buy_place_id, int data_status,
			short report_period_id, decimal? buy_energy_houseneed_fact, decimal? buy_energy_techneed_fact, decimal? buy_loss_houseneed_fact,
			decimal? buy_loss_techneed_fact, bool? is_deleted)
		{
			var new_tz_bal = new TZ_InOutBuyEnergy_Fact()
			{
				input_tz_id = input_tz_id,
				out_tz_id = output_tz_id,
				source_id = source_id,
				buy_place_id = buy_place_id,
				data_status = data_status,
				report_period_id = report_period_id,
				buy_energy_houseneed_fact = buy_energy_houseneed_fact,
				buy_energy_techneed_fact = buy_energy_techneed_fact,
				buy_loss_houseneed_fact = buy_loss_houseneed_fact,
				buy_loss_techneed_fact = buy_loss_techneed_fact,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		//Создание модели для добавления новой записи по продаже тепловой энергии (производство - план)
		[NonAction]
		public TZ_InOutBuyEnergy_Plan GetNewTZInOutBuyEnergyPlanModel(int input_tz_id, int output_tz_id, int source_id, short buy_place_id, int data_status,
			int perspective_year, short report_period_id, decimal? buy_energy_houseneed_plan, decimal? buy_energy_techneed_plan, decimal? buy_loss_houseneed_plan,
			decimal? buy_loss_techneed_plan)
		{
			var new_tz_bal = new TZ_InOutBuyEnergy_Plan()
			{
				input_tz_id = input_tz_id,
				out_tz_id = output_tz_id,
				source_id = source_id,
				buy_place_id = buy_place_id,
				data_status = data_status,
				perspective_year = perspective_year,
				report_period_id = report_period_id,
				buy_energy_houseneed_plan = buy_energy_houseneed_plan,
				buy_energy_techneed_plan = buy_energy_techneed_plan,
				buy_loss_houseneed_plan = buy_loss_houseneed_plan,
				buy_loss_techneed_plan = buy_loss_techneed_plan,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		//Создание модели для добавления новой записи по балансу тепловой энергии (передача - факт)
		[NonAction]
		public TZ_BalanceHeatEnergyTransfer_Fact GetNewTZBalanceTransferFactModel(int tz_id, int data_status, short report_period_id,
			decimal? loss_heatnet_fact, decimal? ownprod_fact, decimal? household_needs_fact, decimal? tso_fact, decimal? buget_consumers_fact,
			decimal? public_fact, decimal? other_consumers_fact, string? notes_fact, bool? is_deleted)
		{
			var new_tz_bal = new TZ_BalanceHeatEnergyTransfer_Fact()
			{
				tz_id = tz_id,
				data_status = data_status,
				report_period_id = report_period_id,
				loss_heatnet_fact = loss_heatnet_fact,
				ownprod_fact = ownprod_fact,
				household_needs_fact = household_needs_fact,
				tso_fact = tso_fact,
				buget_consumers_fact = buget_consumers_fact,
				public_fact = public_fact,
				other_consumers_fact = other_consumers_fact,
				notes_fact = notes_fact,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		//Создание модели для добавления новой записи по балансу тепловой энергии (передача - план)
		[NonAction]
		public TZ_BalanceHeatEnergyTransfer_Plan GetNewTZBalanceTransferPlanModel(int tz_id, int data_status, int perspective_year, short report_period_id, 
			decimal? loss_heatnet_plan, decimal? ownprod_plan, decimal? household_needs_plan, decimal? tso_plan, decimal? buget_consumers_plan,
			decimal? public_plan, decimal? other_consumers_plan, string? notes_plan, bool? is_deleted)
		{
			var new_tz_bal = new TZ_BalanceHeatEnergyTransfer_Plan()
			{
				tz_id = tz_id,
				data_status = data_status,
				perspective_year = perspective_year,
				report_period_id = report_period_id,
				loss_heatnet_plan = loss_heatnet_plan,
				ownprod_plan = ownprod_plan,
				household_needs_plan = household_needs_plan,
				tso_plan = tso_plan,
				buget_consumers_plan = buget_consumers_plan,
				public_plan = public_plan,
				other_consumers_plan = other_consumers_plan,
				notes_plan = notes_plan,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		//Создание модели для добавления новой записи по транспортировке тепловой энергии (передача - факт)
		[NonAction]
		public TZ_InOutTransferEnergy_Fact GetNewTZInOutTransferEnergyFactModel(int input_tz_id, int output_tz_id, int source_id, int data_status,
			short report_period_id, decimal? loss_heatnet_fact, decimal? ownprod_fact, decimal? household_needs_fact, decimal? tso_fact,
			decimal? buget_consumers_fact, decimal? public_fact, decimal? other_consumers_fact, bool? is_deleted)
		{
			var new_tz_bal = new TZ_InOutTransferEnergy_Fact()
			{
				input_tz_id = input_tz_id,
				out_tz_id = output_tz_id,
				source_id = source_id,
				data_status = data_status,
				report_period_id = report_period_id,
				loss_heatnet_fact = loss_heatnet_fact,
				ownprod_fact = ownprod_fact,
				household_needs_fact = household_needs_fact,
				tso_fact = tso_fact,
				buget_consumers_fact = buget_consumers_fact,
				public_fact = public_fact,
				other_consumers_fact = other_consumers_fact,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		//Создание модели для добавления новой записи по транспортировке тепловой энергии (передача - план)
		[NonAction]
		public TZ_InOutTransferEnergy_Plan GetNewTZInOutTransferEnergyPlanModel(int input_tz_id, int output_tz_id, int source_id, int data_status, int perspective_year,
			short report_period_id, decimal? loss_heatnet_plan, decimal? ownprod_plan, decimal? household_needs_plan, decimal? tso_plan,
			decimal? buget_consumers_plan, decimal? public_plan, decimal? other_consumers_plan, bool? is_deleted)
		{
			var new_tz_bal = new TZ_InOutTransferEnergy_Plan()
			{
				input_tz_id = input_tz_id,
				out_tz_id = output_tz_id,
				source_id = source_id,
				data_status = data_status,
				perspective_year = perspective_year,
				report_period_id = report_period_id,
				loss_heatnet_plan = loss_heatnet_plan,
				ownprod_plan = ownprod_plan,
				household_needs_plan = household_needs_plan,
				tso_plan = tso_plan,
				buget_consumers_plan = buget_consumers_plan,
				public_plan = public_plan,
				other_consumers_plan = other_consumers_plan,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		#region ViewComponents
		//Тепловой баланс - производство
		public ActionResult OnGetCallTZ_ProductionList_PartialViewComponent(int data_status, int perspective_year)
		{
			return ViewComponent("TZ_ProductionDataList_Partial", new { data_status, perspective_year, userId });
		}
		//Тепловой баланс - передача
		public ActionResult OnGetCallTZ_TransferList_PartialViewComponent(int data_status, int perspective_year)
		{
			return ViewComponent("TZ_TransferDataList_Partial", new { data_status, perspective_year, userId });
		}
		//Исходящая тепловая энергия - перспектива/план
		public ActionResult OnGetCallTZ_OutputEnergyDataList_PartialViewComponent(int tz_id, int data_status)
		{
			return ViewComponent("TZ_OutputEnergyDataList_Partial", new { tz_id, data_status, userId });
		}
		//Исходящая тепловая энергия по источникам
		public ActionResult OnGetCallTZ_OutputEnergySourcesListData_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_OutputEnergySourcesListData_Partial", new { tz_id, data_status, perspective_year, userId });
		}

		//Транспортируемая тепловая энергия - перспектива/план
		public ActionResult OnGetCallTZ_OutputTransferEnergyDataList_PartialViewComponent(int tz_id, int data_status)
		{
			return ViewComponent("TZ_OutputTransferEnergyDataList_Partial", new { tz_id, data_status, userId });
		}
		//Транспортируемая тепловая энергия по источникам
		public ActionResult OnGetCallTZ_OutputTransferEnergySourcesListData_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_OutputTransferEnergySourcesListData_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		#endregion
	}
}
