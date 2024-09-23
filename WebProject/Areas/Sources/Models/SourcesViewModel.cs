using DataBase.Models;
using DataBase.Models.Sources;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.Fonts;
using System.ComponentModel.DataAnnotations.Schema;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Data;

namespace WebProject.Areas.Sources.Models
{
    [Keyless]
    public class SourcesViewModel
    {
        public int source_id { get; set; }
        public int source_unom { get; set; }
        public string? source_name { get; set; }
        public string? address { get; set;}
        public string? short_name { get; set; }
		public string? district_name { get; set; }
        public string? tz_num { get; set; }
        public string? source_type_name { get; set; }
        public string? source_status_name { get; set; }
    }

	[Keyless]
	public class SourcesOneDataViewModel
	{
		public int source_id { get; set; }
		public int data_status { get; set; }
		//public int perspective_year { get; set; }
	}

	[Keyless]
	public class SourceGeneralInfoViewModel : IActionController
	{
		public int source_id { get; set; }
		public int source_unom { get; set; }
		public int data_status { get; set; }
		public string? source_name { get; set; }
		public string? kadnum_zu { get; set; }
		public string? kadnum_oks { get; set; }
		public int? fias_build { get; set; }
		public int? fias_zu { get; set; }
		public string? address { get; set; }
		public int? district_id { get; set; }
		public string? tz_num { get; set; } 
		public int? tz_id { get; set; }
		public int? org_owner_id { get; set; }
		public short? source_type_id { get; set; }
		public string? source_type_name { get; set; }
		public int? start_year_expl { get; set; }
		public double? phys_wear { get; set; }
		public double? amortis_wear { get; set;}
		public bool? obj_vacant_prop { get; set; }
		public bool? obj_old_prop { get; set; }

        public async Task<IActionResult> SaveAsync(HssDbContext _context, int userId, IFormCollection keyValues = null)
        {
            try
            {
				int sources_id; bool is_new = false; int? unom = 1; string? source_type_name;
				var _source_gen_info_upd = await _context.Sources.Where(x => x.source_id == source_id).FirstOrDefaultAsync();
				var _source_h_gen_info_upd = await _context.S_Histories.Where(x => x.source_id == source_id && x.data_status == data_status).FirstOrDefaultAsync();
				var last_unom = await _context.Sources.OrderByDescending(x => x.source_unom).Select(x => x.source_unom).FirstOrDefaultAsync();
				(sources_id, last_unom, is_new) = await SetModel(_source_gen_info_upd, last_unom, is_new, userId, _context);
				await SetModel(_source_h_gen_info_upd, sources_id, userId, _context);
				source_type_name = await _context.fnt_GetSourcesTypeList().Where(x => x.value_id == source_type_id).Select(x => x.value_name).FirstOrDefaultAsync();
				if(source_unom != 0)
				{
					unom = source_unom;
				}
				else
				{
                    unom = last_unom;
                }
				string unom_source = unom + " (" + source_name + " " + address + " " + source_type_name + ")";
				return new JsonResult(new { success = true, sources_id, is_new, unom_source });
			}
            catch (Exception ex) 
			{
				return new JsonResult(new { success = false });
			}
        }

		private async Task<(int, int?, bool)> SetModel(WebProject.Areas.HeatPointsAndConsumers.Models.Sources model, int? last_unom, bool is_new, int userId, HssDbContext context)
		{
			if (model == null)
			{
				if (last_unom != null)
				{
					last_unom++;
				}
				model = new WebProject.Areas.HeatPointsAndConsumers.Models.Sources { create_date = DateTime.Now, source_unom = last_unom };
				SetExistModel(model, userId, true);
				await context.AddAsync(model);
				is_new = true;

            }
			else
			{
				SetExistModel(model, userId);
				context.Update(model);
				is_new = false;
			}
			await context.SaveChangesAsync();
			return (model.source_id, last_unom, is_new);
		}

		private async Task SetModel(S_History model, int source_id, int userId, HssDbContext context)
		{
			if (model == null)
			{
				model = new S_History {data_status = data_status, source_id = source_id, create_date = DateTime.Now };
				SetExistModel(model, userId, true);
				await context.AddAsync(model);
			}
			else
			{
				SetExistModel(model,  userId);
				context.Update(model);
			}
			await context.SaveChangesAsync();
		}


