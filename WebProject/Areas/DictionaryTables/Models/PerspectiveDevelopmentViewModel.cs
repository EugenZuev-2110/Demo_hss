using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Controllers;
using WebProject.Data;

namespace WebProject.Areas.DictionaryTables.Models
{
    [Keyless]
    public class PerspectiveDevelopment_MainListViewModel
    {
        public int? dev_prog_id { get; set; }
        public string? unom_num { get; set; }
        public string? type_prog_name { get; set; }
    }

    [Keyless]
    public class PerspectiveDevelopmentHeaderViewModel
    {
        public int? dev_prog_id { get; set; }
    }

    [Keyless]
    public class PerspectiveDevelopment_GenInfoViewModel
    {
        public int? dev_prog_id { get; set; }
        public string? prog_name { get; set; }
        public int? type_dev_prog_id { get; set; }
        public DateTime? date_dev_prog { get; set; }
        public string? prog_doc_num { get; set; }
        public int? year_dev_prog { get; set; }
        public int? layer_id { get; set; }
        public int? layer_sys { get; set; }
        public bool? is_deleted { get; set; }
        public bool? is_new_dev_prog { get; set; }
    }

    [Keyless]
    public class PerspectiveDevelopment_FacilityListViewModel
    {
        public int? consumer_id { get; set; }
        public int? dev_prog_id { get; set; }
        public string? obj_num { get; set; }
        public string? obj_name { get; set; }
        public string? address { get; set; }
    }


    [Keyless]
    public class PerspectiveDevelopment_FacilitiesHeaderViewModel
    {
        public int? consumer_id { get; set; }
        public int? dev_prog_id { get; set; }
        public string? unom_prog_name { get; set; }
        [Precision(18, 8)]
        public decimal? area { get; set; }
        public decimal? hl_gvss_hw_sum { get; set; }
        public decimal? hl_gvss_steam_sum { get; set; }
        public decimal? calc_sum_hw { get; set; }
        public decimal? calc_sum_steam { get; set; }
        public bool? is_new_dev_prog { get; set; }
    }

    [Keyless]
    public class PerspectiveDevelopmentFacility_GenInfoViewModel
    {
        public int? consumer_id { get; set; }
        public int? dev_prog_id { get; set; }
        public int? consumer_id_old { get; set; }
        public int? building_id { get; set; }
        public string? obj_num { get; set; }
        public string? address { get; set; }
        public int? district_id { get; set; }
        public string? obj_name { get; set; }
        public string? appl_name { get; set; }
        public string? cad_num_zu { get; set; }
        public int? bti_build_unom { get; set; }
        public DateTime? connect_req_date { get; set; }
        public string? connect_req_num { get; set; }
        public DateTime? connect_cond_date { get; set; }
        public string? connect_cond_num { get; set; }
        public DateTime? connect_agree_date { get; set; }
        public string? connect_agree_num { get; set; }
        public string? connect_point_hn { get; set; }
        public bool? is_deleted { get; set; }
        public bool? is_new_facility { get; set; }
    }

    #region PerspectiveDevelopment_Facilities_DestRelCatHSViewModel
    [Keyless]
    public class PerspectiveDevelopment_Facilities_DestRelCatHSViewModel
    {
        public int consumer_id { get; set; }
        public int? dev_prog_id { get; set; }
        public short? category_rel_id { get; set; }
        //public bool? is_in_soc_obj { get; set; }
        public short? type_purpose_id { get; set; }
        public short? main_purpose_type_id { get; set; }
        public short? type_prod_supply_id { get; set; }
        public short? floor_id { get; set; }

        [NotMapped]
        public bool? is_deleted { get; set; }
    }

    #endregion

    #region PerspectiveDevelopment_Facilities_BuildingVolumeHeatLoadsViewModel

