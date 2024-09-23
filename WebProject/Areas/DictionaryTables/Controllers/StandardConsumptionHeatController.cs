using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.DictionaryTables.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("DictionaryTables")]
	public class StandardConsumptionHeatController : Controller
	{
		private readonly ILogger<StandardConsumptionHeatController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		public StandardConsumptionHeatController(ILogger<StandardConsumptionHeatController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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
					userDisplayName = "Сергеев Андрей Сергеевич";
				}
			}
		}
		#region ViewComponent
		public ActionResult OnGetCallBuildingCharacteristics_PartialViewComponent(int data_status, int PurporseTypeBuild)
		{
			return ViewComponent("BuildingCharacteristics_Partial", new { userId, data_status, PurporseTypeBuild });
		}

		public ActionResult OnGetCallCoefCalculation_PartialViewComponent(int data_status)
		{
			return ViewComponent("CoefCalculation_Partial", new { userId, data_status });
		}

		public ActionResult OnGetCallSpecificHeatConsumption_PartialViewComponent(int data_status, int consumptionUnit, int district_id)
		{
			List<Dict_Floors> floors = new List<Dict_Floors>();
			floors = ViewBag.Floor = _context.Dict_Floors.Select(x => new Dict_Floors { Id = x.Id, floor_name = x.floor_name }).ToList();
			ViewBag.MaxFloor = floors.Count();
			return ViewComponent("SpecificHeatConsumption_Partial", new { userId, data_status, consumptionUnit, district_id });
		}
		public ActionResult OnGetCallTotalCalcConsumption_PartialViewComponent(int data_status, int consumptionUnit, int district_id)
		{
			List<Dict_Floors> floors = new List<Dict_Floors>();
			floors = ViewBag.Floor = _context.Dict_Floors.Select(x => new Dict_Floors { Id = x.Id, floor_name = x.floor_name }).ToList();
			ViewBag.MaxFloor = floors.Count();
			return ViewComponent("TotalCalcConsumption_Partial", new { userId, data_status, consumptionUnit, district_id });
		}
		public ActionResult OnGetCallCalcConsumptionGVS_PartialViewComponent(int data_status, int consumptionUnit, int district_id)
		{
			return ViewComponent("CalcConsumptionGVS_Partial", new { userId, data_status, consumptionUnit, district_id });
		}

		public ActionResult OnGetCallCoefEnergyEfficiency_PartialViewComponent(int data_status)
		{
			return ViewComponent("CoefEnergyEfficiency_Partial", new { userId, data_status });
		}
		#endregion
		public async Task<IActionResult> StandardConsumptionHeatList()
		{
			int data_status = _m_c.GetCurrentDS();
			List<Dict_Floors> floors = new List<Dict_Floors>();
			ViewBag.PurporseTypeBuild = await _context.fnt_PurporseTypeBuildList().ToListAsync();
			ViewBag.Distric = await _context.fnt_GetDistrictRegionList().ToListAsync();
            floors = ViewBag.Floor = _context.Dict_Floors.Select(x => new Dict_Floors { Id = x.Id, floor_name = x.floor_name }).ToList();
			ViewBag.MaxFloor = floors.Count();
			
			
			return View();
		}

		#region Save_Data
		//Сохранение характеристик зданий по их назначению
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> BuildingCharacteristics_Save(IFormCollection model)
		{
			short purpose_build_type_id = 0, floor_id = 0, coef_id = 0;
			int data_status = 0;
			try
			{
				
				for (int i = 0; i < model["floor_id"].Count; i++)
				{
					purpose_build_type_id = Convert.ToInt16(model["purpose_build_type_id"][i]);
					floor_id = Convert.ToInt16(model["floor_id"][i]);
					data_status = Convert.ToInt32(model["data_status"][i]);

					for (coef_id = 1; coef_id <= 6; coef_id++)
					{
						var buildCharact_upd = await _context.CoefBuildConsumer_Histories.Where(x => x.coef_id == coef_id && x.purpose_build_type_id == purpose_build_type_id && x.data_status == data_status && (x.floors_id == floor_id || x.floors_id == -1)).FirstOrDefaultAsync();

						if (buildCharact_upd != null)
						{
							decimal NormBasicCharact = Convert.ToDecimal(model["NormBasicCharact"][i]);

							switch (coef_id)
							{
								case 3:
									buildCharact_upd.coef_value = Convert.ToDecimal(model["AvgFloorHeight"]);
									break;
								case 4:
									buildCharact_upd.coef_value = Convert.ToDecimal(model["AvgBuildProvision"]);
									break;
								case 1:
									buildCharact_upd.coef_value = NormBasicCharact;
									break;
								case 5:
									buildCharact_upd.coef_value = Convert.ToDecimal(model["CalcAirTemp"]);
									break;
								case 2:
									buildCharact_upd.coef_value = Convert.ToDecimal(model["NormHWConsumption"]);
									break;
								case 6:
									buildCharact_upd.coef_value = Convert.ToDecimal(model["TempHW"]);
									break;
							}
							buildCharact_upd.edit_date = DateTime.Now;
							buildCharact_upd.user_id = userId;
							await _context.SaveChangesAsync();
						}
						else
						{
							CoefBuildConsumer_History buildCharact_new = new();
							buildCharact_new.purpose_build_type_id = purpose_build_type_id;
							buildCharact_new.floors_id = floor_id;
							buildCharact_new.data_status = data_status;
							buildCharact_new.coef_id = coef_id;
							switch (coef_id)
							{
								case 3:
									buildCharact_new.coef_value = Convert.ToDecimal(model["AvgFloorHeight"]);
									break;
								case 4:
									buildCharact_new.coef_value = Convert.ToDecimal(model["AvgBuildProvision"]);
									break;
								case 1:
									buildCharact_new.coef_value = Convert.ToDecimal(model["NormBasicCharact"][i]);
									break;
								case 5:
									buildCharact_new.coef_value = Convert.ToDecimal(model["CalcAirTemp"]);
									break;
								case 2:
									buildCharact_new.coef_value = Convert.ToDecimal(model["NormHWConsumption"]);
									break;
								case 6:
									buildCharact_new.coef_value = Convert.ToDecimal(model["TempHW"]);
									break;
							}
							buildCharact_new.is_deleted = false;
							buildCharact_new.create_date = DateTime.Now;
							buildCharact_new.user_id = userId;
							if (buildCharact_new.coef_value > 0)
							{
								await _context.AddAsync(buildCharact_new);
								await _context.SaveChangesAsync();
							}
						}
					}
				}
			}
			catch (Exception ex) 
			{
				_m_c.ExLog_Save("BuildingCharacteristics_Save", $"data_status={data_status},coef_id={coef_id}, purpose_build_type_id={purpose_build_type_id}, floor_id={floor_id}", ex.Message, userId);
				return Json(new { success = false });
			}
				return Json(new { success = true });
		}

		//Сохранение коэффициентов, участвующих в расчетах
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CalcCoef_Save(CalcCoefViewModel model)
		{
			try
			{
				await CalcCoefBuildConsumer_History_Save(model);
				await CalcCoefHeatLoss_History_Save(model);
				return Json(new { success = true });
			}
			catch
			{
				return Json(new { success = false });
			}
			
		}

		//Добавление новой записи коэффициентов, участвующих в расчетах в таблицу CoefBuildConsumer_Histories
		public async Task CalcCoefBuildConsumer_History_Save(CalcCoefViewModel model)
		{
			short[] coef_id_arr = new short[10] { 7, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
			int i = 0;
			try
			{
				
				decimal? coef_value = 0;
				for (i = 0; i < coef_id_arr.Length; i++)
				{
					switch (coef_id_arr[i])
					{
						case 7:
							coef_value = model.coef_rad;
							break;
						case 10:
							coef_value = model.percent_devition_specific_heat_consumption;
							break;
						case 11:
							coef_value = model.coef_hw_heat_decrease;
							break;
						case 12:
							coef_value = model.coef_hw_vent_decrease;
							break;
						case 13:
							coef_value = model.coef_hw_gvs_decrease;
							break;
						case 14:
							coef_value = model.coef_hw_tech_decrease;
							break;
						case 15:
							coef_value = model.coef_steam_heat_decrease;
							break;
						case 16:
							coef_value = model.coef_steam_vent_decrease;
							break;
						case 17:
							coef_value = model.coef_steam_gvs_decrease;
							break;
						case 18:
							coef_value = model.coef_steam_tech_decrease;
							break;
					}

					var coefBuild_upd = await _context.CoefBuildConsumer_Histories.Where(x => x.coef_id == coef_id_arr[i] && x.purpose_build_type_id == -1 && x.data_status == model.data_status && x.floors_id == -1).FirstOrDefaultAsync();
					if (coefBuild_upd != null)
					{
						coefBuild_upd.coef_value = coef_value;
						coefBuild_upd.user_id = userId;
						coefBuild_upd.is_deleted = false;
						coefBuild_upd.edit_date = DateTime.Now;
					}
					else
					{
						CoefBuildConsumer_History coefBuild_new = new();
						coefBuild_new.coef_id = coef_id_arr[i];
						coefBuild_new.purpose_build_type_id = -1;
						coefBuild_new.floors_id = -1;
						coefBuild_new.data_status = model.data_status;
						coefBuild_new.coef_value = coef_value;
						coefBuild_new.user_id = userId;
						coefBuild_new.is_deleted = false;
						coefBuild_new.create_date = DateTime.Now;

						if (coefBuild_new.coef_value > 0)
						{
							await _context.AddAsync(coefBuild_new);
						}
					}
					await _context.SaveChangesAsync();
				}
			}
			catch(Exception ex)
			{
				_m_c.ExLog_Save("CalcCoef_Save", $"data_status={model.data_status},coef_id={coef_id_arr[i]}", ex.Message, userId);
				throw new Exception();
			}
		}

		//Добавление новой записи коэффициентов, участвующих в расчетах в таблицу CoefHeatLoss_Histories
		public async Task CalcCoefHeatLoss_History_Save(CalcCoefViewModel model)
		{
			short coef_id = 0;
			try
			{
				decimal? coef_value = 0;
				for (coef_id = 1; coef_id <= 8; coef_id++ )
				{
					switch(coef_id)
					{
						case 1:
							coef_value = model.coef_heat_loss_systems_gvs_insulated_risers_gvs;
							break;
						case 2:
							coef_value = model.coef_heat_loss_systems_gvs_insulated_risers_heated_towel_rail_gvs;
							break;
						case 3:
							coef_value = model.coef_heat_loss_systems_gvs_uninsulated_risers_heated_towel_rail_gvs;
							break;
						case 4:
							coef_value = model.coef_heat_loss_systems_gvs_insulated_risers;
							break;
						case 5:
							coef_value = model.coef_heat_loss_systems_gvs_insulated_risers_heated_towel_rail;
							break;
						case 6:
							coef_value = model.coef_heat_loss_systems_gvs_uninsulated_risers_heated_towel_rail;
							break;
						case 7:
							coef_value = model.coef_heat_loss_heat_network;
							break;
						case 8:
							coef_value = model.coef_heat_loss_steam_network;
							break;
					}

					var coefHeatLoss = await _context.CoefHeatLoss_Histories.Where(x => x.coef_id == coef_id && x.data_status == model.data_status).FirstOrDefaultAsync();
					if (coefHeatLoss != null)
					{
						coefHeatLoss.coef_value = coef_value;
						coefHeatLoss.edit_date = DateTime.Now;
						coefHeatLoss.user_id = userId;
					}
					else
					{
						coefHeatLoss.coef_value = coef_value;
						coefHeatLoss.create_date = DateTime.Now;
						coefHeatLoss.user_id = userId;
						coefHeatLoss.data_status = model.data_status;
						coefHeatLoss.coef_id = coef_id;

						await _context.AddAsync(coefHeatLoss);
					}
					await _context.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("CoefHeatLoss_History_Save", $"data_status={model.data_status},coef_id={coef_id}", ex.Message, userId);
				throw new Exception();
			}
		}

		//Сохранение коэффициенты отношения отопительно-вентиляционной тепловой нагрузки
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CoefHeatventHeatLoad_Save(IFormCollection model)
		{
			short Id = 0, coef_id = 0;
			int data_status = 0;
			try
			{

				for (int i = 0; i < model["Id"].Count; i++)
				{
					Id = Convert.ToInt16(model["Id"][i]);
					data_status = Convert.ToInt32(model["data_status"][i]);
					decimal coefRelationHeat = Convert.ToDecimal(model["coefRelationHeat"][i]);
					decimal coefRelationVent = Convert.ToDecimal(model["coefRelationVent"][i]);

					for (coef_id = 8; coef_id < 10; coef_id++)
					{
						var coefHeatvent_upd = await _context.CoefBuildConsumer_Histories.Where(x => x.coef_id == coef_id && x.data_status == data_status && x.purpose_build_type_id == Id).FirstOrDefaultAsync();

						if (coefHeatvent_upd != null)
						{
							switch (coef_id)
							{
								case 8:
									coefHeatvent_upd.coef_value = coefRelationHeat;
									break;
								case 9:
									coefHeatvent_upd.coef_value = coefRelationVent;
									break;
							}
							coefHeatvent_upd.edit_date = DateTime.Now;
							coefHeatvent_upd.user_id = userId;
						}

						else
						{
							CoefBuildConsumer_History coefHeatvent_new = new();
							switch (coef_id)
							{
								case 8:
									coefHeatvent_new.coef_value = coefRelationHeat;
									break;
								case 9:
									coefHeatvent_new.coef_value = coefRelationVent;
									break;
							}

							coefHeatvent_new.purpose_build_type_id = Id;
							coefHeatvent_new.coef_id = coef_id;
							coefHeatvent_new.data_status = data_status;
							coefHeatvent_new.create_date = DateTime.Now;
							coefHeatvent_new.floors_id = -1;
							coefHeatvent_new.is_deleted = false;

							if (coefHeatvent_new.coef_value > 0)
							{
								await _context.AddAsync(coefHeatvent_new);
							}
						}

						await _context.SaveChangesAsync();
					} 
				}
					
			}
			catch(Exception ex)
			{
				_m_c.ExLog_Save("CoefHeatventHeatLoad_Save", $"data_status={data_status},coef_id={coef_id}", ex.Message, userId);

				return Json(new { success = false });
			}
			return Json(new { success = true });
		}

		public async Task<IActionResult> CoefEnergyEfficiency_Save(IFormCollection model)
		{
			int data_status = Convert.ToInt32(model["data_status"]), perspective = 0;
			short coef_id = 0, type_purpose_id = 0;
			try
			{
				//проходимся по коэффициентам 1 - на нужды отопления и вентиляции 2 - на нужды горячего водоснабжения
				for (coef_id = 1; coef_id < 3; coef_id++)
				{
					//проходимся по типу потребителя 1 - ж 2 - о 3 - 3 - п 4 - ижс
					for (type_purpose_id = 1; type_purpose_id < 5; type_purpose_id++)
					{
						//проходимся по перспективным годам
						for (int i = 0; i < model["perspective_year"].Count; i++)
						{
							perspective = Convert.ToInt32(model["perspective_year"][i]);
							var coefEnergy_upd = await _context.CoefHeatConsumption_Perspectives
								.Where(x => x.coef_id == coef_id && x.type_purpose_id == type_purpose_id && x.perspective_year == perspective && x.data_status == data_status)
								.FirstOrDefaultAsync();

							if (coefEnergy_upd != null)
							{
								CoefEnergyEfficiency_Update(model, coefEnergy_upd, coef_id, type_purpose_id, perspective, i);
							}

							else
							{
								await CoefEnergyEfficiency_Insert(model, data_status, coef_id, type_purpose_id, perspective, i);
							}
							await _context.SaveChangesAsync();
						} 
					}
					
				}
			}
			catch(Exception ex)
			{
				_m_c.ExLog_Save("CoefEnergyEfficiency_Save", $"data_status={data_status},coef_id={coef_id}, type_purpose_id={type_purpose_id}, perspective_year={perspective}", ex.Message, userId);

				return Json(new { success = false });
			}
			return Json(new { success = true });
		}

		public void CoefEnergyEfficiency_Update(IFormCollection model, CoefHeatConsumption_Perspective coefEnergy_upd, short coef_id , short type_purpose_id, int perspective, int i)
		{
			switch (coef_id)
			{
				case 1:
					//типы потребителей для coef_id = 1
					switch (type_purpose_id)
					{
						case 1:
							coefEnergy_upd.coef_value = Convert.ToDecimal(model["coef_value1_resident"][i]);
							break;
						case 2:
							coefEnergy_upd.coef_value = Convert.ToDecimal(model["coef_value1_public"][i]);
							break;
						case 3:
							coefEnergy_upd.coef_value = Convert.ToDecimal(model["coef_value1_industr"][i]);
							break;
						case 4:
							coefEnergy_upd.coef_value = Convert.ToDecimal(model["coef_value1_izhs"][i]);
							break;
					}
					break;
				case 2:
					//типы потребителей для coef_id = 2
					switch (type_purpose_id)
					{
						case 1:
							coefEnergy_upd.coef_value = Convert.ToDecimal(model["coef_value2_resident"][i]);
							break;
						case 2:
							coefEnergy_upd.coef_value = Convert.ToDecimal(model["coef_value2_public"][i]);
							break;
						case 3:
							coefEnergy_upd.coef_value = Convert.ToDecimal(model["coef_value2_industr"][i]);
							break;
						case 4:
							coefEnergy_upd.coef_value = Convert.ToDecimal(model["coef_value2_izhs"][i]);
							break;
					}
					break;
			}
			coefEnergy_upd.edit_date = DateTime.Now;
			coefEnergy_upd.user_id = userId;
		}

		public async Task CoefEnergyEfficiency_Insert(IFormCollection model, int data_status, short coef_id, short type_purpose_id, int perspective, int i)
		{
			CoefHeatConsumption_Perspective coefEnergy_new = new();
			switch (coef_id)
			{
				case 1:
					//типы потребителей для coef_id = 1
					switch (type_purpose_id)
					{
						case 1:
							coefEnergy_new.coef_value = Convert.ToDecimal(model["coef_value1_resident"][i]);
							break;
						case 2:
							coefEnergy_new.coef_value = Convert.ToDecimal(model["coef_value1_public"][i]);
							break;
						case 3:
							coefEnergy_new.coef_value = Convert.ToDecimal(model["coef_value1_industr"][i]);
							break;
						case 4:
							coefEnergy_new.coef_value = Convert.ToDecimal(model["coef_value1_izhs"][i]);
							break;
					}
					break;
				case 2:
					//типы потребителей для coef_id = 2
					switch (type_purpose_id)
					{
						case 1:
							coefEnergy_new.coef_value = Convert.ToDecimal(model["coef_value2_resident"][i]);
							break;
						case 2:
							coefEnergy_new.coef_value = Convert.ToDecimal(model["coef_value2_public"][i]);
							break;
						case 3:
							coefEnergy_new.coef_value = Convert.ToDecimal(model["coef_value2_industr"][i]);
							break;
						case 4:
							coefEnergy_new.coef_value = Convert.ToDecimal(model["coef_value2_izhs"][i]);
							break;
					}
					break;
			}

			coefEnergy_new.coef_id = coef_id;
			coefEnergy_new.type_purpose_id = type_purpose_id;
			coefEnergy_new.data_status = data_status;
			coefEnergy_new.perspective_year = perspective;
			coefEnergy_new.create_date = DateTime.Now;
			coefEnergy_new.is_deleted = false;
			coefEnergy_new.user_id = userId;
			if (coefEnergy_new.coef_value > 0)
				await _context.AddAsync(coefEnergy_new);
		}
		#endregion
	}
}
