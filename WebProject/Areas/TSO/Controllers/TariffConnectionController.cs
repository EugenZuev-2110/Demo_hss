using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Security.Policy;
using WebProject.Areas.TSO.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using WebProject.Models;

namespace WebProject.Areas.TSO.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("TSO")]
	public class TariffConnectionController : Controller
    {
		private readonly ILogger<TariffConnectionController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;
		private TZHeaderTariffDataViewModel _tariffConnectionViewModel;
		//private PrincipalContext _ctx = new PrincipalContext(ContextType.Domain);
		public TariffConnectionController(ILogger<TariffConnectionController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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
					userDisplayName = "Зуев Евгений Александрович";
				}
			}
		}

		public IActionResult TZTariffConnection()
		{
			return View();
		}

	

		#region OpenPopups
		public async Task<IActionResult> OpenTariffConnection(int id, int data_status, int perspective_year)
		{
			var _tariffConnectionViewModel = (await _context.TZHeaderTariffDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZCalcTariffHotWaterSteamDataOne {id},{data_status},{perspective_year},{userId}")
							.ToListAsync()).FirstOrDefault() ?? new TZHeaderTariffDataViewModel();
			try
			{
				_tariffConnectionViewModel.tz_id = id;
				_tariffConnectionViewModel.data_status = data_status;
				_tariffConnectionViewModel.perspective_year = perspective_year;

				ViewBag.TZTSOList = _context.fnt_GetTZTSOList(data_status, perspective_year, "all").ToList();
				ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(_tariffConnectionViewModel);
		}

		public async Task<IActionResult> OpenTPIndividualFee(int consumer_id, string? decision_num, int isModified, int data_status)
		{
			var _openIndividualFeeTable = (await _context.OpenIndividualFeeTable.FromSqlInterpolated($"exec tarif_zone.sp_GetTZIndividualFeeDataOne {consumer_id},{decision_num}")
							.ToListAsync()).FirstOrDefault() ?? new OpenIndividualFeeTable();
			try
			{
				_openIndividualFeeTable.individual_fee_one_data_status = data_status;
				_openIndividualFeeTable.individual_fee_one_is_modyfied = isModified;

				ViewBag.TZTSOList = _context.fnt_GetOrgList(data_status, 1).ToList();
				ViewBag.UnomPPList = _context.fnt_GetUnomPPListByChars("", data_status);
				ViewBag.DevProgList = _context.fnt_GetDevProgramsList(consumer_id);
				ViewBag.DecisionStatusList = _context.fnt_GetDecisionStatusList();
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenTPIndividualFee", $"consumer_id={consumer_id},decision_num={decision_num},data_status={data_status}", ex.Message, consumer_id);
			}

			return PartialView("OpenTPIndividualFee", _openIndividualFeeTable);
		}

		public async Task<IActionResult> OpenStandardizedRates(int tso_id, string? decision_num, int data_status, int isModified)
		{
			var _openStandardizedRatesTable = (await _context.OpenStandardizedRatesTable.FromSqlInterpolated($"exec tarif_zone.sp_GetTZStandardizedRatesDataOne {tso_id},{data_status},{decision_num}")
							.ToListAsync()).FirstOrDefault() ?? new OpenStandardizedRatesTable();
			try
			{
				_openStandardizedRatesTable.standardized_rates_one_tso_id = tso_id;
				_openStandardizedRatesTable.standardized_rates_one_data_status = data_status;
				_openStandardizedRatesTable.standardized_rates_one_decision_num = decision_num;
				_openStandardizedRatesTable.standardized_rates_one_is_modyfied = isModified;

				ViewBag.TZTSOList = _context.fnt_GetOrgList(data_status, 1).ToList();
				ViewBag.DecisionStatusList = _context.fnt_GetDecisionStatusList();
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenStandardizedRates", $"id={tso_id},data_status={data_status}", ex.Message, userId);
			}
			return PartialView("OpenStandardizedRates",_openStandardizedRatesTable);
		}

		public async Task<IActionResult> OpenPowerReservePayment(int tso_id, string? decision_num, int data_status, int isModified)
		{
			var _openPowerReservePaymentTable = (await _context.OpenPowerReservePaymentTable.FromSqlInterpolated($"exec tarif_zone.sp_GetTZPowerReservePaymentDataOne {tso_id},{data_status},{decision_num}")
							.ToListAsync()).FirstOrDefault() ?? new OpenPowerReservePaymentTable();
			try
			{
				_openPowerReservePaymentTable.power_reserve_payment_one_tso_id = tso_id;
				_openPowerReservePaymentTable.power_reserve_payment_one_data_status = data_status;
				_openPowerReservePaymentTable.power_reserve_payment_one_decision_num = decision_num;
				_openPowerReservePaymentTable.power_reserve_payment_one_is_modyfied = isModified;

				ViewBag.TZTSOList = _context.fnt_GetOrgList(data_status, 1).ToList();
				ViewBag.DecisionStatusList = _context.fnt_GetDecisionStatusList();
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPowerReservePayment", $"tso_id={tso_id},data_status={data_status}", ex.Message, userId);
			}
			return PartialView("OpenPowerReservePayment", _openPowerReservePaymentTable);
		}

		#endregion

		#region ViewComponents

		//список потребителей
		public ActionResult OnGetConsumersListViewComponent(int data_status, string address_string)
		{
			return ViewComponent("EventsList", new { data_status, address_string });
		}

		//Тарифы и плата за подключение - тарифы - горячая вода
		public ActionResult OnGetCallTZ_TariffHotWaterList_PartialViewComponent(int data_status, int perspective_year)
		{
			return ViewComponent("TZ_TariffHotWaterDataList_Partial", new { data_status, perspective_year, userId });
		}

		//Тарифы и плата за подключение - тарифы - пар
		public ActionResult OnGetCallTZ_TariffSteamList_PartialViewComponent(int data_status, int perspective_year)
		{
			return ViewComponent("TZ_TariffSteamDataList_Partial", new { data_status, perspective_year, userId });
		}

		//Тарифы и плата за подключение - индивидуальнрая плата ТП
		public ActionResult OnGetCallTZ_IndividualFeeList_PartialViewComponent(int data_status)
		{
			return ViewComponent("TZ_IndividualFeeDataList_Partial", new { data_status, userId });
		}

		//Тарифы и плата за подключение - стандартизированные ставки ТП
		public ActionResult OnGetCallTZ_HPStandardizedRatesList_PartialViewComponent(int data_status, int tso_id)
		{
			return ViewComponent("TZ_HPStandardizedRatesDataList_Partial", new { data_status, tso_id, userId });
		}

		//Тарифы и плата за подключение - плата за резерв мощности
		public ActionResult OnGetCallTZ_PowerReservePaymentList_PartialViewComponent(int data_status, int tso_id)
		{
			return ViewComponent("TZ_PowerReservePaymentDataList_Partial", new { data_status, tso_id, userId });
		}
		#endregion

		#region DELETE DATA ROWS

		public async Task<IActionResult> TZDeleteDataRow_IndividualFee(int consumer_id, string? decision_num, int data_status)
		{
			var _openIndividualFeeTable = (await _context.OpenIndividualFeeTable.FromSqlInterpolated($"exec tarif_zone.sp_GetTZIndividualFeeDataOne {consumer_id},{decision_num}")
							.ToListAsync()).FirstOrDefault();
			try
			{
				if( _openIndividualFeeTable != null )
				{
					var obj = await GetDto_IndividuaFee(_openIndividualFeeTable);
					obj.is_deleted = true;
					await _context.SaveChangesAsync();
				}

				return Json(new {isDelete = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZDeleteDataRow_IndividualFee", $"consumer_id={consumer_id},decision_num={decision_num},data_status={data_status}", ex.Message, consumer_id);
			}

			return Json(new { isDelete = false });
		}

		public async Task<IActionResult> TZDeleteDataRow_StandardizedRates(int tso_id, string? decision_num, int data_status)
		{
			var _openStandardizedRatesTable = (await _context.OpenStandardizedRatesTable.FromSqlInterpolated($"exec tarif_zone.sp_GetTZStandardizedRatesDataOne {tso_id},{data_status},{decision_num}")
							.ToListAsync()).FirstOrDefault();
			try
			{
				if (_openStandardizedRatesTable != null)
				{
					var obj = await GetDto_StandardizedRates(_openStandardizedRatesTable);
					obj.is_deleted = true;
					await _context.SaveChangesAsync();
				}

				return Json(new { isDelete = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZDeleteDataRow_StandardizedRates", $"tso_id={tso_id},decision_num={decision_num},data_status={data_status}", ex.Message, tso_id);
			}

			return Json(new { isDelete = false });
		}

		public async Task<IActionResult> TZDeleteDataRow_PowerReservePayment(int tso_id, string? decision_num, int data_status)
		{
			var _openPowerReservePaymentTable = (await _context.OpenPowerReservePaymentTable.FromSqlInterpolated($"exec tarif_zone.sp_GetTZPowerReservePaymentDataOne {tso_id},{data_status},{decision_num}")
							.ToListAsync()).FirstOrDefault();
			try
			{
				if (_openPowerReservePaymentTable != null)
				{
					var obj = await GetDto_PowerReservePayment(_openPowerReservePaymentTable);
					obj.is_deleted = true;
					await _context.SaveChangesAsync();
				}

				return Json(new { isDelete = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZDeleteDataRow_PowerReservePayment", $"tso_id={tso_id},decision_num={decision_num},data_status={data_status}", ex.Message, tso_id);
			}

			return Json(new { isDelete = false });
		}

		#endregion

		#region SAVE_DATA HOT WATER / STEAM
		//Сохранение тарифов (горячая вода / пар). Тариф на тепловую энергию в паре (одноставочный), руб./Гкал без НДС
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZHeatEnergyTariffSteam_Save(HeatEnergyTariffSteam model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					await SetAllParameters_TariffsHESteam(model);
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZProduction_Save", $"id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}



		// Сохранение тарифов на тепловую энергию в горячей воде, руб./Гкал без НДС. Двухставочный тариф, без НДС
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZTwoStageTariff_Save(TwoStageTariffUnit model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					await SetAllParameters_TwoStageTariff(model);
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZProduction_Save", $"id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		// Сохранение тарифов на тепловую энергию в горячей воде, руб./Гкал без НДС. Одноставочный тариф, без НДС
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZSingleStageTariff_Save(SingleStageTariffUnit model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					await SetAllParameters_TariffsSingleHE(model);
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZProduction_Save", $"id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		// Сохранение тарифов на тепловую энергию в горячей воде, руб./Гкал без НДС. Главное меню
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZHeatEnergyTariffWater_Save(HeatEnergyTariffWater model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					await SetAllParameters_HeatEnergyTariffWater(model);
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZProduction_Save", $"id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		// Сохранение тарифов на передачу тепловой энергии, руб./Гкал без НДС
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZHeatEnergyTransfer_Save(HeatEnergyTransfer model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					await SetAllParameters_HeatEnergyTransfer(model);
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZProduction_Save", $"id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		// Сохранение тарифов на передачу тепловой энергии, руб./Гкал без НДС
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZETO_Amount_Preference_Save(ETO_Amount_Preference model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					await SetAllParameters_ETO_Amount_Preference(model);
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZProduction_Save", $"id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		// Сохранение тарифов на передачу тепловой энергии, руб./Гкал без НДС
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZHeatEnergyTariffSteamHotWater_Save(HeatEnergyTariffSteamHotWater model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					await SetAllParameters_HeatEnergyTariffSteamHotWater(model);
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZProduction_Save", $"id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		// Сохранение тарифов на передачу тепловой энергии, руб./Гкал без НДС
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZCrossSubsidizationAmount_Save(CrossSubsidizationAmount model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					await SetAllParameters_CrossSubsidizationAmount(model);
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZProduction_Save", $"id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		#endregion

		#region SAVE_DATA INDIVIDUAL FEE

		// Сохранение платы в индивидуальном порядке
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZIndividualFee_Save(OpenIndividualFeeTable model)
		{
			try
			{
				if (model.individual_fee_one_tso_id > 0)
				{
					var isObjEx = await SetValues_IndividuaFee(model);

					if(isObjEx)
						return Json(new { success = false, error = "Введенные Вами данные уже существуют!" });

					await _context.SaveChangesAsync();
				}

				return Json(new 
				{
					success = true,
					consumer_id = model.individual_fee_one_consumer_id,
					isModified = model.individual_fee_one_is_modyfied,
					decision_num = model.individual_fee_one_decision_num,
					method_name = "individual_fee"
				});
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZIndividualFee_Save", $"id={model.individual_fee_one_tso_id},data_status={model.individual_fee_one_data_status},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		#endregion

		#region SAVE_DATA STANDARDIZED RATES

		// Сохранение стандартизированных ставок ТП
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZStandardizedRates_Save(OpenStandardizedRatesTable model)
		{
			try
			{
				var isObjEx = await SetValues_StandardizedRates(model);

				if (isObjEx)
					return Json(new { success = false, error = "Введенные Вами данные уже существуют!" });

				await _context.SaveChangesAsync();
				return Json(new 
				{ 
					success = true,
					tso_id = model.standardized_rates_one_tso_id,
					isModified = model.standardized_rates_one_is_modyfied,
					decision_num = model.standardized_rates_one_decision_num,
					method_name = "standardized_rates"
				});
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZStandardizedRates_Save", $"id={model.standardized_rates_one_tso_id},data_status={model.standardized_rates_one_data_status}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		#endregion

		#region SAVE_DATA POWER RESERVE PAYMENT

		// Сохранение стандартизированных ставок ТП
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZPowerReservePayment_Save(OpenPowerReservePaymentTable model)
		{
			try
			{
				var isObjEx = await SetValues_PowerReservePayment(model);

				if (isObjEx)
					return Json(new { success = false, error = "Введенные Вами данные уже существуют!" });

				await _context.SaveChangesAsync();
				return Json(new 
				{ 
					success = true, tso_id = model.power_reserve_payment_one_tso_id,
					isModified = model.power_reserve_payment_one_is_modyfied,
					decision_num = model.power_reserve_payment_one_decision_num,
					method_name = "power_reserve_payment"});
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZPowerReservePayment_Save", $"id={model.power_reserve_payment_one_tso_id},data_status={model.power_reserve_payment_one_data_status}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		#endregion

		#region Модели для сохранения данных вкладки Тарифы

		#region Модель для добавления новой записи или редактирования существующей. Тариф на тепловую энергию в паре (одноставочный), руб./Гкал без НДС

		//Создание модели для добавления новой записи или редактирования существующей. Тариф на тепловую энергию в паре (одноставочный), руб./Гкал без НДС
		[NonAction]
		public TZ_TariffsHESteam_FactPlan GetNewTZTariffsHESteamModel(HeatEnergyTariffSteam model, short report_period_id, short steam_parameter_id)
		{
			var members = model.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"{report_period_id}")).ToList();

			var new_tz_bal = new TZ_TariffsHESteam_FactPlan()
			{
				tz_id = model.tz_id,
				data_status = model.data_status,
				perspective_year = model.perspective_year,
				report_period_id = report_period_id,
				steam_parameter_id = steam_parameter_id,
				tariff_econom_justified = CalculateEconomJustified_TariffsHESteam(members, "tariff_econom_justified", model, report_period_id, steam_parameter_id),
				useful_release_ownprod_needs = GetValue_TariffsHESteam(members, "useful_release_ownprod_needs", model, report_period_id, steam_parameter_id),
				useful_release_household_needs = GetValue_TariffsHESteam(members, "useful_release_household_needs", model, report_period_id, steam_parameter_id),
				tariff_weighted_invest = GetValue_CalculateWeightedTariffInvestComponent_TariffsHESteam(members, "tariff_weighted_invest", model, report_period_id, steam_parameter_id),
				invest_component = GetValue_CalculateWeightedTariffInvestComponent_TariffsHESteam(members, "invest_component", model, report_period_id, steam_parameter_id),
				tariff_tso = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "tso", model, report_period_id, steam_parameter_id),
				volume_he_tso = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "tso", model, report_period_id, steam_parameter_id),
				tariff_budgetcons = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "budgetcons", model, report_period_id, steam_parameter_id),
				volume_he_budgetcons = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "budgetcons", model, report_period_id, steam_parameter_id),
				tariff_public = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "public", model, report_period_id, steam_parameter_id),
				volume_he_public = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "public", model, report_period_id, steam_parameter_id),
				tariff_other = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "other", model, report_period_id, steam_parameter_id),
				volume_he_other = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "other", model, report_period_id, steam_parameter_id),
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		[NonAction]
		public decimal? GetValue_TariffsHESteam(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffSteam majorModel, short report_period_id, short steam_parameter_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"2")).ToList();

				var first = GetValue_TariffsHESteam(members_1, propertyName, majorModel, 1, steam_parameter_id);
				var second = GetValue_TariffsHESteam(members_2, propertyName, majorModel, 2, steam_parameter_id);
				return first + second;
			}
		
			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? CalculateEconomJustified_TariffsHESteam(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffSteam majorModel, short report_period_id, short steam_parameter_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"2")).ToList();

				var outer_1 = CalculateEconomJustified_TariffsHESteam(members_1, propertyName, majorModel, 1, steam_parameter_id);
				var outer_2 = CalculateEconomJustified_TariffsHESteam(members_2, propertyName, majorModel, 2, steam_parameter_id);

				decimal? ans_1_1 = (decimal?)members_1.First(x => x.Name.StartsWith("volume_he_tso_p" + steam_parameter_id + "_per_1")).GetValue(majorModel) ?? 0;
				decimal? ans_1_2 = (decimal?)members_1.First(x => x.Name.StartsWith("volume_he_budgetcons_p" + steam_parameter_id + "_per_1")).GetValue(majorModel) ?? 0;
				decimal? ans_1_3 = (decimal?)members_1.First(x => x.Name.StartsWith("volume_he_public_p" + steam_parameter_id + "_per_1")).GetValue(majorModel) ?? 0;
				decimal? ans_1_4 = (decimal?)members_1.First(x => x.Name.StartsWith("volume_he_other_p" + steam_parameter_id + "_per_1")).GetValue(majorModel) ?? 0;

				decimal? ans_2_1 = (decimal?)members_2.First(x => x.Name.StartsWith("volume_he_tso_p" + steam_parameter_id + "_per_2")).GetValue(majorModel) ?? 0;
				decimal? ans_2_2 = (decimal?)members_2.First(x => x.Name.StartsWith("volume_he_budgetcons_p" + steam_parameter_id + "_per_2")).GetValue(majorModel) ?? 0;
				decimal? ans_2_3 = (decimal?)members_2.First(x => x.Name.StartsWith("volume_he_public_p" + steam_parameter_id + "_per_2")).GetValue(majorModel) ?? 0;
				decimal? ans_2_4 = (decimal?)members_2.First(x => x.Name.StartsWith("volume_he_other_p" + steam_parameter_id + "_per_2")).GetValue(majorModel) ?? 0;

				if ((ans_1_1 + ans_1_2 + ans_1_3 + ans_1_4 + ans_2_1 + ans_2_2 + ans_2_3 + ans_2_4) == 0)
					return 0;

				decimal? ans_finally = (outer_1 * (ans_1_1 + ans_1_2 + ans_1_3 + ans_1_4) + outer_2 * (ans_2_1 + ans_2_2 + ans_2_3 + ans_2_4)) / (ans_1_1 + ans_1_2 + ans_1_3 + ans_1_4 + ans_2_1 + ans_2_2 + ans_2_3 + ans_2_4);
				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_CalculateWeightedTariffInvestComponent_TariffsHESteam(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffSteam majorModel, short report_period_id, short steam_parameter_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"2")).ToList();

				var ans_1_2 = (decimal?)members_1.First(n => n.Name.StartsWith("volume_he_budgetcons_p" + steam_parameter_id + "_per_1")).GetValue(majorModel) ?? 0;
				var ans_1_3 = (decimal?)members_1.First(n => n.Name.StartsWith("volume_he_public_p" + steam_parameter_id + "_per_1")).GetValue(majorModel) ?? 0;
				var ans_1_4 = (decimal?)members_1.First(n => n.Name.StartsWith("volume_he_other_p" + steam_parameter_id + "_per_1")).GetValue(majorModel) ?? 0;

				var ans_2_2 = (decimal?)members_2.First(n => n.Name.StartsWith("volume_he_budgetcons_p" + steam_parameter_id + "_per_2")).GetValue(majorModel) ?? 0;
				var ans_2_3 = (decimal?)members_2.First(n => n.Name.StartsWith("volume_he_public_p" + steam_parameter_id + "_per_2")).GetValue(majorModel) ?? 0;
				var ans_2_4 = (decimal?)members_2.First(n => n.Name.StartsWith("volume_he_other_p" + steam_parameter_id + "_per_2")).GetValue(majorModel) ?? 0;

				var outer_1 = GetValue_CalculateWeightedTariffInvestComponent_TariffsHESteam(members_1, propertyName, majorModel, 1, steam_parameter_id);
				var outer_2 = GetValue_CalculateWeightedTariffInvestComponent_TariffsHESteam(members_2, propertyName, majorModel, 2, steam_parameter_id);
				var ans_finally = (outer_1 * (ans_1_2 + ans_1_3 + ans_1_4) + outer_2 * (ans_2_2 + ans_2_3 + ans_2_4)) / (ans_1_2 + ans_1_3 + ans_1_4 + ans_2_2 + ans_2_3 + ans_2_4);

				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_CalculateHeatEnergyTariff_TariffsHESteam(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffSteam majorModel, short report_period_id, short steam_parameter_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"2")).ToList();
				var ans_1_1 = (decimal?)members_1.First(n => n.Name.StartsWith("volume_he_" + propertyName + "_p" + steam_parameter_id + "_per_1")).GetValue(majorModel) ?? 0;
				var ans_2_1 = (decimal?)members_2.First(n => n.Name.StartsWith("volume_he_" + propertyName + "_p" + steam_parameter_id + "_per_2")).GetValue(majorModel) ?? 0;
				var outer_1 = GetValue_CalculateHeatEnergyTariff_TariffsHESteam(members_1, propertyName, majorModel, 1, steam_parameter_id);
				var outer_2 = GetValue_CalculateHeatEnergyTariff_TariffsHESteam(members_2, propertyName, majorModel, 2, steam_parameter_id);
				var ans_finally = (outer_1 * ans_1_1 + outer_2 * ans_2_1) / (ans_1_1 + ans_2_1);

				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith("tariff_" + propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_CalculateHeatEnergyVolume_TariffsHESteam(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffSteam majorModel, short report_period_id, short steam_parameter_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"2")).ToList();
				var outer_1 = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members_1, propertyName, majorModel, 1, steam_parameter_id);
				var outer_2 = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members_2, propertyName, majorModel, 2, steam_parameter_id);
				var ans_finally = outer_1 + outer_2;

				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith("volume_he_" + propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public void SetExistModel_TariffsHESteam(TZ_TariffsHESteam_FactPlan minorModel, HeatEnergyTariffSteam majorModel, short report_period_id, short steam_parameter_id)
		{
			var members = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("p" + $"{steam_parameter_id}" + "_per_" + $"{report_period_id}")).ToList();

			minorModel.tariff_econom_justified = CalculateEconomJustified_TariffsHESteam(members, "tariff_econom_justified", majorModel, report_period_id, steam_parameter_id);
			minorModel.useful_release_ownprod_needs = GetValue_TariffsHESteam(members, "useful_release_ownprod_needs", majorModel, report_period_id, steam_parameter_id);
			minorModel.useful_release_household_needs = GetValue_TariffsHESteam(members, "useful_release_household_needs", majorModel, report_period_id, steam_parameter_id);
			minorModel.tariff_weighted_invest = GetValue_CalculateWeightedTariffInvestComponent_TariffsHESteam(members, "tariff_weighted_invest", majorModel, report_period_id, steam_parameter_id);
			minorModel.invest_component = GetValue_CalculateWeightedTariffInvestComponent_TariffsHESteam(members, "invest_component", majorModel, report_period_id, steam_parameter_id);
			minorModel.tariff_tso = GetValue_CalculateHeatEnergyTariff_TariffsHESteam(members, "tso", majorModel, report_period_id, steam_parameter_id);
			minorModel.volume_he_tso = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "tso", majorModel, report_period_id, steam_parameter_id);
			minorModel.tariff_budgetcons = GetValue_CalculateHeatEnergyTariff_TariffsHESteam(members, "budgetcons", majorModel, report_period_id, steam_parameter_id);
			minorModel.volume_he_budgetcons = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "budgetcons", majorModel, report_period_id, steam_parameter_id);
			minorModel.tariff_public = GetValue_CalculateHeatEnergyTariff_TariffsHESteam(members, "public", majorModel, report_period_id, steam_parameter_id);
			minorModel.volume_he_public = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "public", majorModel, report_period_id, steam_parameter_id);
			minorModel.tariff_other = GetValue_CalculateHeatEnergyTariff_TariffsHESteam(members, "other", majorModel, report_period_id, steam_parameter_id);
			minorModel.volume_he_other = GetValue_CalculateHeatEnergyVolume_TariffsHESteam(members, "other", majorModel, report_period_id, steam_parameter_id);
			minorModel.create_date = DateTime.Now;
			minorModel.user_id = userId;
		}

		[NonAction]
		public async Task SetAllParameters_TariffsHESteam(HeatEnergyTariffSteam model)
		{
			for(short steamParam = 1; steamParam <= 4; steamParam++)
				for (short period = 1; period <= 3; period++)
				{
					var tz_upd_1 = await _context.TZ_TariffsHESteam_FactPlan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status && model.perspective_year == x.perspective_year
									&& x.report_period_id == period && x.steam_parameter_id == steamParam).FirstOrDefaultAsync();

					if (tz_upd_1 != null)
					{
						SetExistModel_TariffsHESteam(tz_upd_1, model, period, steamParam);
						_context.TZ_TariffsHESteam_FactPlan.Update(tz_upd_1);
					}
					else
					{
						var new_tz_bal = GetNewTZTariffsHESteamModel(model, period, steamParam);
						await _context.TZ_TariffsHESteam_FactPlan.AddAsync(new_tz_bal);
					}					
				}

			await _context.SaveChangesAsync();
		}

		#endregion

		#region Модель для добавления новой записи или редактирования существующей. Тариф на тепловую энергию в горячей воде, руб./Гкал без НДС. Двухставочный тариф, без НДС

		//Создание модели для добавления новой записи или редактирования существующей. Тариф на тепловую энергию в паре (одноставочный), руб./Гкал без НДС
		[NonAction]
		public TZ_TariffsDoubleHE_Plan GetNewTZTwoStageTariffModel_Plan(TwoStageTariffUnit model, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById(consumer_type_id);
			var members = model.GetType().GetProperties().Where(n => n.Name.EndsWith($"{consumer_type_string}" + "_per_" + $"{report_period_id}")).ToList();

			var new_tz_bal = new TZ_TariffsDoubleHE_Plan()
			{
				tz_id = model.tz_id,
				data_status = model.data_status,
				perspective_year = model.perspective_year,
				report_period_id = report_period_id,
				consumer_type_id = consumer_type_id,
				convers_doublerate_to_singlerate = GetValue_TwoStageTariff_SetYearValueDoubleStage_1_2(members, "convers_twostage_to_singlestage", model, report_period_id, consumer_type_id),
				doublerate_heat_energy = GetValue_TwoStageTariff_SetYearValueDoubleStage_1_2(members, "heatenergy_doublerate_rate", model, report_period_id, consumer_type_id),
				volume_heat_energy = GetValue_TwoStageTariff_SetYearValueDoubleStage_3(members, "heatenergy_doublerate_volume", model, report_period_id, consumer_type_id),
				doublerate_heat_load = GetValue_TwoStageTariff_SetYearValueDoubleStage_4(members, "heatload_doublerate_rate", model, report_period_id, consumer_type_id),
				volume_heat_load = GetValue_TwoStageTariff_SetYearValueDoubleStage_5(members, "heatload_doublerate_value", model, report_period_id, consumer_type_id),
				
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		[NonAction]
		public TZ_TariffsDoubleHE_Fact GetNewTZTwoStageTariffModel_Fact(TwoStageTariffUnit model, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById(consumer_type_id);
			var members = model.GetType().GetProperties().Where(n => n.Name.EndsWith($"{consumer_type_string}" + "_per_" + $"{report_period_id}")).ToList();

			var new_tz_bal = new TZ_TariffsDoubleHE_Fact()
			{
				tz_id = model.tz_id,
				data_status = model.data_status,
				report_period_id = report_period_id,
				consumer_type_id = consumer_type_id,
				amount_grants_budget = GetValue_TwoStageTariff_SetYearValueDoubleStage_6(members, "amount_grants", model, report_period_id, consumer_type_id),

				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		[NonAction]
		public string? GetComsumerTypeById(short id)
		{
			var consumer_type_list = new Dictionary<short, string>();
			consumer_type_list.Add(1, "_budget");
			consumer_type_list.Add(2, "_public");
			consumer_type_list.Add(3, "_other");

			return consumer_type_list.GetValueOrDefault(id);
		}

		[NonAction]
		public decimal? GetValue_TwoStageTariff(IEnumerable<PropertyInfo> members, string propertyName, TwoStageTariffUnit majorModel, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById(consumer_type_id);

			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var first = GetValue_TwoStageTariff(members_1, propertyName, majorModel, 1, consumer_type_id);
				var second = GetValue_TwoStageTariff(members_2, propertyName, majorModel, 2, consumer_type_id);
				return first + second;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName + consumer_type_string)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_TwoStageTariff_SetYearValueDoubleStage_1_2(IEnumerable<PropertyInfo> members, string propertyName, TwoStageTariffUnit majorModel, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById(consumer_type_id);

			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();
				
				var mul_1 = GetValue_TwoStageTariff_SetYearValueDoubleStage_1_2(members_1, $"heatenergy_doublerate_volume", majorModel, 1, consumer_type_id);
				var mul_2 = GetValue_TwoStageTariff_SetYearValueDoubleStage_1_2(members_2, $"heatenergy_doublerate_volume", majorModel, 2, consumer_type_id);
				var fp_1 = GetValue_TwoStageTariff_SetYearValueDoubleStage_1_2(members_1, propertyName, majorModel, 1, consumer_type_id);
				var fp_2 = GetValue_TwoStageTariff_SetYearValueDoubleStage_1_2(members_2, propertyName, majorModel, 2, consumer_type_id);
				var ans_3 = (fp_1 * mul_1 + mul_2 * fp_2) / (mul_1 + mul_2);

				return ans_3;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName + consumer_type_string)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_TwoStageTariff_SetYearValueDoubleStage_3(IEnumerable<PropertyInfo> members, string propertyName, TwoStageTariffUnit majorModel, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById(consumer_type_id);

			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();
				var mul_1 = GetValue_TwoStageTariff_SetYearValueDoubleStage_3(members_1, $"heatenergy_doublerate_volume", majorModel, 1, consumer_type_id);
				var mul_2 = GetValue_TwoStageTariff_SetYearValueDoubleStage_3(members_2, $"heatenergy_doublerate_volume", majorModel, 2, consumer_type_id);
				var ans_3 = mul_1 + mul_2;

				return ans_3;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName + consumer_type_string)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_TwoStageTariff_SetYearValueDoubleStage_4(IEnumerable<PropertyInfo> members, string propertyName, TwoStageTariffUnit majorModel, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById(consumer_type_id);

			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();
				var mul_1 = GetValue_TwoStageTariff_SetYearValueDoubleStage_4(members_1, $"heatload_doublerate_value", majorModel, 1, consumer_type_id);
				var mul_2 = GetValue_TwoStageTariff_SetYearValueDoubleStage_4(members_2, $"heatload_doublerate_value", majorModel, 2, consumer_type_id);
				var fp_1 = GetValue_TwoStageTariff_SetYearValueDoubleStage_4(members_1, propertyName, majorModel, 1, consumer_type_id);
				var fp_2 = GetValue_TwoStageTariff_SetYearValueDoubleStage_4(members_2, propertyName, majorModel, 2, consumer_type_id);
				var ans_3 = (fp_1 * mul_1 + mul_2 * fp_2) / ((mul_1 + mul_2) / 2);

				return ans_3;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName + consumer_type_string)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_TwoStageTariff_SetYearValueDoubleStage_5(IEnumerable<PropertyInfo> members, string propertyName, TwoStageTariffUnit majorModel, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById(consumer_type_id);

			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();
				var mul_1 = GetValue_TwoStageTariff_SetYearValueDoubleStage_5(members_1, $"heatload_doublerate_value", majorModel, 1, consumer_type_id);
				var mul_2 = GetValue_TwoStageTariff_SetYearValueDoubleStage_5(members_2, $"heatload_doublerate_value", majorModel, 2, consumer_type_id);
				var ans_3 = (mul_1 + mul_2) / 2;

				return ans_3;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName + consumer_type_string)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_TwoStageTariff_SetYearValueDoubleStage_6(IEnumerable<PropertyInfo> members, string propertyName, TwoStageTariffUnit majorModel, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById(consumer_type_id);

			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();
				var mul_1 = GetValue_TwoStageTariff_SetYearValueDoubleStage_6(members_1, $"amount_grants", majorModel, 1, consumer_type_id);
				var mul_2 = GetValue_TwoStageTariff_SetYearValueDoubleStage_6(members_2, $"amount_grants", majorModel, 2, consumer_type_id);
				var ans_3 = mul_1 + mul_2;

				return ans_3;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName + consumer_type_string)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public void SetExistModel_TwoStageTariff(TZ_TariffsDoubleHE_Plan minorPlan, TZ_TariffsDoubleHE_Fact minorFact, TwoStageTariffUnit majorModel, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById(consumer_type_id);
			var members = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith(consumer_type_string + "_per_" + $"{report_period_id}")).ToList();

			minorPlan.convers_doublerate_to_singlerate = GetValue_TwoStageTariff_SetYearValueDoubleStage_1_2(members, "convers_twostage_to_singlestage", majorModel, report_period_id, consumer_type_id);
			minorPlan.doublerate_heat_energy = GetValue_TwoStageTariff_SetYearValueDoubleStage_1_2(members, "heatenergy_doublerate_rate", majorModel, report_period_id, consumer_type_id);
			minorPlan.volume_heat_energy = GetValue_TwoStageTariff_SetYearValueDoubleStage_3(members, "heatenergy_doublerate_volume", majorModel, report_period_id, consumer_type_id);
			minorPlan.doublerate_heat_load = GetValue_TwoStageTariff_SetYearValueDoubleStage_4(members, "heatload_doublerate_rate", majorModel, report_period_id, consumer_type_id);
			minorPlan.volume_heat_load = GetValue_TwoStageTariff_SetYearValueDoubleStage_5(members, "heatload_doublerate_value", majorModel, report_period_id, consumer_type_id);
			minorFact.amount_grants_budget = GetValue_TwoStageTariff_SetYearValueDoubleStage_6(members, "amount_grants", majorModel, report_period_id, consumer_type_id);

			minorPlan.create_date = DateTime.Now;
			minorPlan.user_id = userId;
		}

		[NonAction]
		public async Task SetAllParameters_TwoStageTariff(TwoStageTariffUnit model)
		{
			for (short steamParam = 1; steamParam <= 3; steamParam++)
				for (short period = 1; period <= 3; period++)
				{
					var tz_upd_plan = await _context.TZ_TariffsDoubleHE_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status && model.perspective_year == x.perspective_year
									&& x.report_period_id == period && x.consumer_type_id == steamParam).FirstOrDefaultAsync();

					var tz_upd_fact = await _context.TZ_TariffsDoubleHE_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
									&& x.report_period_id == period && x.consumer_type_id == steamParam).FirstOrDefaultAsync();

					if (tz_upd_plan != null)
					{
						SetExistModel_TwoStageTariff(tz_upd_plan, tz_upd_fact, model, period, steamParam);
						_context.TZ_TariffsDoubleHE_Fact.Update(tz_upd_fact);
						_context.TZ_TariffsDoubleHE_Plan.Update(tz_upd_plan);
					}
					else
					{
						var new_tz_plan = GetNewTZTwoStageTariffModel_Plan(model, period, steamParam);
						var new_tz_fact = GetNewTZTwoStageTariffModel_Fact(model, period, steamParam);
						await _context.TZ_TariffsDoubleHE_Fact.AddAsync(new_tz_fact);
						await _context.TZ_TariffsDoubleHE_Plan.AddAsync(new_tz_plan);
					}
				}

			await _context.SaveChangesAsync();
		}

		#endregion

		#region Модель для добавления новой записи и редактирования существующей. Тариф на тепловую энергию в горячей воде. Одноставочный тариф

		//Создание модели для добавления новой записи или редактирования существующей. Тариф на тепловую энергию в горячей воде. Одноставочный тариф
		[NonAction]
		public TZ_TariffsSingleHE_FactPlan GetNewTZTariffsSingleHEModel(SingleStageTariffUnit model, short report_period_id)
		{
			var members = model.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"{report_period_id}")).ToList();

			var new_tz_bal = new TZ_TariffsSingleHE_FactPlan()
			{
				tz_id = model.tz_id,
				data_status = model.data_status,
				perspective_year = model.perspective_year,
				report_period_id = report_period_id,
				tariff_tso = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members, "tso", model, report_period_id),
				tariff_budgetcons = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members, "budgetcons", model, report_period_id),
				tariff_public = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members, "public", model, report_period_id),
				tariff_other = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members, "other", model, report_period_id),

				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		[NonAction]
		public decimal? GetValue_TariffsSingleHE(IEnumerable<PropertyInfo> members, string propertyName, SingleStageTariffUnit majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var first = GetValue_TariffsSingleHE(members_1, propertyName, majorModel, 1);
				var second = GetValue_TariffsSingleHE(members_2, propertyName, majorModel, 2);
				return first + second;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(IEnumerable<PropertyInfo> members, string propertyName, SingleStageTariffUnit majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var fp_1 = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members_1, propertyName, majorModel, 1);
				var fp_2 = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members_2, propertyName, majorModel, 2);
				decimal? mul_1 = 0;
				decimal? mul_2 = 0;

				if(propertyName == "tso")
				{
					mul_1 = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members_1, "useful_release_tso_salers_plan_per_1", majorModel, 1);
					mul_2 = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members_2, "useful_release_tso_salers_plan_per_2", majorModel, 2);
				}
				else if (propertyName == "budgetcons")
				{
					mul_1 = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members_1, "useful_release_buget_cons_plan_per_1", majorModel, 1);
					mul_2 = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members_2, "useful_release_buget_cons_plan_per_2", majorModel, 2);
				}
				else if (propertyName == "public")
				{
					mul_1 = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members_1, "useful_release_public_plan_per_1", majorModel, 1);
					mul_2 = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members_2, "useful_release_public_plan_per_2", majorModel, 2);
				}
				else if (propertyName == "other")
				{
					mul_1 = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members_1, "useful_release_other_cons_plan_per_1", majorModel, 1);
					mul_2 = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members_2, "useful_release_other_cons_plan_per_2", majorModel, 2);
				}


				var fp_3 = (fp_1 * mul_1 + mul_2 * fp_2) / (mul_1 + mul_2);
				return fp_3;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public void SetExistModel_TariffsSingleHE(TZ_TariffsSingleHE_FactPlan minorModel, SingleStageTariffUnit majorModel, short report_period_id)
		{
			var members = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"{report_period_id}")).ToList();

			minorModel.tariff_tso = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members, "tso", majorModel, report_period_id);
			minorModel.tariff_budgetcons = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members, "budgetcons", majorModel, report_period_id);
			minorModel.tariff_public = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members, "public", majorModel, report_period_id);
			minorModel.tariff_other = GetValue_TariffsSingleHE_SetYearValueSingleStageTariffUnit(members, "other", majorModel, report_period_id);

			minorModel.create_date = DateTime.Now;
			minorModel.user_id = userId;
		}

		[NonAction]
		public async Task SetAllParameters_TariffsSingleHE(SingleStageTariffUnit model)
		{
				for (short period = 1; period <= 3; period++)
				{
					var tz_upd_1 = await _context.TZ_TariffsSingleHE_FactPlan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status && model.perspective_year == x.perspective_year
									&& x.report_period_id == period).FirstOrDefaultAsync();

					if (tz_upd_1 != null)
					{
					SetExistModel_TariffsSingleHE(tz_upd_1, model, period);
						_context.TZ_TariffsSingleHE_FactPlan.Update(tz_upd_1);
					}
					else
					{
						var new_tz_bal = GetNewTZTariffsSingleHEModel(model, period);
						await _context.TZ_TariffsSingleHE_FactPlan.AddAsync(new_tz_bal);
					}
				}

			await _context.SaveChangesAsync();
		}

		#endregion

		#region Модель для добавления новой записи и редактирования существующей. Тариф на тепловую энергию в горячей воде. Главное меню

		//Создание модели для добавления новой записи или редактирования существующей. Тариф на тепловую энергию в горячей воде. Главное меню
		[NonAction]
		public TZ_TariffsSingleHE_FactPlan GetNewTZHeatEnergyTariffWaterModel(HeatEnergyTariffWater model, short report_period_id)
		{
			var members = model.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"{report_period_id}")).ToList();

			var new_tz_bal = new TZ_TariffsSingleHE_FactPlan()
			{
				tz_id = model.tz_id,
				data_status = model.data_status,
				perspective_year = model.perspective_year,
				report_period_id = report_period_id,
				tariff_econom_justified = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members, "econom_justified", model, report_period_id),
				tariff_weighted_invest = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members, "weighted_invest", model, report_period_id),
				invest_component = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members, "invest_component", model, report_period_id),

				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		[NonAction]
		public decimal? GetValue_HeatEnergyTariffWater(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffWater majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var first = GetValue_HeatEnergyTariffWater(members_1, propertyName, majorModel, 1);
				var second = GetValue_HeatEnergyTariffWater(members_2, propertyName, majorModel, 2);
				return first + second;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}


		[NonAction]
		public decimal? GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffWater majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var outer_1 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, $"heat_energy_tariff_water_{propertyName}_per_1", majorModel, 1);
				var outer_2 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, $"heat_energy_tariff_water_{propertyName}_per_2", majorModel, 2);

				var mul_1_1 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, "heat_energy_tariff_water_useful_release_tso_salers_plan_per_1", majorModel, 1);
				var mul_1_2 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, "heat_energy_tariff_water_useful_release_buget_cons_plan_per_1", majorModel, 1);
				var mul_1_3 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, "heat_energy_tariff_water_useful_release_public_plan_per_1", majorModel, 1);
				var mul_1_4 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, "heat_energy_tariff_water_useful_release_other_cons_plan_per_1", majorModel, 1);
				var mul_2_1 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, "heat_energy_tariff_water_useful_release_tso_salers_plan_per_2", majorModel, 2);
				var mul_2_2 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, "heat_energy_tariff_water_useful_release_buget_cons_plan_per_2", majorModel, 2);
				var mul_2_3 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, "heat_energy_tariff_water_useful_release_public_plan_per_2", majorModel, 2);
				var mul_2_4 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, "heat_energy_tariff_water_useful_release_other_cons_plan_per_2", majorModel, 2);

				decimal? ans_finally = 0;

				if(propertyName == "econom_justified")
					ans_finally = (outer_1 * (mul_1_1 + mul_1_2 + mul_1_3 + mul_1_4) + outer_2 * (mul_2_1 + mul_2_2 + mul_2_3 + mul_2_4)) / (mul_1_1 + mul_1_2 + mul_1_3 + mul_1_4 + mul_2_1 + mul_2_2 + mul_2_3 + mul_2_4);
				else if (propertyName == "invest_component")
					ans_finally = (outer_1 * (mul_1_2 + mul_1_3 + mul_1_4) + outer_2 * (mul_2_2 + mul_2_3 + mul_2_4)) / (mul_1_2 + mul_1_3 + +mul_1_4 + mul_2_2 + mul_2_3 + mul_2_4);
				else if (propertyName == "weighted_invest")
				{
					var mux_1_1 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, "heat_energy_tariff_water_convers_doublerate_to_singlerate_budjet_per_1", majorModel, 1);
					var multy_1_1 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, "heat_energy_tariff_water_volume_heat_energy_budjet_per_1", majorModel, 1);

					var mux_1_2 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, "heat_energy_tariff_water_convers_doublerate_to_singlerate_public_per_1", majorModel, 1);
					var multy_1_2 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, "heat_energy_tariff_water_volume_heat_energy_public_per_1", majorModel, 1);

					var mux_1_3 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, "heat_energy_tariff_water_convers_doublerate_to_singlerate_other_per_1", majorModel, 1);
					var multy_1_3 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_1, "heat_energy_tariff_water_volume_heat_energy_other_per_1", majorModel, 1);

					var mux_2_1 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, "heat_energy_tariff_water_convers_doublerate_to_singlerate_budjet_per_2", majorModel, 2);
					var multy_2_1 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, "heat_energy_tariff_water_volume_heat_energy_budjet_per_2", majorModel, 2);

					var mux_2_2 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, "heat_energy_tariff_water_convers_doublerate_to_singlerate_public_per_2", majorModel, 2);
					var multy_2_2 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, "heat_energy_tariff_water_volume_heat_energy_public_per_2", majorModel, 2);

					var mux_2_3 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, "heat_energy_tariff_water_convers_doublerate_to_singlerate_other_per_2", majorModel, 2);
					var multy_2_3 = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members_2, "heat_energy_tariff_water_volume_heat_energy_other_per_2", majorModel, 2);

					ans_finally = (outer_1 * (mul_1_2 + mul_1_3 + mul_1_4 + mux_1_1 * multy_1_1 + mux_1_2 * multy_1_2 + mux_1_3 * multy_1_3)
		   + outer_2 * (mul_2_2 + mul_2_3 + mul_2_4 + mux_2_1 * multy_2_1 + mux_2_2 * multy_2_2 + mux_2_3 * multy_2_3)) / (mul_1_2 + mul_1_3 + mul_1_4 + mul_2_2 + mul_2_3 + mul_2_4);
				}

					return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public void SetExistModel_HeatEnergyTariffWater(TZ_TariffsSingleHE_FactPlan minorModel, HeatEnergyTariffWater majorModel, short report_period_id)
		{
			var members = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"{report_period_id}")).ToList();

			minorModel.tariff_econom_justified = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members, "econom_justified", majorModel, report_period_id);
			minorModel.tariff_weighted_invest = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members, "weighted_invest", majorModel, report_period_id);
			minorModel.invest_component = GetValue_HeatEnergyTariffWater_SetYearValueMainTariffUnit(members, "invest_component", majorModel, report_period_id);

			minorModel.create_date = DateTime.Now;
			minorModel.user_id = userId;
		}

		[NonAction]
		public async Task SetAllParameters_HeatEnergyTariffWater(HeatEnergyTariffWater model)
		{
			for (short period = 1; period <= 3; period++)
			{
				var tz_upd_1 = await _context.TZ_TariffsSingleHE_FactPlan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status && model.perspective_year == x.perspective_year
								&& x.report_period_id == period).FirstOrDefaultAsync();

				if (tz_upd_1 != null)
				{
					SetExistModel_HeatEnergyTariffWater(tz_upd_1, model, period);
					_context.TZ_TariffsSingleHE_FactPlan.Update(tz_upd_1);
				}
				else
				{
					var new_tz_bal = GetNewTZHeatEnergyTariffWaterModel(model, period);
					await _context.TZ_TariffsSingleHE_FactPlan.AddAsync(new_tz_bal);
				}
			}

			await _context.SaveChangesAsync();
		}

		#endregion

		#region Модель для добавления новой записи и редактирования существующей. Тариф на передачу тепловой энергии, руб./Гкал без НДС

		//Создание модели для добавления новой записи или редактирования существующей. Тариф на передачу тепловой энергии, руб./Гкал без НДС
		[NonAction]
		public TZ_TariffsHETransfer_FactPlan GetNewTZHeatEnergyTransferModel(HeatEnergyTransfer model, short report_period_id)
		{
			var members = model.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"{report_period_id}")).ToList();

			var new_tz_bal = new TZ_TariffsHETransfer_FactPlan()
			{
				tz_id = model.tz_id,
				data_status = model.data_status,
				perspective_year = model.perspective_year,
				report_period_id = report_period_id,
				tariff_weighted_invest = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members, "tariff_weighted_invest", model, report_period_id),
				invest_component = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members, "invest_component", model, report_period_id),
				tariff_org_needs = GetValue_HeatEnergyTransfer_CalculateOrganizationTransfer(members, "household_needs_tariff", model, report_period_id),
				tariff_tso = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members, "tariff_tso", model, report_period_id, "tso_salers"),
				tariff_budgetcons = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members, "tariff_budgetcons", model, report_period_id, "buget_cons"),
				tariff_public = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members, "tariff_public", model, report_period_id, "public"),
				tariff_other = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members, "tariff_other", model, report_period_id, "other_cons"),

				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		[NonAction]
		public decimal? GetValue_HeatEnergyTransfer(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTransfer majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var first = GetValue_HeatEnergyTransfer(members_1, propertyName, majorModel, 1);
				var second = GetValue_HeatEnergyTransfer(members_2, propertyName, majorModel, 2);
				return first + second;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTransfer majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var outer_1 = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members_1, $"he_transfer_{propertyName}_per_1", majorModel, 1);
				var outer_2 = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members_2, $"he_transfer_{propertyName}_per_2", majorModel, 2);

				var mul_1_1 = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members_1, "he_transfer_useful_release_buget_cons_plan_per_1", majorModel, 1);
				var mul_1_2 = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members_1, "he_transfer_useful_release_public_plan_per_1", majorModel, 1);
				var mul_1_3 = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members_1, "he_transfer_useful_release_other_cons_plan_per_1", majorModel, 1);
				var mul_2_1 = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members_2, "he_transfer_useful_release_buget_cons_plan_per_2", majorModel, 2);
				var mul_2_2 = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members_2, "he_transfer_useful_release_public_plan_per_2", majorModel, 2);
				var mul_2_3 = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members_2, "he_transfer_useful_release_other_cons_plan_per_2", majorModel, 2);

				var ans_finally = (outer_1 * (mul_1_1 + mul_1_2 + mul_1_3) + outer_2 * (mul_2_1 + mul_2_2 + mul_2_3)) / (mul_1_1 + mul_1_2 + mul_1_3 + mul_2_1 + mul_2_2 + mul_2_3);

				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_HeatEnergyTransfer_CalculateOrganizationTransfer(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTransfer majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var outer_1 = GetValue_HeatEnergyTransfer_CalculateOrganizationTransfer(members_1, "he_transfer_household_needs_tariff_per_1", majorModel, 1);
				var outer_2 = GetValue_HeatEnergyTransfer_CalculateOrganizationTransfer(members_2, "he_transfer_household_needs_tariff_per_2", majorModel, 2);

				var mul_1_1 = GetValue_HeatEnergyTransfer_CalculateOrganizationTransfer(members_1, "he_transfer_useful_release_ownprod_plan_per_1", majorModel, 1);
				var mul_1_2 = GetValue_HeatEnergyTransfer_CalculateOrganizationTransfer(members_1, "he_transfer_useful_release_household_needs_plan_per_1", majorModel, 1);
				var mul_2_1 = GetValue_HeatEnergyTransfer_CalculateOrganizationTransfer(members_2, "he_transfer_useful_release_ownprod_plan_per_2", majorModel, 2);
				var mul_2_2 = GetValue_HeatEnergyTransfer_CalculateOrganizationTransfer(members_2, "he_transfer_useful_release_household_needs_plan_per_2", majorModel, 2);

				var ans_finally = (outer_1 * (mul_1_1 + mul_1_2) + outer_2 * (mul_2_1 + mul_2_2)) / (mul_1_1 + mul_1_2 + mul_2_1 + mul_2_2);

				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTransfer majorModel, short report_period_id, string argument_type)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var outer_1 = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members_1, $"he_transfer_{propertyName}_per_1", majorModel, 1, argument_type);
				var outer_2 = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members_2, $"he_transfer_{propertyName}_per_2", majorModel, 2, argument_type);

				var mul_1 = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members_1, $"he_transfer_useful_release_{argument_type}_plan_per_1", majorModel, 1, argument_type);
				var mul_2 = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members_2, $"he_transfer_useful_release_{argument_type}_plan_per_2", majorModel, 2, argument_type);

				var ans_finally = (outer_1 * mul_1 + mul_2 * outer_2) / (mul_1 + mul_2);

				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}


		[NonAction]
		public void SetExistModel_HeatEnergyTransfer(TZ_TariffsHETransfer_FactPlan minorModel, HeatEnergyTransfer majorModel, short report_period_id)
		{
			var members = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"{report_period_id}")).ToList();

			minorModel.tariff_weighted_invest = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members, "tariff_weighted_invest", majorModel, report_period_id);
			minorModel.invest_component = GetValue_HeatEnergyTransfer_CalculateHeatEnergyTransferTariffWeightedInvest(members, "invest_component", majorModel, report_period_id);
			minorModel.tariff_org_needs = GetValue_HeatEnergyTransfer_CalculateOrganizationTransfer(members, "household_needs_tariff", majorModel, report_period_id);
			minorModel.tariff_tso = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members, "tariff_tso", majorModel, report_period_id, "tso_salers");
			minorModel.tariff_budgetcons = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members, "tariff_budgetcons", majorModel, report_period_id, "buget_cons");
			minorModel.tariff_public = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members, "tariff_public", majorModel, report_period_id, "public");
			minorModel.tariff_other = GetValue_HeatEnergyTransfer_CalculateTsoBudgetPublicOther(members, "tariff_other", majorModel, report_period_id, "other_cons");

			minorModel.create_date = DateTime.Now;
			minorModel.user_id = userId;
		}

		[NonAction]
		public async Task SetAllParameters_HeatEnergyTransfer(HeatEnergyTransfer model)
		{
			for (short period = 1; period <= 3; period++)
			{
				var tz_upd_1 = await _context.TZ_TariffsHETransfer_FactPlan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status && model.perspective_year == x.perspective_year
								&& x.report_period_id == period).FirstOrDefaultAsync();

				if (tz_upd_1 != null)
				{
					SetExistModel_HeatEnergyTransfer(tz_upd_1, model, period);
					_context.TZ_TariffsHETransfer_FactPlan.Update(tz_upd_1);
				}
				else
				{
					var new_tz_bal = GetNewTZHeatEnergyTransferModel(model, period);
					await _context.TZ_TariffsHETransfer_FactPlan.AddAsync(new_tz_bal);
				}
			}

			await _context.SaveChangesAsync();
		}

		#endregion

		#region Модель для добавления новой записи или редактирования существующей. Тариф ЕТО / Тариф с субсидир. / Льготный тариф, руб./Гкал без НДС

		//Создание модели для добавления новой записи или редактирования существующей. Тариф ЕТО / Тариф с субсидир. / Льготный тариф, руб./Гкал без НДС
		[NonAction]
		public TZ_TariffsOthers_Fact GetNewTZETO_Amount_PreferenceModel(ETO_Amount_Preference model, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById_ETO_Amount_Preference(consumer_type_id);
			var members = model.GetType().GetProperties().Where(n => n.Name.EndsWith($"{consumer_type_string}" + "_per_" + $"{report_period_id}")).ToList();

			var new_tz_bal = new TZ_TariffsOthers_Fact()
			{
				tz_id = model.tz_id,
				data_status = model.data_status,
				report_period_id = report_period_id,
				consumer_type_id = consumer_type_id,
				tariff_eto = GetValue_ETO_Amount_Preference(members, "tariff_eto", model, report_period_id, consumer_type_id),
				tariff_compensation = GetValue_ETO_Amount_Preference(members, "tariff_compensation", model, report_period_id, consumer_type_id),
				tariff_preferential = GetValue_ETO_Amount_Preference(members, "tariff_preferential", model, report_period_id, consumer_type_id),

				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		[NonAction]
		public string? GetComsumerTypeById_ETO_Amount_Preference(short id)
		{
			var consumer_type_list = new Dictionary<short, string>();

			consumer_type_list.Add(1, "_budgetcons");
			consumer_type_list.Add(2, "_public");
			consumer_type_list.Add(3, "_other");

			return consumer_type_list.GetValueOrDefault(id);
		}

		[NonAction]
		public string? GetComsumerTypeById_ETO_Amount_Preference_3_Period(short id)
		{
			var consumer_type_list = new Dictionary<short, string>();

			consumer_type_list.Add(1, "buget_cons");
			consumer_type_list.Add(2, "public_cons");
			consumer_type_list.Add(3, "other_cons");

			return consumer_type_list.GetValueOrDefault(id);
		}

		[NonAction]
		public decimal? GetValue_ETO_Amount_Preference(IEnumerable<PropertyInfo> members, string propertyName, ETO_Amount_Preference majorModel, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById_ETO_Amount_Preference(consumer_type_id);

			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var outer_1 = GetValue_ETO_Amount_Preference(members_1, propertyName, majorModel, 1, consumer_type_id);
				var outer_2 = GetValue_ETO_Amount_Preference(members_2, propertyName, majorModel, 2, consumer_type_id);

				var mul_1 = GetValue_ETO_Amount_Preference_ThirdPeriod(members_1, $"eto_amount_preference_twostage_useful_release_", majorModel, 1, consumer_type_id);
				var mul_2 = GetValue_ETO_Amount_Preference_ThirdPeriod(members_2, $"eto_amount_preference_twostage_useful_release_", majorModel, 2, consumer_type_id);

				var ans_finally = (outer_1 * mul_1 + outer_2 * mul_2) / (mul_1 + mul_2);
				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName + consumer_type_string)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_ETO_Amount_Preference_ThirdPeriod(IEnumerable<PropertyInfo> members, string propertyName, ETO_Amount_Preference majorModel, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById_ETO_Amount_Preference_3_Period(consumer_type_id);

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName + consumer_type_string)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public void SetExistModel_ETO_Amount_Preference(TZ_TariffsOthers_Fact minorFact, ETO_Amount_Preference majorModel, short report_period_id, short consumer_type_id)
		{
			var consumer_type_string = GetComsumerTypeById_ETO_Amount_Preference(consumer_type_id);
			var members = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith(consumer_type_string + "_per_" + $"{report_period_id}")).ToList();

			minorFact.tz_id = majorModel.tz_id;
			minorFact.data_status = majorModel.data_status;
			minorFact.report_period_id = report_period_id;
			minorFact.consumer_type_id = consumer_type_id;
			minorFact.tariff_eto = GetValue_ETO_Amount_Preference(members, "tariff_eto", majorModel, report_period_id, consumer_type_id);
			minorFact.tariff_compensation = GetValue_ETO_Amount_Preference(members, "tariff_compensation", majorModel, report_period_id, consumer_type_id);
			minorFact.tariff_preferential = GetValue_ETO_Amount_Preference(members, "tariff_preferential", majorModel, report_period_id, consumer_type_id);

			minorFact.create_date = DateTime.Now;
			minorFact.user_id = userId;
		}

		[NonAction]
		public async Task SetAllParameters_ETO_Amount_Preference(ETO_Amount_Preference model)
		{
			for (short steamParam = 1; steamParam <= 3; steamParam++)
				for (short period = 1; period <= 3; period++)
				{
					var tz_upd_plan = await _context.TZ_TariffsOthers_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
									&& x.report_period_id == period && x.consumer_type_id == steamParam).FirstOrDefaultAsync();

					if (tz_upd_plan != null)
					{
						SetExistModel_ETO_Amount_Preference(tz_upd_plan, model, period, steamParam);
						_context.TZ_TariffsOthers_Fact.Update(tz_upd_plan);
					}
					else
					{
						var new_tz_fact = GetNewTZETO_Amount_PreferenceModel(model, period, steamParam);
						await _context.TZ_TariffsOthers_Fact.AddAsync(new_tz_fact);
					}
				}

			await _context.SaveChangesAsync();
		}

		#endregion

		#region Модель для добавления новой записи и редактирования существующей. Тариф на теплоноситель (горячая вода / пар) и тарифы на горячую воду, без НДС

		//Создание модели для добавления новой записи или редактирования существующей. Тариф на теплоноситель (горячая вода / пар) и тарифы на горячую воду, без НДС
		[NonAction]
		public TZ_TariffsHeatCarrier_Fact GetNewTZHeatEnergyTariffSteamHotWaterModel(HeatEnergyTariffSteamHotWater model, short report_period_id)
		{
			var members = model.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"{report_period_id}")).ToList();

			var new_tz_bal = new TZ_TariffsHeatCarrier_Fact()
			{
				tz_id = model.tz_id,
				data_status = model.data_status,
				report_period_id = report_period_id,
				tariff_hc_hw = GetValue_HeatEnergyTariffSteamHotWater_CalculateSteamHotWater(members, "hot_water_heat_carrier_tariff", model, report_period_id),
				tariff_hc_steam = GetValue_HeatEnergyTariffSteamHotWater_CalculateSteamHotWater(members, "steam_heat_carrier_tariff", model, report_period_id),
				tariff_single_component_hw = GetValue_HeatEnergyTariffSteamHotWater_CalculateSingleComponentTariffHotWater(members, "hot_water_tariff", model, report_period_id),
				tariff_double_component_hc = GetValue_HeatEnergyTariffSteamHotWater_CalculateSingleComponentTariffHotWater(members, "heat_carrier_component", model, report_period_id),
				tariff_double_component_he = GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(members, "heat_energy_component", model, report_period_id),

				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		[NonAction]
		public decimal? GetValue_HeatEnergyTariffSteamHotWater(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffSteamHotWater majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var first = GetValue_HeatEnergyTariffSteamHotWater(members_1, propertyName, majorModel, 1);
				var second = GetValue_HeatEnergyTariffSteamHotWater(members_2, propertyName, majorModel, 2);
				return first + second;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_HeatEnergyTariffSteamHotWater_CalculateSteamHotWater(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffSteamHotWater majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var outer_1 = GetValue_HeatEnergyTariffSteamHotWater_CalculateSteamHotWater(members_1, propertyName, majorModel, 1);
				var outer_2 = GetValue_HeatEnergyTariffSteamHotWater_CalculateSteamHotWater(members_2, propertyName, majorModel, 2);
				var mul_1 = GetValue_HeatEnergyTariffSteamHotWater_CalculateSteamHotWater(members_1, "he_tariff_steam_hotwater_volume_buy_heat_carrier_plan_per_1", majorModel, 1);
				var mul_2 = GetValue_HeatEnergyTariffSteamHotWater_CalculateSteamHotWater(members_2, "he_tariff_steam_hotwater_volume_buy_heat_carrier_plan_per_2", majorModel, 2);

				var ans_finally = (outer_1 * mul_1 + outer_2 * mul_2) / (mul_1 + mul_2);

				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public decimal? GetValue_HeatEnergyTariffSteamHotWater_CalculateSingleComponentTariffHotWater(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffSteamHotWater majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var outer_1 = GetValue_HeatEnergyTariffSteamHotWater_CalculateSingleComponentTariffHotWater(members_1, propertyName, majorModel, 1);
				var outer_2 = GetValue_HeatEnergyTariffSteamHotWater_CalculateSingleComponentTariffHotWater(members_2, propertyName, majorModel, 2);

				var ans_finally = (outer_1 + outer_2) / 2;

				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}


		[NonAction]
		public decimal? GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(IEnumerable<PropertyInfo> members, string propertyName, HeatEnergyTariffSteamHotWater majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var outer_1 = GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(members_1, "heat_energy_component_per_1", majorModel, 1);
				var outer_2 = GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(members_2, "heat_energy_component_per_2", majorModel, 2);
				var mul_1_1 = GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(members_1, "he_tariff_steam_hotwater_useful_release_buget_cons_plan_per_1", majorModel, 1);
				var mul_1_2 = GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(members_1, "he_tariff_steam_hotwater_useful_release_public_cons_plan_per_1", majorModel, 1);
				var mul_1_3 = GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(members_1, "he_tariff_steam_hotwater_useful_release_other_cons_plan_per_1", majorModel, 1);
				var mul_2_1 = GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(members_2, "he_tariff_steam_hotwater_useful_release_buget_cons_plan_per_2", majorModel, 2);
				var mul_2_2 = GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(members_2, "he_tariff_steam_hotwater_useful_release_public_cons_plan_per_2", majorModel, 2);
				var mul_2_3 = GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(members_2, "he_tariff_steam_hotwater_useful_release_other_cons_plan_per_2", majorModel, 2);
				var ans_finally = (outer_1 * (mul_1_1 + mul_1_2 + mul_1_3) + outer_2 * (mul_2_1 + mul_2_2 + mul_2_3)) / (mul_1_1 + mul_1_2 + mul_1_3 + mul_2_1 + mul_2_2 + mul_2_3);

				return ans_finally;
			}

			var ans = (decimal?)members.First(n => n.Name.StartsWith(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public void SetExistModel_HeatEnergyTariffSteamHotWater(TZ_TariffsHeatCarrier_Fact minorModel, HeatEnergyTariffSteamHotWater majorModel, short report_period_id)
		{
			var members = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"{report_period_id}")).ToList();

			minorModel.tariff_hc_hw = GetValue_HeatEnergyTariffSteamHotWater_CalculateSteamHotWater(members, "hot_water_heat_carrier_tariff", majorModel, report_period_id);
			minorModel.tariff_hc_steam = GetValue_HeatEnergyTariffSteamHotWater_CalculateSteamHotWater(members, "steam_heat_carrier_tariff", majorModel, report_period_id);
			minorModel.tariff_single_component_hw = GetValue_HeatEnergyTariffSteamHotWater_CalculateSingleComponentTariffHotWater(members, "hot_water_tariff", majorModel, report_period_id);
			minorModel.tariff_double_component_hc = GetValue_HeatEnergyTariffSteamHotWater_CalculateSingleComponentTariffHotWater(members, "heat_carrier_component", majorModel, report_period_id);
			minorModel.tariff_double_component_he = GetValue_HeatEnergyTariffSteamHotWater_CalculateHeatEnergyComponent(members, "heat_energy_component", majorModel, report_period_id);

			minorModel.create_date = DateTime.Now;
			minorModel.user_id = userId;
		}

		[NonAction]
		public async Task SetAllParameters_HeatEnergyTariffSteamHotWater(HeatEnergyTariffSteamHotWater model)
		{
			for (short period = 1; period <= 3; period++)
			{
				var tz_upd_1 = await _context.TZ_TariffsHeatCarrier_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
								&& x.report_period_id == period).FirstOrDefaultAsync();

				if (tz_upd_1 != null)
				{
					SetExistModel_HeatEnergyTariffSteamHotWater(tz_upd_1, model, period);
					_context.TZ_TariffsHeatCarrier_Fact.Update(tz_upd_1);
				}
				else
				{
					var new_tz_bal = GetNewTZHeatEnergyTariffSteamHotWaterModel(model, period);
					await _context.TZ_TariffsHeatCarrier_Fact.AddAsync(new_tz_bal);
				}
			}

			await _context.SaveChangesAsync();
		}

		#endregion

		#region Модель для добавления новой записи и редактирования существующей. Объем перекрестного субсидирования

		//Создание модели для добавления новой записи или редактирования существующей. Объем перекрестного субсидирования
		[NonAction]
		public TZ_TariffsCrossSubsidy_Fact GetNewTZCrossSubsidizationAmountModel(CrossSubsidizationAmount model, short report_period_id)
		{
			var members = model.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"{report_period_id}")).ToList();

			var new_tz_bal = new TZ_TariffsCrossSubsidy_Fact()
			{
				tz_id = model.tz_id,
				data_status = model.data_status,
				report_period_id = report_period_id,
				volume_tso = GetValue_CrossSubsidizationAmount(members, "tariff_tso", model, report_period_id),
				volume_budgetcons = GetValue_CrossSubsidizationAmount(members, "tariff_budgetcons", model, report_period_id),
				volume_public = GetValue_CrossSubsidizationAmount(members, "tariff_public", model, report_period_id),
				volume_other = GetValue_CrossSubsidizationAmount(members, "tariff_other", model, report_period_id),

				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz_bal;
		}

		[NonAction]
		public decimal? GetValue_CrossSubsidizationAmount(IEnumerable<PropertyInfo> members, string propertyName, CrossSubsidizationAmount majorModel, short report_period_id)
		{
			if (report_period_id == 3)
			{
				var members_1 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"1")).ToList();
				var members_2 = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"2")).ToList();

				var first = GetValue_CrossSubsidizationAmount(members_1, propertyName, majorModel, 1);
				var second = GetValue_CrossSubsidizationAmount(members_2, propertyName, majorModel, 2);
				return first + second;
			}

			var ans = (decimal?)members.First(n => n.Name.Contains(propertyName)).GetValue(majorModel) ?? 0;
			return ans;
		}

		[NonAction]
		public void SetExistModel_CrossSubsidizationAmount(TZ_TariffsCrossSubsidy_Fact minorModel, CrossSubsidizationAmount majorModel, short report_period_id)
		{
			var members = majorModel.GetType().GetProperties().Where(n => n.Name.EndsWith("_per_" + $"{report_period_id}")).ToList();

			minorModel.volume_tso = GetValue_CrossSubsidizationAmount(members, "tariff_tso", majorModel, report_period_id);
			minorModel.volume_budgetcons = GetValue_CrossSubsidizationAmount(members, "tariff_budgetcons", majorModel, report_period_id);
			minorModel.volume_public = GetValue_CrossSubsidizationAmount(members, "tariff_public", majorModel, report_period_id);
			minorModel.volume_other = GetValue_CrossSubsidizationAmount(members, "tariff_other", majorModel, report_period_id);

			minorModel.create_date = DateTime.Now;
			minorModel.user_id = userId;
		}

		[NonAction]
		public async Task SetAllParameters_CrossSubsidizationAmount(CrossSubsidizationAmount model)
		{
			for (short period = 1; period <= 3; period++)
			{
				var tz_upd_1 = await _context.TZ_TariffsCrossSubsidy_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
								&& x.report_period_id == period).FirstOrDefaultAsync();

				if (tz_upd_1 != null)
				{
					SetExistModel_CrossSubsidizationAmount(tz_upd_1, model, period);
					_context.TZ_TariffsCrossSubsidy_Fact.Update(tz_upd_1);
				}
				else
				{
					var new_tz_bal = GetNewTZCrossSubsidizationAmountModel(model, period);
					await _context.TZ_TariffsCrossSubsidy_Fact.AddAsync(new_tz_bal);
				}
			}

			await _context.SaveChangesAsync();
		}

		#endregion

		#endregion

		#region Модель для сохранения данных вкладки Индивидуальная плата

		[NonAction]
		private async Task<TSO_ConnectChargePC_Fact?> GetDto_IndividuaFee(OpenIndividualFeeTable model)
		{
			return await _context.TSO_ConnectChargePC_Fact.Where(x => x.decision_num == model.individual_fee_one_decision_num && x.consumer_id == model.individual_fee_one_consumer_id)
				.FirstOrDefaultAsync();
		}

		[NonAction]
		private async Task<bool> SetModyfiedModel_IndividuaFee(TSO_ConnectChargePC_Fact? dto, OpenIndividualFeeTable model)
		{
			dto.dev_prog_id = model.individual_fee_one_dev_prog_id;
			dto.consumer_id = model.individual_fee_one_consumer_id;
			dto.data_status = model.individual_fee_one_data_status;
			dto.tso_id = model.individual_fee_one_tso_id;
			dto.decision_charge_status_id = (short)model.individual_fee_one_decision_status_id;
			dto.decision_num = model.individual_fee_one_decision_num;
			dto.decision_date = Convert.ToDateTime(model.individual_fee_one_decision_date);
			dto.protocol_num = model.individual_fee_one_protocol_num;
			dto.protocol_date = Convert.ToDateTime(model.individual_fee_one_protocol_date);
			dto.confirm_size_charge_connect = model.individual_fee_one_confirm_size_charge_connect;
			dto.edit_date = DateTime.Now;
			dto.is_deleted = false;
			dto.user_id = userId;
			_context.TSO_ConnectChargePC_Fact.Update(dto);

			return false;
		}

		[NonAction]
		private TSO_ConnectChargePC_Fact GetNewModel_IndividuaFee(OpenIndividualFeeTable model)
		{

			var new_tz = new TSO_ConnectChargePC_Fact
			{
				decision_charge_status_id = (short)model.individual_fee_one_decision_status_id,
				dev_prog_id = model.individual_fee_one_dev_prog_id,
				consumer_id = model.individual_fee_one_consumer_id,
				data_status = model.individual_fee_one_data_status,
				tso_id = model.individual_fee_one_tso_id,
				decision_num = model.individual_fee_one_decision_num,
				decision_date = Convert.ToDateTime(model.individual_fee_one_decision_date),
				protocol_num = model.individual_fee_one_protocol_num,
				protocol_date = Convert.ToDateTime(model.individual_fee_one_protocol_date),
				confirm_size_charge_connect = model.individual_fee_one_confirm_size_charge_connect,
				create_date = DateTime.Now,
				is_deleted = false,
				user_id = userId
			};

			return new_tz;
		}

		[NonAction]
		private async Task<bool> SetDto_IndividuaFee(OpenIndividualFeeTable model, Values_List selected)
		{			
			return selected != null && model.individual_fee_one_decision_status_id != 3
				&& (selected.value_id != model.individual_fee_one_consumer_id || selected.value_name != model.individual_fee_one_decision_num);
		}

		[NonAction]
		public async Task<bool> SetValues_IndividuaFee(OpenIndividualFeeTable model)
		{
			TSO_ConnectChargePC_Fact? tz_upd = await GetDto_IndividuaFee(model);
			var selected = await _context.fnt_GetDevProgramSelected(model.individual_fee_one_consumer_id, model.individual_fee_one_decision_num).FirstOrDefaultAsync();
			var isFalse = await SetDto_IndividuaFee(model, selected);

			if (isFalse)
				return true;

			if (model.individual_fee_one_is_modyfied == 1)
			{ 
				if (tz_upd == null)
					return true;

				return await SetModyfiedModel_IndividuaFee(tz_upd, model);
			}

			if (tz_upd != null)
				return true;

			var new_tz = GetNewModel_IndividuaFee(model);
			await _context.TSO_ConnectChargePC_Fact.AddAsync(new_tz);
			return false;
		}

		#endregion

		#region Модель для сохранения даных вкладки Стандартизированные ставки ТП

		[NonAction]
		public async Task<bool> SetValues_StandardizedRates(OpenStandardizedRatesTable model)
		{
			TSO_ConnectCharge_Fact? tz_upd = await GetDto_StandardizedRates(model);
			var selected = await _context.fnt_GetDevProgramSelectedStandardized(model.standardized_rates_one_tso_id, model.standardized_rates_one_decision_num).FirstOrDefaultAsync();
			var isFalse = await SetDto_StandardizedRates(model, selected);

			if (isFalse)
				return true;

			if (model.standardized_rates_one_is_modyfied == 1)
			{
				if (tz_upd == null)
					return true;

				return await SetModyfiedModel_StandardizedRates(tz_upd, model);
			}

			if (tz_upd != null)
				return true;

			var new_tz = GetNewModel_StandardizedRates(model).Result;
			await _context.TSO_ConnectCharge_Fact.AddAsync(new_tz);
			return false;
		}

		[NonAction]
		private async Task<TSO_ConnectCharge_Fact?> GetDto_StandardizedRates(OpenStandardizedRatesTable model)
		{
			return await _context.TSO_ConnectCharge_Fact.Where(x => x.tso_id == model.standardized_rates_one_tso_id	&& x.decision_num == model.standardized_rates_one_decision_num)
				.FirstOrDefaultAsync();
		}

		[NonAction]
		private async Task<bool> SetDto_StandardizedRates(OpenStandardizedRatesTable model, Values_List selected)
		{
			return selected != null && model.standardized_rates_one_decision_status_id != 3
				&& (selected.value_id != model.standardized_rates_one_tso_id || selected.value_name != model.standardized_rates_one_decision_num);
		}

		[NonAction]
		private async Task<TSO_ConnectCharge_Fact> GetNewModel_StandardizedRates(OpenStandardizedRatesTable model)
		{
			TSO_ConnectCharge_Fact new_tz = null;

			await Task.Run(() =>
			{
				new_tz = new TSO_ConnectCharge_Fact
				{
					decision_charge_status_id = (short)model.standardized_rates_one_decision_status_id,
					data_status = model.standardized_rates_one_data_status,
					tso_id = model.standardized_rates_one_tso_id,
					decision_num = model.standardized_rates_one_decision_num,
					decision_date = Convert.ToDateTime(model.standardized_rates_one_decision_date),
					protocol_num = model.standardized_rates_one_protocol_num,
					protocol_date = Convert.ToDateTime(model.standardized_rates_one_protocol_date),

					cost_connect_event = model.standardized_rates_one_cost_connect_event,
					cost_hn_underground_ch_du250 = model.standardized_rates_one_cost_hn_underground_ch_du250,
					cost_hn_underground_ch_du251_400 = model.standardized_rates_one_cost_hn_underground_ch_du251_400,
					cost_hn_underground_ch_du401_550 = model.standardized_rates_one_cost_hn_underground_ch_du401_550,
					cost_hn_underground_ch_du551_700 = model.standardized_rates_one_cost_hn_underground_ch_du551_700,
					cost_hn_underground_ch_du700 = model.standardized_rates_one_cost_hn_underground_ch_du700,

					cost_hn_underground_ch_free_du250 = model.standardized_rates_one_cost_hn_underground_ch_free_du250,
					cost_hn_underground_ch_free_du251_400 = model.standardized_rates_one_cost_hn_underground_ch_free_du251_400,
					cost_hn_underground_ch_free_du401_550 = model.standardized_rates_one_cost_hn_underground_ch_free_du401_550,
					cost_hn_underground_ch_free_du551_700 = model.standardized_rates_one_cost_hn_underground_ch_free_du551_700,
					cost_hn_underground_ch_free_du700 = model.standardized_rates_one_cost_hn_underground_ch_free_du700,

					cost_hn_overground_du250 = model.standardized_rates_one_cost_hn_overground_du250,
					cost_hn_overground_du251_400 = model.standardized_rates_one_cost_hn_overground_du251_400,
					cost_hn_overground_du401_550 = model.standardized_rates_one_cost_hn_overground_du401_550,
					cost_hn_overground_du551_700 = model.standardized_rates_one_cost_hn_overground_du551_700,
					cost_hn_overground_du700 = model.standardized_rates_one_cost_hn_overground_du700,

					cost_hp = model.standardized_rates_one_cost_hp,
					profit_tax = model.standardized_rates_one_profit_tax,

					create_date = DateTime.Now,
					is_deleted = false,
					user_id = userId
				};
			});

			return new_tz;
		}

		[NonAction]
		private async Task<bool> SetModyfiedModel_StandardizedRates(TSO_ConnectCharge_Fact? dto, OpenStandardizedRatesTable model)
		{
			await Task.Run(() =>
			{
				dto.decision_charge_status_id = (short)model.standardized_rates_one_decision_status_id;
				dto.data_status = model.standardized_rates_one_data_status;
				dto.tso_id = model.standardized_rates_one_tso_id;
				dto.decision_num = model.standardized_rates_one_decision_num;
				dto.decision_date = Convert.ToDateTime(model.standardized_rates_one_decision_date);
				dto.protocol_num = model.standardized_rates_one_protocol_num;
				dto.protocol_date = Convert.ToDateTime(model.standardized_rates_one_protocol_date);

				dto.cost_connect_event = model.standardized_rates_one_cost_connect_event;
				dto.cost_hn_underground_ch_du250 = model.standardized_rates_one_cost_hn_underground_ch_du250;
				dto.cost_hn_underground_ch_du251_400 = model.standardized_rates_one_cost_hn_underground_ch_du251_400;
				dto.cost_hn_underground_ch_du401_550 = model.standardized_rates_one_cost_hn_underground_ch_du401_550;
				dto.cost_hn_underground_ch_du551_700 = model.standardized_rates_one_cost_hn_underground_ch_du551_700;
				dto.cost_hn_underground_ch_du700 = model.standardized_rates_one_cost_hn_underground_ch_du700;

				dto.cost_hn_underground_ch_free_du250 = model.standardized_rates_one_cost_hn_underground_ch_free_du250;
				dto.cost_hn_underground_ch_free_du251_400 = model.standardized_rates_one_cost_hn_underground_ch_free_du251_400;
				dto.cost_hn_underground_ch_free_du401_550 = model.standardized_rates_one_cost_hn_underground_ch_free_du401_550;
				dto.cost_hn_underground_ch_free_du551_700 = model.standardized_rates_one_cost_hn_underground_ch_free_du551_700;
				dto.cost_hn_underground_ch_free_du700 = model.standardized_rates_one_cost_hn_underground_ch_free_du700;

				dto.cost_hn_overground_du250 = model.standardized_rates_one_cost_hn_overground_du250;
				dto.cost_hn_overground_du251_400 = model.standardized_rates_one_cost_hn_overground_du251_400;
				dto.cost_hn_overground_du401_550 = model.standardized_rates_one_cost_hn_overground_du401_550;
				dto.cost_hn_overground_du551_700 = model.standardized_rates_one_cost_hn_overground_du551_700;
				dto.cost_hn_overground_du700 = model.standardized_rates_one_cost_hn_overground_du700;

				dto.cost_hp = model.standardized_rates_one_cost_hp;
				dto.profit_tax = model.standardized_rates_one_profit_tax;

				dto.edit_date = DateTime.Now;
				dto.is_deleted = false;
				dto.user_id = userId;
				_context.TSO_ConnectCharge_Fact.Update(dto);
			});

			return false;
		}

		#endregion


		#region Модель для сохранения даных вкладки Плата за резерв мощности

		[NonAction]
		public async Task<bool> SetValues_PowerReservePayment(OpenPowerReservePaymentTable model)
		{
			TSO_ReserveHPCharge_Fact? tz_upd = await GetDto_PowerReservePayment(model);
			var selected = await _context.fnt_GetDevProgramSelectedPowerReserve(model.power_reserve_payment_one_tso_id, model.power_reserve_payment_one_decision_num).FirstOrDefaultAsync();
			var isFalse = await SetDto_PowerReservePayment(model, selected);

			if (isFalse)
				return true;

			if (model.power_reserve_payment_one_is_modyfied == 1)
			{
				if (tz_upd == null)
					return true;

				return await SetModyfiedModel_PowerReservePayment(tz_upd, model);
			}

			if (tz_upd != null)
				return true;

			var new_tz = GetNewModel_PowerReservePayment(model).Result;
			await _context.TSO_ReserveHPCharge_Fact.AddAsync(new_tz);
			return false;
		}

		[NonAction]
		private async Task<TSO_ReserveHPCharge_Fact?> GetDto_PowerReservePayment(OpenPowerReservePaymentTable model)
		{
			return await _context.TSO_ReserveHPCharge_Fact.Where(x => x.tso_id == model.power_reserve_payment_one_tso_id && x.decision_num == model.power_reserve_payment_one_decision_num)
				.FirstOrDefaultAsync();
		}

		[NonAction]
		private async Task<bool> SetDto_PowerReservePayment(OpenPowerReservePaymentTable model, Values_List selected)
		{
			return selected != null && model.power_reserve_payment_one_decision_status_id != 3
				&& (selected.value_id != model.power_reserve_payment_one_tso_id || selected.value_name != model.power_reserve_payment_one_decision_num);
		}

		[NonAction]
		private async Task<TSO_ReserveHPCharge_Fact> GetNewModel_PowerReservePayment(OpenPowerReservePaymentTable model)
		{
			TSO_ReserveHPCharge_Fact new_tz = null;

			await Task.Run(() =>
			{
				new_tz = new TSO_ReserveHPCharge_Fact
				{
					decision_charge_status_id = (short)model.power_reserve_payment_one_decision_status_id,
					data_status = model.power_reserve_payment_one_data_status,
					tso_id = model.power_reserve_payment_one_tso_id,
					decision_num = model.power_reserve_payment_one_decision_num,
					decision_date = Convert.ToDateTime(model.power_reserve_payment_one_decision_date),
					protocol_num = model.power_reserve_payment_one_protocol_num,
					protocol_date = Convert.ToDateTime(model.power_reserve_payment_one_protocol_date),
					cost_service_before_hp = model.power_reserve_payment_one_cost_service_before_hp,
					cost_service_after_hp = model.power_reserve_payment_one_cost_service_after_hp,

					create_date = DateTime.Now,
					is_deleted = false,
					user_id = userId
				};
			});

			return new_tz;
		}

		[NonAction]
		private async Task<bool> SetModyfiedModel_PowerReservePayment(TSO_ReserveHPCharge_Fact? dto, OpenPowerReservePaymentTable model)
		{
			await Task.Run(() =>
			{
				dto.decision_charge_status_id = (short)model.power_reserve_payment_one_decision_status_id;
				dto.data_status = model.power_reserve_payment_one_data_status;
				dto.tso_id = model.power_reserve_payment_one_tso_id;
				dto.decision_num = model.power_reserve_payment_one_decision_num;
				dto.decision_date = Convert.ToDateTime(model.power_reserve_payment_one_decision_date);
				dto.protocol_num = model.power_reserve_payment_one_protocol_num;
				dto.protocol_date = Convert.ToDateTime(model.power_reserve_payment_one_protocol_date);
				dto.cost_service_before_hp = model.power_reserve_payment_one_cost_service_before_hp;
				dto.cost_service_after_hp = model.power_reserve_payment_one_cost_service_after_hp;

				dto.edit_date = DateTime.Now;
				dto.is_deleted = false;
				dto.user_id = userId;
			});

			return false;
		}

		#endregion
	}
}
