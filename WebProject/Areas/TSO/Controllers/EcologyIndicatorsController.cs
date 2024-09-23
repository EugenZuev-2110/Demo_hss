using Microsoft.AspNetCore.Mvc;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace WebProject.Areas.TSO.Controllers
{
    [TypeFilter(typeof(ControllerActionFilter))]
    [Area("TSO")]
    public class EcologyIndicatorsController : Controller
    {
        private readonly ILogger<ObjectsInServiceController> _logger;
        private readonly HssDbContext _context;
        private readonly ApplicationDbContext _context2;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string? _user;
        private int userId;
        private string? userDisplayName;
        private readonly HSSController _m_c;

        public EcologyIndicatorsController(ILogger<ObjectsInServiceController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment,  HSSController c)
        {
            _logger = logger;
            _context = context;
            _context2 = context2;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;

            userId = userId;
            userDisplayName = userDisplayName;
            _m_c = c;
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

        public IActionResult EcologyIndicators()
        {
            return View();
        }

		#region ViewComponents
		//Тарифы и плата за подключение - тарифы
		public ActionResult OnGetCallEcologyIndicators_PartialViewComponent(int data_status, int perspective_year)
		{
			return ViewComponent("EcologyIndicators_Partial", new { data_status, perspective_year, userId });
		}
		#endregion
	}
}
