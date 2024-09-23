using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Areas.HPConsumers.Models
{
    [Table("ConsumersHistory", Schema = "consumers")]
    public class ConsumersHistory
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int? main_dev_prog_id { get; set; }
        public bool? calc_on_main_dev_prog { get; set; }
        public string? purpose_name { get; set; }
        public short? type_purpose_id { get; set; }
        public short? type_prod_supply_id { get; set; }
        public short? category_rel_id { get; set; }
        public bool? is_in_soc_obj { get; set; }
        public int? start_year_caprepair { get; set; }
        public int? manag_org_id { get; set; }
        public bool? is_deleted { get; set; }
        public string? muid { get; set; }
        public long? bti_build_unom { get; set; }
        public string? con_code { get; set; }
        public long? kod_dp_m { get; set; }
        public long? kks_m { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("Consumers", Schema = "consumers")]
    public class Consumers
    {
        public int consumer_id { get; set; }
        public int? building_id { get; set; }
        public string? unom_pp { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("Dict_ReliabilityCategories", Schema = "consumers")]
    public class Dict_ReliabilityCategories
    {
        public short Id { get; set; }
        public string? rcat_name { get; set; }
        public string? rcat_short { get; set; }
    }

    [Table("Dict_CalcPurposeTypes", Schema = "consumers")]
    public class Dict_CalcPurposeTypes
    {
        public short Id { get; set; }
        public string? cpurp_type_name { get; set; }
        public short? build_type_id { get; set; }
        public short? main_purpose_type_id { get; set; }
        public short? floor_id { get; set; }
    }

    [Table("Dict_ProdSupplyType", Schema = "consumers")]
    public class Dict_ProdSupplyType
    {
        public short Id { get; set; }
        public string? ps_type_name { get; set; }
        public string? ps_type_short { get; set; }
    }

    [Table("Dict_MainPurposeTypes", Schema = "consumers")]
    public class Dict_MainPurposeTypes
    {
        public short Id { get; set; }
        public string? ptype_name { get; set; }
        public string? ptype_short { get; set; }
    }

    [Table("ContractLoads", Schema = "consumers")]
    public class ContractLoads
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int? contract_id { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_heat_hw { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_vent_hw { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_gvsm_hw { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_gvss_hw { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_tech_hw { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_other_hw { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_cond_hw { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_curtains_hw { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_heat_steam { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_vent_steam { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_gvsm_steam { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_gvss_steam { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_tech_steam { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_other_steam { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_cond_steam { get; set; }
        [Precision(18, 8)]
        public decimal? ctr_hl_curtains_steam { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("ContractConsumers", Schema = "consumers")]
    public class ContractConsumers
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int? contract_id { get; set; }
        public string? address { get; set; }
        public string? purpose { get; set; }
        public int? bti_build_unom { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("LoadsCalculates", Schema = "consumers")]
    public class LoadsCalculates
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int perspective_year { get; set; }
        public short? consumer_status_id { get; set; }
        public int? heat_point_id { get; set; }
        [Precision(18, 8)]
        public decimal? area { get; set; }
        [Precision(18, 8)]
        public decimal? increase_area { get; set; }
        [Precision(18, 8)]
        public decimal? calc_hl_heat_hw { get; set; }
        [Precision(18, 8)]
        public decimal? calc_hl_vent_hw { get; set; }
        [Precision(18, 8)]
        public decimal? calc_hl_gvss_hw { get; set; }
        [Precision(18, 8)]
        public decimal? calc_hl_tech_hw { get; set; }
        [Precision(18, 8)]
        public decimal? calc_hl_heat_steam { get; set; }
        [Precision(18, 8)]
        public decimal? calc_hl_vent_steam { get; set; }
        [Precision(18, 8)]
        public decimal? calc_hl_gvss_steam { get; set; }
        [Precision(18, 8)]
        public decimal? calc_hl_tech_steam { get; set; }
        public bool? is_in_open_gvs { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("HeatWaterConsumptions_Fact", Schema = "consumers")]
    public class HeatWaterConsumptions_Fact
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public short? hc_calc_method_id { get; set; }
        [Precision(18, 8)]
        public decimal? fact_total_hc_hw { get; set; }
        [Precision(18, 8)]
        public decimal? fact_total_hc_steam { get; set; }
        [Precision(18, 8)]
        public decimal? fact_watercons_gvs { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("HeatConsumptions_Perspective", Schema = "consumers")]
    public class HeatConsumptions_Perspective
    {
        public int data_status { get; set; }
        public int consumer_id { get; set; }
        public int? perspective_year { get; set; }
        [Precision(18, 8)]
        public decimal? watercons_gvs { get; set; }
        [Precision(18, 8)]
        public decimal? hc_heat_hw { get; set; }
        [Precision(18, 8)]
        public decimal? hc_vent_hw { get; set; }
        [Precision(18, 8)]
        public decimal? hc_gvs_hw { get; set; }
        [Precision(18, 8)]
        public decimal? hc_tech_hw { get; set; }
        [Precision(18, 8)]
        public decimal? hc_heat_steam { get; set; }
        [Precision(18, 8)]
        public decimal? hc_vent_steam { get; set; }
        [Precision(18, 8)]
        public decimal? hc_gvs_steam { get; set; }
        [Precision(18, 8)]
        public decimal? hc_tech_steam { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("DevProgramms", Schema = "consumers")]
    public class DevProgramms
    {
        public int dev_prog_id { get; set; }
        public string? unom_num { get; set; }
        public int? layer_id { get; set; }
        public int? layer_sys { get; set; }
        public int? type_dev_prog_id { get; set; }
        public string? prog_name { get; set; }
        public string? prog_doc_num { get; set; }
        public int? year_dev_prog { get; set; }
        public DateTime? date_dev_prog { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("DevProgrammsConsumers", Schema = "consumers")]
    public class DevProgrammsConsumers
    {
        public int consumer_id { get; set; }
        public int dev_prog_id { get; set; }
        public short? hl_calc_type_id { get; set; }
        public short? area_calc_type_id { get; set; }
        public int? bti_build_unom { get; set; }
        public string? obj_num { get; set; }
        public string? obj_name { get; set; }
        public string? appl_name { get; set; }
        public int? district_id { get; set; }
        public short? type_purpose_id { get; set; }
        public short? type_prod_supply_id { get; set; }
        public string? address { get; set; }
        public string? cad_num_zu { get; set; }
        public short? floors_id { get; set; }
        public short? type_purpose_calc_hl_id { get; set; }
        [Precision(18, 3)]
        public decimal? area { get; set; }
        public int? start_year { get; set; }
        public int? end_year { get; set; }
        public string? connect_point_hn { get; set; }
        public short? category_rel_id { get; set; }
        public string? connect_req_num { get; set; }
        public DateTime? connect_req_date { get; set; }
        public string? connect_cond_num { get; set; }
        public DateTime? connect_cond_date { get; set; }
        public string? connect_agree_num { get; set; }
        public DateTime? connect_agree_date { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
        public decimal? hl_tech_hw { get; set; }
        public decimal? hl_heat_hw { get; set; }
        public decimal? hl_vent_hw { get; set; }
        public decimal? hl_gvss_hw { get; set; }
        public decimal? hl_tech_steam { get; set; }
        public decimal? hl_heat_steam { get; set; }
        public decimal? hl_vent_steam { get; set; }
        public decimal? hl_gvss_steam { get; set; }
    }

    [Table("Dict_DevProgTypes", Schema = "consumers")]
    public class Dict_DevProgTypes
    {
        public short Id { get; set; }
        public string? type_prog_name { get; set; }
        public string? type_prog_short { get; set; }
    }

    [Table("Dict_CalcHeatLoadsTypes", Schema = "consumers")]
    public class Dict_CalcHeatLoadsTypes
    {
        public short Id { get; set; }
        public string? hl_calc_type_name { get; set; }
    }

    [Table("Dict_CalcAreaTypes", Schema = "consumers")]
    public class Dict_CalcAreaTypes
    {
        public short Id { get; set; }
        public string? area_calc_type_name { get; set; }
    }

    [Table("DevProgrammsAreaCalcCoefs", Schema = "consumers")]
    public class DevProgrammsAreaCalcCoefs
    {
        public int consumer_id { get; set; }
        public int? dev_prog_id { get; set; }
        public decimal? hw_heat_decrease_coef { get; set; }
        public decimal? hw_vent_decrease_coef { get; set; }
        public decimal? hw_gvs_decrease_coef { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("DevProgrammsLoadsCalcCoefs", Schema = "consumers")]
    public class DevProgrammsLoadsCalcCoefs
    {
        public int consumer_id { get; set; }
        public int? dev_prog_id { get; set; }
        public decimal? hw_heat_decrease_coef { get; set; }
        public decimal? hw_vent_decrease_coef { get; set; }
        public decimal? hw_gvs_decrease_coef { get; set; }
        public decimal? hw_tech_decrease_coef { get; set; }
        public decimal? steam_heat_decrease_coef { get; set; }
        public decimal? steam_vent_decrease_coef { get; set; }
        public decimal? steam_gvs_decrease_coef { get; set; }
        public decimal? steam_tech_decrease_coef { get; set; }
        public decimal? heat_loss_heat_net_coef { get; set; }
        public decimal? heat_loss_steam_net_coef { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("DevProgrammsLoadsContract", Schema = "consumers")]
    public class DevProgrammsLoadsContract
    {
        public int consumer_id { get; set; }
        public int? dev_prog_id { get; set; }
        public decimal? hl_heat_hw { get; set; }
        public decimal? hl_vent_hw { get; set; }
        public decimal? hl_gvss_hw { get; set; }
        public decimal? hl_gvsm_hw { get; set; }
        public decimal? hl_tech_hw { get; set; }
        public decimal? hl_cond_hw { get; set; }
        public decimal? hl_curtains_hw { get; set; }
        public decimal? hl_other_hw { get; set; }
        public decimal? hl_gvss_hw_sum { get; set; }
        public decimal? hl_gvsm_hw_sum { get; set; }
        public decimal? hl_heat_steam { get; set; }
        public decimal? hl_vent_steam { get; set; }
        public decimal? hl_gvss_steam { get; set; }
        public decimal? hl_gvsm_steam { get; set; }
        public decimal? hl_cond_steam { get; set; }
        public decimal? hl_curtains_steam { get; set; }
        public decimal? hl_tech_steam { get; set; }
        public decimal? hl_other_steam { get; set; }
        public decimal? hl_gvss_steam_sum { get; set; }
        public decimal? hl_gvsm_steam_sum { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("DevProgrammsLoadsDeclared", Schema = "consumers")]
    public class DevProgrammsLoadsDeclared
    {
        public int consumer_id { get; set; }
        public int? dev_prog_id { get; set; }
        public decimal? hl_heat_hw { get; set; }
        public decimal? hl_vent_hw { get; set; }
        public decimal? hl_gvss_hw { get; set; }
        public decimal? hl_gvsm_hw { get; set; }
        public decimal? hl_tech_hw { get; set; }
        public decimal? hl_cond_hw { get; set; }
        public decimal? hl_curtains_hw { get; set; }
        public decimal? hl_other_hw { get; set; }
        public decimal? hl_gvss_hw_sum { get; set; }
        public decimal? hl_gvsm_hw_sum { get; set; }
        public decimal? hl_heat_steam { get; set; }
        public decimal? hl_vent_steam { get; set; }
        public decimal? hl_gvss_steam { get; set; }
        public decimal? hl_gvsm_steam { get; set; }
        public decimal? hl_cond_steam { get; set; }
        public decimal? hl_curtains_steam { get; set; }
        public decimal? hl_tech_steam { get; set; }
        public decimal? hl_other_steam { get; set; }
        public decimal? hl_gvss_steam_sum { get; set; }
        public decimal? hl_gvsm_steam_sum { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("DevProgrammsLoadsCalculated", Schema = "consumers")]
    public class DevProgrammsLoadsCalculated
    {
        public int? dev_prog_id { get; set; }
        public int consumer_id { get; set; }     
        public int? perspective_year { get; set; }

        [Precision(18, 3)]
        public decimal? increase_area { get; set; }
        public decimal? hl_heat_hw { get; set; }
        public decimal? hl_vent_hw { get; set; }
        public decimal? hl_gvss_hw { get; set; }
        public decimal? hl_tech_hw { get; set; }
        public decimal? hl_heat_steam { get; set; }
        public decimal? hl_vent_steam { get; set; }
        public decimal? hl_gvss_steam { get; set; }
        public decimal? hl_tech_steam { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }
}
