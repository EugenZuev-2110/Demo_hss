using Microsoft.EntityFrameworkCore;

namespace WebProject.Models
{
    //[NotMapped]
    [Keyless]
    public class UnomsViewModel
    {
        public int Id { get; set; }
        public string? unom_num { get; set; }
        public string? category_name { get; set; }
        public int? ets_project_number { get; set; }
        public DateTime? ets_date { get; set; }
        public string? ets_appeal_number { get; set; }
        public string? organisation_name { get; set; }
        public DateTime? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? result_review { get; set; }
        public string changes_is_required { get; set; }
        public string? changes_type { get; set; }
        public string? tags { get; set; }
        public DateTime? out_appeal_date { get; set; }
        public string? out_appeal_number { get; set; }
        public string? state { get; set; }
        public string? executor_name { get; set; }
        public string? directory_link { get; set; }
        public DateTime? data_status { get; set; }
    }
    [Keyless]
    public class UnomInfoViewModel
    {
        public int Id { get; set; }
        public string? unom_num { get; set; }
        public string? category_name { get; set; }
        public DateTime? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public bool? changes_is_required { get; set; }
        public bool? is_appr_scheme_exist { get; set; }
        public string? changes_type { get; set; }
        public string? tags { get; set; }
        public DateTime? out_appeal_date { get; set; }
        public string? out_appeal_number { get; set; }
        public string? state { get; set; }
        public string? executor_name { get; set; }
        public int? executor_id { get; set; }
        public bool? ItsExecutorOrAdmin { get; set; }
        public string? directory_link { get; set; }
        public DateTime? data_status { get; set; }
        public int? layer_id { get; set; }
        public string? accounted_events { get; set; }
        public string? new_events { get; set; }
        public string? tso_events { get; set; }
        public string? reasons_mismath_events { get; set; }
        public bool? accordance_events { get; set; }
    }

    [Keyless]
    public class UnomViewModel
    {
        public int Id { get; set; }
        public string unom_num { get; set; }
        public int? category_id { get; set; }
        public int? ets_project_number { get; set; }
        public DateTime? ets_date_dt { get; set; }
        public string? ets_date { get; set; }
        public string? ets_appeal_number { get; set; }
        public int? org_id { get; set; }
        public DateTime? dzhkh_date_dt { get; set; }
        public string? dzhkh_date { get; set; }
        public string? dzhkh_number { get; set; }
        public string? appeal_desc_short { get; set; }
        public string? result_review { get; set; }
        public bool? changes_is_required { get; set; }
        public string? changes_type { get; set; }
        public int[] tags { get; set; }
        public List<DataBase.Models.DictUnomTags> tags_list { get; set; }
        public string? out_appeal_date { get; set; }
        public DateTime? out_appeal_date_dt { get; set; }
        public string? out_appeal_number { get; set; }
        public int? state_id { get; set; }
        public int? executor_id { get; set; }
        public string? directory_link { get; set; }
        public DateTime? data_status { get; set; }
    }
}