    [Keyless]
    public class PerspectiveDevelopment_Facilities_BuildingVolumeHeatLoadsViewModel : IActionController
    {
        public int consumer_id { get; set; }
        public int? dev_prog_id { get; set; }
        public int? data_status { get; set; }
        public short? area_calc_type_id { get; set; }
        public decimal? area { get; set; }
        public int? start_year { get; set; }
        public int? end_year { get; set; }
        public decimal? hw_heat_decrease_coef { get; set; }
        public decimal? hw_vent_decrease_coef { get; set; }
        public decimal? hw_gvs_decrease_coef { get; set; }
        public decimal? spec_hc_declared { get; set; }
        public decimal? spec_hc_calculated { get; set; }
        public decimal? spec_hc_deviation { get; set; }
        public decimal? spec_hc_deviation_norm { get; set; }
        public short? hl_calc_type_id { get; set; }
        public decimal? dp_lcc_hw_heat_decrease_coef { get; set; }
        public decimal? dp_lcc_hw_vent_decrease_coef { get; set; }
        public decimal? dp_lcc_hw_gvs_decrease_coef { get; set; }
        public decimal? dp_lcc_hw_tech_decrease_coef { get; set; }
        public decimal? dp_lcc_steam_heat_decrease_coef { get; set; }
        public decimal? dp_lcc_steam_vent_decrease_coef { get; set; }
        public decimal? dp_lcc_steam_gvs_decrease_coef { get; set; }
        public decimal? dp_lcc_steam_tech_decrease_coef { get; set; }
        public decimal? dp_lcc_heat_loss_heat_net_coef { get; set; }
        public decimal? dp_lcc_heat_loss_steam_net_coef { get; set; }
        public decimal? dpld_hl_tech_hw { get; set; }
        public decimal? dpld_hl_heat_hw { get; set; }
        public decimal? dpld_hl_vent_hw { get; set; }
        public decimal? dpld_hl_cond_hw { get; set; }
        public decimal? dpld_hl_gvss_hw { get; set; }
        public decimal? dpld_hl_gvsm_hw { get; set; }
        public decimal? dpld_hl_curtains_hw { get; set; }
        public decimal? dpld_hl_other_hw { get; set; }
        public decimal? dpld_hl_gvss_hw_sum { get; set; }
        public decimal? dpld_hl_gvsm_hw_sum { get; set; }
        public decimal? dpl_contr_hl_tech_hw { get; set; }
        public decimal? dpl_contr_hl_heat_hw { get; set; }
        public decimal? dpl_contr_hl_vent_hw { get; set; }
        public decimal? dpl_contr_hl_cond_hw { get; set; }
        public decimal? dpl_contr_hl_gvss_hw { get; set; }
        public decimal? dpl_contr_hl_gvsm_hw { get; set; }
        public decimal? dpl_contr_hl_curtains_hw { get; set; }
        public decimal? dpl_contr_hl_other_hw { get; set; }
        public decimal? dpl_contr_hl_gvss_hw_sum { get; set; }
        public decimal? dpl_contr_hl_gvsm_hw_sum { get; set; }
        public decimal? equal_hl_tech_hw { get; set; }
        public decimal? equal_hl_heat_hw { get; set; }
        public decimal? equal_hl_vent_hw { get; set; }
        public decimal? equal_hl_gvss_hw { get; set; }
        public decimal? equal_hl_gvss_hw_sum { get; set; }
        public decimal? dplc_hl_tech_hw { get; set; }
        public decimal? dplc_hl_heat_hw { get; set; }
        public decimal? dplc_hl_vent_hw { get; set; }
        public decimal? dplc_hl_gvss_hw { get; set; }
        public decimal? dplc_hl_gvss_hw_sum { get; set; }
        public decimal? dpld_dplc_hl_tech_hw_rel { get; set; }
        public decimal? dpld_dplc_hl_heat_hw_rel { get; set; }
        public decimal? dpld_dplc_hl_vent_hw_rel { get; set; }
        public decimal? dpld_dplc_hl_gvss_hw_rel { get; set; }
        public decimal? dpld_dplc_hl_gvss_hw_rel_sum { get; set; }
        public decimal? dpld_hl_tech_steam { get; set; }
        public decimal? dpld_hl_heat_steam { get; set; }
        public decimal? dpld_hl_vent_steam { get; set; }
        public decimal? dpld_hl_cond_steam { get; set; }
        public decimal? dpld_hl_gvss_steam { get; set; }
        public decimal? dpld_hl_gvsm_steam { get; set; }
        public decimal? dpld_hl_curtains_steam { get; set; }
        public decimal? dpld_hl_other_steam { get; set; }
        public decimal? dpld_hl_gvss_steam_sum { get; set; }
        public decimal? dpld_hl_gvsm_steam_sum { get; set; }
        public decimal? dpl_contr_hl_tech_steam { get; set; }
        public decimal? dpl_contr_hl_heat_steam { get; set; }
        public decimal? dpl_contr_hl_vent_steam { get; set; }
        public decimal? dpl_contr_hl_cond_steam { get; set; }
        public decimal? dpl_contr_hl_gvss_steam { get; set; }
        public decimal? dpl_contr_hl_gvsm_steam { get; set; }
        public decimal? dpl_contr_hl_curtains_steam { get; set; }
        public decimal? dpl_contr_hl_other_steam { get; set; }
        public decimal? dpl_contr_hl_gvss_steam_sum { get; set; }
        public decimal? dpl_contr_hl_gvsm_steam_sum { get; set; }
        public decimal? equal_hl_tech_steam { get; set; }
        public decimal? equal_hl_heat_steam { get; set; }
        public decimal? equal_hl_vent_steam { get; set; }
        public decimal? equal_hl_gvss_steam { get; set; }
        public decimal? equal_hl_gvss_steam_sum { get; set; }
        public decimal? dplc_hl_tech_steam { get; set; }
        public decimal? dplc_hl_heat_steam { get; set; }
        public decimal? dplc_hl_vent_steam { get; set; }
        public decimal? dplc_hl_gvss_steam { get; set; }
        public decimal? dplc_hl_gvss_steam_sum { get; set; }
        public decimal? dpld_dplc_hl_tech_steam_rel { get; set; }
        public decimal? dpld_dplc_hl_heat_steam_rel { get; set; }
        public decimal? dpld_dplc_hl_vent_steam_rel { get; set; }
        public decimal? dpld_dplc_hl_gvss_steam_rel { get; set; }
        public decimal? dpld_dplc_hl_gvss_steam_rel_sum { get; set; }
        public decimal? spec_hc_tech { get; set; }
        public decimal? spec_hc_heat { get; set; }
        public decimal? spec_hc_vent { get; set; }
        public decimal? spec_hc_gvss { get; set; }
        public decimal? spec_hc_gvss_sum { get; set; }

