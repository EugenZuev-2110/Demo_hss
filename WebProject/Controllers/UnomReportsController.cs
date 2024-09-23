using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProject.Models;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using System.Data;
using DataBase.Models;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using FastMember;
using ClosedXML.Excel;
using System.ComponentModel;

namespace WebProject.Controllers
{
    public class UnomReportsController : Controller
    {
        private readonly ILogger<UnomReportsController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string? _user;
        private int userId;
        private string? userDisplayName;
        //private PrincipalContext _ctx = new PrincipalContext(ContextType.Domain);
        public UnomReportsController(ILogger<UnomReportsController> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment)
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
            else
            {
                userId = 34;
            }
        }

        #region View
        //Отчёты
        public IActionResult Reports()
        {
            return View();
        }
        //Общее действие для выгрузки отчётов
        public async Task<ActionResult> ReportExport(DateTime? startDate, DateTime? endDate, int report_type)
        {
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;

            if (startDate == null || endDate == null)
            {
                startDate = DateTime.Now;
                endDate = DateTime.Now;
            }
            endDate = endDate.Value.AddDays(1);

            if (userId > 0 || host.Contains("localhost"))
            {
                //if (host.Contains("localhost"))
                //    host = "http://" + host;
                //else
                //    host = "https://" + host;

                host = "http://" + host;

                var result = new string[2];
                
                if (report_type > 0 || report_type == -1)
                {
                    if (report_type == 1)
                    {
                        result = await ReportOthers_t15_Export(startDate, endDate, host);
                    }
                    else if (report_type == 2)
                    {
                        result = await ReportTechConnection_t9_Export(startDate, endDate, host);
                    }
                    else if (report_type == 3)
                    {
                        result = await ReportDecommissioning_t9_Export(startDate, endDate, host);
                    }
                    else if (report_type == 4)
                    {
                        result = await ReportOthers_t9_Export(startDate, endDate, host);
                    }
                    else if (report_type == 5)
                    {
                        result = await ReportAll_t9_Export(startDate, endDate, host);
                    }
                    else if (report_type == 6)
                    {
                        result = await ReportTechConnection_t15_Export(startDate, endDate, host);
                    }
                    else if (report_type == 7)
                    {
                        result = await ReportDecommissioning_t15_Export(startDate, endDate, host);
                    }
                    else if (report_type == 8)
                    {
                        result = await ReportKSIO_t15_Export(startDate, endDate, host);
                    }
                    else if (report_type == 9)
                    {
                        result = await ReportAll_t15_Export(startDate, endDate, host);
                    }
                    else if (report_type == -1)
                    {
                        result = await ReportUnomItemsCheckList_Export(startDate, endDate, host);
                    }
                    return Json(new { success = result[0], filename = result[1] });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            else
            {
                return RedirectToAction("UnomReports", "Reports");
            }
        }

        public String[] ToArray(object model_name, int type_values)
        {
            List<string> arr = new List<string>();

            foreach (var prop in model_name.GetType().GetProperties())
            {
                string value = "";
                if (type_values == 1)
                {
                    if (prop.Name != null)
                    {
                        value = prop.Name;
                    }
                }
                else
                {
                    var attribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().Single();
                    if (prop.Name != null)
                    {
                        value = attribute.DisplayName;
                    }
                }
                
                arr.Add(value);
            }

            return arr.ToArray();
        }

        //Формирование проверочного отчёта по заполненности Уномов и Пунктов
        public async Task<string[]> ReportUnomItemsCheckList_Export(DateTime? startDate, DateTime? endDate, string host)
        {
            try
            {
                List<UnomItemsCheckListViewModel> rep1 = await _context.UnomItemsCheckListViewModel.FromSqlInterpolated($"exec sp_GetUnomItemsCheckList {startDate},{endDate}").ToListAsync();
                await _context.DisposeAsync();

                DataTable dataTable = new DataTable();

                var arr = ToArray(new UnomItemsCheckListViewModel(), 1);

                using (var reader = ObjectReader.Create(rep1, arr))
                {
                    dataTable.Load(reader);
                }
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Проверочный отчёт");

                var arr_names = ToArray(new UnomItemsCheckListViewModel(), 2);
                for (int i = 1; i <= arr_names.Count(); i++)
                {
                    ws.Cell(1, i).Value = arr_names[i-1];
                }

                ws.Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Row(1).Height = 60;
                ws.Columns("1-28").Width = 15;
                ws.Style.Alignment.WrapText = true;
                ws.Cell(2, 1).InsertData(dataTable);

                var rngTable = ws.Range($"A1:AB{dataTable.Rows.Count + 1}");
                //rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                //rngTable.Style.Font.FontName = "Times New Roman";

                foreach (var t in rngTable.Rows())
                {
                    foreach (var cell in t.Cells())
                    {
                        if (cell.Value.ToString() == " - ")
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var filename = "/ReportsDownload/проверочный_отчёт_userid_" + userId + ".xlsx";
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stream excelStream = System.IO.File.Create(webRootPath + filename);
                wb.SaveAs(excelStream);
                excelStream.Dispose();

                return new string[] { "true", host + filename };
            }
            catch (Exception ex)
            {
                return new string[] { "false", ex.Message };
            }
        }

        //Формирование отчёта "Прочие" для Тома 15 (по категориям ППТ, ЕТО, КРТ, Градпотенциал, Прочие)
        public async Task<string[]> ReportOthers_t15_Export(DateTime? startDate, DateTime? endDate, string host)
        {
            try
            {
                List<ReportOther_t15_ViewModel> rep1 = await _context.ReportOther_t15_ViewModel.FromSqlInterpolated($"exec sp_GetReportOther_t15_List {startDate},{endDate}").ToListAsync();
                await _context.DisposeAsync();

                DataTable dataTable = new DataTable();

                using (var reader = ObjectReader.Create(rep1, "RowNumber", "organisation_name", "dzhkh_date", "dzhkh_number", "appeal_desc_short", "district_name", "source_name", "justification", "res_calc", "result_review", "changes_is_required", "unom_num"))
                {
                    dataTable.Load(reader);
                }
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Прочие");
                ws.Range("A1:A2").Merge();
                ws.Range("B1:B2").Merge();
                ws.Range("C1:D1").Merge();
                ws.Range("E1:E2").Merge();
                ws.Range("F1:F2").Merge();
                ws.Range("G1:G2").Merge();
                ws.Range("H1:H2").Merge();
                ws.Range("I1:I2").Merge();
                ws.Range("J1:K1").Merge();
                ws.Range("L1:L2").Merge();

                ws.Cell("A1").Value = "№ п/п";
                ws.Cell("B1").Value = "Наименование организации";
                ws.Cell("C1").Value = "Реквизиты обращения, зарегистрированные в ДЖКХ г. Москвы";
                ws.Cell("C2").Value = "Дата";
                ws.Cell("D2").Value = "Номер";
                ws.Cell("E1").Value = "Предложение заявителя в обращении";
                ws.Cell("F1").Value = "Административный округ, в который попадает";
                ws.Cell("G1").Value = "Зона действия источника, в которую попадает объект обращения";
                ws.Cell("H1").Value = "Технико-экономическое обоснование (при необходимости)";
                ws.Cell("I1").Value = "Результат гидравлического расчета (при необходимости)";
                ws.Cell("J1").Value = "Вывод и заключения по результатам рассмотрения обращения";
                ws.Cell("J2").Value = "Результат рассмотрения обращения";
                ws.Cell("K2").Value = "Необходимость внесения изменений в актуализацию схемы теплоснабжения";
                ws.Cell("L1").Value = "УНОМ";

                ws.Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Columns("2,4,5,7,8,9,10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                ws.FirstRow().Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Range("J2:K2").Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Column(1).Width = 10;
                ws.Columns(2, 3).Width = 15;
                ws.Column(4).Width = 20;
                ws.Column(5).Width = 50;
                ws.Columns(6,9).Width = 18;
                ws.Column(10).Width = 100;
                ws.Column(11).Width = 50;
                ws.Row(1).Height = 40;
                ws.Row(2).Height = 80;
                ws.Style.Alignment.WrapText = true;
                ws.Cell(3,1).InsertData(dataTable);
                    
                var rngTable = ws.Range($"A1:L{dataTable.Rows.Count+2}");
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Font.FontName = "Times New Roman";

                foreach (var t in rngTable.Rows())
                {
                    foreach (var cell in t.Cells())
                    {
                        if (cell.Value.ToString() == " - ")
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var filename = "/ReportsDownload/прочие_том_15_userid_" + userId + ".xlsx";
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stream excelStream = System.IO.File.Create(webRootPath + filename);
                wb.SaveAs(excelStream);
                excelStream.Dispose();

                return new string[] { "true", host + filename };
            }
            catch (Exception ex)
            {
                return new string[] { "false", ex.Message };
            }
        }

        //Формирование отчёта "Техприс" для Тома 9 (табл. 2.1) (по категории Техприс)
        public async Task<string[]> ReportTechConnection_t9_Export(DateTime? startDate, DateTime? endDate, string host)
        {
            try
            {
                List<ReportTechConViewModel> rep2 = await _context.ReportTechConViewModel.FromSqlInterpolated($"exec sp_GetReportTechConList {startDate},{endDate}").ToListAsync();
                await _context.DisposeAsync();

                DataTable dataTable = new DataTable();

                using (var reader = ObjectReader.Create(rep2, "RowNumber", "organisation_name", "dzhkh_date", "dzhkh_number", "appeal_desc_short", "result_review", "source_name", "district_name", "is_appr_scheme_exist", "changes_is_required", "changes_type", "unom_num"))
                {
                    dataTable.Load(reader);
                }
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Техприс");
                ws.Range("A1:A3").Merge();
                ws.Range("B1:E1").Merge();
                ws.Range("B2:B3").Merge();
                ws.Range("C2:D2").Merge();
                ws.Range("E2:E3").Merge();
                ws.Range("F1:F3").Merge();
                ws.Range("G1:G3").Merge();
                ws.Range("H1:H3").Merge();
                ws.Range("I1:I3").Merge();
                ws.Range("J1:J3").Merge();
                ws.Range("K1:K3").Merge();
                ws.Range("L1:L3").Merge();

                ws.Cell("A1").Value = "№ п/п";
                ws.Cell("B1").Value = "Сведения об обращении";
                ws.Cell("B2").Value = "Наименование организации";
                ws.Cell("C2").Value = "Реквизиты обращения, зарегистрированные в ДЖКХ г. Москвы";
                ws.Cell("C3").Value = "Дата";
                ws.Cell("D3").Value = "Номер";
                ws.Cell("E2").Value = "Краткое содержание обращения";
                ws.Cell("F1").Value = "Результат рассмотрения обращения";
                ws.Cell("G1").Value = "Наименование источника";
                ws.Cell("H1").Value = "Административный округ";
                ws.Cell("I1").Value = "Подключаемый объект учтен в утвержденной Актуализации схемы (приказ Минэнерго России от 02.11.2022 №1183)";
                ws.Cell("J1").Value = "Необходимость рассмотрения предложения заявителя в настоящей Актуализации схемы";
                ws.Cell("K1").Value = "Перечень мероприятий, необходимых к учету в схеме теплоснабжения";
                ws.Cell("L1").Value = "УНОМ";

                ws.Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Columns("2,4,5,7,8,9,10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //ws.FirstRow().Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Range("J2:K2").Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Column(1).Width = 10;
                ws.Columns(2, 3).Width = 15;
                ws.Column(4).Width = 20;
                ws.Column(5).Width = 50;
                ws.Column(6).Width = 100;
                ws.Columns("7,8,9,10").Width = 20;
                ws.Column(11).Width = 100;

                ws.Row(2).Height = 90;
                ws.Style.Alignment.WrapText = true;
                ws.Cell(4, 1).InsertData(dataTable);

                var rngTable = ws.Range($"A1:L{dataTable.Rows.Count + 3}");
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Font.FontName = "Times New Roman";

                foreach (var t in rngTable.Rows())
                {
                    foreach (var cell in t.Cells())
                    {
                        if (cell.Value.ToString() == " - ")
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var filename = "/ReportsDownload/техприс_том_9_userid_" + userId + ".xlsx";
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stream excelStream = System.IO.File.Create(webRootPath + filename);
                wb.SaveAs(excelStream);
                excelStream.Dispose();

                return new string[] { "true", host + filename };
            }
            catch (Exception ex)
            {
				
				return new string[] { "false", ex.Message };
            }
        }

        //Формирование отчёта по "Выводам" для Тома 10 (табл. 2.2) (по категории Вывод (из эксплуатации))
        public async Task<string[]> ReportDecommissioning_t9_Export(DateTime? startDate, DateTime? endDate, string host)
        {
            try
            {
                List<ReportDecommissioningViewModel> rep2 = await _context.ReportDecommissioningViewModel.FromSqlInterpolated($"exec sp_GetReportDecommissioningList {startDate},{endDate}").ToListAsync();
                await _context.DisposeAsync();

                DataTable dataTable = new DataTable();

                using (var reader = ObjectReader.Create(rep2, "RowNumber", "organisation_name", "dzhkh_date", "dzhkh_number", "appeal_desc_short", "result_review", "source_name", "district_name", "is_appr_scheme_exist", "changes_is_required", "unom_num"))
                {
                    dataTable.Load(reader);
                }
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Вывод");
                ws.Range("A1:A3").Merge();
                ws.Range("B1:E1").Merge();
                ws.Range("B2:B3").Merge();
                ws.Range("C2:D2").Merge();
                ws.Range("E2:E3").Merge();
                ws.Range("F1:F3").Merge();
                ws.Range("G1:G3").Merge();
                ws.Range("H1:H3").Merge();
                ws.Range("I1:I3").Merge();
                ws.Range("J1:J3").Merge();
                ws.Range("K1:K3").Merge();

                ws.Cell("A1").Value = "№ п/п";
                ws.Cell("B1").Value = "Сведения об обращении";
                ws.Cell("B2").Value = "Наименование организации";
                ws.Cell("C2").Value = "Реквизиты обращения, зарегистрированные в ДЖКХ г. Москвы";
                ws.Cell("C3").Value = "Дата";
                ws.Cell("D3").Value = "Номер";
                ws.Cell("E2").Value = "Краткое содержание обращения";
                ws.Cell("F1").Value = "Результат рассмотрения обращения";
                ws.Cell("G1").Value = "Наименование источника";
                ws.Cell("H1").Value = "Административный округ";
                ws.Cell("I1").Value = "Учтено в утвержденной Актуализации схемы (приказ Минэнерго России от 02.11.2022 №1183)";
                ws.Cell("J1").Value = "Необходимость рассмотрения предложения заявителя в настоящей Актуализации схемы";
                ws.Cell("K1").Value = "УНОМ";

                ws.Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Columns("2,4,5,7,8,9,10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //ws.FirstRow().Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Range("J2:K2").Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Column(1).Width = 10;
                ws.Columns(2, 3).Width = 15;
                ws.Column(4).Width = 20;
                ws.Column(5).Width = 50;
                ws.Column(6).Width = 100;
                ws.Columns("7,8,9,10").Width = 20;

                ws.Row(2).Height = 90;
                ws.Style.Alignment.WrapText = true;
                ws.Cell(4, 1).InsertData(dataTable);

                var rngTable = ws.Range($"A1:K{dataTable.Rows.Count + 3}");
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Font.FontName = "Times New Roman";

                foreach (var t in rngTable.Rows())
                {
                    foreach (var cell in t.Cells())
                    {
                        if (cell.Value.ToString() == " - ")
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var filename = "/ReportsDownload/вывод_том_9_userid_" + userId + ".xlsx";
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stream excelStream = System.IO.File.Create(webRootPath + filename);
                wb.SaveAs(excelStream);
                excelStream.Dispose();

                return new string[] { "true", host + filename };
            }
            catch (Exception ex)
            {
                return new string[] { "false", ex.Message };
            }
        }

        //Формирование отчёта "Прочие" для Тома 9 (табл. 2.3) (по категориям КСИО, ППТ, ЕТО, КРТ, Градпотенциал, Прочие)
        public async Task<string[]> ReportOthers_t9_Export(DateTime? startDate, DateTime? endDate, string host)
        {
            try
            {
                List<ReportOthers_t9_ViewModel> rep2 = await _context.ReportOthers_t9_ViewModel.FromSqlInterpolated($"exec sp_GetReportOthers_t9_List {startDate},{endDate}").ToListAsync();
                await _context.DisposeAsync();

                DataTable dataTable = new DataTable();

                using (var reader = ObjectReader.Create(rep2, "RowNumber", "organisation_name", "dzhkh_date", "dzhkh_number", "appeal_desc_short", "result_review", "source_name", "district_name", "is_appr_scheme_exist", "changes_is_required", "unom_num"))
                {
                    dataTable.Load(reader);
                }
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Прочие_том_9");
                ws.Range("A1:A3").Merge();
                ws.Range("B1:E1").Merge();
                ws.Range("B2:B3").Merge();
                ws.Range("C2:D2").Merge();
                ws.Range("E2:E3").Merge();
                ws.Range("F1:F3").Merge();
                ws.Range("G1:G3").Merge();
                ws.Range("H1:H3").Merge();
                ws.Range("I1:I3").Merge();
                ws.Range("J1:J3").Merge();
                ws.Range("K1:K3").Merge();

                ws.Cell("A1").Value = "№ п/п";
                ws.Cell("B1").Value = "Сведения об обращении";
                ws.Cell("B2").Value = "Наименование организации";
                ws.Cell("C2").Value = "Реквизиты обращения, зарегистрированные в ДЖКХ г. Москвы";
                ws.Cell("C3").Value = "Дата";
                ws.Cell("D3").Value = "Номер";
                ws.Cell("E2").Value = "Краткое содержание обращения";
                ws.Cell("F1").Value = "Результат рассмотрения обращения";
                ws.Cell("G1").Value = "Наименование источника";
                ws.Cell("H1").Value = "Административный округ";
                ws.Cell("I1").Value = "Учтено в утвержденной Актуализации схемы (приказ Минэнерго России от 02.11.2022 №1183)";
                ws.Cell("J1").Value = "Необходимость рассмотрения предложения заявителя в настоящей Актуализации схемы";
                ws.Cell("K1").Value = "УНОМ";

                ws.Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Columns("2,4,5,7,8,9,10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //ws.FirstRow().Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Range("J2:K2").Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Column(1).Width = 10;
                ws.Columns(2, 3).Width = 15;
                ws.Column(4).Width = 20;
                ws.Column(5).Width = 50;
                ws.Column(6).Width = 100;
                ws.Columns("7,8,9,10").Width = 20;

                ws.Row(2).Height = 90;
                ws.Style.Alignment.WrapText = true;
                ws.Cell(4, 1).InsertData(dataTable);

                var rngTable = ws.Range($"A1:K{dataTable.Rows.Count + 3}");
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Font.FontName = "Times New Roman";

                foreach (var t in rngTable.Rows())
                {
                    foreach (var cell in t.Cells())
                    {
                        if (cell.Value.ToString() == " - ")
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var filename = "/ReportsDownload/прочие_том_9_userid_" + userId + ".xlsx";
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stream excelStream = System.IO.File.Create(webRootPath + filename);
                wb.SaveAs(excelStream);
                excelStream.Dispose();

                return new string[] { "true", host + filename };
            }
            catch (Exception ex)
            {
                return new string[] { "false", ex.Message };
            }
        }

        //Формирование отчёта "Сводный реестр" для Тома 10 (табл. 3.1) (по ВСЕМ категориям)
        public async Task<string[]> ReportAll_t9_Export(DateTime? startDate, DateTime? endDate, string host)
        {
            try
            {
                List<ReportAll_t9_ViewModel> rep2 = await _context.ReportAll_t9_ViewModel.FromSqlInterpolated($"exec sp_GetReportAll_t9_List {startDate},{endDate}").ToListAsync();
                await _context.DisposeAsync();

                DataTable dataTable = new DataTable();

                using (var reader = ObjectReader.Create(rep2, "RowNumber", "organisation_name", "dzhkh_date", "dzhkh_number", "appeal_desc_short", "result_review", "tso_events", "changes_type",
                    "conditional_address", "district_name", "building_purpose", "fact_area", "d_heat", "d_vent", "d_gvs_a", "d_heat_curtain", "d_cond", "d_other", "d_load_sum",
                    "UP_date", "UP_num", "fact_start", "exists_aip", "building_permits_num", "project_num_ip", "realiz_period_ip", "s_num", "source_address", "source_name",
                    "q_inst_heat", "q_netto_heat", "eto_num", "eto_name", "hssn_num", "unom_num"))
                {
                    dataTable.Load(reader);
                }
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Сводный_реестр_том_10");
                ws.Range("A1:A3").Merge();
                ws.Range("B1:E1").Merge();
                ws.Range("B1:E1").Merge();
                ws.Range("B2:B3").Merge();
                ws.Range("C2:D2").Merge();
                ws.Range("E2:E3").Merge();
                ws.Range("F1:F3").Merge();
                ws.Range("G1:G3").Merge();
                ws.Range("H1:H3").Merge();
                ws.Range("I1:X1").Merge();
                ws.Range("I2:J2").Merge();
                ws.Range("K2:K3").Merge();
                ws.Range("L2:L3").Merge();
                ws.Range("M2:S2").Merge();
                ws.Range("T2:U2").Merge();
                ws.Range("V2:V3").Merge();
                ws.Range("W2:W3").Merge();
                ws.Range("X2:X3").Merge();
                ws.Range("Y1:Z2").Merge();
                ws.Range("AA1:AE1").Merge();
                ws.Range("AA2:AA3").Merge();
                ws.Range("AB2:AB3").Merge();
                ws.Range("AC2:AC3").Merge();
                ws.Range("AD2:AD3").Merge();
                ws.Range("AE2:AE3").Merge();
                ws.Range("AF1:AH2").Merge();
                ws.Range("AI1:AI3").Merge();

                ws.Cell("A1").Value = "№ п/п";
                ws.Cell("B1").Value = "Сведения об обращении";
                ws.Cell("B2").Value = "Наименование организации";
                ws.Cell("C2").Value = "Реквизиты обращения, зарегистрированные в ДЖКХ г. Москвы";
                ws.Cell("C3").Value = "Дата";
                ws.Cell("D3").Value = "Номер";
                ws.Cell("E2").Value = "Краткое содержание обращения";
                ws.Cell("F1").Value = "Результат рассмотрения обращения";
                ws.Cell("G1").Value = "Перечень мероприятий, предложенных ТСО";
                ws.Cell("H1").Value = "Перечень изменений, необходимых для учета в актуализации схемы теплоснабжения";
                ws.Cell("I1").Value = "Информация о новых или реконструируемых объектах капитального строительства";
                ws.Cell("I2").Value = "Местоположение";
                ws.Cell("I3").Value = "Адрес";
                ws.Cell("J3").Value = "Административный округ";
                ws.Cell("K2").Value = "Назначение объекта";
                ws.Cell("L2").Value = "Общая площадь объекта, м2";
                ws.Cell("M2").Value = "Структура заявленной тепловой нагрузки, Гкал/ч";
                ws.Cell("M3").Value = "Отопление";
                ws.Cell("N3").Value = "Вентиляция";
                ws.Cell("O3").Value = "ГВС ср.ч.";
                ws.Cell("P3").Value = "Тепловые завесы";
                ws.Cell("Q3").Value = "Кондициониро-вание";
                ws.Cell("R3").Value = "Прочее";
                ws.Cell("S3").Value = "Всего";
                ws.Cell("T2").Value = "Реквизиты заявки на технологическое присоединение (реквизиты условий подключения)";
                ws.Cell("T3").Value = "Дата заявки";
                ws.Cell("U3").Value = "№ заявки";
                ws.Cell("V2").Value = "Срок присоединения объекта";
                ws.Cell("W2").Value = "Сведения о включении объекта в АИП г. Москвы (да / нет)";
                ws.Cell("X2").Value = "Номер разрешения на строительство (при наличии)";
                ws.Cell("Y1").Value = "Сведения о наличии мероприятий в утвержденной инвестиционной программе";
                ws.Cell("Y3").Value = "№ проекта";
                ws.Cell("Z3").Value = "Срок реализации";
                ws.Cell("AA1").Value = "Сведения об источнике теплоснабжения";
                ws.Cell("AA2").Value = "Код источника";
                ws.Cell("AB2").Value = "Адрес источника";
                ws.Cell("AC2").Value = "Наименование источника";
                ws.Cell("AD2").Value = "Установленная тепловая мощность, Гкал/ч";
                ws.Cell("AE2").Value = "Тепловая мощность нетто, Гкал/ч";
                ws.Cell("AF1").Value = "Сведения о теплоснабжающей организации";
                ws.Cell("AF3").Value = "Код ЕТО";
                ws.Cell("AG3").Value = "Наименование ЕТО";
                ws.Cell("AH3").Value = "№ системы теплоснабжения";
                ws.Cell("AI1").Value = "УНОМ";

                ws.Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Columns("2,4,5,7,8,9,10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //ws.FirstRow().Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Range("J2:K2").Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Column(1).Width = 10;
                ws.Columns("2,3,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,29,30,31,34").Width = 15;
                ws.Columns("4,9,10,11,28,33").Width = 20;
                ws.Columns("5,7,8").Width = 50;
                ws.Column(6).Width = 100;

                ws.Rows(2,3).Height = 90;
                ws.Style.Alignment.WrapText = true;
                ws.Cell(4, 1).InsertData(dataTable);

                var rngTable = ws.Range($"A1:AI{dataTable.Rows.Count + 3}");
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Font.FontName = "Times New Roman";

                foreach (var t in rngTable.Rows())
                {
                    foreach (var cell in t.Cells())
                    {
                        if (cell.Value.ToString() == " - ")
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var filename = "/ReportsDownload/сводный_реестр_том_10_userid_" + userId + ".xlsx";
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stream excelStream = System.IO.File.Create(webRootPath + filename);
                wb.SaveAs(excelStream);
                excelStream.Dispose();

                return new string[] { "true", host + filename };
            }
            catch (Exception ex)
            {
                return new string[] { "false", ex.Message };
            }
        }

        //Формирование отчёта "КСИО" для Тома 15 (табл. 1.3) (по категории КСИО)
        public async Task<string[]> ReportKSIO_t15_Export(DateTime? startDate, DateTime? endDate, string host)
        {
            try
            {
                List<ReportKSIO_t15_ViewModel> rep2 = await _context.ReportKSIO_t15_ViewModel.FromSqlInterpolated($"exec sp_GetReportKSIO_t15_List {startDate},{endDate}").ToListAsync();
                await _context.DisposeAsync();

                DataTable dataTable = new DataTable();

                using (var reader = ObjectReader.Create(rep2, "RowNumber", "ksio_name", "dzhkh_date", "dzhkh_number", "source_name", "district_name", "decl_area", "heat_sum", 
                    "vent_sum", "gvs_a_sum", "heat_curtain_sum", "cond_sum", "other_sum", "load_sum_gvs_a", "objects_list_comments", "heat_loads_comments", "dev_events_comments",
                    "others_comments", "out_appeal_date", "out_appeal_number", "result_review", "date_ppt", "num_ppt", "ksio_is_agree", "exists_ksio", "date_ksio", "num_ksio", "unom_num"))
                {
                    dataTable.Load(reader);
                }
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("КСИО_том_15");
                ws.Range("A1:A3").Merge();
                ws.Range("B1:B3").Merge();
                ws.Range("C1:D1").Merge();
                ws.Range("C2:C3").Merge();
                ws.Range("D2:D3").Merge();
                ws.Range("E1:E3").Merge();
                ws.Range("F1:F3").Merge();
                ws.Range("G1:N1").Merge();
                ws.Range("G2:G3").Merge();
                ws.Range("H2:N2").Merge();
                ws.Range("O1:O3").Merge();
                ws.Range("P1:P3").Merge();
                ws.Range("Q1:Q3").Merge();
                ws.Range("R1:R3").Merge();
                ws.Range("S1:T1").Merge();
                ws.Range("S2:S3").Merge();
                ws.Range("T2:T3").Merge();
                ws.Range("U1:U3").Merge();
                ws.Range("V1:W1").Merge();
                ws.Range("V2:V3").Merge();
                ws.Range("W2:W3").Merge();
                ws.Range("X1:AA1").Merge();
                ws.Range("X2:X3").Merge();
                ws.Range("Y2:Y3").Merge();
                ws.Range("Z2:AA2").Merge();
                ws.Range("AB1:AB3").Merge();

                ws.Cell("A1").Value = "№ п/п";
                ws.Cell("B1").Value = "Наименование КСИО";
                ws.Cell("C1").Value = "Реквизиты обращения, зарегистрированные в ДЖКХ г. Москвы";
                ws.Cell("C2").Value = "Дата";
                ws.Cell("D2").Value = "Номер";
                ws.Cell("E1").Value = "Источники тепслонабжения указаннных территорий";
                ws.Cell("F1").Value = "Адиминистративный округ";
                ws.Cell("G1").Value = "Сведения об объектах капитального строительства в соответствии с КСИО";
                ws.Cell("G2").Value = "Суммарная поэтажная площадь, м²";
                ws.Cell("H2").Value = "Тепловая нагрузка, Гкал/ч";
                ws.Cell("H3").Value = "Отопление";
                ws.Cell("I3").Value = "Вентиляция";
                ws.Cell("J3").Value = "ГВСср.ч.";
                ws.Cell("K3").Value = "Тепловые завесы";
                ws.Cell("L3").Value = "Кондиционирование";
                ws.Cell("M3").Value = "Прочее";
                ws.Cell("N3").Value = "Нагрузка суммарная (с ГВСср.ч.)";
                ws.Cell("O1").Value = "Анализ перечня объектов капитального строительства";
                ws.Cell("P1").Value = "Анализ расчетов тепловых нагрузок вводимых потребителей";
                ws.Cell("Q1").Value = "Анализ разработанных мероприятий и результатов выполненных расчетов гидравлических режимов работы тепловых сетей";
                ws.Cell("R1").Value = "Прочие замечания к материалам КСИО";
                ws.Cell("S1").Value = "Реквизиты заключения";
                ws.Cell("S2").Value = "Дата";
                ws.Cell("T2").Value = "Номер ответного";
                ws.Cell("U1").Value = "Выводы о возможности принятия решений о включении в схему теплоснабжения изменений по итогам рассмотрения";
                ws.Cell("V1").Value = "ППТ, на основании которого разработано КСИО";
                ws.Cell("V2").Value = "Дата утверждения ППТ";
                ws.Cell("W2").Value = "Номер приказа об утверждении";
                ws.Cell("X1").Value = "Окончательный результат рассмотрения на момент актуализации схемы теплоснабжения";
                ws.Cell("X2").Value = "Наличие согласованного проекта КСИО (Да/Нет)";
                ws.Cell("Y2").Value = "Утверждение материалов КСИО (Да/Нет)";
                ws.Cell("Z2").Value = "Реквизиты распоряжения об утверждении КСИО";
                ws.Cell("Z3").Value = "Дата";
                ws.Cell("AA3").Value = "Номер";
                ws.Cell("AB1").Value = "УНОМ";

                ws.Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Columns("2,4,5,7,8,9,10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //ws.FirstRow().Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Range("J2:K2").Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Column(1).Width = 10;
                ws.Columns("2,3,6,7,8,9,10,11,12,13,14,19,20,22,23,24,25,26,27").Width = 15;
                ws.Columns("4,5").Width = 20;
                ws.Columns("15,16,17,18").Width = 50;
                ws.Column(21).Width = 100;

                ws.Row(1).Height = 30;
                ws.Rows(2, 3).Height = 90;
                ws.Style.Alignment.WrapText = true;
                ws.Cell(4, 1).InsertData(dataTable);

                var rngTable = ws.Range($"A1:AB{dataTable.Rows.Count + 3}");
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Font.FontName = "Times New Roman";

                foreach (var t in rngTable.Rows())
                {
                    foreach (var cell in t.Cells())
                    {
                        if (cell.Value.ToString() == " - ")
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var filename = "/ReportsDownload/ксио_том_15_userid_" + userId + ".xlsx";
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stream excelStream = System.IO.File.Create(webRootPath + filename);
                wb.SaveAs(excelStream);
                excelStream.Dispose();

                return new string[] { "true", host + filename };
            }
            catch (Exception ex)
            {
                return new string[] { "false", ex.Message };
            }
        }

        //Формирование отчёта "Техприс" для Тома 15 (табл. 1.1) (по категории Техприс)
        public async Task<string[]> ReportTechConnection_t15_Export(DateTime? startDate, DateTime? endDate, string host)
        {
            try
            {
                List<ReportTechCon_t15_ViewModel> rep2 = await _context.ReportTechCon_t15_ViewModel.FromSqlInterpolated($"exec sp_GetReportTechCon_t15_List {startDate},{endDate}").ToListAsync();
                await _context.DisposeAsync();

                DataTable dataTable = new DataTable();

                using (var reader = ObjectReader.Create(rep2, "RowNumber", "organisation_name", "dzhkh_date", "dzhkh_number", "appeal_desc_short", "conditional_address",
                    "fact_area", "d_heat", "d_vent", "d_gvs_a", "d_gvs_m", "d_heat_curtain", "d_cond", "d_other", "d_sum_gvs_m", "d_sum_gvs_a", "c_heat", "c_vent", "c_gvs_a",
                    "c_tech", "c_sum_gvs_a", "d_spec_heat_vent", "d_spec_gvs_a", "d_spec_all", "c_spec_heat_vent", "c_spec_gvs_a", "c_spec_all", "spec_dev_heat_vent", "spec_dev_gvs_a",
                    "spec_dev_sum", "q_reserve_heat", "bandwidth_reserve", "hp_name", "source_name", "connect_point", "tso_events", "project_num_ip", 
                    "realiz_period_ip", "accounted_events", "accordance_events", "reasons_mismath_events", "result_review", "changes_is_required", "unom_num"))
                {
                    dataTable.Load(reader);
                }
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("техприс_том_15");
                ws.Range("A1:A3").Merge();
                ws.Range("B1:B3").Merge();
                ws.Range("C1:D1").Merge();
                ws.Range("C2:C3").Merge();
                ws.Range("D2:D3").Merge();
                ws.Range("E1:E3").Merge();
                ws.Range("F1:F3").Merge();
                ws.Range("G1:G3").Merge();
                ws.Range("H1:P1").Merge();
                ws.Range("Q1:U1").Merge();
                ws.Range("H2:H3").Merge();
                ws.Range("I2:I3").Merge();
                ws.Range("J2:J3").Merge();
                ws.Range("K2:K3").Merge();
                ws.Range("L2:L3").Merge();
                ws.Range("M2:M3").Merge();
                ws.Range("N2:N3").Merge();
                ws.Range("O2:O3").Merge();
                ws.Range("P2:P3").Merge();
                ws.Range("Q2:Q3").Merge();
                ws.Range("R2:R3").Merge();
                ws.Range("S2:S3").Merge();
                ws.Range("T2:T3").Merge();
                ws.Range("U2:U3").Merge();
                ws.Range("V1:AD1").Merge();
                ws.Range("V2:X2").Merge();
                ws.Range("Y2:AA2").Merge();
                ws.Range("AB2:AD2").Merge();
                ws.Range("AE1:AF1").Merge();
                ws.Range("AE2:AE3").Merge();
                ws.Range("AF2:AF3").Merge();
                ws.Range("AG1:AI1").Merge();
                ws.Range("AG2:AG3").Merge();
                ws.Range("AH2:AH3").Merge();
                //ws.Range("AI2:AI3").Merge();
                ws.Range("AI2:AI3").Merge();
                ws.Range("AJ1:AO1").Merge();
                ws.Range("AJ2:AJ3").Merge();
                ws.Range("AK2:AL2").Merge();
                ws.Range("AM2:AM3").Merge();
                ws.Range("AN2:AN3").Merge();
                ws.Range("AO2:AO3").Merge();
                ws.Range("AP1:AQ1").Merge();
                ws.Range("AP2:AP3").Merge();
                ws.Range("AQ1:AQ3").Merge();
                ws.Range("AR1:AR3").Merge();


                ws.Cell("A1").Value = "№ п/п";
                ws.Cell("B1").Value = "Наименование организации";
                ws.Cell("C1").Value = "Реквизиты обращения, зарегистрированные в ДЖКХ г. Москвы";
                ws.Cell("C2").Value = "Дата";
                ws.Cell("D2").Value = "Номер";
                ws.Cell("E1").Value = "Предложение заявителя в обращении";
                ws.Cell("F1").Value = "Адрес объекта капитального строительства";
                ws.Cell("G1").Value = "Площадь объекта, тыс. м²";
                ws.Cell("H1").Value = "Заявленная тепловая нагрузка (договорная), Гкал/ч";
                ws.Cell("H2").Value = "Отопление";
                ws.Cell("I2").Value = "Вентиляция";
                ws.Cell("J2").Value = "ГВСср.ч.";
                ws.Cell("K2").Value = "ГВСмакс.ч.";
                ws.Cell("L2").Value = "Тепловые завесы";
                ws.Cell("M2").Value = "Кондициониро-вание";
                ws.Cell("N2").Value = "Прочее";
                ws.Cell("O2").Value = "Нагрузка суммарная (с ГВСмакс.ч.)";
                ws.Cell("P2").Value = "Нагрузка суммарная (с ГВСср.ч.)";
                ws.Cell("Q1").Value = "Расчетная тепловая нагрузка, Гкал/ч";
                ws.Cell("Q2").Value = "Отопление";
                ws.Cell("R2").Value = "Вентиляция";
                ws.Cell("S2").Value = "ГВСср.ч.";
                ws.Cell("T2").Value = "Технология";
                ws.Cell("U2").Value = "Нагрузка суммарная (с ГВСср.ч.)";
                ws.Cell("V1").Value = "Удельный расход тепловой энергии (заявленный), ккал/(ч·м2)";
                ws.Cell("V2").Value = "Заявленный";
                ws.Cell("Y2").Value = "Нормативный";
                ws.Cell("AB2").Value = "Отклонение,%";
                ws.Cell("V3").Value = "Отопление+вен-тиляция";
                ws.Cell("W3").Value = "ГВСср.ч.";
                ws.Cell("X3").Value = "Суммарный";
                ws.Cell("Y3").Value = "Отопление+вен-тиляция";
                ws.Cell("Z3").Value = "ГВСср.ч.";
                ws.Cell("AA3").Value = "Суммарный";
                ws.Cell("AB3").Value = "Отопление+вен-тиляция";
                ws.Cell("AC3").Value = "ГВСср.ч.";
                ws.Cell("AD3").Value = "Суммарный";
                ws.Cell("AE1").Value = "Анализ технической возможности технологического присоединения";
                ws.Cell("AE2").Value = "Резерв тепловой мощности источника, Гкал/ч";
                ws.Cell("AF2").Value = "Резерв пропускной способности в точке подключения, т/ч";
                ws.Cell("AG1").Value = "Связь объекта капитального строительства с электронной моделью схемы теплоснабжения";
                ws.Cell("AG2").Value = "Идентификатор теплопотребляющей установки";
                ws.Cell("AH2").Value = "Наименование источника";
                //ws.Cell("AI2").Value = "Идентификатор участков тепловой сети, реконструируемых/ строящихся для подключения объекта капитального сторительства";
                ws.Cell("AI2").Value = "Камера присоединения теплового ввода потребителя к сетям централизованного теплоснабжения";
                ws.Cell("AJ1").Value = "Анализ мероприятий по технологическому присоединению объекта капитального строительства, предложенных ТСО в обращении";
                ws.Cell("AJ2").Value = "Перечень мероприятий, предложенных ТСО";
                ws.Cell("AK2").Value = "Реквизиты";
                ws.Cell("AK3").Value = "Идентификатор мероприятия";
                ws.Cell("AL3").Value = "Срок реализации мероприятия";
                ws.Cell("AM2").Value = "Перечень мероприятий согласно актуализации схеме теплоснабжения";
                ws.Cell("AN2").Value = "Соответствие мероприятий (да/Нет)";
                ws.Cell("AO2").Value = "Причины несоответствия мерпориятий предложенных ТСО и учтенных в Схеме теплоснабжения";
                ws.Cell("AP1").Value = "Выводы и заключения по итогам рассмотрения обращения";
                ws.Cell("AP2").Value = "Результат рассмотрения обращения";
                ws.Cell("AQ1").Value = "Необходимость внесения изменений в актуализацию схемы теплоснабжения (Да/Нет)";
                ws.Cell("AR1").Value = "УНОМ";


                ws.Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Columns("2,4,5,7,8,9,10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //ws.FirstRow().Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Range("J2:K2").Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Column(1).Width = 10;
                ws.Columns("2,3,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,34,38,40,43").Width = 15;
                ws.Columns("4,6,33,35,36,37").Width = 20;
                ws.Columns("5,39,41").Width = 50;
                ws.Column(42).Width = 100;

                ws.Row(1).Height = 50;
                ws.Row(3).Height = 90;
                //ws.Rows(2, 3).Height = 90;
                ws.Style.Alignment.WrapText = true;
                ws.Cell(4, 1).InsertData(dataTable);

                var rngTable = ws.Range($"A1:AR{dataTable.Rows.Count + 3}");
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Font.FontName = "Times New Roman";

                foreach (var t in rngTable.Rows())
                {
                    foreach (var cell in t.Cells())
                    {
                        if (cell.Value.ToString() == " - ")
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var filename = "/ReportsDownload/техприс_том_15_userid_" + userId + ".xlsx";
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stream excelStream = System.IO.File.Create(webRootPath + filename);
                wb.SaveAs(excelStream);
                excelStream.Dispose();

                return new string[] { "true", host + filename };
            }
            catch (Exception ex)
            {
                return new string[] { "false", ex.Message };
            }
        }

        //Формирование отчёта "Сводный реестр" для Тома 15 (табл. 5.1) (по всем категориям)
        public async Task<string[]> ReportAll_t15_Export(DateTime? startDate, DateTime? endDate, string host)
        {
            try
            {
                List<ReportAll_t15_ViewModel> rep2 = await _context.ReportAll_t15_ViewModel.FromSqlInterpolated($"exec sp_GetReportAll_t15_List {startDate},{endDate}").ToListAsync();
                await _context.DisposeAsync();

                DataTable dataTable = new DataTable();

                using (var reader = ObjectReader.Create(rep2, "RowNumber", "organisation_name", "dzhkh_date", "dzhkh_number", "appeal_desc_short", "eto_num", "eto_name",
                    "hssn_num", "s_num", "source_name", "source_address", "q_inst_heat", "q_netto_heat", "conditional_address", "district_name", "building_purpose", "fact_area",
                    "UP_date", "UP_num", "fact_start", "d_heat", "d_vent", "d_gvs_a", "d_gvs_m", "d_heat_curtain", "d_cond", "d_other", "d_sum_gvs_m", "d_sum_gvs_a", "exists_aip",
                    "building_permits_date", "building_permits_num", "connect_point", "tso_events", "unom_event", "project_num_ip", "realiz_period_ip", "result_review", "changes_type", "unom_num"))
                {
                    dataTable.Load(reader);
                }
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Сводный_реестр_том_15");
                ws.Range("A1:A3").Merge();
                ws.Range("B1:B3").Merge();
                ws.Range("C1:D1").Merge();
                ws.Range("C2:C3").Merge();
                ws.Range("D2:D3").Merge();
                ws.Range("E1:E3").Merge();
                ws.Range("F1:F3").Merge();
                ws.Range("G1:G3").Merge();
                ws.Range("H1:H3").Merge();
                ws.Range("I1:M1").Merge();
                ws.Range("I2:I3").Merge();
                ws.Range("J2:J3").Merge();
                ws.Range("K2:K3").Merge();
                ws.Range("L2:L3").Merge();
                ws.Range("M2:M3").Merge();
                ws.Range("N1:AC1").Merge();
                ws.Range("N2:N3").Merge();
                ws.Range("O2:O3").Merge();
                ws.Range("P2:P3").Merge();
                ws.Range("Q2:Q3").Merge();
                ws.Range("R2:S2").Merge();
                ws.Range("T2:T3").Merge();
                ws.Range("U2:AC2").Merge();
                ws.Range("AD1:AD3").Merge();
                ws.Range("AE1:AF2").Merge();
                ws.Range("AG1:AG3").Merge();
                ws.Range("AH1:AH3").Merge();
                ws.Range("AI1:AI3").Merge();
                ws.Range("AJ1:AK2").Merge();
                ws.Range("AL1:AM2").Merge();
                ws.Range("AN1:AN3").Merge();

                ws.Cell("A1").Value = "№ п/п";
                ws.Cell("B1").Value = "Наименование организации";
                ws.Cell("C1").Value = "Реквизиты обращения, зарегистрированные в ДЖКХ г. Москвы";
                ws.Cell("C2").Value = "Дата";
                ws.Cell("D2").Value = "Номер";
                ws.Cell("E1").Value = "Предложение заявителя в обращении (кратко)";
                ws.Cell("F1").Value = "Код ЕТО";
                ws.Cell("G1").Value = "Наименование ЕТО";
                ws.Cell("H1").Value = "Номер СТ";
                ws.Cell("I1").Value = "Описание источника";
                ws.Cell("I2").Value = "Код источника";
                ws.Cell("J2").Value = "Наименование источника";
                ws.Cell("K2").Value = "Адрес источника";
                ws.Cell("L2").Value = "Установленная тепловая мощность, Гкал/ч";
                ws.Cell("M2").Value = "Тепловая мощность нетто, Гкал/ч";
                ws.Cell("N1").Value = "Информация об объекте капитального строительства";
                ws.Cell("N2").Value = "Адрес объекта";
                ws.Cell("O2").Value = "Административный округ";
                ws.Cell("P2").Value = "Назначение объекта";
                ws.Cell("Q2").Value = "Общая площадь (при наличии), м2";
                ws.Cell("R2").Value = "Реквизиты заявки на технологическое присоединение (реквизиты условий подключения)";
                ws.Cell("R3").Value = "Дата заявки";
                ws.Cell("S3").Value = "Номер заявки";
                ws.Cell("T2").Value = "Срок присоединения";
                ws.Cell("U2").Value = "Структура заявленной тепловой нагрузки (договорная), Гкал/ч";
                ws.Cell("U3").Value = "Отопление";
                ws.Cell("V3").Value = "Вентиляция";
                ws.Cell("W3").Value = "ГВСср.ч.";
                ws.Cell("X3").Value = "ГВСмакс.ч.";
                ws.Cell("Y3").Value = "Тепловые завесы";
                ws.Cell("Z3").Value = "Кондициониро-вание";
                ws.Cell("AA3").Value = "Прочее";
                ws.Cell("AB3").Value = "Нагрузка суммарная (с ГВСмакс.ч.)";
                ws.Cell("AC3").Value = "Нагрузка суммарная (с ГВСср.ч.)";
                ws.Cell("AD1").Value = "Сведения о наличии перспективного объекта в АИП (при наличии)";
                ws.Cell("AE1").Value = "Сведения о выданных разрешениях на строительство (при наличии)";
                ws.Cell("AE3").Value = "Дата";
                ws.Cell("AF3").Value = "Номер";
                ws.Cell("AG1").Value = "Точка присоединения к системе централизованного теплоснабжения";
                ws.Cell("AH1").Value = "Перечень мероприятий, предлагаемых ТСО в обращении";
                ws.Cell("AI1").Value = "УНОМ мероприятия в схеме теплоснабжения";
                ws.Cell("AJ1").Value = "Наличие мероприятия в утвержденной ИП ТС (при наличии)";
                ws.Cell("AJ3").Value = "Номер проекта";
                ws.Cell("AK3").Value = "Срок реализации мероприятия";
                ws.Cell("AL1").Value = "Выводы и заключения о внесении изменений в Актуализацию Схемы теплснабжения";
                ws.Cell("AL3").Value = "Результат рассмотрения";
                ws.Cell("AM3").Value = "Перечень изменений, необходимых для учета в последующей актуализации схемы теплоснабжения";
                ws.Cell("AN1").Value = "УНОМ";

                ws.Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Columns("2,4,5,7,8,9,10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //ws.FirstRow().Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Range("J2:K2").Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                
                ws.Column(1).Width = 10;
                ws.Columns("2,3,9,10,12,13,17,18,19,20,21,22,23,24,25,26,27,28,29,30,36,37").Width = 15;
                ws.Columns("4,7,11,14,15,16,33").Width = 20;
                ws.Columns("35").Width = 30;
                ws.Columns("5,34").Width = 50;
                ws.Columns("38,39").Width = 100;

                ws.Row(1).Height = 45;
                ws.Rows(2, 3).Height = 90;
                ws.Style.Alignment.WrapText = true;
                ws.Cell(4, 1).InsertData(dataTable);

                var rngTable = ws.Range($"A1:AN{dataTable.Rows.Count + 3}");
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Font.FontName = "Times New Roman";

                foreach (var t in rngTable.Rows())
                {
                    foreach (var cell in t.Cells())
                    {
                        if (cell.Value.ToString() == " - ")
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var filename = "/ReportsDownload/сводный_реестр_том_15_userid_" + userId + ".xlsx";
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stream excelStream = System.IO.File.Create(webRootPath + filename);
                wb.SaveAs(excelStream);
                excelStream.Dispose();

                return new string[] { "true", host + filename };
            }
            catch (Exception ex)
            {
                return new string[] { "false", ex.Message };
            }
        }

        //Формирование отчёта "Информационно-аналитическое сопровождение рассмотрения обращений о выводе из эксплуатации источников тепловой энергии,
        //оборудования источников тепловой энергии и тепловых сетей" для Тома 15 (табл. 1.2) (по категории "Вывод")
        public async Task<string[]> ReportDecommissioning_t15_Export(DateTime? startDate, DateTime? endDate, string host)
        {
            try
            {
                List<ReportDecommissioning_t15_ViewModel> rep2 = await _context.ReportDecommissioning_t15_ViewModel.FromSqlInterpolated($"exec sp_GetReportDecommissioning_t15_List {startDate},{endDate}").ToListAsync();
                await _context.DisposeAsync();

                DataTable dataTable = new DataTable();

                using (var reader = ObjectReader.Create(rep2, "RowNumber", "organisation_name", "dzhkh_date", "dzhkh_number", "appeal_desc_short", "source_name", "source_address",
                    "eto_num", "eto_name", "hssn_num", "q_inst_heat", "q_netto_heat", "q_reserve_heat", "decom_equip_list", "decom_date", "decom_q_inst_heat_equip",
                    "after_decom_q_inst_heat_equip", "after_decom_q_netto_heat", "after_decom_q_reserve_heat", "ch_scheme_is_no_need", "decom_need_pause", "source_finance_events",
                    "consumers_heat_pause", "c_heat", "c_vent", "c_gvs_a", "c_tech", "c_sum_gvs_a", "approved_list_events", "alter_var_events", "start_realiz_event", "result_review", "unom_num"))
                {
                    dataTable.Load(reader);
                }
                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Вывод_том_15");
                ws.Range("A1:A2").Merge();
                ws.Range("B1:B2").Merge();
                ws.Range("C1:D1").Merge();
                ws.Range("E1:E2").Merge();
                ws.Range("F1:F2").Merge();
                ws.Range("G1:G2").Merge();
                ws.Range("H1:H2").Merge();
                ws.Range("I1:I2").Merge();
                ws.Range("J1:J2").Merge();
                ws.Range("K1:M1").Merge();
                ws.Range("N1:P1").Merge();
                ws.Range("Q1:S1").Merge();
                ws.Range("T1:T2").Merge();
                ws.Range("U1:U2").Merge();
                ws.Range("V1:V2").Merge();
                ws.Range("W1:W2").Merge();
                ws.Range("X1:AB1").Merge();
                ws.Range("AC1:AC2").Merge();
                ws.Range("AD1:AE1").Merge();
                ws.Range("AF1:AF2").Merge();
                ws.Range("AG1:AG2").Merge();

                ws.Cell("A1").Value = "№ п/п";
                ws.Cell("B1").Value = "Наименование организации";
                ws.Cell("C1").Value = "Реквизиты обращения, зарегистрированные в ДЖКХ г. Москвы";
                ws.Cell("C2").Value = "Дата";
                ws.Cell("D2").Value = "Номер";
                ws.Cell("E1").Value = "Предложение заявителя в обращении";
                ws.Cell("F1").Value = "Наименование источника";
                ws.Cell("G1").Value = "Адрес источника";
                ws.Cell("H1").Value = "Номер зоны деятельности ЕТО";
                ws.Cell("I1").Value = "Наименование ЕТО";
                ws.Cell("J1").Value = "Код системы теплоснабжения";
                ws.Cell("K1").Value = "До вывода из эксплуатации";
                ws.Cell("K2").Value = "Установленная тепловая мощность источника (всего), Гкал/ч";
                ws.Cell("L2").Value = "Тепловая мощность источника нетто (всего), Гкал/ч";
                ws.Cell("M2").Value = "Резерв/дефицит тепловой мощности, Гкал/ч";
                ws.Cell("N1").Value = "Вывод из эксплуатации";
                ws.Cell("N2").Value = "Перечень выводимого оборудования";
                ws.Cell("O2").Value = "Срок вывода из эксплуатации";
                ws.Cell("P2").Value = "Установленная тепловая мощность выводимого источника/оборудования источника, Гкал/ч";
                ws.Cell("Q1").Value = "После вывода из эксплуатации";
                ws.Cell("Q2").Value = "Установленная тепловая мощность источника (всего), Гкал/ч";
                ws.Cell("R2").Value = "Тепловая мощность источника нетто (всего), Гкал/ч";
                ws.Cell("S2").Value = "Резерв/дефицит тепловой мощности, Гкал/ч";
                ws.Cell("T1").Value = "Возможность теплоснабжения потребителей без изменения схемы присоединения (да/нет)";
                ws.Cell("U1").Value = "Необходимость приостановления вывода из эксплуатации с целью обеспечения потребилетей тепловой энергией";
                ws.Cell("V1").Value = "Источники финансирования компенсационных мероприятий";
                ws.Cell("W1").Value = "Перечень потребителей, теплоснабжение коротых будет приостановлено при выводе источника/оборудования";
                ws.Cell("X1").Value = "Структура тепловой нагрузки потребителей, теплоснабжение которых может быть приостановлено, Гкал/ч";
                ws.Cell("X2").Value = "Отопление";
                ws.Cell("Y2").Value = "Вентиляция";
                ws.Cell("Z2").Value = "ГВСср.ч.";
                ws.Cell("AA2").Value = "Технология";
                ws.Cell("AB2").Value = "Нагрузка суммарная (с ГВСср.ч.)";
                ws.Cell("AC1").Value = "Перечень мероприятий согласно утвержденной схеме теплоснабжения";
                ws.Cell("AD1").Value = "Формирование альтернативного варианта организации теплоснабжения";
                ws.Cell("AD2").Value = "Перечень мероприятий";
                ws.Cell("AE2").Value = "Срок реализации";
                ws.Cell("AF1").Value = "Результат рассмотрения";
                ws.Cell("AG1").Value = "УНОМ";


                ws.Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Columns("2,4,5,7,8,9,10").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //ws.FirstRow().Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                //ws.Range("J2:K2").Style
                //    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                //    .Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                ws.Column(1).Width = 10;
                ws.Columns("2,3,6,8,9,10,11,12,13,15,16,17,18,19,20,24,25,26,27,28,31").Width = 15;
                ws.Columns("4,7,22").Width = 20;
                ws.Columns("21,23").Width = 30;
                ws.Columns("5,14,29,30").Width = 50;
                ws.Columns("32").Width = 100;

                ws.Row(1).Height = 45;
                ws.Row(2).Height = 60;
                ws.Style.Alignment.WrapText = true;
                ws.Cell(3, 1).InsertData(dataTable);

                var rngTable = ws.Range($"A1:AG{dataTable.Rows.Count + 2}");
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Font.FontName = "Times New Roman";

                foreach (var t in rngTable.Rows())
                {
                    foreach (var cell in t.Cells())
                    {
                        if (cell.Value.ToString() == " - ")
                        {
                            cell.Style.Fill.BackgroundColor = XLColor.Red;
                        }
                    }
                }

                var filename = "/ReportsDownload/Вывод_том_15_userid_" + userId + ".xlsx";
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stream excelStream = System.IO.File.Create(webRootPath + filename);
                wb.SaveAs(excelStream);
                excelStream.Dispose();

                return new string[] { "true", host + filename };
            }
            catch (Exception ex)
            {
                return new string[] { "false", ex.Message };
            }
        }

        //public async Task<ActionResult> ReportExport_XLSIO(DateTime? startDate, DateTime? endDate)
        //{
        //    string host = _httpContextAccessor.HttpContext.Request.Host.Value;

        //    if (userId > 0 || host.Contains("localhost"))
        //    {
        //        if (startDate == null || endDate == null)
        //        {
        //            startDate = DateTime.Now;
        //            endDate = DateTime.Now;
        //        }

        //        endDate = endDate.Value.AddDays(1);

        //        List<ReportOtherViewModel> rep1 = await _context.ReportOtherViewModel.FromSqlInterpolated($"exec sp_GetReportOtherList {startDate},{endDate}").ToListAsync();
        //        await _context.DisposeAsync();

        //        try
        //        {
        //            using (ExcelEngine excelEngine = new ExcelEngine())
        //            {
        //                IApplication application = excelEngine.Excel;
        //                application.DefaultVersion = ExcelVersion.Excel2016;

        //                //Create a new workbook
        //                IWorkbook workbook = application.Workbooks.Create(1);
        //                IWorksheet sheet = workbook.Worksheets[0];

        //                DataTable dataTable = new DataTable();

        //                using (var reader = ObjectReader.Create(rep1, "RowNumber", "organisation_name", "dzhkh_date", "dzhkh_number", "appeal_desc_short", "district_name", "source_name", "justification", "res_calc", "result_review", "changes_is_required"))
        //                {
        //                    dataTable.Load(reader);
        //                }


        //                //Import data from the data table with column header, at first row and first column, 
        //                //and by its column type.
        //                sheet.ImportDataTable(dataTable, false, 3, 1, false);
        //                //IAutoFilter filter = sheet.AutoFilters[1];
        //                //Creating Excel table or list object and apply style to the table
        //                IListObject table = sheet.ListObjects.Create("Employee_PersonalDetails", sheet.UsedRange);
        //                table.Worksheet.Range["J1:K1"].Merge();
        //                table.Worksheet.Range["A1:A2"].Merge();
        //                table.Worksheet.Range["B1:B2"].Merge();
        //                table.Worksheet.Range["C1:C2"].Merge();
        //                table.Worksheet.Range["D1:D2"].Merge();
        //                table.Worksheet.Range["E1:E2"].Merge();
        //                table.Worksheet.Range["F1:F2"].Merge();
        //                table.Worksheet.Range["G1:G2"].Merge();
        //                table.Worksheet.Range["H1:H2"].Merge();
        //                table.Worksheet.Range["I1:I2"].Merge();

        //                table.Worksheet.Range["J1:K1"].Text = "Вывод и заключения по результатам рассмотрения обращения";
        //                table.Worksheet.Range["A1:A2"].Text = "№ п/п";
        //                table.Worksheet.Range["B1:B2"].Text = "Наименование организации";
        //                table.Worksheet.Range["C1:C2"].Text = "Дата обращения, зарегистрированная в ДЖКХ г. Москвы";
        //                table.Worksheet.Range["D1:D2"].Text = "№ обращения, зарегистрированный в ДЖКХ г. Москвы";
        //                table.Worksheet.Range["E1:E2"].Text = "Предложение заявителя в обращении";
        //                table.Worksheet.Range["F1:F2"].Text = "Административный округ, в который попадает";
        //                table.Worksheet.Range["G1:G2"].Text = "Зона действия источника, в которую попадает объект обращения";
        //                table.Worksheet.Range["H1:H2"].Text = "Технико-экономическое обоснование (при необходимости)";
        //                table.Worksheet.Range["I1:I2"].Text = "Результат гидравлического расчета (при необходимости)";
        //                table.Worksheet.Range["J2"].Text = "Результат рассмотрения обращения";
        //                table.Worksheet.Range["K2"].Text = "Необходимость внесения изменений в актуализацию схемы теплоснабжения";
        //                table.Worksheet.Range[$"A1:K{rep1.Count + 3}"].WrapText = true;

        //                table.Worksheet.Range["A1"].ColumnWidth = 5;
        //                table.Worksheet.Range["B1:C1"].ColumnWidth = 15;
        //                table.Worksheet.Range["D1"].ColumnWidth = 20;
        //                table.Worksheet.Range["E1"].ColumnWidth = 50;
        //                table.Worksheet.Range["F1:I1"].ColumnWidth = 18;
        //                table.Worksheet.Range["J1"].ColumnWidth = 100;
        //                table.Worksheet.Range["K2"].ColumnWidth = 20;

        //                table.Worksheet.Range[$"A2:K{rep1.Count + 3}"].RowHeight = 100;

        //                table.Worksheet.Range["A1:K1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
        //                table.Worksheet.Range["A1:K1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
        //                table.Worksheet.Range["J2:K2"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
        //                table.Worksheet.Range["J2:K2"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

        //                table.Worksheet.Range[$"A3:K{rep1.Count + 3}"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
        //                table.Worksheet.Range[$"A3:K{rep1.Count + 3}"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

        //                table.ShowAutoFilter = false;

        //                //table.BuiltInTableStyle = TableBuiltInStyles.TableStyleMedium14;

        //                //Autofit the columns
        //                //sheet.UsedRange.AutofitColumns();

        //                //Save the file in the given path
        //                //Stream excelStream = System.IO.File.Create(Path.GetFullPath(@"Output.xlsx"));

        //                var filename = userId;

        //                // application's base path
        //                //string contentRootPath = _hostingEnvironment.ContentRootPath;
        //                // application's publishing path
        //                string webRootPath = _hostingEnvironment.WebRootPath;
        //                Stream excelStream = System.IO.File.Create(webRootPath + "/Reports/прочие_" + filename + ".xlsx");
        //                //Stream excelStream = System.IO.File.Create(Path.GetFullPath(HttpServerUtility.MapPath("~/Reports/прочие_" + filename + ".xlsx")));
        //                //string path = Microsoft.AspNetCore.Http.HttpContext.MapPath("~/GoldenChocos/" + user.IdInt);
        //                //Stream excelStream = System.IO.File.Create(System.Web.HttpContext.Current.Server.MapPath("~/Reports/прочие_" + filename + ".xlsx"));

        //                //workbook.Worksheets[0].Remove();
        //                workbook.SaveAs(excelStream);
        //                excelStream.Dispose();

        //                /*string download = Environment.GetEnvironmentVariable("USERPROFILE") + @"\" + "Downloads";
        //                var myPath = Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty).ToString();

        //                var log = new Logs()
        //                {
        //                    Exception = "download: " + download + "; myPath: " + myPath,
        //                    UserId = adm.IdInt,
        //                    CreateDate = DateTime.Now
        //                };
        //                context.Logs.Add(log);
        //                context.SaveChanges();

        //                using (var client = new WebClient())
        //                {
        //                    client.DownloadFile("https://alpengold.me/FilesExport/участники_" + filename + ".xlsx", myPath + "\\участники_" + filename + ".xlsx");
        //                }*/

        //                return Json(new { success = true, filename = "http://"+ host + "/Reports/прочие_" + filename + ".xlsx" });
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //var log = new Logs()
        //            //{
        //            //    Exception = ex.ToString(),
        //            //    UserId = adm.IdInt,
        //            //    CreateDate = DateTime.Now
        //            //};
        //            //context.Logs.Add(log);
        //            //context.SaveChanges();

        //            return Json(new { success = false, error = "Произошла ошибка при загрузки файла" });
        //        }
        //    }

        //    return RedirectToAction("Login", "Admin");
        //}
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}