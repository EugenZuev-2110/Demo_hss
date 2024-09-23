using DataBase.Models.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using WebProject.Areas.Events.Models;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.Events.Controllers
{
    [TypeFilter(typeof(ControllerActionFilter))]
    [Area("Events")]
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

        //страница мероприятий
        public async Task<IActionResult> Events()
        {
            ViewBag.YearsList = await _context.YearsView.Select(x => new { x.year }).OrderByDescending(x => x.year).ToListAsync();
            return View();
        }

        #region OpenPopups
        //открытие попапа добавления, редактирования или копирования МЕРОПРИЯТИЯ
        public async Task<IActionResult> OpenEvent(int id, string action_for, short object_type, short year)
        {
            var event_vm = new EventViewModel();
            if (id > 0)
            {
                if (object_type == 1)
                {
                    event_vm = await _context.Sources.Where(x => x.Id == id).Select(i => new EventViewModel()
                    {
                        object_type = object_type,
                        action_for = action_for,
                        year = i.year,
                        Id = i.Id,
                        unom = i.unom,
                        source_num = i.source_num,
                        ip_num = i.ip_num,
                        is_city_own = i.is_city_own,
                        equip_id = i.equip_id,
                        obj_code = i.obj_code,
                        purpose_code = i.purpose_code,
                        type_code = i.type_code,
                        sfinance_code = i.sfinance_code,
                        start_realize_year = i.start_realize_year,
                        end_realize_year = i.end_realize_year,
                        d_p_before = i.power_before,
                        d_p_after = i.power_after,
                        event_name = i.event_name,
                        cost_before = i.cost_before
                    }).FirstOrDefaultAsync();
                }
                else if (object_type == 2 || object_type == 3)
                {
                    event_vm = await _context.Networks.Where(x => x.Id == id).Select(i => new EventViewModel()
                    {
                        object_type = object_type,
                        action_for = action_for,
                        year = i.year,
                        Id = i.Id,
                        unom = i.unom,
                        ip_num = i.ip_num,
                        is_city_own = i.is_city_own,
                        sys = i.sys,
                        source_num = i.source_num,
                        tso_code = i.tso_code,
                        obj_code = i.obj_code,
                        purpose_code = i.purpose_code,
                        type_code = i.type_code,
                        sfinance_code = i.sfinance_code,
                        start_realize_year = i.start_realize_year,
                        end_realize_year = i.end_realize_year,
                        d_p_before = i.diameter_before,
                        d_p_after = i.diameter_after,
                        address_start = i.address_start,
                        address_end = i.address_end,
                        length_before = i.length_before,
                        length_after = i.length_after,
                        reg_id = i.reg_id,
                        year_of_laying = i.year_of_laying,
                        cap_invest = i.cap_invest
                    }).FirstOrDefaultAsync();
                }
                else if (object_type == 4)
                {
                    event_vm = await _context.ClosedScheme.Where(x => x.Id == id).Select(i => new EventViewModel()
                    {
                        object_type = object_type,
                        action_for = action_for,
                        year = i.year,
                        Id = i.Id,
                        unom = i.unom,
                        ip_num = i.ip_num,
                        is_city_own = i.is_city_own,
                        source_num = i.source_num,
                        tso_code = i.tso_code,
                        obj_code = i.obj_code,
                        purpose_code = i.purpose_code,
                        type_code = i.type_code,
                        sfinance_code = i.sfinance_code,
                        start_realize_year = i.start_realize_year,
                        end_realize_year = i.end_realize_year,
                        address_start = i.address_start,
                        address_end = i.address_end,
                        reg_id = i.reg_id,
                        event_name = i.event_name,
                        cap_invest = i.cap_invest
                    }).FirstOrDefaultAsync();
                }
            }
            else
            {
                event_vm.action_for = action_for;
                event_vm.object_type = object_type;
                event_vm.year = year;
            }
            ViewBag.SourcesList = await _context.SourceListView.Select(x => new { x.s_num, x.source_name }).ToListAsync();
            ViewBag.TSOList = await _context.TSOListView.Select(x => new { x.tso_code, x.org_name }).ToListAsync();
            ViewBag.ObjectsCodes = await _context.DictObjectsCodes.Select(x => new { x.Id, x.name }).ToListAsync();
            ViewBag.PurposeCodes = await _context.DictPurposeCodes.Select(x => new { x.Id, x.name }).ToListAsync();
            ViewBag.EventTypes = await _context.DictEventsTypes.Select(x => new { x.Id, x.name }).ToListAsync();
            ViewBag.SFinanceCodes = await _context.DictSFinanceCodes.Select(x => new { x.Id, x.name }).ToListAsync();
            ViewBag.DistrictsList = await _context.DictDistricts.Select(x => new { x.Id, x.district_name }).ToListAsync();

            await _context.DisposeAsync();
            return PartialView(event_vm);
        }
        #endregion

        #region ViewComponents
        //список мероприятий
        public ActionResult OnGetCallEventsListViewComponent(int year, int object_type)
        {
            return ViewComponent("EventsList", new { year, object_type });
        }
        #endregion

        //Сохранение мероприятия
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EventUpdate(EventViewModel model)
        {
            //if (!string.IsNullOrEmpty(model.ip_num))
            //{
            //    return Json(new { success = false, error = "Указанного Номера ИП не существует!" });
            //}

            //if (!ModelState.IsValid)
            //    return View(model);

            if (model.Id > 0 && model.action_for == "edit")
            {
                var new_unom_num = model.unom;
                if (model.object_type == 1)
                {
                    if (await _context.Sources.AnyAsync(x => x.Id == model.Id))
                    {
                        var unom_upd = await _context.Sources.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                        if (unom_upd.source_num != model.source_num 
                            || unom_upd.purpose_code != model.purpose_code 
                            || unom_upd.type_code != model.type_code 
                            || unom_upd.sfinance_code != model.sfinance_code)
                        {
                            new_unom_num = await GetNewUnomNumber(unom_upd.Id, model.year, model.object_type, model.tso_code, model.source_num, model.obj_code, model.purpose_code, model.type_code, model.sfinance_code);
                        }    

                        unom_upd.unom = new_unom_num;
                        unom_upd.source_num = model.source_num;
                        unom_upd.ip_num = model.ip_num;
                        unom_upd.equip_id = model.equip_id;
                        unom_upd.is_city_own = model.is_city_own;
                        //unom_upd.obj_code = model.obj_code;
                        unom_upd.purpose_code = model.purpose_code;
                        unom_upd.type_code = model.type_code;
                        unom_upd.sfinance_code = model.sfinance_code;
                        unom_upd.start_realize_year = model.start_realize_year;
                        unom_upd.end_realize_year = model.end_realize_year;
                        unom_upd.power_before = model.d_p_before;
                        unom_upd.power_after = model.d_p_after;
                        unom_upd.event_name = model.event_name;
                        unom_upd.cost_before = model.cost_before;
                        unom_upd.edit_date = DateTime.Now;
                        unom_upd.user_id = userId;
                    }
                }
                else if (model.object_type == 2 || model.object_type == 3)
                {
                    if (await _context.Networks.AnyAsync(x => x.Id == model.Id))
                    {
                        var unom_upd = await _context.Networks.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                        if (unom_upd.source_num != model.source_num 
                            || unom_upd.tso_code != model.tso_code 
                            || unom_upd.obj_code != model.obj_code
                            || (model.object_type == 2 && (unom_upd.purpose_code != model.purpose_code || unom_upd.type_code != model.type_code)) 
                            || unom_upd.sfinance_code != model.sfinance_code)
                        {
                            new_unom_num = await GetNewUnomNumber(unom_upd.Id, model.year, model.object_type, model.tso_code, model.source_num, model.obj_code, model.purpose_code, model.type_code, model.sfinance_code);
                        }

                        unom_upd.unom = new_unom_num;
                        unom_upd.ip_num = model.ip_num;
                        unom_upd.is_city_own = model.is_city_own;
                        unom_upd.sys = model.sys;
                        unom_upd.source_num = model.source_num;
                        unom_upd.tso_code = model.tso_code;
                        unom_upd.obj_code = model.obj_code;
                        unom_upd.purpose_code = model.object_type == 2 ? model.purpose_code : unom_upd.purpose_code;
                        unom_upd.type_code = model.object_type == 2 ? model.type_code : unom_upd.type_code;
                        unom_upd.sfinance_code = model.sfinance_code;
                        unom_upd.start_realize_year = model.start_realize_year;
                        unom_upd.end_realize_year = model.end_realize_year;
                        unom_upd.address_start = model.address_start;
                        unom_upd.address_end = model.address_end;
                        unom_upd.length_before = model.length_before;
                        unom_upd.length_after = model.length_after;
                        unom_upd.diameter_before = model.d_p_before;
                        unom_upd.diameter_after = model.d_p_after;
                        unom_upd.reg_id = model.reg_id;
                        unom_upd.year_of_laying = model.year_of_laying;
                        unom_upd.cap_invest = model.cap_invest;
                        unom_upd.edit_date = DateTime.Now;
                        unom_upd.user_id = userId;
                    }
                }
                else if (model.object_type == 4)
                {
                    if (await _context.ClosedScheme.AnyAsync(x => x.Id == model.Id))
                    {
                        var unom_upd = await _context.ClosedScheme.Where(x => x.Id == model.Id).FirstOrDefaultAsync();

                        if (unom_upd.source_num != model.source_num
                            || unom_upd.tso_code != model.tso_code
                            || unom_upd.obj_code != model.obj_code
                            || unom_upd.type_code != model.type_code
                            || unom_upd.sfinance_code != model.sfinance_code)
                        {
                            new_unom_num = await GetNewUnomNumber(unom_upd.Id, model.year, model.object_type, model.tso_code, model.source_num, model.obj_code, model.purpose_code, model.type_code, model.sfinance_code);
                        }

                        unom_upd.unom = new_unom_num;
                        unom_upd.ip_num = model.ip_num;
                        unom_upd.is_city_own = model.is_city_own;
                        unom_upd.source_num = model.source_num;
                        unom_upd.tso_code = model.tso_code;
                        unom_upd.obj_code = model.obj_code;
                        //unom_upd.purpose_code = model.purpose_code;
                        unom_upd.type_code = model.type_code;
                        unom_upd.sfinance_code = model.sfinance_code;
                        unom_upd.start_realize_year = model.start_realize_year;
                        unom_upd.end_realize_year = model.end_realize_year;
                        unom_upd.address_start = model.address_start;
                        unom_upd.address_end = model.address_end;
                        unom_upd.reg_id = model.reg_id;
                        unom_upd.event_name = model.event_name;
                        unom_upd.cap_invest = model.cap_invest;
                        unom_upd.edit_date = DateTime.Now;
                        unom_upd.user_id = userId;
                    }
                }
            }
            else
            {
                var new_unom_num = string.Empty;
                new_unom_num = await GetNewUnomNumber(0, model.year, model.object_type, model.tso_code, model.source_num, model.obj_code, model.purpose_code, model.type_code, model.sfinance_code);

                int last_num = 0;
                int.TryParse(new_unom_num.Split('.')[9], out last_num);
                
                if (model.object_type == 1)
                {
					var new_unom = new DataBase.Models.Events.Sources()
					{
                        year = model.year,
                        unom = new_unom_num,
                        last_num = last_num,
                        source_num = model.source_num,
                        ip_num = model.ip_num,
                        equip_id = model.equip_id,
                        is_city_own = model.is_city_own,
                        obj_code = 1,
                        purpose_code = model.purpose_code,
                        type_code = model.type_code,
                        sfinance_code = model.sfinance_code,
                        start_realize_year = model.start_realize_year,
                        end_realize_year = model.end_realize_year,
                        power_before = model.d_p_before,
                        power_after = model.d_p_after,
                        event_name = model.event_name,
                        cost_before = model.cost_before,
                        create_date = DateTime.Now,
                        status = 1,
                        user_id = userId
                    };
                    await _context.Sources.AddAsync(new_unom);
                }
                else if (model.object_type == 2 || model.object_type == 3)
                {
                    var new_unom = new Networks()
                    {
                        net_type = model.object_type,
                        year = model.year,
                        unom = new_unom_num,
                        last_num = last_num,
                        ip_num = model.ip_num,
                        is_city_own = model.is_city_own,
                        sys = model.sys,
                        source_num = model.source_num,
                        tso_code = model.tso_code,
                        obj_code = model.obj_code,
                        purpose_code = model.object_type == 2 ? model.purpose_code : 3,
                        type_code = model.object_type == 2 ? model.type_code : 2,
                        sfinance_code = model.sfinance_code,
                        start_realize_year = model.start_realize_year,
                        end_realize_year=model.end_realize_year,
                        address_start = model.address_start,
                        address_end = model.address_end,
                        length_before = model.length_before,
                        length_after = model.length_after,
                        diameter_before = model.d_p_before,
                        diameter_after = model.d_p_after,
                        reg_id = model.reg_id,
                        year_of_laying = model.year_of_laying,
                        cap_invest = model.cap_invest,
                        create_date = DateTime.Now,
                        status = 1,
                        user_id = userId
                    };
                    await _context.Networks.AddAsync(new_unom);
                }
                else if (model.object_type == 4)
                {
                    var new_unom = new ClosedScheme()
                    {
                        year = model.year,
                        unom = new_unom_num,
                        last_num = last_num,
                        ip_num = model.ip_num,
                        is_city_own = model.is_city_own,
                        source_num = model.source_num,
                        tso_code = model.tso_code,
                        obj_code = model.obj_code,
                        purpose_code = 6,
                        type_code = model.type_code,
                        sfinance_code = model.sfinance_code,
                        start_realize_year = model.start_realize_year,
                        end_realize_year = model.end_realize_year,
                        address_start = model.address_start,
                        address_end = model.address_end,
                        reg_id = model.reg_id,
                        event_name = model.event_name,
                        cap_invest = model.cap_invest,
                        create_date = DateTime.Now,
                        status = 1,
                        user_id = userId
                    };
                    await _context.ClosedScheme.AddAsync(new_unom);
                }
            }

            await _context.SaveChangesAsync();
            await _context.DisposeAsync();
            return Json(new { success = true, error = "" });
        }

        #region вспомогательные функции
        //отображение имени авторизованного пользователя
        [HttpPost]
        public JsonResult UserName()
        {
            return Json(new { userName = userDisplayName ?? "" });
        }
        //формирование нового номера УНОМ
        [NonAction]
        public async Task<string> GetNewUnomNumber(int id, short? year, short? object_type, int? tso_code, int? source_num, short? obj_code, short? purpose_code, short? type_code, short? sfinance_code)
        {
            string new_unom_num = string.Empty;

            var IdParam = new SqlParameter("@id", id);
            var YearParam = new SqlParameter("@year", year);
            var ObjTypeParam = new SqlParameter("@object_type", object_type);
            var TSOCodeParam = new SqlParameter("@tso_code", tso_code ?? -1);
            var SourceNumParam = new SqlParameter("@source_num", source_num);
            var ObjCodeParam = new SqlParameter("@obj_code", obj_code ?? -1);
            var PurposeCodeParam = new SqlParameter("@purpose_code", purpose_code ?? -1);
            var TypeCodeParam = new SqlParameter("@type_code", type_code ?? -1);
            var SFinanceCodeParam = new SqlParameter("@sfinance_code", sfinance_code);
            var OutParam = new SqlParameter("@new_unom_num", SqlDbType.VarChar, 50);
            OutParam.Direction = ParameterDirection.Output;
            await _context.Database.ExecuteSqlRawAsync("exec events.sp_GenerateNewUnomNUM @id, @year, " +
                "@object_type, @tso_code, @source_num, @obj_code, @purpose_code, @type_code, @sfinance_code, @new_unom_num out",
                IdParam,
                YearParam,
                ObjTypeParam,
                TSOCodeParam,
                SourceNumParam,
                ObjCodeParam,
                PurposeCodeParam,
                TypeCodeParam,
                SFinanceCodeParam,
                OutParam);

            new_unom_num = OutParam.Value.ToString();

            return new_unom_num;
        }
        #endregion
    }
}
