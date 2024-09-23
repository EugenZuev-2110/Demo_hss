using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using WebProject.Models;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("DictionaryTables")]
	public class NormLossController : Controller
	{
		private readonly ILogger<NormLossController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		public NormLossController(ILogger<NormLossController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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
		public IActionResult NormLossList()
		{
			return View();
		}

		#region ViewComponents

		public ActionResult OnGetCallNormLossList_PartialViewComponent()
		{
			return ViewComponent("NormLossList_Partial", new { userId });
		}

		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenNormLoss(int id, int data_status, string action_for = "")
		{
			var _normLoss = new NormLossOneDataViewModel();
			try
			{
				_normLoss = (await _context.NormLossOneDataViewModels.FromSqlInterpolated($" exec networks.sp_GetNormLossOneData {id}, {data_status}").ToListAsync())
					.FirstOrDefault() ?? new NormLossOneDataViewModel();
				_normLoss.data_status = data_status;
				ViewBag.Action_for = action_for;
				if (action_for == "copy")
					_normLoss.Id = 0;
				else
					_normLoss.Id = id;
				ViewBag.NormLossList = await _context.fnt_GetUnomNormLossList(data_status, 0).ToArrayAsync();
				ViewBag.Diam = await _context.Dict_Diameters_Consumptions.Select(x => new Values_List { value_id = x.Id, value_name = x.cond_ht_net_diam.ToString() }).ToArrayAsync();
				ViewBag.Temp = await _context.TemperatureGraphics.Select(x => new Values_List { value_id = x.temp_graph_id, value_name = x.temp_graph_name}).ToArrayAsync();
				ViewBag.NetLayingTypes = await _context.Dict_NetLayingTypes.Select(x => new Values_List { value_id = x.Id, value_name = x.net_laying_type_name }).ToArrayAsync();
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenIzolTypes", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenNormLoss", _normLoss);
		}
		#endregion

		#region Save_Data
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NormLoss_Save(NormLossOneDataViewModel model)
		{
			try
			{
				var _normLoss_upd = await _context.Dict_NormLoss_History.Where(x => x.Id == model.Id && x.data_status == model.data_status).FirstOrDefaultAsync();
				int normloss_id = 0; bool is_new = false; string unom_normloss = "";
				if (_normLoss_upd != null)
				{
					normloss_id = _normLoss_upd.Id = model.Id;
					_normLoss_upd.data_status = model.data_status;
					_normLoss_upd.net_diam_id = model.net_diam_id;
					_normLoss_upd.temp_graph_id = model.temp_graph_id;
					_normLoss_upd.net_laying_type_id = model.net_laying_type_id;
					_normLoss_upd.norm_density = model.norm_density;
					is_new = true;
					await _context.SaveChangesAsync();
					unom_normloss = await _context.fnt_GetUnomNormLossList(_normLoss_upd.data_status, _normLoss_upd.Id).Select(x => x.value_name).FirstOrDefaultAsync();
				}
				else
				{
					Dict_NormLoss_History _normLoss_new = new Dict_NormLoss_History();
					var last_normloss = await _context.Dict_NormLoss_History.OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefaultAsync();
					if(last_normloss != null && last_normloss > 0)
					{
						last_normloss++;
					}
					normloss_id = _normLoss_new.Id = last_normloss ;
					_normLoss_new.data_status = model.data_status;
					_normLoss_new.net_diam_id = model.net_diam_id;
					_normLoss_new.temp_graph_id = model.temp_graph_id;
					_normLoss_new.net_laying_type_id = model.net_laying_type_id;
					_normLoss_new.norm_density = model.norm_density;

					await _context.AddAsync(_normLoss_new);
					await _context.SaveChangesAsync();

					is_new = true;
					normloss_id = last_normloss;
					
				}
				return Json(new { success = true, normloss_id, is_new, unom_normloss });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("NormLoss_Save", $"id={model.Id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
		#endregion
	}
}
