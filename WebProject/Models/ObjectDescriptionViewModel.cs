using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;

namespace WebProject.Models
{
    [Keyless]
    public class ObjectDescriptionViewModel
    {
        public int itemId { get; set; }
        public string unom_num { get; set; }
        public string directory_link { get; set; }
        public int? unom_category_id { get; set; }
        public int? object_type_id { get; set; }
        public string object_id { get; set; }
        public string object_address { get; set; }
        public int[] sections_id { get; set; }
        public bool ItsExecutorOrAdmin { get; set; }
        public ObjectDescription obj_desc { get; set; }
        public SourceViewModel source { get; set; }
        public LoadsViewModel loads { get; set; }
        public KSIO ksio { get; set; }
        public Decommissioning decommissioning { get; set; }
        public HeatNetworksEvents hn_events { get; set; }

    }

    [Keyless]
    public class SourceViewModel
    {
        public Int64 source_id { get; set; }
        public int source_num { get; set; }
        public int? eto_num { get; set; }
        public string? eto_name { get; set; }
        public int? hssn_num { get; set; }
        public string? source_name { get; set; }
        public string? source_address { get; set; }
        //public decimal? q_inst_electr { get; set; }
        public decimal? q_inst_heat { get; set; }
        //public decimal? q_avail_heat { get; set; }
        public decimal? q_netto_heat { get; set; }
        //public decimal? q_calc_load_heat { get; set; }
        public decimal? q_reserve_heat { get; set; }

    }

    [Keyless]
    public class LoadsViewModel
    {
        //public string address { get; set; }
        //public string district { get; set; }
        public string? region_name { get; set; }
        public string? hp_name { get; set; }
        public string? project_num_ip { get; set; }
        public string? unom_event { get; set; }
        public string? realiz_period_ip { get; set; }
        public string? sys_heat_network { get; set; }
        public string? building_purpose { get; set; }
        public decimal? fact_area { get; set; }
        public decimal? decl_area { get; set; }
        public string? exists_aip { get; set; }
        public string? is_appr_scheme_exist { get; set; }
        //public string exists_like_ksio { get; set; }
        //public string exists_ksio { get; set; }
        //public DateTime? date_ksio { get; set; }
        //public string num_ksio { get; set; }
        //public DateTime? date_ppt { get; set; }
        //public string num_ppt { get; set; }
        public int? fact_start { get; set; }
        public int? decl_start { get; set; }
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

        //static LoadsViewModel()
        //{
        //    d_vent = 0;
        //}
    }

    [Keyless]
    public class ObjectDescriptionFullViewModel
    {
        public int itemId { get; set; }
        public int? unom_category_id { get; set; }
        public int? object_type_id { get; set; }
        public int[] sections_id { get; set; }
        public string? address { get; set; }
        public string? region_name { get; set; }
        public string? hp_name { get; set; }
        public string? project_num_ip { get; set; }
        public string? realiz_period_ip { get; set; }
        public string? unom_event { get; set; }
        public int? eto_num { get; set; }
        public string? eto_name { get; set; }
        public int? hssn_num { get; set; }
        public string? connect_point { get; set; }
        public string? sys_heat_network { get; set; }
        public string? building_purpose { get; set; }
        public string? building_permits_date { get; set; }
        public string? building_permits_num { get; set; }
        public decimal? fact_area { get; set; }
        public decimal? decl_area { get; set; }
        public string? exists_aip { get; set; }
        public int? fact_start { get; set; }
        //public ObjectDescription obj_desc { get; set; }
        public Int64 source_id { get; set; }
        public int source_num { get; set; }
        public string source_name { get; set; }
        public string source_address { get; set; }
        //public decimal? q_inst_electr { get; set; }
        public decimal? q_inst_heat { get; set; }
        //public decimal? q_avail_heat { get; set; }
        public decimal? q_netto_heat { get; set; }
        //public decimal? q_calc_load_heat { get; set; }
        public decimal? q_reserve_heat { get; set; }
        //public SourceViewModel source { get; set; }
        public string is_appr_scheme_exist { get; set; }
        public int? decl_start { get; set; }
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
        //public LoadsViewModel loads { get; set; }
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
        //public KSIO ksio { get; set; }
        public string? ch_scheme_is_no_need { get; set; }
        public string? decom_need_pause { get; set; }
        public string? source_finance_events { get; set; }
        public string? approved_list_events { get; set; }
        public string? alter_var_need { get; set; }
        public string? alter_var_events { get; set; }
        public string? start_realiz_event { get; set; }
        public string? consumers_heat_pause { get; set; }
        public string? decom_equip_list { get; set; }
        public string? st_num_equip { get; set; }
        public int? decom_date { get; set; }
        //public int? q_inst_electr_equip { get; set; }
        public decimal? decom_q_inst_heat_equip { get; set; }
        public decimal? after_decom_q_inst_heat_equip { get; set; }
        public decimal? after_decom_q_netto_heat { get; set; }
        public decimal? after_decom_q_reserve_heat { get; set; }
        //public int? q_inst_electr_equip { get; set; }
        //public Decommissioning decommissioning { get; set; }
        public string? accounted_events { get; set; }
        public string? new_events { get; set; }
        public string? tso_events { get; set; }
        public string? reasons_mismath_events { get; set; }
        public int? bandwidth_reserve { get; set; }
        //public HeatNetworksEvents hn_events { get; set; }

    }

}