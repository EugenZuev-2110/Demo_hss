using ClosedXML.Excel;
using DataBase.Models;
using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using System.Net;
using System.Security.Policy;
using WebProject.Areas.TSO.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using WebProject.Models;
using static System.Net.WebRequestMethods;

namespace WebProject.Areas.TSO.Controllers
{
    [TypeFilter(typeof(ControllerActionFilter))]
    [Area("TSO")]
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;
        private readonly HssDbContext _context;
        private readonly ApplicationDbContext _context2;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string? _user;
        private int userId;
        private string? userDisplayName;
		private readonly HSSController _m_c;
		//private PrincipalContext _ctx = new PrincipalContext(ContextType.Domain);
		public MainController(ILogger<MainController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, 
			IWebHostEnvironment hostingEnvironment, HSSController m_c)
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
        public IActionResult TSOList()
        {
            return View();
        }

        public IActionResult TSOUploadList()
        {
			var model = new TSOUploadedListViewModel()
			{
				
			};

            return View();
        }

        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public IActionResult TSOSaveFileToDB(TSOUploadFileViewModel model)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;

                string webRootPath = _hostingEnvironment.WebRootPath;
                string upload = webRootPath + "/UploadedFiles/TSO/";

                string extension = Path.GetExtension(files[0].FileName);
                string fileName = Path.GetFileNameWithoutExtension(files[0].FileName);
                string url = upload + fileName + extension;
                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                //var filename = "/UploadedFiles/TSO/загрузка_ТСО_" + DateTime.Now + '_' +  + userId + ".xlsx";
				//Stream excelStream = System.IO.File.Create(webRootPath + filename);
				//files[0].SaveAs(excelStream);
                //excelStream.Dispose();

    //            var workbook = new XLWorkbook(url);
				//var ws1 = workbook.Worksheet(1);

    //            var rows = ws1.RangeUsed().RowsUsed().Skip(1); // Skip header row
    //            foreach (var row in rows)
    //            {
    //                var rowNumber = row.RowNumber();
                    
				//	var cells = ws1.Row(rowNumber).CellsUsed();

    //                foreach (var cell in cells)
				//	{
    //                    //var cell = row.Cell(rowNumber);
    //                    string value = cell.GetValue<string>();
    //                }
                        
    //            }

                //var row = ws1.Row(1);
                //var cell = row.Cell(1);
                //object value = cell.Value;

                using (var workBook = new XLWorkbook(url))
                {
                    var workSheet = workBook.Worksheet(1);
                    var firstRowUsed = workSheet.FirstRowUsed();
                    var firstPossibleAddress = workSheet.Row(firstRowUsed.RowNumber()).FirstCell().Address;
                    var lastPossibleAddress = workSheet.LastCellUsed().Address;
                    var range = workSheet.Range(firstPossibleAddress, lastPossibleAddress).AsRange(); //.RangeUsed();
                    var table = range.AsTable();
                    var dataList = new List<string[]>
					{
						table.DataRange.Rows()
							.Select(tableRow =>
								tableRow.Field("Id")
									.GetString())
							.ToArray(),
						table.DataRange.Rows()
							.Select(tableRow => tableRow.Field("Name").GetString())
							.ToArray(),
						table.DataRange.Rows()
						.Select(tableRow => tableRow.Field("INN").GetString())
						.ToArray()
					};
                    //Convert List to DataTable

                    var users = new List<TestUploadTSO>();
                    var dataTable = ConvertListToDataTable(dataList);

					int last_batch_id = _context.TestUploadTSO.OrderByDescending(x => x.batch_id).Select(x => x.batch_id).FirstOrDefault();
					last_batch_id = last_batch_id+1;

					for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
						var id = 0;
						int.TryParse(dataTable.Rows[i][0].ToString(), out id);

                        var user = new TestUploadTSO()
						{
							data_status = model.data_status,
							batch_id = last_batch_id,
							is_uploaded = false,
							Id = id,
                            Name = dataTable.Rows[i][1].ToString(),
							INN = dataTable.Rows[i][2].ToString(),
							CreateDate = DateTime.Now,
							UserId = userId
                        };
						users.Add(user);
					}

