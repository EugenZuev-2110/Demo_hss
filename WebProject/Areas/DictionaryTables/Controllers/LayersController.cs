using DataBaseHSS.Models;
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
	public class LayersController : Controller
	{
		private readonly ILogger<LayersController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		public LayersController(ILogger<LayersController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult LayersList()
		{
			return View();
		}

		#region ViewComponents

		public ActionResult OnGetCallLayersList_PartialViewComponent(int? data_status)
		{
			return ViewComponent("LayersList_Partial", new {data_status, userId });
		}

		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenLayer(short id, string action_for = "")
		{
			var _layer = new Layers();
			try
			{
				_layer = (await _context.Layers.Where(x => x.Id == id).ToListAsync())
					.FirstOrDefault() ?? new Layers();

				ViewBag.Action_for = action_for;
				if (action_for == "copy")
					_layer.Id = 0;
				else
					_layer.Id = id;
				ViewBag.Layers = await _context.Layers.Select(x => new Layers { Id = x.Id, layer_unom = x.layer_unom }).ToListAsync();

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenLayer", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenLayer", _layer);
		}
		#endregion

		#region Save_Data
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Layers_Save(Layers model)
		{
			try
			{
				var _layer_upd = await _context.Layers.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
				int layer_id = 0; bool is_new = false; string layer_unom = "001";
				if (_layer_upd != null)
				{
					layer_id = _layer_upd.Id = model.Id;
					_layer_upd.layer_status_id = model.layer_status_id;
					_layer_upd.layer_type_id = model.layer_type_id;
					_layer_upd.layer_data_status = model.layer_data_status;
					_layer_upd.layer_perspective_year = model.layer_perspective_year;
					_layer_upd.layer_filename = model.layer_filename;
					_layer_upd.layer_filepath = model.layer_filepath;
					_layer_upd.layer_description = model.layer_description;
					_layer_upd.layer_delete_year = model.layer_delete_year;
					_layer_upd.layer_delete_reason = model.layer_delete_reason;
					_layer_upd.edit_date = DateTime.Now;
					_layer_upd.user_id = userId;
					await _context.SaveChangesAsync();
				}
				else
				{
				Layers _layer_new = new Layers();

					var last_unom = await _context.Layers.OrderByDescending(x => x.Id).Select(x => x.layer_unom).FirstOrDefaultAsync();

					if (last_unom != null)
					{
						int last_unom_num = 0;
						int.TryParse(last_unom.Substring(1), out last_unom_num);
						last_unom_num = last_unom_num + 1;
						if (last_unom_num < 10)
						{
							layer_unom = _layer_new.layer_unom = "00" + last_unom_num;
						}
						else
						{
							layer_unom = _layer_new.layer_unom = "0" + last_unom_num;
						}
					}
					_layer_new.layer_status_id = model.layer_status_id;
					_layer_new.layer_type_id = model.layer_type_id;
					_layer_new.layer_data_status = model.layer_data_status;
					_layer_new.layer_perspective_year = model.layer_perspective_year;
					_layer_new.layer_filename = model.layer_filename;
					_layer_new.layer_filepath = model.layer_filepath;
					_layer_new.layer_description = model.layer_description;
					_layer_new.layer_delete_year = model.layer_delete_year;
					_layer_new.layer_delete_reason = model.layer_delete_reason;
					_layer_new.create_date = DateTime.Now;
					_layer_new.user_id = userId;

					await _context.AddAsync(_layer_new);
					await _context.SaveChangesAsync();
					layer_id = _layer_new.Id;
					is_new = true;
				}
				return Json(new { success = true, layer_id, is_new });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("Layers_Save", $"id={model.Id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
		#endregion
	}
}
