using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using WebProject.Areas.TSO.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.TSO.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("TSO")]
	public class CostsAndPricesController : Controller
	{
		private readonly ILogger<CostsAndPricesController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;
		//private PrincipalContext _ctx = new PrincipalContext(ContextType.Domain);
		public CostsAndPricesController(ILogger<CostsAndPricesController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		//Затраты и цены
		public IActionResult TZCostsPrices()
		{
			return View();
		}

		#region OpenPopups
		public IActionResult OpenCostsPrices(int id, int data_status, int perspective_year)
		{
			var tz = new TZOneCalcCostsDataViewModel();
			try
			{
				if (id > 0)
				{
					List<TZOneCalcCostsDataViewModel> tz_l = _context.TZOneCalcCostsDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZCalcCostsDataOne {id},{data_status},{perspective_year},{userId}").ToList();
					if (tz_l.Count > 0)
					{
						tz = tz_l[0];
					}
				}
				tz.tz_id = id;
				tz.data_status = data_status;
				tz.perspective_year = perspective_year;

				ViewBag.TZTSOList = _context.fnt_GetTZTSOList(data_status, perspective_year, "all").ToList();
				ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenCostsPrices", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(tz);
		}

		//Необходимая валовая выручка (НВВ) организации ТСО
		public IActionResult OpenNVV(int id, int data_status, int perspective_year)
		{
			var tz = new TZOneCalcCostsDataViewModel();
			try
			{
				if (id > 0)
				{
					tz = _context.TZOneCalcCostsDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZCalcCostsDataOne {id},{data_status},{perspective_year},{userId}").ToList().FirstOrDefault();
				}
				tz.tz_id = id;
				tz.data_status = data_status;
				tz.perspective_year = perspective_year;

				ViewBag.TZTSOList = _context.fnt_GetTZTSOList(data_status, perspective_year, "all").ToList();
				ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenNVV", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(tz);
		}

		//Расходы ИП ТС и КС
		public IActionResult OpenExpenses_IPHS_CA(int id, int data_status, int perspective_year)
		{
			var tz = new TZOneCalcCostsDataViewModel();
			try
			{
				
				if (id > 0)
				{
					tz = _context.TZOneCalcCostsDataViewModel.FromSqlInterpolated($"exec tarif_zone.sp_GetTZCalcCostsDataOne {id},{data_status},{perspective_year},{userId}").ToList().FirstOrDefault();
				}
				tz.tz_id = id;
				tz.data_status = data_status;
				tz.perspective_year = perspective_year;

				ViewBag.TZTSOList = _context.fnt_GetTZTSOList(data_status, perspective_year, "all").ToList();
			}
			catch (Exception ex)
			{
				
				_m_c.ExLog_Save("OpenExpenses_IPHS_CA", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(tz);
		}
		#endregion

		#region Get_Data_From_Form_CostAndPrices
		//Топливо на технологические цели (факт)
		public async Task<IActionResult> TZ_SaveFuelTechNeedFact(TZFuelTechNeedFactListViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					var tz_upd = await _context.TZ_FuelTechNeeds_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status 
						&& x.fuel_type_id == model.fuel_type_id_old && x.report_period_id == model.report_period_id_old).FirstOrDefaultAsync();

					if (tz_upd != null)
					{
						if (model.fuel_type_id != model.fuel_type_id_old || model.report_period_id != model.report_period_id_old)
						{
							if (await _context.TZ_FuelTechNeeds_Fact.AnyAsync(x => x.tz_id == model.tz_id && x.data_status == model.data_status
							&& x.fuel_type_id == model.fuel_type_id && x.report_period_id == model.report_period_id))
							{
								return Json(new { success = false, error = "Указанный период по данному виду топлива уже был внесён. Выберите другой период." });
							}

							_context.TZ_FuelTechNeeds_Fact.Remove(tz_upd);
						}
						else
						{
							tz_upd.consumption_natural_fuel_fact = model.consumption_natural_fuel_fact;
							tz_upd.lowestheat_combust_fuel_fact = model.lowestheat_combust_fuel_fact;
							tz_upd.price_fuel_fact = model.price_fuel_fact;
							tz_upd.edit_date = DateTime.Now;
							tz_upd.user_id = userId;
						}
					}
					
					if (model.fuel_type_id != model.fuel_type_id_old || model.report_period_id != model.report_period_id_old)
					{
						if (await _context.TZ_FuelTechNeeds_Fact.AnyAsync(x => x.tz_id == model.tz_id && x.data_status == model.data_status
							&& x.fuel_type_id == model.fuel_type_id && x.report_period_id == model.report_period_id))
						{
							return Json(new { success = false, error = "Указанный период по данному виду топлива уже был внесён. Выберите другой период." });
						}

						var new_tz = new TZ_FuelTechNeeds_Fact()
						{
							tz_id = model.tz_id,
							data_status = model.data_status,
							report_period_id = model.report_period_id,
							fuel_type_id = model.fuel_type_id,
							consumption_natural_fuel_fact = model.consumption_natural_fuel_fact,
							lowestheat_combust_fuel_fact = model.lowestheat_combust_fuel_fact,
							price_fuel_fact = model.price_fuel_fact,
							is_deleted = false,
							create_date = DateTime.Now,
							user_id = userId
						};
						await _context.TZ_FuelTechNeeds_Fact.AddAsync(new_tz);
					}

					await _context.SaveChangesAsync();

					await InsertUpdateFuelTechNeedsFact(model.tz_id, model.data_status, model.fuel_type_id);
					if (model.fuel_type_id_old > 0 && model.fuel_type_id_old != model.fuel_type_id)
					{
						await InsertUpdateFuelTechNeedsFact(model.tz_id, model.data_status, model.fuel_type_id_old);
					}
					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZ_SaveFuelTechNeedFact", $"tz_id={model.tz_id},data_status={model.data_status}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
		//Топливо на технологические цели (план)
		public async Task<IActionResult> TZ_SaveFuelTechNeedPlan(TZFuelTechNeedPlanListViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					var tz_upd = await _context.TZ_FuelTechNeeds_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.fuel_type_id == model.fuel_type_id_old && x.report_period_id == model.report_period_id_old).FirstOrDefaultAsync();

					if (model.fuel_type_id != model.fuel_type_id_old || model.report_period_id != model.report_period_id_old)
					{
						if (await _context.TZ_FuelTechNeeds_Plan.AnyAsync(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.fuel_type_id == model.fuel_type_id && x.report_period_id == model.report_period_id))
						{
							return Json(new { success = false, error = "Указанный период по данному виду топлива уже был внесён. Выберите другой период." });
						}
					}

					if (tz_upd != null)
					{
						if (model.fuel_type_id != model.fuel_type_id_old || model.report_period_id != model.report_period_id_old)
						{
							_context.TZ_FuelTechNeeds_Plan.Remove(tz_upd);
						}
						else
						{
							tz_upd.consumption_natural_fuel_plan = model.consumption_natural_fuel_plan;
							tz_upd.norm_consump_fuel_plan = model.norm_consump_fuel_plan;
							tz_upd.lowestheat_combust_fuel_plan = model.lowestheat_combust_fuel_plan;
							tz_upd.price_fuel_plan = model.price_fuel_plan;
							tz_upd.edit_date = DateTime.Now;
							tz_upd.user_id = userId;
						}
					}

					if (model.fuel_type_id != model.fuel_type_id_old || model.report_period_id != model.report_period_id_old)
					{
						var new_tz = new TZ_FuelTechNeeds_Plan()
						{
							tz_id = model.tz_id,
							data_status = model.data_status,
							perspective_year = model.perspective_year,
							report_period_id = model.report_period_id,
							fuel_type_id = model.fuel_type_id,
							consumption_natural_fuel_plan = model.consumption_natural_fuel_plan,
							norm_consump_fuel_plan = model.norm_consump_fuel_plan,
							lowestheat_combust_fuel_plan = model.lowestheat_combust_fuel_plan,
							price_fuel_plan = model.price_fuel_plan,
							create_date = DateTime.Now,
							user_id = userId
						};
						await _context.TZ_FuelTechNeeds_Plan.AddAsync(new_tz);
					}

					await _context.SaveChangesAsync();

					if (model.report_period_id != 3)
					{
						await InsertUpdateFuelTechNeedsPlan(model.tz_id, model.data_status, model.perspective_year, model.fuel_type_id);
						if (model.fuel_type_id_old > 0 && model.fuel_type_id_old != model.fuel_type_id)
						{
							await InsertUpdateFuelTechNeedsPlan(model.tz_id, model.data_status, model.perspective_year, model.fuel_type_id_old);
						}
						await _context.SaveChangesAsync();
					}
					
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZ_SaveFuelTechNeedPlan", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZFuelReserveCosts_Save(TZFuelReserveCostsDataViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					var tz_upd_f1 = await _context.TZ_CostFuelReserve_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 1).FirstOrDefaultAsync();
					var tz_upd_f2 = await _context.TZ_CostFuelReserve_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 2).FirstOrDefaultAsync();
					var tz_upd_f3 = await _context.TZ_CostFuelReserve_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 3).FirstOrDefaultAsync();

					var tz_upd_p1 = await _context.TZ_CostFuelReserve_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 1).FirstOrDefaultAsync();
					var tz_upd_p2 = await _context.TZ_CostFuelReserve_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 2).FirstOrDefaultAsync();
					var tz_upd_p3 = await _context.TZ_CostFuelReserve_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 3).FirstOrDefaultAsync();

					
					//факт
					if (tz_upd_f3 != null)
					{
						tz_upd_f1.cost_fuel_reserve_fact = model.cost_fuel_reserve_fact_1;
						tz_upd_f1.edit_date = DateTime.Now;
						tz_upd_f1.user_id = userId;

						tz_upd_f2.cost_fuel_reserve_fact = model.cost_fuel_reserve_fact_2;
						tz_upd_f2.edit_date = DateTime.Now;
						tz_upd_f2.user_id = userId;

						tz_upd_f3.cost_fuel_reserve_fact = model.cost_fuel_reserve_fact_3;
						tz_upd_f3.edit_date = DateTime.Now;
						tz_upd_f3.user_id = userId;
					}
					else
					{
						var new_tz_f1 = GetNewTZFuelReserveCostsFactModel(model.tz_id, model.data_status, 1, model.cost_fuel_reserve_fact_1, false);
						await _context.TZ_CostFuelReserve_Fact.AddAsync(new_tz_f1);

						var new_tz_f2 = GetNewTZFuelReserveCostsFactModel(model.tz_id, model.data_status, 2, model.cost_fuel_reserve_fact_2, false);
						await _context.TZ_CostFuelReserve_Fact.AddAsync(new_tz_f2);

						var new_tz_f3 = GetNewTZFuelReserveCostsFactModel(model.tz_id, model.data_status, 3,
							(model.cost_fuel_reserve_fact_1 ?? 0) + (model.cost_fuel_reserve_fact_2 ?? 0), false);
						await _context.TZ_CostFuelReserve_Fact.AddAsync(new_tz_f3);
					}

					//план
					if (tz_upd_p3 != null)
					{
						tz_upd_p1.cost_fuel_reserve_plan = model.cost_fuel_reserve_plan_1;
						tz_upd_p1.edit_date = DateTime.Now;
						tz_upd_p1.user_id = userId;

						tz_upd_p2.cost_fuel_reserve_plan = model.cost_fuel_reserve_plan_1;
						tz_upd_p2.edit_date = DateTime.Now;
						tz_upd_p2.user_id = userId;

						tz_upd_p3.cost_fuel_reserve_plan = (model.cost_fuel_reserve_plan_1 ?? 0) + (model.cost_fuel_reserve_plan_2 ?? 0);
						tz_upd_p3.edit_date = DateTime.Now;
						tz_upd_p3.user_id = userId;
					}
					else
					{
						var new_tz_p1 = GetNewTZFuelReserveCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_fuel_reserve_plan_1);
						await _context.TZ_CostFuelReserve_Plan.AddAsync(new_tz_p1);

						var new_tz_p2 = GetNewTZFuelReserveCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_fuel_reserve_plan_1);
						await _context.TZ_CostFuelReserve_Plan.AddAsync(new_tz_p2);

						var new_tz_p3 = GetNewTZFuelReserveCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 3,
							(model.cost_fuel_reserve_plan_1 ?? 0) + (model.cost_fuel_reserve_plan_2 ?? 0));
						await _context.TZ_CostFuelReserve_Plan.AddAsync(new_tz_p3);
					}
					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZFuelReserveCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
		//Вода (водоотведение) на технологические и хозяйственные цели
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZWaterNeedCosts_Save(TZWaterNeedCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					var tz_upd_f1 = await _context.TZ_WaterTechNeeds_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 1).FirstOrDefaultAsync();
					var tz_upd_f2 = await _context.TZ_WaterTechNeeds_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 2).FirstOrDefaultAsync();
					var tz_upd_f3 = await _context.TZ_WaterTechNeeds_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 3).FirstOrDefaultAsync();

					var tz_upd_p1 = await _context.TZ_WaterTechNeeds_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 1).FirstOrDefaultAsync();
					var tz_upd_p2 = await _context.TZ_WaterTechNeeds_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 2).FirstOrDefaultAsync();
					var tz_upd_p3 = await _context.TZ_WaterTechNeeds_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 3).FirstOrDefaultAsync();

					decimal avg_price_cw_f = 0; decimal avg_price_wd_f = 0;
					if ((model.volume_cold_water_fact_1 ?? 0) + (model.volume_cold_water_fact_2 ?? 0) > 0)
					{
						avg_price_cw_f = ((model.volume_cold_water_fact_1 ?? 0) * (model.price_cold_water_fact_1 ?? 0)
						+ (model.volume_cold_water_fact_2 ?? 0) * (model.price_cold_water_fact_2 ?? 0))
						/ ((model.volume_cold_water_fact_1 ?? 0) + (model.volume_cold_water_fact_2 ?? 0));
					};

					if ((model.volume_water_dispos_fact_1 ?? 0) + (model.volume_water_dispos_fact_2 ?? 0) > 0)
					{
						avg_price_wd_f = ((model.volume_water_dispos_fact_1 ?? 0) * (model.price_water_dispos_fact_1 ?? 0)
						+ (model.volume_water_dispos_fact_2 ?? 0) * (model.price_water_dispos_fact_2 ?? 0))
						/ ((model.volume_water_dispos_fact_1 ?? 0) + (model.volume_water_dispos_fact_2 ?? 0));
					};

					decimal avg_price_cw_p = 0; decimal avg_price_wd_p = 0;
					if ((model.volume_cold_water_plan_1 ?? 0) + (model.volume_cold_water_plan_2 ?? 0) > 0)
					{
						avg_price_cw_p = ((model.volume_cold_water_plan_1 ?? 0) * (model.price_cold_water_plan_1 ?? 0)
						+ (model.volume_cold_water_plan_2 ?? 0) * (model.price_cold_water_plan_2 ?? 0))
						/ ((model.volume_cold_water_plan_1 ?? 0) + (model.volume_cold_water_plan_2 ?? 0));
					};

					if ((model.volume_water_dispos_plan_1 ?? 0) + (model.volume_water_dispos_plan_2 ?? 0) > 0)
					{
						avg_price_wd_p = ((model.volume_water_dispos_plan_1 ?? 0) * (model.price_water_dispos_plan_1 ?? 0)
						+ (model.volume_water_dispos_plan_2 ?? 0) * (model.price_water_dispos_plan_2 ?? 0))
						/ ((model.volume_water_dispos_plan_1 ?? 0) + (model.volume_water_dispos_plan_2 ?? 0));
					};

					//факт
					if (tz_upd_f3 != null)
					{
						tz_upd_f1.volume_buy_coldwater_fact = model.volume_cold_water_fact_1;
						tz_upd_f1.price_coldwater_fact = model.price_cold_water_fact_1;
						tz_upd_f1.volume_waterdispos_fact = model.volume_water_dispos_fact_1;
						tz_upd_f1.price_waterdispos_fact = model.price_water_dispos_fact_1;
						tz_upd_f1.edit_date = DateTime.Now;
						tz_upd_f1.user_id = userId;

						tz_upd_f2.volume_buy_coldwater_fact = model.volume_cold_water_fact_2;
						tz_upd_f2.price_coldwater_fact = model.price_cold_water_fact_2;
						tz_upd_f2.volume_waterdispos_fact = model.volume_water_dispos_fact_2;
						tz_upd_f2.price_waterdispos_fact = model.price_water_dispos_fact_2;
						tz_upd_f2.edit_date = DateTime.Now;
						tz_upd_f2.user_id = userId;

						tz_upd_f3.volume_buy_coldwater_fact = (model.volume_cold_water_fact_1 ?? 0) + (model.volume_cold_water_fact_2 ?? 0);
						tz_upd_f3.price_coldwater_fact = avg_price_cw_f;
						tz_upd_f3.volume_waterdispos_fact = (model.volume_water_dispos_fact_1 ?? 0) + (model.volume_water_dispos_fact_2 ?? 0);
						tz_upd_f3.price_waterdispos_fact = avg_price_wd_f;
						tz_upd_f3.edit_date = DateTime.Now;
						tz_upd_f3.user_id = userId;
					}
					else
					{
						var new_tz_f1 = GetNewTZWaterCostsFactModel(model.tz_id, model.data_status, 1, model.volume_cold_water_fact_1,
							model.price_cold_water_fact_1, model.volume_water_dispos_fact_1, model.price_water_dispos_fact_1, false);
						await _context.TZ_WaterTechNeeds_Fact.AddAsync(new_tz_f1);

						var new_tz_f2 = GetNewTZWaterCostsFactModel(model.tz_id, model.data_status, 2, model.volume_cold_water_fact_2,
							model.price_cold_water_fact_2, model.volume_water_dispos_fact_2, model.price_water_dispos_fact_2, false);
						await _context.TZ_WaterTechNeeds_Fact.AddAsync(new_tz_f2);

						var new_tz_f3 = GetNewTZWaterCostsFactModel(model.tz_id, model.data_status, 3, 
							(model.volume_cold_water_fact_1 ?? 0) + (model.volume_cold_water_fact_2 ?? 0), avg_price_cw_f,
							(model.volume_water_dispos_fact_1 ?? 0) + (model.volume_water_dispos_fact_2 ?? 0), avg_price_wd_f, false);
						await _context.TZ_WaterTechNeeds_Fact.AddAsync(new_tz_f3);
					}

					//план
					if (tz_upd_p3 != null)
					{
						tz_upd_p1.volume_buy_coldwater_plan = model.volume_cold_water_plan_1;
						tz_upd_p1.price_coldwater_plan = model.price_cold_water_plan_1;
						tz_upd_p1.volume_waterdispos_plan = model.volume_water_dispos_plan_1;
						tz_upd_p1.price_waterdispos_plan = model.price_water_dispos_plan_1;
						tz_upd_p1.edit_date = DateTime.Now;
						tz_upd_p1.user_id = userId;

						tz_upd_p2.volume_buy_coldwater_plan = model.volume_cold_water_plan_2;
						tz_upd_p2.price_coldwater_plan = model.price_cold_water_plan_2;
						tz_upd_p2.volume_waterdispos_plan = model.volume_water_dispos_plan_2;
						tz_upd_p2.price_waterdispos_plan = model.price_water_dispos_plan_2;
						tz_upd_p2.edit_date = DateTime.Now;
						tz_upd_p2.user_id = userId;

						tz_upd_p3.volume_buy_coldwater_plan = (model.volume_cold_water_plan_1 ?? 0) + (model.volume_cold_water_plan_2 ?? 0);
						tz_upd_p3.price_coldwater_plan = avg_price_cw_p;
						tz_upd_p3.volume_waterdispos_plan = (model.volume_water_dispos_plan_1 ?? 0) + (model.volume_water_dispos_plan_2 ?? 0);
						tz_upd_p3.price_waterdispos_plan = avg_price_wd_p;
						tz_upd_p3.edit_date = DateTime.Now;
						tz_upd_p3.user_id = userId;
					}
					else
					{
						var new_tz_p1 = GetNewTZWaterCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 1, model.volume_cold_water_plan_1,
							model.price_cold_water_plan_1, model.volume_water_dispos_plan_1, model.price_water_dispos_plan_1, false);
						await _context.TZ_WaterTechNeeds_Plan.AddAsync(new_tz_p1);

						var new_tz_p2 = GetNewTZWaterCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 2, model.volume_cold_water_plan_2,
							model.price_cold_water_plan_2, model.volume_water_dispos_plan_2, model.price_water_dispos_plan_2, false);
						await _context.TZ_WaterTechNeeds_Plan.AddAsync(new_tz_p2);

						var new_tz_p3 = GetNewTZWaterCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 3,
							(model.volume_cold_water_plan_1 ?? 0) + (model.volume_cold_water_plan_2 ?? 0), avg_price_cw_p,
							(model.volume_water_dispos_plan_1 ?? 0) + (model.volume_water_dispos_plan_2 ?? 0), avg_price_wd_p, false);
						await _context.TZ_WaterTechNeeds_Plan.AddAsync(new_tz_p3);
					}
					await _context.SaveChangesAsync();
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZWaterNeedCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZHeatCarrierNeedCosts_Save(TZHeatCarrierNeedCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					var tz_upd_f1 = await _context.TZ_HeatCarrierTechNeeds_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 1).FirstOrDefaultAsync();
					var tz_upd_f2 = await _context.TZ_HeatCarrierTechNeeds_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 2).FirstOrDefaultAsync();
					var tz_upd_f3 = await _context.TZ_HeatCarrierTechNeeds_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 3).FirstOrDefaultAsync();

					var tz_upd_p1 = await _context.TZ_HeatCarrierTechNeeds_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 1).FirstOrDefaultAsync();
					var tz_upd_p2 = await _context.TZ_HeatCarrierTechNeeds_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 2).FirstOrDefaultAsync();
					var tz_upd_p3 = await _context.TZ_HeatCarrierTechNeeds_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 3).FirstOrDefaultAsync();


					//факт
					if (tz_upd_f3 != null)
					{
						tz_upd_f1.cost_heat_carrier_fact = model.cost_heatcarrier_fact_1;
						tz_upd_f1.volume_buy_heat_carrier_fact = model.volume_heatcarrier_fact_1;
						tz_upd_f1.edit_date = DateTime.Now;
						tz_upd_f1.user_id = userId;

						tz_upd_f2.cost_heat_carrier_fact = model.cost_heatcarrier_fact_2;
						tz_upd_f2.volume_buy_heat_carrier_fact = model.volume_heatcarrier_fact_2;
						tz_upd_f2.edit_date = DateTime.Now;
						tz_upd_f2.user_id = userId;

						tz_upd_f3.cost_heat_carrier_fact = (model.cost_heatcarrier_fact_1 ?? 0) + (model.cost_heatcarrier_fact_2 ?? 0);
						tz_upd_f3.volume_buy_heat_carrier_fact = (model.volume_heatcarrier_fact_1 ?? 0) + (model.volume_heatcarrier_fact_2 ?? 0);
						tz_upd_f3.edit_date = DateTime.Now;
						tz_upd_f3.user_id = userId;
					}
					else
					{
						var new_tz_f1 = GetNewTZHeatCarrierCostsFactModel(model.tz_id, model.data_status, 1, model.cost_heatcarrier_fact_1,
							model.volume_heatcarrier_fact_1, false);
						await _context.TZ_HeatCarrierTechNeeds_Fact.AddAsync(new_tz_f1);

						var new_tz_f2 = GetNewTZHeatCarrierCostsFactModel(model.tz_id, model.data_status, 2, model.cost_heatcarrier_fact_2,
							model.volume_heatcarrier_fact_2, false);
						await _context.TZ_HeatCarrierTechNeeds_Fact.AddAsync(new_tz_f2);

						var new_tz_f3 = GetNewTZHeatCarrierCostsFactModel(model.tz_id, model.data_status, 3,
							(model.cost_heatcarrier_fact_1 ?? 0) + (model.cost_heatcarrier_fact_2 ?? 0),
							(model.volume_heatcarrier_fact_2 ?? 0) + (model.volume_heatcarrier_fact_1 ?? 0), false);
						await _context.TZ_HeatCarrierTechNeeds_Fact.AddAsync(new_tz_f3);
					}

					//план
					if (tz_upd_p3 != null)
					{
						tz_upd_p1.cost_heat_carrier_plan = model.cost_heatcarrier_plan_1;
						tz_upd_p1.volume_buy_heat_carrier_plan = model.volume_heatcarrier_plan_1;
						tz_upd_p1.edit_date = DateTime.Now;
						tz_upd_p1.user_id = userId;

						tz_upd_p2.cost_heat_carrier_plan = model.cost_heatcarrier_plan_2;
						tz_upd_p2.volume_buy_heat_carrier_plan = model.volume_heatcarrier_plan_2;
						tz_upd_p2.edit_date = DateTime.Now;
						tz_upd_p2.user_id = userId;

						tz_upd_p3.cost_heat_carrier_plan = (model.cost_heatcarrier_plan_1 ?? 0) + (model.cost_heatcarrier_plan_2 ?? 0);
						tz_upd_p3.volume_buy_heat_carrier_plan = (model.volume_heatcarrier_plan_1 ?? 0) + (model.volume_heatcarrier_plan_2 ?? 0);
						tz_upd_p3.edit_date = DateTime.Now;
						tz_upd_p3.user_id = userId;
					}
					else
					{
						var new_tz_p1 = GetNewTZHeatCarrierCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_heatcarrier_plan_1, 
							model.volume_heatcarrier_plan_1);
						await _context.TZ_HeatCarrierTechNeeds_Plan.AddAsync(new_tz_p1);

						var new_tz_p2 = GetNewTZHeatCarrierCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_heatcarrier_plan_2,
							model.volume_heatcarrier_plan_2);
						await _context.TZ_HeatCarrierTechNeeds_Plan.AddAsync(new_tz_p2);

						var new_tz_p3 = GetNewTZHeatCarrierCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 3,
							(model.cost_heatcarrier_plan_1 ?? 0) + (model.cost_heatcarrier_plan_2 ?? 0),
							(model.volume_heatcarrier_plan_1 ?? 0) + (model.volume_heatcarrier_plan_2 ?? 0));
						await _context.TZ_HeatCarrierTechNeeds_Plan.AddAsync(new_tz_p3);
					}
					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZHeatCarrierNeedCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZHETransferCosts_Save(TZHeatEnergyNeedCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					var tz_upd_f1 = await _context.TZ_CostHeatTransfer_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 1).FirstOrDefaultAsync();
					var tz_upd_f2 = await _context.TZ_CostHeatTransfer_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 2).FirstOrDefaultAsync();
					var tz_upd_f3 = await _context.TZ_CostHeatTransfer_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == 3).FirstOrDefaultAsync();

					var tz_upd_p1 = await _context.TZ_CostHeatTransfer_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 1).FirstOrDefaultAsync();
					var tz_upd_p2 = await _context.TZ_CostHeatTransfer_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 2).FirstOrDefaultAsync();
					var tz_upd_p3 = await _context.TZ_CostHeatTransfer_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == 3).FirstOrDefaultAsync();


					//факт
					if (tz_upd_f3 != null)
					{
						tz_upd_f1.cost_heattrans_service_fact = model.costs_heat_transfer_fact_1;
						tz_upd_f1.edit_date = DateTime.Now;
						tz_upd_f1.user_id = userId;

						tz_upd_f2.cost_heattrans_service_fact = model.costs_heat_transfer_fact_2;
						tz_upd_f2.edit_date = DateTime.Now;
						tz_upd_f2.user_id = userId;

						tz_upd_f3.cost_heattrans_service_fact = model.costs_heat_transfer_fact_3;
						tz_upd_f3.edit_date = DateTime.Now;
						tz_upd_f3.user_id = userId;
					}
					else
					{
						var new_tz_f1 = GetNewTZHeatTransferCostsFactModel(model.tz_id, model.data_status, 1, model.costs_heat_transfer_fact_1);
						await _context.TZ_CostHeatTransfer_Fact.AddAsync(new_tz_f1);

						var new_tz_f2 = GetNewTZHeatTransferCostsFactModel(model.tz_id, model.data_status, 2, model.costs_heat_transfer_fact_2);
						await _context.TZ_CostHeatTransfer_Fact.AddAsync(new_tz_f2);

						var new_tz_f3 = GetNewTZHeatTransferCostsFactModel(model.tz_id, model.data_status, 3,
							(model.costs_heat_transfer_fact_1 ?? 0) + (model.costs_heat_transfer_fact_2 ?? 0));
						await _context.TZ_CostHeatTransfer_Fact.AddAsync(new_tz_f3);
					}

					//план
					if (tz_upd_p3 != null)
					{
						tz_upd_p1.cost_heattrans_service_plan = model.costs_heat_transfer_plan_1;
						tz_upd_p1.edit_date = DateTime.Now;
						tz_upd_p1.user_id = userId;

						tz_upd_p2.cost_heattrans_service_plan = model.costs_heat_transfer_plan_2;
						tz_upd_p2.edit_date = DateTime.Now;
						tz_upd_p2.user_id = userId;

						tz_upd_p3.cost_heattrans_service_plan = (model.costs_heat_transfer_plan_1 ?? 0) + (model.costs_heat_transfer_plan_2 ?? 0);
						tz_upd_p3.edit_date = DateTime.Now;
						tz_upd_p3.user_id = userId;
					}
					else
					{
						var new_tz_p1 = GetNewTZHeatTransferCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 1, model.costs_heat_transfer_plan_1);
						await _context.TZ_CostHeatTransfer_Plan.AddAsync(new_tz_p1);

						var new_tz_p2 = GetNewTZHeatTransferCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 2, model.costs_heat_transfer_plan_2);
						await _context.TZ_CostHeatTransfer_Plan.AddAsync(new_tz_p2);

						var new_tz_p3 = GetNewTZHeatTransferCostsPlanModel(model.tz_id, model.data_status, model.perspective_year, 3,
							(model.costs_heat_transfer_plan_1 ?? 0) + (model.costs_heat_transfer_plan_2 ?? 0));
						await _context.TZ_CostHeatTransfer_Plan.AddAsync(new_tz_p3);
					}
					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZHETransferCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZElectroEnergyCosts_Save(TZElectroEnergyCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 1, 1, model.volume_low_voltage_electricity_techneed_fact_1,
						model.volume_low_voltage_el_power_techneed_fact_1, model.volume_low_voltage_electricity_houseneed_fact_1,
						model.volume_low_voltage_el_power_houseneed_fact_1, model.price_low_voltage_electricity_fact_1,
						model.price_low_voltage_el_power_fact_1, false);

					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 1, 2, model.volume_medium1_voltage_electricity_techneed_fact_1,
						model.volume_medium1_voltage_el_power_techneed_fact_1, model.volume_medium1_voltage_electricity_houseneed_fact_1,
						model.volume_medium1_voltage_el_power_houseneed_fact_1, model.price_medium1_voltage_electricity_fact_1,
						model.price_medium1_voltage_el_power_fact_1, false);

					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 1, 3, model.volume_medium2_voltage_electricity_techneed_fact_1,
						model.volume_medium2_voltage_el_power_techneed_fact_1, model.volume_medium2_voltage_electricity_houseneed_fact_1,
						model.volume_medium2_voltage_el_power_houseneed_fact_1, model.price_medium2_voltage_electricity_fact_1,
						model.price_medium2_voltage_el_power_fact_1, false);

					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 1, 4, model.volume_high_voltage_electricity_techneed_fact_1,
						model.volume_high_voltage_el_power_techneed_fact_1, model.volume_high_voltage_electricity_houseneed_fact_1,
						model.volume_high_voltage_el_power_houseneed_fact_1, model.price_high_voltage_electricity_fact_1,
						model.price_high_voltage_el_power_fact_1, false);

					//факт (2 полугодие)
					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 2, 1, model.volume_low_voltage_electricity_techneed_fact_2,
						model.volume_low_voltage_el_power_techneed_fact_2, model.volume_low_voltage_electricity_houseneed_fact_2,
						model.volume_low_voltage_el_power_houseneed_fact_2, model.price_low_voltage_electricity_fact_2,
						model.price_low_voltage_el_power_fact_2, false);

					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 2, 2, model.volume_medium1_voltage_electricity_techneed_fact_2,
						model.volume_medium1_voltage_el_power_techneed_fact_2, model.volume_medium1_voltage_electricity_houseneed_fact_2,
						model.volume_medium1_voltage_el_power_houseneed_fact_2, model.price_medium1_voltage_electricity_fact_2,
						model.price_medium1_voltage_el_power_fact_2, false);

					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 2, 3, model.volume_medium2_voltage_electricity_techneed_fact_2,
						model.volume_medium2_voltage_el_power_techneed_fact_2, model.volume_medium2_voltage_electricity_houseneed_fact_2,
						model.volume_medium2_voltage_el_power_houseneed_fact_2, model.price_medium2_voltage_electricity_fact_2,
						model.price_medium2_voltage_el_power_fact_2, false);

					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 2, 4, model.volume_high_voltage_electricity_techneed_fact_2,
						model.volume_high_voltage_el_power_techneed_fact_2, model.volume_high_voltage_electricity_houseneed_fact_2,
						model.volume_high_voltage_el_power_houseneed_fact_2, model.price_high_voltage_electricity_fact_2,
						model.price_high_voltage_el_power_fact_2, false);

					//средняя цена электроэнергии за год (факт)
					var avg_price_low_electricity_f = GetAvgPriceEnergy(model.volume_low_voltage_electricity_techneed_fact_1, model.volume_low_voltage_electricity_houseneed_fact_1,
						model.volume_low_voltage_electricity_techneed_fact_2, model.volume_low_voltage_electricity_houseneed_fact_2,
						model.price_low_voltage_electricity_fact_1, model.price_low_voltage_electricity_fact_2);

					var avg_price_medium1_electricity_f = GetAvgPriceEnergy(model.volume_medium1_voltage_electricity_techneed_fact_1, model.volume_medium1_voltage_electricity_houseneed_fact_1,
						model.volume_medium1_voltage_electricity_techneed_fact_2, model.volume_medium1_voltage_electricity_houseneed_fact_2,
						model.price_medium1_voltage_electricity_fact_1, model.price_medium1_voltage_electricity_fact_2);

					var avg_price_medium2_electricity_f = GetAvgPriceEnergy(model.volume_medium2_voltage_electricity_techneed_fact_1, model.volume_medium2_voltage_electricity_houseneed_fact_1,
						model.volume_medium2_voltage_electricity_techneed_fact_2, model.volume_medium2_voltage_electricity_houseneed_fact_2,
						model.price_medium2_voltage_electricity_fact_1, model.price_medium2_voltage_electricity_fact_2);

					var avg_price_high_electricity_f = GetAvgPriceEnergy(model.volume_high_voltage_electricity_techneed_fact_1, model.volume_high_voltage_electricity_houseneed_fact_1,
						model.volume_high_voltage_electricity_techneed_fact_2, model.volume_high_voltage_electricity_houseneed_fact_2,
						model.price_high_voltage_electricity_fact_1, model.price_high_voltage_electricity_fact_2);

					//средняя цена эл. мощности за год (факт)
					var avg_price_low_el_power_f = GetAvgPriceEnergy(model.volume_low_voltage_el_power_techneed_fact_1, model.volume_low_voltage_el_power_houseneed_fact_1,
						model.volume_low_voltage_el_power_techneed_fact_2, model.volume_low_voltage_el_power_houseneed_fact_2,
						model.price_low_voltage_el_power_fact_1, model.price_low_voltage_el_power_fact_2);

					var avg_price_medium1_el_power_f = GetAvgPriceEnergy(model.volume_medium1_voltage_el_power_techneed_fact_1, model.volume_medium1_voltage_el_power_houseneed_fact_1,
						model.volume_medium1_voltage_el_power_techneed_fact_2, model.volume_medium1_voltage_el_power_houseneed_fact_2,
						model.price_medium1_voltage_el_power_fact_1, model.price_medium1_voltage_el_power_fact_2);

					var avg_price_medium2_el_power_f = GetAvgPriceEnergy(model.volume_medium2_voltage_el_power_techneed_fact_1, model.volume_medium2_voltage_el_power_houseneed_fact_1,
						model.volume_medium2_voltage_el_power_techneed_fact_2, model.volume_medium2_voltage_el_power_houseneed_fact_2,
						model.price_medium2_voltage_el_power_fact_1, model.price_medium2_voltage_el_power_fact_2);

					var avg_price_high_el_power_f = GetAvgPriceEnergy(model.volume_high_voltage_el_power_techneed_fact_1, model.volume_high_voltage_el_power_houseneed_fact_1,
						model.volume_high_voltage_el_power_techneed_fact_2, model.volume_high_voltage_el_power_houseneed_fact_2,
						model.price_high_voltage_el_power_fact_1, model.price_high_voltage_el_power_fact_2);

					//факт (ГОД)
					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 3, 1, 
						(model.volume_low_voltage_electricity_techneed_fact_1 ?? 0) + (model.volume_low_voltage_electricity_techneed_fact_2 ?? 0),
						(model.volume_low_voltage_el_power_techneed_fact_1 ?? 0) + (model.volume_low_voltage_el_power_techneed_fact_2 ?? 0), 
						(model.volume_low_voltage_electricity_houseneed_fact_1 ?? 0) + (model.volume_low_voltage_electricity_houseneed_fact_2 ?? 0),
						(model.volume_low_voltage_el_power_houseneed_fact_1 ?? 0) + (model.volume_low_voltage_el_power_houseneed_fact_2 ?? 0), 
						await avg_price_low_electricity_f, await avg_price_low_el_power_f, false);

					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 3, 2, 
						(model.volume_medium1_voltage_electricity_techneed_fact_1 ?? 0) + (model.volume_medium1_voltage_electricity_techneed_fact_2 ?? 0),
						(model.volume_medium1_voltage_el_power_techneed_fact_1 ?? 0) + (model.volume_medium1_voltage_el_power_techneed_fact_2 ?? 0), 
						(model.volume_medium1_voltage_electricity_houseneed_fact_1 ?? 0) + (model.volume_medium1_voltage_electricity_houseneed_fact_2 ?? 0),
						(model.volume_medium1_voltage_el_power_houseneed_fact_1 ?? 0) + (model.volume_medium1_voltage_el_power_houseneed_fact_2 ?? 0), 
						await avg_price_medium1_electricity_f, await avg_price_medium1_el_power_f, false);

					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 3, 3, 
						(model.volume_medium2_voltage_electricity_techneed_fact_1 ?? 0) + (model.volume_medium2_voltage_electricity_techneed_fact_2 ?? 0),
						(model.volume_medium2_voltage_el_power_techneed_fact_1 ?? 0) + (model.volume_medium2_voltage_el_power_techneed_fact_2 ?? 0), 
						(model.volume_medium2_voltage_electricity_houseneed_fact_1 ?? 0) + (model.volume_medium2_voltage_electricity_houseneed_fact_2 ?? 0),
						(model.volume_medium2_voltage_el_power_houseneed_fact_1 ?? 0) + (model.volume_medium2_voltage_el_power_houseneed_fact_2 ?? 0), 
						await avg_price_medium2_electricity_f, await avg_price_medium2_el_power_f, false);

					await InsertUpdateElEnergyCostsFact(model.tz_id, model.data_status, 3, 4, 
						(model.volume_high_voltage_electricity_techneed_fact_1 ?? 0) + (model.volume_high_voltage_electricity_techneed_fact_2 ?? 0), 
						(model.volume_high_voltage_el_power_techneed_fact_1 ?? 0) + (model.volume_high_voltage_el_power_techneed_fact_2 ?? 0), 
						(model.volume_high_voltage_electricity_houseneed_fact_1 ?? 0) + (model.volume_high_voltage_electricity_houseneed_fact_2 ?? 0), 
						(model.volume_high_voltage_el_power_houseneed_fact_1 ?? 0) + (model.volume_high_voltage_el_power_houseneed_fact_2 ?? 0), 
						await avg_price_high_electricity_f, await avg_price_high_el_power_f, false);

					//план (1 полугодие)
					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, 1, model.volume_low_voltage_electricity_techneed_plan_1,
						model.volume_low_voltage_el_power_techneed_plan_1, model.volume_low_voltage_electricity_houseneed_plan_1,
						model.volume_low_voltage_el_power_houseneed_plan_1, model.price_low_voltage_electricity_plan_1,
						model.price_low_voltage_el_power_plan_1);

					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, 2, model.volume_medium1_voltage_electricity_techneed_plan_1,
						model.volume_medium1_voltage_el_power_techneed_plan_1, model.volume_medium1_voltage_electricity_houseneed_plan_1,
						model.volume_medium1_voltage_el_power_houseneed_plan_1, model.price_medium1_voltage_electricity_plan_1,
						model.price_medium1_voltage_el_power_plan_1);

					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, 3, model.volume_medium2_voltage_electricity_techneed_plan_1,
						model.volume_medium2_voltage_el_power_techneed_plan_1, model.volume_medium2_voltage_electricity_houseneed_plan_1,
						model.volume_medium2_voltage_el_power_houseneed_plan_1, model.price_medium2_voltage_electricity_plan_1,
						model.price_medium2_voltage_el_power_plan_1);

					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, 4, model.volume_high_voltage_electricity_techneed_plan_1,
						model.volume_high_voltage_el_power_techneed_plan_1, model.volume_high_voltage_electricity_houseneed_plan_1,
						model.volume_high_voltage_el_power_houseneed_plan_1, model.price_high_voltage_electricity_plan_1,
						model.price_high_voltage_el_power_plan_1);

					//план (2 полугодие)
					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, 1, model.volume_low_voltage_electricity_techneed_plan_2,
						model.volume_low_voltage_el_power_techneed_plan_2, model.volume_low_voltage_electricity_houseneed_plan_2,
						model.volume_low_voltage_el_power_houseneed_plan_2, model.price_low_voltage_electricity_plan_2,
						model.price_low_voltage_el_power_plan_1);

					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, 2, model.volume_medium1_voltage_electricity_techneed_plan_2,
						model.volume_medium1_voltage_el_power_techneed_plan_2, model.volume_medium1_voltage_electricity_houseneed_plan_2,
						model.volume_medium1_voltage_el_power_houseneed_plan_2, model.price_medium1_voltage_electricity_plan_2,
						model.price_medium1_voltage_el_power_plan_2);

					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, 3, model.volume_medium2_voltage_electricity_techneed_plan_2,
						model.volume_medium2_voltage_el_power_techneed_plan_2, model.volume_medium2_voltage_electricity_houseneed_plan_2,
						model.volume_medium2_voltage_el_power_houseneed_plan_2, model.price_medium2_voltage_electricity_plan_2,
						model.price_medium2_voltage_el_power_plan_2);

					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, 4, model.volume_high_voltage_electricity_techneed_plan_2,
						model.volume_high_voltage_el_power_techneed_plan_2, model.volume_high_voltage_electricity_houseneed_plan_2,
						model.volume_high_voltage_el_power_houseneed_plan_2, model.price_high_voltage_electricity_plan_2,
						model.price_high_voltage_el_power_plan_2);

					//средняя цена электроэнергии за год (план)
					var avg_price_low_electricity_p = GetAvgPriceEnergy(model.volume_low_voltage_electricity_techneed_plan_1, model.volume_low_voltage_electricity_houseneed_plan_1,
						model.volume_low_voltage_electricity_techneed_plan_2, model.volume_low_voltage_electricity_houseneed_plan_2,
						model.price_low_voltage_electricity_plan_1, model.price_low_voltage_electricity_plan_2);

					var avg_price_medium1_electricity_p = GetAvgPriceEnergy(model.volume_medium1_voltage_electricity_techneed_plan_1, model.volume_medium1_voltage_electricity_houseneed_plan_1,
						model.volume_medium1_voltage_electricity_techneed_plan_2, model.volume_medium1_voltage_electricity_houseneed_plan_2,
						model.price_medium1_voltage_electricity_plan_1, model.price_medium1_voltage_electricity_plan_2);

					var avg_price_medium2_electricity_p = GetAvgPriceEnergy(model.volume_medium2_voltage_electricity_techneed_plan_1, model.volume_medium2_voltage_electricity_houseneed_plan_1,
						model.volume_medium2_voltage_electricity_techneed_plan_2, model.volume_medium2_voltage_electricity_houseneed_plan_2,
						model.price_medium2_voltage_electricity_plan_1, model.price_medium2_voltage_electricity_plan_2);

					var avg_price_high_electricity_p = GetAvgPriceEnergy(model.volume_high_voltage_electricity_techneed_plan_1, model.volume_high_voltage_electricity_houseneed_plan_1,
						model.volume_high_voltage_electricity_techneed_plan_2, model.volume_high_voltage_electricity_houseneed_plan_2,
						model.price_high_voltage_electricity_plan_1, model.price_high_voltage_electricity_plan_2);

					//средняя цена эл. мощности за год (план)
					var avg_price_low_el_power_p = GetAvgPriceEnergy(model.volume_low_voltage_el_power_techneed_plan_1, model.volume_low_voltage_el_power_houseneed_plan_1,
						model.volume_low_voltage_el_power_techneed_plan_2, model.volume_low_voltage_el_power_houseneed_plan_2,
						model.price_low_voltage_el_power_plan_1, model.price_low_voltage_el_power_plan_2);

					var avg_price_medium1_el_power_p = GetAvgPriceEnergy(model.volume_medium1_voltage_el_power_techneed_plan_1, model.volume_medium1_voltage_el_power_houseneed_plan_1,
						model.volume_medium1_voltage_el_power_techneed_plan_2, model.volume_medium1_voltage_el_power_houseneed_plan_2,
						model.price_medium1_voltage_el_power_plan_1, model.price_medium1_voltage_el_power_plan_2);

					var avg_price_medium2_el_power_p = GetAvgPriceEnergy(model.volume_medium2_voltage_el_power_techneed_plan_1, model.volume_medium2_voltage_el_power_houseneed_plan_1,
						model.volume_medium2_voltage_el_power_techneed_plan_2, model.volume_medium2_voltage_el_power_houseneed_plan_2,
						model.price_medium2_voltage_el_power_plan_1, model.price_medium2_voltage_el_power_plan_2);

					var avg_price_high_el_power_p = GetAvgPriceEnergy(model.volume_high_voltage_el_power_techneed_plan_1, model.volume_high_voltage_el_power_houseneed_plan_1,
						model.volume_high_voltage_el_power_techneed_plan_2, model.volume_high_voltage_el_power_houseneed_plan_2,
						model.price_high_voltage_el_power_plan_1, model.price_high_voltage_el_power_plan_2);

					//факт (ГОД)
					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 1,
						(model.volume_low_voltage_electricity_techneed_plan_1 ?? 0) + (model.volume_low_voltage_electricity_techneed_plan_2 ?? 0),
						(model.volume_low_voltage_el_power_techneed_plan_1 ?? 0) + (model.volume_low_voltage_el_power_techneed_plan_2 ?? 0),
						(model.volume_low_voltage_electricity_houseneed_plan_1 ?? 0) + (model.volume_low_voltage_electricity_houseneed_plan_2 ?? 0),
						(model.volume_low_voltage_el_power_houseneed_plan_1 ?? 0) + (model.volume_low_voltage_el_power_houseneed_plan_2 ?? 0),
						await avg_price_low_electricity_p, await avg_price_low_el_power_p);

					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 2,
						(model.volume_medium1_voltage_electricity_techneed_plan_1 ?? 0) + (model.volume_medium1_voltage_electricity_techneed_plan_2 ?? 0),
						(model.volume_medium1_voltage_el_power_techneed_plan_1 ?? 0) + (model.volume_medium1_voltage_el_power_techneed_plan_2 ?? 0),
						(model.volume_medium1_voltage_electricity_houseneed_plan_1 ?? 0) + (model.volume_medium1_voltage_electricity_houseneed_plan_2 ?? 0),
						(model.volume_medium1_voltage_el_power_houseneed_plan_1 ?? 0) + (model.volume_medium1_voltage_el_power_houseneed_plan_2 ?? 0),
						await avg_price_medium1_electricity_p, await avg_price_medium1_el_power_p);

					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 3,
						(model.volume_medium2_voltage_electricity_techneed_plan_1 ?? 0) + (model.volume_medium2_voltage_electricity_techneed_plan_2 ?? 0),
						(model.volume_medium2_voltage_el_power_techneed_plan_1 ?? 0) + (model.volume_medium2_voltage_el_power_techneed_plan_2 ?? 0),
						(model.volume_medium2_voltage_electricity_houseneed_plan_1 ?? 0) + (model.volume_medium2_voltage_electricity_houseneed_plan_2 ?? 0),
						(model.volume_medium2_voltage_el_power_houseneed_plan_1 ?? 0) + (model.volume_medium2_voltage_el_power_houseneed_plan_2 ?? 0),
						await avg_price_medium2_electricity_p, await avg_price_medium2_el_power_p);

					await InsertUpdateElEnergyCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 4,
						(model.volume_high_voltage_electricity_techneed_plan_1 ?? 0) + (model.volume_high_voltage_electricity_techneed_plan_2 ?? 0),
						(model.volume_high_voltage_el_power_techneed_plan_1 ?? 0) + (model.volume_high_voltage_el_power_techneed_plan_2 ?? 0),
						(model.volume_high_voltage_electricity_houseneed_plan_1 ?? 0) + (model.volume_high_voltage_electricity_houseneed_plan_2 ?? 0),
						(model.volume_high_voltage_el_power_houseneed_plan_1 ?? 0) + (model.volume_high_voltage_el_power_houseneed_plan_2 ?? 0),
						await avg_price_high_electricity_p, await avg_price_high_el_power_p);

					await _context.SaveChangesAsync();

				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZHETransferCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на оплату труда
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZSalaryCosts_Save(TZSalaryCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 1, 1, model.count_prod_workers_fact_1,
						model.avg_salary_prod_workers_fact_1, model.soc_deduction_prod_workers_fact_1, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 1, 2, model.count_repair_workers_fact_1,
						model.avg_salary_repair_workers_fact_1, model.soc_deduction_repair_workers_fact_1, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 1, 3, model.count_shop_workers_fact_1,
						model.avg_salary_shop_workers_fact_1, model.soc_deduction_shop_workers_fact_1, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 1, 4, model.count_adm_manag_workers_fact_1,
						model.avg_salary_adm_manag_workers_fact_1, model.soc_deduction_adm_manag_workers_fact_1, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 1, 5, model.count_other_workers_fact_1,
						model.avg_salary_other_workers_fact_1, model.soc_deduction_other_workers_fact_1, false);

					//факт (2 полугодие)
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 2, 1, model.count_prod_workers_fact_2,
						model.avg_salary_prod_workers_fact_2, model.soc_deduction_prod_workers_fact_2, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 2, 2, model.count_repair_workers_fact_2,
						model.avg_salary_repair_workers_fact_2, model.soc_deduction_repair_workers_fact_2, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 2, 3, model.count_shop_workers_fact_2,
						model.avg_salary_shop_workers_fact_2, model.soc_deduction_shop_workers_fact_2, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 2, 4, model.count_adm_manag_workers_fact_2,
						model.avg_salary_adm_manag_workers_fact_2, model.soc_deduction_adm_manag_workers_fact_2, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 2, 5, model.count_other_workers_fact_2,
						model.avg_salary_other_workers_fact_2, model.soc_deduction_other_workers_fact_2, false);

					//Отчисления на соц. нужды, % (факт)
					var soc_deduction_prod_f = GetSocDeductionSalary(model.avg_salary_prod_workers_fact_1, model.avg_salary_prod_workers_fact_2,
						model.count_prod_workers_fact_1, model.count_prod_workers_fact_2, 
						model.soc_deduction_prod_workers_fact_1, model.soc_deduction_prod_workers_fact_2);
					var soc_deduction_repair_f = GetSocDeductionSalary(model.avg_salary_repair_workers_fact_1, model.avg_salary_repair_workers_fact_2,
						model.count_repair_workers_fact_1, model.count_repair_workers_fact_2,
						model.soc_deduction_repair_workers_fact_1, model.soc_deduction_repair_workers_fact_2);
					var soc_deduction_shop_f = GetSocDeductionSalary(model.avg_salary_shop_workers_fact_1, model.avg_salary_shop_workers_fact_2,
						model.count_shop_workers_fact_1, model.count_shop_workers_fact_2,
						model.soc_deduction_shop_workers_fact_1, model.soc_deduction_shop_workers_fact_2);
					var soc_deduction_adm_manag_f = GetSocDeductionSalary(model.avg_salary_adm_manag_workers_fact_1, model.avg_salary_adm_manag_workers_fact_2,
						model.count_adm_manag_workers_fact_1, model.count_adm_manag_workers_fact_2,
						model.soc_deduction_adm_manag_workers_fact_1, model.soc_deduction_adm_manag_workers_fact_2);
					var soc_deduction_other_f = GetSocDeductionSalary(model.avg_salary_other_workers_fact_1, model.avg_salary_other_workers_fact_2,
						model.count_other_workers_fact_1, model.count_other_workers_fact_2,
						model.soc_deduction_other_workers_fact_1, model.soc_deduction_other_workers_fact_2);

					//Среднемесячная оплата труда, руб./мес. (факт)
					var avg_salary_prod_f = GetAvgMonthSalary(model.avg_salary_prod_workers_fact_1, model.avg_salary_prod_workers_fact_2,
						model.count_prod_workers_fact_1, model.count_prod_workers_fact_2);
					var avg_salary_repair_f = GetAvgMonthSalary(model.avg_salary_repair_workers_fact_1, model.avg_salary_repair_workers_fact_2,
						model.count_repair_workers_fact_1, model.count_repair_workers_fact_2);
					var avg_salary_shop_f = GetAvgMonthSalary(model.avg_salary_shop_workers_fact_1, model.avg_salary_shop_workers_fact_2,
						model.count_shop_workers_fact_1, model.count_shop_workers_fact_2);
					var avg_salary_adm_f = GetAvgMonthSalary(model.avg_salary_adm_manag_workers_fact_1, model.avg_salary_adm_manag_workers_fact_2,
						model.count_adm_manag_workers_fact_1, model.count_adm_manag_workers_fact_2);
					var avg_salary_other_f = GetAvgMonthSalary(model.avg_salary_other_workers_fact_1, model.avg_salary_other_workers_fact_2,
						model.count_other_workers_fact_1, model.count_other_workers_fact_2);

					//Численность персонала, чел. (факт)
					var count_prod_f = GetCountWorkers(model.avg_salary_prod_workers_fact_1, model.avg_salary_prod_workers_fact_2,
						model.count_prod_workers_fact_1, model.count_prod_workers_fact_2, await avg_salary_prod_f);
					var count_repair_f = GetCountWorkers(model.avg_salary_repair_workers_fact_1, model.avg_salary_repair_workers_fact_2,
						model.count_repair_workers_fact_1, model.count_repair_workers_fact_2, await avg_salary_repair_f);
					var count_shop_f = GetCountWorkers(model.avg_salary_shop_workers_fact_1, model.avg_salary_shop_workers_fact_2,
						model.count_shop_workers_fact_1, model.count_shop_workers_fact_2, await avg_salary_shop_f);
					var count_adm_f = GetCountWorkers(model.avg_salary_adm_manag_workers_fact_1, model.avg_salary_adm_manag_workers_fact_2,
						model.count_adm_manag_workers_fact_1, model.count_adm_manag_workers_fact_2, await avg_salary_adm_f);
					var count_other_f = GetCountWorkers(model.avg_salary_other_workers_fact_1, model.avg_salary_other_workers_fact_2,
						model.count_other_workers_fact_1, model.count_other_workers_fact_2, await avg_salary_other_f);

					//факт (ГОД)
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 3, 1, await count_prod_f,
						await avg_salary_prod_f, await soc_deduction_prod_f, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 3, 2, await count_repair_f,
						await avg_salary_repair_f, await soc_deduction_repair_f, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 3, 3, await count_shop_f,
						await avg_salary_shop_f, await soc_deduction_shop_f, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 3, 4, await count_adm_f,
						await avg_salary_adm_f, await soc_deduction_adm_manag_f, false);
					await InsertUpdateSalaryCostsFact(model.tz_id, model.data_status, 3, 5, await count_other_f,
						await avg_salary_other_f, await soc_deduction_other_f, false);


					//план (1 полугодие)
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, 1, model.count_prod_workers_plan_1,
						model.avg_salary_prod_workers_plan_1, model.soc_deduction_prod_workers_plan_1);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, 2, model.count_repair_workers_plan_1,
						model.avg_salary_repair_workers_plan_1, model.soc_deduction_repair_workers_plan_1);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, 3, model.count_shop_workers_plan_1,
						model.avg_salary_shop_workers_plan_1, model.soc_deduction_shop_workers_plan_1);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, 4, model.count_adm_manag_workers_plan_1,
						model.avg_salary_adm_manag_workers_plan_1, model.soc_deduction_adm_manag_workers_plan_1);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, 5, model.count_other_workers_plan_1,
						model.avg_salary_other_workers_plan_1, model.soc_deduction_other_workers_plan_1);

					//план (2 полугодие)
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, 1, model.count_prod_workers_plan_2,
						model.avg_salary_prod_workers_plan_2, model.soc_deduction_prod_workers_plan_2);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, 2, model.count_repair_workers_plan_2,
						model.avg_salary_repair_workers_plan_2, model.soc_deduction_repair_workers_plan_2);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, 3, model.count_shop_workers_plan_2,
						model.avg_salary_shop_workers_plan_2, model.soc_deduction_shop_workers_plan_2);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, 4, model.count_adm_manag_workers_plan_2,
						model.avg_salary_adm_manag_workers_plan_2, model.soc_deduction_adm_manag_workers_plan_2);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, 5, model.count_other_workers_plan_2,
						model.avg_salary_other_workers_plan_2, model.soc_deduction_other_workers_plan_2);

					//Отчисления на соц. нужды, % (план)
					var soc_deduction_prod_p = GetSocDeductionSalary(model.avg_salary_prod_workers_plan_1, model.avg_salary_prod_workers_plan_2,
						model.count_prod_workers_plan_1, model.count_prod_workers_plan_2,
						model.soc_deduction_prod_workers_plan_1, model.soc_deduction_prod_workers_plan_2);
					var soc_deduction_repair_p = GetSocDeductionSalary(model.avg_salary_repair_workers_plan_1, model.avg_salary_repair_workers_plan_2,
						model.count_repair_workers_plan_1, model.count_repair_workers_plan_2,
						model.soc_deduction_repair_workers_plan_1, model.soc_deduction_repair_workers_plan_2);
					var soc_deduction_shop_p = GetSocDeductionSalary(model.avg_salary_shop_workers_plan_1, model.avg_salary_shop_workers_plan_2,
						model.count_shop_workers_plan_1, model.count_shop_workers_plan_2,
						model.soc_deduction_shop_workers_plan_1, model.soc_deduction_shop_workers_plan_2);
					var soc_deduction_adm_manag_p = GetSocDeductionSalary(model.avg_salary_adm_manag_workers_plan_1, model.avg_salary_adm_manag_workers_plan_2,
						model.count_adm_manag_workers_plan_1, model.count_adm_manag_workers_plan_2,
						model.soc_deduction_adm_manag_workers_plan_1, model.soc_deduction_adm_manag_workers_plan_2);
					var soc_deduction_other_p = GetSocDeductionSalary(model.avg_salary_other_workers_plan_1, model.avg_salary_other_workers_plan_2,
						model.count_other_workers_plan_1, model.count_other_workers_plan_2,
						model.soc_deduction_other_workers_plan_1, model.soc_deduction_other_workers_plan_2);

					//Среднемесячная оплата труда, руб./мес. (план)
					var avg_salary_prod_p = GetAvgMonthSalary(model.avg_salary_prod_workers_plan_1, model.avg_salary_prod_workers_plan_2,
						model.count_prod_workers_plan_1, model.count_prod_workers_plan_2);
					var avg_salary_repair_p = GetAvgMonthSalary(model.avg_salary_repair_workers_plan_1, model.avg_salary_repair_workers_plan_2,
						model.count_repair_workers_plan_1, model.count_repair_workers_plan_2);
					var avg_salary_shop_p = GetAvgMonthSalary(model.avg_salary_shop_workers_plan_1, model.avg_salary_shop_workers_plan_2,
						model.count_shop_workers_plan_1, model.count_shop_workers_plan_2);
					var avg_salary_adm_p = GetAvgMonthSalary(model.avg_salary_adm_manag_workers_plan_1, model.avg_salary_adm_manag_workers_plan_2,
						model.count_adm_manag_workers_plan_1, model.count_adm_manag_workers_plan_2);
					var avg_salary_other_p = GetAvgMonthSalary(model.avg_salary_other_workers_plan_1, model.avg_salary_other_workers_plan_2,
						model.count_other_workers_plan_1, model.count_other_workers_plan_2);

					//Численность персонала, чел. (план)
					var count_prod_p = GetCountWorkers(model.avg_salary_prod_workers_plan_1, model.avg_salary_prod_workers_plan_2,
						model.count_prod_workers_plan_1, model.count_prod_workers_plan_2, await avg_salary_prod_p);
					var count_repair_p = GetCountWorkers(model.avg_salary_repair_workers_plan_1, model.avg_salary_repair_workers_plan_2,
						model.count_repair_workers_plan_1, model.count_repair_workers_plan_2, await avg_salary_repair_p);
					var count_shop_p = GetCountWorkers(model.avg_salary_shop_workers_plan_1, model.avg_salary_shop_workers_plan_2,
						model.count_shop_workers_plan_1, model.count_shop_workers_plan_2, await avg_salary_shop_p);
					var count_adm_p = GetCountWorkers(model.avg_salary_adm_manag_workers_plan_1, model.avg_salary_adm_manag_workers_plan_2,
						model.count_adm_manag_workers_plan_1, model.count_adm_manag_workers_plan_2, await avg_salary_adm_p);
					var count_other_p = GetCountWorkers(model.avg_salary_other_workers_plan_1, model.avg_salary_other_workers_plan_2,
						model.count_other_workers_plan_1, model.count_other_workers_plan_2, await avg_salary_other_p);

					//план (ГОД)
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 1, await count_prod_p,
						await avg_salary_prod_p, await soc_deduction_prod_p);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 2, await count_repair_p,
						await avg_salary_repair_p, await soc_deduction_repair_p);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 3, await count_shop_p,
						await avg_salary_shop_p, await soc_deduction_shop_p);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 4, await count_adm_p,
						await avg_salary_adm_p, await soc_deduction_adm_manag_p);
					await InsertUpdateSalaryCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 5, await count_other_p,
						await avg_salary_other_p, await soc_deduction_other_p);

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZSalaryCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на приобретение сырья и материалов
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZMaterialsCosts_Save(TZMaterialsCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateMaterialsCostsFact(model.tz_id, model.data_status, 1, model.cost_reagents_fact_1, model.cost_gsm_fact_1,
						model.cost_repair_fact_1, model.cost_tech_service_fact_1, model.cost_spec_clothes_fact_1, model.cost_house_inventary_fact_1, 
						model.cost_other_fact_1, false);
					//факт (2 полугодие)
					await InsertUpdateMaterialsCostsFact(model.tz_id, model.data_status, 2, model.cost_reagents_fact_2, model.cost_gsm_fact_2,
						model.cost_repair_fact_2, model.cost_tech_service_fact_2, model.cost_spec_clothes_fact_2, model.cost_house_inventary_fact_2,
						model.cost_other_fact_2, false);
					//факт (ГОД)
					await InsertUpdateMaterialsCostsFact(model.tz_id, model.data_status, 3, 
						(model.cost_reagents_fact_1 ?? 0) + (model.cost_reagents_fact_2 ?? 0), 
						(model.cost_gsm_fact_1 ?? 0) + (model.cost_gsm_fact_2 ?? 0), 
						(model.cost_repair_fact_1 ?? 0) + (model.cost_repair_fact_2 ?? 0), 
						(model.cost_tech_service_fact_1 ?? 0) + (model.cost_tech_service_fact_2 ?? 0), 
						(model.cost_spec_clothes_fact_1 ?? 0) + (model.cost_spec_clothes_fact_2 ?? 0), 
						(model.cost_house_inventary_fact_1 ?? 0) + (model.cost_house_inventary_fact_2 ?? 0), 
						(model.cost_other_fact_1 ?? 0) + (model.cost_other_fact_2 ?? 0), false);

					//план (1 полугодие)
					await InsertUpdateMaterialsCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_reagents_plan_1, model.cost_gsm_plan_1,
						model.cost_repair_plan_1, model.cost_tech_service_plan_1, model.cost_spec_clothes_plan_1, model.cost_house_inventary_plan_1,
						model.cost_other_plan_1);
					//план (2 полугодие)
					await InsertUpdateMaterialsCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_reagents_plan_2, model.cost_gsm_plan_2,
						model.cost_repair_plan_2, model.cost_tech_service_plan_2, model.cost_spec_clothes_plan_2, model.cost_house_inventary_plan_2,
						model.cost_other_plan_2);
					//план (ГОД)
					await InsertUpdateMaterialsCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 
						(model.cost_reagents_plan_1 ?? 0) + (model.cost_reagents_plan_2 ?? 0),
						(model.cost_gsm_plan_1 ?? 0) + (model.cost_gsm_plan_2 ?? 0), 
						(model.cost_repair_plan_1 ?? 0) + (model.cost_repair_plan_2 ?? 0),
						(model.cost_tech_service_plan_1 ?? 0) + (model.cost_tech_service_plan_2 ?? 0), 
						(model.cost_spec_clothes_plan_1 ?? 0) + (model.cost_spec_clothes_plan_2 ?? 0),
						(model.cost_house_inventary_plan_1 ?? 0) + (model.cost_house_inventary_plan_2 ?? 0), 
						(model.cost_other_plan_1 ?? 0) + (model.cost_other_plan_2 ?? 0));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZMaterialsCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на оплату работ и услуг производственного характера, выполняемых по договорам со сторонними организациями
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZOtherOrgCosts_Save(TZOtherOrgCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateOtherOrgCostsFact(model.tz_id, model.data_status, 1, model.cost_transport_fact_1, model.cost_tech_regulation_fact_1,
						model.cost_other_support_prod_fact_1, model.cost_other_service_prod_fact_1, false);
					//факт (2 полугодие)
					await InsertUpdateOtherOrgCostsFact(model.tz_id, model.data_status, 2, model.cost_transport_fact_2, model.cost_tech_regulation_fact_2,
						model.cost_other_support_prod_fact_2, model.cost_other_service_prod_fact_2, false);
					//факт (ГОД)
					await InsertUpdateOtherOrgCostsFact(model.tz_id, model.data_status, 3, 
						(model.cost_transport_fact_1 ?? 0) + (model.cost_transport_fact_2 ?? 0),
						(model.cost_tech_regulation_fact_1 ?? 0) + (model.cost_tech_regulation_fact_2 ?? 0), 
						(model.cost_other_support_prod_fact_1 ?? 0) + (model.cost_other_support_prod_fact_2 ?? 0),
						(model.cost_other_service_prod_fact_1 ?? 0) + (model.cost_other_service_prod_fact_2 ?? 0), false);

					//план (1 полугодие)
					await InsertUpdateOtherOrgCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_transport_plan_1, 
						model.cost_tech_regulation_plan_1, model.cost_other_support_prod_plan_1, model.cost_other_service_prod_plan_1);
					//план (2 полугодие)
					await InsertUpdateOtherOrgCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_transport_plan_2,
						model.cost_tech_regulation_plan_2, model.cost_other_support_prod_plan_2, model.cost_other_service_prod_plan_2);
					//план (ГОД)
					await InsertUpdateOtherOrgCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 
						(model.cost_transport_plan_1 ?? 0) + (model.cost_transport_plan_2 ?? 0), 
						(model.cost_tech_regulation_plan_1 ?? 0) + (model.cost_tech_regulation_plan_2 ?? 0),
						(model.cost_other_support_prod_plan_1 ?? 0) + (model.cost_other_support_prod_plan_2 ?? 0),
						(model.cost_other_service_prod_plan_1 ?? 0) + (model.cost_other_service_prod_plan_2 ?? 0));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZOtherOrgCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на оплату иных работ и услуг, выполняемых по договорам с организациями
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZOrgServiceOtherCosts_Save(TZOrgServiceOtherCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateOrgOtherServiceCostsFact(model.tz_id, model.data_status, 1, model.cost_connect_service_fact_1, model.cost_security_service_fact_1,
						model.cost_communal_service_fact_1, model.cost_consulting_service_fact_1, model.cost_legal_service_fact_1, model.cost_inform_service_fact_1,
						model.cost_audit_service_fact_1, model.cost_strategic_manag_service_fact_1, model.cost_prepare_develop_service_fact_1,
						model.cost_targeted_funds_fact_1, model.cost_additional_insure_fact_1, model.cost_other_work_service_fact_1, false);
					//факт (2 полугодие)
					await InsertUpdateOrgOtherServiceCostsFact(model.tz_id, model.data_status, 2, model.cost_connect_service_fact_2, model.cost_security_service_fact_2,
						model.cost_communal_service_fact_2, model.cost_consulting_service_fact_2, model.cost_legal_service_fact_2, model.cost_inform_service_fact_2,
						model.cost_audit_service_fact_2, model.cost_strategic_manag_service_fact_2, model.cost_prepare_develop_service_fact_2,
						model.cost_targeted_funds_fact_2, model.cost_additional_insure_fact_2, model.cost_other_work_service_fact_2, false);
					//факт (ГОД)
					await InsertUpdateOrgOtherServiceCostsFact(model.tz_id, model.data_status, 3, 
						(model.cost_connect_service_fact_1 ?? 0) + (model.cost_connect_service_fact_2 ?? 0), 
						(model.cost_security_service_fact_1 ?? 0) + (model.cost_security_service_fact_2 ?? 0),
						(model.cost_communal_service_fact_1 ?? 0) + (model.cost_communal_service_fact_2 ?? 0), 
						(model.cost_consulting_service_fact_1 ?? 0) + (model.cost_consulting_service_fact_2 ?? 0), 
						(model.cost_legal_service_fact_1 ?? 0) + (model.cost_legal_service_fact_2 ?? 0), 
						(model.cost_inform_service_fact_1 ?? 0) + (model.cost_inform_service_fact_2 ?? 0),
						(model.cost_audit_service_fact_1 ?? 0) + (model.cost_audit_service_fact_2 ?? 0), 
						(model.cost_strategic_manag_service_fact_1 ?? 0) + (model.cost_strategic_manag_service_fact_2 ?? 0), 
						(model.cost_prepare_develop_service_fact_1 ?? 0) + (model.cost_prepare_develop_service_fact_2 ?? 0),
						(model.cost_targeted_funds_fact_1 ?? 0) + (model.cost_targeted_funds_fact_2 ?? 0), 
						(model.cost_additional_insure_fact_1 ?? 0) + (model.cost_additional_insure_fact_2 ?? 0), 
						(model.cost_other_work_service_fact_1 ?? 0) + (model.cost_other_work_service_fact_2 ?? 0), false);

					//план (1 полугодие)
					await InsertUpdateOrgOtherServiceCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_connect_service_plan_1, 
						model.cost_security_service_plan_1, model.cost_communal_service_plan_1, model.cost_consulting_service_plan_1, model.cost_legal_service_plan_1, 
						model.cost_inform_service_plan_1, model.cost_audit_service_plan_1, model.cost_strategic_manag_service_plan_1, model.cost_prepare_develop_service_plan_1,
						model.cost_targeted_funds_plan_1, model.cost_additional_insure_plan_1, model.cost_other_work_service_plan_1);
					//план (2 полугодие)
					await InsertUpdateOrgOtherServiceCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_connect_service_plan_2, 
						model.cost_security_service_plan_2, model.cost_communal_service_plan_2, model.cost_consulting_service_plan_2, model.cost_legal_service_plan_2,
						model.cost_inform_service_plan_2, model.cost_audit_service_plan_2, model.cost_strategic_manag_service_plan_2, model.cost_prepare_develop_service_plan_2,
						model.cost_targeted_funds_plan_2, model.cost_additional_insure_plan_2, model.cost_other_work_service_plan_2);
					//план (ГОД)
					await InsertUpdateOrgOtherServiceCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3,
						(model.cost_connect_service_plan_1 ?? 0) + (model.cost_connect_service_plan_2 ?? 0),
						(model.cost_security_service_plan_1 ?? 0) + (model.cost_security_service_plan_2 ?? 0),
						(model.cost_communal_service_plan_1 ?? 0) + (model.cost_communal_service_plan_2 ?? 0),
						(model.cost_consulting_service_plan_1 ?? 0) + (model.cost_consulting_service_plan_2 ?? 0),
						(model.cost_legal_service_plan_1 ?? 0) + (model.cost_legal_service_plan_2 ?? 0),
						(model.cost_inform_service_plan_1 ?? 0) + (model.cost_inform_service_plan_2 ?? 0),
						(model.cost_audit_service_plan_1 ?? 0) + (model.cost_audit_service_plan_2 ?? 0),
						(model.cost_strategic_manag_service_plan_1 ?? 0) + (model.cost_strategic_manag_service_plan_2 ?? 0),
						(model.cost_prepare_develop_service_plan_1 ?? 0) + (model.cost_prepare_develop_service_plan_2 ?? 0),
						(model.cost_targeted_funds_plan_1 ?? 0) + (model.cost_targeted_funds_plan_2 ?? 0),
						(model.cost_additional_insure_plan_1 ?? 0) + (model.cost_additional_insure_plan_2 ?? 0),
						(model.cost_other_work_service_plan_1 ?? 0) + (model.cost_other_work_service_plan_2 ?? 0));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZOrgServiceOtherCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на оплату иных работ и услуг, выполняемых по договорам с организациями
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZRepairFundingCosts_Save(TZRepairFundingCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateRepairFundingCostsFact(model.tz_id, model.data_status, 1, model.cost_repair_funding_fact_1, false);
					//факт (2 полугодие)
					await InsertUpdateRepairFundingCostsFact(model.tz_id, model.data_status, 2, model.cost_repair_funding_fact_2, false);
					//факт (ГОД)
					await InsertUpdateRepairFundingCostsFact(model.tz_id, model.data_status, 3, 
						(model.cost_repair_funding_fact_1 ?? 0) + (model.cost_repair_funding_fact_2 ?? 0), false);

					//план (1 полугодие)
					await InsertUpdateRepairFundingCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_repair_funding_plan_1);
					//план (2 полугодие)
					await InsertUpdateRepairFundingCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_repair_funding_plan_2);
					//план (ГОД)
					await InsertUpdateRepairFundingCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3,
						(model.cost_repair_funding_plan_1 ?? 0) + (model.cost_repair_funding_plan_2 ?? 0));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZRepairFundingCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на вывод из эксплуатации (в том числе на консервацию) и вывод из консервации
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZDecomissionCosts_Save(TZDecomissionCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateDecomissionCostsFact(model.tz_id, model.data_status, 1, model.cost_decomission_fact_1, false);
					//факт (2 полугодие)
					await InsertUpdateDecomissionCostsFact(model.tz_id, model.data_status, 2, model.cost_decomission_fact_2, false);
					//факт (ГОД)
					await InsertUpdateDecomissionCostsFact(model.tz_id, model.data_status, 3,
						(model.cost_decomission_fact_1 ?? 0) + (model.cost_decomission_fact_2 ?? 0), false);

					//план (1 полугодие)
					await InsertUpdateDecomissionCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_decomission_plan_1);
					//план (2 полугодие)
					await InsertUpdateDecomissionCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_decomission_plan_2);
					//план (ГОД)
					await InsertUpdateDecomissionCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3,
						(model.cost_decomission_plan_1 ?? 0) + (model.cost_decomission_plan_2 ?? 0));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZDecomissionCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на вывод из эксплуатации (в том числе на консервацию) и вывод из консервации
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZOtherOperatingsCosts_Save(TZOtherOperatingsCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateOtherOperatingsCostsFact(model.tz_id, model.data_status, 1, model.cost_travel_fact_1, model.cost_staff_train_fact_1, 
						model.cost_bank_service_fact_1, model.rent_nonprod_obj_state_prop_fact_1, model.rent_nonprod_obj_municipal_prop_fact_1,
						model.rent_nonprod_obj_other_fact_1, model.leasing_nonprod_obj_without_own_rights_fact_1, model.cost_other_operating_house_fact_1,
						model.cost_other_operating_fact_1, false);
					//факт (2 полугодие)
					await InsertUpdateOtherOperatingsCostsFact(model.tz_id, model.data_status, 2, model.cost_travel_fact_2, model.cost_staff_train_fact_2,
						model.cost_bank_service_fact_2, model.rent_nonprod_obj_state_prop_fact_2, model.rent_nonprod_obj_municipal_prop_fact_2,
						model.rent_nonprod_obj_other_fact_2, model.leasing_nonprod_obj_without_own_rights_fact_2, model.cost_other_operating_house_fact_2,
						model.cost_other_operating_fact_2, false);
					//факт (ГОД)
					await InsertUpdateOtherOperatingsCostsFact(model.tz_id, model.data_status, 3, model.cost_travel_fact_1 + model.cost_travel_fact_2,
						(model.cost_staff_train_fact_1 ?? 0) + (model.cost_staff_train_fact_2 ?? 0), (model.cost_bank_service_fact_1 ?? 0) + (model.cost_bank_service_fact_2 ?? 0),
						(model.rent_nonprod_obj_state_prop_fact_1 ?? 0) + (model.rent_nonprod_obj_state_prop_fact_2 ?? 0),
						(model.rent_nonprod_obj_municipal_prop_fact_1 ?? 0) + (model.rent_nonprod_obj_municipal_prop_fact_2 ?? 0),
						(model.rent_nonprod_obj_other_fact_1 ?? 0) + (model.rent_nonprod_obj_other_fact_2 ?? 0),
						(model.leasing_nonprod_obj_without_own_rights_fact_1 ?? 0) + (model.leasing_nonprod_obj_without_own_rights_fact_2 ?? 0),
						(model.cost_other_operating_house_fact_1 ?? 0) + (model.cost_other_operating_house_fact_2 ?? 0),
						(model.cost_other_operating_fact_1 ?? 0) + (model.cost_other_operating_fact_2 ?? 0), false);

					//план (1 полугодие)
					await InsertUpdateOtherOperatingsCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_travel_plan_1, 
						model.cost_staff_train_plan_1, model.cost_bank_service_plan_1, model.rent_nonprod_obj_state_prop_plan_1, 
						model.rent_nonprod_obj_municipal_prop_plan_1, model.rent_nonprod_obj_other_plan_1, model.leasing_nonprod_obj_without_own_rights_plan_1, 
						model.cost_other_operating_house_plan_1, model.cost_other_operating_plan_1);
					//план (2 полугодие)
					await InsertUpdateOtherOperatingsCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_travel_plan_2,
						model.cost_staff_train_plan_2, model.cost_bank_service_plan_2, model.rent_nonprod_obj_state_prop_plan_2,
						model.rent_nonprod_obj_municipal_prop_plan_2, model.rent_nonprod_obj_other_plan_2, model.leasing_nonprod_obj_without_own_rights_plan_2,
						model.cost_other_operating_house_plan_2, model.cost_other_operating_plan_2);
					//план (ГОД)
					await InsertUpdateOtherOperatingsCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 
						(model.cost_travel_plan_1 ?? 0) + (model.cost_travel_plan_2 ?? 0),
						(model.cost_staff_train_plan_1 ?? 0) + (model.cost_staff_train_plan_2 ?? 0), (model.cost_bank_service_plan_1 ?? 0) + (model.cost_bank_service_plan_2 ?? 0),
						(model.rent_nonprod_obj_state_prop_plan_1 ?? 0) + (model.rent_nonprod_obj_state_prop_plan_2 ?? 0),
						(model.rent_nonprod_obj_municipal_prop_plan_1 ?? 0) + (model.rent_nonprod_obj_municipal_prop_plan_2 ?? 0),
						(model.rent_nonprod_obj_other_plan_1 ?? 0) + (model.rent_nonprod_obj_other_plan_2 ?? 0),
						(model.leasing_nonprod_obj_without_own_rights_plan_1 ?? 0) + (model.leasing_nonprod_obj_without_own_rights_plan_2 ?? 0),
						(model.cost_other_operating_house_plan_1 ?? 0) + (model.cost_other_operating_house_plan_2 ?? 0),
						(model.cost_other_operating_plan_1 ?? 0) + (model.cost_other_operating_plan_2 ?? 0));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZOtherOperatingsCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на вывод из эксплуатации (в том числе на консервацию) и вывод из консервации
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZTaxesCosts_Save(TZTaxesCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateTaxesCostsFact(model.tz_id, model.data_status, 1, model.cost_emissions_pollutant_fact_1, model.cost_required_insure_fact_1,
						model.cost_tax_org_property_fact_1, model.cost_tax_land_fact_1, model.cost_tax_transport_fact_1,
						model.cost_tax_water_fact_1, model.cost_tax_other_fact_1, model.cost_other_pay_fact_1, false);
					//факт (2 полугодие)
					await InsertUpdateTaxesCostsFact(model.tz_id, model.data_status, 2, model.cost_emissions_pollutant_fact_2, model.cost_required_insure_fact_2,
						model.cost_tax_org_property_fact_2, model.cost_tax_land_fact_2, model.cost_tax_transport_fact_2,
						model.cost_tax_water_fact_2, model.cost_tax_other_fact_2, model.cost_other_pay_fact_2, false);
					//факт (ГОД)
					await InsertUpdateTaxesCostsFact(model.tz_id, model.data_status, 3, (model.cost_emissions_pollutant_fact_1 ?? 0) + (model.cost_emissions_pollutant_fact_2 ?? 0),
						(model.cost_required_insure_fact_1 ?? 0) + (model.cost_required_insure_fact_2 ?? 0), 
						(model.cost_tax_org_property_fact_1 ?? 0) + (model.cost_tax_org_property_fact_2 ?? 0),
						(model.cost_tax_land_fact_1 ?? 0) + (model.cost_tax_land_fact_2 ?? 0),
						(model.cost_tax_transport_fact_1 ?? 0) + (model.cost_tax_transport_fact_2 ?? 0),
						(model.cost_tax_water_fact_1 ?? 0) + (model.cost_tax_water_fact_2 ?? 0),
						(model.cost_tax_other_fact_1 ?? 0) + (model.cost_tax_other_fact_2 ?? 0),
						(model.cost_other_pay_fact_1 ?? 0) + (model.cost_other_pay_fact_2 ?? 0), false);

					//план (1 полугодие)
					await InsertUpdateTaxesCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_emissions_pollutant_plan_1, 
						model.cost_required_insure_plan_1, model.cost_tax_org_property_plan_1, model.cost_tax_land_plan_1, model.cost_tax_transport_plan_1,
						model.cost_tax_water_plan_1, model.cost_tax_other_plan_1, model.cost_other_pay_plan_1);
					//план (2 полугодие)
					await InsertUpdateTaxesCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_emissions_pollutant_plan_2,
						model.cost_required_insure_plan_2, model.cost_tax_org_property_plan_2, model.cost_tax_land_plan_2, model.cost_tax_transport_plan_2,
						model.cost_tax_water_plan_2, model.cost_tax_other_plan_2, model.cost_other_pay_plan_2);
					//план (ГОД)
					await InsertUpdateTaxesCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 
						(model.cost_emissions_pollutant_plan_1 ?? 0) + (model.cost_emissions_pollutant_plan_2 ?? 0),
						(model.cost_required_insure_plan_1 ?? 0) + (model.cost_required_insure_plan_2 ?? 0),
						(model.cost_tax_org_property_plan_1 ?? 0) + (model.cost_tax_org_property_plan_2 ?? 0),
						(model.cost_tax_land_plan_1 ?? 0) + (model.cost_tax_land_plan_2 ?? 0),
						(model.cost_tax_transport_plan_1 ?? 0) + (model.cost_tax_transport_plan_2 ?? 0),
						(model.cost_tax_water_plan_1 ?? 0) + (model.cost_tax_water_plan_2 ?? 0),
						(model.cost_tax_other_plan_1 ?? 0) + (model.cost_tax_other_plan_2 ?? 0),
						(model.cost_other_pay_plan_1 ?? 0) + (model.cost_other_pay_plan_2 ?? 0));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZTaxesCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на амортизационные начисления
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZAmortizationCosts_Save(TZAmortizationCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateAmortizationCostsFact(model.tz_id, model.data_status, 1, model.amortization_prod_equip_fact_1, 
						model.amortization_other_means_fact_1, false);
					//факт (2 полугодие)
					await InsertUpdateAmortizationCostsFact(model.tz_id, model.data_status, 2, model.amortization_prod_equip_fact_2,
						model.amortization_other_means_fact_2, false);
					//факт (ГОД)
					await InsertUpdateAmortizationCostsFact(model.tz_id, model.data_status, 3, 
						(model.amortization_prod_equip_fact_1 ?? 0) + (model.amortization_prod_equip_fact_2 ?? 0),
						(model.amortization_other_means_fact_1 ?? 0) + (model.amortization_other_means_fact_2 ?? 0), false);

					//план (1 полугодие)
					await InsertUpdateAmortizationCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.amortization_prod_equip_plan_1,
						model.amortization_other_means_plan_1);
					//план (2 полугодие)
					await InsertUpdateAmortizationCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.amortization_prod_equip_plan_2,
						model.amortization_other_means_plan_2);
					//план (ГОД)
					await InsertUpdateAmortizationCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3,
						(model.amortization_prod_equip_plan_1 ?? 0) + (model.amortization_prod_equip_plan_2 ?? 0),
						(model.amortization_other_means_plan_1 ?? 0) + (model.amortization_other_means_plan_2 ?? 0));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZAmortizationCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на оплату услуг, оказываемых организациями, осуществляющими регулируемые виды деятельности
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZRegOrgServiceCosts_Save(TZRegOrgServiceCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateRegOrgServiceCostsFact(model.tz_id, model.data_status, 1, model.cost_reg_org_service_fact_1, false);
					//факт (2 полугодие)
					await InsertUpdateRegOrgServiceCostsFact(model.tz_id, model.data_status, 2, model.cost_reg_org_service_fact_2, false);
					//факт (ГОД)
					await InsertUpdateRegOrgServiceCostsFact(model.tz_id, model.data_status, 3, 
						(model.cost_reg_org_service_fact_1 ?? 0) + (model.cost_reg_org_service_fact_2 ?? 0), false);

					//план (1 полугодие)
					await InsertUpdateRegOrgServiceCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_reg_org_service_plan_1);
					//план (2 полугодие)
					await InsertUpdateRegOrgServiceCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_reg_org_service_plan_2);
					//план (ГОД)
					await InsertUpdateRegOrgServiceCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3,
						(model.cost_reg_org_service_plan_1 ?? 0) + (model.cost_reg_org_service_plan_2 ?? 0));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZRegOrgServiceCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по расходам на налог на прибыль
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZIncomeTaxCosts_Save(TZIncomeTaxCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateIncomeTaxCostsFact(model.tz_id, model.data_status, 1, model.cost_income_tax_fact_1, false);
					//факт (2 полугодие)
					await InsertUpdateIncomeTaxCostsFact(model.tz_id, model.data_status, 2, model.cost_income_tax_fact_2, false);
					//факт (ГОД)
					await InsertUpdateIncomeTaxCostsFact(model.tz_id, model.data_status, 3, (model.cost_income_tax_fact_1 ?? 0) + (model.cost_income_tax_fact_2 ?? 0), false);

					//план (1 полугодие)
					await InsertUpdateIncomeTaxCostsPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.cost_income_tax_plan_1);
					//план (2 полугодие)
					await InsertUpdateIncomeTaxCostsPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.cost_income_tax_plan_2);
					//план (ГОД)
					await InsertUpdateIncomeTaxCostsPlan(model.tz_id, model.data_status, model.perspective_year, 3, 
						(model.cost_income_tax_plan_1 ?? 0) + (model.cost_income_tax_plan_2 ?? 0));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZIncomeTaxCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по прочим неподконтрольным расходам
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZUncontrolOtherCosts_Save(TZUncontrolOtherCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateUncontrolOtherCostsFact(model, 1, "_fact_", false);
					//факт (2 полугодие)
					await InsertUpdateUncontrolOtherCostsFact(model, 2, "_fact_", false);
					//факт (ГОД)
					await InsertUpdateUncontrolOtherCostsFact(model, 3, "_fact_", false);

					//план (1 полугодие)
					await InsertUpdateUncontrolOtherCostsPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateUncontrolOtherCostsPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateUncontrolOtherCostsPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZUncontrolOtherCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//по нормативному удельному расходу топлива на отпуск тепла (только план) (это уже не актуально, т.к. расчёт поменялся)
		/*[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZFuelNormConsumption_Save(TZFuelPriceDataViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//план (1 полугодие)
					await InsertUpdateFuelNormConsumptionPlan(model.tz_id, model.data_status, model.perspective_year, 1, model.norm_consump_fuel_plan_1);
					//план (2 полугодие)
					await InsertUpdateFuelNormConsumptionPlan(model.tz_id, model.data_status, model.perspective_year, 2, model.norm_consump_fuel_plan_2);
					//план (ГОД)
					await InsertUpdateFuelNormConsumptionPlan(model.tz_id, model.data_status, model.perspective_year, 3, 
						(model.norm_consump_fuel_plan_1 * model.ghe_minus_hc_plan_1 + model.norm_consump_fuel_plan_2 * model.ghe_minus_hc_plan_2)
							/ (model.ghe_minus_hc_plan_1 + model.ghe_minus_hc_plan_2));

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZFuelNormConsumption_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}*/

		//сохранение данных по Распределению расходов на производство и транспорт
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZCostDistribution_Save(TZCalcCostsDistributionViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					var tz_upd = await _context.TZ_CoefsСostDistribution_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status).FirstOrDefaultAsync();

					if (tz_upd != null)
					{
						tz_upd.coef_cost_on_transport_fact = model.coef_cost_on_transport_fact_3;
						tz_upd.coef_amortiz_on_transport_fact = model.coef_amortiz_on_transport_fact_3;
					}
					else
					{
						var new_tz = new TZ_CoefsСostDistribution_Fact()
						{
							tz_id = model.tz_id,
							data_status = model.data_status,
							coef_cost_on_transport_fact = model.coef_cost_on_transport_fact_3,
							coef_amortiz_on_transport_fact = model.coef_amortiz_on_transport_fact_3,
							is_deleted = false,
							create_date = DateTime.Now,
							user_id = userId
						};
						await _context.TZ_CoefsСostDistribution_Fact.AddAsync(new_tz);
					}

					var tz_upd_p = await _context.TZ_CoefsСostDistribution_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status 
						&& x.perspective_year == model.perspective_year).FirstOrDefaultAsync();

					if (tz_upd_p != null)
					{
						tz_upd_p.coef_cost_on_transport_plan = model.coef_cost_on_transport_plan_3;
						tz_upd_p.coef_amortiz_on_transport_plan = model.coef_amortiz_on_transport_plan_3;
					}
					else
					{
						var new_tz_p = new TZ_CoefsСostDistribution_Plan()
						{
							tz_id = model.tz_id,
							data_status = model.data_status,
							perspective_year = model.perspective_year,
							coef_cost_on_transport_plan = model.coef_cost_on_transport_plan_3,
							coef_amortiz_on_transport_plan = model.coef_amortiz_on_transport_plan_3,
							create_date = DateTime.Now,
							user_id = userId
						};
						await _context.TZ_CoefsСostDistribution_Plan.AddAsync(new_tz_p);
					}

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZCostDistribution_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
		#endregion

		#region Get_Data_From_Form_НВВ
		//сохранение данных по прибыли (НВВ) 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZProfitNVV_Save(TZProfitNVVViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateProfitNVVFact(model, 1, "_fact_", false);
					//факт (2 полугодие)
					await InsertUpdateProfitNVVFact(model, 2, "_fact_", false);
					//факт (ГОД)
					await InsertUpdateProfitNVVFact(model, 3, "_fact_", false);

					//план (1 полугодие)
					await InsertUpdateProfitNVVPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateProfitNVVPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateProfitNVVPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZProfitNVV_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по недополученным доходам/выпадающим расходам (НВВ) 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZLostProfitOutCosts_Save(TZLostProfitOutCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//план (1 полугодие)
					await InsertUpdateLostProfitOutCostsPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateLostProfitOutCostsPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateLostProfitOutCostsPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZLostProfitOutCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по избытку средств (НВВ) 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZExcessFunds_Save(TZExcessFundsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//план (1 полугодие)
					await InsertUpdateExcessFundsPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateExcessFundsPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateExcessFundsPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZExcessFunds_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по объему и финансированию капитальных вложений (НВВ) 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZCapitalInvestment_Save(TZCapitalInvestmentViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//план (1 полугодие)
					await InsertUpdateCapitalInvestmentPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateCapitalInvestmentPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateCapitalInvestmentPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZCapitalInvestment_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по перекрёстному субсидированию (НВВ) 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZCrossSubsidy_Save(TZCrossSubsidyViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//план (1 полугодие)
					await InsertUpdateCrossSubsidyPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateCrossSubsidyPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateCrossSubsidyPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZCrossSubsidy_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по экономии операционных и неподконтрольных расходов (НВВ) 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZEconomyCosts_Save(TZEconomyCostsViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//план (1 полугодие)
					await InsertUpdateEconomyCostsPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateEconomyCostsPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateEconomyCostsPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZEconomyCosts_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по индексам (НВВ) 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZIndexesNVV_Save(TZIndexesNVVViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//план (1 полугодие)
					await InsertUpdateIndexesNVVPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateIndexesNVVPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateIndexesNVVPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZIndexesNVV_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по методу индексации (НВВ) 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZMethodIndexationNVV_Save(TZMethodIndexationViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//план (1 полугодие)
					await InsertUpdateMethodIndexationNVVPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateMethodIndexationNVVPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateMethodIndexationNVVPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZMethodIndexationNVV_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по методу доходности инвестированного капитала (НВВ) 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZMethodProfitInvestNVV_Save(TZMethodProfitInvestViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//план (1 полугодие)
					await InsertUpdateMethodProfitInvestNVVPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateMethodProfitInvestNVVPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateMethodProfitInvestNVVPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZMethodProfitInvestNVV_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по методу сравнения аналогов (НВВ) 
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZMethodAnaloguesComparisonNVV_Save(TZMethodAnaloguesComparisonViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//план (1 полугодие)
					await InsertUpdateMethodAnaloguesComparisonNVVPlan(model, 1, "_plan_");
					//план (2 полугодие)
					await InsertUpdateMethodAnaloguesComparisonNVVPlan(model, 2, "_plan_");
					//план (ГОД)
					await InsertUpdateMethodAnaloguesComparisonNVVPlan(model, 3, "_plan_");

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZMethodAnaloguesComparisonNVV_Save", $"tz_id={model.tz_id},data_status={model.data_status},perspective_year={model.perspective_year}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
		#endregion

		#region Get_Data_From_Form_ИП_ТС_и_КС
		//сохранение данных по Фактическим расходам на финансирование в соответствии с ИП ТС и КС
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZExpenses_IPHS_CA_Save(TZExpenses_IPHS_CA_ViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateExpenses_IPHS_CA_Fact(model, 1, model.finance_type_id, "_fact_", false);
					//факт (2 полугодие)
					await InsertUpdateExpenses_IPHS_CA_Fact(model, 2, model.finance_type_id, "_fact_", false);
					//факт (ГОД)
					await InsertUpdateExpenses_IPHS_CA_Fact(model, 3, model.finance_type_id, "_fact_", false);

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZExpenses_IPHS_CA_Save", $"tz_id={model.tz_id},data_status={model.data_status},finance_type_id={model.finance_type_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		//сохранение данных по Фактическому объему дотаций и полученным доходам сверх реализации по утвержденным тарифам
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TZExpensesAddValues_Save(TZExpensesAddValues_ViewModel model)
		{
			try
			{
				if (model.tz_id > 0)
				{
					//факт (1 полугодие)
					await InsertUpdateExpensesAddValues_Fact(model, 1, "_fact_", false);
					//факт (2 полугодие)
					await InsertUpdateExpensesAddValues_Fact(model, 2, "_fact_", false);
					//факт (ГОД)
					await InsertUpdateExpensesAddValues_Fact(model, 3, "_fact_", false);

					await _context.SaveChangesAsync();
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TZExpensesAddValues_Save", $"tz_id={model.tz_id},data_status={model.data_status}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		#endregion

		#region Дополнительные_функции
		//рассчёт Численности персонала, чел.
		[NonAction]
		public async Task<decimal> GetCountWorkers(decimal? avg_salary_1, decimal? avg_salary_2, decimal? count_workers_1, decimal? count_workers_2,
			decimal avg_salary)
		{
			decimal avg_count = 0;
			if (avg_salary > 0)
			{
				avg_count = (((avg_salary_1 ?? 0) * (count_workers_1 ?? 0) * 6) + ((avg_salary_2 ?? 0) * (count_workers_2 ?? 0) * 6)) 
					/ (avg_salary * 12);
			}
			return avg_count;
		}

		//рассчёт отчислений на соц. нужды, % (за год)
		[NonAction]
		public async Task<decimal> GetSocDeductionSalary(decimal? avg_salary_1, decimal? avg_salary_2, decimal? count_workers_1, decimal? count_workers_2,
			decimal? soc_deduction_workers_1, decimal? soc_deduction_workers_2)
		{
			decimal divider = ((avg_salary_1 ?? 0) * (count_workers_1 ?? 0)) + ((avg_salary_2 ?? 0) * (count_workers_2 ?? 0));

			decimal soc_deduction = 0;
			if (divider > 0)
			{
				soc_deduction = (((avg_salary_1 ?? 0) * (count_workers_1 ?? 0) * (soc_deduction_workers_1 ?? 0))
								+ ((avg_salary_2 ?? 0) * (count_workers_2 ?? 0) * (soc_deduction_workers_2 ?? 0)))
							/ divider;
			}
			return soc_deduction;
		}
		//рассчёт среднемесячной оплаты труда, руб./мес.
		[NonAction]
		public async Task<decimal> GetAvgMonthSalary(decimal? avg_salary_1, decimal? avg_salary_2, decimal? count_workers_1, decimal? count_workers_2)
		{
			decimal divider = (count_workers_1 ?? 0) + (count_workers_2 ?? 0);

			decimal avg_salary = 0;
			if (divider > 0)
			{
				avg_salary = (((avg_salary_1 ?? 0) * (count_workers_1 ?? 0)) + ((avg_salary_2 ?? 0) * (count_workers_2 ?? 0))) / divider;
			}
			return avg_salary;
		}

		//рассчёт средней цены энергии
		[NonAction]
		public async Task<decimal> GetAvgPriceEnergy(decimal? volume_techneed_1, decimal? volume_houseneed_1, decimal? volume_techneed_2, decimal? volume_houseneed_2,
			decimal? price_1, decimal? price_2)
		{
			decimal vol_sum = (volume_techneed_1 ?? 0) + (volume_houseneed_1 ?? 0) + (volume_techneed_2 ?? 0) + (volume_houseneed_2 ?? 0);

			decimal avg_price = 0;
			if (vol_sum > 0)
			{
				avg_price = (((volume_houseneed_1 ?? 0) + (volume_techneed_1 ?? 0)) * (price_1 ?? 0)
								+ ((volume_houseneed_2 ?? 0) + (volume_techneed_2 ?? 0)) * (price_2 ?? 0))
							/ vol_sum;
			}
			return avg_price;
		}

		//получение значения из модели по имени атрибута
		[NonAction]
		public decimal GetDecimalValue(object model, string name, short report_period_id)
		{
			object d_value;
			if (report_period_id == 3)
			{
				object d_value_1 = model.GetType().GetProperties().Where(a => a.Name == name + "1").First().GetValue(model, null) ?? 0;
				object d_value_2 = model.GetType().GetProperties().Where(a => a.Name == name + "2").First().GetValue(model, null) ?? 0;
				d_value = Convert.ToDecimal(d_value_1) + Convert.ToDecimal(d_value_2);
			}
			else
			{
				d_value = model.GetType().GetProperties().Where(a => a.Name == name + report_period_id.ToString()).First().GetValue(model, null) ?? 0;
			}

			return Convert.ToDecimal(d_value);
		}
		#endregion

		#region Create_Models
		//Создание модели для добавления новой записи по затратам воды (водоотведению) на тех. и хоз. нужды (факт)
		[NonAction]
		public TZ_WaterTechNeeds_Fact GetNewTZWaterCostsFactModel(int tz_id, int data_status, short report_period_id,
			decimal? volume_buy_coldwater_fact, decimal? price_coldwater_fact, decimal? volume_waterdispos_fact, decimal? price_waterdispos_fact, bool? is_deleted)
		{
			var new_tz = new TZ_WaterTechNeeds_Fact()
			{
				tz_id = tz_id,
				data_status = data_status,
				report_period_id = report_period_id,
				volume_buy_coldwater_fact = volume_buy_coldwater_fact,
				price_coldwater_fact = price_coldwater_fact,
				volume_waterdispos_fact = volume_waterdispos_fact,
				price_waterdispos_fact = price_waterdispos_fact,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz;
		}
		//Создание модели для добавления новой записи по затратам воды (водоотведению) на тех. и хоз. нужды (план)
		[NonAction]
		public TZ_WaterTechNeeds_Plan GetNewTZWaterCostsPlanModel(int tz_id, int data_status, int perspective_year, short report_period_id,
			decimal? volume_buy_coldwater_plan, decimal? price_coldwater_plan, decimal? volume_waterdispos_plan, decimal? price_waterdispos_plan, bool? is_deleted)
		{
			var new_tz = new TZ_WaterTechNeeds_Plan()
			{
				tz_id = tz_id,
				data_status = data_status,
				perspective_year = perspective_year,
				report_period_id = report_period_id,
				volume_buy_coldwater_plan = volume_buy_coldwater_plan,
				price_coldwater_plan = price_coldwater_plan,
				volume_waterdispos_plan = volume_waterdispos_plan,
				price_waterdispos_plan = price_waterdispos_plan,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz;
		}

		//Создание модели для добавления новой записи по расходам, связанным с созданием норнмативных запасов топлива (факт)
		[NonAction]
		public TZ_CostFuelReserve_Fact GetNewTZFuelReserveCostsFactModel(int tz_id, int data_status, short report_period_id, decimal? cost_fuel_reserve, bool? is_deleted)
		{
			var new_tz = new TZ_CostFuelReserve_Fact()
			{
				tz_id = tz_id,
				data_status = data_status,
				report_period_id = report_period_id,
				cost_fuel_reserve_fact = cost_fuel_reserve,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz;
		}
		//Создание модели для добавления новой записи по расходам, связанным с созданием норнмативных запасов топлива (план)
		[NonAction]
		public TZ_CostFuelReserve_Plan GetNewTZFuelReserveCostsPlanModel(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_fuel_reserve)
		{
			var new_tz = new TZ_CostFuelReserve_Plan()
			{
				tz_id = tz_id,
				data_status = data_status,
				perspective_year = perspective_year,
				report_period_id = report_period_id,
				cost_fuel_reserve_plan = cost_fuel_reserve,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz;
		}

		//Создание модели для добавления новой записи по покупке теплоносителя (факт)
		[NonAction]
		public TZ_HeatCarrierTechNeeds_Fact GetNewTZHeatCarrierCostsFactModel(int tz_id, int data_status, short report_period_id, decimal? cost_heat_carrier,
			decimal? volume_buy_heat_carrier, bool? is_deleted)
		{
			var new_tz = new TZ_HeatCarrierTechNeeds_Fact()
			{
				tz_id = tz_id,
				data_status = data_status,
				report_period_id = report_period_id,
				cost_heat_carrier_fact = cost_heat_carrier,
				volume_buy_heat_carrier_fact = volume_buy_heat_carrier,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz;
		}
		//Создание модели для добавления новой записи по покупке теплоносителя (план)
		[NonAction]
		public TZ_HeatCarrierTechNeeds_Plan GetNewTZHeatCarrierCostsPlanModel(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_heat_carrier,
			decimal? volume_buy_heat_carrier)
		{
			var new_tz = new TZ_HeatCarrierTechNeeds_Plan()
			{
				tz_id = tz_id,
				data_status = data_status,
				perspective_year = perspective_year,
				report_period_id = report_period_id,
				cost_heat_carrier_plan = cost_heat_carrier,
				volume_buy_heat_carrier_plan = volume_buy_heat_carrier,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz;
		}

		//Создание модели для добавления новой записи по оплате услуг по передаче тепловой энергии (факт)
		[NonAction]
		public TZ_CostHeatTransfer_Fact GetNewTZHeatTransferCostsFactModel(int tz_id, int data_status, short report_period_id, decimal? cost_heattrans_service)
		{
			var new_tz = new TZ_CostHeatTransfer_Fact()
			{
				tz_id = tz_id,
				data_status = data_status,
				report_period_id = report_period_id,
				cost_heattrans_service_fact = cost_heattrans_service,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz;
		}

		//Создание модели для добавления новой записи по оплате услуг по передаче тепловой энергии (план)
		[NonAction]
		public TZ_CostHeatTransfer_Plan GetNewTZHeatTransferCostsPlanModel(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_heattrans_service)
		{
			var new_tz = new TZ_CostHeatTransfer_Plan()
			{
				tz_id = tz_id,
				data_status = data_status,
				perspective_year = perspective_year,
				report_period_id = report_period_id,
				cost_heattrans_service_plan = cost_heattrans_service,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_tz;
		}

		//вставка и обновление данных по затратам электроэнергии (факт)
		[NonAction]
		public async Task<bool> InsertUpdateElEnergyCostsFact(int tz_id, int data_status, short report_period_id, short voltage_type_id,
			decimal? volume_electricity_techneed, decimal? volume_power_techneed, decimal? volume_electricity_houseneed,
			decimal? volume_power_houseneed, decimal? price_buy_electricity, decimal? price_buy_power, bool? is_deleted)
		{

			var tz_upd = await _context.TZ_ElectroEnergyAllNeeds_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.voltage_type_id == voltage_type_id && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.volume_electricity_houseneed_fact = volume_electricity_houseneed ?? 0;
				tz_upd.volume_electricity_techneed_fact = volume_electricity_techneed ?? 0;
				tz_upd.volume_power_houseneed_fact = volume_power_houseneed ?? 0;
				tz_upd.volume_power_techneed_fact = volume_power_techneed ?? 0;
				tz_upd.price_buy_electricity_fact = price_buy_electricity ?? 0;
				tz_upd.price_buy_power_fact = price_buy_power ?? 0;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_ElectroEnergyAllNeeds_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					voltage_type_id = voltage_type_id,
					volume_electricity_houseneed_fact = volume_electricity_houseneed ?? 0,
					volume_electricity_techneed_fact = volume_electricity_techneed ?? 0,
					volume_power_techneed_fact = volume_power_techneed ?? 0,
					volume_power_houseneed_fact = volume_power_houseneed ?? 0,
					price_buy_electricity_fact = price_buy_electricity ?? 0,
					price_buy_power_fact = price_buy_power ?? 0,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_ElectroEnergyAllNeeds_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам электроэнергии (план)
		[NonAction]
		public async Task<bool> InsertUpdateElEnergyCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, short voltage_type_id,
			decimal? volume_electricity_techneed, decimal? volume_power_techneed, decimal? volume_electricity_houseneed,
			decimal? volume_power_houseneed, decimal? price_buy_electricity, decimal? price_buy_power)
		{

			var tz_upd = await _context.TZ_ElectroEnergyAllNeeds_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.voltage_type_id == voltage_type_id && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.volume_electricity_houseneed_plan = volume_electricity_houseneed ?? 0;
				tz_upd.volume_electricity_techneed_plan = volume_electricity_techneed ?? 0;
				tz_upd.volume_power_houseneed_plan = volume_power_houseneed ?? 0;
				tz_upd.volume_power_techneed_plan = volume_power_techneed ?? 0;
				tz_upd.price_buy_electricity_plan = price_buy_electricity ?? 0;
				tz_upd.price_buy_power_plan = price_buy_power ?? 0;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_ElectroEnergyAllNeeds_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					voltage_type_id = voltage_type_id,
					volume_electricity_houseneed_plan = volume_electricity_houseneed ?? 0,
					volume_electricity_techneed_plan = volume_electricity_techneed ?? 0,
					volume_power_techneed_plan = volume_power_techneed ?? 0,
					volume_power_houseneed_plan = volume_power_houseneed ?? 0,
					price_buy_electricity_plan = price_buy_electricity ?? 0,
					price_buy_power_plan = price_buy_power ?? 0,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_ElectroEnergyAllNeeds_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на оплату труда (факт)
		[NonAction]
		public async Task<bool> InsertUpdateSalaryCostsFact(int tz_id, int data_status, short report_period_id, short staff_type_id,
			decimal? count_workers_fact, decimal? average_salary_fact, decimal? deduction_soc_need_fact, bool? is_deleted)
		{

			var tz_upd = await _context.TZ_StaffValues_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.staff_type_id == staff_type_id && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.count_workers_fact = count_workers_fact ?? 0;
				tz_upd.average_salary_fact = average_salary_fact ?? 0;
				tz_upd.deduction_soc_need_fact = deduction_soc_need_fact ?? 0;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_StaffValues_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					staff_type_id = staff_type_id,
					count_workers_fact = count_workers_fact ?? 0,
					average_salary_fact = average_salary_fact ?? 0,
					deduction_soc_need_fact = deduction_soc_need_fact ?? 0,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_StaffValues_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на оплату труда (план)
		[NonAction]
		public async Task<bool> InsertUpdateSalaryCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, short staff_type_id,
			decimal? count_workers_plan, decimal? average_salary_plan, decimal? deduction_soc_need_plan)
		{

			var tz_upd = await _context.TZ_StaffValues_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.staff_type_id == staff_type_id && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.count_workers_plan = count_workers_plan ?? 0;
				tz_upd.average_salary_plan = average_salary_plan ?? 0;
				tz_upd.deduction_soc_need_plan = deduction_soc_need_plan ?? 0;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_StaffValues_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					staff_type_id = staff_type_id,
					count_workers_plan = count_workers_plan ?? 0,
					average_salary_plan = average_salary_plan ?? 0,
					deduction_soc_need_plan = deduction_soc_need_plan ?? 0,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_StaffValues_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на приобретение сырья и материалов (факт)
		[NonAction]
		public async Task<bool> InsertUpdateMaterialsCostsFact(int tz_id, int data_status, short report_period_id, decimal? cost_reagents, 
			decimal? cost_gsm, decimal? cost_repair, decimal? cost_tech_service, decimal? cost_spec_clothes, decimal? cost_house_inventary, 
			decimal? cost_other, bool? is_deleted)
		{

			var tz_upd = await _context.TZ_CostRawMaterials_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_reagents_fact = cost_reagents ?? 0;
				tz_upd.cost_gsm_fact = cost_gsm ?? 0;
				tz_upd.cost_repair_fact = cost_repair ?? 0;
				tz_upd.cost_tech_service_fact = cost_tech_service ?? 0;
				tz_upd.cost_spec_clothes_fact = cost_spec_clothes ?? 0;
				tz_upd.cost_house_inventary_fact = cost_house_inventary ?? 0;
				tz_upd.cost_other_fact = cost_other ?? 0;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostRawMaterials_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					cost_reagents_fact = cost_reagents ?? 0,
					cost_gsm_fact = cost_gsm ?? 0,
					cost_repair_fact = cost_repair ?? 0,
					cost_tech_service_fact = cost_tech_service ?? 0,
					cost_spec_clothes_fact = cost_spec_clothes ?? 0,
					cost_house_inventary_fact = cost_house_inventary ?? 0,
					cost_other_fact = cost_other ?? 0,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostRawMaterials_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на приобретение сырья и материалов (план)
		[NonAction]
		public async Task<bool> InsertUpdateMaterialsCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_reagents,
			decimal? cost_gsm, decimal? cost_repair, decimal? cost_tech_service, decimal? cost_spec_clothes, decimal? cost_house_inventary,
			decimal? cost_other)
		{

			var tz_upd = await _context.TZ_CostRawMaterials_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_reagents_plan = cost_reagents ?? 0;
				tz_upd.cost_gsm_plan = cost_gsm ?? 0;
				tz_upd.cost_repair_plan = cost_repair ?? 0;
				tz_upd.cost_tech_service_plan = cost_tech_service ?? 0;
				tz_upd.cost_spec_clothes_plan = cost_spec_clothes ?? 0;
				tz_upd.cost_house_inventary_plan = cost_house_inventary ?? 0;
				tz_upd.cost_other_plan = cost_other ?? 0;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostRawMaterials_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					cost_reagents_plan = cost_reagents ?? 0,
					cost_gsm_plan = cost_gsm ?? 0,
					cost_repair_plan = cost_repair ?? 0,
					cost_tech_service_plan = cost_tech_service ?? 0,
					cost_spec_clothes_plan = cost_spec_clothes ?? 0,
					cost_house_inventary_plan = cost_house_inventary ?? 0,
					cost_other_plan = cost_other ?? 0,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostRawMaterials_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на оплату работ и услуг производственного характера, выполняемых по договорам со сторонними организациями (факт)
		[NonAction]
		public async Task<bool> InsertUpdateOtherOrgCostsFact(int tz_id, int data_status, short report_period_id, decimal? cost_transport,
			decimal? cost_tech_regulation, decimal? cost_other_support_prod, decimal? cost_other_service_prod, bool? is_deleted)
		{

			var tz_upd = await _context.TZ_CostOtherOrg_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_transport_fact = cost_transport ?? 0;
				tz_upd.cost_tech_regulation_fact = cost_tech_regulation ?? 0;
				tz_upd.cost_other_support_prod_fact = cost_other_support_prod ?? 0;
				tz_upd.cost_other_service_prod_fact = cost_other_service_prod ?? 0;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostOtherOrg_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					cost_transport_fact = cost_transport ?? 0,
					cost_tech_regulation_fact = cost_tech_regulation ?? 0,
					cost_other_support_prod_fact = cost_other_support_prod ?? 0,
					cost_other_service_prod_fact = cost_other_service_prod ?? 0,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostOtherOrg_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на оплату работ и услуг производственного характера, выполняемых по договорам со сторонними организациями (план)
		[NonAction]
		public async Task<bool> InsertUpdateOtherOrgCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_transport,
			decimal? cost_tech_regulation, decimal? cost_other_support_prod, decimal? cost_other_service_prod)
		{

			var tz_upd = await _context.TZ_CostOtherOrg_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_transport_plan = cost_transport ?? 0;
				tz_upd.cost_tech_regulation_plan = cost_tech_regulation ?? 0;
				tz_upd.cost_other_support_prod_plan = cost_other_support_prod ?? 0;
				tz_upd.cost_other_service_prod_plan = cost_other_service_prod ?? 0;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostOtherOrg_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					cost_transport_plan = cost_transport ?? 0,
					cost_tech_regulation_plan = cost_tech_regulation ?? 0,
					cost_other_support_prod_plan = cost_other_support_prod ?? 0,
					cost_other_service_prod_plan = cost_other_service_prod ?? 0,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostOtherOrg_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на оплату работ и услуг производственного характера, выполняемых по договорам со сторонними организациями (факт)
		[NonAction]
		public async Task<bool> InsertUpdateOrgOtherServiceCostsFact(int tz_id, int data_status, short report_period_id, decimal? cost_connect_service = 0,
			decimal? cost_security_service = 0, decimal? cost_communal_service = 0, decimal? cost_consulting_service = 0, decimal? cost_legal_service = 0, 
			decimal? cost_inform_service = 0, decimal? cost_audit_service = 0, decimal? cost_strategic_manag_service = 0, decimal? cost_prepare_develop_service = 0, 
			decimal? cost_targeted_funds = 0, decimal? cost_additional_insure = 0, decimal? cost_other_work_service = 0, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_CostOrgOtherWorkService_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_connect_service_fact = cost_connect_service;
				tz_upd.cost_security_service_fact = cost_security_service;
				tz_upd.cost_communal_service_fact = cost_communal_service;
				tz_upd.cost_consulting_service_fact = cost_consulting_service;
				tz_upd.cost_legal_service_fact = cost_legal_service;
				tz_upd.cost_inform_service_fact = cost_inform_service;
				tz_upd.cost_audit_service_fact = cost_audit_service;
				tz_upd.cost_strategic_manag_service_fact = cost_strategic_manag_service;
				tz_upd.cost_prepare_develop_service_fact = cost_prepare_develop_service;
				tz_upd.cost_targeted_funds_fact = cost_targeted_funds;
				tz_upd.cost_additional_insure_fact = cost_additional_insure;
				tz_upd.cost_other_work_service_fact = cost_other_work_service;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostOrgOtherWorkService_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					cost_connect_service_fact = cost_connect_service,
					cost_security_service_fact = cost_security_service,
					cost_communal_service_fact = cost_communal_service,
					cost_consulting_service_fact = cost_consulting_service,
					cost_legal_service_fact = cost_legal_service,
					cost_inform_service_fact = cost_inform_service,
					cost_audit_service_fact = cost_audit_service,
					cost_strategic_manag_service_fact = cost_strategic_manag_service,
					cost_prepare_develop_service_fact = cost_prepare_develop_service,
					cost_targeted_funds_fact = cost_targeted_funds,
					cost_additional_insure_fact = cost_additional_insure,
					cost_other_work_service_fact = cost_other_work_service,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostOrgOtherWorkService_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на оплату работ и услуг производственного характера, выполняемых по договорам со сторонними организациями (план)
		[NonAction]
		public async Task<bool> InsertUpdateOrgOtherServiceCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_connect_service = 0,
			decimal? cost_security_service = 0, decimal? cost_communal_service = 0, decimal? cost_consulting_service = 0, decimal? cost_legal_service = 0,
			decimal? cost_inform_service = 0, decimal? cost_audit_service = 0, decimal? cost_strategic_manag_service = 0, decimal? cost_prepare_develop_service = 0,
			decimal? cost_targeted_funds = 0, decimal? cost_additional_insure = 0, decimal? cost_other_work_service = 0)
		{

			var tz_upd = await _context.TZ_CostOrgOtherWorkService_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_connect_service_plan = cost_connect_service;
				tz_upd.cost_security_service_plan = cost_security_service;
				tz_upd.cost_communal_service_plan = cost_communal_service;
				tz_upd.cost_consulting_service_plan = cost_consulting_service;
				tz_upd.cost_legal_service_plan = cost_legal_service;
				tz_upd.cost_inform_service_plan = cost_inform_service;
				tz_upd.cost_audit_service_plan = cost_audit_service;
				tz_upd.cost_strategic_manag_service_plan = cost_strategic_manag_service;
				tz_upd.cost_prepare_develop_service_plan = cost_prepare_develop_service;
				tz_upd.cost_targeted_funds_plan = cost_targeted_funds;
				tz_upd.cost_additional_insure_plan = cost_additional_insure;
				tz_upd.cost_other_work_service_plan = cost_other_work_service;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostOrgOtherWorkService_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					cost_connect_service_plan = cost_connect_service,
					cost_security_service_plan = cost_security_service,
					cost_communal_service_plan = cost_communal_service,
					cost_consulting_service_plan = cost_consulting_service,
					cost_legal_service_plan = cost_legal_service,
					cost_inform_service_plan = cost_inform_service,
					cost_audit_service_plan = cost_audit_service,
					cost_strategic_manag_service_plan = cost_strategic_manag_service,
					cost_prepare_develop_service_plan = cost_prepare_develop_service,
					cost_targeted_funds_plan = cost_targeted_funds,
					cost_additional_insure_plan = cost_additional_insure,
					cost_other_work_service_plan = cost_other_work_service,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostOrgOtherWorkService_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на ремонт основных средств, выполняемый подрядным способом (факт)
		[NonAction]
		public async Task<bool> InsertUpdateRepairFundingCostsFact(int tz_id, int data_status, short report_period_id, decimal? cost_repair = 0, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_CostRepairFunding_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_repair_fact = cost_repair;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostRepairFunding_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					cost_repair_fact = cost_repair,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostRepairFunding_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на ремонт основных средств, выполняемый подрядным способом (план)
		[NonAction]
		public async Task<bool> InsertUpdateRepairFundingCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_repair = 0)
		{

			var tz_upd = await _context.TZ_CostRepairFunding_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_repair_plan = cost_repair;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostRepairFunding_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					cost_repair_plan = cost_repair,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostRepairFunding_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на вывод из эксплуатации (в том числе на консервацию) и вывод из консервации (факт)
		[NonAction]
		public async Task<bool> InsertUpdateDecomissionCostsFact(int tz_id, int data_status, short report_period_id, decimal? cost_decomission = 0, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_CostDecomission_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_decomission_fact = cost_decomission;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostDecomission_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					cost_decomission_fact = cost_decomission,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostDecomission_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по затратам на вывод из эксплуатации (в том числе на консервацию) и вывод из консервации (план)
		[NonAction]
		public async Task<bool> InsertUpdateDecomissionCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_decomission = 0)
		{

			var tz_upd = await _context.TZ_CostDecomission_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_decomission_plan = cost_decomission;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostDecomission_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					cost_decomission_plan = cost_decomission,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostDecomission_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по прочим операционным расходам (факт)
		[NonAction]
		public async Task<bool> InsertUpdateOtherOperatingsCostsFact(int tz_id, int data_status, short report_period_id, decimal? cost_travel = 0, 
			decimal? cost_staff_train = 0, decimal? cost_bank_service = 0, decimal? rent_nonprod_obj_state_prop = 0, decimal? rent_nonprod_obj_municipal_prop = 0,
			decimal? rent_nonprod_obj_other = 0, decimal? leasing_nonprod_obj_without_own_rights = 0, decimal? cost_other_operating_house = 0,
			decimal? cost_other_operating = 0, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_CostOperatingsOther_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_travel_fact = cost_travel;
				tz_upd.cost_staff_train_fact = cost_staff_train;
				tz_upd.cost_bank_service_fact = cost_bank_service;
				tz_upd.rent_nonprod_obj_state_prop_fact = rent_nonprod_obj_state_prop;
				tz_upd.rent_nonprod_obj_municipal_prop_fact = rent_nonprod_obj_municipal_prop;
				tz_upd.rent_nonprod_obj_other_fact = rent_nonprod_obj_other;
				tz_upd.leasing_nonprod_obj_without_own_rights_fact = leasing_nonprod_obj_without_own_rights;
				tz_upd.cost_other_operating_house_fact = cost_other_operating_house;
				tz_upd.cost_other_operating_fact = cost_other_operating;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostOperatingsOther_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					cost_travel_fact = cost_travel,
					cost_staff_train_fact = cost_staff_train,
					cost_bank_service_fact = cost_bank_service,
					rent_nonprod_obj_state_prop_fact = rent_nonprod_obj_state_prop,
					rent_nonprod_obj_municipal_prop_fact = rent_nonprod_obj_municipal_prop,
					rent_nonprod_obj_other_fact = rent_nonprod_obj_other,
					leasing_nonprod_obj_without_own_rights_fact = leasing_nonprod_obj_without_own_rights,
					cost_other_operating_house_fact = cost_other_operating_house,
					cost_other_operating_fact = cost_other_operating,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostOperatingsOther_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по прочим операционным расходам (план)
		[NonAction]
		public async Task<bool> InsertUpdateOtherOperatingsCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_travel = 0,
			decimal? cost_staff_train = 0, decimal? cost_bank_service = 0, decimal? rent_nonprod_obj_state_prop = 0, decimal? rent_nonprod_obj_municipal_prop = 0,
			decimal? rent_nonprod_obj_other = 0, decimal? leasing_nonprod_obj_without_own_rights = 0, decimal? cost_other_operating_house = 0,
			decimal? cost_other_operating = 0)
		{

			var tz_upd = await _context.TZ_CostOperatingsOther_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_travel_plan = cost_travel;
				tz_upd.cost_staff_train_plan = cost_staff_train;
				tz_upd.cost_bank_service_plan = cost_bank_service;
				tz_upd.rent_nonprod_obj_state_prop_plan = rent_nonprod_obj_state_prop;
				tz_upd.rent_nonprod_obj_municipal_prop_plan = rent_nonprod_obj_municipal_prop;
				tz_upd.rent_nonprod_obj_other_plan = rent_nonprod_obj_other;
				tz_upd.leasing_nonprod_obj_without_own_rights_plan = leasing_nonprod_obj_without_own_rights;
				tz_upd.cost_other_operating_house_plan = cost_other_operating_house;
				tz_upd.cost_other_operating_plan = cost_other_operating;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostOperatingsOther_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					perspective_year = perspective_year,
					cost_travel_plan = cost_travel,
					cost_staff_train_plan = cost_staff_train,
					cost_bank_service_plan = cost_bank_service,
					rent_nonprod_obj_state_prop_plan = rent_nonprod_obj_state_prop,
					rent_nonprod_obj_municipal_prop_plan = rent_nonprod_obj_municipal_prop,
					rent_nonprod_obj_other_plan = rent_nonprod_obj_other,
					leasing_nonprod_obj_without_own_rights_plan = leasing_nonprod_obj_without_own_rights,
					cost_other_operating_house_plan = cost_other_operating_house,
					cost_other_operating_plan = cost_other_operating,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostOperatingsOther_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по расходам на уплату налогов, сборов и других обязательных платежей (факт)
		[NonAction]
		public async Task<bool> InsertUpdateTaxesCostsFact(int tz_id, int data_status, short report_period_id, 
			decimal? cost_emissions_pollutant = 0, decimal? cost_required_insure = 0, decimal? cost_tax_org_property = 0, decimal? cost_tax_land = 0,
			decimal? cost_tax_transport = 0, decimal? cost_tax_water = 0, decimal? cost_tax_other = 0, decimal? cost_other = 0, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_CostRequiredTaxes_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_emissions_pollutant_fact = cost_emissions_pollutant;
				tz_upd.cost_required_insure_fact = cost_required_insure;
				tz_upd.cost_tax_org_property_fact = cost_tax_org_property;
				tz_upd.cost_tax_land_fact = cost_tax_land;
				tz_upd.cost_tax_transport_fact = cost_tax_transport;
				tz_upd.cost_tax_water_fact = cost_tax_water;
				tz_upd.cost_tax_other_fact = cost_tax_other;
				tz_upd.cost_other_fact = cost_other;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostRequiredTaxes_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					cost_emissions_pollutant_fact = cost_emissions_pollutant,
					cost_required_insure_fact = cost_required_insure,
					cost_tax_org_property_fact = cost_tax_org_property,
					cost_tax_land_fact = cost_tax_land,
					cost_tax_transport_fact = cost_tax_transport,
					cost_tax_water_fact = cost_tax_water,
					cost_tax_other_fact = cost_tax_other,
					cost_other_fact = cost_other,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostRequiredTaxes_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по расходам на уплату налогов, сборов и других обязательных платежей (план)
		[NonAction]
		public async Task<bool> InsertUpdateTaxesCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, 
			decimal? cost_emissions_pollutant = 0, decimal? cost_required_insure = 0, decimal? cost_tax_org_property = 0, decimal? cost_tax_land = 0,
			decimal? cost_tax_transport = 0, decimal? cost_tax_water = 0, decimal? cost_tax_other = 0, decimal? cost_other = 0)
		{

			var tz_upd = await _context.TZ_CostRequiredTaxes_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_emissions_pollutant_plan = cost_emissions_pollutant;
				tz_upd.cost_required_insure_plan = cost_required_insure;
				tz_upd.cost_tax_org_property_plan = cost_tax_org_property;
				tz_upd.cost_tax_land_plan = cost_tax_land;
				tz_upd.cost_tax_transport_plan = cost_tax_transport;
				tz_upd.cost_tax_water_plan = cost_tax_water;
				tz_upd.cost_tax_other_plan = cost_tax_other;
				tz_upd.cost_other_plan = cost_other;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostRequiredTaxes_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					cost_emissions_pollutant_plan = cost_emissions_pollutant,
					cost_required_insure_plan = cost_required_insure,
					cost_tax_org_property_plan = cost_tax_org_property,
					cost_tax_land_plan = cost_tax_land,
					cost_tax_transport_plan = cost_tax_transport,
					cost_tax_water_plan = cost_tax_water,
					cost_tax_other_plan = cost_tax_other,
					cost_other_plan = cost_other,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostRequiredTaxes_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по амортизационным начислениям (факт)
		[NonAction]
		public async Task<bool> InsertUpdateAmortizationCostsFact(int tz_id, int data_status, short report_period_id,
			decimal? amortization_prod_equip = 0, decimal? amortization_other_means = 0, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_Amortization_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.amortization_prod_equip_fact = amortization_prod_equip;
				tz_upd.amortization_other_means_fact = amortization_other_means;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_Amortization_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					amortization_prod_equip_fact = amortization_prod_equip,
					amortization_other_means_fact = amortization_other_means,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_Amortization_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по амортизационным начислениям (план)
		[NonAction]
		public async Task<bool> InsertUpdateAmortizationCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id,
			decimal? amortization_prod_equip = 0, decimal? amortization_other_means = 0)
		{

			var tz_upd = await _context.TZ_Amortization_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.amortization_prod_equip_plan = amortization_prod_equip;
				tz_upd.amortization_other_means_plan = amortization_other_means;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_Amortization_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					amortization_prod_equip_plan = amortization_prod_equip,
					amortization_other_means_plan = amortization_other_means,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_Amortization_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по расходам на оплату услуг, оказываемых организациями, осуществляющими регулируемые виды деятельности (факт)
		[NonAction]
		public async Task<bool> InsertUpdateRegOrgServiceCostsFact(int tz_id, int data_status, short report_period_id, decimal? cost_reg_org_service = 0, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_CostRegOrgService_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_reg_org_service_fact = cost_reg_org_service;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostRegOrgService_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					cost_reg_org_service_fact = cost_reg_org_service,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostRegOrgService_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по расходам на оплату услуг, оказываемых организациями, осуществляющими регулируемые виды деятельности (план)
		[NonAction]
		public async Task<bool> InsertUpdateRegOrgServiceCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_reg_org_service = 0)
		{

			var tz_upd = await _context.TZ_CostRegOrgService_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_reg_org_service_plan = cost_reg_org_service;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostRegOrgService_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					cost_reg_org_service_plan = cost_reg_org_service,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostRegOrgService_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по налогу на прибыль (факт)
		[NonAction]
		public async Task<bool> InsertUpdateIncomeTaxCostsFact(int tz_id, int data_status, short report_period_id, decimal? cost_income_tax = 0, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_CostIncomeTax_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_income_tax_fact = cost_income_tax;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostIncomeTax_Fact()
				{
					tz_id = tz_id,
					data_status = data_status,
					report_period_id = report_period_id,
					cost_income_tax_fact = cost_income_tax,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostIncomeTax_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по расходам на оплату услуг, оказываемых организациями, осуществляющими регулируемые виды деятельности (план)
		[NonAction]
		public async Task<bool> InsertUpdateIncomeTaxCostsPlan(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? cost_income_tax = 0)
		{

			var tz_upd = await _context.TZ_CostIncomeTax_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.cost_income_tax_plan = cost_income_tax;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostIncomeTax_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					cost_income_tax_plan = cost_income_tax,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostIncomeTax_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по прочим неподконтрольным расходам (факт)
		[NonAction]
		public async Task<bool> InsertUpdateUncontrolOtherCostsFact(TZUncontrolOtherCostsViewModel model, short report_period_id, string type, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_CostUncontrolOther_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal cost_concession = GetDecimalValue(model, "cost_concession" + type, report_period_id);
			decimal cost_doubtful_debt = GetDecimalValue(model, "cost_doubtful_debt" + type, report_period_id);
			decimal cost_credit_contracts = GetDecimalValue(model, "cost_credit_contracts" + type, report_period_id);
			decimal rent_prod_obj_state_prop = GetDecimalValue(model, "rent_prod_obj_state_prop" + type, report_period_id);
			decimal rent_prod_obj_municipal_prop = GetDecimalValue(model, "rent_prod_obj_municipal_prop" + type, report_period_id);
			decimal rent_prod_obj_other = GetDecimalValue(model, "rent_prod_obj_other" + type, report_period_id);
			decimal leasing_prod_obj = GetDecimalValue(model, "leasing_prod_obj" + type, report_period_id);
			decimal leasing_nonprod_obj_with_own_rights = GetDecimalValue(model, "leasing_nonprod_obj_with_own_rights" + type, report_period_id);
			decimal cost_uncontrol_other = GetDecimalValue(model, "cost_uncontrol_other" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.cost_concession_fact = cost_concession;
				tz_upd.cost_doubtful_debt_fact = cost_doubtful_debt;
				tz_upd.cost_credit_contracts_fact = cost_credit_contracts;
				tz_upd.rent_prod_obj_state_prop_fact = rent_prod_obj_state_prop;
				tz_upd.rent_prod_obj_municipal_prop_fact = rent_prod_obj_municipal_prop;
				tz_upd.rent_prod_obj_other_fact = rent_prod_obj_other;
				tz_upd.leasing_prod_obj_fact = leasing_prod_obj;
				tz_upd.leasing_nonprod_obj_with_own_rights_fact = leasing_nonprod_obj_with_own_rights;
				tz_upd.cost_uncontrol_other_fact = cost_uncontrol_other;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostUncontrolOther_Fact()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					report_period_id = report_period_id,
					cost_concession_fact = cost_concession,
					cost_doubtful_debt_fact = cost_doubtful_debt,
					cost_credit_contracts_fact = cost_credit_contracts,
					rent_prod_obj_state_prop_fact = rent_prod_obj_state_prop,
					rent_prod_obj_municipal_prop_fact = rent_prod_obj_municipal_prop,
					rent_prod_obj_other_fact = rent_prod_obj_other,
					leasing_prod_obj_fact = leasing_prod_obj,
					leasing_nonprod_obj_with_own_rights_fact = leasing_nonprod_obj_with_own_rights,
					cost_uncontrol_other_fact = cost_uncontrol_other,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostUncontrolOther_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по прочим неподконтрольным расходам (план)
		[NonAction]
		public async Task<bool> InsertUpdateUncontrolOtherCostsPlan(TZUncontrolOtherCostsViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_CostUncontrolOther_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status && x.perspective_year == model.perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal cost_concession = GetDecimalValue(model, "cost_concession" + type, report_period_id);
			decimal cost_doubtful_debt = GetDecimalValue(model, "cost_doubtful_debt" + type, report_period_id);
			decimal cost_credit_contracts = GetDecimalValue(model, "cost_credit_contracts" + type, report_period_id);
			decimal rent_prod_obj_state_prop = GetDecimalValue(model, "rent_prod_obj_state_prop" + type, report_period_id);
			decimal rent_prod_obj_municipal_prop = GetDecimalValue(model, "rent_prod_obj_municipal_prop" + type, report_period_id);
			decimal rent_prod_obj_other = GetDecimalValue(model, "rent_prod_obj_other" + type, report_period_id);
			decimal leasing_prod_obj = GetDecimalValue(model, "leasing_prod_obj" + type, report_period_id);
			decimal leasing_nonprod_obj_with_own_rights = GetDecimalValue(model, "leasing_nonprod_obj_with_own_rights" + type, report_period_id);
			decimal cost_uncontrol_other = GetDecimalValue(model, "cost_uncontrol_other" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.cost_concession_plan = cost_concession;
				tz_upd.cost_doubtful_debt_plan = cost_doubtful_debt;
				tz_upd.cost_credit_contracts_plan = cost_credit_contracts;
				tz_upd.rent_prod_obj_state_prop_plan = rent_prod_obj_state_prop;
				tz_upd.rent_prod_obj_municipal_prop_plan = rent_prod_obj_municipal_prop;
				tz_upd.rent_prod_obj_other_plan = rent_prod_obj_other;
				tz_upd.leasing_prod_obj_plan = leasing_prod_obj;
				tz_upd.leasing_nonprod_obj_with_own_rights_plan = leasing_nonprod_obj_with_own_rights;
				tz_upd.cost_uncontrol_other_plan = cost_uncontrol_other;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostUncontrolOther_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					cost_concession_plan = cost_concession,
					cost_doubtful_debt_plan = cost_doubtful_debt,
					cost_credit_contracts_plan = cost_credit_contracts,
					rent_prod_obj_state_prop_plan = rent_prod_obj_state_prop,
					rent_prod_obj_municipal_prop_plan = rent_prod_obj_municipal_prop,
					rent_prod_obj_other_plan = rent_prod_obj_other,
					leasing_prod_obj_plan = leasing_prod_obj,
					leasing_nonprod_obj_with_own_rights_plan = leasing_nonprod_obj_with_own_rights,
					cost_uncontrol_other_plan = cost_uncontrol_other,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostUncontrolOther_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//расчёт годовых показателей по расходам на топливо (факт)
		[NonAction]
		public async Task<bool> InsertUpdateFuelTechNeedsFact(int tz_id, int data_status, short fuel_type_id)
		{

			var tz_f1 = await _context.TZ_FuelTechNeeds_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.fuel_type_id == fuel_type_id && x.report_period_id == 1).FirstOrDefaultAsync();
			var tz_f2 = await _context.TZ_FuelTechNeeds_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.fuel_type_id == fuel_type_id && x.report_period_id == 2).FirstOrDefaultAsync();

			var tz_upd = await _context.TZ_FuelTechNeeds_Fact.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.fuel_type_id == fuel_type_id && x.report_period_id == 3).FirstOrDefaultAsync();

			if (tz_f1 == null && tz_f2 == null && tz_upd != null)
			{
				_context.TZ_FuelTechNeeds_Fact.Remove(tz_upd);
			}
			else
			{
				var consumption_natural_fuel_1 = (tz_f1 != null ? (tz_f1.consumption_natural_fuel_fact ?? 0) : 0);
				var consumption_natural_fuel_2 = (tz_f2 != null ? (tz_f2.consumption_natural_fuel_fact ?? 0) : 0);
				var lowestheat_combust_fuel_1 = (tz_f1 != null ? (tz_f1.lowestheat_combust_fuel_fact ?? 0) : 0);
				var lowestheat_combust_fuel_2 = (tz_f2 != null ? (tz_f2.lowestheat_combust_fuel_fact ?? 0) : 0);
				var price_fuel_1 = (tz_f1 != null ? (tz_f1.price_fuel_fact ?? 0) : 0);
				var price_fuel_2 = (tz_f2 != null ? (tz_f2.price_fuel_fact ?? 0) : 0);

				var consumption_natural_fuel_3 = consumption_natural_fuel_1 + consumption_natural_fuel_2;

				decimal lowestheat_combust_fuel_3 = 0; decimal price_fuel_3 = 0;
				if (consumption_natural_fuel_3 > 0)
				{
					lowestheat_combust_fuel_3 = (consumption_natural_fuel_1 * lowestheat_combust_fuel_1 + consumption_natural_fuel_2 * lowestheat_combust_fuel_2)
						/ consumption_natural_fuel_3;

					price_fuel_3 = (consumption_natural_fuel_1 * price_fuel_1 + consumption_natural_fuel_2 * price_fuel_2) / consumption_natural_fuel_3;
				}

				if (tz_upd != null)
				{
					tz_upd.consumption_natural_fuel_fact = consumption_natural_fuel_3;
					tz_upd.lowestheat_combust_fuel_fact = lowestheat_combust_fuel_3;
					tz_upd.price_fuel_fact = price_fuel_3;
					tz_upd.edit_date = DateTime.Now;
					tz_upd.user_id = userId;
				}
				else
				{
					var new_tz = new TZ_FuelTechNeeds_Fact()
					{
						tz_id = tz_id,
						data_status = data_status,
						fuel_type_id = fuel_type_id,
						report_period_id = 3,
						consumption_natural_fuel_fact = consumption_natural_fuel_3,
						lowestheat_combust_fuel_fact = lowestheat_combust_fuel_3,
						price_fuel_fact = price_fuel_3,
						is_deleted = false,
						create_date = DateTime.Now,
						user_id = userId
					};
					await _context.TZ_FuelTechNeeds_Fact.AddAsync(new_tz);
				}
				
			}
			return true;
		}

		//расчёт годовых показателей по расходам на топливо (план)
		[NonAction]
		public async Task<bool> InsertUpdateFuelTechNeedsPlan(int tz_id, int data_status, int perspective_year, short fuel_type_id)
		{

			var tz_f1 = await _context.TZ_FuelTechNeeds_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.fuel_type_id == fuel_type_id 
				&& x.perspective_year == perspective_year && x.report_period_id == 1).FirstOrDefaultAsync();
			var tz_f2 = await _context.TZ_FuelTechNeeds_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.fuel_type_id == fuel_type_id
				&& x.perspective_year == perspective_year && x.report_period_id == 2).FirstOrDefaultAsync();

			var tz_upd = await _context.TZ_FuelTechNeeds_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.fuel_type_id == fuel_type_id
				&& x.perspective_year == perspective_year && x.report_period_id == 3).FirstOrDefaultAsync();

			if (tz_f1 == null && tz_f2 == null && tz_upd != null)
			{
				_context.TZ_FuelTechNeeds_Plan.Remove(tz_upd);
			}
			else
			{
				var consumption_natural_fuel_1 = (tz_f1 != null ? (tz_f1.consumption_natural_fuel_plan ?? 0) : 0);
				var consumption_natural_fuel_2 = (tz_f2 != null ? (tz_f2.consumption_natural_fuel_plan ?? 0) : 0);
				var lowestheat_combust_fuel_1 = (tz_f1 != null ? (tz_f1.lowestheat_combust_fuel_plan ?? 0) : 0);
				var lowestheat_combust_fuel_2 = (tz_f2 != null ? (tz_f2.lowestheat_combust_fuel_plan ?? 0) : 0);
				var price_fuel_1 = (tz_f1 != null ? (tz_f1.price_fuel_plan ?? 0) : 0);
				var price_fuel_2 = (tz_f2 != null ? (tz_f2.price_fuel_plan ?? 0) : 0);
				var norm_consump_fuel_1 = (tz_f1 != null ? (tz_f1.norm_consump_fuel_plan ?? 0) : 0);
				var norm_consump_fuel_2 = (tz_f2 != null ? (tz_f2.norm_consump_fuel_plan ?? 0) : 0);

				var consumption_natural_fuel_3 = consumption_natural_fuel_1 + consumption_natural_fuel_2;

				var norm_consump_fuel_3 = (consumption_natural_fuel_1 * lowestheat_combust_fuel_1 > 0 || consumption_natural_fuel_2 * lowestheat_combust_fuel_2 > 0) ?
					((norm_consump_fuel_1 * consumption_natural_fuel_1 * lowestheat_combust_fuel_1 / 7000) +
					(norm_consump_fuel_2 * consumption_natural_fuel_2 * lowestheat_combust_fuel_2 / 7000))
					/ ((consumption_natural_fuel_1 * lowestheat_combust_fuel_1 / 7000) + (consumption_natural_fuel_2 * lowestheat_combust_fuel_2 / 7000)) : 0;

				decimal lowestheat_combust_fuel_3 = 0; decimal price_fuel_3 = 0;
				if (consumption_natural_fuel_3 > 0)
				{
					lowestheat_combust_fuel_3 = (consumption_natural_fuel_1 * lowestheat_combust_fuel_1 + consumption_natural_fuel_2 * lowestheat_combust_fuel_2)
						/ consumption_natural_fuel_3;

					price_fuel_3 = (consumption_natural_fuel_1 * price_fuel_1 + consumption_natural_fuel_2 * price_fuel_2) / consumption_natural_fuel_3;
				}

				if (tz_upd != null)
				{
					tz_upd.consumption_natural_fuel_plan = consumption_natural_fuel_3;
					tz_upd.lowestheat_combust_fuel_plan = lowestheat_combust_fuel_3;
					tz_upd.price_fuel_plan = price_fuel_3;
					tz_upd.norm_consump_fuel_plan = norm_consump_fuel_3;
					tz_upd.edit_date = DateTime.Now;
					tz_upd.user_id = userId;
				}
				else
				{
					var new_tz = new TZ_FuelTechNeeds_Plan()
					{
						tz_id = tz_id,
						data_status = data_status,
						perspective_year = perspective_year,
						fuel_type_id = fuel_type_id,
						report_period_id = 3,
						consumption_natural_fuel_plan = consumption_natural_fuel_3,
						lowestheat_combust_fuel_plan = lowestheat_combust_fuel_3,
						norm_consump_fuel_plan = norm_consump_fuel_3,
						price_fuel_plan = price_fuel_3,
						create_date = DateTime.Now,
						user_id = userId
					};
					await _context.TZ_FuelTechNeeds_Plan.AddAsync(new_tz);
				}
			}
			return true;
		}

		//вставка и обновление данных по нормативному удельному расходу топлива на отпуск тепла (только план)
		[NonAction]
		public async Task<bool> InsertUpdateFuelNormConsumptionPlan(int tz_id, int data_status, int perspective_year, short report_period_id, decimal? norm_consump_fuel = 0)
		{

			var tz_upd = await _context.TZ_FuelNormConsump_Plan.Where(x => x.tz_id == tz_id && x.data_status == data_status && x.perspective_year == perspective_year
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			if (tz_upd != null)
			{
				tz_upd.norm_consump_fuel_plan = norm_consump_fuel;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_FuelNormConsump_Plan()
				{
					tz_id = tz_id,
					data_status = data_status,
					perspective_year = perspective_year,
					report_period_id = report_period_id,
					norm_consump_fuel_plan = norm_consump_fuel,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_FuelNormConsump_Plan.AddAsync(new_tz);
			}
			return true;
		}

		#endregion


		#region Update_Insert_Data_НВВ
		//вставка и обновление данных по прибыли (факт)
		[NonAction]
		public async Task<bool> InsertUpdateProfitNVVFact(TZProfitNVVViewModel model, short report_period_id, string type, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_Profit_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal profit_prod_develop = GetDecimalValue(model, "profit_prod_develop" + type, report_period_id);
			decimal profit_social_develop = GetDecimalValue(model, "profit_social_develop" + type, report_period_id);
			decimal profit_stimul = GetDecimalValue(model, "profit_stimul" + type, report_period_id);
			decimal profit_other_needs = GetDecimalValue(model, "profit_other_needs" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.profit_prod_develop = profit_prod_develop;
				tz_upd.profit_social_develop = profit_social_develop;
				tz_upd.profit_stimul = profit_stimul;
				tz_upd.profit_other_needs = profit_other_needs;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_Profit_Fact()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					report_period_id = report_period_id,
					profit_prod_develop = profit_prod_develop,
					profit_social_develop = profit_social_develop,
					profit_stimul = profit_stimul,
					profit_other_needs = profit_other_needs,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_Profit_Fact.AddAsync(new_tz);
			}
			return true;
		}
		//вставка и обновление данных по прибыли (план)
		[NonAction]
		public async Task<bool> InsertUpdateProfitNVVPlan(TZProfitNVVViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_Profit_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status && x.perspective_year == model.perspective_year
						&& x.perspective_year == model.perspective_year && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal calc_enterprising_profit = GetDecimalValue(model, "calc_enterprising_profit" + type, report_period_id);
			decimal funds_repayment_credit = GetDecimalValue(model, "funds_repayment_credit" + type, report_period_id);
			decimal cost_capital_invest = GetDecimalValue(model, "cost_capital_invest" + type, report_period_id);
			decimal return_rate = GetDecimalValue(model, "return_rate" + type, report_period_id);
			decimal cost_economic_pay = GetDecimalValue(model, "cost_economic_pay" + type, report_period_id);
			decimal regular_lvl_profit = GetDecimalValue(model, "regular_lvl_profit" + type, report_period_id);
			decimal regular_lvl_profit_percent = GetDecimalValue(model, "regular_lvl_profit_percent" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.calc_enterprising_profit = calc_enterprising_profit;
				tz_upd.funds_repayment_credit = funds_repayment_credit;
				tz_upd.cost_capital_invest = cost_capital_invest;
				tz_upd.return_rate = return_rate;
				tz_upd.cost_economic_pay = cost_economic_pay;
				tz_upd.regular_lvl_profit = regular_lvl_profit;
				tz_upd.regular_lvl_profit_percent = regular_lvl_profit_percent;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_Profit_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					calc_enterprising_profit = calc_enterprising_profit,
					funds_repayment_credit = funds_repayment_credit,
					cost_capital_invest = cost_capital_invest,
					return_rate = return_rate,
					cost_economic_pay = cost_economic_pay,
					regular_lvl_profit = regular_lvl_profit,
					regular_lvl_profit_percent = regular_lvl_profit_percent,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_Profit_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по недополученным доходам/выпадающим расходам (план)
		[NonAction]
		public async Task<bool> InsertUpdateLostProfitOutCostsPlan(TZLostProfitOutCostsViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_LostRevenue_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal lost_revenue_pretrial_dispute = GetDecimalValue(model, "lost_revenue_pretrial_dispute" + type, report_period_id);
			decimal lost_revenue_disagree = GetDecimalValue(model, "lost_revenue_disagree" + type, report_period_id);
			decimal cost_notaccounted_reg_body = GetDecimalValue(model, "cost_notaccounted_reg_body" + type, report_period_id);
			decimal cost_grow_price_product = GetDecimalValue(model, "cost_grow_price_product" + type, report_period_id);
			decimal cost_service_borrowed_funds = GetDecimalValue(model, "cost_service_borrowed_funds" + type, report_period_id);
			decimal lost_revenue_from_connect_obj = GetDecimalValue(model, "lost_revenue_from_connect_obj" + type, report_period_id);
			decimal lost_revenue_other = GetDecimalValue(model, "lost_revenue_other" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.lost_revenue_pretrial_dispute = lost_revenue_pretrial_dispute;
				tz_upd.lost_revenue_disagree = lost_revenue_disagree;
				tz_upd.cost_notaccounted_reg_body = cost_notaccounted_reg_body;
				tz_upd.cost_grow_price_product = cost_grow_price_product;
				tz_upd.cost_service_borrowed_funds = cost_service_borrowed_funds;
				tz_upd.lost_revenue_from_connect_obj = lost_revenue_from_connect_obj;
				tz_upd.lost_revenue_other = lost_revenue_other;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_LostRevenue_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					lost_revenue_pretrial_dispute = lost_revenue_pretrial_dispute,
					lost_revenue_disagree = lost_revenue_disagree,
					cost_notaccounted_reg_body = cost_notaccounted_reg_body,
					cost_grow_price_product = cost_grow_price_product,
					cost_service_borrowed_funds = cost_service_borrowed_funds,
					lost_revenue_from_connect_obj = lost_revenue_from_connect_obj,
					lost_revenue_other = lost_revenue_other,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_LostRevenue_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по избытку средств (план)
		[NonAction]
		public async Task<bool> InsertUpdateExcessFundsPlan(TZExcessFundsViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_ExcessFunds_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal excess_funds = GetDecimalValue(model, "excess_funds" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.excess_funds = excess_funds;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_ExcessFunds_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					excess_funds = excess_funds,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_ExcessFunds_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по объему и финансированию капитальных вложений (план)
		[NonAction]
		public async Task<bool> InsertUpdateCapitalInvestmentPlan(TZCapitalInvestmentViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_CapitalInvestment_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal volume_invest_prod_electroenergy = GetDecimalValue(model, "volume_invest_prod_electroenergy" + type, report_period_id);
			decimal volume_invest_prod_heatenergy = GetDecimalValue(model, "volume_invest_prod_heatenergy" + type, report_period_id);
			decimal volume_invest_prod_heatcarrier = GetDecimalValue(model, "volume_invest_prod_heatcarrier" + type, report_period_id);
			decimal volume_invest_transfer_heatenergy = GetDecimalValue(model, "volume_invest_transfer_heatenergy" + type, report_period_id);
			decimal volume_invest_other = GetDecimalValue(model, "volume_invest_other" + type, report_period_id);
			decimal amortisation_deduction_fonds = GetDecimalValue(model, "amortisation_deduction_fonds" + type, report_period_id);
			decimal amortisation_deduction_nonmaterial_active = GetDecimalValue(model, "amortisation_deduction_nonmaterial_active" + type, report_period_id);
			decimal finance_by_nonused_funds = GetDecimalValue(model, "finance_by_nonused_funds" + type, report_period_id);
			decimal finance_by_profit = GetDecimalValue(model, "finance_by_profit" + type, report_period_id);
			decimal finance_by_federal_budjet = GetDecimalValue(model, "finance_by_federal_budjet" + type, report_period_id);
			decimal finance_by_local_budjet = GetDecimalValue(model, "finance_by_local_budjet" + type, report_period_id);
			decimal finance_by_regional_budjet = GetDecimalValue(model, "finance_by_regional_budjet" + type, report_period_id);
			decimal finance_by_other_sources = GetDecimalValue(model, "finance_by_other_sources" + type, report_period_id);
			decimal finance_by_credits = GetDecimalValue(model, "finance_by_credits" + type, report_period_id);
			decimal finance_by_realize_securities = GetDecimalValue(model, "finance_by_realize_securities" + type, report_period_id);
			decimal finance_by_connection_charge = GetDecimalValue(model, "finance_by_connection_charge" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.volume_invest_prod_electroenergy = volume_invest_prod_electroenergy;
				tz_upd.volume_invest_prod_heatenergy = volume_invest_prod_heatenergy;
				tz_upd.volume_invest_prod_heatcarrier = volume_invest_prod_heatcarrier;
				tz_upd.volume_invest_transfer_heatenergy = volume_invest_transfer_heatenergy;
				tz_upd.volume_invest_other = volume_invest_other;
				tz_upd.amortisation_deduction_fonds = amortisation_deduction_fonds;
				tz_upd.amortisation_deduction_nonmaterial_active = amortisation_deduction_nonmaterial_active;
				tz_upd.finance_by_nonused_funds = finance_by_nonused_funds;
				tz_upd.finance_by_profit = finance_by_profit;
				tz_upd.finance_by_federal_budjet = finance_by_federal_budjet;
				tz_upd.finance_by_local_budjet = finance_by_local_budjet;
				tz_upd.finance_by_regional_budjet = finance_by_regional_budjet;
				tz_upd.finance_by_other_sources = finance_by_other_sources;
				tz_upd.finance_by_credits = finance_by_credits;
				tz_upd.finance_by_realize_securities = finance_by_realize_securities;
				tz_upd.finance_by_connection_charge = finance_by_connection_charge;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CapitalInvestment_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					volume_invest_prod_electroenergy = volume_invest_prod_electroenergy,
					volume_invest_prod_heatenergy = volume_invest_prod_heatenergy,
					volume_invest_prod_heatcarrier = volume_invest_prod_heatcarrier,
					volume_invest_transfer_heatenergy = volume_invest_transfer_heatenergy,
					volume_invest_other = volume_invest_other,
					amortisation_deduction_fonds = amortisation_deduction_fonds,
					amortisation_deduction_nonmaterial_active = amortisation_deduction_nonmaterial_active,
					finance_by_nonused_funds = finance_by_nonused_funds,
					finance_by_profit = finance_by_profit,
					finance_by_federal_budjet = finance_by_federal_budjet,
					finance_by_local_budjet = finance_by_local_budjet,
					finance_by_regional_budjet = finance_by_regional_budjet,
					finance_by_other_sources = finance_by_other_sources,
					finance_by_credits = finance_by_credits,
					finance_by_realize_securities = finance_by_realize_securities,
					finance_by_connection_charge = finance_by_connection_charge,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CapitalInvestment_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по перекрёстному субсидированию (план)
		[NonAction]
		public async Task<bool> InsertUpdateCrossSubsidyPlan(TZCrossSubsidyViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_CrossSubsidy_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal cross_subs_between_activ = GetDecimalValue(model, "cross_subs_between_activ" + type, report_period_id);
			decimal cross_subs_between_consumer = GetDecimalValue(model, "cross_subs_between_consumer" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.cross_subs_between_activ = cross_subs_between_activ;
				tz_upd.cross_subs_between_consumer = cross_subs_between_consumer;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CrossSubsidy_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					cross_subs_between_activ = cross_subs_between_activ,
					cross_subs_between_consumer = cross_subs_between_consumer,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CrossSubsidy_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по экономии операционных и неподконтрольных расходов (план)
		[NonAction]
		public async Task<bool> InsertUpdateEconomyCostsPlan(TZEconomyCostsViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_EconomyCosts_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal economy_operation_cost = GetDecimalValue(model, "economy_operation_cost" + type, report_period_id);
			decimal total_economy_noncontrol_cost = GetDecimalValue(model, "total_economy_noncontrol_cost" + type, report_period_id);
			decimal economy_consump_heatenergy = GetDecimalValue(model, "economy_consump_heatenergy" + type, report_period_id);
			decimal economy_consump_electroenergy = GetDecimalValue(model, "economy_consump_electroenergy" + type, report_period_id);
			decimal economy_consump_fuel = GetDecimalValue(model, "economy_consump_fuel" + type, report_period_id);
			decimal economy_consump_coldwater = GetDecimalValue(model, "economy_consump_coldwater" + type, report_period_id);
			decimal economy_consump_waterdisp = GetDecimalValue(model, "economy_consump_waterdisp" + type, report_period_id);
			decimal economy_consump_heat = GetDecimalValue(model, "economy_consump_heat" + type, report_period_id);
			decimal economy_consump_hotwater = GetDecimalValue(model, "economy_consump_hotwater" + type, report_period_id);
			decimal increase_economy_operation_cost = GetDecimalValue(model, "increase_economy_operation_cost" + type, report_period_id);
			decimal increase_economy_consump_heatenergy = GetDecimalValue(model, "increase_economy_consump_heatenergy" + type, report_period_id);
			decimal increase_economy_consump_electroenergy = GetDecimalValue(model, "increase_economy_consump_electroenergy" + type, report_period_id);
			decimal increase_economy_consump_fuel = GetDecimalValue(model, "increase_economy_consump_fuel" + type, report_period_id);
			decimal increase_economy_consump_coldwater = GetDecimalValue(model, "increase_economy_consump_coldwater" + type, report_period_id);
			decimal increase_economy_consump_waterdisp = GetDecimalValue(model, "increase_economy_consump_waterdisp" + type, report_period_id);
			decimal increase_economy_consump_heat = GetDecimalValue(model, "increase_economy_consump_heat" + type, report_period_id);
			decimal increase_economy_consump_hotwater = GetDecimalValue(model, "increase_economy_consump_hotwater" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.economy_operation_cost = economy_operation_cost;
				tz_upd.total_economy_noncontrol_cost = total_economy_noncontrol_cost;
				tz_upd.economy_consump_heatenergy = economy_consump_heatenergy;
				tz_upd.economy_consump_electroenergy = economy_consump_electroenergy;
				tz_upd.economy_consump_fuel = economy_consump_fuel;
				tz_upd.economy_consump_coldwater = economy_consump_coldwater;
				tz_upd.economy_consump_waterdisp = economy_consump_waterdisp;
				tz_upd.economy_consump_heat = economy_consump_heat;
				tz_upd.economy_consump_hotwater = economy_consump_hotwater;
				tz_upd.increase_economy_operation_cost = increase_economy_operation_cost;
				tz_upd.increase_economy_consump_heatenergy = increase_economy_consump_heatenergy;
				tz_upd.increase_economy_consump_electroenergy = increase_economy_consump_electroenergy;
				tz_upd.increase_economy_consump_fuel = increase_economy_consump_fuel;
				tz_upd.increase_economy_consump_coldwater = increase_economy_consump_coldwater;
				tz_upd.increase_economy_consump_waterdisp = increase_economy_consump_waterdisp;
				tz_upd.increase_economy_consump_heat = increase_economy_consump_heat;
				tz_upd.increase_economy_consump_hotwater = increase_economy_consump_hotwater;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_EconomyCosts_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					economy_operation_cost = economy_operation_cost,
					total_economy_noncontrol_cost = total_economy_noncontrol_cost,
					economy_consump_heatenergy = economy_consump_heatenergy,
					economy_consump_electroenergy = economy_consump_electroenergy,
					economy_consump_fuel = economy_consump_fuel,
					economy_consump_coldwater = economy_consump_coldwater,
					economy_consump_waterdisp = economy_consump_waterdisp,
					economy_consump_heat = economy_consump_heat,
					economy_consump_hotwater = economy_consump_hotwater,
					increase_economy_operation_cost = increase_economy_operation_cost,
					increase_economy_consump_heatenergy = increase_economy_consump_heatenergy,
					increase_economy_consump_electroenergy = increase_economy_consump_electroenergy,
					increase_economy_consump_fuel = increase_economy_consump_fuel,
					increase_economy_consump_coldwater = increase_economy_consump_coldwater,
					increase_economy_consump_waterdisp = increase_economy_consump_waterdisp,
					increase_economy_consump_heat = increase_economy_consump_heat,
					increase_economy_consump_hotwater = increase_economy_consump_hotwater,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_EconomyCosts_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по индексам (план)
		[NonAction]
		public async Task<bool> InsertUpdateIndexesNVVPlan(TZIndexesNVVViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_IndexesNVV_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal index_effective_cost = GetDecimalValue(model, "index_effective_cost" + type, report_period_id);
			decimal index_consumers_price = GetDecimalValue(model, "index_consumers_price" + type, report_period_id);
			decimal index_count_actives = GetDecimalValue(model, "index_count_actives" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.index_effective_cost = index_effective_cost;
				tz_upd.index_consumers_price = index_consumers_price;
				tz_upd.index_count_actives = index_count_actives;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_IndexesNVV_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					index_effective_cost = index_effective_cost,
					index_consumers_price = index_consumers_price,
					index_count_actives = index_count_actives,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_IndexesNVV_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по методу индексации (план)
		[NonAction]
		public async Task<bool> InsertUpdateMethodIndexationNVVPlan(TZMethodIndexationViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_MethodIndexation_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal economically_justified_costs = GetDecimalValue(model, "economically_justified_costs" + type, report_period_id);
			decimal profit_regulated_organisation = GetDecimalValue(model, "profit_regulated_organisation" + type, report_period_id);
			decimal economy_consump_resources = GetDecimalValue(model, "economy_consump_resources" + type, report_period_id);
			decimal correct_accounting_deviation = GetDecimalValue(model, "correct_accounting_deviation" + type, report_period_id);
			decimal correct_safety_quality_product = GetDecimalValue(model, "correct_safety_quality_product" + type, report_period_id);
			decimal correct_nvv_change_invest_prog = GetDecimalValue(model, "correct_nvv_change_invest_prog" + type, report_period_id);
			decimal correct_subj_accounting_nvv = GetDecimalValue(model, "correct_subj_accounting_nvv" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.economically_justified_costs = economically_justified_costs;
				tz_upd.profit_regulated_organisation = profit_regulated_organisation;
				tz_upd.economy_consump_resources = economy_consump_resources;
				tz_upd.correct_accounting_deviation = correct_accounting_deviation;
				tz_upd.correct_safety_quality_product = correct_safety_quality_product;
				tz_upd.correct_nvv_change_invest_prog = correct_nvv_change_invest_prog;
				tz_upd.correct_subj_accounting_nvv = correct_subj_accounting_nvv;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_MethodIndexation_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					economically_justified_costs = economically_justified_costs,
					profit_regulated_organisation = profit_regulated_organisation,
					economy_consump_resources = economy_consump_resources,
					correct_accounting_deviation = correct_accounting_deviation,
					correct_safety_quality_product = correct_safety_quality_product,
					correct_nvv_change_invest_prog = correct_nvv_change_invest_prog,
					correct_subj_accounting_nvv = correct_subj_accounting_nvv,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_MethodIndexation_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по методу доходности инвестированного капитала (план)
		[NonAction]
		public async Task<bool> InsertUpdateMethodProfitInvestNVVPlan(TZMethodProfitInvestViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_MethodProfitInvest_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal full_value_invest = GetDecimalValue(model, "full_value_invest" + type, report_period_id);
			decimal return_period_invest = GetDecimalValue(model, "return_period_invest" + type, report_period_id);
			decimal initial_amount_invest = GetDecimalValue(model, "initial_amount_invest" + type, report_period_id);
			decimal profit_initial_amount_invest = GetDecimalValue(model, "profit_initial_amount_invest" + type, report_period_id);
			decimal base_invest = GetDecimalValue(model, "base_invest" + type, report_period_id);
			decimal pure_working_capital = GetDecimalValue(model, "pure_working_capital" + type, report_period_id);
			decimal norm_pure_working_capital = GetDecimalValue(model, "norm_pure_working_capital" + type, report_period_id);
			decimal norm_profit = GetDecimalValue(model, "norm_profit" + type, report_period_id);
			decimal norm_profit_new_capital = GetDecimalValue(model, "norm_profit_new_capital" + type, report_period_id);
			decimal norm_profit_old_capital = GetDecimalValue(model, "norm_profit_old_capital" + type, report_period_id);
			decimal amortization = GetDecimalValue(model, "amortization" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.full_value_invest = full_value_invest;
				tz_upd.return_period_invest = return_period_invest;
				tz_upd.initial_amount_invest = initial_amount_invest;
				tz_upd.profit_initial_amount_invest = profit_initial_amount_invest;
				tz_upd.base_invest = base_invest;
				tz_upd.pure_working_capital = pure_working_capital;
				tz_upd.norm_pure_working_capital = norm_pure_working_capital;
				tz_upd.norm_profit = norm_profit;
				tz_upd.norm_profit_new_capital = norm_profit_new_capital;
				tz_upd.norm_profit_old_capital = norm_profit_old_capital;
				tz_upd.amortization = amortization;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_MethodProfitInvest_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					full_value_invest = full_value_invest,
					return_period_invest = return_period_invest,
					initial_amount_invest = initial_amount_invest,
					profit_initial_amount_invest = profit_initial_amount_invest,
					base_invest = base_invest,
					pure_working_capital = pure_working_capital,
					norm_pure_working_capital = norm_pure_working_capital,
					norm_profit = norm_profit,
					norm_profit_new_capital = norm_profit_new_capital,
					norm_profit_old_capital = norm_profit_old_capital,
					amortization = amortization,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_MethodProfitInvest_Plan.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по методу сравнения аналогов (план)
		[NonAction]
		public async Task<bool> InsertUpdateMethodAnaloguesComparisonNVVPlan(TZMethodAnaloguesComparisonViewModel model, short report_period_id, string type)
		{

			var tz_upd = await _context.TZ_MethodAnaloguesComparison_Plan.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.perspective_year == model.perspective_year && x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal baseline_cost_reg_managed = GetDecimalValue(model, "baseline_cost_reg_managed" + type, report_period_id);
			decimal correct_nvv_accounting_deviation = GetDecimalValue(model, "correct_nvv_accounting_deviation" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.baseline_cost_reg_managed = baseline_cost_reg_managed;
				tz_upd.correct_nvv_accounting_deviation = correct_nvv_accounting_deviation;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_MethodAnaloguesComparison_Plan()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					perspective_year = model.perspective_year,
					report_period_id = report_period_id,
					baseline_cost_reg_managed = baseline_cost_reg_managed,
					correct_nvv_accounting_deviation = correct_nvv_accounting_deviation,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_MethodAnaloguesComparison_Plan.AddAsync(new_tz);
			}
			return true;
		}
		#endregion

		#region Update_Insert_Data_ИП_ТС_и_КС
		//вставка и обновление данных по Фактическим расходам на финансирование в соответствии с ИП ТС и КС (факт)
		[NonAction]
		public async Task<bool> InsertUpdateExpenses_IPHS_CA_Fact(TZExpenses_IPHS_CA_ViewModel model, short report_period_id, short finance_type_id, string type, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_CostFinanceCapInv_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == report_period_id && x.finance_type_id == finance_type_id).FirstOrDefaultAsync();

			decimal profit_own_funds = GetDecimalValue(model, "profit_own_funds" + type, report_period_id);
			decimal amortization_finance_invest = GetDecimalValue(model, "amortization_finance_invest" + type, report_period_id);
			decimal amortization_deduction_on_credit = GetDecimalValue(model, "amortization_deduction_on_credit" + type, report_period_id);
			decimal federal_budget = GetDecimalValue(model, "federal_budget" + type, report_period_id);
			decimal regional_budget = GetDecimalValue(model, "regional_budget" + type, report_period_id);
			decimal local_budget = GetDecimalValue(model, "local_budget" + type, report_period_id);
			decimal credits = GetDecimalValue(model, "attracted_credits" + type, report_period_id);
			decimal loans = GetDecimalValue(model, "attracted_loans" + type, report_period_id);
			decimal other_funds = GetDecimalValue(model, "attracted_other_funds" + type, report_period_id);
			decimal received_funds_securities = GetDecimalValue(model, "attracted_received_funds_securities" + type, report_period_id);
			decimal connection_charge = GetDecimalValue(model, "connection_charge" + type, report_period_id);
			decimal other_means = GetDecimalValue(model, "other_means" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.profit_own_funds = profit_own_funds;
				tz_upd.amortization_finance_invest = amortization_finance_invest;
				tz_upd.amortization_deduction_on_credit = amortization_deduction_on_credit;
				tz_upd.federal_budget = federal_budget;
				tz_upd.regional_budget = regional_budget;
				tz_upd.local_budget = local_budget;
				tz_upd.credits = credits;
				tz_upd.loans = loans;
				tz_upd.other_funds = other_funds;
				tz_upd.received_funds_securities = received_funds_securities;
				tz_upd.connection_charge = connection_charge;
				tz_upd.other_means = other_means;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostFinanceCapInv_Fact()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					report_period_id = report_period_id,
					finance_type_id = finance_type_id,
					profit_own_funds = profit_own_funds,
					amortization_finance_invest = amortization_finance_invest,
					amortization_deduction_on_credit = amortization_deduction_on_credit,
					federal_budget = federal_budget,
					regional_budget = regional_budget,
					local_budget = local_budget,
					credits = credits,
					loans = loans,
					other_funds = other_funds,
					received_funds_securities = received_funds_securities,
					connection_charge = connection_charge,
					other_means = other_means,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostFinanceCapInv_Fact.AddAsync(new_tz);
			}
			return true;
		}

		//вставка и обновление данных по Фактическому объему дотаций и полученным доходам сверх реализации по утвержденным тарифам (факт)
		[NonAction]
		public async Task<bool> InsertUpdateExpensesAddValues_Fact(TZExpensesAddValues_ViewModel model, short report_period_id, string type, bool? is_deleted = false)
		{

			var tz_upd = await _context.TZ_CostFinanceCapInvAddValues_Fact.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status
						&& x.report_period_id == report_period_id).FirstOrDefaultAsync();

			decimal actual_grants_budget = GetDecimalValue(model, "actual_grants_budget" + type, report_period_id);
			decimal actual_over_profit = GetDecimalValue(model, "actual_over_profit" + type, report_period_id);

			if (tz_upd != null)
			{
				tz_upd.actual_grants_budget = actual_grants_budget;
				tz_upd.actual_over_profit = actual_over_profit;
				tz_upd.is_deleted = is_deleted;
				tz_upd.edit_date = DateTime.Now;
				tz_upd.user_id = userId;
			}
			else
			{
				var new_tz = new TZ_CostFinanceCapInvAddValues_Fact()
				{
					tz_id = model.tz_id,
					data_status = model.data_status,
					report_period_id = report_period_id,
					actual_grants_budget = actual_grants_budget,
					actual_over_profit = actual_over_profit,
					is_deleted = is_deleted,
					create_date = DateTime.Now,
					user_id = userId
				};
				await _context.TZ_CostFinanceCapInvAddValues_Fact.AddAsync(new_tz);
			}
			return true;
		}
		#endregion

		#region ViewComponents
		//Вкладка "Энергетические ресурсы"
		public ActionResult OnGetCallTZOne_EnergyCosts_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_EnergyCostsData_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Вкладка "Операционные расходы"
		public ActionResult OnGetCallTZOne_OperationCosts_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_OperationCostsData_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Вкладка "Неподконтрольные расходы"
		public ActionResult OnGetCallTZOne_UncontrolCosts_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_UncontrolCostsData_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Калькуляция затрат (суммы)
		public ActionResult OnGetCallTZ_CalcAllCosts_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_CalcAllCosts_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Стоимость топлива с учётом транспортировки
		public ActionResult OnGetCallTZ_FuelPriceData_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_FuelPriceData_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Топливо - фактические показатели
		public ActionResult OnGetCallTZ_FuelTechNeedFactList_PartialViewComponent(int tz_id, int data_status)
		{
			return ViewComponent("TZ_FuelTechNeedFactList_Partial", new { tz_id, data_status, userId });
		}
		//Топливо - плановые показатели
		public ActionResult OnGetCallTZ_FuelTechNeedPlanList_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_FuelTechNeedPlanList_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Калькуляция затрат (общий список)
		public ActionResult OnGetCallTZ_CalcCostsList_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_CalcCostsList_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Затраты на транспорт
		public ActionResult OnGetCallTZ_CalcCostsOnTransportList_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_CalcCostsOnTransportList_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Затраты на производство
		public ActionResult OnGetCallTZ_CalcCostsOnProductionList_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_CalcCostsOnProductionList_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//НВВ
		public ActionResult OnGetCallTZ_CalcCostsNVVList_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_CalcCostsNVVList_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//ИП ТС и КС
		public ActionResult OnGetCallTZ_CalcCostsIpHsCaList_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_CalcCostsIpHsCaList_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		//Распределение расходов на производство и транспорт
		public ActionResult OnGetCallTZ_CalcCostsDistribution_PartialViewComponent(int tz_id, int data_status, int perspective_year)
		{
			return ViewComponent("TZ_CalcCostsDistribution_Partial", new { tz_id, data_status, perspective_year, userId });
		}
		#endregion
	}
}
