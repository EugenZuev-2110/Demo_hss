using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Security.Policy;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.TSO.Models;
using WebProject.Components;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using WebProject.Models;

namespace WebProject.Areas.HeatPointsAndConsumers.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("HeatPointsAndConsumers")]
	public class HeatPointController : Controller
	{
		private readonly ILogger<HeatPointController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private string? _new_hp_num = string.Empty;
		private string? _new_hp_address = string.Empty;
		private bool _is_new_hp = false;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;
		private TZHeaderTariffDataViewModel _tariffConnectionViewModel;
		//private PrincipalContext _ctx = new PrincipalContext(ContextType.Domain);
		public HeatPointController(ILogger<HeatPointController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult HeatPointMainView()
		{
			return View();
		}

		#region OpenPopups
		public async Task<IActionResult> OpenHeatPointSwitch(int data_status)
		{
			try
			{
				ViewBag.data_status = data_status;
				ViewBag.TZSourcesList = await _context.fnt_GetSourcesByHPList(data_status).ToListAsync();
				ViewBag.PerspectiveYearsList = _m_c.GetPerspectiveYearsList(data_status);
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView();
		}

		public async Task<IActionResult> OpenHeatPointAddRemove(int data_status, int heat_point_id)
		{
			var _hpHeaderAddRemoveDataViewModel = (await _context.HPHeaderAddRemoveDataViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHpHeaderAddRemoveDataOne {data_status},{heat_point_id}")
							.ToListAsync()).FirstOrDefault() ?? new HPHeaderAddRemoveDataViewModel() { data_status = data_status, hp_id = heat_point_id };

			try
			{
				ViewBag.HpNumberAddressList = await _context.fnt_GetUnomTpAddressListByChars("", data_status).ToListAsync();
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(_hpHeaderAddRemoveDataViewModel);
		}

		public async Task<IActionResult> OpenLoadsAndExpensives(int data_status, int heat_point_id)
		{
			var _hpHeaderAddRemoveDataViewModel = (await _context.HPHeaderAddRemoveDataViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHpHeaderAddRemoveDataOne {data_status},{heat_point_id}")
							.ToListAsync()).FirstOrDefault() ?? new HPHeaderAddRemoveDataViewModel() { data_status = data_status, hp_id = heat_point_id };

			try
			{
				ViewBag.HpNumberAddressList = await _context.fnt_GetUnomTpAddressListByChars("", data_status).ToListAsync();
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}
			return PartialView(_hpHeaderAddRemoveDataViewModel);
		}

		#endregion

		#region ViewComponents

		public ActionResult OnGetHeatPointList_PartialViewComponent(int data_status, int perspective_year, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
		{
			return ViewComponent("HeatPointList_Partial", new { data_status, perspective_year, hp_type_id, hp_status_id, source_id, tso_id });
		}

		//Состав оборудования. Теплообменное оборудование
		public ActionResult OnGetHP_EquipmentPart_HeatExchange_ViewComponent(int data_status, int heat_point_id)
		{
			return ViewComponent("HP_EquipmentPart_HeatExchange_Partial", new { data_status, heat_point_id });
		}

		//Состав оборудования. Насосное оборудование
		public ActionResult OnGetHP_EquipmentPart_Pump_ViewComponent(int data_status, int heat_point_id)
		{
			return ViewComponent("HP_EquipmentPart_Pump_Partial", new { data_status, heat_point_id });
		}

		//Учет ресурсов
		public ActionResult OnGetHP_EquipmentPart_HP_AccResources_ViewComponent(int data_status, int heat_point_id)
		{
			return ViewComponent("HP_EquipmentPart_AccResources_Partial", new { data_status, heat_point_id });
		}

		//Документация и фотоматериалы по ТП
		public ActionResult OnGetHP_HP_DocsFootage_ViewComponent(int data_status, int heat_point_id)
		{
			return ViewComponent("HP_DocsFootage_Partial", new { data_status, heat_point_id });
		}

		//Нагрузки и расходы
		public ActionResult OnGet_LoadExpensive_ViewComponent(int data_status, int[] perspective_years, int load_type = 2, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
		{
			return ViewComponent("LoadExpensiveList_Partial", new { data_status, perspective_years, hp_type_id, load_type, hp_status_id, source_id, tso_id });
		}

		//Оборудование
		public ActionResult OnGet_Equipment_ViewComponent(int data_status, int perspective_year, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
		{
			return ViewComponent("HeatPointsEquipment_Partial", new { data_status, perspective_year, hp_type_id, hp_status_id, source_id, tso_id });
		}

		//Схема присоединения нагрузок
		public ActionResult OnGet_LoadAttachmentSchemas_ViewComponent(int data_status, int perspective_year, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
		{
			return ViewComponent("HeatPointsLoadAttachmentSchemas_Partial", new { data_status, perspective_year, hp_type_id, hp_status_id, source_id, tso_id });
		}

		//Автоматизация
		public ActionResult OnGet_Automatization_ViewComponent(int data_status, int perspective_year, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
		{
			return ViewComponent("HeatPointsAutomatization_Partial", new { data_status, perspective_year, hp_type_id, hp_status_id, source_id, tso_id });
		}

		//Учёт энергетических ресурсов
		public ActionResult OnGet_EnergyResourceAccounting_ViewComponent(int data_status, int perspective_year, int hp_type_id = -1, int hp_status_id = -1, int source_id = -1, int tso_id = -1)
		{
			return ViewComponent("HeatPointsEnergyResourceAccounting_Partial", new { data_status, perspective_year, hp_type_id, hp_status_id, source_id, tso_id });
		}

		#endregion

		public async Task<IActionResult> ShowHeatPointSwitch(string heat_point_id_list, int data_status, int perspective_year_before, int perspective_year_after)
		{
			List<HP_SwitchableUnit> list;

			try
			{
				list = await _context.HP_SwitchableUnit.FromSqlInterpolated
					($"exec sources.sp_GetListSwitchHPTable {heat_point_id_list},{data_status}, {perspective_year_before},{perspective_year_after}").ToListAsync();
			}
			catch (Exception ex)
			{
				list = new List<HP_SwitchableUnit>();
				//_m_c.ExLog_Save("OpenTariffConnection", $"id={id},data_status={data_status},perspective_year={perspective_year}", ex.Message, userId);
			}

			return Json(new { list });
		}

		#region SAVE_DATA HEAT POINT SWITCHING

		// Сохранение значений при переключении ТП
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HpSwitching_Save(IFormCollection model)
		{
			var outer_sources_list = (model["hpSwitchParamNumberTso"]).ToString().Split(" ");
			//var selected_source_id = Convert.ToInt32(model["hpSwitchSourcesList"]);
			var selected_output_id = Convert.ToInt32(model["hpSwitchSourceHeadsList"]);
			var date_start = Convert.ToInt32(model["hpSwitchSourcePerspectiveYearsListStart"]);
			var date_end = Convert.ToInt32(model["hpSwitchSourcePerspectiveYearsListFinish"]);
			var data_status = Convert.ToInt32(model["hp_data_status"]);
			var dto = _context.HP_Perspective;

			try
			{
				for (int i = 0; i < outer_sources_list.Length; i++)
				{
					var sources = await dto.Where(n => n.data_status == data_status && n.heat_point_id == Convert.ToInt32(outer_sources_list[i])
						&& n.perspective_year >= date_start && n.perspective_year <= date_end).ToListAsync();

					if (sources.Count > 0)
					{
						foreach (var s in sources)
						{
							s.source_output_id = selected_output_id;
							s.edit_date = DateTime.Now;
							s.user_id = userId;
						}

						await _context.SaveChangesAsync();
					}
				}

				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		#endregion

		#region SAVE_DATA ADD REMOVE GEN INFO

		// Сохранение значений при добавлении или удалении общих сведений ТП
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HpAddRemove_GenInfoData_Save(HPAddRemoveGenInfoDataViewModel model)
		{
			try
			{
				var hp_id = await SetAllParameters_AddRemove_GenInfoData(model);
				await _context.SaveChangesAsync();				

                if (model.is_deleted != null && (bool)model.is_deleted)
					return Json(new { success = true, is_deleted = true, _is_new_hp });

                string address = model.hp_id + " (" + model.add_remove_gen_info_tso_hp_num + " - " + model.add_remove_gen_info_hp_address + ")";
                return Json(new { success = true, is_deleted = false, _is_new_hp, heat_point_id = hp_id, address = address });
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		private async Task<int?> SetAllParameters_AddRemove_GenInfoData(HPAddRemoveGenInfoDataViewModel model)
		{
			var hp_hist = await _context.HP_History.Where(x => x.data_status == model.data_status && x.heat_point_id == model.hp_id).FirstOrDefaultAsync();
			var hp = await _context.HeatPoint.Where(x => x.heat_point_id == model.hp_id).FirstOrDefaultAsync();

			try
			{
				if (hp_hist == null && hp != null)
				{
					hp_hist = await CreateHP_History(model, hp.heat_point_id);
					SetExistModel_GenInfoData_HeatPoint(hp, model);
					_context.HeatPoint.Update(hp);
				}
				else if (hp_hist == null && hp == null)
				{
					hp = await CreateHeatPoint(model);
					hp_hist = await CreateHP_History(model, hp.heat_point_id);
					_is_new_hp = true;
				}
				else
				{
					SetExistModel_GenInfoData_HP_History(hp_hist, model);
					SetExistModel_GenInfoData_HeatPoint(hp, model);
					_context.HP_History.Update(hp_hist);
					_context.HeatPoint.Update(hp);
				}
			}
			catch (Exception ex) { }

			return hp_hist.heat_point_id;
		}

		private void SetExistModel_GenInfoData_HP_History(HP_History history, HPAddRemoveGenInfoDataViewModel model, bool isNewData = false)
		{
			history.tz_id = model.add_remove_gen_info_tz_id;
			history.address = model.add_remove_gen_info_hp_address;
			history.tso_hp_num = model.add_remove_gen_info_tso_hp_num;
			history.org_owner_id = model.add_remove_gen_info_hp_org_owner_id;
			history.obj_vacant_prop = model.add_remove_gen_info_is_ownerless_hp_id;
			history.obj_old_prop = model.add_remove_gen_info_is_dilapidated_hp;
			history.last_year_reconstr = model.add_remove_gen_info_last_year_reconstr;
			history.year_liquidate = model.add_remove_gen_info_liqiudate_year;
			if (!isNewData)
				history.edit_date = DateTime.Now;
			history.user_id = userId;
			history.is_deleted = model.is_deleted ?? false;
		}

		private void SetExistModel_GenInfoData_HeatPoint(HeatPoint heatPoint, HPAddRemoveGenInfoDataViewModel model, bool isNewData = false)
		{
			heatPoint.hp_type_id = model.add_remove_gen_info_hp_type_id;
			heatPoint.hp_name = model.add_remove_gen_info_hp_name;
			heatPoint.hp_type_loc_id = model.add_remove_gen_info_hp_location_id;
			heatPoint.district_id = model.add_remove_gen_info_district_hp_id;
			heatPoint.kad_num_zu = model.add_remove_gen_info_kad_num_zu;
			heatPoint.kad_num_oks = model.add_remove_gen_info_kad_num_oks;
			heatPoint.fias_zu = model.add_remove_gen_info_fias_zu;
			heatPoint.fias_build = model.add_remove_gen_info_fias_oks;
			heatPoint.start_year_expl = model.add_remove_gen_info_start_year_expl;
			if (!isNewData)
				heatPoint.edit_date = DateTime.Now;
			heatPoint.user_id = userId;
		}

		private async Task<HP_History?> CreateHP_History(HPAddRemoveGenInfoDataViewModel model, int heat_point_id)
		{
			try
			{
				var new_hp_hist = new HP_History { data_status = model.data_status, heat_point_id = heat_point_id, create_date = DateTime.Now };
				SetExistModel_GenInfoData_HP_History(new_hp_hist, model, true);
				await _context.HP_History.AddAsync(new_hp_hist);
				await _context.SaveChangesAsync();
				return new_hp_hist;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		private async Task<HeatPoint?> CreateHeatPoint(HPAddRemoveGenInfoDataViewModel model)
		{
			try
			{
				var new_hp = new HeatPoint { create_date = DateTime.Now };
				SetExistModel_GenInfoData_HeatPoint(new_hp, model, true);
				await _context.HeatPoint.AddAsync(new_hp);
				await _context.SaveChangesAsync();
				return new_hp;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		[HttpPost]
		private async Task SetHpPerspectiveStatusId(int data_status, int heat_point_id)
		{
			var hp_per_list = _context.HP_History.Where(n => n.heat_point_id == heat_point_id);
			var _data_status_max = hp_per_list.Where(n => n.heat_point_id == heat_point_id).Max(n => n.data_status);
			var item = await hp_per_list.FirstOrDefaultAsync(n => n.data_status == _data_status_max && n.heat_point_id == heat_point_id);
			string? last_num_from_db = string.Empty;
			bool is_cur_data_status_exist = _data_status_max == data_status;

			if (item.tso_hp_num != null && !item.tso_hp_num.StartsWith("OP"))
			{
				item.tso_hp_num = _new_hp_num = await GenerateTsoHpNum(heat_point_id, hp_per_list);
				_new_hp_address = heat_point_id + " (" + item.tso_hp_num + " - " + item.address + ")";

				if (!is_cur_data_status_exist)
				{
					item.data_status = data_status;
					await _context.HP_History.AddAsync(item);
				}								
				else
					_context.HP_History.Update(item);

				await _context.SaveChangesAsync();
			}
		}

		private async Task<string?> GenerateTsoHpNum(int heat_point_id, IQueryable<HP_History> hp_list)
		{
			var is_per_num = await hp_list.Where(n => n.heat_point_id == heat_point_id).FirstOrDefaultAsync(n => n.tso_hp_num.Contains("OP"));

			if (is_per_num != null)
				return is_per_num.tso_hp_num;

			var last_num_from_db = (await _context.HP_History.Where(n => n.tso_hp_num.Contains("OP")).OrderByDescending(n => n.tso_hp_num).FirstOrDefaultAsync()).tso_hp_num;
			var num_char_arr = last_num_from_db.Replace("OP", "").ToCharArray();
			var zero_char_count = 0;
			var nine_char_count = 0;
			var last_num = Convert.ToInt32(last_num_from_db.Replace("OP", "")) + 1;

			return "OP" + last_num.ToString("D7");
		}

		#endregion

		#region SAVE_DATA ADD REMOVE YEAR IMPLEMENT SCHEME PARAM

		// Сохранение значений при добавлении или удалении общих сведений ТП
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HpAddRemove_YearImplementSchemeParamData_Save(IFormCollection model)
		{
			try
			{
				var dict = YearImplementSchemeParamData_CreateModelsDict(model);
				await YearImplementSchemeParamData_CreateDataRows(dict);
				return Json(new { success = true,  new_hp_num = _new_hp_num, new_hp_address = _new_hp_address });
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		private Dictionary<string, string?[]> YearImplementSchemeParamData_CreateModelsDict(IFormCollection model)
		{
			var dictionary = new Dictionary<string, string?[]>()
			{
				["add_remove_yisp_perspective_year"] = model["add_remove_yisp_perspective_year"].ToArray(),
				["add_remove_yisp_tso_select"] = model["add_remove_yisp_tso_select"].ToArray(),
				["add_remove_yisp_status_select"] = model["add_remove_yisp_status_select"].ToArray(),
				["add_remove_yisp_source_output_select"] = model["add_remove_yisp_source_output_select"].ToArray(),
				["add_remove_yisp_heat_network_input"] = model["add_remove_yisp_heat_network_input"].ToArray(),
				["add_remove_yisp_temp_ht_plan_select"] = model["add_remove_yisp_temp_ht_plan_select"].ToArray(),
				["add_remove_yisp_hp_type_scheme_select"] = model["add_remove_yisp_hp_type_scheme_select"].ToArray(),
				["add_remove_yisp_tech_connect_select"] = model["add_remove_yisp_tech_connect_select"].ToArray(),
				["add_remove_yisp_temp_tech_plan_select"] = model["add_remove_yisp_temp_tech_plan_select"].ToArray(),
				["add_remove_yisp_heat_connect_select"] = model["add_remove_yisp_heat_connect_select"].ToArray(),
				["add_remove_yisp_temp_heat_plan_select"] = model["add_remove_yisp_temp_heat_plan_select"].ToArray(),
				["add_remove_yisp_vent_connect_select"] = model["add_remove_yisp_vent_connect_select"].ToArray(),
				["add_remove_yisp_temp_vent_plan_select"] = model["add_remove_yisp_temp_vent_plan_select"].ToArray(),
				["add_remove_yisp_hw_connect_select"] = model["add_remove_yisp_hw_connect_select"].ToArray(),
				["add_remove_yisp_temp_gvs_plan_select"] = model["add_remove_yisp_temp_gvs_plan_select"].ToArray(),
				["data_status"] = model["tz_list.data_status"].ToArray(),
				["hp_id"] = model["tz_list.hp_id"].ToArray()
			};
			return dictionary;
		}

		private async Task YearImplementSchemeParamData_CreateDataRows(Dictionary<string, string?[]> dict)
		{
			var _data_status = Convert.ToInt16(dict["data_status"][0]);
			var _heat_point_id = Convert.ToInt16(dict["hp_id"][0]);
			bool is_new;
			var list = new List<HP_Perspective>();
			int row_count = Convert.ToInt32(dict["add_remove_yisp_perspective_year"].Length);

			for (int i = 0; i < row_count; i++)
			{
				is_new = false;
				var _perspective_year = Convert.ToInt16(dict["add_remove_yisp_perspective_year"][i]);
				HP_Perspective row;
				row = await _context.HP_Perspective.FirstOrDefaultAsync(n => n.data_status == _data_status && n.perspective_year == _perspective_year && n.heat_point_id == _heat_point_id);

				if (row == null)
				{
					row = new HP_Perspective();
					is_new = true;
				}

				row.perspective_year = _perspective_year;
				row.tso_id = dict["add_remove_yisp_tso_select"][i] == "" ? null : Convert.ToInt32(dict["add_remove_yisp_tso_select"][i]);
				row.hp_status_id = dict["add_remove_yisp_status_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_status_select"][i]);
				row.source_output_id = dict["add_remove_yisp_source_output_select"][i] == "" ? null : Convert.ToInt32(dict["add_remove_yisp_source_output_select"][i]);
				row.heat_network_id = dict["add_remove_yisp_heat_network_input"][i] == "" ? null : Convert.ToInt32(dict["add_remove_yisp_heat_network_input"][i]);
				row.temp_ht_plan_id = dict["add_remove_yisp_temp_ht_plan_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_temp_ht_plan_select"][i]);
				row.hp_type_scheme_id = dict["add_remove_yisp_hp_type_scheme_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_hp_type_scheme_select"][i]);
				row.tech_connect_id = dict["add_remove_yisp_tech_connect_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_tech_connect_select"][i]);
				row.temp_tech_plan_id = dict["add_remove_yisp_temp_tech_plan_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_temp_tech_plan_select"][i]);
				row.heat_connect_id = dict["add_remove_yisp_heat_connect_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_heat_connect_select"][i]);
				row.temp_heat_plan_id = dict["add_remove_yisp_temp_heat_plan_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_temp_heat_plan_select"][i]);
				row.vent_connect_id = dict["add_remove_yisp_vent_connect_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_vent_connect_select"][i]);
				row.temp_vent_plan_id = dict["add_remove_yisp_temp_vent_plan_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_temp_vent_plan_select"][i]);
				row.hw_connect_id = dict["add_remove_yisp_hw_connect_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_hw_connect_select"][i]);
				row.temp_gvs_plan_id = dict["add_remove_yisp_temp_gvs_plan_select"][i] == "" ? null : Convert.ToInt16(dict["add_remove_yisp_temp_gvs_plan_select"][i]);
				row.data_status = _data_status;
				row.heat_point_id = _heat_point_id;
				row.user_id = userId;

				if (i == 0 && (row.hp_status_id == -1 || row.hp_status_id == 5))
					await SetHpPerspectiveStatusId(_data_status, _heat_point_id);

				await YearImplementSchemeParamData_SaveDataRow(row, is_new);
			}
		}

		private async Task YearImplementSchemeParamData_AddDataRow(HP_Perspective perspective)
		{
			perspective.create_date = DateTime.Now;
			await _context.HP_Perspective.AddAsync(perspective);
		}

		private async Task YearImplementSchemeParamData_UpdateDataRow(HP_Perspective perspective)
		{
			perspective.edit_date = DateTime.Now;
			_context.HP_Perspective.Update(perspective);
		}

		private async Task YearImplementSchemeParamData_SaveDataRow(HP_Perspective perspective, bool isNew)
		{
			if (isNew)
				await YearImplementSchemeParamData_AddDataRow(perspective);
			else
				await YearImplementSchemeParamData_UpdateDataRow(perspective);
			await _context.SaveChangesAsync();
		}

		#endregion

		#region SAVE_DATA EQUIPMENT PART 

		// Сохранение значений при добавлении или изменении в Состав оборудования
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HpAddRemove_EquipmentPart_Save(HP_TankBatteryAvailables model)
		{
			try
			{
				await HpAddRemove_EquipmentPart_CreateData(model);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		private async Task HpAddRemove_EquipmentPart_CreateData(HP_TankBatteryAvailables model)
		{
			bool is_new = false;
			var item = await _context.HP_TankBatteryAvailables.FirstOrDefaultAsync(n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id);

			if (item == null)
			{
				item = new HP_TankBatteryAvailables();
				is_new = true;
			}

			if (model.avail_tank_battery != null && (bool)model.avail_tank_battery)
				item.tank_capacity = model.tank_capacity;
			else
				item.tank_capacity = null;

			item.avail_tank_battery = model.avail_tank_battery;
			item.heat_point_id = model.heat_point_id;
			item.data_status = model.data_status;
			item.user_id = userId;

			await HpAddRemove_EquipmentPart_SaveDb(item, is_new);
		}

		private async Task HpAddRemove_EquipmentPart_SaveDb(HP_TankBatteryAvailables item, bool is_new)
		{
			if (is_new)
			{
				item.create_date = DateTime.Now;
				await _context.HP_TankBatteryAvailables.AddAsync(item);
			}
			else
			{
				item.edit_date = DateTime.Now;
				_context.HP_TankBatteryAvailables.Update(item);
			}

			await _context.SaveChangesAsync();
		}

		#endregion

		#region SAVE_DATA HEAT_EXCHANGE_EQUIPMENT

		// Сохранение значений при добавлении или изменении в Теплообменное оборудование Состава оборудования
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HpAddRemove_HeatExchangeEquipment_Save(HPAddRemoveHP_HeatExchangerMappsDataViewModel model)
		{
			try
			{
				await HpAddRemove_HeatExchangeEquipment_CreateData(model);

				if (model.is_deleted != null && (bool)model.is_deleted)
					return Json(new { success = true, is_deleted = true });

				return Json(new { success = true, is_deleted = false });
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		private async Task HpAddRemove_HeatExchangeEquipment_CreateData(HPAddRemoveHP_HeatExchangerMappsDataViewModel model)
		{
			bool is_new = false;
			var item = await _context.HP_HeatExchangerMapps.FirstOrDefaultAsync
				(n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id && n.ht_exch_num == model.ht_exch_num);

			if (item == null)
			{
				if (model.ht_exch_num != 0)
				{
					item = await _context.HP_HeatExchangerMapps.FirstOrDefaultAsync
						(n => n.heat_point_id == model.heat_point_id && n.ht_exch_num == model.ht_exch_num);
				}
				else
				{
					var value = await _context.HP_HeatExchangerMapps.Where
						(n => n.heat_point_id == model.heat_point_id).MaxAsync(n => (short?)n.ht_exch_num) ?? 0;

					item = new HP_HeatExchangerMapps { ht_exch_num = ++value };
				}
				is_new = true;
			}
			else
				item.ht_exch_num = model.ht_exch_num;

			item.type_equip_id = model.type_equip_id;
			item.type_id = model.type_id;
			item.stage_scheme_gvs_id = model.stage_scheme_gvs_id;
			item.equip_id = model.equip_id;
			item.ht_exch_count = model.ht_exch_count;
			item.heat_exchange_surface = model.heat_exchange_surface;
			item.inst_heat_power = model.inst_heat_power;
			item.section_count = model.section_count;
			item.casing_diameter = model.casing_diameter;
			item.length_section = model.length_section;
			item.heat_point_id = model.heat_point_id;
			item.data_status = model.data_status;
			item.user_id = userId;
			item.is_deleted = model.is_deleted ?? false;

			await HpAddRemove_HeatExchangeEquipment_SaveDb(item, is_new);
			var val = HpAddRemove_HeatExchangeEquipment_CalculateInstHeatPower(model).Result;
			await HpAddRemove_HeatExchangeEquipment_SaveCalculateInstHeatPower(val, model);
		}

		private async Task HpAddRemove_HeatExchangeEquipment_SaveDb(HP_HeatExchangerMapps item, bool is_new)
		{
			if (is_new)
			{
				item.create_date = DateTime.Now;
				await _context.HP_HeatExchangerMapps.AddAsync(item);
			}
			else
			{
				item.edit_date = DateTime.Now;
				_context.HP_HeatExchangerMapps.Update(item);
			}
			await _context.SaveChangesAsync();
		}

		private async Task<decimal> HpAddRemove_HeatExchangeEquipment_CalculateInstHeatPower(HPAddRemoveHP_HeatExchangerMappsDataViewModel model)
		{
			var list = await _context.HPAddRemoveHP_HeatExchangerMappsDataViewModel.FromSqlInterpolated($"exec heat_points.sp_GetHP_HeatExchange {model.data_status},{model.heat_point_id}").ToListAsync();

			decimal sum = 0;

			foreach (var item in list)
				if (item.inst_heat_power != null && item.ht_exch_count != null)
					sum += (decimal)item.inst_heat_power * (int)item.ht_exch_count;

			return sum;
		}

		private async Task HpAddRemove_HeatExchangeEquipment_SaveCalculateInstHeatPower(decimal value, HPAddRemoveHP_HeatExchangerMappsDataViewModel model)
		{
			bool is_new = false;
			var item = await _context.HP_History.Where(n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id).FirstOrDefaultAsync();

			if (item == null)
			{
				item = await _context.HP_History.Where(n => n.heat_point_id == model.heat_point_id).OrderByDescending(n => n.data_status).FirstAsync();
				item.data_status = model.data_status;
				is_new = true;
			}

			if (item.inst_heat_power == value)
				return;

			item.inst_heat_power = value;
			item.user_id = userId;

			if (is_new)
			{
				item.create_date = DateTime.Now;
				await _context.HP_History.AddAsync(item);
			}
			else
			{
				item.edit_date = DateTime.Now;
				_context.HP_History.Update(item);
			}

			_context.SaveChanges();
		}

		#endregion

		#region SAVE_DATA PUMP

		// Сохранение значений при добавлении или изменении в Насосное оборудование Состава оборудования
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HpAddRemove_Pump_Save(HPAddRemoveHP_PumpMappsDataViewModel model)
		{
			try
			{
				await HpAddRemove_Pump_CreateData(model);

				if (model.is_deleted != null && (bool)model.is_deleted)
					return Json(new { success = true, is_deleted = true });

				return Json(new { success = true, is_deleted = false });
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		private async Task HpAddRemove_Pump_CreateData(HPAddRemoveHP_PumpMappsDataViewModel model)
		{
			bool is_new = false;
			var item = await _context.HP_PumpMapps.FirstOrDefaultAsync
				(n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id && n.pump_num == model.pump_num);

			if (item == null)
			{
				if (model.pump_num != 0)
				{
					item = await _context.HP_PumpMapps.FirstOrDefaultAsync
						(n => n.heat_point_id == model.heat_point_id && n.pump_num == model.pump_num);
				}
				else
				{
					var value = await _context.HP_PumpMapps.Where
						(n => n.heat_point_id == model.heat_point_id).MaxAsync(n => (short?)n.pump_num) ?? 0;

					item = new HP_PumpMapps { pump_num = ++value };
				}
				is_new = true;
			}
			else
				item.pump_num = model.pump_num;

			item.type_pump_id = model.type_pump_id;
			item.equip_id = model.equip_id;
			item.pump_count = model.pump_count;
			item.pump_capacity = model.pump_capacity;
			item.pump_press = model.pump_press;
			item.heat_point_id = model.heat_point_id;
			item.data_status = model.data_status;
			item.user_id = userId;
			item.is_deleted = model.is_deleted ?? false;

			await HpAddRemove_Pump_SaveDb(item, is_new);
		}

		private async Task HpAddRemove_Pump_SaveDb(HP_PumpMapps item, bool is_new)
		{
			if (is_new)
			{
				item.create_date = DateTime.Now;
				await _context.HP_PumpMapps.AddAsync(item);
			}
			else
			{
				item.edit_date = DateTime.Now;
				_context.HP_PumpMapps.Update(item);
			}
			await _context.SaveChangesAsync();
		}

		#endregion

		#region SAVE_DATA AUTOMATIZATION

		// Сохранение значений при добавлении или изменении в Состав оборудования
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HpAddRemove_Automatization_Save(HPAddRemove_AutomatizationViewModel model)
		{
			try
			{
				await HpAddRemove_Automatization_CreateData(model);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		private async Task HpAddRemove_Automatization_CreateData(HPAddRemove_AutomatizationViewModel model)
		{
			bool is_new = false;
			var item = await _context.HP_Automatization.FirstOrDefaultAsync(n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id);

			if (item == null)
			{
				item = new HP_Automatization();
				is_new = true;
			}

			if (model.avail_automatic != null && (bool)model.avail_automatic)
				item.automatic_type_id = model.automatic_type_id;
			else
				item.automatic_type_id = null;

			item.avail_automatic = model.avail_automatic;
			item.avail_flow_limiter = model.avail_flow_limiter;
			item.avail_automatic_heat_control = model.avail_automatic_heat_control;
			item.heat_point_id = model.heat_point_id;
			item.data_status = model.data_status;
			item.user_id = userId;

			await HpAddRemove_Automatization_SaveDb(item, is_new);
		}

		private async Task HpAddRemove_Automatization_SaveDb(HP_Automatization item, bool is_new)
		{
			if (is_new)
			{
				item.create_date = DateTime.Now;
				await _context.HP_Automatization.AddAsync(item);
			}
			else
			{
				item.edit_date = DateTime.Now;
				_context.HP_Automatization.Update(item);
			}

			await _context.SaveChangesAsync();
		}

		#endregion

		#region SAVE_DATA ACC RESOURCES

		// Сохранение значений при добавлении или изменении в Учет ресурсов
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HpAddRemove_AccResources_Save(HPAddRemove_AccResourcesViewModel model)
		{
			try
			{
				await HpAddRemove_AccResources_CreateData(model);

				if (model.is_deleted != null && (bool)model.is_deleted)
					return Json(new { success = true, is_deleted = true });

				return Json(new { success = true, is_deleted = false });
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		private async Task HpAddRemove_AccResources_CreateData(HPAddRemove_AccResourcesViewModel model)
		{
			bool is_new = false;
			var item = await _context.HP_AccResources.FirstOrDefaultAsync
				(n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id && n.device_num == model.device_num);

			if (item == null)
			{
				if (model.device_num != 0)
				{
					item = await _context.HP_AccResources.FirstOrDefaultAsync
						(n => n.heat_point_id == model.heat_point_id && n.device_num == model.device_num);
				}
				else
				{
					var value = await _context.HP_AccResources.Where
						(n => n.heat_point_id == model.heat_point_id).MaxAsync(n => (int?)n.device_num) ?? 0;

					item = new HP_AccResources { device_num = ++value };
				}
				is_new = true;
			}
			else
				item.device_num = model.device_num;

			item.acc_resource_type_id = model.acc_resource_type_id;
			item.meter_device_mark = model.meter_device_mark;
			item.count_devices = model.count_devices;
			item.heat_point_id = model.heat_point_id;
			item.data_status = model.data_status;
			item.user_id = userId;
			item.is_deleted = model.is_deleted ?? false;

			await HpAddRemove_AccResources_SaveDb(item, is_new);
		}

		private async Task HpAddRemove_AccResources_SaveDb(HP_AccResources item, bool is_new)
		{
			if (is_new)
			{
				item.create_date = DateTime.Now;
				await _context.HP_AccResources.AddAsync(item);
			}
			else
			{
				item.edit_date = DateTime.Now;
				_context.HP_AccResources.Update(item);
			}
			await _context.SaveChangesAsync();
		}

		#endregion

		#region SAVE_DATA NUM SIGN OTHER DATA BASE

		// Сохранение значений при добавлении или изменении в Номера и обозначения ТП в других базах данных 
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HpAddRemove_NumSignOtherDb_Save(HPAddRemove_NumSignOtherDbViewModel model)
		{
			try
			{
				await HpAddRemove_NumSignOtherDb_CreateData(model);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		private async Task HpAddRemove_NumSignOtherDb_CreateData(HPAddRemove_NumSignOtherDbViewModel model)
		{
			bool is_new = false;
			var item = await _context.HP_History.FirstOrDefaultAsync
				(n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id);

			if (item == null)
			{
				item = await _context.HP_History.Where
						(n => n.heat_point_id == model.heat_point_id).OrderByDescending(n => n.data_status).FirstAsync();

				if (item == null)
					item = new HP_History();
				is_new = true;
			}

			item.muid = model.muid;
			item.lotus_id = model.lotus_id;
			item.kasu_composite_id = model.kasu_composite_id;
			item.kasu_id = model.kasu_id;
			item.kasu_map_id = model.kasu_map_id;
			item.data_status = model.data_status;
			item.user_id = userId;

			await HpAddRemove_NumSignOtherDb_SaveDb(item, is_new);
		}

		private async Task HpAddRemove_NumSignOtherDb_SaveDb(HP_History item, bool is_new)
		{
			if (is_new)
			{
				item.create_date = DateTime.Now;
				await _context.HP_History.AddAsync(item);
			}
			else
			{
				item.edit_date = DateTime.Now;
				_context.HP_History.Update(item);
			}
			await _context.SaveChangesAsync();
		}

		#endregion

		#region SAVE_DATA DOCS FOOTAGE

		// Сохранение документации и фотоматериалов по ТП
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HpAddRemove_DocsFootage_Save(HPAddRemove_DocsFootageViewModel model)
		{
			try
			{
				await HpAddRemove_DocsFootage_CreateData(model);

				if (model.is_deleted != null && (bool)model.is_deleted)
					return Json(new { success = true, is_deleted = true });

				return Json(new { success = true, is_deleted = false });
			}
			catch (Exception ex)
			{
				//_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		private async Task HpAddRemove_DocsFootage_CreateData(HPAddRemove_DocsFootageViewModel model)
		{
			bool is_new = false;
			bool is_deleting_file = false;
			var item = await _context.HP_DocsPhotos_History.FirstOrDefaultAsync
				(n => n.data_status == model.data_status && n.heat_point_id == model.heat_point_id && n.doc_num == model.doc_num);

			if (item == null)
			{
				if (model.doc_num != 0)
				{
					item = await _context.HP_DocsPhotos_History.FirstOrDefaultAsync
						(n => n.heat_point_id == model.heat_point_id && n.doc_num == model.doc_num);
				}
				else
				{
					var value = await _context.HP_DocsPhotos_History.Where
						(n => n.heat_point_id == model.heat_point_id).MaxAsync(n => (int?)n.doc_num) ?? 0;

					item = new HP_DocsPhotos_History { doc_num = ++value };
				}
				is_new = true;
			}
			else
			{
				is_deleting_file = true;
				item.doc_num = model.doc_num;
			}

			item.doc_type_id = model.doc_type_id;
			item.doc_description = model.doc_description;
			item.heat_point_id = model.heat_point_id;
			item.data_status = model.data_status;
			item.user_id = userId;
			item.is_deleted = model.is_deleted ?? false;

			if ((bool)!item.is_deleted)
				HpAddRemove_DocsFootage_SaveFile(item, is_deleting_file);

			await HpAddRemove_DocsFootage_SaveDb(item, is_new);
		}

		private async Task HpAddRemove_DocsFootage_SaveDb(HP_DocsPhotos_History item, bool is_new)
		{
			if (is_new)
			{
				item.create_date = DateTime.Now;
				await _context.HP_DocsPhotos_History.AddAsync(item);
			}
			else
			{
				item.edit_date = DateTime.Now;
				_context.HP_DocsPhotos_History.Update(item);
			}
			await _context.SaveChangesAsync();
		}

		private void HpAddRemove_DocsFootage_SaveFile(HP_DocsPhotos_History item, bool isDelFile)
		{
			var file = HttpContext.Request.Form.Files.First();
			string webRootPath = _hostingEnvironment.WebRootPath;
			string upload = webRootPath + "\\Docs\\HeatPointDocs\\";

			if (file == null)
				return;

			string fileName = Path.GetFileNameWithoutExtension(file.FileName);
			string extension = Path.GetExtension(file.FileName);
			string url = upload + fileName + extension;
			var oldUrl = Path.Combine(upload, item.doc_url ?? "");

			if (isDelFile && System.IO.File.Exists(oldUrl))
				System.IO.File.Move(oldUrl, oldUrl + "_deleted");

			string date = DateTime.Now.ToString("yyyy/MM/dd/hh/mm/ss");
			item.doc_name = fileName + extension;
			fileName = fileName + "_" + item.data_status + "_" + item.heat_point_id + "_" + item.doc_num + "_" + date + extension;

			using (var fs = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
				file.CopyTo(fs);

			item.doc_url = fileName;
		}

		#endregion
	}
}
