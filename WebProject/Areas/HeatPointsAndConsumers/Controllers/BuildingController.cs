using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.HeatPointsAndConsumers.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("HeatPointsAndConsumers")]
	public class BuildingController : Controller
	{
		private readonly ILogger<BuildingController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		public BuildingController(ILogger<BuildingController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult BuildingList()
		{
			return View();
		}

		#region ViewComponents
		public ActionResult OnGetCallBuildingList_PartialViewComponent(int data_status)
		{
			return ViewComponent("BuildingList_Partial", new { data_status});
		}
		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenBuilding(int data_status, int building_id, string action_for = "")
		{
			if (data_status == 0)
			{
				data_status = _m_c.GetCurrentDS();
			}
			var _build = (await _context.BuildingOneDataViewModels.FromSqlInterpolated($"exec consumers.sp_GetBuildingOneData {data_status},{building_id}")
								.ToListAsync()).FirstOrDefault() ?? new BuildingOneDataViewModel();

			_build.data_status = data_status;
			_build.building_id = building_id;
			ViewBag.UnomBuildingList = await _context.fnt_GetBildingUnomList(data_status).ToListAsync();
			ViewBag.Distr = await _context.fnt_GetDistrictList().ToListAsync();
			ViewBag.Floor = await _context.Dict_Floors.Select(x => new Dict_Floors {Id = x.Id, floor_name = x.floor_name }).ToListAsync();
			return PartialView(_build);
		}
		#endregion
		
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> Building_Save(BuildingOneDataViewModel model)
		{
			int build_id = 0;
			bool is_new = false;
			string unom_build = "";
			var build_upd = await _context.Buildings.Where(x => x.building_id == model.building_id).FirstOrDefaultAsync();

			try
			{

				if (build_upd != null)
				{
					
					build_upd.district_id = model.district_id;
					build_upd.kadnum_zu = model.kadnum_zu;
					build_upd.kadnum_oks = model.kadnum_oks;
					build_upd.fias_build = model.fias_build;
					build_upd.fias_zu = model.fias_zu;
					build_upd.func_num_zone = model.func_num_zone;
					build_upd.bti_build_unom = model.bti_build_unom;
					build_upd.edit_date = DateTime.Now;
					build_upd.user_id = userId;

					var build_h_upd = await _context.buildings_Histories.Where(x => x.building_id == model.building_id && x.data_status == model.data_status).FirstOrDefaultAsync();

					if (build_h_upd != null)
					{
						build_h_upd.address = model.address;
						build_h_upd.floor_id = model.floor_id;
						build_h_upd.is_deleted = false;
						build_h_upd.edit_date = DateTime.Now;
						build_h_upd.user_id = userId;
					}
					else
					{
						build_h_upd = new Buildings_History();
						build_h_upd.address = model.address;
						build_h_upd.is_deleted = false;
						build_h_upd.create_date = DateTime.Now;
						build_h_upd.user_id = userId;
						await _context.AddAsync(build_h_upd);
					}
				
					build_id = build_h_upd.building_id;
					unom_build = build_id + " (" + build_h_upd.address + ")";
					await _context.SaveChangesAsync();
				}
				else
				{
					Buildings build_new = new Buildings();
					
					build_new.district_id = model.district_id;
					build_new.kadnum_zu = model.kadnum_zu;
					build_new.kadnum_oks = model.kadnum_oks;
					build_new.fias_build = model.fias_build;
					build_new.fias_zu = model.fias_zu;
					build_new.func_num_zone = model.func_num_zone;
					build_new.bti_build_unom = model.bti_build_unom;
					build_new.edit_date = DateTime.Now;
					build_new.user_id = userId;

					await _context.AddAsync(build_new);
					await _context.SaveChangesAsync();

					build_id = build_new.building_id;

					var build_h_new = new Buildings_History();

					build_h_new.building_id = build_id;
					build_h_new.address = model.address;
					build_h_new.floor_id = model.floor_id;
					build_h_new.data_status = model.data_status;
					build_h_new.is_deleted = false;
					build_h_new.create_date = DateTime.Now;
					build_h_new.user_id = userId;

					await _context.AddAsync(build_h_new);
					await _context.SaveChangesAsync();
					is_new = true;
					unom_build = build_id + " (" + build_h_new.address + ")";
				}

				return Json(new { success = true, is_new, model.data_status, build_id, unom_build });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("Building_Save", $"data_status={model.data_status} build_id={build_id}", ex.Message, userId);
				return Json(new { success = false });
			}
		}
	}
}
