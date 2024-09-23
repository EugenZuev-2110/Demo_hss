using Microsoft.EntityFrameworkCore;

namespace WebProject.Areas.Sources.Models
{
    [Keyless]
    public class SourcesEquipTurbineViewModel
    {
        public int s_equip_id { get; set; }
        public string equip_unom { get; set; }
        public string? turbine_num { get; set; }
        public string? equip_status_name { get; set; }
        public string? turbine_type_name { get; set; }
        public string? mark { get; set; }
        public short? queue_num { get; set;}
        public short? energy_input_num { get; set;}
        public string? manufacturer { get; set;}
        public int? start_year_turbine { get;set;}
        public int source_id { get; set;}
        public int source_unom { get; set;}
        public string? source_name { get; set;}
        public string? tso_name { get; set;}
        public string? tz_num { get; set;}
        public string? source_type_name { get;set;}
        public string? source_status_name { get; set; }
    }
}
