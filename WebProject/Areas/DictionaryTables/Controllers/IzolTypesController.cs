using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("DictionaryTables")]
	public class IzolTypesController : Controller
	{
		private readonly ILogger<IzolTypesController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		public IzolTypesController(ILogger<IzolTypesController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult IzolTypesList()
		{
			return View();
		}

		#region ViewComponents

		public ActionResult OnGetCallDict_IzolTypesList_PartialViewComponent()
		{
			return ViewComponent("Dict_IzolTypesList_Partial", new { userId });
		}

		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenIzolTypes(short id, string action_for = "")
		{
			var _izoltype = new Dict_IzolTypes();
			try
			{
				_izoltype = (await _context.Dict_IzolTypes.Where(x => x.Id == id).ToListAsync())
					.FirstOrDefault() ?? new Dict_IzolTypes();

				ViewBag.Action_for = action_for;
				if (action_for == "copy")
					_izoltype.Id = 0;
				else
					_izoltype.Id = id;
				ViewBag.Dict_IzolTypesist = await _context.Dict_IzolTypes.Select(x => new Dict_IzolTypes {Id = x.Id, izol_type_name = x.izol_type_name }).ToListAsync();

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenIzolTypes", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenIzolTypes", _izoltype);
		}
		#endregion

		#region Save_Data
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> IzolTypes_Save(Dict_IzolTypes model)
		{
			try
			{
				var _izol_upd = await _context.Dict_IzolTypes.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
				int izol_id = 0; bool is_new = false;
				if (_izol_upd != null)
				{
					izol_id = _izol_upd.Id = model.Id;
					_izol_upd.izol_type_name = model.izol_type_name;
					_izol_upd.ht_conduct_coef = model.ht_conduct_coef;
					_izol_upd.ht_trasfer_coef = model.ht_trasfer_coef;
					await _context.SaveChangesAsync();
				}
				else
				{
					Dict_IzolTypes _izol_new = new Dict_IzolTypes();

					_izol_new.izol_type_name = model.izol_type_name;
					_izol_new.ht_conduct_coef = model.ht_conduct_coef;
					_izol_new.ht_trasfer_coef = model.ht_trasfer_coef;

					await _context.AddAsync(_izol_new);
					await _context.SaveChangesAsync();

					is_new = true;
					izol_id = await _context.Dict_IzolTypes.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
				}
				return Json(new { success = true, izol_id, is_new });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("IzolTypes_Save", $"id={model.Id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
		#endregion
	}
}
