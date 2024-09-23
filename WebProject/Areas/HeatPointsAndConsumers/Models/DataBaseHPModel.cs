using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Areas.HeatPointsAndConsumers.Models
{
	[Table("HP_Perspective", Schema = "heat_points")]
	public class HP_Perspective
	{
		public int? data_status { get; set; }
		public int? heat_point_id { get; set; }
		public int? perspective_year { get; set; }
		public int? heat_network_id { get; set; }
		public int? source_output_id { get; set; }
		public int? tso_id { get; set; }
		public short? hp_status_id { get; set; }
		public short? hp_type_scheme_id { get; set; }
		public short? heat_connect_id { get; set; }
		public short? vent_connect_id { get; set; }
		public short? hw_connect_id { get; set; }
		public short? tech_connect_id { get; set; }
		public short? temp_ht_plan_id { get; set; }
		public short? temp_heat_plan_id { get; set; }
		public short? temp_vent_plan_id { get; set; }
		public short? temp_gvs_plan_id { get; set; }
		public short? temp_tech_plan_id { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("Sources", Schema = "sources")]
	public class Sources
	{
		public int source_id { get; set; }
		public int? source_unom { get; set; }
		public string? address { get; set; }
		public int? district_id { get; set; }
		public string? kadnum_zu { get; set; }
		public string? kadnum_oks { get; set; }
		public int? fias_build { get; set; }
		public int? fias_zu { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("S_Outputs", Schema = "sources")]
	public class S_Outputs
	{
		
		public int? source_id { get; set; }
		public int source_output_id { get; set; }
		public string? unom_output { get; set; }
		public string? output_name { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("HP_History", Schema = "heat_points")]
	public class HP_History
	{
		public int? data_status { get; set; }
		public int? heat_point_id { get; set; }
		public int? tz_id { get;set; }
		public string? address { get; set; }
		public string? tso_hp_num { get; set; }
		public short? obj_ownership_id { get; set; }
		public bool? obj_vacant_prop { get; set; }
		public bool? obj_old_prop { get; set; }
		public string? muid { get; set; }
		public string? lotus_id { get; set; }
		public string? kasu_composite_id { get; set; }
		public string? kasu_id { get; set; }
		public string? kasu_map_id { get; set; }
		public int? last_year_reconstr { get; set; }
		public int? year_liquidate { get; set; }
		public int? org_owner_id { get; set; }
		public decimal? inst_heat_power { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("HeatPoints", Schema = "heat_points")]
	public class HeatPoint
	{
		public int heat_point_id { get; set; }
		public short? hp_type_id { get; set; }
		public string? hp_name { get; set; }
		public short? hp_type_loc_id { get; set; }
		public int? district_id { get; set; }
		public string? kad_num_zu { get; set; }
		public string? kad_num_oks { get; set; }
		public int? fias_build { get; set; }
		public int? fias_zu { get; set; }
		public int? start_year_expl { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("HP_TankBatteryAvailables", Schema = "heat_points")]
	public class HP_TankBatteryAvailables
	{
		public int data_status { get; set; }
		public int? heat_point_id { get; set; }
		public bool? avail_tank_battery { get; set; }
		public decimal? tank_capacity { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("HP_HeatExchangerMapps", Schema = "heat_points")]
	public class HP_HeatExchangerMapps
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }	
		public short ht_exch_num { get; set; }
		public short? type_equip_id { get; set; }
		public short? type_id { get; set; }
		public short? stage_scheme_gvs_id { get; set; }
		public int? equip_id { get; set; }
		public short? ht_exch_count { get; set; }
		public decimal? heat_exchange_surface { get; set; }
		public decimal? inst_heat_power { get; set; }
		public int? section_count { get; set; }
		public decimal? casing_diameter { get; set; }
		public decimal? length_section { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("HP_PumpMapps", Schema = "heat_points")]
	public class HP_PumpMapps
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }
		public short pump_num { get; set; }
		public short? type_pump_id { get; set; }
		public int? equip_id { get; set; }
		public short? pump_count { get; set; }
		public decimal? pump_capacity { get; set; }
		public decimal? pump_press { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("Dict_PumpTypes", Schema = "heat_points")]
	public class Dict_PumpTypes
	{
		public short Id { get; set; }
		public string? pump_type_name { get; set; }
	}

	[Table("Dict_HtExchangerEquipTypes", Schema = "heat_points")]
	public class Dict_HtExchangerEquipTypes
	{
		public short Id { get; set; }
		public string? htexch_equip_type_name { get; set; }
	}

	[Table("Dict_HeatExchangerTypes", Schema = "heat_points")]
	public class Dict_HeatExchangerTypes
	{
		public short Id { get; set; }
		public string? ht_type_name { get; set; }
		public string? ht_type_short { get; set; }
	}

	[Table("Dict_StageGVSSchemes", Schema = "heat_points")]
	public class Dict_StageGVSSchemes
	{
		public short Id { get; set; }
		public string? stage_scheme_gvs_name { get; set; }
	}

	[Table("Equipments", Schema = "dictionary")]
	public class Equipments
	{
		public int equip_id { get; set; }
		public string? unom_equip { get; set; }
		public short? equip_type_id { get; set; }
		public string? mark { get; set; }
		public string? manufacturer { get; set; }
		public decimal? inst_electric_power { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? capacity { get; set; }
		public decimal? kpd { get; set; }
		public decimal? ihp_heat_selections { get; set; }
		public decimal? ihp_prod_selections { get; set; }
		public decimal? fresh_steam_pressure { get; set; }
		public decimal? fresh_steam_temp { get; set; }
		public decimal? max_temp_out { get; set; }
		public decimal? net_water_consump_min { get; set; }
		public decimal? net_water_consump_nom { get; set; }
		public decimal? net_water_consump_max { get; set; }
		public decimal? pump_press { get; set; }
		public decimal? park_resources { get; set; }
		public decimal? norm_service_life { get; set; }
		public decimal? norm_count_start { get; set; }
		public decimal? volume { get; set; }
		public decimal? diameter { get; set; }
		public decimal? pipe_heigh_ground_lvl { get; set; }
		public decimal? heat_exchange_surface { get; set; }
		public int? section_count { get; set; }
		public decimal? casing_diameter { get; set; }
		public decimal? length_section { get; set; }
		public short? htexch_type_id { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}


	[Table("Dict_HPSchemes", Schema = "heat_points")]
	public class Dict_HPSchemes
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
		public int? user_id { get; set; }
	}

	[Table("Dict_ConnectTypesHeat", Schema = "heat_points")]
	public class Dict_ConnectTypesHeat
	{
		public short Id { get; set; }
		public string? heat_connect_name { get; set; }
		public string? heat_connect_short { get; set; }
	}

	[Table("Dict_ConnectTypesVent", Schema = "heat_points")]
	public class Dict_ConnectTypesVent
	{
		public short Id { get; set; }
		public string? vent_connect_name { get; set; }
		public string? vent_connect_short { get; set; }
	}

	[Table("Dict_ConnectTypesHW", Schema = "heat_points")]
	public class Dict_ConnectTypesHW
	{
		public short Id { get; set; }
		public string? hw_connect_name { get; set; }
		public string? hw_connect_short { get; set; }
	}

	[Table("HP_Automatization", Schema = "heat_points")]
	public class HP_Automatization
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }
		public bool? avail_flow_limiter { get; set; }
		public bool? avail_automatic { get; set; }
		public short? automatic_type_id { get; set; }
		public bool? avail_automatic_heat_control { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("Dict_AccResourcesTypes", Schema = "heat_points")]
	public class Dict_AccResourcesTypes
	{
		public short Id { get; set; }
		public string? acc_resource_type { get; set; }
	}

	[Table("HP_AccResources", Schema = "heat_points")]
	public class HP_AccResources
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }
		public short? acc_resource_type_id { get; set; }
		public int device_num { get; set; }
		public string? meter_device_mark { get; set; }
		public int count_devices { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("DocumentTypes", Schema = "dictionary")]
	public class DocumentTypes
	{
		public short Id { get; set; }
		public string? doc_type { get; set; }
	}

	[Table("HP_DocsPhotos_History", Schema = "heat_points")]
	public class HP_DocsPhotos_History
	{
		public int data_status { get; set; }
		public int heat_point_id { get; set; }
		public short? doc_type_id { get; set; }
		public int doc_num { get; set; }
		public string? doc_description { get; set; }
		public string? doc_url { get; set; }
		public string? doc_name { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("Dict_LoadTypes", Schema = "consumers")]
	public class Dict_LoadTypes
	{
		public short Id { get; set; }
		public string? load_type_name { get; set; }
	}
}
