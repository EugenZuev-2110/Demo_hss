using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Areas.Events.Models
{
    [Keyless]
    public class EventsViewModel
    {
        public int Id { get; set; }
        public string? unom { get; set; }
        public string? ip_num { get; set; }
        public long? equip_id { get; set; }
        public string? is_city_own { get; set; }
        public int? sys { get; set; }
        public int? source_num { get; set; }
        public int? eto_num { get; set; }
        public int? hssn_num { get; set; }
        public int? tso_code { get; set; }
        public string? obj_name { get; set; }
        public string? purpose_name { get; set; }
        public string? type_name { get; set; }
        public string? sfinance_name { get; set; }
        public short? start_realize_year { get; set; }
        public short? end_realize_year { get; set; }
        public string? address_start { get; set; }
        public string? address_end { get; set; }
        public double? length_before { get; set; }
        public double? length_after { get; set; }
        public double? d_p_before { get; set; }
        public double? d_p_after { get; set; }
        public string? event_name { get; set; }
        public double? cost_before { get; set; }
        public double? mat_char { get; set; }
        public string? reg_name { get; set; }
        public int? year_of_laying { get; set; }
        public double? cap_invest { get; set; }
    }

    [Keyless]
    public class EventViewModel
    {
        public Int16? object_type { get; set; }
        public string action_for { get; set; }
        public int Id { get; set; }
        public Int16? year { get; set; }
        public string? unom { get; set; }
        public string? ip_num { get; set; }
        [Required(ErrorMessage = "Не указан Id оборудования")]
        public long? equip_id { get; set; }
        public bool? is_city_own { get; set; }
        public int? sys { get; set; }
        public int? source_num { get; set; }
        public int? tso_code { get; set; }
        public Int16? obj_code { get; set; }
        public Int16? purpose_code { get; set; }
        public Int16? type_code { get; set; }
        public Int16? sfinance_code { get; set; }
        public Int16? start_realize_year { get; set; }
        public Int16? end_realize_year { get; set; }
        public string? address_start { get; set; }
        public string? address_end { get; set; }
        public double? length_before { get; set; }
        public double? length_after { get; set; }
        public double? d_p_before { get; set; }
        public double? d_p_after { get; set; }
        public int? reg_id { get; set; }
        public string? event_name { get; set; }
        public double? cost_before { get; set; }
        public int? year_of_laying { get; set; }
        public double? cap_invest { get; set; }

    }
}