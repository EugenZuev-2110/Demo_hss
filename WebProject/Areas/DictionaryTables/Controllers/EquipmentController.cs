using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("DictionaryTables")]
	public class EquipmentController : Controller
	{
		private readonly ILogger<EquipmentController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		public EquipmentController(ILogger<EquipmentController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult SteamTurbineList()
		{
			return View();
		}


		#region ViewComponents

		public ActionResult OnGetSteamTurbineList_PartialViewComponent()
		{
			return ViewComponent("SteamTurbineList_Partial", new { userId });
		}
		public ActionResult OnGetGasTurbineList_PartialViewComponent()
		{
			return ViewComponent("GasTurbineList_Partial", new { userId });
		}
		public ActionResult OnGetSteamBoilersList_PartialViewComponent()
		{
			return ViewComponent("SteamBoilersList_Partial", new { userId });
		}
		public ActionResult OnGetHWBoilersList_PartialViewComponent()
		{
			return ViewComponent("HWBoilersList_Partial", new { userId });
		}
		public ActionResult OnGetPistonInstallationsList_PartialViewComponent()
		{
			return ViewComponent("PistonInstallationsList_Partial", new { userId });
		}
		public ActionResult OnGetROUList_PartialViewComponent()
		{
			return ViewComponent("ROUList_Partial", new { userId });
		}
		public ActionResult OnGetWaterHeaterList_PartialViewComponent()
		{
			return ViewComponent("WaterHeaterList_Partial", new { userId });
		}
		public ActionResult OnGetPumpsList_PartialViewComponent()
		{
			return ViewComponent("PumpsList_Partial", new { userId });
		}
		public ActionResult OnGetFiltersVPUList_PartialViewComponent()
		{
			return ViewComponent("FiltersVPUList_Partial", new { userId });
		}
		public ActionResult OnGetClarifierVPUList_PartialViewComponent()
		{
			return ViewComponent("ClarifierVPUList_Partial", new { userId });
		}
		public ActionResult OnGetReverseOsmosisInstalVPUList_PartialViewComponent()
		{
			return ViewComponent("ReverseOsmosisInstalVPUList_Partial", new { userId });
		}
		public ActionResult OnGetNanoFiltrationInstallVPUList_PartialViewComponent()
		{
			return ViewComponent("NanoFiltrationInstallVPUList_Partial", new { userId });
		}
		public ActionResult OnGetDeaeratorsList_PartialViewComponent()
		{
			return ViewComponent("DeaeratorsList_Partial", new { userId });
		}
		public ActionResult OnGetTanksVPUList_PartialViewComponent()
		{
			return ViewComponent("TanksVPUList_Partial", new { userId });
		}
		public ActionResult OnGetComplexonsVPUList_PartialViewComponent()
		{
			return ViewComponent("ComplexonsVPUList_Partial", new { userId });
		}
		public ActionResult OnGetSmokePipesList_PartialViewComponent()
		{
			return ViewComponent("SmokePipesList_Partial", new { userId });
		}
		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenPopupSteamTurbine(int id, string action_for = "")
		{
			var _steamTurbine = new SteamTurbineViewmodel();
			try
			{
				_steamTurbine = (await _context.Equipments.Select(x => new SteamTurbineViewmodel {equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, inst_electric_power = x.inst_electric_power, inst_heat_power = x.inst_heat_power, ihp_heat_selections = x.ihp_heat_selections, ihp_prod_selections = x.ihp_prod_selections, fresh_steam_pressure = x.fresh_steam_pressure, park_resources = x.park_resources, fresh_steam_temp = x.fresh_steam_temp})
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new SteamTurbineViewmodel();
				_steamTurbine.equip_type_id = 1;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new  Equipments{ equip_id =  x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 1).ToArrayAsync();
				if (action_for == "copy")
					_steamTurbine.equip_id = 0;
				else
					_steamTurbine.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupSteamTurbine", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenSteamTurbine", _steamTurbine);
		}

		public async Task<IActionResult> OpenPopupGasTurbine(int id, string action_for = "")
		{
			var _gasTurbine = new GasTurbineViewmodel();
			try
			{
				_gasTurbine = (await _context.Equipments.Select(x => new GasTurbineViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, inst_electric_power = x.inst_electric_power, park_resources = x.park_resources, norm_count_start = x.norm_count_start })
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new GasTurbineViewmodel();
				_gasTurbine.equip_type_id = 2;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 2).ToArrayAsync();
				if (action_for == "copy")
					_gasTurbine.equip_id = 0;
				else
					_gasTurbine.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupGasTurbine", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupGasTurbine", _gasTurbine);
		}

		public async Task<IActionResult> OpenPopupSteamBoiler(int id, string action_for = "")
		{
			var _steamBoiler = new SteamBoilersViewmodel();
			try
			{
				_steamBoiler = (await _context.Equipments.Select(x => new SteamBoilersViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, capacity = x.capacity, inst_heat_power = x.inst_heat_power, norm_service_life = x.norm_service_life, kpd = x.kpd, fresh_steam_pressure = x.fresh_steam_pressure, fresh_steam_temp = x.fresh_steam_temp })
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new SteamBoilersViewmodel();
				_steamBoiler.equip_type_id = 3;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 3).ToArrayAsync();
				if (action_for == "copy")
					_steamBoiler.equip_id = 0;
				else
					_steamBoiler.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupSteamBoiler", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupSteamBoiler", _steamBoiler);
		}

		public async Task<IActionResult> OpenPopupHWBoiler(int id, string action_for = "")
		{
			var _hwBoilers = new HWBoilersViewmodel();
			try
			{
				_hwBoilers = (await _context.Equipments.Select(x => new HWBoilersViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, inst_heat_power = x.inst_heat_power, max_temp_out = x.max_temp_out, norm_service_life = x.norm_service_life, net_water_consump_min = x.net_water_consump_min, net_water_consump_nom = x.net_water_consump_nom, net_water_consump_max = x.net_water_consump_max, kpd = x.kpd })
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new HWBoilersViewmodel();
				_hwBoilers.equip_type_id = 4;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 4).ToArrayAsync();
				if (action_for == "copy")
					_hwBoilers.equip_id = 0;
				else
					_hwBoilers.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupHWBoiler", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupHWBoiler", _hwBoilers);
		}

		public async Task<IActionResult> OpenPopupPistonInstallations(int id, string action_for = "")
		{
			var _piston = new PistonInstallationsViewmodel();
			try
			{
				_piston = (await _context.Equipments.Select(x => new PistonInstallationsViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, inst_electric_power = x.inst_electric_power, inst_heat_power = x.inst_heat_power, park_resources = x.park_resources})
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new PistonInstallationsViewmodel();
				_piston.equip_type_id = 5;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 5).ToArrayAsync();
				if (action_for == "copy")
					_piston.equip_id = 0;
				else
					_piston.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupPistonInstallations", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupPistonInstallations", _piston);
		}
		public async Task<IActionResult> OpenPopupROU(int id, string action_for = "")
		{
			var _rou = new ROUViewmodel();
			try
			{
				_rou = (await _context.Equipments.Select(x => new ROUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity, fresh_steam_pressure = x.fresh_steam_pressure, fresh_steam_temp = x.fresh_steam_temp })
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new ROUViewmodel();
				_rou.equip_type_id = 6;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 6).ToArrayAsync();
				if (action_for == "copy")
					_rou.equip_id = 0;
				else
					_rou.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupROU", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupROU", _rou);
		}

		public async Task<IActionResult> OpenPopupWaterHeater(int id, string action_for = "")
		{
			var _waterHeater = new WaterHeaterOneDataViewmodel();
			try
			{
				_waterHeater = (await _context.Equipments.Select(x => new WaterHeaterOneDataViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, htexch_type_id = x.htexch_type_id, heat_exchange_surface = x.heat_exchange_surface, section_count = x.section_count, casing_diameter = x.casing_diameter, inst_heat_power = x.inst_heat_power, length_section = x.length_section, max_temp_out = x.max_temp_out, net_water_consump_nom = x.net_water_consump_nom, net_water_consump_max = x.net_water_consump_max})
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new WaterHeaterOneDataViewmodel();
				_waterHeater.equip_type_id = 7;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 7).ToArrayAsync();
				if (action_for == "copy")
					_waterHeater.equip_id = 0;
				else
					_waterHeater.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupWaterHeater", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupWaterHeater", _waterHeater);
		}

		public async Task<IActionResult> OpenPopupPumps(int id, string action_for = "")
		{
			var _pumps = new PumpsViewmodel();
			try
			{
				_pumps = (await _context.Equipments.Select(x => new PumpsViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, manufacturer = x.manufacturer, capacity = x.capacity, pump_press = x.pump_press})
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new PumpsViewmodel();
				_pumps.equip_type_id = 8;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 8).ToArrayAsync();
				if (action_for == "copy")
					_pumps.equip_id = 0;
				else
					_pumps.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupPumps", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupPumps", _pumps);
		}

		public async Task<IActionResult> OpenPopupFiltersVPU(int id, string action_for = "")
		{
			var _filtersVPU = new FiltersVPUViewmodel();
			try
			{
				_filtersVPU = (await _context.Equipments.Select(x => new FiltersVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity, diameter = x.diameter})
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new FiltersVPUViewmodel();
				_filtersVPU.equip_type_id = 9;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 9).ToArrayAsync();
				if (action_for == "copy")
					_filtersVPU.equip_id = 0;
				else
					_filtersVPU.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupFiltersVPU", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupFiltersVPU", _filtersVPU);
		}

		public async Task<IActionResult> OpenPopupClarifierVPU(int id, string action_for = "")
		{
			var _сlarifierVPU = new ClarifierVPUViewmodel();
			try
			{
				_сlarifierVPU = (await _context.Equipments.Select(x => new ClarifierVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity, volume = x.volume, diameter = x.diameter})
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new ClarifierVPUViewmodel();
				_сlarifierVPU.equip_type_id = 10;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 10).ToArrayAsync();
				if (action_for == "copy")
					_сlarifierVPU.equip_id = 0;
				else
					_сlarifierVPU.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupClarifierVPU", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupClarifierVPU", _сlarifierVPU);
		}
		public async Task<IActionResult> OpenPopupReverseOsmosisInstalVPU(int id, string action_for = "")
		{
			var _reverseOsmosis = new ReverseOsmosisInstalVPUViewmodel();
			try
			{
				_reverseOsmosis = (await _context.Equipments.Select(x => new ReverseOsmosisInstalVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity})
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new ReverseOsmosisInstalVPUViewmodel();
				_reverseOsmosis.equip_type_id = 11;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 11).ToArrayAsync();
				if (action_for == "copy")
					_reverseOsmosis.equip_id = 0;
				else
					_reverseOsmosis.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupReverseOsmosisInstalVPU", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupReverseOsmosisInstalVPU", _reverseOsmosis);
		}

		public async Task<IActionResult> OpenPopupNanoFiltrationInstallVPU(int id, string action_for = "")
		{
			var _nanoFiltration = new ReverseOsmosisInstalVPUViewmodel();
			try
			{
				_nanoFiltration = (await _context.Equipments.Select(x => new ReverseOsmosisInstalVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity })
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new ReverseOsmosisInstalVPUViewmodel();
				_nanoFiltration.equip_type_id = 12;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 12).ToArrayAsync();
				if (action_for == "copy")
					_nanoFiltration.equip_id = 0;
				else
					_nanoFiltration.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupNanoFiltrationInstallVPU", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupNanoFiltrationInstallVPU", _nanoFiltration);
		}

		public async Task<IActionResult> OpenPopupDeaerators(int id, string action_for = "")
		{
			var _deaerators = new ReverseOsmosisInstalVPUViewmodel();
			try
			{
				_deaerators = (await _context.Equipments.Select(x => new ReverseOsmosisInstalVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity })
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new ReverseOsmosisInstalVPUViewmodel();
				_deaerators.equip_type_id = 13;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 13).ToArrayAsync();
				if (action_for == "copy")
					_deaerators.equip_id = 0;
				else
					_deaerators.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupDeaerators", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupDeaerators", _deaerators);
		}

		public async Task<IActionResult> OpenPopupTanksVPU(int id, string action_for = "")
		{
			var _tanksVPU = new TanksVPUViewmodel();
			try
			{
				_tanksVPU = (await _context.Equipments.Select(x => new TanksVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, volume = x.volume})
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new TanksVPUViewmodel();
				_tanksVPU.equip_type_id = 14;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 14).ToArrayAsync();
				if (action_for == "copy")
					_tanksVPU.equip_id = 0;
				else
					_tanksVPU.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupTanksVPU", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupTanksVPU", _tanksVPU);
		}

		public async Task<IActionResult> OpenPopupComplexonsVPU(int id, string action_for = "")
		{
			var _complexons = new ReverseOsmosisInstalVPUViewmodel();
			try
			{
				_complexons = (await _context.Equipments.Select(x => new ReverseOsmosisInstalVPUViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, capacity = x.capacity })
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new ReverseOsmosisInstalVPUViewmodel();
				_complexons.equip_type_id = 15;
				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 15).ToArrayAsync();
				if (action_for == "copy")
					_complexons.equip_id = 0;
				else
					_complexons.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupComplexonsVPU", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupComplexonsVPU", _complexons);
		}

		public async Task<IActionResult> OpenPopupSmokePipes(int id, string action_for = "")
		{
			var _smokePipes = new SmokePipesViewmodel();
			try
			{
				_smokePipes = (await _context.Equipments.Select(x => new SmokePipesViewmodel { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id, mark = x.mark, pipe_heigh_ground_lvl = x.pipe_heigh_ground_lvl, diameter = x.diameter})
					.Where(x => x.equip_id == id).ToListAsync())
				.FirstOrDefault() ?? new SmokePipesViewmodel();
				_smokePipes.equip_type_id = 16;

				ViewBag.Action_for = action_for;
				ViewBag.EquipList = await _context.Equipments.Select(x => new Equipments { equip_id = x.equip_id, unom_equip = x.unom_equip, equip_type_id = x.equip_type_id }).Where(x => x.equip_type_id == 16).ToArrayAsync();
				if (action_for == "copy")
					_smokePipes.equip_id = 0;
				else
					_smokePipes.equip_id = id;

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenPopupSmokePipes", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenPopupSmokePipes", _smokePipes);
		}
		#endregion

		#region Save_Data
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Equipment_Save(Equipments model)
		{
			try
			{
				var _equip_upd = await _context.Equipments.Where(x => x.equip_id == model.equip_id).FirstOrDefaultAsync();
				int equip_id = 0; bool is_new = false;
				string unom_equip = "", unom_type = "", count_unom_num = "";
				switch(model.equip_type_id)
				{
					case 1: 
						unom_equip = "Т000001";
						unom_type = "T";
						count_unom_num = "D6";
						break;
					case 2:
						unom_equip = "ГТ00001";
						unom_type = "ГT";
						break;
					case 3:
						unom_equip = "К000001";
						unom_type = "К";
						count_unom_num = "D6";
						break;
					case 4:
						unom_equip = "КВ00001";
						unom_type = "КВ";
						count_unom_num = "D5";
						break;
					case 5:
						unom_equip = "П000001";
						unom_type = "П";
						count_unom_num = "D6";
						break;
					case 6:
						unom_equip = "Р000001";
						unom_type = "Р";
						count_unom_num = "D6";
						break;
					case 7:
						unom_equip = "В000001";
						unom_type = "В";
						count_unom_num = "D6";
						break;
					case 8:
						unom_equip = "Н000001";
						unom_type = "Н";
						count_unom_num = "D6";
						break;
					case 9:
						unom_equip = "Ф000001";
						unom_type = "Ф";
						count_unom_num = "D6";
						break;
					case 10:
						unom_equip = "ОВ00001";
						unom_type = "ОВ";
						count_unom_num = "D5";
						break;
					case 11:
						unom_equip = "УО00001";
						unom_type = "У";
						count_unom_num = "D6";
						break;
					case 12:
						unom_equip = "УН00001";
						unom_type = "УН";
						count_unom_num = "D5";
						break;
					case 13:
						unom_equip = "Д000001";
						unom_type = "Д";
						count_unom_num = "D6";
						break;
					case 14:
						unom_equip = "Б000001";
						unom_type = "Б";
						count_unom_num = "D6";
						break;
					case 15:
						unom_equip = "КП00001";
						unom_type = "КП";
						count_unom_num = "D5";
						break;
					case 16:
						unom_equip = "ДТ00001";
						unom_type = "ДT";
						count_unom_num = "D5";
						break;
				}
				if (_equip_upd != null)
				{
					equip_id  = model.equip_id;
					_equip_upd.unom_equip = model.unom_equip;
					_equip_upd.mark = model.mark;
					_equip_upd.manufacturer = model.manufacturer;
					_equip_upd.inst_electric_power = model.inst_electric_power;
					_equip_upd.inst_heat_power = model.inst_heat_power;
					_equip_upd.capacity = model.capacity;
					_equip_upd.kpd = model.kpd;
					_equip_upd.ihp_heat_selections = model.ihp_heat_selections;
					_equip_upd.ihp_prod_selections = model.ihp_prod_selections;
					_equip_upd.fresh_steam_pressure = model.fresh_steam_pressure;
					_equip_upd.fresh_steam_temp = model.fresh_steam_temp;
					_equip_upd.max_temp_out = model.max_temp_out;
					_equip_upd.net_water_consump_min = model.net_water_consump_min;
					_equip_upd.net_water_consump_nom = model.net_water_consump_nom;
					_equip_upd.net_water_consump_max = model.net_water_consump_max;
					_equip_upd.pump_press = model.pump_press;
					_equip_upd.park_resources = model.park_resources;
					_equip_upd.norm_service_life = model.norm_service_life;
					_equip_upd.norm_count_start = model.norm_count_start;
					_equip_upd.volume = model.volume;
					_equip_upd.diameter = model.diameter;
					_equip_upd.pipe_heigh_ground_lvl = model.pipe_heigh_ground_lvl;
					_equip_upd.heat_exchange_surface = model.heat_exchange_surface;
					_equip_upd.section_count = model.section_count;
					_equip_upd.casing_diameter = model.casing_diameter;
					_equip_upd.length_section = model.length_section;
					_equip_upd.htexch_type_id = model.htexch_type_id;
					_equip_upd.edit_date = DateTime.Now;
					_equip_upd.user_id = userId;
					await _context.SaveChangesAsync();
				}
				else
				{
					Equipments _equip_new = new Equipments();

					var last_unom = await _context.Equipments.OrderByDescending(x => x.equip_id).Where(x => x.equip_type_id == model.equip_type_id).Select(x => x.unom_equip).FirstOrDefaultAsync();

					if (last_unom != null)
					{
						int last_unom_num = 0;
						int.TryParse(last_unom.Substring(1), out last_unom_num);
						last_unom_num = last_unom_num + 1;
						unom_equip = unom_type + last_unom_num.ToString(count_unom_num);

					}
					_equip_new.unom_equip = unom_equip;
					_equip_new.equip_type_id = model.equip_type_id;
					_equip_new.mark = model.mark;
					_equip_new.manufacturer = model.manufacturer;
					_equip_new.inst_electric_power = model.inst_electric_power;
					_equip_new.inst_heat_power = model.inst_heat_power;
					_equip_new.capacity = model.capacity;
					_equip_new.kpd = model.kpd;
					_equip_new.ihp_heat_selections = model.ihp_heat_selections;
					_equip_new.ihp_prod_selections = model.ihp_prod_selections;
					_equip_new.fresh_steam_pressure = model.fresh_steam_pressure;
					_equip_new.fresh_steam_temp = model.fresh_steam_temp;
					_equip_new.max_temp_out = model.max_temp_out;
					_equip_new.net_water_consump_min = model.net_water_consump_min;
					_equip_new.net_water_consump_nom = model.net_water_consump_nom;
					_equip_new.net_water_consump_max = model.net_water_consump_max;
					_equip_new.pump_press = model.pump_press;
					_equip_new.park_resources = model.park_resources;
					_equip_new.norm_service_life = model.norm_service_life;
					_equip_new.norm_count_start = model.norm_count_start;
					_equip_new.volume = model.volume;
					_equip_new.diameter = model.diameter;
					_equip_new.pipe_heigh_ground_lvl = model.pipe_heigh_ground_lvl;
					_equip_new.heat_exchange_surface = model.heat_exchange_surface;
					_equip_new.section_count = model.section_count;
					_equip_new.casing_diameter = model.casing_diameter;
					_equip_new.length_section = model.length_section;
					_equip_new.htexch_type_id = model.htexch_type_id;
					_equip_new.create_date = DateTime.Now;
					_equip_new.user_id = userId;

					await _context.AddAsync(_equip_new);
					await _context.SaveChangesAsync();

					is_new = true;
					equip_id = _equip_new.equip_id;
				}
				return Json(new { success = true, equip_id, is_new });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("Equipment_Save", $"id={model.equip_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
		#endregion
	}
}
