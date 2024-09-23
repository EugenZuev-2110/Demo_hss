using DataBase.Models.Sources;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Controllers;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.Sources.Models;
using WebProject.Controllers;
using WebProject.Data;
using WebProject.Filters;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.Sources.Controllers
{
	[TypeFilter(typeof(ControllerActionFilter))]
	[Area("Sources")]
	public class SourcesController : ModuleBaseController
	{
		public SourcesController(ILogger<TariffZoneController> logger, HssDbContext context, ApplicationDbContext context2, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, HSSController m_c)
			: base(logger, context, context2, httpContextAccessor, hostingEnvironment, m_c) { }

		public async Task<IActionResult> SourcesList()
		{
			int data_status = _m_c.GetCurrentDS();
			ViewBag.Tso = await _context.fnt_GetTSOName(data_status).ToListAsync();
			ViewBag.SourcesType = await _context.fnt_GetSourcesTypeList().ToListAsync();
			return View();
		}

		#region ViewComponent
		public ActionResult OnGetSourcesViewComponent(int data_status, int perspective_year, int tz, int status, int org, int type)
		{
			return ViewComponent("SourceList_Partial", new { userId, data_status, perspective_year, tz, status, org, type });
		}

        //Документация и фотоматериалы по Источнику
        public ActionResult OnGetSource_DocsFootage_ViewComponent(int data_status, int source_id)
        {
            return ViewComponent("Source_DocsFootage_Partial", new { data_status, source_id });
        }
        #endregion

        #region OpenPopups
        public async Task<IActionResult> OpenSource(int id, int data_status, string action_for = "")
		{
			ViewBag.Source = await _context.fnt_GetSourcesUnomList(data_status).ToListAsync();
			SourcesOneDataViewModel sourcesOneData = new() { data_status = data_status, source_id = id };
			return PartialView("OpenSource", sourcesOneData);
		}
		#endregion

		#region Save_Data

		#region Save_Year_Implement_Scheme_Param
		/// <summary>
		/// Сохранение параметров по годам реализации схемы
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<IActionResult> Source_YearImplementSchemeParam_Save(IFormCollection model)
		{
			try {
				var is_new_per = await Source_Perspective_Save(model);


				return Json(new { success = true , is_new_per = true, is_per_save = true});
			}
			catch (Exception ex) {
				return Json(new { success = false });
			}
		}

		/// <summary>
		/// Сохранение в таблицу Source_Perspective
		/// </summary>
		/// <param name="data_status"></param>
		/// <param name="source_id"></param>
		/// <returns></returns>
		private async Task<bool> Source_Perspective_Save(IFormCollection model)
		{
			bool is_new_per = false;
			for (int i = 0; i < model["perspective_year"].Count; i++)
			{
				
					var _source_p_gen_info_upd = await _context.S_Perspectives.Where(x => x.source_id == Convert.ToInt32(model["source_id"][0]) && x.data_status == Convert.ToInt32(model["data_status"][0]) && x.perspective_year == Convert.ToInt32(model["perspective_year"][i])).FirstOrDefaultAsync();
					if (_source_p_gen_info_upd != null)
					{
						Source_Perspective_Update(_source_p_gen_info_upd, model, i);
					}
					else
					{
					 	is_new_per = await Source_Perspective_Create(model, i, is_new_per);
					}
					await _context.SaveChangesAsync();
            }
            return is_new_per;
        }
		/// <summary>
		/// Обновление записи в таблице Source_Perspective
		/// </summary>
		/// <param name="_source_per_upd"></param>
		/// <param name="model"></param>
		/// <param name="i"></param>
		private void Source_Perspective_Update(S_Perspective _source_per_upd, IFormCollection model, int i, short t1t2 = 0, short t3t4 = 0, short t5t6 = 0, short HeatOutScheme = 0)
		{
			try
			{
				if (model.ContainsKey("source_status_select"))
				{
					_source_per_upd.hssn_id = (i >= model["hss_select"].Count() || model["hss_select"][i] == "") ? null : Convert.ToInt32(model["hss_select"][i]);
					_source_per_upd.tso_id = (i >= model["tso_select"].Count() || model["tso_select"][i] == "") ? null : Convert.ToInt32(model["tso_select"][i]);
					_source_per_upd.source_status_id = model["source_status_select"][i] == "" ? null : Convert.ToInt16(model["source_status_select"][i]);
				}
				if (model.ContainsKey("heat_out_scheme_select"))
				{
					_source_per_upd.hn_heat_out_scheme_type_id = (HeatOutScheme == 0) ? null : HeatOutScheme;
					_source_per_upd.temp_plan_id = (t1t2 == 0) ? null : t1t2;
					_source_per_upd.temp_plan_heat_id = (t3t4 == 0) ? null : t3t4;
					_source_per_upd.temp_plan_gvs_id = (t5t6 == 0) ? null : t5t6;
				}
				_source_per_upd.edit_date = DateTime.Now;
				_source_per_upd.user_id = userId;
			}
			catch(Exception ex)
			{ }
		}
		/// <summary>
		/// Создание записи в таблице Source_Perspective
		/// </summary>
		/// <param name="model"></param>
		/// <param name="i"></param>
		/// <returns></returns>
		private async Task<bool> Source_Perspective_Create(IFormCollection model, int i, bool is_new_per)
		{
			S_Perspective _source_per_new = new();
			_source_per_new.data_status = Convert.ToInt32(model["data_status"][0]);
			_source_per_new.perspective_year = Convert.ToInt32(model["perspective_year"][i]);
			_source_per_new.source_id = Convert.ToInt32(model["source_id"][0]);
			_source_per_new.hssn_id = model["hss_select"][i] == "" && i < model["hss_select"].Count() ? null : Convert.ToInt32(model["hss_select"][i]);
			_source_per_new.tso_id = model["tso_select"][i] == "" ? null : Convert.ToInt32(model["tso_select"][i]);
			_source_per_new.source_status_id = model["source_status_select"][i] == "" ? null : Convert.ToInt16(model["source_status_select"][i]);
			_source_per_new.create_date = DateTime.Now;
			_source_per_new.user_id = userId;

			await _context.AddAsync(_source_per_new);
			return is_new_per;
		}
        #endregion

        #region Save_SourceHeatPowerOutput
        public async Task<IActionResult> Source_HeatPowerOutput_Save(IFormCollection model)
		{
			var TempGraphT1T2 = model["TempGraphT1T2"][0].Split(new char[] {','});
			var TempGraphT3T4 = model["TempGraphT3T4"][0].Split(new char[] {','});
			var TempGraphT5T6 = model["TempGraphT5T6"][0].Split(new char[] {','});
			var HeatOutScheme = model["HeatOutScheme"][0].Split(new char[] {','});
            var _source_p_gen_info_upd = await _context.S_Histories.Where(x => x.source_id == Convert.ToInt32(model["source_id"][0]) && x.data_status == Convert.ToInt32(model["data_status"][0])).FirstOrDefaultAsync();
			Source_History_Update(_source_p_gen_info_upd, model);
			for (int i = 0; i < model["perspective_year"].Count; i++)
			{
				var _source_per_upd = await _context.S_Perspectives.Where(x => x.source_id == Convert.ToInt32(model["source_id"][0]) && x.data_status == Convert.ToInt32(model["data_status"][0]) && x.perspective_year == Convert.ToInt32(model["perspective_year"][i])).FirstOrDefaultAsync();
				if (_source_p_gen_info_upd != null)
				{
					Source_Perspective_Update(_source_per_upd, model, i, Convert.ToInt16(TempGraphT1T2[i] == "" ? null : TempGraphT1T2[i] ), Convert.ToInt16(TempGraphT3T4[i] == "" ? null : TempGraphT3T4[i]), Convert.ToInt16(TempGraphT5T6[i] == "" ? null : TempGraphT5T6[i]), Convert.ToInt16(HeatOutScheme[i] == "null" ? null : HeatOutScheme[i]));
				}
			}
                await _context.SaveChangesAsync();
			JsonResult result = new JsonResult(new { success = true });
            return Json(new { success = true });
        }

		private void Source_History_Update(S_History _source_h_gen_info_upd, IFormCollection model)
		{
				_source_h_gen_info_upd.temp_supply_fact_T1 = (model.ContainsKey("temp_supply_fact_T1") && model["temp_supply_fact_T1"][0] == "") ? null : model.ContainsKey("temp_supply_fact_T1") ? Convert.ToDouble(model["temp_supply_fact_T1"][0]) : null;
				_source_h_gen_info_upd.temp_reverse_fact_T2 = (model.ContainsKey("temp_reverse_fact_T2") && model["temp_reverse_fact_T2"][0] == "") ? null : model.ContainsKey("temp_reverse_fact_T2") ? Convert.ToDouble(model["temp_reverse_fact_T2"][0]) : null;
				_source_h_gen_info_upd.temp_supply_fact_T3 = (model.ContainsKey("temp_supply_fact_T3") &&  model["temp_supply_fact_T3"][0] == "" ) ? null : model.ContainsKey("temp_supply_fact_T3") ? Convert.ToDouble(model["temp_supply_fact_T3"][0]) : null;
				_source_h_gen_info_upd.temp_reverse_fact_T4 = (model.ContainsKey("temp_reverse_fact_T4") && model["temp_reverse_fact_T4"][0] == "") ? null : model.ContainsKey("temp_reverse_fact_T4") ? Convert.ToDouble(model["temp_reverse_fact_T4"][0]) : null;
				_source_h_gen_info_upd.temp_supply_fact_T5 = (model.ContainsKey("temp_supply_fact_T5") && model["temp_supply_fact_T5"][0] == "") ? null : model.ContainsKey("temp_supply_fact_T5") ? Convert.ToDouble(model["temp_supply_fact_T5"][0]) : null;
				_source_h_gen_info_upd.temp_reverse_fact_T6 = (model.ContainsKey("temp_reverse_fact_T6") && model["temp_reverse_fact_T6"][0] == "") ? null : model.ContainsKey("temp_reverse_fact_T6") ? Convert.ToDouble(model["temp_reverse_fact_T6"][0]) : null;
				_source_h_gen_info_upd.temp_steam_consumers = (model.ContainsKey("temp_steam_consumers") && model["temp_steam_consumers"][0] == "") ? null : model.ContainsKey("temp_steam_consumers") ? Convert.ToDouble(model["temp_steam_consumers"][0]) : null;
				_source_h_gen_info_upd.pressure_steam_consumers = (model.ContainsKey("temp_supply_fact_T1") && model["pressure_steam_consumers"][0] == "") ? null : model.ContainsKey("temp_supply_fact_T1") ?  Convert.ToDouble(model["pressure_steam_consumers"][0]) : null;
				_source_h_gen_info_upd.avail_condens_pipe = (model.ContainsKey("avail_condens_pipe") && model["avail_condens_pipe"][0] == "") ? null : model.ContainsKey("avail_condens_pipe") ? Convert.ToBoolean(model["avail_condens_pipe"][0]) : null;
				_source_h_gen_info_upd.edit_date = DateTime.Now;
				_source_h_gen_info_upd.user_id = userId;
			
        }

        private void Source_Perspective_Updat(S_Perspective _source_per_upd, IFormCollection model, int i)
		{
            _source_per_upd.hssn_id = (i >= model["hss_select"].Count() || model["hss_select"][i] == "") ? null : Convert.ToInt32(model["hss_select"][i]);
            _source_per_upd.tso_id = (i >= model["tso_select"].Count() || model["tso_select"][i] == "") ? null : Convert.ToInt32(model["tso_select"][i]);
            _source_per_upd.source_status_id = model["source_status_select"][i] == "" ? null : Convert.ToInt16(model["source_status_select"][i]);
            _source_per_upd.edit_date = DateTime.Now;
            _source_per_upd.user_id = userId;
        }
        #endregion

        #endregion


        #region SAVE_DATA DOCS FOOTAGE

        // Сохранение документации и фотоматериалов по источнику
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(ControllerActionFilterCheckDS))]
        public async Task<IActionResult> Source_DocsFootage_Save(Source_DocsFootageViewModel model)
        {
            try
            {
                await Source_DocsFootage_CreateData(model);

                if (model.is_deleted != null && (bool)model.is_deleted)
                    return Json(new { success = true, is_deleted = true });

                return Json(new { success = true, is_deleted = false });
            }
            catch (Exception ex)
            {
                _m_c.ExLog_Save("Source_DocsFootage_Save", $"data_status={model.data_status},source_id={model.source_id},doc_num={model.doc_num}", ex.Message, userId);
                return Json(new { success = false });
            }
        }

        private async Task Source_DocsFootage_CreateData(Source_DocsFootageViewModel model)
        {
            bool is_new = false;
            bool is_deleting_file = false;
            var item = await _context.S_DocsPhotos_History.FirstOrDefaultAsync
                (n => n.data_status == model.data_status && n.source_id == model.source_id && n.doc_num == model.doc_num);

            if (item == null)
            {
                if (model.doc_num != 0)
                {
                    item = await _context.S_DocsPhotos_History.FirstOrDefaultAsync
                        (n => n.source_id == model.source_id && n.doc_num == model.doc_num);
                }
                else
                {
                    var value = await _context.S_DocsPhotos_History.Where
                        (n => n.source_id == model.source_id).MaxAsync(n => (int?)n.doc_num) ?? 0;

                    item = new S_DocsPhotos_History { doc_num = ++value };
                }
                is_new = true;
            }
            else
            {
                is_deleting_file = true;
                item.doc_num = model.doc_num;
            }

            item.doc_type_id = model.doc_type_id;
            item.doc_description = model.doc_description;
            item.source_id = model.source_id;
            item.data_status = model.data_status;
            item.user_id = userId;
            item.is_deleted = model.is_deleted ?? false;

            if ((bool)!item.is_deleted)
                Source_DocsFootage_SaveFile(item, is_deleting_file);

            await Source_DocsFootage_SaveDb(item, is_new);
        }

        private async Task Source_DocsFootage_SaveDb(S_DocsPhotos_History item, bool is_new)
        {
            if (is_new)
            {
                item.create_date = DateTime.Now;
                await _context.S_DocsPhotos_History.AddAsync(item);
            }
            else
            {
                item.edit_date = DateTime.Now;
                _context.S_DocsPhotos_History.Update(item);
            }
            await _context.SaveChangesAsync();
        }

        private void Source_DocsFootage_SaveFile(S_DocsPhotos_History item, bool isDelFile)
        {
            var file = HttpContext.Request.Form.Files.First();
            string webRootPath = _hostingEnvironment.WebRootPath;
            string upload = webRootPath + "\\Docs\\SourceDocs\\";

            if (file == null)
                return;

            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            string url = upload + fileName + extension;
            var oldUrl = Path.Combine(upload, item.doc_url ?? "");

            if (isDelFile && System.IO.File.Exists(oldUrl))
                System.IO.File.Move(oldUrl, oldUrl + "_deleted");

            string date = DateTime.Now.ToString("yyyy/MM/dd/hh/mm/ss");
            item.doc_name = fileName + extension;
            fileName = fileName + "_" + item.data_status + "_" + item.source_id + "_" + item.doc_num + "_" + date + extension;

            using (var fs = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                file.CopyTo(fs);

            item.doc_url = fileName;
        }

        #endregion
    }
}
