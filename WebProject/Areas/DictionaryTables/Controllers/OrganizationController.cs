using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;

namespace WebProject.Areas.DictionaryTables.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("DictionaryTables")]
	public class OrganizationController : Controller
	{
		private readonly ILogger<OrganizationController> _logger;
		private readonly HssDbContext _context;
		private readonly ApplicationDbContext _context2;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly string? _user;
		private int userId;
		private string? userDisplayName;
		private readonly HSSController _m_c;

		public OrganizationController(ILogger<OrganizationController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
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

		public IActionResult OrganizationList()
		{
			return View();
		}
		#region ViewComponents

		public ActionResult OnGetCallOrganizationList_PartialViewComponent()
		{
			return ViewComponent("OrganizationList_Partial", new { userId });
		}

		#endregion

		#region OpenPopups
		public async Task<IActionResult> OpenOrganization(int id, int data_status, string action_for = "")
		{
			var _org = new OrganizationOneDataViewModel();
			try
			{
				_org = (await _context.OrganizationOneDataViewModels.FromSqlInterpolated($" exec org.sp_GetOrganization {id}, {data_status}").ToListAsync())
				.FirstOrDefault() ?? new OrganizationOneDataViewModel();
				if(id == 0)
				{
					_org.data_status = data_status;
				}
				ViewBag.Action_for = action_for;
				if (action_for == "copy")
					_org.org_id = 0;
				else
					_org.org_id = id;
				ViewBag.OrgList = await _context.Organisations.Select(x => new { x.org_id, x.unom_org }).ToListAsync();
				ViewBag.OrgType = await _context.Dict_ActivityTypes.Select(x => new { x.Id, x.activity_type_name }).ToListAsync();

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("OpenOrganization", $"id={id}, data_status={data_status}, action_for={action_for}", ex.Message, userId);
			}
			return PartialView("OpenOrganization", _org);
		}
		#endregion

		#region Save_Data
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Organization_Save(OrganizationOneDataViewModel model)
		{
			try
			{
				var org_upd = await _context.Organisations.Where(x => x.org_id == model.org_id).FirstOrDefaultAsync();
				string unom_org = "ТО1"; int org_id = 0; bool is_new = false;

				if(org_upd != null) 
				{
					org_upd.org_activity_type_id = model.activity_type_id;
					org_upd.inn = model.inn;
					org_upd.kpp = model.kpp;
					org_upd.ogrn = model.ogrn;
					org_upd.org_struct = model.org_struct;
					org_upd.edit_date = DateTime.Now;
					org_upd.user_id = userId;

					var org_h_upd = await _context.Org_History.Where(x => x.org_id == model.org_id && x.data_status == model.data_status).FirstOrDefaultAsync();
					if(org_h_upd != null)
					{
						//org_h_upd.data_status = model.data_status;
						org_h_upd.org_status_id = model.org_status_id;
						org_h_upd.full_name = model.full_name;
						org_h_upd.short_name = model.short_name;
						org_h_upd.pozition_head_manager = model.pozition_head_manager;
						org_h_upd.l_name_head_manager = model.l_name_head_manager;
						org_h_upd.f_name_head_manager = model.f_name_head_manager;
						org_h_upd.m_name_head_manager = model.m_name_head_manager;
						org_h_upd.org_contact_phones = model.org_contact_phones;
						org_h_upd.org_emails = model.org_emails;
						org_h_upd.edit_date = DateTime.Now;
						org_h_upd.user_id = userId;
					}
					else
					{
						var org_new_his = new Org_History()
						{
							org_id = model.org_id,
							data_status = model.data_status,
							full_name = model.full_name,
							short_name = model.short_name,
							pozition_head_manager = model.pozition_head_manager,
							l_name_head_manager = model.l_name_head_manager,
							f_name_head_manager = model.f_name_head_manager,
							m_name_head_manager = model.m_name_head_manager,
							org_contact_phones = model.org_contact_phones,
							org_emails = model.org_emails,
							is_deleted = false,
							create_date = DateTime.Now,
							user_id = userId
						};
						await _context.Org_History.AddAsync(org_new_his);
					}
					if(model.activity_type_id == 1)
					{
					await	tso_Save(model.org_id, model.data_status);
					await	tso_p_Save(model, org_id);
					}
					
				}
				else
				{
					Organisations org_new = new Organisations();
					var last_unom = await _context.Organisations.OrderByDescending(x => x.org_id).Select(x => x.unom_org).FirstOrDefaultAsync();

					if (last_unom != null)
					{
						int last_unom_num = 0;
						int.TryParse(last_unom.Substring(2), out last_unom_num);
						last_unom_num = last_unom_num + 1;
						unom_org = org_new.unom_org = "ТО" + last_unom_num;
					}
					org_new.unom_org = unom_org;
					org_new.org_activity_type_id = model.activity_type_id;
					org_new.inn = model.inn;
					org_new.kpp = model.kpp;
					org_new.ogrn = model.ogrn;
					org_new.org_struct = model.org_struct;
					org_new.create_date = DateTime.Now;
					org_new.user_id = userId;

					await _context.AddAsync(org_new);
					await _context.SaveChangesAsync();

					int last_org_id = org_id = await _context.Organisations.OrderByDescending(x => x.org_id).Select(x => x.org_id).FirstOrDefaultAsync();

					Org_History org_h_new = new Org_History();
					org_h_new.org_id = org_new.org_id;
					org_h_new.data_status = model.data_status;
					org_h_new.org_status_id = model.org_status_id;
					org_h_new.full_name = model.full_name;
					org_h_new.short_name = model.short_name;
					org_h_new.pozition_head_manager = model.pozition_head_manager;
					org_h_new.l_name_head_manager = model.l_name_head_manager;
					org_h_new.f_name_head_manager = model.f_name_head_manager;
					org_h_new.m_name_head_manager = model.m_name_head_manager;
					org_h_new.org_contact_phones = model.org_contact_phones;
					org_h_new.org_emails = model.org_emails;
					org_h_new.is_deleted = false;
					org_h_new.create_date = DateTime.Now;
					org_h_new.user_id = userId;

					await _context.AddAsync(org_h_new);

					if (model.activity_type_id == 1)
					{
						await tso_Save(org_id, model.data_status);
						await tso_p_Save(model, org_id);
					}

					await _context.SaveChangesAsync();
					is_new = true;
				}
				await _context.SaveChangesAsync();

				return Json(new { success = true, org_id, unom_org, is_new});

			}
			catch (Exception ex)
			{
				_m_c.ExLog_Save("Organization_Save", $"id={model.org_id}, data_status={model.data_status}", ex.Message, userId);
				return Json(new { success = false });
			}
		}

		public async Task tso_Save(int org_id, int data_status)
		{
			var tso = await _context.TSO_History.Where(x => x.org_id == org_id).FirstOrDefaultAsync() ?? new TSO_History();
			if (tso.org_id == 0)
			{
				tso.org_id = org_id;
				tso.data_status = data_status;
				tso.is_deleted = false;
				tso.create_date = DateTime.Now;
				tso.user_id = userId;

				await _context.AddAsync(tso);
			}
		}

		public async Task tso_p_Save(OrganizationOneDataViewModel model, int org_id)
		{
			var tso_p = await _context.TSO_Perspective.Where(x => x.org_id == org_id && x.data_status == model.data_status).ToListAsync();
			if (tso_p.Count != 0 && tso_p[0].org_status_id != model.org_status_id)
			{
				foreach (var t_p in tso_p)
				{
					t_p.org_status_id = model.org_status_id;
					t_p.user_id = userId;
					t_p.edit_date = DateTime.Now;
				}

			}
			if (tso_p.Count == 0)
			{
				TSO_Perspective tso_per = new TSO_Perspective();
				var per_year = perspective_years(model.data_status);
				foreach (var p_y in per_year)
				{
					tso_per.org_id = org_id;
					tso_per.data_status = model.data_status;
					tso_per.perspective_year = p_y;
					tso_per.tso_type_id = 2;
					tso_per.org_status_id = model.org_status_id;
					tso_per.user_id = userId;
					tso_per.create_date = DateTime.Now;

					await _context.AddAsync(tso_per);
					await _context.SaveChangesAsync();
				}
			}
		}

		public List<int> perspective_years(int data_status)
		{
			var p_years = _context.PerspectiveYears.Where(x => x.data_status ==  data_status).Select(x => x.perspective_year).ToList();

			return p_years;
		}

		#endregion

		#region DELETE_DATA
		// Удаление Org
		public async Task<IActionResult> Organization_Delete(int org_id, int data_status)
		{
			if (org_id > 0)
			{
				try
				{
					if (await _context.Organisations.AnyAsync(x => x.org_id == org_id))
					{
						var org_his_upd = await _context.Org_History.Where(x => x.org_id == org_id).OrderByDescending(x => x.data_status).FirstOrDefaultAsync();

						if (org_his_upd.is_deleted == false)
						{
							var tso_his_upd = await _context.TSO_History.Where(x => x.org_id == org_id && x.data_status == data_status).OrderByDescending(x => x.data_status).FirstOrDefaultAsync();
							if (org_his_upd.data_status == data_status)
							{
								org_his_upd.is_deleted = true;
								org_his_upd.edit_date = DateTime.Now;
								org_his_upd.user_id = userId;
								if (tso_his_upd != null)
								{
									tso_his_upd.is_deleted = true;
									tso_his_upd.edit_date = DateTime.Now;
									tso_his_upd.user_id = userId;
								}
								//else
								//{
								//	var tso_new_his = new TSO_History()
								//	{
								//		org_id = org_id,
								//		data_status = data_status,
								//		code_tso = tso_his_upd.code_tso,
								//		org_property_type_id = tso_his_upd.org_property_type_id,
								//		eto_id = tso_his_upd.eto_id,
								//		org_size_own_capital = tso_his_upd.org_size_own_capital,
								//		year_own_capital = tso_his_upd.year_own_capital,
								//		tso_nds_pay = tso_his_upd.tso_nds_pay,
								//		send_letters_type_id = tso_his_upd.send_letters_type_id,
								//		is_deleted = true,
								//		create_date = DateTime.Now,
								//		user_id = userId
								//	};
								//	await _context.TSO_History.AddAsync(tso_new_his);
								//}
							}
							else
							{
								var org_new_his = new Org_History()
								{
									org_id = org_id,
									data_status = data_status,
									full_name = org_his_upd.full_name,
									short_name = org_his_upd.short_name,
									pozition_head_manager = org_his_upd.pozition_head_manager,
									l_name_head_manager = org_his_upd.l_name_head_manager,
									f_name_head_manager = org_his_upd.f_name_head_manager,
									m_name_head_manager = org_his_upd.m_name_head_manager,
									org_contact_phones = org_his_upd.org_contact_phones,
									org_emails = org_his_upd.org_emails,
									is_deleted = true,
									create_date = DateTime.Now,
									user_id = userId
								};
								await _context.Org_History.AddAsync(org_new_his);

								//var tso_new_his = new TSO_History()
								//{
								//	org_id = org_id,
								//	data_status = data_status,
								//	code_tso = tso_his_upd.code_tso,
								//	org_property_type_id = tso_his_upd.org_property_type_id,
								//	eto_id = tso_his_upd.eto_id,
								//	org_size_own_capital = tso_his_upd.org_size_own_capital,
								//	year_own_capital = tso_his_upd.year_own_capital,
								//	tso_nds_pay = tso_his_upd.tso_nds_pay,
								//	send_letters_type_id = tso_his_upd.send_letters_type_id,
								//	is_deleted = true,
								//	create_date = DateTime.Now,
								//	user_id = userId
								//};
								//await _context.TSO_History.AddAsync(tso_new_his);
							}
							await _context.SaveChangesAsync();
						}
					}
				}
				catch (Exception ex)
				{
					_m_c.ExLog_Save("Organization_Delete", $"id={org_id}, data_status={data_status}", ex.Message, userId);
					return Json(new { success = false });
				}
			}
			return Json(new { success = true });
		}
		#endregion
	}
}
