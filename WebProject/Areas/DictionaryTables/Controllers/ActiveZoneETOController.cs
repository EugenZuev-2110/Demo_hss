using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Syncfusion.XlsIO;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("DictionaryTables")]
	public class ActiveZoneETOController : Controller
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

		public ActiveZoneETOController(ILogger<TariffZoneController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult ActiveZoneETOList()
		{
			return View();
		}

		#region ViewComponents

		public ActionResult OnGetCallActiveZoneETOList_PartialViewComponent()
		{
			return ViewComponent("ActiveZoneETOList_Partial", new {userId});
		}

		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenActiveZoneETO(int id, string action_for = "")
		{
			var _activeZoneEto = new ActiveZoneETOOneDataViewModel();
			try
			{
				_activeZoneEto = (await _context.ActiveZoneETOOneDataViewModels.FromSqlInterpolated($" exec tso.sp_GetETOZone {id}").ToListAsync())
				.FirstOrDefault() ?? new ActiveZoneETOOneDataViewModel();
				_activeZoneEto.districts = _context.Districts.Select(x => new District() { id = x.Id, distr_name = x.district_name }).ToList();
				_activeZoneEto.ActiveZoneETODistrictList = await _context.DistrictViewModals.FromSqlInterpolated($"select * from tso.fn_GetZoneETODistricts({id})").ToListAsync();
				_activeZoneEto.eto_distr = await _context.ETO_DistrictsMapps.Where(x => x.eto_id == id && x.is_deleted == false).Select(x => x.district_id).ToArrayAsync();
				_activeZoneEto.eto_hss = await _context.HeatSupplySystems.Where(x => x.eto_id == id).Select(x => x.hss_id).ToArrayAsync();
				_activeZoneEto.unomHSS = await _context.HeatSupplySystems.Where(x => x.is_liquidated == false).Select(x => new UnomHSSListViewModel(){hss_id = x.hss_id, unom_hss = x.unom_hss}).ToListAsync();
				ViewBag.Action_for = action_for;
				if (action_for == "copy")
					_activeZoneEto.eto_id = 0;
				else
					_activeZoneEto.eto_id = id;
				ViewBag.ETOList = await _context.ETO.Select(x => new { x.eto_id, x.unom_eto }).ToListAsync();
				//ViewBag.HssList = await _context.fn_GetUnomHssList().ToArrayAsync();
				
			}
			catch (Exception ex) 
			{
				_m_c.ExLog_Save("OpenActiveZoneETO", $"id={id}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenActiveZoneETO", _activeZoneEto);
		}
		#endregion

		#region Save_Data
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ActiveZoneETO_Save(ActiveZoneETOOneDataViewModel model, int[] distr_id, int[] hss_id)
		{
			try
			{
				var eto_upd = await _context.ETO.Where(x => x.eto_id == model.eto_id).FirstOrDefaultAsync();
				string unom_eto = "01"; int eto_id = 0; bool is_new = false;
				if (eto_upd != null)
				{
					eto_upd.unom_eto = model.unom_eto;
					eto_id = eto_upd.eto_id = model.eto_id;
					eto_upd.is_liquidated = model.is_liquidated;
					eto_upd.year_liquidation = model.year_liquidation;
					eto_upd.reason_liquidation = model.reason_liquidation;
					eto_upd.edit_date = DateTime.Now;
					eto_upd.user_id = userId;

					for (int i = 0; i < distr_id.Length; i++)
					{
						var eto_district_list = await _context.ETO_DistrictsMapps.Where(x => x.eto_id == model.eto_id).ToListAsync();
						if (distr_id != null || (distr_id == null && eto_district_list.Any(x => x.is_deleted == false)))
						{
							if (eto_district_list.Count() > 0)
							{
								List<int> distr_delete;

								if (distr_id != null)
									distr_delete = eto_district_list.Where(x => !distr_id.Any(y => y == x.district_id) && x.is_deleted == false).Select(x => x.district_id).Distinct().ToList();
								else
									distr_delete = eto_district_list.Where(x => x.is_deleted == false).Select(x => x.district_id).Distinct().ToList();

								if (distr_delete.Count > 0)
								{
									foreach (var distr in distr_delete)
									{
										var tz_district = eto_district_list.Where(x => x.district_id == distr).OrderBy(x => x.district_id).LastOrDefault();
										if (tz_district.is_deleted == false)
										{
											if (tz_district.district_id == distr)
											{
												var distr_upd = await _context.ETO_DistrictsMapps.Where(x => x.eto_id == model.eto_id && x.district_id == distr).FirstOrDefaultAsync();
												distr_upd.is_deleted = true;
												distr_upd.edit_date = DateTime.Now;
											}
											else
											{
												var del_distr = GetNewETO_DistrictMappsModel(model.eto_id, distr, true);
												await _context.AddAsync(del_distr);
											}
										}

									}
								}

							}
							if (distr_id != null)
							{
								var eto_district = await _context.ETO_DistrictsMapps.Where(x => x.eto_id == model.eto_id && x.district_id == distr_id[i]).FirstOrDefaultAsync();
								if (eto_district == null)
								{
									foreach (var distr_add in distr_id)
									{
										if (!eto_district_list.Any(x => x.district_id == distr_add))
										{
											var new_distr = GetNewETO_DistrictMappsModel(model.eto_id, distr_add, false);
											await _context.ETO_DistrictsMapps.AddAsync(new_distr);
										}
										else
										{
											var eto_distr = eto_district_list.Where(x => x.district_id == distr_add).OrderBy(x => x.district_id).LastOrDefault();

											if (eto_distr.is_deleted == true)
											{
												if (eto_distr.district_id == distr_add)
												{
													var distr_upd = await _context.ETO_DistrictsMapps.Where(x => x.eto_id == model.eto_id && x.district_id == distr_add).FirstOrDefaultAsync();
													distr_upd.is_deleted = false;
													distr_upd.edit_date = DateTime.Now;
												}
												else
												{
													var del_distr = GetNewETO_DistrictMappsModel(model.eto_id, distr_add, false);
													await _context.AddAsync(del_distr);
												}
											}
										}
									}
								}
							}

						}
					}
					
					await _context.SaveChangesAsync();
					var eto_id_Param = new SqlParameter("@eto_id", eto_upd.eto_id);
					await _context.Database.ExecuteSqlRawAsync("exec tso.UpdateTerritorisETO @eto_id", eto_id_Param);
				}
				else
				{
					var eto_new = new ETO();
					var last_unom = await _context.ETO.OrderByDescending(x => x.eto_id).Select(x => x.unom_eto).FirstOrDefaultAsync();

					if (last_unom != null)
					{
						int last_unom_num = 0;
						int.TryParse(last_unom.Substring(1), out last_unom_num);
						last_unom_num = last_unom_num + 1;
						if(last_unom_num < 10)
						unom_eto = eto_new.unom_eto = "00" + last_unom_num;
						else
						unom_eto = eto_new.unom_eto = "0" + last_unom_num;
					}
					eto_new.unom_eto = unom_eto;
					eto_new.is_liquidated = model.is_liquidated;
					eto_new.reason_liquidation = model.reason_liquidation;
					eto_new.create_date = DateTime.Now;
					eto_new.user_id = userId;

					await _context.AddAsync(eto_new);
					await _context.SaveChangesAsync();
					 eto_id = eto_new.eto_id;

					var eto_hss = await _context.HeatSupplySystems.Where(x => x.hss_id == model.hss_id).FirstOrDefaultAsync();
					if (eto_hss != null)
					{
					    eto_hss.eto_id = eto_id;
						eto_hss.edit_date = DateTime.Now;
						eto_hss.user_id = userId;
					}

					for (int i = 0; i < distr_id.Length; i++)
					{
						ETO_DistrictsMapps eto_DistrictsMapps = new ETO_DistrictsMapps();
						eto_DistrictsMapps.district_id = distr_id[i];
						eto_DistrictsMapps.eto_id = eto_id;
						eto_DistrictsMapps.is_deleted = false;
						eto_DistrictsMapps.create_date = DateTime.Now;
						eto_DistrictsMapps.user_id = userId;

						await _context.AddAsync(eto_DistrictsMapps);
					}
					await _context.SaveChangesAsync();
					is_new = true;
					var eto_id_Param = new SqlParameter("@eto_id", eto_new.eto_id);
					await _context.Database.ExecuteSqlRawAsync("exec tso.UpdateTerritorisETO @eto_id", eto_id_Param);
				}
				return Json(new { success = true, unom_eto, eto_id, is_new });
			}
			catch (Exception ex) 
			{
				_m_c.ExLog_Save("ActiveZoneETO_Save", $"id={model.eto_id}", ex.Message, userId);
				return Json(new { success = false }); 
			}
			
		}

	

		//Создание модели для добавления нового района в ТЗ
		[NonAction]
		public ETO_DistrictsMapps GetNewETO_DistrictMappsModel(int eto_id, int distr_id, bool? is_deleted)
		{
			var new_distr = new ETO_DistrictsMapps()
			{
				eto_id = eto_id,
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
