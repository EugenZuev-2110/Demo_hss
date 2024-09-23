using DataBaseHSS.Models;
using Microsoft.AspNetCore.Hosting;
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
	public class Dict_HPSchemesController : Controller
	{
		private readonly ILogger<Dict_HPSchemesController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		private readonly HttpClient _httpClient;

		public Dict_HPSchemesController(ILogger<Dict_HPSchemesController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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
			_httpClient = new HttpClient();
		}
		public IActionResult Dict_HPSchemesList()
		{
			return View();
		}

		#region ViewComponents

		public ActionResult OnGetCallHPSchemesList_PartialViewComponent()
		{
			return ViewComponent("Dict_HPSchemesList_Partial", new {userId });
		}

		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenHPSchemes(short id, string action_for = "")
		{
			var _hpschem = new Dict_HPSchemes();
			try
			{
				_hpschem = (await _context.Dict_HPSchemes.Where(x => x.scheme_id == id).ToListAsync())
					.FirstOrDefault() ?? new Dict_HPSchemes();

				ViewBag.Action_for = action_for;
				if (action_for == "copy")
					_hpschem.scheme_id = 0;
				else
					_hpschem.scheme_id = id;
				ViewBag.HPSchemes = await _context.Dict_HPSchemes.Select(x => x.scheme_id).ToListAsync();
				ViewBag.ConnectTypesHeat = await _context.Dict_ConnectTypesHeats.Select(x => new  { x.Id, x.heat_connect_name }).ToListAsync();
				ViewBag.ConnectTypesVent = await _context.Dict_ConnectTypesVents.Select(x => new  { x.Id, x.vent_connect_name }).ToListAsync();
				ViewBag.ConnectTypesHW = await _context.Dict_ConnectTypesHWs.Select(x => new  { x.Id, x.hw_connect_name }).ToListAsync();

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenHPSchemes", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenHPSchemes", _hpschem);
		}
		#endregion


		#region Save_Data
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> HPSchemes_Save(HPSchemesViewModel model)
		{
			try
			{
				var files = HttpContext.Request.Form.Files;
				var _hpSchemes_upd = await _context.Dict_HPSchemes.Where(x => x.scheme_id == model.scheme_id).FirstOrDefaultAsync();
				string webRootPath = _hostingEnvironment.WebRootPath;
				string upload = webRootPath + "/hss/images/HPSchemes/";
				string fileName = null;

				int scheme_id = 0;
				if (_hpSchemes_upd != null)
				{
					string extension = null;
					if (files.Count > 0)
					{
						extension = Path.GetExtension(files[0].FileName);
						fileName = Path.GetFileNameWithoutExtension(files[0].FileName);
						string url = upload + fileName + extension;

						var oldFile = Path.Combine(upload, _hpSchemes_upd.scheme_file_url);

						if (System.IO.File.Exists(oldFile))
						{
							System.IO.File.Delete(oldFile);
						}
						if (System.IO.File.Exists(url))
						{
							fileName += "_1";
						}
						fileName += extension;
						
						using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
						{
							files[0].CopyTo(fileStream);
						}
					}
					scheme_id = _hpSchemes_upd.scheme_id				= model.scheme_id;
					_hpSchemes_upd.heat_connect_id						= model.heat_connect_id;
					_hpSchemes_upd.vent_connect_id						= model.vent_connect_id;
					_hpSchemes_upd.hw_connect_id						= model.hw_connect_id;
					_hpSchemes_upd.avail_reg_temp_heat_vent				= model.avail_reg_temp_heat_vent;
					_hpSchemes_upd.avail_reg_temp_hw					= model.avail_reg_temp_hw;
					_hpSchemes_upd.avail_threeway_reg_temp_hw			= model.avail_threeway_reg_temp_hw;
					_hpSchemes_upd.avail_mix_block_hw					= model.avail_mix_block_hw;
					_hpSchemes_upd.avail_makeup_pump_heat_vent			= model.avail_makeup_pump_heat_vent;
					_hpSchemes_upd.avail_circul_pump_reverse_heat_vent	= model.avail_circul_pump_reverse_heat_vent;
					_hpSchemes_upd.avail_circul_pump_feed_heat_vent		= model.avail_circul_pump_feed_heat_vent;
					_hpSchemes_upd.avail_circul_pump_hw					= model.avail_circul_pump_hw;
					_hpSchemes_upd.avail_elevator_heat_vent				= model.avail_elevator_heat_vent;
					_hpSchemes_upd.avail_mix_pump_heat_vent				= model.avail_mix_pump_heat_vent;
					_hpSchemes_upd.avail_mix_pump_heat_vent				= model.avail_mix_pump_heat_vent;
					_hpSchemes_upd.avail_washer_feed_heat_vent			= model.avail_washer_feed_heat_vent;
					_hpSchemes_upd.avail_washer_reverse_heat_vent		= model.avail_washer_reverse_heat_vent;
					_hpSchemes_upd.avail_washer_feed_hw					= model.avail_washer_feed_hw;
					_hpSchemes_upd.edit_date							= DateTime.Now;
					_hpSchemes_upd.user_id								= userId;
					if(fileName != null )
					_hpSchemes_upd.scheme_file_url						= fileName;

					await _context.SaveChangesAsync();
					scheme_id = _hpSchemes_upd.scheme_id;
				}
				else
				{
					Dict_HPSchemes _hpSchemes_new = new Dict_HPSchemes();

					if (files.Count > 0)
					{
						string extension = Path.GetExtension(files[0].FileName);
						fileName = Path.GetFileNameWithoutExtension(files[0].FileName);
						string url = upload + fileName + extension;
						if (System.IO.File.Exists(url))
						{
							fileName += "_1";
						}
						fileName += extension;
						using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
						{
							files[0].CopyTo(fileStream);
						}
					}

					_hpSchemes_new.heat_connect_id						= model.heat_connect_id;
					_hpSchemes_new.vent_connect_id						= model.vent_connect_id;
					_hpSchemes_new.hw_connect_id						= model.hw_connect_id;
					_hpSchemes_new.avail_reg_temp_heat_vent				= model.avail_reg_temp_heat_vent;
					_hpSchemes_new.avail_reg_temp_hw					= model.avail_reg_temp_hw;
					_hpSchemes_new.avail_threeway_reg_temp_hw			= model.avail_threeway_reg_temp_hw;
					_hpSchemes_new.avail_mix_block_hw					= model.avail_mix_block_hw;
					_hpSchemes_new.avail_makeup_pump_heat_vent			= model.avail_makeup_pump_heat_vent;
					_hpSchemes_new.avail_circul_pump_reverse_heat_vent	= model.avail_circul_pump_reverse_heat_vent;
					_hpSchemes_new.avail_circul_pump_feed_heat_vent		= model.avail_circul_pump_feed_heat_vent;
					_hpSchemes_new.avail_circul_pump_hw					= model.avail_circul_pump_hw;
					_hpSchemes_new.avail_elevator_heat_vent				= model.avail_elevator_heat_vent;
					_hpSchemes_new.avail_mix_pump_heat_vent				= model.avail_mix_pump_heat_vent;
					_hpSchemes_new.avail_mix_pump_heat_vent				= model.avail_mix_pump_heat_vent;
					_hpSchemes_new.avail_washer_feed_heat_vent			= model.avail_washer_feed_heat_vent;
					_hpSchemes_new.avail_washer_reverse_heat_vent		= model.avail_washer_reverse_heat_vent;
					_hpSchemes_new.avail_washer_feed_hw					= model.avail_washer_feed_hw;
					_hpSchemes_new.create_date							= DateTime.Now;
					_hpSchemes_new.user_id								= userId;
					if (model.scheme_file_url == null)
					{
						_hpSchemes_new.scheme_file_url = fileName;
					}
					else
					{
						_hpSchemes_new.scheme_file_url = model.scheme_file_url;
					}
					await _context.AddAsync(_hpSchemes_new);
					await _context.SaveChangesAsync();

					scheme_id = _hpSchemes_new.scheme_id;
				}
				return Json(new { success = true, scheme_id});
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("HPSchemes_Save", $"id={model.scheme_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
		#endregion
	}
}
