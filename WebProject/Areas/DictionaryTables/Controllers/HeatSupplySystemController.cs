using DataBase.Models.Sources;
using DataBase.Models.TSO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("DictionaryTables")]
	public class HeatSupplySystemController : Controller
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

		public HeatSupplySystemController(ILogger<TariffZoneController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult HeatSupplySystemList()
		{
			return View();
		}

		#region ViewComponents

		public ActionResult OnGetCallHeatSupplySystemList_PartialViewComponent()
		{
			return ViewComponent("HeatSupplySystemList_Partial", new { userId });
		}

		#endregion


		#region OpenPopups
		public async Task<IActionResult> OpenHSS(int id, string action_for = "")
		{
			var _hss = new HeatSupplySystemOneDataViewModel();
			try
			{
				_hss = (await _context.HeatSupplySystemOneDataViewModels.FromSqlInterpolated($" exec sources.sp_GetHSS {id}").ToListAsync())
				.FirstOrDefault() ?? new HeatSupplySystemOneDataViewModel();
				_hss.districts = _context.Districts.Select(x => new District() { id = x.Id, distr_name = x.district_name }).ToList();
				_hss.HSSDistrictList = await _context.DistrictViewModals.FromSqlInterpolated($"select * from sources.fn_GetHssDistricts ({id})").ToListAsync();
				_hss.hss_distr = await _context.HSS_DistrictsMapps.Where(x => x.hss_id == id && x.is_deleted == false).Select(x => x.district_id).ToArrayAsync();
				ViewBag.Action_for = action_for;
				if (action_for == "copy")
					_hss.hss_id = 0;
				else
					_hss.hss_id = id;
				ViewBag.HSSList = await _context.HeatSupplySystems.Select(x => new { x.hss_id, x.unom_hss }).ToListAsync();
				ViewBag.LayerList = await _context.Layers.Select(x => new { x.Id, x.layer_unom }).ToListAsync();
				ViewBag.EtoList = await _context.ETO.Select(x => new {x.eto_id, x.unom_eto}).ToListAsync();

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenHSS", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenHeatSupplySystem", _hss);
		}
		#endregion

		#region Save_Data
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> HSS_Save(HeatSupplySystemOneDataViewModel model, int[] distr_id)
		{
			try
			{
				var hss_upd = await _context.HeatSupplySystems .Where(x => x.hss_id == model.hss_id).FirstOrDefaultAsync();
				string unom_hss = "001"; int hss_id = 0; bool is_new = false;
				if (hss_upd != null)
				{
					hss_upd.unom_hss = model.unom_hss;
					hss_id = hss_upd.hss_id = model.hss_id;
					hss_upd.eto_id = model.eto_id;
					hss_upd.is_liquidated = model.is_liquidated;
					hss_upd.year_liquidation = model.year_liquidation;
					hss_upd.reason_liquidation = model.reason_liquidation;
					hss_upd.edit_date = DateTime.Now;
					hss_upd.user_id = userId;

					for (int i = 0; i < distr_id.Length; i++)
					{
						var hss_district_list = await _context.HSS_DistrictsMapps.Where(x => x.hss_id == model.hss_id).ToListAsync();
						if (distr_id != null || (distr_id == null && hss_district_list.Any(x => x.is_deleted == false)))
						{
							if (hss_district_list.Count() > 0)
							{
								List<int> distr_delete;

								if (distr_id != null)
									distr_delete = hss_district_list.Where(x => !distr_id.Any(y => y == x.district_id) && x.is_deleted == false).Select(x => x.district_id).Distinct().ToList();
								else
									distr_delete = hss_district_list.Where(x => x.is_deleted == false).Select(x => x.district_id).Distinct().ToList();

								if (distr_delete.Count > 0)
								{
									foreach (var distr in distr_delete)
									{
										var hss_district = hss_district_list.Where(x => x.district_id == distr).OrderBy(x => x.district_id).LastOrDefault();
										if (hss_district.is_deleted == false)
										{
											if (hss_district.district_id == distr)
											{
												var distr_upd = await _context.HSS_DistrictsMapps.Where(x => x.hss_id == model.hss_id && x.district_id == distr).FirstOrDefaultAsync();
												distr_upd.is_deleted = true;
												distr_upd.edit_date = DateTime.Now;
											}
											else
											{
												var del_distr = GetNewHSS_DistrictMappsModel(model.hss_id, distr, true);
												await _context.AddAsync(del_distr);
											}
										}

									}
								}

							}
							if (distr_id != null)
							{
								var hss_district = await _context.HSS_DistrictsMapps.Where(x => x.hss_id == model.hss_id && x.district_id == distr_id[i]).FirstOrDefaultAsync();
								if (hss_district == null)
								{
									foreach (var distr_add in distr_id)
									{
										if (!hss_district_list.Any(x => x.district_id == distr_add))
										{
											var new_distr = GetNewHSS_DistrictMappsModel(model.hss_id, distr_add, false);
											await _context.HSS_DistrictsMapps.AddAsync(new_distr);
										}
										else
										{
											var hss_distr = hss_district_list.Where(x => x.district_id == distr_add).OrderBy(x => x.district_id).LastOrDefault();

											if (hss_distr.is_deleted == true)
											{
												if (hss_distr.district_id == distr_add)
												{
													var distr_upd = await _context.ETO_DistrictsMapps.Where(x => x.eto_id == model.hss_id && x.district_id == distr_add).FirstOrDefaultAsync();
													distr_upd.is_deleted = false;
													distr_upd.edit_date = DateTime.Now;
												}
												else
												{
													var del_distr = GetNewHSS_DistrictMappsModel(model.hss_id, distr_add, false);
													await _context.AddAsync(del_distr);
												}
											}
										}
									}
								}
							}

						}
					}
					
					//var hss_Layers = await _context.HSS_LayersMapping.

					await _context.SaveChangesAsync();
					var hss_id_Param = new SqlParameter("@hss_id", hss_upd.hss_id);
					await _context.Database.ExecuteSqlRawAsync("exec sources.UpdateTerritorisHSS @hss_id", hss_id_Param);
				}
				else
				{
					var hss_new = new HeatSupplySystem();
					var last_unom = await _context.HeatSupplySystems.OrderByDescending(x => x.hss_id).Select(x => x.unom_hss).FirstOrDefaultAsync();

					if (last_unom != null)
					{
						int last_unom_num = 0;
						int.TryParse(last_unom.Substring(1), out last_unom_num);
						last_unom_num = last_unom_num + 1;
						if (last_unom_num < 10)
							unom_hss = hss_new.unom_hss = "00" + last_unom_num;
						else
							unom_hss = hss_new.unom_hss = "0" + last_unom_num;
					}
					hss_new.unom_hss = unom_hss;
					hss_new.eto_id = model.eto_id;
					hss_new.is_liquidated = model.is_liquidated;
					hss_new.reason_liquidation = model.reason_liquidation;
					hss_new.create_date = DateTime.Now;
					hss_new.user_id = userId;

					await _context.AddAsync(hss_new);
					await _context.SaveChangesAsync();
					hss_id = hss_new.hss_id;




					for (int i = 0; i < distr_id.Length; i++)
					{
						HSS_DistrictsMapps hss_DistrictsMapps = new HSS_DistrictsMapps();
						hss_DistrictsMapps.district_id = distr_id[i];
						hss_DistrictsMapps.hss_id = hss_new.hss_id;
						hss_DistrictsMapps.is_deleted = false;
						hss_DistrictsMapps.create_date = DateTime.Now;
						hss_DistrictsMapps.user_id = userId;

						await _context.AddAsync(hss_DistrictsMapps);
					}
					await _context.SaveChangesAsync();
					is_new = true;
					var hss_id_Param = new SqlParameter("@hss_id", hss_new.hss_id);
					await _context.Database.ExecuteSqlRawAsync("exec sources.UpdateTerritorisHSS @hss_id", hss_id_Param);
				}
				return Json(new { success = true, unom_hss, hss_id, is_new });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("HSS_Save", $"id={model.hss_id}", ex.Message, userId);
				return Json(new { success = false });
			}

		}



		//Создание модели для добавления нового района в HSS
		[NonAction]
		public HSS_DistrictsMapps GetNewHSS_DistrictMappsModel(int hss_id, int distr_id, bool? is_deleted)
		{
			var new_distr = new HSS_DistrictsMapps()
			{
				hss_id = hss_id,
				district_id = distr_id,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_distr;
		}
		#endregion
	}
}