		private void SetExistModel(WebProject.Areas.HeatPointsAndConsumers.Models.Sources sources, int userId, bool isNewData = false)
		{

			
			sources.address = address;
			sources.district_id = district_id;
			sources.fias_build = fias_build;
			sources.fias_zu = fias_zu;
			sources.kadnum_oks = kadnum_oks;
			sources.kadnum_zu = kadnum_zu;
			if (!isNewData)
				sources.edit_date = DateTime.Now;
			sources.user_id = userId;
		}

		private void SetExistModel(S_History source_h, int userId, bool isNewData = false)
		{

			
			source_h.data_status = data_status;
			source_h.tz_id = tz_id;
			source_h.is_deleted = false;
			source_h.source_type_id = source_type_id;
			source_h.source_name = source_name;
			source_h.org_owner_id = org_owner_id;
			source_h.obj_vacant_prop = obj_vacant_prop;
			source_h.obj_old_prop = obj_old_prop;
			source_h.phys_wear = phys_wear;
			source_h.amortis_wear = amortis_wear;
			source_h.start_year_expl = start_year_expl;
			source_h.create_date = DateTime.Now;
			source_h.user_id = userId;
			if (!isNewData)
				source_h.edit_date = DateTime.Now;
			
			source_h.user_id = userId;
		}
	}

	[Keyless]
	public class SourcePerspectiveViewModel
	{
		public int source_id { get; set; }
		public int? data_status { get; set; }
		public string? perspective_year_dt { get; set;}
		public int perspective_year { get; set; }
		public short? source_status_id { get; set; }
		public int? hssn_id { get; set; }
		public int? tso_id { get; set; }
		public decimal? calc_hl_hw_sum { get; set; }
		public decimal? calc_hl_steam_sum { get; set; }
	}

	[Keyless]
	public class SourceNumSignOtherDBViewModel : IActionController
	{
		public int data_status { get; set; }
		public int source_id { get; set; }
		public string? muid { get; set; }
		public string? lotus_id { get; set; }
		public string? kasu_composite_id { get; set; }
		public string? kasu_id { get; set; }
		public string? kasu_map_id { get; set; }

		public async Task<IActionResult> SaveAsync(HssDbContext _context, int userId, IFormCollection keyValues = null)
		{
			try
			{
				var _source_h_num_sign_db = _context.S_Histories.Where(x => x.source_id == source_id && x.data_status == data_status).FirstOrDefault();
				await SetModel(_source_h_num_sign_db, _context, userId);
				return new JsonResult(new { success = true });
			}
			catch (Exception ex)
			{
				return new JsonResult(new { success = false });
			}
		}
		private async Task SetModel(S_History model, HssDbContext context, int userId)
		{
			if (model != null)
			{
				SetExistModel(model, userId);
				context.Update(model);
			}
			await context.SaveChangesAsync();
		}

		private void SetExistModel(S_History source_h, int userId)
		{
			source_h.data_status = data_status;
			source_h.muid = muid;
			source_h.kasu_composite_id = kasu_composite_id;
			source_h.kasu_id = kasu_id;
			source_h.kasu_map_id = kasu_map_id;
			source_h.lotus_id = lotus_id;
			source_h.edit_date = DateTime.Now;
			source_h.user_id = userId;
		}
	}


    [Keyless]
    public class Source_DocsFootageViewModel
    {
        public int data_status { get; set; }
        public int source_id { get; set; }
        public short? doc_type_id { get; set; }
        public int doc_num { get; set; }
        public string? doc_description { get; set; }
        public string? doc_url { get; set; }
        public string? doc_name { get; set; }
        [NotMapped]
        public bool? is_deleted { get; set; }
    }

	[Keyless]
	public class SourceHeatPowerOutputViewModel
    {
		public int? data_status { get; set; }
		public int? source_id { get; set; }
		public int? perspective_year { get; set; }
		public string? perspective_year_dt { get; set; }
		public short? hn_heat_out_scheme_type_id { get; set; }
		public int? count_out { get; set; }
		public short? temp_plan_id { get; set; }
		public short? temp_plan_heat_id { get; set; }
		public short? temp_plan_gvs_id { get; set; }
		public double? temp_supply_fact_T1 { get; set; }
		public double? temp_reverse_fact_T2 { get; set; }
		public double? temp_supply_fact_T3 { get; set; }
		public double? temp_reverse_fact_T4 { get; set; }
		public double? temp_supply_fact_T5 { get; set; }
		public double? temp_reverse_fact_T6 { get; set; }
		public double? temp_steam_consumers { get; set; }
		public double? pressure_steam_consumers { get; set; }
		public bool? avail_condens_pipe {  get; set; }
	}
}