        [NotMapped]
        public bool? is_deleted { get; set; }

        public new async Task<IActionResult> SaveAsync(HssDbContext _context, int userId, IFormCollection keyValues = null)
        {
            try
            {
                var dev_prog_cons = await _context.DevProgrammsConsumers.FirstOrDefaultAsync(n => n.consumer_id == consumer_id && n.dev_prog_id == dev_prog_id);
                var dev_prog_area_calc_coef = await _context.DevProgrammsAreaCalcCoefs.FirstOrDefaultAsync(n => n.consumer_id == consumer_id && n.dev_prog_id == dev_prog_id);
                var dev_prog_load_calc_coef = await _context.DevProgrammsLoadsCalcCoefs.FirstOrDefaultAsync(n => n.consumer_id == consumer_id && n.dev_prog_id == dev_prog_id);
                var dev_prog_load_contract = await _context.DevProgrammsLoadsContract.FirstOrDefaultAsync(n => n.consumer_id == consumer_id && n.dev_prog_id == dev_prog_id);
                var dev_prog_load_declared = await _context.DevProgrammsLoadsDeclared.FirstOrDefaultAsync(n => n.consumer_id == consumer_id && n.dev_prog_id == dev_prog_id);

                await SetModel(dev_prog_cons, userId, _context);
                await SetModel(dev_prog_area_calc_coef, userId, _context);
                await SetModel(dev_prog_load_calc_coef, userId, _context);
                await SetModel(dev_prog_load_contract, userId, _context);
                await SetModel(dev_prog_load_declared, userId, _context);
                await _context.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                //_m_c.ExLog_Save("HpSwitching_Save", $"data_status={data_status},perspective_year_start={date_start},consumer_id={model.individual_fee_one_consumer_id},dev_prog_id={model.individual_fee_one_dev_prog_id}", ex.Message, userId);
                return new JsonResult(new { success = false });
            }
        }

        private async Task SetModel(DevProgrammsConsumers model, int userId, HssDbContext context)
        {
            if (model == null)
            {
                model = new DevProgrammsConsumers { consumer_id = consumer_id, dev_prog_id = dev_prog_id ?? 0, create_date = DateTime.Now };
                SetExistModel(model, userId, true);
                await context.AddAsync(model);
            }
            else
            {
                SetExistModel(model, userId);
                context.Update(model);
            }
        }

