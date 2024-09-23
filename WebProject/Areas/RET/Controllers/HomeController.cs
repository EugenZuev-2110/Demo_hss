using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.RET.Controllers
{
    [TypeFilter(typeof(ControllerActionFilter))]
    [Area("RET")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string? _user;
        private int userId;
        private string? userDisplayName;
        //private PrincipalContext _ctx = new PrincipalContext(ContextType.Domain);
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.User.Identity.Name;
            _hostingEnvironment = hostingEnvironment;
            if (_user != null)
            {
                var user = _context.DictWinUsers.Where(x => x.UserLogin == _user).FirstOrDefault();
                userDisplayName = user.UserName;
                userId = user.Id;
            }
        }
        public async Task<IActionResult> MainRET()
        {
            ViewBag.YearsList = await _context.DataStatusesView.Select(x => new { year = x.hss_id }).ToListAsync();
            //ViewBag.YearsList = await _context.YearsView.Select(x => new { x.year }).OrderByDescending(x => x.year).ToListAsync();
            ViewBag.LayersList = await _context.DictLayers.Where(x => x.is_perspective == true).Select(x => new { x.Id, x.layer_name }).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetLastData(int year, int layer_id)
        {
            var year_Param = new SqlParameter("@year", year);
            var layer_id_Param = new SqlParameter("@layer_id", layer_id);
            var OutParam = new SqlParameter("@last_dt_param", SqlDbType.VarChar, 20);
            OutParam.Direction = ParameterDirection.Output;
            var OutParam2 = new SqlParameter("@last_dt_uch_op", SqlDbType.VarChar, 20);
            OutParam2.Direction = ParameterDirection.Output;

            var OutParam_pir_ret_k = new SqlParameter("@exist_pir_ret_k", SqlDbType.Int);
            OutParam_pir_ret_k.Direction = ParameterDirection.Output;
            var OutParam_infl_k = new SqlParameter("@exist_infl_k", SqlDbType.Int);
            OutParam_infl_k.Direction = ParameterDirection.Output;
            var OutParam_klim_k = new SqlParameter("@exist_klim_k", SqlDbType.Int);
            OutParam_klim_k.Direction = ParameterDirection.Output;
            var OutParam_temp_k = new SqlParameter("@exist_temp_k", SqlDbType.Int);
            OutParam_temp_k.Direction = ParameterDirection.Output;
            var OutParam_smr_k = new SqlParameter("@exist_smr_k", SqlDbType.Int);
            OutParam_smr_k.Direction = ParameterDirection.Output;
            var OutParam_tz_c = new SqlParameter("@exist_tz_c", SqlDbType.Int);
            OutParam_tz_c.Direction = ParameterDirection.Output;
            var OutParam_tz_s = new SqlParameter("@exist_tz_s", SqlDbType.Int);
            OutParam_tz_s.Direction = ParameterDirection.Output;
            var OutParam_nloss_k = new SqlParameter("@exist_nloss_k", SqlDbType.Int);
            OutParam_nloss_k.Direction = ParameterDirection.Output;
            var OutParam_consumption = new SqlParameter("@exist_consumption", SqlDbType.Int);
            OutParam_consumption.Direction = ParameterDirection.Output;

            await _context.Database.ExecuteSqlRawAsync("exec ret.sp_GetLastData @year, @layer_id, @last_dt_param out, @last_dt_uch_op out, @exist_pir_ret_k out, " +
                "@exist_infl_k out, @exist_klim_k out, @exist_temp_k out, @exist_smr_k out, @exist_tz_c out, @exist_tz_s out, @exist_nloss_k out, @exist_consumption out", 
                year_Param, layer_id_Param, OutParam, OutParam2, OutParam_pir_ret_k, OutParam_infl_k, OutParam_klim_k, OutParam_temp_k, OutParam_smr_k, OutParam_tz_c, OutParam_tz_s,
                OutParam_nloss_k, OutParam_consumption);

            return Json(new { last_dt = OutParam.Value.ToString(), last_dt_uch_op = OutParam2.Value.ToString(), pir_ret_k = OutParam_pir_ret_k.Value.ToString(), 
                infl_k = OutParam_infl_k.Value.ToString(), klim_k = OutParam_klim_k.Value.ToString(), temp_k = OutParam_temp_k.Value.ToString(), 
                smr_k = OutParam_smr_k.Value.ToString(), tz_c = OutParam_tz_c.Value.ToString(), tz_s = OutParam_tz_s.Value.ToString(), nloss_k = OutParam_nloss_k.Value.ToString(),
                consumption_k = OutParam_consumption.Value.ToString()
            });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateRET(int year, int layer_id)
        {
            var year_Param = new SqlParameter("@year", year);
            var layer_id_Param = new SqlParameter("@layer_id", layer_id);

            await _context.Database.ExecuteSqlRawAsync("exec ret.sp_execHeatNetworkOP @year, @layer_id", year_Param, layer_id_Param);

            return Json(new { success = true });
        }

        //Общее действие для выгрузки шаблонов
        public async Task<ActionResult> DownloadKoefData(int? type_koef, int? is_empty, int? year)
        {
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;

            if (userId > 0 || host.Contains("localhost"))
            {
                if (host.Contains("localhost"))
                    host = "http://" + host;
                else
                    host = "https://" + host;

                var result = new string[2];

                if (type_koef > 0 && year > 0)
                {
                    if (is_empty == 0)
                    {
                        var template = await _context.DictTemplates.Where(x => x.type_id == type_koef).FirstOrDefaultAsync();
                        result[0] = "true";
                        result[1] = template.template_path + template.template_name;
                    }
                    //if (type_koef == 1)
                    //{
                        
                    //    result = await RetKoef_Export(startDate, endDate, host);
                    //}
                    
                    return Json(new { success = result[0], filename = result[1] });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return RedirectToAction("Home", "MainRET");
            }
        }

        //Общее действие для загрузки данных по шаблонам
        public async Task<ActionResult> UploadKoefData(int? type_koef, int? year)
        {
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;

            if (userId > 0 || host.Contains("localhost"))
            {
                if (host.Contains("localhost"))
                    host = "http://" + host;
                else
                    host = "https://" + host;

                var result = new string[2];

                if (type_koef > 0 && year > 0)
                {
                    
                    //if (type_koef == 1)
                    //{

                    //    result = await RetKoef_Export(startDate, endDate, host);
                    //}

                    return Json(new { success = result[0], filename = result[1] });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return RedirectToAction("Home", "MainRET");
            }
        }
    }
}
