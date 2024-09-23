using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Areas.HPConsumers.Models
{
    /// <summary>
    /// Добавление / удаление потребителя
    /// </summary>
    [Keyless]
    public class Consumer_MainListUnit
    {
        public int? source_unom { get; set; }
        public string? source_name { get; set; }
        public string? source_status_name { get; set; }
        public string? unom_output { get; set; }
        public string? output_name { get; set; }
        public int? heat_point_id { get; set; }
        public string? tso_hp_num { get; set; }
        public string? hp_status_name { get; set; }
        public string? hp_address { get; set; }
		public int? consumer_id { get; set; }
		public string? status_name { get; set; }
        public string? region_short { get; set; }
        public string? district_name { get; set; }
        public string? consumer_address { get; set; }
        public string? purpose_build_type_id { get; set; }
        public string? cons_ptype_name { get; set; }
        public string? org_name { get; set; }
        public string? inn { get; set; }
        public string? purpose_name { get; set; }
        public int? func_num_zone { get; set; }
        public string? ps_type_name { get; set; }
        public string? rcat_name { get; set; }
        public string? is_in_soc_obj { get; set; }
        public string? is_in_open_gvs { get; set; }
        public string? is_in_caprepair { get; set; }
        public int? start_year_caprepair { get; set; }
        public string? unom_pp { get; set; }
        public decimal? area { get; set; }
        public int? cons_year_destr { get; set; }
        public string? type_prog_name { get; set; }
        public string? dev_prog_ptype_name { get; set; }
        public string? dev_prog_floor_name { get; set; }
		public string? developer { get; set; }
		public string? building_name { get; set; }
		public int? start_year { get; set; }
        public int? end_year { get; set; }
        public decimal? dev_prog_area { get; set; }
        public string? kadnum_zu { get; set; }
        public string? kadnum_oks { get; set; }
        public int? fias_build { get; set; }
        public int? fias_zu { get; set; }
        public decimal? hw_heat_decrease_coef { get; set; }
        public decimal? hw_vent_decrease_coef { get; set; }
        public decimal? hw_gvs_decrease_coef { get; set; }
        public decimal? hw_tech_decrease_coef { get; set; }
        public string? contract_status_name { get; set; }
        public string? ctr_org_name { get; set; }
        public string? ctr_org_inn { get; set; }
        public string? contract_num { get; set; }
        public DateTime? contract_date { get; set; }
        public DateTime? contract_valid_date { get; set; }
        public string? ctr_address { get; set; }
        public string? ctr_purpose_name { get; set; }
        public int? ctr_bti_build_unom { get; set; }
		public string? is_compare { get; set; }
		public string? diff_loads { get; set; }
		public decimal? ctr_hl_tech_hw { get; set; }
        public decimal? ctr_hl_heat_hw { get; set; }
        public decimal? ctr_hl_vent_hw { get; set; }
        public decimal? ctr_hl_gvss_hw { get; set; }
        public decimal? ctr_hl_sum_hw { get; set; }
        public decimal? ctr_hl_tech_steam { get; set; }
        public decimal? ctr_hl_heat_steam { get; set; }
        public decimal? ctr_hl_vent_steam { get; set; }
        public decimal? ctr_hl_gvss_steam { get; set; }
        public decimal? ctr_hl_sum_steam { get; set; }
        public decimal? ctr_hl_sum_steam_gkalh { get; set; }
        public decimal? hl_heat_hw { get; set; }
        public decimal? hl_vent_hw { get; set; }
        public decimal? hl_gvss_hw { get; set; }
        public decimal? hl_gvsm_hw { get; set; }
        public decimal? hl_tech_hw { get; set; }
        public decimal? hl_other_hw { get; set; }
        public decimal? hl_gvss_hw_sum { get; set; }
        public decimal? hl_gvsm_hw_sum { get; set; }
        public decimal? hl_heat_steam { get; set; }
        public decimal? hl_vent_steam { get; set; }
        public decimal? hl_gvss_steam { get; set; }
        public decimal? hl_gvsm_steam { get; set; }
        public decimal? hl_tech_steam { get; set; }
        public decimal? hl_other_steam { get; set; }
        public decimal? hl_gvss_steam_sum { get; set; }
        public decimal? hl_gvsm_steam_sum { get; set; }
        public decimal? calc_hl_heat_hw { get; set; }
        public decimal? calc_hl_vent_hw { get; set; }
        public decimal? calc_hl_gvss_hw { get; set; }
		public decimal? calc_hl_tech_hw { get; set; }
		public decimal? calc_hl_gvss_hw_sum { get; set; }
        public decimal? calc_hl_heat_steam { get; set; }
        public decimal? calc_hl_vent_steam { get; set; }
        public decimal? calc_hl_gvss_steam { get; set; }
        public decimal? calc_hl_tech_steam { get; set; }
        public decimal? calc_hl_gvss_steam_sum { get; set; }
        public decimal? b_calc_hl_heat_hw { get; set; }
        public decimal? b_calc_hl_vent_hw { get; set; }
        public decimal? b_calc_hl_gvss_hw { get; set; }
        public decimal? b_calc_hl_tech_hw { get; set; }
        public decimal? b_calc_hl_sum_hw { get; set; }
        public decimal? b_calc_hl_heat_steam { get; set; }
        public decimal? b_calc_hl_vent_steam { get; set; }
        public decimal? b_calc_hl_gvss_steam { get; set; }
        public decimal? b_calc_hl_tech_steam { get; set; }
        public decimal? b_calc_hl_gvss_steam_sum { get; set; }
        public decimal? b_calc_hl_gvss_steam_sum_gkalh { get; set; }
        public decimal? hc_heat_hw { get; set; }
        public decimal? hc_vent_hw { get; set; }
        public decimal? hc_gvs_hw { get; set; }
        public decimal? hc_tech_hw { get; set; }
        public decimal? calc_hc_hw_sum { get; set; }
        public decimal? hc_heat_steam { get; set; }
        public decimal? hc_vent_steam { get; set; }
        public decimal? hc_gvs_steam { get; set; }
        public decimal? hc_tech_steam { get; set; }
        public decimal? calc_hc_steam_sum { get; set; }
        public decimal? calc_hc_steam_sum_gkalh { get; set; }
        public decimal? fact_total_hc_hw { get; set; }
        public decimal? fact_total_hc_steam { get; set; }
        public decimal? fact_total_hc_steam_gkalh { get; set; }
        public decimal? fact_watercons_gvs { get; set; }
        public decimal? heat_carrier_consump_heat_hw { get; set; }
        public decimal? heat_carrier_consump_vent_hw { get; set; }
        public decimal? heat_carrier_consump_gvs_hw { get; set; }
        public decimal? heat_carrier_consump_tech_hw { get; set; }
        public decimal? heat_carrier_consump_sum_hw { get; set; }
        public decimal? heat_carrier_consump_heat_steam { get; set; }
        public decimal? heat_carrier_consump_vent_steam { get; set; }
        public decimal? heat_carrier_consump_gvs_steam { get; set; }
        public decimal? heat_carrier_consump_tech_steam { get; set; }
        public decimal? heat_carrier_consump_sum_steam { get; set; }
    }


    #region ConsumerHeaderAddRemoveDataViewModel
    [Keyless]
    public class ConsumerHeaderAddRemoveDataViewModel
    {
        public int? data_status { get; set; }
        public int? consumer_id { get; set; }
        public string? add_remove_gen_info_district_name { get; set; }
    }
    #endregion

    #region ConsumerAddRemoveGenInfoDataViewModel
    [Keyless]
    public class ConsumerAddRemoveGenInfoDataViewModel
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int? add_remove_gen_info_cons_org_owner_id { get; set; }
        public int? add_remove_gen_info_building_id { get; set; }

        [NotMapped]
        public bool? is_deleted { get; set; }
    }
    #endregion

    #region Consumers_DestRelCatHSDataViewModel
    [Keyless]
    public class Consumers_DestRelCatHSDataViewModel
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public short? category_rel_id { get; set; }
        public bool? is_in_soc_obj { get; set; }
        public short? type_purpose_id { get; set; }
        public short? main_purpose_type_id { get; set; }
        public short? type_prod_supply_id { get; set; }
        public string? purpose_name { get; set; }

        [NotMapped]
        public bool? is_deleted { get; set; }
    }
    #endregion

    #region Consumers_HeatLoadsSupplyContractDataViewModel
    [Keyless]
    public class Consumers_HeatLoadsSupplyContractDataViewModel
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int? contract_id { get; set; }
        public string? address { get; set; }
        public string? purpose { get; set; }
        public int? bti_build_unom { get; set; }
        public decimal? ctr_hl_heat_hw { get; set; }
        public decimal? ctr_hl_vent_hw { get; set; }
        public decimal? ctr_hl_gvsm_hw { get; set; }
        public decimal? ctr_hl_gvss_hw { get; set; }
        public decimal? ctr_hl_tech_hw { get; set; }
        public decimal? ctr_hl_other_hw { get; set; }
        public decimal? ctr_hl_cond_hw { get; set; }
        public decimal? ctr_hl_curtains_hw { get; set; }
        public decimal? ctr_hl_heat_steam { get; set; }
        public decimal? ctr_hl_vent_steam { get; set; }
        public decimal? ctr_hl_gvsm_steam { get; set; }
        public decimal? ctr_hl_gvss_steam { get; set; }
        public decimal? ctr_hl_tech_steam { get; set; }
        public decimal? ctr_hl_other_steam { get; set; }
        public decimal? ctr_hl_cond_steam { get; set; }
        public decimal? ctr_hl_curtains_steam { get; set; }
        public decimal? ctr_hl_gvss_steam_sum { get; set; }
        public decimal? ctr_hl_gvsm_steam_sum { get; set; }
        public decimal? ctr_hl_gvss_hw_sum { get; set; }
        public decimal? ctr_hl_gvsm_hw_sum { get; set; }
    }
    #endregion

    #region Consumers_YearParamsCalcLoadViewModel

    [Keyless]
    public class Consumers_YearParamsCalcLoadViewModel
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int? consumer_start_year_caprepair { get; set; }
        public int? consumer_perspective_year { get; set; }
        public string? consumer_perspective_year_dt { get; set; }
        public short? consumer_status_id { get; set; }
        [Precision(18, 8)]
        public decimal? area { get; set; }
        public decimal? increase_area { get; set; }
        public string? unom_source_name { get; set; }
        public int? consumer_heat_point_id { get; set; }
        public bool? is_in_open_gvs { get; set; }
        public int? main_dev_prog_id { get; set; }
        public bool? calc_on_main_dev_prog { get; set; }
        public decimal? calc_hl_heat_hw { get; set; }
        public decimal? calc_hl_vent_hw { get; set; }
        public decimal? calc_hl_gvss_hw { get; set; }
        public decimal? calc_hl_tech_hw { get; set; }
        public decimal? calc_hl_sum_hw { get; set; }
        public decimal? calc_hl_heat_steam { get; set; }
        public decimal? calc_hl_vent_steam { get; set; }
        public decimal? calc_hl_gvss_steam { get; set; }
        public decimal? calc_hl_tech_steam { get; set; }
        public decimal? calc_hl_sum_steam { get; set; }
    }

    #endregion

    #region Consumers_YearHeatConsumptionViewModel

    [Keyless]
    public class Consumers_YearHeatConsumptionViewModel_Perspective
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int? consumer_perspective_year { get; set; }
        public string? consumer_perspective_year_dt { get; set; }
        public string? consumer_status_name { get; set; }
        public string? unom_source_name { get; set; }
        public string? consumer_heat_point_name { get; set; }
        public decimal? watercons_gvs { get; set; }
        public decimal? hc_heat_hw { get; set; }
        public decimal? hc_vent_hw { get; set; }
        public decimal? hc_gvs_hw { get; set; }
        public decimal? hc_tech_hw { get; set; }
        public decimal? hc_sum_hw { get; set; }
        public decimal? rel_hc_sum_hw { get; set; }
        public decimal? hc_heat_steam { get; set; }
        public decimal? hc_vent_steam { get; set; }
        public decimal? hc_gvs_steam { get; set; }
        public decimal? hc_tech_steam { get; set; }
        public decimal? hc_sum_steam { get; set; }
        public decimal? rel_hc_sum_steam { get; set; }
    }

    [Keyless]
    public class Consumers_YearHeatConsumptionViewModel_Fact
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public short? hc_calc_method_id { get; set; }
        public decimal? fact_total_hc_hw { get; set; }
        public decimal? fact_total_hc_steam { get; set; }
        public decimal? fact_watercons_gvs { get; set; }
    }

    [Keyless]
    public class Consumers_YearHeatConsumptionViewModel
    {
        public Consumers_YearHeatConsumptionViewModel_Fact fact { get; set; }
        public List<Consumers_YearHeatConsumptionViewModel_Perspective> perspective { get; set; }
    }

    #endregion

    #region Consumers_NumSignOtherDBViewModel

    [Keyless]
    public class Consumers_NumSignOtherDBViewModel
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public string? muid { get; set; }
        public string? con_code { get; set; }
        public long? kod_dp_m { get; set; }
        public long? kks_m { get; set; }
    }

    #endregion

    #region Consumers_PerspectiveDev_SubPartialViewModel

    [Keyless]
    public class Consumers_PerspectiveDev_SubPartialViewModel
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int dev_prog_id { get; set; }
        public string? type_prog_name { get; set; }
        public string? obj_num { get; set; }
        public DateTime? connect_req_date { get; set; }
        public string? connect_req_num { get; set; }
        public DateTime? connect_agree_date { get; set; }
        public string? connect_agree_num { get; set; }
        public int? year_dev_prog { get; set; }
        public DateTime? date_dev_prog { get; set; }
        public string? prog_doc_num { get; set; }
        public string? appl_name { get; set; }
        public DateTime? connect_cond_date { get; set; }
        public string? connect_cond_num { get; set; }
        public string? connect_point_hn { get; set; }
        public decimal? area { get; set; }
        public int? start_year { get; set; }
        public int? end_year { get; set; }
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
    }

    #endregion

    #region Consumers_PerspectiveDev_YearDynamicViewModel

    [Keyless]
    public class Consumers_PerspectiveDev_YearDynamicViewModel
    {
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public int? consumer_id { get; set; }
        public int? dev_prog_id { get; set; }
        public decimal? increase_area { get; set; }
        public decimal? calc_sum_hw { get; set; }
        public decimal? calc_sum_steam { get; set; }
    }

    #endregion

    #region Consumers_SubPartialYearDynamicViewModel

    [Keyless]
    public class Consumers_SubPartialYearDynamicViewModel
    {
        public Consumers_PerspectiveDev_SubPartialViewModel Consumers_PerspectiveDev_SubPartialViewModel { get; set; }
        public List<Consumers_PerspectiveDev_YearDynamicViewModel> Consumers_PerspectiveDev_YearDynamicViewModel { get; set; }
    }

    #endregion

    #region Consumers_PerspectiveDevelopmentViewModel

    [Keyless]
    public class Consumers_PerspectiveDevelopmentViewModel
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int? perspective_year { get; set; }
        public int? dev_prog_id { get; set; }
        public bool? is_main_prog { get; set; }
        public string? unom_num { get; set; }
        public string? type_prog_name { get; set; }
        public string? obj_num { get; set; }
        public string? obj_name { get; set; }
        public decimal? area { get; set; }
        public int? start_year { get; set; }
        public int? end_year { get; set; }
        public decimal? hl_gvss_hw_sum { get; set; }
        public decimal? calc_sum_hw { get; set; }
    }

    #endregion
}
