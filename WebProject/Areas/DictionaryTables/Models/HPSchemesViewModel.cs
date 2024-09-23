using Microsoft.EntityFrameworkCore;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class HPSchemesListViewModel
	{
		public int scheme_id { get; set; }
		public string? heat_connect_name { get; set; }
		public string? vent_connect_name { get; set; }
		public string? hw_connect_name { get; set; }
		public string? avail_reg_temp_heat_vent { get; set; }
		public string? avail_reg_temp_hw { get; set; }
		public string? avail_threeway_reg_temp_hw { get; set; }
		public string? avail_mix_block_hw { get; set; }
		public string? avail_makeup_pump_heat_vent { get; set; }
		public string? avail_circul_pump_reverse_heat_vent { get; set; }
		public string? avail_circul_pump_feed_heat_vent { get; set; }
		public string? avail_circul_pump_hw { get; set; }
		public string? avail_elevator_heat_vent { get; set; }
		public string? avail_mix_pump_heat_vent { get; set; }
		public string? avail_jumper_reverse_feed_heat_vent { get; set; }
		public string? avail_washer_feed_heat_vent { get; set; }
		public string? avail_washer_reverse_heat_vent { get; set; }
		public string? avail_washer_feed_hw { get; set; }
		public string? scheme_file_url { get; set; }
	}

	[Keyless]
	public class HPSchemesViewModel
	{
		public int scheme_id { get; set; }
		public short? heat_connect_id { get; set; }
		public short? vent_connect_id { get; set; }
		public short? hw_connect_id { get; set; }
		public bool avail_reg_temp_heat_vent { get; set; }
		public bool avail_reg_temp_hw { get; set; }
		public bool avail_threeway_reg_temp_hw { get; set; }
		public bool avail_mix_block_hw { get; set; }
		public bool avail_makeup_pump_heat_vent { get; set; }
		public bool avail_circul_pump_reverse_heat_vent { get; set; }
		public bool avail_circul_pump_feed_heat_vent { get; set; }
		public bool avail_circul_pump_hw { get; set; }
		public bool avail_elevator_heat_vent { get; set; }
		public bool avail_mix_pump_heat_vent { get; set; }
		public bool avail_jumper_reverse_feed_heat_vent { get; set; }
		public bool avail_washer_feed_heat_vent { get; set; }
		public bool avail_washer_reverse_heat_vent { get; set; }
		public bool avail_washer_feed_hw { get; set; }
		public string? scheme_file_url { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
	}
}