        private async Task SetModel(DevProgrammsAreaCalcCoefs model, int userId, HssDbContext context)
        {
            if (model == null)
            {
                model = new DevProgrammsAreaCalcCoefs { consumer_id = consumer_id, dev_prog_id = dev_prog_id ?? 0, create_date = DateTime.Now };
                SetExistModel(model, userId, true);
                await context.AddAsync(model);
            }
            else
            {
                SetExistModel(model, userId);
                context.Update(model);
            }
        }

        private async Task SetModel(DevProgrammsLoadsCalcCoefs model, int userId, HssDbContext context)
        {
            if (model == null)
            {
                model = new DevProgrammsLoadsCalcCoefs { consumer_id = consumer_id, dev_prog_id = dev_prog_id ?? 0, create_date = DateTime.Now };
                SetExistModel(model, userId, true);
                await context.AddAsync(model);
            }
            else
            {
                SetExistModel(model, userId);
                context.Update(model);
            }
        }

        private async Task SetModel(DevProgrammsLoadsContract model, int userId, HssDbContext context)
        {
            if (model == null)
            {
                model = new DevProgrammsLoadsContract { consumer_id = consumer_id, dev_prog_id = dev_prog_id ?? 0, create_date = DateTime.Now };
                SetExistModel(model, userId, true);
                await context.AddAsync(model);
            }
            else
            {
                SetExistModel(model, userId);
                context.Update(model);
            }
        }

        private async Task SetModel(DevProgrammsLoadsDeclared model, int userId, HssDbContext context)
        {
            if (model == null)
            {
                model = new DevProgrammsLoadsDeclared { consumer_id = consumer_id, dev_prog_id = dev_prog_id ?? 0, create_date = DateTime.Now };
                SetExistModel(model, userId, true);
                await context.AddAsync(model);
            }
            else
            {
                SetExistModel(model, userId);
                context.Update(model);
            }
        }

        private void SetExistModel(DevProgrammsConsumers dev_prog, int userId, bool isNewData = false)
        {
            dev_prog.area_calc_type_id = area_calc_type_id;
            dev_prog.area = area;
            dev_prog.start_year = start_year;
            dev_prog.end_year = end_year;
            dev_prog.hl_calc_type_id = hl_calc_type_id;
            dev_prog.hl_tech_hw = dplc_hl_tech_hw;
            dev_prog.hl_heat_hw = dplc_hl_heat_hw;
            dev_prog.hl_vent_hw = dplc_hl_vent_hw;
            dev_prog.hl_gvss_hw = dplc_hl_gvss_hw;
            dev_prog.hl_tech_steam = dplc_hl_tech_steam;
            dev_prog.hl_heat_steam = dplc_hl_heat_steam;
            dev_prog.hl_vent_steam = dplc_hl_vent_steam;
            dev_prog.hl_gvss_steam = dplc_hl_gvss_steam;

            if (!isNewData)
                dev_prog.edit_date = DateTime.Now;
            dev_prog.user_id = userId;
            dev_prog.is_deleted = is_deleted ?? false;
        }

        private void SetExistModel(DevProgrammsAreaCalcCoefs dev_prog, int userId, bool isNewData = false)
        {
            dev_prog.hw_heat_decrease_coef = hw_heat_decrease_coef;
            dev_prog.hw_vent_decrease_coef = hw_vent_decrease_coef;
            dev_prog.hw_gvs_decrease_coef = hw_gvs_decrease_coef;

            if (!isNewData)
                dev_prog.edit_date = DateTime.Now;
            dev_prog.user_id = userId;
        }

        private void SetExistModel(DevProgrammsLoadsCalcCoefs dev_prog, int userId, bool isNewData = false)
        {
            dev_prog.heat_loss_heat_net_coef = dp_lcc_heat_loss_heat_net_coef;
            dev_prog.heat_loss_steam_net_coef = dp_lcc_heat_loss_steam_net_coef;
            dev_prog.hw_heat_decrease_coef = dp_lcc_hw_heat_decrease_coef;
            dev_prog.hw_vent_decrease_coef = dp_lcc_hw_vent_decrease_coef;
            dev_prog.hw_gvs_decrease_coef = dp_lcc_hw_gvs_decrease_coef;
            dev_prog.hw_tech_decrease_coef = dp_lcc_hw_tech_decrease_coef;
            dev_prog.steam_heat_decrease_coef = dp_lcc_steam_heat_decrease_coef;
            dev_prog.steam_vent_decrease_coef = dp_lcc_steam_vent_decrease_coef;
            dev_prog.steam_gvs_decrease_coef = dp_lcc_steam_gvs_decrease_coef;
            dev_prog.steam_tech_decrease_coef = dp_lcc_steam_tech_decrease_coef;

            if (!isNewData)
                dev_prog.edit_date = DateTime.Now;
            dev_prog.user_id = userId;
        }

