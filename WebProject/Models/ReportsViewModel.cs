using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace WebProject.Models
{
    [Keyless]
    public class UnomItemsCheckListViewModel
    {
        [DisplayName("УНОМ")]
        public string? unom_num { get; set; }
        [DisplayName("Тип обращения")]
        public string? category_name { get; set; }
        [DisplayName("Номер проекта ЭТС-проект")]
        public string? ets_project_number { get; set; }
        [DisplayName("Входящая дата ЭТС-Проект")]
        public string? ets_date { get; set; }
        [DisplayName("Входящий номер ЭТС-Проект")]
        public string? ets_appeal_number { get; set; }
        [DisplayName("Наименование организации")]
        public string? organisation_name { get; set; }
        [DisplayName("Исходящая дата ДЖКХ")]
        public string? dzhkh_date { get; set; }
        [DisplayName("Исходящий номер ДЖКХ")]
        public string? dzhkh_number { get; set; }
        [DisplayName("Краткое содержание обращения")]
        public string? appeal_desc_short { get; set; }
        [DisplayName("Результат рассмотрения")]
        public string? result_review { get; set; }
        [DisplayName("Необходимость внесения изменений в схему теплоснабжения")]
        public string changes_is_required { get; set; }
        [DisplayName("Перечень изменений, вносимых в разрабатываемую схему теплоснабжения")]
        public string? changes_type { get; set; }
        [DisplayName("Учтено в утверждённой схеме")]
        public string is_appr_scheme_exist { get; set; }
        [DisplayName("Дата исходящего обращения")]
        public string? out_appeal_date { get; set; }
        [DisplayName("Номер исходящего обращения")]
        public string? out_appeal_number { get; set; }
        [DisplayName("Тэги")]
        public string? tags { get; set; }
        [DisplayName("Статус")]
        public string? state { get; set; }
        [DisplayName("Исполнитель")]
        public string? executor_name { get; set; }
        [DisplayName("Ссылка на папку")]
        public string? directory_link { get; set; }
        [DisplayName("Название слоя")]
        public string? layer_name { get; set; }
        [DisplayName("Номер пункта")]
        public string? item_number { get; set; }
        [DisplayName("Условный адрес")]
        public string? conditional_address { get; set; }
        [DisplayName("Тип объекта")]
        public string? object_type_name { get; set; }
        [DisplayName("Id объекта")]
        public string? object_id { get; set; }
        [DisplayName("Наименование источника")]
        public string? source_name { get; set; }
        [DisplayName("Округ")]
        public string? district_name { get; set; }
        [DisplayName("Источник данных")]
        public string? data_status { get; set; }
        [DisplayName("3ий уровень")]
        public string? is_complete { get; set; }
    }

    [Keyless]
    public class ReportOther_t15_ViewModel
    {
        public long RowNumber { get; set; }
        public string? organisation_name { get; set; }
        public string? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? district_name { get; set; }
        public string? source_name { get; set; }
        public string? justification { get; set; }
        public string? res_calc { get; set; }
        public string? result_review { get; set; }
        public string? changes_is_required { get; set; }
        public string? unom_num { get; set; }
    }

    [Keyless]
    public class ReportTechConViewModel
    {
        public long RowNumber { get; set; }
        public string? organisation_name { get; set; }
        public string? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? result_review { get; set; }
        public string? source_name { get; set; }
        public string? district_name { get; set; }
        public string? is_appr_scheme_exist { get; set; }
        public string? changes_is_required { get; set; }
        public string? changes_type { get; set; }
        public string? unom_num { get; set; }
    }

    [Keyless]
    public class ReportDecommissioningViewModel
    {
        public long RowNumber { get; set; }
        public string? organisation_name { get; set; }
        public string? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? result_review { get; set; }
        public string? source_name { get; set; }
        public string? district_name { get; set; }
        public string? is_appr_scheme_exist { get; set; }
        public string? changes_is_required { get; set; }
        public string? unom_num { get; set; }
    }

    [Keyless]
    public class ReportOthers_t9_ViewModel
    {
        public long RowNumber { get; set; }
        public string? organisation_name { get; set; }
        public string? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? result_review { get; set; }
        public string? source_name { get; set; }
        public string? district_name { get; set; }
        public string? is_appr_scheme_exist { get; set; }
        public string? changes_is_required { get; set; }
        public string? unom_num { get; set; }
    }

    [Keyless]
    public class ReportAll_t9_ViewModel
    {
        public long RowNumber { get; set; }
        public string? organisation_name { get; set; }
        public string? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? result_review { get; set; }
        public string? tso_events { get; set; }
        public string? changes_type { get; set; }
        public string? conditional_address { get; set; }
        public string? district_name { get; set; }
        public string? building_purpose { get; set; }
        public string? fact_area { get; set; }
        public string? d_heat { get; set; }
        public string? d_vent { get; set; }
        public string? d_gvs_a { get; set; }
        public string? d_heat_curtain { get; set; }
        public string? d_cond { get; set; }
        public string? d_other { get; set; }
        public string? d_load_sum { get; set; }
        public string? UP_date { get; set; }
        public string? UP_num { get; set; }
        public string? fact_start { get; set; }
        public string? exists_aip { get; set; }
        public string? building_permits_num { get; set; }
        public string? project_num_ip { get; set; }
        public string? realiz_period_ip { get; set; }
        public string? s_num { get; set; }
        public string? source_address { get; set; }
        public string? source_name { get; set; }
        public string? q_inst_heat { get; set; }
        public string? q_netto_heat { get; set; }
        public string? eto_num { get; set; }
        public string? eto_name { get; set; }
        public string? hssn_num { get; set; }
        public string? unom_num { get; set; }

    }
    [Keyless]
    public class ReportKSIO_t15_ViewModel
    {
        public long RowNumber { get; set; }
        public string? ksio_name { get; set; }
        public string? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? source_name { get; set; }
        public string? district_name { get; set; }
        public string? decl_area { get; set; }
        public string? heat_sum { get; set; }
        public string? vent_sum { get; set; }
        public string? gvs_a_sum { get; set; }
        public string? heat_curtain_sum { get; set; }
        public string? cond_sum { get; set; }
        public string? other_sum { get; set; }
        public string? load_sum_gvs_a { get; set; }
        public string? objects_list_comments { get; set; }
        public string? heat_loads_comments { get; set; }
        public string? dev_events_comments { get; set; }
        public string? others_comments { get; set; }
        public string? out_appeal_date { get; set; }
        public string? out_appeal_number { get; set; }
        public string? result_review { get; set; }
        public string? date_ppt { get; set; }
        public string? num_ppt { get; set; }
        public string? ksio_is_agree { get; set; }
        public string? exists_ksio { get; set; }
        public string? date_ksio { get; set; }
        public string? num_ksio { get; set; }
        public string? unom_num { get; set; }

    }

    [Keyless]
    public class ReportTechCon_t15_ViewModel
    {
        public long RowNumber { get; set; }
        public string? organisation_name { get; set; }
        public string? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? conditional_address { get; set; }
        public string? fact_area { get; set; }
        public string? d_heat { get; set; }
        public string? d_vent { get; set; }
        public string? d_gvs_a { get; set; }
        public string? d_gvs_m { get; set; }
        public string? d_heat_curtain { get; set; }
        public string? d_cond { get; set; }
        public string? d_other { get; set; }
        public string? d_sum_gvs_m { get; set; }
        public string? d_sum_gvs_a { get; set; }
        public string? c_heat { get; set; }
        public string? c_vent { get; set; }
        public string? c_gvs_a { get; set; }
        public string? c_tech { get; set; }
        public string? c_sum_gvs_a { get; set; }
        public string? d_spec_heat_vent { get; set; }
        public string? d_spec_gvs_a { get; set; }
        public string? d_spec_all { get; set; }
        public string? c_spec_heat_vent { get; set; }
        public string? c_spec_gvs_a { get; set; }
        public string? c_spec_all { get; set; }
        public string? spec_dev_heat_vent { get; set; }
        public string? spec_dev_gvs_a { get; set; }
        public string? spec_dev_sum { get; set; }
        public string? q_reserve_heat { get; set; }
        public string? bandwidth_reserve { get; set; }
        public string? hp_name { get; set; }
        public string? source_name { get; set; }
        public string? sys_heat_network { get; set; }
        public string? connect_point { get; set; }
        public string? tso_events { get; set; }
        public string? project_num_ip { get; set; }
        public string? realiz_period_ip { get; set; }
        public string? accounted_events { get; set; }
        public string? accordance_events { get; set; }
        public string? reasons_mismath_events { get; set; }
        public string? result_review { get; set; }
        public string? changes_is_required { get; set; }
        public string? unom_num { get; set; }

    }

    [Keyless]
    public class ReportAll_t15_ViewModel
    {
        public long RowNumber { get; set; }
        public string? organisation_name { get; set; }
        public string? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? eto_num { get; set; }
        public string? eto_name { get; set; }
        public string? hssn_num { get; set; }
        public string? s_num { get; set; }
        public string? source_address { get; set; }
        public string? source_name { get; set; }
        public string? q_inst_heat { get; set; }
        public string? q_netto_heat { get; set; }
        public string? conditional_address { get; set; }
        public string? district_name { get; set; }
        public string? building_purpose { get; set; }
        public string? fact_area { get; set; }
        public string? UP_date { get; set; }
        public string? UP_num { get; set; }
        public string? fact_start { get; set; }
        public string? d_heat { get; set; }
        public string? d_vent { get; set; }
        public string? d_gvs_a { get; set; }
        public string? d_gvs_m { get; set; }
        public string? d_heat_curtain { get; set; }
        public string? d_cond { get; set; }
        public string? d_other { get; set; }
        public string? d_sum_gvs_m { get; set; }
        public string? d_sum_gvs_a { get; set; }
        public string? exists_aip { get; set; }
        public string? building_permits_date { get; set; }
        public string? building_permits_num { get; set; }
        public string? connect_point { get; set; }
        public string? tso_events { get; set; }
        public string? unom_event { get; set; }
        public string? project_num_ip { get; set; }
        public string? realiz_period_ip { get; set; }
        public string? result_review { get; set; }
        public string? changes_type { get; set; }
        public string? unom_num { get; set; }
    }

    [Keyless]
    public class ReportDecommissioning_t15_ViewModel
    {
        public long RowNumber { get; set; }
        public string? organisation_name { get; set; }
        public string? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? source_name { get; set; }
        public string? source_address { get; set; }
        public string? eto_num { get; set; }
        public string? eto_name { get; set; }
        public string? hssn_num { get; set; }
        public string? q_inst_heat { get; set; }
        public string? q_netto_heat { get; set; }
        public string? q_reserve_heat { get; set; }
        public string? decom_equip_list { get; set; }
        public string? decom_date { get; set; }
        public string? decom_q_inst_heat_equip { get; set; }
        public string? after_decom_q_inst_heat_equip { get; set; }
        public string? after_decom_q_netto_heat { get; set; }
        public string? after_decom_q_reserve_heat { get; set; }
        public string? ch_scheme_is_no_need { get; set; }
        public string? decom_need_pause { get; set; }
        public string? source_finance_events { get; set; }
        public string? consumers_heat_pause { get; set; }
        public string? c_heat { get; set; }
        public string? c_vent { get; set; }
        public string? c_gvs_a { get; set; }
        public string? c_tech { get; set; }
        public string? c_sum_gvs_a { get; set; }
        public string? approved_list_events { get; set; }
        public string? alter_var_events { get; set; }
        public string? start_realiz_event { get; set; }
        public string? result_review { get; set; }
        public string? unom_num { get; set; }
    }
}