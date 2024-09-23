using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
	public class DiametersConsumptionsController : Controller
	{
			private readonly ILogger<DiametersConsumptionsController> _logger;
			private readonly HssDbContext _context;
			private readonly ApplicationDbContext _context2;
			private readonly IHttpContextAccessor _httpContextAccessor;
			private readonly IWebHostEnvironment _hostingEnvironment;
			private readonly string? _user;
			private int userId;
			private string? userDisplayName;
			private readonly HSSController _m_c;

			public DiametersConsumptionsController(ILogger<DiametersConsumptionsController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

			public IActionResult DiametersConsumptionsList()
			{
				return View();
			}

			#region ViewComponents

			public ActionResult OnGetCallDiametersConsumptionsList_PartialViewComponent()
			{
				return ViewComponent("DiametersConsumptionsList_Partial", new { userId });
			}

			#endregion

			#region OpenPopups
			public async Task<IActionResult> OpenDiametersConsumptions(short id, string action_for = "")
			{
				var _diameter = new Dict_Diameters_Consumptions();
				try
				{
				_diameter = (await _context.Dict_Diameters_Consumptions.Where(x => x.Id == id).ToListAsync())
					.FirstOrDefault() ?? new Dict_Diameters_Consumptions();
					
					ViewBag.Action_for = action_for;
					if (action_for == "copy")
					_diameter.Id = 0;
					else
					_diameter.Id = id;
					ViewBag.DiametersConsumptionsList = await _context.Dict_Diameters_Consumptions.Select(x => new Dict_Diameters_Consumptions { Id = x.Id, cond_ht_net_diam = x.cond_ht_net_diam }).ToListAsync();
					
				}
				catch (Exception ex)
				{
					_m_c.ExLog_Save("OpenDiametersConsumptions", $"id={id}, action_for={action_for}", ex.Message, userId);
				}
				return PartialView("OpenDiametersConsumptions", _diameter);
			}
			#endregion

			#region Save_Data
			[ValidateAntiForgeryToken]
			public async Task<IActionResult> DiametersConsumptions_Save(Dict_Diameters_Consumptions model)
			{
				try
				{
				var _diam_upd = await _context.Dict_Diameters_Consumptions.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
				int diam_id = 0; bool is_new = false;
				if (_diam_upd != null)
				{
					diam_id = _diam_upd.Id = model.Id;
					_diam_upd.cond_ht_net_diam = model.cond_ht_net_diam;
					_diam_upd.ht_net_in_diam = model.ht_net_in_diam;
					_diam_upd.ht_net_out_diam = model.ht_net_out_diam;
					_diam_upd.consumption = model.consumption;
					await _context.SaveChangesAsync();
				}
				else
				{
					Dict_Diameters_Consumptions _diam_new = new Dict_Diameters_Consumptions();

					_diam_new.cond_ht_net_diam = model.cond_ht_net_diam;
					_diam_new.ht_net_in_diam = model.ht_net_in_diam;
					_diam_new.ht_net_out_diam = model.ht_net_out_diam;
					_diam_new.consumption = model.consumption;

					await _context.AddAsync(_diam_new);
					await _context.SaveChangesAsync();
					diam_id = await _context.Dict_Diameters_Consumptions.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
					is_new = true;
				}
				return Json(new { success = true, diam_id, is_new });
				}
				catch (Exception ex)
				{
					_m_c.ExLog_Save("DiametersConsumptions_Save", $"id={model.Id}", ex.Message, userId);
					return Json(new { success = false });
				}
			}
			#endregion
		}
	}

