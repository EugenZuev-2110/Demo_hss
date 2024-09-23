using DataBase.Models.Sources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using static DataBase.Models.Sources.Dict_FuelTypes;

namespace WebProject.Areas.DictionaryTables.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("DictionaryTables")]
	public class FuelController : Controller
	{
		private readonly ILogger<FuelController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		public FuelController(ILogger<FuelController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult FuelList()
		{
			return View();
		}

		#region ViewComponents

		public ActionResult OnGetCallFuelList_PartialViewComponent()
		{
			return ViewComponent("FuelList_Partial", new { userId });
		}

		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenFuel(short id, string action_for = "")
		{
			var _fuel = new Dict_FuelTypes();
			try
			{
				_fuel = (await _context.Dict_FuelTypes.Where(x => x.Id == id).ToListAsync())
					.FirstOrDefault() ?? new Dict_FuelTypes();

				ViewBag.Action_for = action_for;
				if (action_for == "copy")
					_fuel.Id = 0;
				else
					_fuel.Id = id;
				ViewBag.Fuel = await _context.Dict_FuelTypes.Select(x => new Dict_FuelTypes() {Id = x.Id, fuel_type_name = x.fuel_type_name }).ToListAsync();

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenFuel", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenFuel", _fuel);
		}
		#endregion


		#region Save_Data
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Fuel_Save(Dict_FuelTypes model)
		{
			try
			{
				var _fuel_upd = await _context.Dict_FuelTypes.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
				int fuel_id = 0; bool is_new = false;
				if (_fuel_upd != null)
				{
					fuel_id = _fuel_upd.Id = model.Id;
					_fuel_upd.fuel_type_name = model.fuel_type_name;
					_fuel_upd.fuel_type_short = model.fuel_type_short;
					_fuel_upd.calc_low_combus_ht = model.calc_low_combus_ht;
					await _context.SaveChangesAsync();
				}
				else
				{
					Dict_FuelTypes _fuel_new = new Dict_FuelTypes();

					_fuel_new.fuel_type_name = model.fuel_type_name;
					_fuel_new.fuel_type_short = model.fuel_type_short;
					_fuel_new.calc_low_combus_ht = model.calc_low_combus_ht;

					await _context.AddAsync(_fuel_new);
					await _context.SaveChangesAsync();

					is_new = true;
					fuel_id = await _context.Dict_FuelTypes.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
				}
				return Json(new { success = true, fuel_id, is_new });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("Fuel_Save", $"id={model.Id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
		#endregion
	}
}