                    _context.TestUploadTSO.AddRange(users);
                    _context.SaveChanges();

					//var dataTable = ConvertListToDataTable(dataList);
					//var rows_count = dataTable.Rows.Count;
					//var col_count = dataTable.Columns.Count;
					//var a = dataTable.Rows[0][0].ToString();

                    //Unique column values, to avoid duplication
                    //var uniqueCols = dataTable.DefaultView.ToTable(true, "S.Number");
                    //for (var i = uniqueCols.Rows.Count - 1; i >= 0; i--)
                    //{
                    //    var dr = uniqueCols.Rows[i];
                    //    if (dr != null && (string)dr["S.Number"] == "None")
                    //        dr.Delete();
                    //}
                    //Console.WriteLine("Total number of unique s.number in Excel : " + uniqueCols.Rows.Count);
                }


                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _m_c.ExLog_Save("TSOSaveFileToDB", $"data_status={model.data_status}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private static DataTable ConvertListToDataTable(IReadOnlyList<string[]> list)
        {
            var table = new DataTable("CustomTable");
            var rows = list.Select(array => array.Length).Concat(new[] { 0 }).Max();

            table.Columns.Add("Id");
            table.Columns.Add("Name");
            table.Columns.Add("INN");

            for (var j = 0; j < rows; j++)
            {
                var row = table.NewRow();
                row["Id"] = list[0][j];
                row["Name"] = list[1][j];
                row["INN"] = list[2][j];
                table.Rows.Add(row);
            }
            return table;
        }

        #region OpenPopups
        //открытие попапа добавления или редактирования ТСО
        public IActionResult OpenTSO(int id, int data_status, string action_for = "")
        {
			var tso = new TSOViewModel();
			try
			{
				if (id > 0)
				{
					tso = (from org in _context.Organisations
						   join org_h in _context.Org_History on org.org_id equals org_h.org_id
						   join tso_h in _context.TSO_History on org.org_id equals tso_h.org_id
						   where org.org_id == id && org_h.is_deleted == false && tso_h.is_deleted == false
							   && org_h.data_status == (_context.Org_History.Where(x => x.data_status <= data_status && x.org_id == id).Max(x => x.data_status))
							   && tso_h.data_status == (_context.TSO_History.Where(x => x.data_status <= data_status && x.org_id == id).Max(x => x.data_status))
						   select new TSOViewModel
						   {
							   Id = org.org_id,
							   short_name = org_h.short_name,
							   full_name = org_h.full_name,
							   code_tso = tso_h.code_tso,
							   notes = tso_h.notes,
							   inn = org.inn,
							   ogrn = org.ogrn,
							   kpp = org.kpp,
							   org_struct = org.org_struct ?? false,
							   tso_nds_pay = tso_h.tso_nds_pay ?? false,
							   org_size_own_capital = tso_h.org_size_own_capital,
							   year_own_capital = tso_h.year_own_capital,
							   pozition_head_manager = org_h.pozition_head_manager,
							   l_name_head_manager = org_h.l_name_head_manager,
							   f_name_head_manager = org_h.f_name_head_manager,
							   m_name_head_manager = org_h.m_name_head_manager,
							   org_property_type_id = tso_h.org_property_type_id,
							   org_contact_phones = org_h.org_contact_phones,
							   org_emails = org_h.org_emails,
							   eto_id = tso_h.eto_id,
							   etos = _context.TSO_ETOMapps
									  .Where(x => x.tso_id == id && x.is_deleted == false
										  && x.data_status == _context.TSO_ETOMapps.Where(y => y.tso_id == id && y.data_status <= data_status && y.eto_id == x.eto_id).Max(y => y.data_status))
									  .Select(x => x.eto_id).ToArray(),
							   send_letters_type_id = tso_h.send_letters_type_id
						   }).FirstOrDefault();
				}
				tso.data_status = data_status;
				tso.eto_list_status = _context.fnt_GetETOStatusList(tso.Id).ToList();
				tso.eto_list = _context.ETO.Select(x => new ETOViewModel() { eto_id = x.eto_id, unom_eto = x.unom_eto }).ToList();

				if (action_for == "copy")
					tso.Id = 0;

				ViewBag.PropertyTypesList = _context.Dict_TSOPropertyTypes.ToList();
				ViewBag.SendLettersTypesList = _context.Dict_SendLettersTypes.ToList();
				ViewBag.TSOList = _context.fnt_GetOrgList(data_status, 1).ToList();
			}
			catch (Exception ex)
			{

			}
            return PartialView(tso);
        }
		#endregion

		#region SAVE_DATA
		//Сохранение перспективы по ТСО
		
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> TSO_Save_Perspective(int Id, int data_status, int[] perspective_year, short?[] org_status_id, short?[] tso_type_id)
		{
			if (Id > 0)
            {
				try
				{
					short os_id = 0; short tt_id = 0;

					for (int i = 0; i < perspective_year.Length; i++)
					{
						os_id = 0; tt_id = 0;

						if (await _context.TSO_Perspective.AnyAsync(x => x.data_status == data_status && x.org_id == Id && x.perspective_year == perspective_year[i]))
						{
							var tso_perspective_upd = await _context.TSO_Perspective.Where(x => x.data_status == data_status && x.org_id == Id && x.perspective_year == perspective_year[i]).FirstOrDefaultAsync();

							if (tso_perspective_upd.org_status_id != org_status_id[i] || tso_perspective_upd.tso_type_id != tso_type_id[i])
							{
								tso_perspective_upd.org_status_id = org_status_id[i];
								tso_perspective_upd.tso_type_id = tso_type_id[i];
								tso_perspective_upd.edit_date = DateTime.Now;
								tso_perspective_upd.user_id = userId;
							}
						}
						else
						{
							var tso_perspective_new = new TSO_Perspective();
							{
								tso_perspective_new.data_status = data_status;
								tso_perspective_new.perspective_year = perspective_year[i];
								tso_perspective_new.org_id = Id;
								tso_perspective_new.org_status_id = org_status_id[i];
								tso_perspective_new.tso_type_id = tso_type_id[i];
								tso_perspective_new.create_date = DateTime.Now;
								tso_perspective_new.user_id = userId;
							}
							await _context.TSO_Perspective.AddAsync(tso_perspective_new);
						}

						if (data_status == perspective_year[i])
						{
							var org_ds_last = await _context.Org_History.Where(x => x.org_id == Id && x.data_status <= data_status).OrderByDescending(x => x.data_status).FirstOrDefaultAsync();

							if (org_ds_last.org_status_id != org_status_id[i])
							{
								if (org_ds_last.data_status == data_status) 
								{
									org_ds_last.org_status_id = org_status_id[i];
									org_ds_last.edit_date = DateTime.Now;
									org_ds_last.user_id = userId;
								}
								else
								{
									var org_ds_new = new Org_History();
									{
										org_ds_new.org_id = Id;
										org_ds_new.data_status = data_status;
										org_ds_new.org_status_id = org_status_id[i];
										org_ds_new.full_name = org_ds_last.full_name;
										org_ds_new.short_name = org_ds_last.short_name;
										org_ds_new.pozition_head_manager = org_ds_last.pozition_head_manager;
										org_ds_new.l_name_head_manager = org_ds_last.l_name_head_manager;
										org_ds_new.f_name_head_manager = org_ds_last.f_name_head_manager;
										org_ds_new.m_name_head_manager = org_ds_last.m_name_head_manager;
										org_ds_new.org_contact_phones = org_ds_last.org_contact_phones;
										org_ds_new.org_emails = org_ds_last.org_emails;
										org_ds_new.is_deleted = false;
										org_ds_new.create_date = DateTime.Now;
										org_ds_new.user_id = userId; 
									}
									await _context.Org_History.AddAsync(org_ds_new);
								}
							}
						}

						await _context.SaveChangesAsync();
					}
				}
				catch (Exception ex)
				{

				}
            }
			return Json(new { success = true });
		}

		//сохранение / удаление ответственного лица 
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> TSO_SavePerson(int tso_Id, int data_status, TSOResponsiblePersonsListViewModel model)
        {
            if (tso_Id > 0)
            {
                try
                {
					if (await _context.ResponsiblePersons.AnyAsync(x => x.org_id == tso_Id && x.data_status == data_status && x.person_num == model.person_num))
					{
						var resp_pers_upd = await _context.ResponsiblePersons.Where(x => x.org_id == tso_Id
							&& x.data_status == data_status && x.person_num == model.person_num).FirstOrDefaultAsync();

						resp_pers_upd.position_respons_pers = model.position_respons_pers;
						resp_pers_upd.l_name_respons_pers = model.l_name_respons_pers;
						resp_pers_upd.f_name_respons_pers = model.f_name_respons_pers;
						resp_pers_upd.m_name_respons_pers = model.m_name_respons_pers;
						resp_pers_upd.phones_respons_pers = model.phones_respons_pers;
						resp_pers_upd.emails_respons_pers = model.emails_respons_pers;
						resp_pers_upd.edit_date = DateTime.Now;
						resp_pers_upd.is_deleted = model.is_deleted;
						resp_pers_upd.user_id = userId;
					}
					else
					{
						int last_person_num = 0;
						if (await _context.ResponsiblePersons.AnyAsync(x => x.org_id == tso_Id && x.person_num == model.person_num))
						{
							last_person_num = model.person_num;
						}
						else
						{
							last_person_num = await _context.ResponsiblePersons.Where(x => x.org_id == tso_Id).MaxAsync(x => (int?)x.person_num) ?? 0;
							last_person_num = last_person_num + 1;
						}

						var resp_pers_new = new ResponsiblePersons
						{
							org_id = tso_Id,
							data_status = data_status,
							person_num = last_person_num,
							position_respons_pers = model.position_respons_pers,
							l_name_respons_pers = model.l_name_respons_pers,
							f_name_respons_pers = model.f_name_respons_pers,
							m_name_respons_pers = model.m_name_respons_pers,
							phones_respons_pers = model.phones_respons_pers,
							emails_respons_pers = model.emails_respons_pers,
							is_deleted = model.is_deleted,
							create_date = DateTime.Now,
							user_id = userId
						};
						await _context.AddAsync(resp_pers_new);
					}
					await _context.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					_m_c.ExLog_Save("TSO_SavePerson", $"tso_Id={tso_Id},data_status={data_status},person_num={model.person_num}", ex.Message, userId);
					return Json(new { success = false });
				}
			}

			return Json(new { success = true });
		}

		//Сохранение ТСО
		[ValidateAntiForgeryToken]
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> TSO_Save(TSOViewModel model)
        {
			try
			{
				var org_upd = await _context.Organisations.Where(x => x.org_id == model.Id).FirstOrDefaultAsync();

				string unom_org = "ТО1"; byte is_new_tso = 0;
				if (org_upd != null)
				{
					org_upd.org_activity_type_id = 1;
					org_upd.inn = model.inn;
					org_upd.kpp = model.kpp;
					org_upd.ogrn = model.ogrn;
					org_upd.org_struct = model.org_struct;
					org_upd.edit_date = DateTime.Now;
					org_upd.user_id = userId;

					unom_org = org_upd.unom_org;
				}
				else
				{
					is_new_tso = 1;
					var last_unom = await _context.Organisations.Where(x => x.org_activity_type_id == 1).OrderByDescending(x => x.org_id).Select(x => x.unom_org).FirstOrDefaultAsync();
					if (last_unom != null)
					{
						int last_unom_num = 0;
						int.TryParse(last_unom.Substring(2), out last_unom_num);
						last_unom_num = last_unom_num + 1;
						unom_org = "ТО" + last_unom_num;
					}

					var org_new = new Organisations()
					{
						unom_org = unom_org,
						org_activity_type_id = 1,
						inn = model.inn,
						kpp = model.kpp,
						ogrn = model.ogrn,
						org_struct = model.org_struct,
						create_date = DateTime.Now,
						user_id = userId
					};
					await _context.Organisations.AddAsync(org_new);
					await _context.SaveChangesAsync();
					model.Id = org_new.org_id;
				}

				var org_upd_h = await _context.Org_History.Where(x => x.org_id == model.Id && x.data_status == model.data_status).FirstOrDefaultAsync();
				if (org_upd_h != null)
				{
					org_upd_h.full_name = model.full_name;
					org_upd_h.short_name = model.short_name;
					org_upd_h.pozition_head_manager = model.pozition_head_manager;
					org_upd_h.l_name_head_manager = model.l_name_head_manager;
					org_upd_h.f_name_head_manager = model.f_name_head_manager;
					org_upd_h.m_name_head_manager = model.m_name_head_manager;
					org_upd_h.org_contact_phones = model.org_contact_phones;
					org_upd_h.org_emails = model.org_emails;
					org_upd_h.edit_date = DateTime.Now;
					org_upd_h.user_id = userId;
				}
				else
				{
                    var org_ds_last = await _context.Org_History.Where(x => x.org_id == model.Id && x.data_status <= model.data_status).OrderByDescending(x => x.data_status).FirstOrDefaultAsync();

					var org_new_his = new Org_History()
					{
						org_id = model.Id,
						data_status = model.data_status,
						org_status_id = org_ds_last != null ? org_ds_last.org_status_id : null,
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

				var tso_upd_h = await _context.TSO_History.Where(x => x.org_id == model.Id).FirstOrDefaultAsync();
				if (tso_upd_h != null)
				{
					tso_upd_h.code_tso = model.code_tso;
					tso_upd_h.notes = model.notes;
					tso_upd_h.org_property_type_id = model.org_property_type_id;
					tso_upd_h.eto_id = model.eto_id;
					tso_upd_h.org_size_own_capital = model.org_size_own_capital;
					tso_upd_h.year_own_capital = model.year_own_capital;
					tso_upd_h.tso_nds_pay = model.tso_nds_pay;
					tso_upd_h.send_letters_type_id = model.send_letters_type_id;
					tso_upd_h.edit_date = DateTime.Now;
					tso_upd_h.user_id = userId;
				}
				else
				{
					var tso_new_his = new TSO_History()
					{
						org_id = model.Id,
						data_status = model.data_status,
						code_tso = model.code_tso,
						notes = model.notes,
						org_property_type_id = model.org_property_type_id,
						eto_id = model.eto_id,
						org_size_own_capital = model.org_size_own_capital,
						year_own_capital = model.year_own_capital,
						tso_nds_pay = model.tso_nds_pay,
						send_letters_type_id = model.send_letters_type_id,
						is_deleted = false,
						create_date = DateTime.Now,
						user_id = userId
					};
					await _context.TSO_History.AddAsync(tso_new_his);
				}

				var tso_eto_list = await _context.TSO_ETOMapps.Where(x => x.tso_id == model.Id && x.data_status <= model.data_status).ToListAsync();

				if (model.etos != null || (model.etos == null
					&& tso_eto_list.Any(x => x.is_deleted == false)))
				{
					if (tso_eto_list.Count > 0) 
					{
						List<int> eto_delete;

						if (model.etos != null)
							eto_delete = tso_eto_list.Where(x => !model.etos.Any(y => y == x.eto_id) && x.is_deleted == false).Select(x => x.eto_id).Distinct().ToList();
						else
							eto_delete = tso_eto_list.Where(x => x.is_deleted == false).Select(x => x.eto_id).Distinct().ToList();

						if (eto_delete.Count > 0)
						{
							foreach (var eto_dels in eto_delete)
							{
								var tso_eto = tso_eto_list.Where(x => x.eto_id == eto_dels).OrderBy(x => x.data_status).LastOrDefault();
								if (tso_eto.is_deleted == false)
								{
									if (tso_eto.data_status == model.data_status)
									{
										var eto_upd = await _context.TSO_ETOMapps.Where(x => x.tso_id == model.Id && x.eto_id == eto_dels
											&& x.data_status == model.data_status).FirstOrDefaultAsync();
										eto_upd.is_deleted = true;
										eto_upd.edit_date = DateTime.Now;
									}
									else
									{
										var del_eto = GetNewTSO_ETOMappsModel(model.Id, model.data_status, eto_dels, true);
										await _context.TSO_ETOMapps.AddAsync(del_eto);
									}
								}
							}
						}
					}
					if (model.etos != null)
					{
						foreach (var eto_add in model.etos)
						{
							if (!tso_eto_list.Any(x => x.eto_id == eto_add))
							{
								var new_eto = GetNewTSO_ETOMappsModel(model.Id, model.data_status, eto_add, false);
								await _context.TSO_ETOMapps.AddAsync(new_eto);
							}
							else
							{
								var tso_eto = tso_eto_list.Where(x => x.eto_id == eto_add).OrderBy(x => x.data_status).LastOrDefault();

								if (tso_eto.is_deleted == true)
								{
									if (tso_eto.data_status == model.data_status)
									{
										var eto_upd = await _context.TSO_ETOMapps.Where(x => x.tso_id == model.Id && x.eto_id == eto_add
											&& x.data_status == model.data_status).FirstOrDefaultAsync();
										eto_upd.is_deleted = false;
										eto_upd.edit_date = DateTime.Now;
										eto_upd.user_id = userId;
									}
									else
									{
										var new_eto = GetNewTSO_ETOMappsModel(model.Id, model.data_status, eto_add, false);
										await _context.TSO_ETOMapps.AddAsync(new_eto);
									}
								}
							}
						}
					}
						
				}

				await _context.SaveChangesAsync();
				//return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
				return Json(new { success = true, is_new_tso, tso_id = model.Id, unom_tso = unom_org + " (" + model.short_name + ")" });
			}
			catch (Exception ex)
			{
				return Json(new { success = false });
			}
		}
		#endregion

		#region DELETE_DATA
		// Удаление ТСО
		[TypeFilter(typeof(ControllerActionFilterCheckDS))]
		public async Task<IActionResult> TSO_Delete(int tso_Id, int data_status)
		{
			if (tso_Id > 0)
			{
				try
				{
					if (await _context.Organisations.AnyAsync(x => x.org_id == tso_Id))
					{
						var org_his_upd = await _context.Org_History.Where(x => x.org_id == tso_Id).OrderByDescending(x => x.data_status).FirstOrDefaultAsync();

						if (org_his_upd.is_deleted == false)
						{
							var tso_his_upd = await _context.TSO_History.Where(x => x.org_id == tso_Id).OrderByDescending(x => x.data_status).FirstOrDefaultAsync();
							if (org_his_upd.data_status == data_status)
							{
								org_his_upd.is_deleted = true;
								org_his_upd.edit_date = DateTime.Now;
								org_his_upd.user_id = userId;
							}
							else
							{
								var org_new_his = new Org_History()
								{
									org_id = tso_Id,
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
							}

							if (tso_his_upd.data_status == data_status)
							{
								tso_his_upd.is_deleted = true;
								tso_his_upd.edit_date = DateTime.Now;
								tso_his_upd.user_id = userId;
							}
							else
							{
								var tso_new_his = new TSO_History()
								{
									org_id = tso_Id,
									data_status = data_status,
									code_tso = tso_his_upd.code_tso,
									org_property_type_id = tso_his_upd.org_property_type_id,
									eto_id = tso_his_upd.eto_id,
									org_size_own_capital = tso_his_upd.org_size_own_capital,
									year_own_capital = tso_his_upd.year_own_capital,
									tso_nds_pay = tso_his_upd.tso_nds_pay,
									send_letters_type_id = tso_his_upd.send_letters_type_id,
									is_deleted = true,
									create_date = DateTime.Now,
									user_id = userId
								};
								await _context.TSO_History.AddAsync(tso_new_his);
							}
							await _context.SaveChangesAsync();
						}
					}
				}
				catch (Exception ex)
				{

				}
			}
			return Json(new { success = true });
		}

        //Удаление ответственного лица
        /*public async Task<IActionResult> TSO_DeletePerson(int tso_Id, int data_status, int person_num)
        {
            if (tso_Id > 0 && person_num > 0)
            {
                try
                {
                    if (await _context.ResponsiblePersons.AnyAsync(x => x.org_id == tso_Id && x.person_num == person_num))
                    {
                        var resp_pers_upd = await _context.ResponsiblePersons.Where(x => x.org_id == tso_Id
                            && x.person_num == person_num).FirstOrDefaultAsync();

                        if (resp_pers_upd.data_status == data_status)
                        {
                            resp_pers_upd.is_deleted = true;
                            resp_pers_upd.edit_date = DateTime.Now;
                            resp_pers_upd.user_id = userId;
                        }
                        else
                        {
                            var resp_pers_new = new ResponsiblePersons
                            {
                                org_id = tso_Id,
                                data_status = data_status,
                                person_num = person_num,
                                position_respons_pers = resp_pers_upd.position_respons_pers,
                                l_name_respons_pers = resp_pers_upd.l_name_respons_pers,
                                f_name_respons_pers = resp_pers_upd.f_name_respons_pers,
                                m_name_respons_pers = resp_pers_upd.m_name_respons_pers,
                                phones_respons_pers = resp_pers_upd.phones_respons_pers,
                                emails_respons_pers = resp_pers_upd.emails_respons_pers,
                                is_deleted = true,
                                create_date = DateTime.Now,
                                user_id = userId
                            };
                            await _context.AddAsync(resp_pers_new);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return Json(new { success = true });
        }*/
        #endregion

        //Создание модели для добавления новой ЕТО в ТСО
        [NonAction]
		public TSO_ETOMapps GetNewTSO_ETOMappsModel(int tso_id, int data_status, int eto_id, bool? is_deleted)
		{
			var new_eto = new TSO_ETOMapps()
			{
				tso_id = tso_id,
				data_status = data_status,
				eto_id = eto_id,
				is_deleted = is_deleted,
				create_date = DateTime.Now,
				user_id = userId
			};
			return new_eto;
		}

		#region ViewComponents
		//список ТСО
		public ActionResult OnGetCallTSOList_PartialViewComponent(int? data_status, int? perspective_year, byte only_reg_contract, byte only_liquidate)
        {
            return ViewComponent("TSOList_Partial", new { data_status, perspective_year, only_reg_contract, only_liquidate, userId });
        }

        //параметры ТСО по годам расчётного периода схемы
        public ActionResult OnGetCallTSO_PerspectiveList_PartialViewComponent(int data_status, int tso_id)
        {
            return ViewComponent("TSO_PerspectiveList_Partial", new { data_status, tso_id, userId });
        }

		//ответственные лица за подготовку данных по схеме
		public ActionResult OnGetCallTSO_RespPersList_PartialViewComponent(int data_status, int tso_id)
		{
			return ViewComponent("TSO_ResponsiblePersonsList_Partial", new { data_status, tso_id, userId });
		}

        //загруженный список ТСО из Excel
        public ActionResult OnGetCallTSO_UploadedList_PartialViewComponent(int data_status)
        {
            return ViewComponent("TSO_UploadedList_Partial", new { data_status, userId });
        }
        #endregion

    }
}
