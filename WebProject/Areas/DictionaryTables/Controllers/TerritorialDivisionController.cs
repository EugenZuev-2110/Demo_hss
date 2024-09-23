using DataBase.Models;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebProject.Areas.DictionaryTables.Components.TerritorialDivisionClimatic_Partial;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Controllers
{
    [TypeFilter(typeof(ControllerActionFilter))]
    [Area("DictionaryTables")]
	public class TerritorialDivisionController : Controller
    {
        private readonly ILogger<TerritorialDivisionController> _logger;
        private readonly HssDbContext _context;
        private readonly ApplicationDbContext _context2;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string? _user;
        private int userId;
        private string? userDisplayName;
        private readonly HSSController _m_c;
        public DateTime time = DateTime.Now;

        public TerritorialDivisionController(ILogger<TerritorialDivisionController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
        {
            _logger = logger;
            _context = context;
            _context2 = context2;
            _httpContextAccessor = httpContextAccessor;
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

        public IActionResult TerritorialDivisionList()
		{
			ViewBag.Months = _context.Months.ToList();
			return View();
		}

        #region ViewComponents
        
        public ActionResult OnGetCallTerritorialDivisionList_PartialViewComponent(int? data_status, int? perspective_year)
        {
            return ViewComponent("TerritorialDivisionList_Partial", new { data_status, userId });
        }

		public ActionResult OnGetCallTerritorialDivisionClimatic_PartialViewComponent(int? data_status, int? perspective_year, int? distr_id)
		{
			return ViewComponent("TerritorialDivisionClimatic_Partial", new { data_status, userId, distr_id});
		}
		public ActionResult OnGetCallTerritorialDivision_PartialViewComponent(int? data_status, int? perspective_year, int? distr_id)
		{
			return ViewComponent("TerritorialDivisionGeneralInfo_Partial", new { data_status, userId, distr_id });
		}
		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenTerrDivision(int id, int data_status, string action_for = "")
		{
            var _terrDivisionViewModel = (await _context.TerritorialDivisionGeneralInfoDataOneViewModels.FromSqlInterpolated($"exec dictionary.GetTerritorialDivisionGeneralInfoDataOne {id},{data_status},{userId}")
                            .ToListAsync()).FirstOrDefault() ?? new TerritorialDivisionGeneralInfoDataOneViewModel();

			try
			{
                	_terrDivisionViewModel.data_status = data_status;
                ViewBag.DistrList = _context.fnt_GetDistrictList().ToList();
                ViewBag.Action_for = action_for;
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenTerrDivision", $"id={id},data_status={data_status}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenTerrDivision",_terrDivisionViewModel);
		}
		#endregion
		
        #region SAVE_DATA
		//Сохранение основной иформации по территориальному делению
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> TerritorialDivisionGeneralInfo_Save(TerritorialDivisionGeneralInfoDataOneViewModel model) 
        {
            try
            {
                var distr_upd = await _context.Districts.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
				string unom_distr = "Р1"; byte is_new_district = 0;
                if (distr_upd != null)
                {

                    distr_upd.district_num = model.district_num;//(short)(last_num + 1);
                    distr_upd.unom_district = model.unom_p;
                    
                    distr_upd.region_id = model.region_id;
                    distr_upd.Id = model.Id;
					unom_distr = model.unom_p + " (" + model.district_name + ")";
                   if(await _context.Districts.AnyAsync(u => u.district_name == model.district_name))
                    {
						return Json(new { success = false, message = "Ошибка, такой район уже существует" });
					}
                    else
                    {
						distr_upd.district_name = model.district_name;
					}
				}
                if (distr_upd == null)
                {
                    is_new_district = 1;
					var distr_new = new Districts();
					var last_num = await _context.Districts.Where(x => x.region_id == model.region_id).OrderByDescending(x => x.district_num).Select(x => x.district_num).FirstOrDefaultAsync();
                    var last_unom = await _context.Districts.OrderByDescending(x => x.Id).Select(x => x.unom_district).FirstOrDefaultAsync();

					distr_new.district_num = (short)(last_num + 1);

                    if (last_unom != null)
                    {
                        int last_unom_num = 0;
                        int.TryParse(last_unom.Substring(1), out last_unom_num);
                        last_unom_num = last_unom_num + 1;
						distr_new.unom_district = "Р" + last_unom_num;
                    }

					distr_new.district_name = model.district_name;
					distr_new.region_id = model.region_id;
					
					unom_distr = distr_new.unom_district + " (" + model.district_name + ")";
					await _context.Districts.AddAsync(distr_new);
					await _context.SaveChangesAsync();
				    model.Id = distr_new.Id;
					var climatic_upd = await _context.DistrictsValues_Histories.Where(x => x.district_id == model.Id && x.data_status == model.data_status).FirstOrDefaultAsync() ?? new DistrictsValues_History();
					var last_id = await _context.Districts.OrderByDescending(x=> x.Id).Select(x => x.Id).FirstOrDefaultAsync();

					if (climatic_upd.district_id == 0)
                    {
                        climatic_upd.district_id = last_id;
                        climatic_upd.data_status = model.data_status;
                        climatic_upd.layer_sys = model.layer_sys;
                        climatic_upd.layer_id = model.layer_id;
                        climatic_upd.is_deleted = false;
                        await _context.DistrictsValues_Histories.AddAsync(climatic_upd);
                    }
				}
				await _context.SaveChangesAsync();
                return Json(new { success = true, is_new_district, distr_id = model.Id, unom_distr  });
			}
			catch (Exception ex) 
            {
				_m_c.ExLog_Save("TerritorialDivisionGeneralInfo_Save", $"id={model.Id},data_status={model.data_status}", ex.Message, userId);
				return Json(new { success = false });
			}
			
		}

        //Сохранение основной иформации по территориальному делению
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TerritorialDivisionClimatic_Save(TerritorialDivisionClimaticDataOneViewModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    return Json(new { success = false, message = "Ошибка, не сохранены общие сведения" });
                }

                var climatic_upd = await _context.DistrictsValues_Histories.Where(x => x.district_id == model.Id && x.data_status == model.data_status).FirstOrDefaultAsync();
                if (climatic_upd != null)
                {

                    climatic_upd.calc_air_temp_ht_vent = model.calc_air_temp_ht_vent;
                    climatic_upd.aver_temp_coldest_month = model.aver_temp_coldest_month;
                    climatic_upd.aver_temp_tap_water_heat_period = model.aver_temp_tap_water_heat_period;
                    climatic_upd.aver_temp_tap_water_notheat_period = model.aver_temp_tap_water_notheat_period;
                    climatic_upd.calc_air_temp_after_calc_coldest = model.calc_air_temp_after_calc_coldest;
                    climatic_upd.aver_temp_heat_period = model.aver_temp_heat_period;
                    climatic_upd.fact_length_heat_period = model.fact_length_heat_period;
                    climatic_upd.length_heat_period = model.length_heat_period;
                    climatic_upd.coldest_month_id = model.coldest_month_id;
                    climatic_upd.length_year_prevent_off_system = model.length_year_prevent_off_system;
                    climatic_upd.is_deleted = false;
                    climatic_upd.edit_date = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
				_m_c.ExLog_Save("TerritorialDivisionClimatic_Save", $"id={model.Id},data_status={model.data_status}", ex.Message, userId);

				return Json(new { success = false, message = "Произошла ошибка." });
			}
		}

        public async Task<IActionResult> TerritorialDivisionPopulation_Save(int Id, int data_status, int[] perspective_year, int[] populate_size)
        {
            try
            {
				if (Id == 0)
				{
					return Json(new { success = false, message = "Ошибка, не сохранены общие сведения" });
				}
				for (int i = 0; i < perspective_year.Count(); i++)
                {
                    if (await _context.DistrictsValues_Perspectives.AnyAsync(x => x.district_id == Id && x.data_status == data_status && x.perspective_year == perspective_year[i]))
                    {
                        var population = await _context.DistrictsValues_Perspectives.Where(x => x.district_id == Id && x.data_status == data_status && x.perspective_year == perspective_year[i]).FirstOrDefaultAsync();
                        if (population != null)
                        {
                            population.district_id = Id;
                            population.data_status = population.data_status;
                            population.perspective_year = perspective_year[i];
                            try
                            {
                                population.populate_size = populate_size[i];
                            }
                            catch (Exception ex) { }
                            population.create_date = population.create_date;
                            population.edit_date = DateTime.Now;
                            population.user_id = userId;
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        var population = new DistrictsValues_Perspective();
						population.district_id = Id;
						population.data_status = data_status;
						population.perspective_year = perspective_year[i];
						try
						{
							population.populate_size = populate_size[i];
						}
						catch (Exception ex) { }
						population.create_date = DateTime.Now;
						population.edit_date = DateTime.Now;
						population.user_id = userId;
						await _context.AddAsync(population);
						await _context.SaveChangesAsync();
					}
                }
                    return Json(new { success = true });
                
            }
            catch (Exception ex)
            {
				_m_c.ExLog_Save("TerritorialDivisionPopulation_Save", $"id={Id},data_status={data_status}", ex.Message, userId);

				return Json(new { success = false, message = "Произошла ошибка." });
            }
        }

		#endregion

		public async Task<IActionResult> TerritorialDivision_UpdateValue(string? col_name, string? col_value, int? data_status, string? distr_unom)
		{
			try
            {
           var t = await _context.TerritorialDivisionUpdateValues.FromSqlInterpolated($"exec[dictionary].[UpdateDistrictsValues_History] {col_name}, {col_value}, {data_status}, {distr_unom}, {userId}").ToListAsync();
				return Json(new { success = true });
			}
            catch (Exception ex)
            {
				_m_c.ExLog_Save("TerritorialDivision_UpdateValue", $"col_name={col_name},col_value={col_value}, data_status={data_status}", ex.Message, userId);

				return Json(new { success = false });
			}
			
		}
	}
}

