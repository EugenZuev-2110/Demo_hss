using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Areas.TSO.Models
{
    [Keyless]
    public class TZObjectsDataViewModel
    {
        public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		//public List<TZTerritoryViewModel> tz_territory { get; set; }
        public bool combine_prod_more25 { get; set; }
		public bool combine_prod_less25 { get; set; }
		public bool not_combine_prod { get; set; }
		public bool transfer { get; set; }
		public bool sale { get; set; }
		public bool delivery_contract { get; set; }
		public short? tariff_differentiation { get; set; }
		public int cnt_sources { get; set; }
		public decimal inst_heat_power_s { get; set; }
		public int cnt_heat_networks { get; set; }
		public decimal aver_diam_all_net { get; set; }
		public decimal sum_mat_char_all_net { get; set; }
		public decimal sum_volume_all_net { get; set; }
		public int cnt_pump_stations { get; set; }
		public decimal sum_pump_capacity_ps { get; set; }
		public int cnt_hp_all { get; set; }
		public decimal inst_heat_power_all_hp { get; set; }

	}

    [Keyless]
    public class TZTerritoryViewModel
    {
		public string? region_name { get; set; }
        public string? districts { get; set; }

    }

	[Keyless]
	public class TSOAdditionalDataViewModel
	{
		public int tso_id { get; set; }
		public int data_status { get; set; }
		public decimal? fixed_assets_cost_prod { get; set; }
		public decimal? fixed_assets_wear { get; set; }
		public decimal? fixed_assets_cost_transfer { get; set; }
		public decimal? network_length { get; set; }
		public decimal? network_length_replaced { get; set; }
		public decimal? useful_supply_large_consumer { get; set; }
		public int? count_service_public { get; set; }
		public int? count_abonent { get; set; }

	}

	[Keyless]
	public class TSOSummaryDataListViewModel
	{
		public int tz_id { get; set; }
		public string unom_tz { get; set; }
		public string? territory { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public string? activity_types { get; set; }
		public int cnt_sources { get; set; }
		public decimal inst_elec_power_s { get; set; }
		public decimal inst_heat_power_s { get; set; }
		public decimal sum_length_net_channel_magistr { get; set; }
		public decimal sum_length_net_channel_raspr { get; set; }
		public decimal sum_length_net_channel_all { get; set; }
		public decimal aver_diam_magistr_net { get; set; }
		public decimal aver_diam_raspr_net { get; set; }
		public decimal aver_diam_all_net { get; set; }
		public decimal sum_mat_char_magistr_net { get; set; }
		public decimal sum_mat_char_raspr_net { get; set; }
		public decimal sum_mat_char_all_net { get; set; }
		public decimal sum_volume_magistr_net { get; set; }
		public decimal sum_volume_raspr_net { get; set; }
		public decimal sum_volume_all_net { get; set; }
		public int cnt_pump_stations { get; set; }
		public decimal sum_pump_capacity_ps { get; set; }
		public int cnt_ihp { get; set; }
		public int cnt_chp { get; set; }
		public int cnt_hp_all { get; set; }
		public decimal inst_heat_power_ihp { get; set; }
		public decimal inst_heat_power_chp { get; set; }
		public decimal inst_heat_power_all_hp { get; set; }
		public decimal calc_hl_steam_hp { get; set; }
		public decimal calc_hl_hw_hp { get; set; }
	}

	[Keyless]
	public class TSOSourcesDataListViewModel
	{
		public int tz_id { get; set; }
		public string? unom_tz { get; set; }
		public string? territory { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public string? activity_types { get; set; }
		public int source_id { get; set; }
		public int? source_unom { get; set; }
		public string? unom_hss { get; set; }
		public string? unom_eto { get; set; }
		public string? source_name { get; set; }
		public string? source_type_name { get; set; }
		public string? region_short { get; set; }
		public string? district_name { get; set; }
		public string? address { get; set; }
		public decimal inst_elec_power { get; set; }
		public decimal inst_heat_power { get; set; }
		public decimal calc_hl_steam_hp { get; set; }
		public decimal calc_hl_hw_hp { get; set; }
		public string? territories { get; set; }
		public string? source_status_name { get; set; }
		public decimal release_elenergy_tires_powerplant { get; set; }
		public decimal release_heatenergy_consumers { get; set; }
		public decimal consump_fuel_release_elenergy { get; set; }
		public decimal consump_fuel_release_elenergy_tires { get; set; }
		public decimal consump_fuel_release_elenergy_collect { get; set; }
		public decimal consump_fuel_release_elenergy_tires_prop { get; set; }
		public decimal consump_fuel_release_elenergy_collect_prop { get; set; }
		public decimal spec_cons_fuel_release_elenergy { get; set; }
		public decimal spec_cons_fuel_release_heatenergy { get; set; }
		public decimal spec_cons_fuel_release_elenergy_prop { get; set; }
		public decimal spec_cons_fuel_release_heatenergy_prop { get; set; }
		public decimal consump_elenergy_own_needs { get; set; }
		public decimal consump_elenergy_prod_elenergy { get; set; }
		public decimal consump_elenergy_release_heatenergy { get; set; }
		public decimal spec_consump_elenergy_own_needs { get; set; }
		public decimal consump_water { get; set; }
		public decimal spec_consump_water { get; set; }
		public string? obj_vacant_prop { get; set; }
	}

	[Keyless]
	public class TZOneSourcesDataListViewModel
	{
		public int tz_id { get; set; }
		public int? source_unom { get; set; }
		public string? unom_hss { get; set; }
		public string? unom_eto { get; set; }
		public string? source_name { get; set; }
		public string? source_type_name { get; set; }
		public string? region_short { get; set; }
		public string? district_name { get; set; }
		public string? address { get; set; }
		public decimal? inst_elec_power { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? calc_hl_steam_hp { get; set; }
		public decimal? calc_hl_hw_hp { get; set; }
		public string? source_status_name { get; set; }
		public decimal? release_elenergy_tires_powerplant { get; set; }
		public decimal? release_heatenergy_consumers { get; set; }
		public string? territories { get; set; }
		public string? inn_org_owner { get; set; }
		public string? org_owner { get; set; }
		public string? obj_vacant_prop { get; set; }
	}

	[Keyless]
	public class TSOHPDataListViewModel
	{
		public int tz_id { get; set; }
		public string? unom_tz { get; set; }
		public string? territory { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public string? activity_types { get; set; }
		public int heat_point_id { get; set; }
		public string? unom_hss { get; set; }
		public string? unom_eto { get; set; }
		public string? hp_name { get; set; }
		public string? tso_hp_num { get; set; }
		public string? type_name { get; set; }
		public string? region_short { get; set; }
		public string? district_name { get; set; }
		public string? address { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? calc_hl_steam_hp { get; set; }
		public decimal? calc_hl_hw_hp { get; set; }
		public string? tech_connect_short { get; set; }
		public string? heat_connect_short { get; set; }
		public string? vent_connect_short { get; set; }
		public string? hw_connect_short { get; set; }
		public string? hp_status_name { get; set; }
		public string? inn_org_owner { get; set; }
		public string? org_owner { get; set; }
		public string? obj_vacant_prop { get; set; }
	}

	[Keyless]
	public class TZOneHPDataListViewModel
	{
		public int tz_id { get; set; }
		public int heat_point_id { get; set; }
		public string? unom_hss { get; set; }
		public string? unom_eto { get; set; }
		public string? hp_name { get; set; }
		public string? tso_hp_num { get; set; }
		public string? type_name { get; set; }
		public string? region_short { get; set; }
		public string? district_name { get; set; }
		public string? address { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? calc_hl_steam_hp { get; set; }
		public decimal? calc_hl_hw_hp { get; set; }
		public string? hp_status_name { get; set; }
		public string? inn_org_owner { get; set; }
		public string? org_owner { get; set; }
		public string? obj_vacant_prop { get; set; }
	}

	[Keyless]
	public class TZOneNetworksDataListViewModel
	{
		public int tz_id { get; set; }
		public int heat_network_id { get; set; }
		public string? unom_hss { get; set; }
		public string? unom_eto { get; set; }
		public string? start_camera_name { get; set; }
		public string? end_camera_name { get; set; }
		public string? net_type_short { get; set; }
		public string? water_net_type_short { get; set; }
		public string? region_short { get; set; }
		public string? district_name { get; set; }
		public string? start_camera_address { get; set; }
		public string? end_camera_address { get; set; }
		public int? s_cond_ht_net_diam { get; set; }
		public int? r_cond_ht_net_diam { get; set; }
		public decimal? length_net_channel { get; set; }
		public decimal? length_net_pipeline { get; set; }
		public string? net_laying_type_short { get; set; }
		public string? channel_laying_type_short { get; set; }
		public decimal? mat_char_net { get; set; }
		public decimal? volume_net { get; set; }
		public string? inn_org_owner { get; set; }
		public string? org_owner { get; set; }
		public string? net_status_name { get; set; }
		public string? obj_vacant_prop { get; set; }
	}

	[Keyless]
	public class TZOnePSDataListViewModel
	{
		public int tz_id { get; set; }
		public int pump_station_id { get; set; }
		public string? unom_hss { get; set; }
		public string? unom_eto { get; set; }
		public string? pump_station_name { get; set; }
		public string? pump_scheme_connect_name { get; set; }
		public string? region_short { get; set; }
		public string? district_name { get; set; }
		public string? address { get; set; }
		public decimal? sum_pump_capacity_ps { get; set; }
		public string? avail_telemech { get; set; }
		public string? inn_org_owner { get; set; }
		public string? org_owner { get; set; }
		public string? pump_station_status_name { get; set; }
		public string? obj_vacant_prop { get; set; }
	}

	[Keyless]
	public class TSOAdditionalDataListViewModel
	{
		public int tso_id { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public decimal fixed_assets_cost_prod { get; set; }
		public decimal fixed_assets_wear { get; set; }
		public decimal fixed_assets_cost_transfer { get; set; }
		public decimal network_length { get; set; }
		public decimal network_length_replaced { get; set; }
		public decimal useful_supply_large_consumer { get; set; }
		public int count_service_public { get; set; }
		public int count_abonent { get; set; }

	}

}