        private void SetExistModel(DevProgrammsLoadsContract dev_prog, int userId, bool isNewData = false)
        {
            dev_prog.hl_tech_hw = dpl_contr_hl_tech_hw;
            dev_prog.hl_heat_hw = dpl_contr_hl_heat_hw;
            dev_prog.hl_vent_hw = dpl_contr_hl_vent_hw;
            dev_prog.hl_cond_hw = dpl_contr_hl_cond_hw;
            dev_prog.hl_gvss_hw = dpl_contr_hl_gvss_hw;
            dev_prog.hl_gvsm_hw = dpl_contr_hl_gvsm_hw;
            dev_prog.hl_curtains_hw = dpl_contr_hl_curtains_hw;
            dev_prog.hl_other_hw = dpl_contr_hl_other_hw;
            dev_prog.hl_gvss_hw_sum = dpl_contr_hl_gvss_hw_sum;
            dev_prog.hl_gvsm_hw_sum = dpl_contr_hl_gvsm_hw_sum;
            dev_prog.hl_tech_steam = dpl_contr_hl_tech_steam;
            dev_prog.hl_heat_steam = dpl_contr_hl_heat_steam;
            dev_prog.hl_vent_steam = dpl_contr_hl_vent_steam;
            dev_prog.hl_cond_steam = dpl_contr_hl_cond_steam;
            dev_prog.hl_gvss_steam = dpl_contr_hl_gvss_steam;
            dev_prog.hl_gvsm_steam = dpl_contr_hl_gvsm_steam;
            dev_prog.hl_curtains_steam = dpl_contr_hl_curtains_steam;
            dev_prog.hl_other_steam = dpl_contr_hl_other_steam;
            dev_prog.hl_gvss_steam_sum = dpl_contr_hl_gvss_steam_sum;
            dev_prog.hl_gvsm_steam_sum = dpl_contr_hl_gvsm_steam_sum;

            if (!isNewData)
                dev_prog.edit_date = DateTime.Now;
            dev_prog.user_id = userId;
        }

        private void SetExistModel(DevProgrammsLoadsDeclared dev_prog, int userId, bool isNewData = false)
        {
            dev_prog.hl_tech_hw = dpld_hl_tech_hw;
            dev_prog.hl_heat_hw = dpld_hl_heat_hw;
            dev_prog.hl_vent_hw = dpld_hl_vent_hw;
            dev_prog.hl_cond_hw = dpld_hl_cond_hw;
            dev_prog.hl_gvss_hw = dpld_hl_gvss_hw;
            dev_prog.hl_gvsm_hw = dpld_hl_gvsm_hw;
            dev_prog.hl_curtains_hw = dpld_hl_curtains_hw;
            dev_prog.hl_other_hw = dpld_hl_other_hw;
            dev_prog.hl_gvss_hw_sum = dpld_hl_gvss_hw_sum;
            dev_prog.hl_gvsm_hw_sum = dpld_hl_gvsm_hw_sum;
            dev_prog.hl_tech_steam = dpld_hl_tech_steam;
            dev_prog.hl_heat_steam = dpld_hl_heat_steam;
            dev_prog.hl_vent_steam = dpld_hl_vent_steam;
            dev_prog.hl_cond_steam = dpld_hl_cond_steam;
            dev_prog.hl_gvss_steam = dpld_hl_gvss_steam;
            dev_prog.hl_gvsm_steam = dpld_hl_gvsm_steam;
            dev_prog.hl_curtains_steam = dpld_hl_curtains_steam;
            dev_prog.hl_other_steam = dpld_hl_other_steam;
            dev_prog.hl_gvss_steam_sum = dpld_hl_gvss_steam_sum;
            dev_prog.hl_gvsm_steam_sum = dpld_hl_gvsm_steam_sum;

            if (!isNewData)
                dev_prog.edit_date = DateTime.Now;
            dev_prog.user_id = userId;
        }
    }

    #endregion

    #region MainIndicatorDynamic

    public class DevProgrammsLoadsCalculated_List : List<DevProgrammsLoadsCalculated>, IActionController
    {
        public Task<IActionResult> SaveAsync(HssDbContext _context, int userId, IFormCollection keyValues)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
