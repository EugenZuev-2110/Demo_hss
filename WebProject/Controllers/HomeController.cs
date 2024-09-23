using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using WebProject.Data;
using System.Data;
using DataBase.Models;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using Microsoft.Data.SqlClient;
using WebProject.Filters;
using System.Net;

namespace WebProject.Controllers
{
    [TypeFilter(typeof(ControllerActionFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string? _user;
        private int userId;
        private bool IsAdmin;
        private string? userDisplayName;
        //private PrincipalContext _ctx = new PrincipalContext(ContextType.Domain);
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.User.Identity.Name;

            //_httpContextAccessor.HttpContext.Request.Headers["dbName"] = "UnomClaims";
            //_hostingEnvironment = hostingEnvironment;
            if (_user != null)
            {
                if (!_context.DictWinUsers.Any(x => x.UserLogin == _user))
                {
                    //добавление нового пользователя в базу из домена
                    PrincipalContext? _ctx = new(ContextType.Domain);
                    UserPrincipal user = UserPrincipal.FindByIdentity(_ctx, _user);

                    userDisplayName = user.DisplayName ?? "";
                    IsAdmin = false;

                    var new_user = new DictWinUsers()
                    {
                        UserLogin = _user ?? "",
                        UserName = userDisplayName,
                        IsNotExecutor = false,
                        IsAdmin = IsAdmin
                    };
                    _context.DictWinUsers.Add(new_user);
                    _context.SaveChanges();

                    userId = new_user.Id;
                }
                else
                {
                    var user = _context.DictWinUsers.Where(x => x.UserLogin == _user).FirstOrDefault();
                    userDisplayName = user.UserName;
                    userId = user.Id;
                    IsAdmin = user.IsAdmin;
                }
            }
            else
            {
                string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                if (host.Contains("localhost"))
                {
                    IsAdmin = true;
                    userId = 1;
                    userDisplayName = "Ермошин Виктор Анатольевич";
                }
                else
                {
                    IsAdmin = true;
                    userId = 34;
                }
            }
        }

        #region View
        public IActionResult Index()
        {
            //ar _dBContext = new ApplicationDbContext();
            //var a = _context.Unoms.Where(x => x.Id == 1).FirstOrDefault();

            //using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["IdentityDb"].ConnectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("GetUnomsList", sqlcon))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            //        {
            //            DataTable dt = new DataTable();

            //            da.Fill(dt);

            //            List<Unoms> unoms = dt.AsEnumerable().Select(m => new Unoms()
            //            {
            //                Id = m.Field<int>("Id"),
            //                unom_num = m.Field<string>("unom_num"),
            //                unom_date = m.Field<DateTime>("unom_date"),
            //                organisation = m.Field<string>("organisation"),
            //                unom_type = m.Field<int>("unom_type"),
            //            }).OrderBy(x => x.Id).ToList();

            //            return View(unoms);
            //        }
            //    }
            //}
            //string username = GetUserName(_httpContextAccessor);
            //var userId = this.User.Identity.Name;

            //UserPrincipal userPrincipal = UserPrincipal.Current;
            //String name = userPrincipal.DisplayName;

            //var user = ServiceSecurityContext.Current?.PrimaryIdentity;

            //Uri uri = new Uri("http://localhost:5117");
            //CookieContainer cookieContainer = new CookieContainer();
            //var cookieHeader = cookieContainer.GetCookieHeader(uri);
            //Cookie nameCookie = new Cookie("dbName", "UnomClaims");
            //cookieContainer.Add(uri, nameCookie);

            ViewData["IsAdmin"] = IsAdmin;
            return View();
        }

        //public static string GetUserFullName(string domain, string userName)
        //{
        //    DirectoryEntry userEntry = new DirectoryEntry("WinNT://" + domain + "/" + userName + ",User");
        //    return (string)userEntry.Properties["fullname"].Value;
        //}

        //public static string GetUserName(IHttpContextAccessor httpContextAccessor)
        //{
        //    string userName = string.Empty;
        //    if (httpContextAccessor != null &&
        //       httpContextAccessor.HttpContext != null &&
        //       httpContextAccessor.HttpContext.User != null &&
        //       httpContextAccessor.HttpContext.User.Identity != null)
        //    {
        //        userName = httpContextAccessor.HttpContext.User.Identity.Name;
        //        string[] usernames = userName.Split('\\');
        //        userName = usernames[1].ToUpper();
        //    }
        //    return userName;
        //}

        public async Task<IActionResult> Items(string? unom_num)
        {
            ViewData["unom_num"] = unom_num ?? string.Empty;
            ViewData["IsAdmin"] = IsAdmin;
            ViewData["userId"] = userId;
            ViewData["category_id"] = 0;

            if (!string.IsNullOrEmpty(unom_num))
            {
                //авто генерация пунктов из DM для категории Техприс
                if (await _context.Unoms.AnyAsync(x => x.unom_num == unom_num && x.category_id == 1))
                {
                    ViewData["category_id"] = 1;
                    var unom_num_Param = new SqlParameter("@unom_num", unom_num);
                    var userId_Param = new SqlParameter("@userId", userId);
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.sp_CreateNewItems @unom_num, @userId", unom_num_Param, userId_Param);
                }
            }

            return View();
        }
        public async Task<IActionResult> ObjectDescription(int itemId)
        {
            var item = await _context.Items.Where(x => x.Id == itemId).FirstOrDefaultAsync();
            var unom = await _context.Unoms.Where(x => x.Id == item.unom_id).Select(x => new { x.category_id, x.executor_id, x.unom_num, x.directory_link }).FirstOrDefaultAsync();

            var model = new ObjectDescriptionViewModel();
            model.ItsExecutorOrAdmin = IsAdmin || unom.executor_id == userId ? true : false;
            model.itemId = itemId;
            model.unom_category_id = unom.category_id;
            model.unom_num = unom.unom_num;

            string dir_link = _context.scale_str.FromSqlInterpolated($"select dbo.fn_GetDirLink ({unom.unom_num}) as scale_str").FirstOrDefault().scale_str;

            // второй способ вызова скалярной функции
            //string dir_link = (from p in _context.Unoms
            //        where p.unom_num == unom.unom_num
            //        select ApplicationDbContext.fn_GetDirLink(p.unom_num)).FirstOrDefault();

            model.directory_link = dir_link;
            model.object_type_id = item.object_type_id;
            model.object_id = item.object_id;

            //List<SourceViewModel> source = await _context.SourceViewModel.FromSqlInterpolated($"exec sp_GetSourceInfo {itemId}").ToListAsync();
            //if (source.Count > 0)
            //    model.source = source[0];
            //else
            //    model.source = new SourceViewModel();

            model.sections_id = await _context.SectionsCategoryMapps.Where(x => x.category_id == unom.category_id).Select(x => x.section_id).ToArrayAsync();

            model.obj_desc = new ObjectDescription();
            if (model.unom_category_id == 1)
            {
                model.object_address = item.conditional_address;
                ObjectDescription obj_desc = await _context.ObjectDescription.Where(x => x.item_id == itemId).FirstOrDefaultAsync();
                if (obj_desc != null)
                    model.obj_desc = obj_desc;
            }

            model.loads = new LoadsViewModel();
            if (model.sections_id.Contains(2))
            {
                List<LoadsViewModel> loads = await _context.LoadsViewModel.FromSqlInterpolated($"exec sp_GetConsumerInfo {itemId}").ToListAsync();
                if (loads.Count > 0)
                    model.loads = loads[0];
            }

            model.source = new SourceViewModel();
            if (model.sections_id.Contains(3))
            {
                List<SourceViewModel> source = await _context.SourceViewModel.FromSqlInterpolated($"exec sp_GetSourceInfo {itemId}").ToListAsync();
                if (source.Count > 0)
                    model.source = source[0];
            }

            model.hn_events = new HeatNetworksEvents();
            if (model.sections_id.Contains(4))
            {
                HeatNetworksEvents hn_events = await _context.HeatNetworksEvents.Where(x => x.item_id == itemId).FirstOrDefaultAsync();
                if (hn_events != null)
                    model.hn_events = hn_events;
            }

            model.decommissioning = new Decommissioning();
            if (model.sections_id.Contains(5))
            {
                Decommissioning decom = await _context.Decommissioning.Where(x => x.item_id == itemId).FirstOrDefaultAsync();
                if (decom != null)
                    model.decommissioning = decom;
            }

            model.ksio = new KSIO();
            if (model.sections_id.Contains(6))
            {
                List<KSIO> ksio = await _context.KSIO.FromSqlInterpolated($"exec sp_GetKSIOInfo {itemId}").ToListAsync();
                //KSIO ksio = await _context.KSIO.Where(x => x.item_id == itemId).FirstOrDefaultAsync();
                if (ksio.Count > 0)
                    model.ksio = ksio[0];
            }

            await _context.DisposeAsync();
            return View(model);
        }
        #endregion

        #region ViewComponents
        //список УНОМов
        public ActionResult OnGetCallUnomsListViewComponent(string searchText, string filter)
        {
            return ViewComponent("UnomsList", new { searchText, filter, userId });
        }
        //список Пунктов
        public ActionResult OnGetCallItemsListViewComponent(string searchText)
        {
            return ViewComponent("ItemsList", new { searchText });
        }
        #endregion

        #region OpenPopups
        //открытие попапа добавления или редактирования УНОМа
        public async Task<IActionResult> OpenUnom(int id)
        {
            var unom = new UnomViewModel();

            ViewData["IsAdmin"] = IsAdmin;

            if (id > 0)
            {
                //var tags = (from utm in _context.UnomTagsMapps
                //            join dict_tags in _context.DictUnomTags on utm.tag_id equals dict_tags.Id
                //            where utm.unom_id == id
                //            select new 
                //            {
                //                dict_tags.Id
                //            }).ToArray();

                unom = await _context.Unoms
                                .Where(x => x.Id == id)
                                .Select(i => new UnomViewModel()
                                {
                                    Id = i.Id,
                                    unom_num = i.unom_num,
                                    ets_project_number = i.ets_project_number,
                                    ets_date_dt = i.ets_date,
                                    ets_appeal_number = i.ets_appeal_number,
                                    dzhkh_date_dt = i.dzhkh_date,
                                    dzhkh_number = i.dzhkh_number,
                                    out_appeal_date_dt = i.out_appeal_date,
                                    out_appeal_number = i.out_appeal_number,
                                    appeal_desc_short = i.appeal_desc_short,
                                    result_review = i.result_review,
                                    executor_id = i.executor_id,
                                    category_id = i.category_id,
                                    tags = _context.UnomTagsMapps.Where(x => x.unom_id == id && x.state == 1).Select(x => x.tag_id).ToArray(),
                                    directory_link = i.directory_link,
                                    org_id = i.org_id,
                                    state_id = i.state_id,
                                    //data_status = i.data_status,
                                    changes_is_required = i.changes_is_required,
                                    changes_type = i.changes_type
                                })
                                .FirstOrDefaultAsync();
            }
            unom.tags_list = await _context.DictUnomTags.ToListAsync();
            ViewBag.UnomStateList = await _context.DictUnomStates.Select(x => new { x.Id, x.state }).ToListAsync();
            ViewBag.ExecutorsList = await _context.ExecutorsListView.Select(x => new { x.Id, x.executor_name, x.sort }).OrderByDescending(x => x.sort).ToListAsync();
            ViewBag.UnomCategoryList = await _context.CategoriesListView.Select(x => new { x.Id, x.category_name, x.sort }).OrderByDescending(x => x.sort).ToListAsync();
            //ViewBag.DataStatusesList = await _context.DataStatusesView.Select(x => new { x.DataStatus, x.Ds }).ToListAsync();
            //ViewBag.UnomTagsList = await _context.DictUnomTags.Select(x => new { x.Id, x.tag_name }).ToListAsync();
            ViewBag.OrganisationsList = await _context.DictOrganisation.Select(x => new { x.Id, org_name = x.short_name.Length > 0 ? x.org_name + " (" + x.short_name + ")" : x.org_name }).ToListAsync();
            ViewBag.EtsProjectsList = await _context.DictETSProjects.Select(x => new { x.Id, x.project_name, x.year }).OrderByDescending(x => x.year).ToListAsync();
            //ViewBag.SourceList = await _context.SourceListView.Select(x => new { x.Id, x.source_name }).ToListAsync();

            await _context.DisposeAsync();
            return PartialView(unom);
        }
        //открытие попапа добавления или редактирования Пункта
        public async Task<IActionResult> OpenItem(int id, string unom_num)
        {
            var item = new ItemViewModel();
            var unom = await _context.Unoms.Where(x => x.unom_num == unom_num).Select(x => new { x.category_id, x.executor_id }).FirstOrDefaultAsync();
            if (id > 0)
            {
                item = await _context.Items
                                .Where(x => x.Id == id)
                                .Select(i => new ItemViewModel()
                                {
                                    Id = i.Id,
                                    unom_num = unom_num ?? "",
                                    conditional_address = i.conditional_address,
                                    object_type_id = i.object_type_id,
                                    object_id = i.object_type_id != 2 ? i.object_id : null,
                                    source_id = i.source_id,
                                    district_id = i.district_id,
                                    ItsExecutorOrAdmin = (IsAdmin || unom.executor_id == userId) && unom.category_id > 1 ? true : false,
                                    category_id = unom.category_id
                                })
                                .FirstOrDefaultAsync();
            }
            else
            {
                item.unom_num = unom_num;
                if (unom.category_id == 2)
                {
                    item.object_type_id = 5;
                }
                else if (unom.category_id.In(3,5,7,8,9))
                {
                    item.object_type_id = 2;
                }
                item.ItsExecutorOrAdmin = (IsAdmin || unom.executor_id == userId) && unom.category_id > 1 ? true : false;
                item.category_id = unom.category_id;
            }

            //ViewBag.UnomList = await _context.Unoms.Select(x => new { x.Id, x.unom_num }).ToListAsync();
            ViewBag.DistrictsList = await _context.DictDistricts.Select(x => new { x.Id, x.district_name }).ToListAsync();
            ViewBag.SourcesList = await _context.SourceListView.Select(x => new { Id = x.Id, x.source_name }).ToListAsync();
            ViewBag.ObjectTypesList = await _context.ObjectTypesListView.Where(x => x.category_id == unom.category_id).Select(x => new { x.Id, x.object_type_name }).ToListAsync();
            if (unom.category_id == 2)
                ViewBag.KSIO_Names = _context.fnt_GetKSIOLocalNames(unom_num).ToList();

            await _context.DisposeAsync();
            return PartialView(item);
        }
        #endregion

        #region Updates
        //Сохранение информации по УНОМу
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnomUpdate(UnomViewModel model)
        {
            int new_unom_id = 0;
            //if (await _context.Unoms.AnyAsync(x => x.unom_num == model.unom_num.Trim() && x.Id != model.Id))
            //{
            //    return Json(new { success = false, error = "УНОМ с таким номером уже существует" });
            //}
            //else if (_context.Unoms.Any(x => x.appeal_number == model.appeal_number.Trim() && x.Id != model.Id))
            //{
            //    return Json(new { success = false, error = "Обращение с таким номером уже существует" });
            //}

            string dl = string.Empty;
            if (model.directory_link != null)
                dl = await GetDirLink(model.directory_link);

            DateTime ets_date_dt;
            if (!DateTime.TryParseExact(model.ets_date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ets_date_dt))
            {
                return Json(new { success = false, error = "Указана некорректная дата!" });
            }

            DateTime dzhkh_date_dt;
            if (!DateTime.TryParseExact(model.dzhkh_date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dzhkh_date_dt))
            {
                return Json(new { success = false, error = "Указана некорректная дата!" });
            }

            DateTime out_appeal_date_dt = DateTime.Now;
            if (model.out_appeal_date != null)
            {
                if (!DateTime.TryParseExact(model.out_appeal_date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out out_appeal_date_dt))
                {
                    return Json(new { success = false, error = "Указана некорректная дата!" });
                }
            }

            //обновление УНОМа
            if (await _context.Unoms.AnyAsync(x => x.Id == model.Id))
            {
                var unom_upd = await _context.Unoms.FirstOrDefaultAsync(x => x.Id == model.Id);
                //unom_upd.unom_num = model.unom_num;
                unom_upd.category_id = model.category_id;
                unom_upd.ets_project_number = model.ets_project_number;
                unom_upd.ets_date = ets_date_dt;
                unom_upd.ets_appeal_number = model.ets_appeal_number == "вх-" ? null : model.ets_appeal_number;
                unom_upd.dzhkh_date = dzhkh_date_dt;
                unom_upd.dzhkh_number = model.dzhkh_number == "01-01-" ? null : model.dzhkh_number;
                unom_upd.appeal_desc_short = model.appeal_desc_short;
                unom_upd.result_review = model.result_review;
                //unom_upd.changes_is_required = model.changes_is_required;
                //unom_upd.changes_type = model.changes_type;
                unom_upd.out_appeal_date = model.out_appeal_date != null ? out_appeal_date_dt : null;
                unom_upd.out_appeal_number = model.out_appeal_number == "исх-" ? null : model.out_appeal_number;
                unom_upd.executor_id = model.executor_id;
                unom_upd.org_id = model.org_id;
                //unom_upd.data_status = model.data_status;
                unom_upd.state_id = model.state_id;
                unom_upd.directory_link = !string.IsNullOrEmpty(dl) ? dl : model.directory_link;
                unom_upd.edit_date = DateTime.Now;
                unom_upd.user_id = userId;
            }
            //обновление УНОМа
            else
            {
                var new_unom_num = await GetNewUnomNumber();

                //добавление нового УНОМа
                var new_unom = new Unoms()
                {
                    unom_num = new_unom_num,
                    category_id = model.category_id,
                    ets_project_number = model.ets_project_number,
                    ets_date = ets_date_dt,
                    ets_appeal_number = model.ets_appeal_number == "вх-" ? null : model.ets_appeal_number,
                    dzhkh_date = dzhkh_date_dt,
                    dzhkh_number = model.dzhkh_number == "01-01-" ? null : model.dzhkh_number,
                    out_appeal_date = out_appeal_date_dt,
                    out_appeal_number = model.out_appeal_number == "исх-" ? null : model.out_appeal_number,
                    appeal_desc_short = model.appeal_desc_short,
                    result_review = model.result_review,
                    //changes_is_required = model.changes_is_required,
                    //changes_type = model.changes_type,
                    //data_status = model.data_status,
                    executor_id = model.executor_id,
                    org_id = model.org_id,
                    state_id = model.state_id,//model.state_id,
                    directory_link = !string.IsNullOrEmpty(dl) ? dl : model.directory_link,
                    //tag_id = model.tag_id,
                    create_date = DateTime.Now,
                    user_id = userId
                };
                await _context.Unoms.AddAsync(new_unom);
                await _context.SaveChangesAsync();
                new_unom_id = new_unom.Id;
                //добавление нового УНОМа
            }

            if (model.tags != null)
            {
                var tags_delete = await _context.UnomTagsMapps.Where(x => !model.tags.Any(y => y == x.tag_id) && x.unom_id == model.Id && model.Id > 0 && x.state == 1).ToListAsync();
                if (tags_delete.Count > 0)
                {
                    foreach (var unoms_tags in tags_delete)
                    {
                        unoms_tags.state = 0;
                        unoms_tags.edit_date = DateTime.Now;
                    }
                }

                foreach (var tag in model.tags)
                {
                    if (!_context.UnomTagsMapps.Any(x => x.unom_id == model.Id && x.tag_id == tag) || new_unom_id > 0)
                    {
                        var new_unom_tag = new UnomTagsMapps()
                        {
                            unom_id = model.Id > 0 ? model.Id : new_unom_id,
                            tag_id = tag,
                            create_date = DateTime.Now,
                            state = 1
                        };
                        await _context.UnomTagsMapps.AddAsync(new_unom_tag);
                    }
                    else if (await _context.UnomTagsMapps.AnyAsync(x => x.unom_id == model.Id && x.tag_id == tag && x.state == 0))
                    {
                        var unom_tag_upd = await _context.UnomTagsMapps.Where(x => x.unom_id == model.Id && x.tag_id == tag).FirstOrDefaultAsync();
                        unom_tag_upd.state = 1;
                        unom_tag_upd.edit_date = DateTime.Now;
                    }
                }
                
            }
            else if (model.tags == null && await _context.UnomTagsMapps.AnyAsync(x => x.unom_id == model.Id && model.Id > 0))
            {
                foreach (var unoms_tags in (await _context.UnomTagsMapps.Where(x => x.unom_id == model.Id).ToListAsync()))
                {
                    unoms_tags.state = 0;
                    unoms_tags.edit_date = DateTime.Now;
                }
            }
            await _context.SaveChangesAsync();
            await _context.DisposeAsync();
            return Json(new { success = true, model.unom_num, error = "" });
        }
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UnomUpdateFromItem(UnomInfoViewModel model)
        {
            if (await _context.Unoms.AnyAsync(x => x.Id == model.Id))
            {
                if (model.data_status == null)
                {
                    model.data_status = await _context.DataStatusesView.MaxAsync(x => x.DataStatus);
                }

                var unom_upd = await _context.Unoms.FirstOrDefaultAsync(x => x.Id == model.Id);
                unom_upd.changes_is_required = model.changes_is_required;
                unom_upd.changes_type = model.changes_type;
                unom_upd.is_appr_scheme_exist = model.is_appr_scheme_exist;
                unom_upd.data_status = model.data_status;
                unom_upd.layer_id = model.layer_id;
                unom_upd.edit_date = DateTime.Now;
                unom_upd.user_id = userId;

                if (!string.IsNullOrEmpty(model.accounted_events) || !string.IsNullOrEmpty(model.new_events) 
                        || !string.IsNullOrEmpty(model.tso_events) || !string.IsNullOrEmpty(model.reasons_mismath_events) || model.accordance_events is not null)
                {
                    if (await _context.UnomHNE.AnyAsync(x => x.unom_id == model.Id))
                    {
                        var hne_upd = await _context.UnomHNE.FirstOrDefaultAsync(x => x.unom_id == model.Id);
                        hne_upd.accounted_events = model.accounted_events;
                        hne_upd.new_events = model.new_events;
                        hne_upd.tso_events = model.tso_events;
                        hne_upd.reasons_mismath_events = model.accordance_events == false ? "-" : model.reasons_mismath_events;
                        hne_upd.edit_date = DateTime.Now;
                        hne_upd.user_id = userId;
                    }
                    else
                    {
                        var new_hne = new UnomHNE()
                        {
                            unom_id = model.Id,
                            accounted_events = model.accounted_events,
                            new_events = model.new_events,
                            tso_events = model.tso_events,
                            reasons_mismath_events = model.accordance_events == false ? "-" : model.reasons_mismath_events,
                            create_date = DateTime.Now,
                            user_id = userId
                        };
                        await _context.UnomHNE.AddAsync(new_hne);
                    }
                }
                else
                {
                    if (await _context.UnomHNE.AnyAsync(x => x.unom_id == model.Id))
                    {

                        var hne_upd = await _context.UnomHNE.FirstOrDefaultAsync(x => x.unom_id == model.Id);
                        hne_upd.accounted_events = model.accounted_events;
                        hne_upd.new_events = model.new_events;
                        hne_upd.tso_events = model.tso_events;
                        hne_upd.reasons_mismath_events = model.accordance_events == false ? "-" : model.reasons_mismath_events;
                        hne_upd.edit_date = DateTime.Now;
                        hne_upd.user_id = userId;
                    }
                }

                await _context.SaveChangesAsync();

                if (unom_upd.category_id == 1)
                {
                    //авто генерация пунктов из DM для категории Техприс
                    var unom_num_Param = new SqlParameter("@unom_num", unom_upd.unom_num);
                    var userId_Param = new SqlParameter("@userId", userId);
                    await _context.Database.ExecuteSqlRawAsync("exec dbo.sp_CreateNewItems @unom_num, @userId", unom_num_Param, userId_Param);
                }

                await _context.DisposeAsync();
            }

            return Json(new { success = true, error = "" });
        }

        //Сохранение информации по Пункту
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ItemUpdate(ItemViewModel model)
        {
            if (model.Id > 0)
            {
                if (await _context.Items.AnyAsync(x => x.Id == model.Id))
                {
                    var item_upd = await _context.Items.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    item_upd.conditional_address = model.conditional_address;
                    item_upd.object_type_id = model.object_type_id;
                    item_upd.object_id = model.object_type_id == 2 ? model.source_id.ToString() : model.object_id;
                    item_upd.source_id = model.object_type_id == 1 || model.object_type_id == 4 ? null : model.source_id;
                    item_upd.district_id = model.district_id;
                    item_upd.user_id = userId;
                    item_upd.edit_date = DateTime.Now;
                }
            }
            else
            {
                var unomId = await _context.Unoms.Where(x => x.unom_num == model.unom_num).Select(x => x.Id).FirstOrDefaultAsync();

                if (unomId > 0)
                {
                    var newItemNum = await _context.Items.Where(x => x.unom_id == unomId).CountAsync() + 1;

                    var newItem = new Items()
                    {
                        item_number = (byte)newItemNum,
                        unom_id = unomId,
                        district_id = model.district_id,
                        conditional_address = model.conditional_address,
                        object_type_id = model.object_type_id,
                        object_id = model.object_type_id == 2 ? model.source_id.ToString() : model.object_id,
                        source_id = model.object_type_id == 1 || model.object_type_id == 4 ? null : model.source_id,
                        user_id = userId,
                        create_date = DateTime.Now
                    };
                    await _context.Items.AddAsync(newItem);
                }
                else
                {
                    return Json(new { success = false, error = "Указанного УНОМа не существует!" });
                }
                
            }

            await _context.SaveChangesAsync();
            await _context.DisposeAsync();
            return Json(new { success = true, model.unom_num, error = "" });
        }

        public async Task<IActionResult> DeleteItem(int id)
        {
            if (await _context.Items.AnyAsync(x => x.Id == id))
            {
                var item = await _context.Items.Where(x => x.Id == id).FirstOrDefaultAsync();

                var item_log = new Items_Logs()
                {
                    ItemId = item.Id,
                    unom_id = item.unom_id,
                    item_number = item.item_number,
                    conditional_address = item.conditional_address,
                    object_type_id = item.object_type_id,
                    object_id = item.object_id,
                    source_id = item.source_id,
                    district_id = item.district_id,
                    create_date = DateTime.Now,
                    user_id = userId
                };
                await _context.Items_Logs.AddAsync(item_log);

                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true, error = "" });
        }

        //Сохранение информации по третьему уровню
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ObjectDescriptionUpdate(ObjectDescriptionFullViewModel model)
        { 
            if (model.sections_id.Contains(3))
            {
                if (await _context.SourceDescription.AnyAsync(x => x.item_id == model.itemId))
                {
                    var item_sd = await _context.SourceDescription.Where(x => x.item_id == model.itemId).FirstOrDefaultAsync();
                    item_sd.source_num = model.source_num;
                    item_sd.source_name = model.source_name;
                    item_sd.source_address = model.source_address;
                    item_sd.eto_num = model.eto_num;
                    item_sd.eto_name = model.eto_name;
                    item_sd.hssn_num = model.hssn_num;
                    item_sd.q_reserve_heat = model.q_reserve_heat;
                    //item_sd.q_inst_electr = model.q_inst_electr;
                    item_sd.q_inst_heat = model.q_inst_heat;
                    //item_sd.q_avail_heat = model.q_avail_heat;
                    item_sd.q_netto_heat = model.q_netto_heat;
                    //item_sd.q_calc_load_heat = model.q_calc_load_heat;
                    item_sd.edit_date = DateTime.Now;
                    item_sd.user_id = userId;
                }
                else
                {
                    var new_item_sd = new SourceDescription()
                    {
                        item_id = model.itemId,
                        source_num = model.source_num,
                        source_name = model.source_name,
                        source_address = model.source_address,
                        eto_num = model.eto_num,
                        eto_name = model.eto_name,
                        hssn_num = model.hssn_num,
                        //q_inst_electr = model.q_inst_electr,
                        q_inst_heat = model.q_inst_heat,
                        //q_avail_heat = model.q_avail_heat,
                        q_netto_heat = model.q_netto_heat,
                        //q_calc_load_heat = model.q_calc_load_heat,
                        q_reserve_heat = model.q_reserve_heat,
                        create_date = DateTime.Now,
                        user_id = userId
                    };
                    await _context.SourceDescription.AddAsync(new_item_sd);
                }
            }
            if (model.sections_id.Contains(1) && model.unom_category_id == 1)
            {
                if (await _context.ObjectDescription.AnyAsync(x => x.item_id == model.itemId))
                {
                    var item_obj_desc = await _context.ObjectDescription.Where(x => x.item_id == model.itemId).FirstOrDefaultAsync();
                    //item_obj_desc.address = model.address;
                    item_obj_desc.region_name = model.region_name;
                    item_obj_desc.hp_name = model.hp_name;
                    item_obj_desc.project_num_ip = model.project_num_ip;
                    item_obj_desc.realiz_period_ip = model.realiz_period_ip;
                    item_obj_desc.unom_event = model.unom_event;
                    //item_obj_desc.eto_num = model.eto_num;
                    //item_obj_desc.eto_name = model.eto_name;
                    //item_obj_desc.hssn_num = model.hssn_num;
                    item_obj_desc.connect_point = model.connect_point;
                    item_obj_desc.sys_heat_network = model.sys_heat_network;
                    item_obj_desc.building_purpose = model.building_purpose;
                    item_obj_desc.fact_area = model.fact_area;
                    item_obj_desc.decl_area = model.decl_area;
                    item_obj_desc.exists_aip = model.exists_aip;
                    item_obj_desc.fact_start = model.fact_start;
                    item_obj_desc.edit_date = DateTime.Now;
                    item_obj_desc.user_id = userId;
                }
                else
                {
                    var new_item_obj_desc = new ObjectDescription()
                    {
                        item_id = model.itemId,
                        //address = model.address,
                        region_name = model.region_name,
                        hp_name = model.hp_name,
                        project_num_ip = model.project_num_ip,
                        realiz_period_ip = model.realiz_period_ip,
                        unom_event = model.unom_event,
                        //eto_num = model.eto_num,
                        //eto_name = model.eto_name,
                        //hssn_num = model.hssn_num,
                        connect_point = model.connect_point,
                        sys_heat_network = model.sys_heat_network,
                        building_purpose = model.building_purpose,
                        fact_area = model.fact_area,
                        decl_area = model.decl_area,
                        exists_aip = model.exists_aip,
                        fact_start = model.fact_start,
                        create_date = DateTime.Now,
                        user_id = userId
                    };
                    await _context.ObjectDescription.AddAsync(new_item_obj_desc);
                }
            }

            if (model.sections_id.Contains(2))
            {
                if (await _context.ConsumersHeatLoad.AnyAsync(x => x.item_id == model.itemId))
                {
                    var item_loads = await _context.ConsumersHeatLoad.Where(x => x.item_id == model.itemId).FirstOrDefaultAsync();
                    item_loads.d_vent = model.d_vent;
                    item_loads.d_sum_gvs_m = model.d_sum_gvs_m;
                    item_loads.d_sum_gvs_a = model.d_sum_gvs_a;
                    item_loads.d_gvs_m = model.d_gvs_m;
                    item_loads.d_gvs_a = model.d_gvs_a;
                    item_loads.d_cond = model.d_cond;
                    item_loads.d_heat = model.d_heat;
                    item_loads.d_other = model.d_other;
                    item_loads.d_heat_curtain = model.d_heat_curtain;
                    item_loads.d_spec_heat_vent = model.d_spec_heat_vent;
                    item_loads.d_spec_gvs_a = model.d_spec_gvs_a;
                    item_loads.d_spec_all = model.d_spec_all;
                    item_loads.c_vent = model.c_vent;
                    item_loads.c_sum_gvs_a = model.c_sum_gvs_a;
                    item_loads.c_gvs_a = model.c_gvs_a;
                    item_loads.c_heat = model.c_heat;
                    item_loads.c_tech = model.c_tech;
                    item_loads.c_spec_heat_vent = model.c_spec_heat_vent;
                    item_loads.c_spec_gvs_a = model.c_spec_gvs_a;
                    item_loads.c_spec_all = model.c_spec_all;
                    item_loads.spec_dev_gvs_a = model.spec_dev_gvs_a;
                    item_loads.spec_dev_heat_vent = model.spec_dev_heat_vent;
                    item_loads.spec_dev_sum = model.spec_dev_sum;
                    item_loads.edit_date = DateTime.Now;
                    item_loads.user_id = userId;
                }
                else
                {
                    var new_item_loads = new ConsumersHeatLoad()
                    {
                        item_id = model.itemId,
                        d_vent = model.d_vent,
                        d_sum_gvs_m = model.d_sum_gvs_m,
                        d_sum_gvs_a = model.d_sum_gvs_a,
                        d_gvs_m = model.d_gvs_m,
                        d_gvs_a = model.d_gvs_a,
                        d_cond = model.d_cond,
                        d_heat = model.d_heat,
                        d_other = model.d_other,
                        d_heat_curtain = model.d_heat_curtain,
                        d_spec_heat_vent = model.d_spec_heat_vent,
                        d_spec_gvs_a = model.d_spec_gvs_a,
                        d_spec_all = model.d_spec_all,
                        c_vent = model.c_vent,
                        c_sum_gvs_a = model.c_sum_gvs_a,
                        c_gvs_a = model.c_gvs_a,
                        c_heat = model.c_heat,
                        c_tech = model.c_tech,
                        c_spec_heat_vent = model.c_spec_heat_vent,
                        c_spec_gvs_a = model.c_spec_gvs_a,
                        c_spec_all = model.c_spec_all,
                        spec_dev_gvs_a = model.spec_dev_gvs_a,
                        spec_dev_heat_vent = model.spec_dev_heat_vent,
                        spec_dev_sum = model.spec_dev_sum,
                        create_date = DateTime.Now,
                        user_id = userId
                    };
                    await _context.ConsumersHeatLoad.AddAsync(new_item_loads);
                }
            }

            if (model.sections_id.Contains(4))
            {
                if (await _context.HeatNetworksEvents.AnyAsync(x => x.item_id == model.itemId))
                {
                    var item_hne = await _context.HeatNetworksEvents.Where(x => x.item_id == model.itemId).FirstOrDefaultAsync();
                    item_hne.is_appr_scheme_exist = model.is_appr_scheme_exist;
                    //item_hne.accounted_events = model.accounted_events;
                    //item_hne.new_events = model.new_events;
                    //item_hne.tso_events = model.tso_events;
                    item_hne.decl_start = model.decl_start;
                    //item_hne.reasons_mismath_events = model.reasons_mismath_events;
                    item_hne.bandwidth_reserve = model.bandwidth_reserve;
                    //item_hne.q_reserve_heat = model.q_reserve_heat;
                    item_hne.edit_date = DateTime.Now;
                    item_hne.user_id = userId;
                }
                else
                {
                    var new_item_hne = new HeatNetworksEvents()
                    {
                        item_id = model.itemId,
                        is_appr_scheme_exist = model.is_appr_scheme_exist,
                        //accounted_events = model.accounted_events,
                        //new_events = model.new_events,
                        //tso_events = model.tso_events,
                        decl_start = model.decl_start,
                        //reasons_mismath_events= model.reasons_mismath_events,
                        bandwidth_reserve = model.bandwidth_reserve,
                        //q_reserve_heat = model.q_reserve_heat,
                        create_date = DateTime.Now,
                        user_id = userId
                    };
                    await _context.HeatNetworksEvents.AddAsync(new_item_hne);
                }
            }
            
            if (model.sections_id.Contains(5))
            {
                if (await _context.Decommissioning.AnyAsync(x => x.item_id == model.itemId))
                {
                    var item_dec = await _context.Decommissioning.Where(x => x.item_id == model.itemId).FirstOrDefaultAsync();
                    item_dec.ch_scheme_is_no_need = model.ch_scheme_is_no_need;
                    item_dec.decom_need_pause = model.decom_need_pause;
                    item_dec.source_finance_events = model.source_finance_events;
                    item_dec.approved_list_events = model.approved_list_events;
                    item_dec.alter_var_need = model.alter_var_need;
                    item_dec.alter_var_events = model.alter_var_events;
                    item_dec.start_realiz_event = model.start_realiz_event;
                    item_dec.consumers_heat_pause = model.consumers_heat_pause;
                    item_dec.c_heat = model.c_heat;
                    item_dec.c_vent = model.c_vent;
                    item_dec.c_gvs_a = model.c_gvs_a;
                    item_dec.c_tech = model.c_tech;
                    item_dec.c_sum_gvs_a = model.c_sum_gvs_a;
                    item_dec.decom_equip_list = model.decom_equip_list;
                    item_dec.st_num_equip = model.st_num_equip;
                    item_dec.decom_date = model.decom_date;
                    item_dec.decom_q_inst_heat_equip = model.decom_q_inst_heat_equip;
                    item_dec.after_decom_q_inst_heat_equip = model.after_decom_q_inst_heat_equip;
                    item_dec.after_decom_q_netto_heat = model.after_decom_q_netto_heat;
                    item_dec.after_decom_q_reserve_heat = model.after_decom_q_reserve_heat;
                    item_dec.edit_date = DateTime.Now;
                    item_dec.user_id = userId;
                }
                else
                {
                    var new_item_dec = new Decommissioning()
                    {
                        item_id = model.itemId,
                        ch_scheme_is_no_need = model.ch_scheme_is_no_need,
                        decom_need_pause = model.decom_need_pause,
                        source_finance_events = model.source_finance_events,
                        approved_list_events = model.approved_list_events,
                        alter_var_need = model.alter_var_need,
                        alter_var_events = model.alter_var_events,
                        start_realiz_event = model.start_realiz_event,
                        consumers_heat_pause=model.consumers_heat_pause,
                        c_heat = model.c_heat,
                        c_vent = model.c_vent,
                        c_gvs_a = model.c_gvs_a,
                        c_tech = model.c_tech,
                        c_sum_gvs_a=model.c_sum_gvs_a,
                        decom_equip_list=model.decom_equip_list,
                        st_num_equip=model.st_num_equip,
                        decom_date=model.decom_date,
                        //q_inst_electr_equip=model.q_inst_electr_equip,
                        decom_q_inst_heat_equip = model.decom_q_inst_heat_equip,
                        after_decom_q_inst_heat_equip=model.after_decom_q_inst_heat_equip,
                        after_decom_q_netto_heat = model.after_decom_q_netto_heat,
                        after_decom_q_reserve_heat = model.after_decom_q_reserve_heat,
                        create_date = DateTime.Now,
                        user_id = userId
                    };
                    await _context.Decommissioning.AddAsync(new_item_dec);
                }
            }

            if (model.sections_id.Contains(6))
            {
                if (await _context.KSIO.AnyAsync(x => x.item_id == model.itemId))
                {
                    var item_dec = await _context.KSIO.Where(x => x.item_id == model.itemId).FirstOrDefaultAsync();
                    item_dec.objects_list_comments = model.objects_list_comments;
                    item_dec.others_comments = model.others_comments;
                    item_dec.dev_events_comments = model.dev_events_comments;
                    item_dec.heat_loads_comments = model.heat_loads_comments;
                    item_dec.exists_like_ksio = model.exists_like_ksio;
                    item_dec.ksio_is_agree = model.ksio_is_agree;
                    item_dec.exists_ksio = model.exists_ksio;
                    item_dec.date_ppt = model.date_ppt;
                    item_dec.num_ppt = model.num_ppt;
                    item_dec.date_ksio = model.date_ksio;
                    item_dec.num_ksio = model.num_ksio;
                    item_dec.source_num_ksio = model.source_num_ksio;
                    item_dec.source_name_ksio = model.source_name_ksio;
                    item_dec.source_address_ksio = model.source_address_ksio;
                    item_dec.eto_num_ksio = model.eto_num_ksio;
                    item_dec.eto_name_ksio= model.eto_name_ksio;
                    item_dec.hssn_num_ksio= model.hssn_num_ksio;
                    //item_dec.q_inst_electr_ksio = model.q_inst_electr_ksio;
                    item_dec.q_inst_heat_ksio = model.q_inst_heat_ksio;
                    //item_dec.q_avail_heat_ksio = model.q_avail_heat_ksio;
                    item_dec.q_netto_heat_ksio = model.q_netto_heat_ksio;
                    //item_dec.q_calc_load_heat_ksio = model.q_calc_load_heat_ksio;
                    item_dec.decl_area_ksio = model.decl_area_ksio;
                    item_dec.heat_sum = model.heat_sum;
                    item_dec.vent_sum = model.vent_sum;
                    item_dec.gvs_a_sum = model.gvs_a_sum;
                    item_dec.heat_curtain_sum = model.heat_curtain_sum;
                    item_dec.cond_sum = model.cond_sum;
                    item_dec.other_sum = model.other_sum;
                    item_dec.load_sum_gvs_a = model.load_sum_gvs_a;
                    item_dec.edit_date = DateTime.Now;
                    item_dec.user_id = userId;
                }
                else
                {
                    var item_ksio = new KSIO()
                    {
                        item_id = model.itemId,
                        objects_list_comments = model.objects_list_comments,
                        others_comments = model.others_comments,
                        dev_events_comments = model.dev_events_comments,
                        heat_loads_comments = model.heat_loads_comments,
                        exists_like_ksio = model.exists_like_ksio,
                        ksio_is_agree = model.ksio_is_agree,
                        exists_ksio = model.exists_ksio,
                        date_ppt = model.date_ppt,
                        num_ppt = model.num_ppt,
                        date_ksio = model.date_ksio,
                        num_ksio = model.num_ksio,
                        source_num_ksio = model.source_num_ksio,
                        source_name_ksio = model.source_name_ksio,
                        source_address_ksio = model.source_address_ksio,
                        eto_num_ksio = model.eto_num_ksio,
                        eto_name_ksio = model.eto_name_ksio,
                        hssn_num_ksio = model.hssn_num_ksio,
                        //q_inst_electr_ksio = model.q_inst_electr_ksio,
                        q_inst_heat_ksio = model.q_inst_heat_ksio,
                        //q_avail_heat_ksio=model.q_avail_heat_ksio,
                        q_netto_heat_ksio = model.q_netto_heat_ksio,
                        //q_calc_load_heat_ksio = model.q_calc_load_heat_ksio,
                        decl_area_ksio = model.decl_area_ksio,
                        heat_sum = model.heat_sum,
                        vent_sum = model.vent_sum,
                        gvs_a_sum = model.gvs_a_sum,
                        heat_curtain_sum = model.heat_curtain_sum,
                        cond_sum = model.cond_sum,
                        other_sum = model.other_sum,
                        load_sum_gvs_a = model.load_sum_gvs_a,
                        create_date = DateTime.Now,
                        user_id = userId
                    };
                    await _context.KSIO.AddAsync(item_ksio);
                }
            }

            await _context.SaveChangesAsync();

            if (model.sections_id.Contains(5))
            {
                var IdParam = new SqlParameter("@item_id", model.itemId);
                await _context.Database.ExecuteSqlRawAsync("exec dbo.sp_GetDecommissioningLoads @item_id", IdParam);
            }

            return Json(new { success = true, model.itemId, error = "" });
        }
        #endregion

        #region обновление справочников
        //Сохранение нового Тэга
        [HttpPost]
        public async Task<JsonResult> SaveNewTag(string new_tag)
        {
            if (!string.IsNullOrEmpty(new_tag))
            {
                new_tag = new_tag.Trim();
                if (_context.DictUnomTags.Any(x => x.tag_name == new_tag))
                {
                    return Json(new { error = "Такой тэг уже существует" });
                }
                var new_tag_add = new DictUnomTags()
                {
                    tag_name = new_tag
                };
                _context.DictUnomTags.Add(new_tag_add);
                await _context.SaveChangesAsync();
                int new_tag_id = new_tag_add.Id;
                await _context.DisposeAsync();

                return Json(new { error = "", new_tag_name = new_tag, new_tag_id });
            }

            return Json(new { error = "введите название" });
        }
        //Сохранение новой организации
        [HttpPost]
        public async Task<JsonResult> SaveNewOrg(string new_org)
        {
            if (!string.IsNullOrEmpty(new_org))
            {
                new_org = new_org.Trim();
                if (_context.DictOrganisation.Any(x => x.org_name == new_org))
                {
                    return Json(new { error = "Такая организация уже существует" });
                }
                var new_org_add = new DictOrganisation()
                {
                    org_name = new_org
                };
                _context.DictOrganisation.Add(new_org_add);
                await _context.SaveChangesAsync();
                int new_org_id = new_org_add.Id;
                await _context.DisposeAsync();

                return Json(new { error = "", new_org_name = new_org, new_org_id });
            }

            return Json(new { error = "введите название" });
        }
        #endregion

        #region вспомогательные функции
        //генерация пустых УНОМов
        //public async Task GenerateNewEmptyUnoms()
        //{
        //    for (int i = 0; i < 599; i++)
        //    {
        //        var new_unom_num = await GetNewUnomNumber();
        //        var new_unom = new Unoms()
        //        {
        //            unom_num = new_unom_num,
        //            create_date = DateTime.Now,
        //            user_id = userId
        //        };
        //        await _context.Unoms.AddAsync(new_unom);
        //        await _context.SaveChangesAsync();
        //    }
        //    await _context.DisposeAsync();
        //    return;
        //}

        //отображение имени авторизованного пользователя
        [HttpPost]
        public JsonResult UserName()
        {
            return Json(new { userName = userDisplayName ?? "" });
        }
        //формирование нового номера УНОМ
        [NonAction]
        public async Task<string> GetNewUnomNumber()
        {
            //var cur_year1 = DateTime.Now.Year;
            var cur_year = DateTime.Now.ToString("yy");

            string new_unom_num = "1/" + cur_year;
            //if (cur_year == "22")
            //{
            //    new_unom_num = "530/" + cur_year;
            //}
            
            if (await _context.Unoms.AnyAsync())
            {
                var last_unom = await _context.Unoms.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                string[] splt_last_unom = last_unom.unom_num.Split('/');

                if (splt_last_unom.Length > 0)
                {
                    if (splt_last_unom[1] == cur_year)
                    {
                        int new_number = 0;
                        int.TryParse(splt_last_unom[0], out new_number);
                        new_unom_num = (new_number + 1).ToString() + "/" + cur_year;
                    }
                }
            }
            
            return new_unom_num;
        }

        //формирование правильной ссылки на директорию
        [NonAction]
        public async Task<string> GetDirLink(string dir_link)
        {
            string r = dir_link.Replace(@"/", @"\");
            r = r.Replace(@"\", @"\\");
            r = r.Replace(@"\\\\", @"\\");
            string[] splt = r.Split(@"\\");
            string dl = string.Empty;
            foreach (string s in splt)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    dl = dl + @"\\" + s;
                }
            }
            dl = @"\\" + dl;

            return dl;
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}