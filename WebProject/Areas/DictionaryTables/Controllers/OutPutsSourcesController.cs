using DocumentFormat.OpenXml.Office2010.Excel;
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
	public class OutPutsSourcesController : Controller
	{
		private readonly ILogger<TariffZoneController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		public OutPutsSourcesController(ILogger<TariffZoneController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult OutPutsSourcesList()
		{
			return View();
		}

		#region ViewComponents

		public ActionResult OnGetCallOutPutsSourcesList_PartialViewComponent()
		{
			return ViewComponent("OutPutsSourcesList_Partial", new { userId });
		}

		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenOutPutsSources(int id, int data_status, string action_for = "")
		{
			var _output = new OutPutsSourcesOnetViewModel();
			try
			{
				_output = (await _context.OutPutsSourcesOnetViewModels.FromSqlInterpolated($" exec sources.sp_GetOutPutsSources {id}, {data_status}").ToListAsync())
				.FirstOrDefault() ?? new OutPutsSourcesOnetViewModel();
				if (id == 0)
				{
					_output.data_status = data_status;
				}
				ViewBag.Action_for = action_for;
				if (action_for == "copy")
					_output.source_output_id = 0;
				else
					_output.source_output_id = id;
				ViewBag.SourcesList = await _context.fnt_GetSourcesByTZList(data_status, -1).ToArrayAsync();
				ViewBag.OutputList = await _context.S_Outputs.Select(x => new S_Outputs { source_output_id = x.source_output_id, unom_output = x.unom_output, output_name = x.output_name }).ToListAsync();

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenOutPutsSources", $"id={id}, data_status={data_status}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenOutPutsSources", _output);
		}
		#endregion

		#region Save_Data
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> OutPutSources_Save(OutPutsSourcesOnetViewModel model)
		{
			try
			{
			   var _output_upd = await _context.S_Outputs.Where(x => x.source_output_id == model.source_output_id).FirstOrDefaultAsync();
				string unom_output = "01"; int output_id = 0; bool is_new = false;
				if (_output_upd != null)
				{
					_output_upd.source_output_id = model.source_output_id;
					_output_upd.unom_output = model.unom_output;
					_output_upd.source_id = model.value_id;
					_output_upd.output_name = model.output_name;
					_output_upd.edit_date = DateTime.Now;
					_output_upd.user_id = userId;
					await _context.SaveChangesAsync();
				}
				else
				{
					S_Outputs _output_new = new S_Outputs();
					var last_unom = await _context.S_Outputs.OrderByDescending(x => x.source_output_id).Select(x => x.unom_output).FirstOrDefaultAsync();
					if(last_unom != null)
					{
						int last_unom_num = 0;
						int.TryParse(last_unom, out last_unom_num);
						last_unom_num = last_unom_num + 1;
						unom_output = _output_new.unom_output = last_unom_num.ToString();
					}
					_output_new.unom_output = unom_output;
					_output_new.output_name = model.output_name;
					_output_new.source_id = model.value_id;
					_output_new.create_date = DateTime.Now;
					_output_new.user_id = userId;

					await _context.AddAsync(_output_new);
					await _context.SaveChangesAsync();
					is_new = true;
				}
					 output_id = await _context.S_Outputs.OrderByDescending(x => x.source_output_id).Select(x => x.source_output_id).FirstOrDefaultAsync();

				return Json(new { success = true, output_id, unom_output, is_new});
			}
			catch(Exception ex)  
			{
				_m_c.ExLog_Save("OutPutSources_Save", $"id={model.source_output_id}, data_status={model.data_status}", ex.Message, userId);
				return Json(new { success = false }); 
			}
		}

		#endregion
	}
}
