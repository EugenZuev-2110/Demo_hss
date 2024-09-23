using DataBaseHSS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HeatPointsAndConsumers.Controllers;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.DictionaryTables.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("DictionaryTables")]
	public class CoefCorrectionClimateController : Controller
    {
		private readonly ILogger<CoefCorrectionClimateController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		public CoefCorrectionClimateController(ILogger<CoefCorrectionClimateController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult CoefCorrectionClimateList()
        {
            return View();
        }

		#region ViewComponents
		public ActionResult CoefCorrectionClimateList_PartialViewComponent(int data_status)
		{
			return ViewComponent("CoefCorrectionClimateList_Partial", new { data_status });
		}
		#endregion

		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> CoefCorrectionClimate_Save(IFormCollection model)
		{
			int coef_id = 0;
			try
			{
				for (int i = 0; i < model["coef_name_html"].Count; i++)
				{
					string name = "";
					for (int j = 0; j < model["perspective_year"].Count; j++)
					{
						name = "coef_value_" + model["perspective_year"][j].ToString();

						var t = model["coef_id"][i];
						var r = model["perspective_year"][j];
						var coef_upd = await _context.CoefCorrection_Perspective.Where(x => x.coef_id == Convert.ToInt16(model["coef_id"][i]) && x.perspective_year == Convert.ToInt32(model["perspective_year"][j]) && x.data_status == Convert.ToInt32(model["data_status"])).FirstOrDefaultAsync();

						if (coef_upd != null)
						{
							coef_upd.coef_value = model[name][i] == "" ? 1 : Convert.ToDecimal(model[name][i]);
							coef_upd.edit_date = DateTime.Now;
							coef_upd.user_id = userId;
							await _context.SaveChangesAsync();
						}
						else
						{
							CoefCorrection_Perspective coef_new = new();

							coef_new.coef_value = model[name][i] == "" ? 0 : Convert.ToDecimal(model[name][i]);
							coef_new.coef_id = Convert.ToInt16(model["coef_id"][i]);
							coef_new.data_status = Convert.ToInt32(model["data_status"]);
							coef_new.perspective_year = Convert.ToInt32(model["perspective_year"][j]);
							coef_new.create_date = DateTime.Now;
							coef_new.user_id = userId;
							await _context.AddAsync(coef_new);
							await _context.SaveChangesAsync();
							coef_id = coef_new.coef_id;
						}
					}
				}
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("CoefCorrectionClimate_Save", $"data_status={Convert.ToInt32(model["data_status"])} coef_id={coef_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
	}
}
