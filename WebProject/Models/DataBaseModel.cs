using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    // Класс содержащий общие столбцы для всех таблиц.
    //[Keyless]

    public class DictWinUsers
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public bool IsNotExecutor { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime? Last_visit_dt { get; set; }
    }
    public class Unoms
    {

        public int Id { get; set; }
        public string unom_num { get; set; }
        public int? category_id { get; set; }
        public int? ets_project_number { get; set; }
        public DateTime? ets_date { get; set; }
        public string? ets_appeal_number { get; set; }
        public int? org_id { get; set; }
        public DateTime? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? result_review { get; set; }
        public bool? changes_is_required { get; set; }
        public string? changes_type { get; set; }
        public bool? is_appr_scheme_exist { get; set; }
        //public int? tag_id { get; set; }
        public DateTime? out_appeal_date { get; set; }
        public string? out_appeal_number { get; set; }
        public int? state_id { get; set; }
        public int? executor_id { get; set; }
        public string? directory_link { get; set; }
        public DateTime? data_status { get; set; }
        public int? layer_id { get; set; }
        public int user_id { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }
    }

    public class DictOrganisation
    {

        public int Id { get; set; }
        public string org_name { get; set; }
        public string short_name { get; set; }
    }
    public class DictUnomStates
    {

        public int Id { get; set; }
        public string state { get; set; }
    }
    public class DictUnomCategories
    {

        public int Id { get; set; }
        public string category_name { get; set; }
    }
    public class DictUnomTags
    {

        public int Id { get; set; }
        public string tag_name { get; set; }
    }

    public class DictETSProjects
    {

        public int Id { get; set; }
        public string project_name { get; set; }
        public int year { get; set; }
    }
    public class DictDistricts
    {
        public int Id { get; set; }
        public string district_name { get; set; }
    }
    public class SourceListView
    {
        public Int64 Id { get; set; }
        public int s_num { get; set; }
        public string source_name { get; set; }
    }

    public class TSOListView
    {
        public Int64 id { get; set; }
        public int tso_code { get; set; }
        public string org_name { get; set; }
    }

    public class ExecutorsListView
    {
        public int Id { get; set; }
        public string executor_name { get; set; }
        public int sort { get; set; }
    }
    public class CategoriesListView
    {
        public int Id { get; set; }
        public string category_name { get; set; }
        public int sort { get; set; }
    }

    public class DataStatusesView
    {

        public DateTime DataStatus { get; set; }
        public string Ds { get; set; }
        public int hss_id { get; set; }
    }
    public class DictLayers
    {
        public int Id { get; set; }
        public string? layer_name { get; set; }
        public string? table_name { get; set; }
        public bool? is_perspective { get; set; }
        public int? layer_year { get; set; }
    }
    public class ObjectTypesListView
    {

        public int Id { get; set; }
        public string object_type_name { get; set; }
        public int category_id { get; set; }
    }
    public class Items
    {

        public int Id { get; set; }
        public int unom_id { get; set; }
        public byte item_number { get; set; }
        public string? conditional_address { get; set; }
        public int? object_type_id { get; set; }
        public string? object_id { get; set; }
        public int? source_id { get; set; }
        public int? district_id { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int user_id { get; set; }
    }

    public class Items_Logs
    {
        [Key]
        public int ItemId { get; set; }
        public int unom_id { get; set; }
        public byte item_number { get; set; }
        public string? conditional_address { get; set; }
        public int? object_type_id { get; set; }
        public string? object_id { get; set; }
        public int? source_id { get; set; }
        public int? district_id { get; set; }
        public DateTime create_date { get; set; }
        public int user_id { get; set; }
    }
    public class UnomTagsMapps
    {

        public int Id { get; set; }
        public int unom_id { get; set; }
        public int tag_id { get; set; }
        public byte state { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }
    }

    public class SectionsCategoryMapps
    {
        public int category_id { get; set; }
        public int section_id { get; set; }
    }
    public class DictObjectTypes
    {
        public int Id { get; set; }
        public string object_type_name { get; set; }
    }
    public class ConsumersHeatLoad
    {
        [Key]
        public int item_id { get; set; }
        public decimal? d_vent { get; set; }
        public decimal? d_sum_gvs_m { get; set; }
        public decimal? d_sum_gvs_a { get; set; }
        public decimal? d_gvs_m { get; set; }
        public decimal? d_gvs_a { get; set; }
        public decimal? d_cond { get; set; }
        public decimal? d_heat { get; set; }
        public decimal? d_other { get; set; }
        public decimal? d_heat_curtain { get; set; }
        public decimal? d_spec_heat_vent { get; set; }
        public decimal? d_spec_gvs_a { get; set; }
        public decimal? d_spec_all { get; set; }
        public decimal? c_vent { get; set; }
        public decimal? c_sum_gvs_a { get; set; }
        public decimal? c_gvs_a { get; set; }
        public decimal? c_heat { get; set; }
        public decimal? c_tech { get; set; }
        public decimal? c_spec_heat_vent { get; set; }
        public decimal? c_spec_gvs_a { get; set; }
        public decimal? c_spec_all { get; set; }
        public decimal? spec_dev_gvs_a { get; set; }
        public decimal? spec_dev_heat_vent { get; set; }
        public decimal? spec_dev_sum { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int user_id { get; set; }

    }
    public class ObjectDescription
    {
        [Key]
        public int item_id { get; set; }
        //public string? address { get; set; }
        public string? region_name { get; set; }
        public string? hp_name { get; set; }
        public string? project_num_ip { get; set; }
        public string? realiz_period_ip { get; set; }
        public string? unom_event { get; set; }
        public string? connect_point { get; set; }
        public string? sys_heat_network { get; set; }
        public string? building_purpose { get; set; }
        public string? building_permits_date { get; set; }
        public string? building_permits_num { get; set; }
        public decimal? fact_area { get; set; }
        public decimal? decl_area { get; set; }
        public string? exists_aip { get; set; }
        public int? fact_start { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int user_id { get; set; }

    }

    public class KSIO
    {
        [Key]
        public int item_id { get; set; }
        public string? objects_list_comments { get; set; }
        public string? others_comments { get; set; }
        public string? dev_events_comments { get; set; }
        public string? heat_loads_comments { get; set; }
        public string? exists_like_ksio { get; set; }
        public string? ksio_is_agree { get; set; }
        public string? exists_ksio { get; set; }
        public string? date_ppt { get; set; }
        public string? num_ppt { get; set; }
        public string? date_ksio { get; set; }
        public string? num_ksio { get; set; }
        public string? source_num_ksio { get; set; }
        public string? source_name_ksio { get; set; }
        public string? source_address_ksio { get; set; }
        public int? eto_num_ksio { get; set; }
        public string? eto_name_ksio { get; set; }
        public int? hssn_num_ksio { get; set; }
        //public string? q_inst_electr_ksio { get; set; }
        public string? q_inst_heat_ksio { get; set; }
        //public string? q_avail_heat_ksio { get; set; }
        public string? q_netto_heat_ksio { get; set; }
        //public string? q_calc_load_heat_ksio { get; set; }
        public decimal? decl_area_ksio { get; set; }
        public decimal? heat_sum { get; set; }
        public decimal? vent_sum { get; set; }
        public decimal? gvs_a_sum { get; set; }
        public decimal? heat_curtain_sum { get; set; }
        public decimal? cond_sum { get; set; }
        public decimal? other_sum { get; set; }
        public decimal? load_sum_gvs_a { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int user_id { get; set; }
    }

    public class Decommissioning
    {
        [Key]
        public int item_id { get; set; }
        public string? ch_scheme_is_no_need { get; set; }
        public string? decom_need_pause { get; set; }
        public string? source_finance_events { get; set; }
        public string? approved_list_events { get; set; }
        public string? alter_var_need { get; set; }
        public string? alter_var_events { get; set; }
        public string? start_realiz_event { get; set; }
        public string? consumers_heat_pause { get; set; }
        public decimal? c_heat { get; set; }
        public decimal? c_vent { get; set; }
        public decimal? c_gvs_a { get; set; }
        public decimal? c_tech { get; set; }
        public decimal? c_sum_gvs_a { get; set; }
        public string? decom_equip_list { get; set; }
        public string? st_num_equip { get; set; }
        public int? decom_date { get; set; }
        //public int? q_inst_electr_equip { get; set; }
        public decimal? decom_q_inst_heat_equip { get; set; }
        public decimal? after_decom_q_inst_heat_equip { get; set; }
        public decimal? after_decom_q_netto_heat { get; set; }
        public decimal? after_decom_q_reserve_heat { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int user_id { get; set; }
    }

    public class SourceDescription
    {
        [Key]
        public int item_id { get; set; }
        public int? source_num { get; set; }
        public string? source_name { get; set; }
        public string? source_address { get; set; }
        public int? eto_num { get; set; }
        public string? eto_name { get; set; }
        public int? hssn_num { get; set; }
        //public decimal? q_inst_electr { get; set; }
        public decimal? q_inst_heat { get; set; }
        //public decimal? q_avail_heat { get; set; }
        public decimal? q_netto_heat { get; set; }
        //public decimal? q_calc_load_heat { get; set; }
        public decimal? q_reserve_heat { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int user_id { get; set; }
    }

    public class HeatNetworksEvents
    {
        [Key]
        public int item_id { get; set; }
        public string? is_appr_scheme_exist { get; set; }
        //public string? accounted_events { get; set; }
        //public string? new_events { get; set; }
        //public string? unom_new_events { get; set; }
        //public string? tso_events { get; set; }
        public int? decl_start { get; set; }
        //public string? reasons_mismath_events { get; set; }
        public int? bandwidth_reserve { get; set; }
        //public decimal? q_reserve_heat { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int user_id { get; set; }
    }

    public class UnomHNE
    {
        [Key]
        public int unom_id { get; set; }
        public string? accounted_events { get; set; }
        public string? new_events { get; set; }
        public string? tso_events { get; set; }
        public string? reasons_mismath_events { get; set; }
        public DateTime create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int user_id { get; set; }
    }
}