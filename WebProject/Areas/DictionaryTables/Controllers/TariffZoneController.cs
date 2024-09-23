using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
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
		public class TariffZoneController : Controller
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
			public DateTime time = DateTime.Now;
			private string? _connectionString;

			public TariffZoneController(ILogger<TariffZoneController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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
			public IActionResult TariffZoneList()
			{
				return View();
			}

		#region ViewComponents

		public ActionResult OnGetCallTariffZoneList_PartialViewComponent(int? data_status, int? perspective_year)
		{
			return ViewComponent("TariffZoneList_Partial", new { data_status, perspective_year, userId });
		}

		#endregion


		#region OpenPopups
		public async Task<IActionResult> OpenTariffZone(int id, int data_status, int perspective_year, string action_for = "")
		{
			var _tarifZoneViewModel = new TariffZoneOneDataViewModel();
			try
			{
				 _tarifZoneViewModel = (await _context.TariffZoneOneDataViewModels.FromSqlInterpolated($"exec tarif_zone.sp_GetTZOneData {id},{data_status},{userId}")
							.ToListAsync()).FirstOrDefault() ?? new TariffZoneOneDataViewModel();

			
				_tarifZoneViewModel.data_status = data_status;
				ViewBag.TZList = _context.fnt_GetTZUnomList(data_status).ToList();
				ViewBag.Action_for = action_for;
				//ViewBag.Districts = _context.fnt_GetDistrictList(data_status).ToList();
				ViewBag.TZDistricts = _context.fnt_GetDistrictTZ(id).ToList();

				_tarifZoneViewModel.districts =  _context.Districts.Select(x => new District() { id = x.Id, distr_name = x.district_name}).ToList();
				_tarifZoneViewModel.TariffZoneTsoList = await _context.TariffZoneTsoViewModals.FromSqlInterpolated($"exec tarif_zone.GetTZTSOPerspective {data_status}, {id}").ToListAsync();
				_tarifZoneViewModel.TariffZoneDistrictList = await _context.DistrictViewModals.FromSqlInterpolated($"select * from tarif_zone.GetTarifZoneDistricts({id})").ToListAsync();
				if (action_for == "copy")
					_tarifZoneViewModel.tz_id = 0;
				else
				_tarifZoneViewModel.tz_id = id;
				_tarifZoneViewModel.data_status = data_status;
				_tarifZoneViewModel.tz_distr = await _context.TZ_DistrictsMapps.Where(x => x.tz_id == id && x.is_deleted == false).Select(x => x.district_id).ToArrayAsync();
				ViewBag.TsoList = await _context.fnt_GetTSOName(data_status).ToArrayAsync();
				ViewBag.LayersList = await _context.Layers.Select(x => new Layers() { Id = x.Id, layer_unom = x.layer_unom }).ToArrayAsync();
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenTariffZone", $"id={id},data_status={data_status}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenTariffZone", _tarifZoneViewModel);
		}
		#endregion

		#region Save_Data
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> TariffZone_Save(TariffZoneOneDataViewModel model, int[] perspective_year, int[] tso_select, int[] distr_id)
		{
			try
			{
				var tz_upd = await _context.TariffZone.Where(x => x.tz_id == model.tz_id).FirstOrDefaultAsync();
				string unom_tz = "01"; int tz_id = 0;
				if (tz_upd != null)
				{
					tz_upd.unom_tz = model.unom_tz;
					tz_id = tz_upd.tz_id = model.tz_id;
					tz_upd.tz_name = model.tz_name;
					tz_upd.combine_prod_more25 = model.combine_prod_more25;
					tz_upd.combine_prod_less25 = model.combine_prod_less25;
					tz_upd.not_combine_prod = model.not_combine_prod;
					tz_upd.transfer = model.transfer;
					tz_upd.sale = model.sale;
					tz_upd.delivery_contract = model.delivery_contract;
					tz_upd.tariff_differentiation = model.tariff_differentiation;
					tz_upd.edit_date = DateTime.Now;
					tz_upd.user_id = userId;

					var tz_h_upd = await _context.TZ_History.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status).FirstOrDefaultAsync();
					if(tz_h_upd != null)
					{
						tz_h_upd.tz_id = model.tz_id;
						tz_h_upd.data_status = model.data_status;
						tz_h_upd.tz_num = model.tz_num;
						tz_h_upd.tz_num_plan = model.tz_num_plan;
						tz_h_upd.layer_id = model.layer_id;
						tz_h_upd.layer_sys = model.layer_sys;
						tz_h_upd.is_deleted = false;
						tz_h_upd.edit_date = DateTime.Now;
						tz_h_upd.user_id = userId;
					}
					else
					{
						tz_h_upd.tz_id = model.tz_id;
						tz_h_upd.data_status = model.data_status;
						tz_h_upd.tz_num = model.tz_num;
						tz_h_upd.tz_num_plan = model.tz_num_plan;
						tz_h_upd.layer_id = model.layer_id;
						tz_h_upd.layer_sys = model.layer_sys;
						tz_h_upd.is_deleted = false;
						tz_h_upd.create_date = DateTime.Now;
						tz_h_upd.user_id = userId;
						await _context.AddAsync(tz_h_upd);
					}

					for (int i = 0; i < perspective_year.Length; i++)
					{

						if (_context.TZ_Perspective.Any(x => x.tz_id == model.tz_id && x.data_status == model.data_status && x.perspective_year == perspective_year[i]))
						{
							var tz_perspective_udp = await _context.TZ_Perspective.Where(x => x.tz_id == model.tz_id && x.data_status == model.data_status && x.perspective_year == perspective_year[i]).FirstOrDefaultAsync();
							//tz_perspective_udp.tz_num = model.tz_num;
							if (tz_perspective_udp.tso_id != tso_select[i])
							{
								tz_perspective_udp.data_status = model.data_status;
								tz_perspective_udp.tso_id = tso_select[i];
								tz_perspective_udp.edit_date = DateTime.Now;
								tz_perspective_udp.user_id = userId;
							}
						}

						else
						{
							TZ_Perspective tz_perspective_new = new TZ_Perspective();
							tz_perspective_new.tz_id = model.tz_id;
							//tz_perspective_new.tz_num = model.tz_num;
							tz_perspective_new.tso_id = tso_select[i];
							tz_perspective_new.data_status = model.data_status;
							tz_perspective_new.perspective_year = perspective_year[i];
							tz_perspective_new.create_date = DateTime.Now;
							tz_perspective_new.user_id = userId;

							await _context.AddAsync(tz_perspective_new);
						}
						await _context.SaveChangesAsync();
					}

					for (int i = 0; i < distr_id.Length; i++)
					{
						var tz_district_list = await _context.TZ_DistrictsMapps.Where(x => x.tz_id == model.tz_id).ToListAsync();
						if (distr_id != null || (distr_id == null && tz_district_list.Any(x => x.is_deleted == false)))
						{
							if (tz_district_list.Count() > 0)
							{
								List<int> distr_delete;

								if (distr_id != null)
									distr_delete = tz_district_list.Where(x => !distr_id.Any(y => y == x.district_id) && x.is_deleted == false).Select(x => x.district_id).Distinct().ToList();
								else
									distr_delete = tz_district_list.Where(x => x.is_deleted == false).Select(x => x.district_id).Distinct().ToList();

								if (distr_delete.Count > 0)
								{
									foreach (var distr in distr_delete)
									{
										var tz_district = tz_district_list.Where(x => x.district_id == distr).OrderBy(x => x.district_id).LastOrDefault();
										if (tz_district.is_deleted == false)
										{
											if (tz_district.district_id == distr)
											{
												var distr_upd = await _context.TZ_DistrictsMapps.Where(x => x.tz_id == model.tz_id && x.district_id == distr).FirstOrDefaultAsync();
												distr_upd.is_deleted = true;
												distr_upd.edit_date = DateTime.Now;
												distr_upd.user_id = userId;
											}
											else
											{
												var del_distr = GetNewTZ_DistrictMappsModel(model.tz_id, distr, true);
												await _context.AddAsync(del_distr);
											}
										}

									}
								}

							}
							if (distr_id != null)
							{
								var tz_district = await _context.TZ_DistrictsMapps.Where(x => x.tz_id == model.tz_id && x.district_id == distr_id[i]).FirstOrDefaultAsync();
								if (tz_district == null)
								{
									foreach (var distr_add in distr_id)
									{
										if (!tz_district_list.Any(x => x.district_id == distr_add))
										{
											var new_distr = GetNewTZ_DistrictMappsModel(model.tz_id, distr_add, false);
											await _context.TZ_DistrictsMapps.AddAsync(new_distr);
										}
										else
										{
											var tz_distr = tz_district_list.Where(x => x.district_id == distr_add).OrderBy(x => x.district_id).LastOrDefault();

											if (tz_distr.is_deleted == true)
											{
												if (tz_distr.district_id == distr_add)
												{
													var distr_upd = await _context.TZ_DistrictsMapps.Where(x => x.tz_id == model.tz_id && x.district_id == distr_add).FirstOrDefaultAsync();
													distr_upd.is_deleted = false;
													distr_upd.edit_date = DateTime.Now;
												}
												else
												{
													var del_distr = GetNewTZ_DistrictMappsModel(model.tz_id, distr_add, false);
													await _context.AddAsync(del_distr);
												}
											}
										}
									}
								}
							}

						}
					}
					var tz_id_Param = new SqlParameter("@tz_id", tz_upd.tz_id);
					await _context.Database.ExecuteSqlRawAsync("exec tarif_zone.UpdateTerritorisTZ @tz_id", tz_id_Param);
				}
				else
				{
					var tz_new = new TariffZone();

					var code_tso = await _context.TSO_History.Where(x => x.org_id == tso_select[0])
						.Select(x => x.code_tso).FirstOrDefaultAsync();
					var count_code_tso = await Task.Run( ()=> _context.TZ_Perspective.Where(x => x.tso_id == tso_select[0] && x.perspective_year == model.data_status).Count());
					//await _context.TariffZone.OrderByDescending(x => x.tz_id).Select(x => x.unom_tz).FirstOrDefaultAsync();
					unom_tz = code_tso + "-" + count_code_tso;
					//if (last_unom != null)
					//{
					//	int last_unom_num = 0;
					//	int.TryParse(last_unom.Substring(1), out last_unom_num);
					//	last_unom_num = last_unom_num + 1;
					//	unom_tz = tz_new.unom_tz = "0" + last_unom_num;

					//}
					tz_new.tz_id = model.tz_id;
					tz_new.unom_tz = unom_tz;
					tz_new.tz_name = model.tz_name;
					tz_new.combine_prod_more25 = model.combine_prod_more25;
					tz_new.combine_prod_less25 = model.combine_prod_less25;
					tz_new.not_combine_prod = model.not_combine_prod;
					tz_new.transfer = model.transfer;
					tz_new.sale = model.sale;
					tz_new.delivery_contract = model.delivery_contract;
					tz_new.tariff_differentiation = model.tariff_differentiation;
					tz_new.create_date = DateTime.Now;
					tz_new.user_id = userId;

					await _context.TariffZone.AddAsync(tz_new);
					await _context.SaveChangesAsync();
					int last_tz_id = tz_id = await _context.TariffZone.OrderByDescending(x => x.tz_id).Select(x => x.tz_id).FirstOrDefaultAsync();

					TZ_History tz_h_new = new TZ_History();
					tz_h_new.tz_id = last_tz_id;
					tz_h_new.data_status = model.data_status;
					tz_h_new.tz_num = model.tz_num;
					tz_h_new.tz_num_plan = model.tz_num_plan;
					tz_h_new.layer_id = model.layer_id;
					tz_h_new.layer_sys = model.layer_sys;
					tz_h_new.is_deleted = false;
					tz_h_new.create_date = DateTime.Now;
					tz_h_new.user_id = userId;
					await _context.AddAsync(tz_h_new);
					if (tso_select != null && tso_select.Length > 0)
					{
						for (int i = 0; i < perspective_year.Length; i++)
						{
							TZ_Perspective tz_perspective_new = new TZ_Perspective();
							tz_perspective_new.tz_id = last_tz_id;
							//tz_perspective_new.tz_num = model.tz_num;
							tz_perspective_new.tso_id = tso_select[i];
							tz_perspective_new.data_status = model.data_status;
							tz_perspective_new.perspective_year = perspective_year[i];
							tz_perspective_new.create_date = DateTime.Now;
							tz_perspective_new.user_id = userId;

							await _context.AddAsync(tz_perspective_new);
						}
					}
					for (int i = 0; i < distr_id.Length; i++)
					{
						TZ_DistrictsMapps tz_DistrictsMapps = new TZ_DistrictsMapps();
						tz_DistrictsMapps.district_id = distr_id[i];
						tz_DistrictsMapps.tz_id = last_tz_id;
						tz_DistrictsMapps.is_deleted = false;
						tz_DistrictsMapps.create_date = DateTime.Now;
						tz_DistrictsMapps.user_id = userId;

						await _context.AddAsync(tz_DistrictsMapps);
					}
					await _context.SaveChangesAsync();
					var tz_id_Param = new SqlParameter("@tz_id", tz_new.tz_id);
					await _context.Database.ExecuteSqlRawAsync("exec tarif_zone.UpdateTerritorisTZ @tz_id", tz_id_Param);
				}

				await _context.SaveChangesAsync();

				return Json(new { success = true, unom_tz, tz_id });
			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("TariffZone_Save", $"id={model.tz_id},data_status={model.data_status}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		public async Task<IActionResult> TariffZone_Delete(int tz_id, int data_status)
		{
			if(tz_id > 0)
			{
				try
				{
					if(await _context.TariffZone.AnyAsync(x => x.tz_id == tz_id))
					{
						var tz_h_upd = await _context.TZ_History.Where(x => x.tz_id == tz_id).OrderByDescending(x => x.data_status).FirstOrDefaultAsync();

						if(tz_h_upd.is_deleted == false)
						{
							tz_h_upd.is_deleted = true;
							tz_h_upd.edit_date = DateTime.Now;
							tz_h_upd.user_id = userId;
						}
						await _context.SaveChangesAsync();
					}
				}
				catch (Exception ex)
				{
					_m_c.ExLog_Save("TariffZone_Delete", $"id={tz_id},data_status={data_status}", ex.Message, userId);
				}
			}
			return Json(new { success = true });
		}


		

		//Создание модели для добавления нового района в ТЗ
		[NonAction]
		public TZ_DistrictsMapps GetNewTZ_DistrictMappsModel(int tz_id, int distr_id, bool? is_deleted)
		{
			var new_distr = new TZ_DistrictsMapps()
			{
				tz_id = tz_id,
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

