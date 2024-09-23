using Microsoft.EntityFrameworkCore;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class EquipmentsViewmodel
	{
		public int equip_id { get; set; }
		public string? unom_equip { get; set; }
		public short? equip_type_id { get; set; }
		public string? mark { get; set; }
	}

	[Keyless]
	public class SteamTurbineViewmodel : EquipmentsViewmodel
	{
		public string? manufacturer { get; set; }
		public decimal? inst_electric_power { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? ihp_heat_selections { get; set; }
		public decimal? ihp_prod_selections { get; set; }
		public decimal? fresh_steam_pressure { get; set; }
		public decimal? park_resources { get; set; }
		public decimal? fresh_steam_temp { get; set; }
	}

	[Keyless]
	public class GasTurbineViewmodel : EquipmentsViewmodel
	{
		public string? manufacturer { get; set; }
		public decimal? inst_electric_power { get; set; }
		public decimal? park_resources { get; set; }
		public decimal? norm_count_start { get; set; }
	}

	[Keyless]
	public class SteamBoilersViewmodel : EquipmentsViewmodel
	{
		public string? manufacturer { get; set; }
		public decimal? capacity { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? norm_service_life { get; set; }
		public decimal? kpd { get; set; }
		public decimal? fresh_steam_pressure { get; set; }
		public decimal? fresh_steam_temp { get; set; }
	}

	[Keyless]
	public class HWBoilersViewmodel : EquipmentsViewmodel
	{
		public string? manufacturer { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? max_temp_out { get; set; }
		public decimal? norm_service_life { get; set; }
		public decimal? net_water_consump_min { get; set; }
		public decimal? net_water_consump_nom { get; set; }
		public decimal? net_water_consump_max { get; set; }
		public decimal? kpd { get; set; }
	}

	[Keyless]
	public class PistonInstallationsViewmodel : EquipmentsViewmodel
	{
		public string? manufacturer { get; set; }
		public decimal? inst_electric_power { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? park_resources { get; set; }
	}

	[Keyless]
	public class ROUViewmodel : EquipmentsViewmodel
	{
		public decimal? capacity { get; set; }
		public decimal? fresh_steam_pressure { get; set; }
		public decimal? fresh_steam_temp { get; set; }
	}

	[Keyless]
	public class WaterHeaterViewmodel : EquipmentsViewmodel
	{
		public string? manufacturer { get; set; }
		public string? htexch_type { get; set; }
		public decimal? heat_exchange_surface { get; set; }
		public int? section_count { get; set; }
		public decimal? casing_diameter { get; set; }
		public decimal? length_section { get; set; }
		public decimal? max_temp_out { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? net_water_consump_nom { get; set; }
		public decimal? net_water_consump_max { get; set; }
	}

	[Keyless]
	public class WaterHeaterOneDataViewmodel : EquipmentsViewmodel
	{
		public string? manufacturer { get; set; }
		public short? htexch_type_id { get; set; }
		public decimal? heat_exchange_surface { get; set; }
		public int? section_count { get; set; }
		public decimal? casing_diameter { get; set; }
		public decimal? length_section { get; set; }
		public decimal? max_temp_out { get; set; }
		public decimal? inst_heat_power { get; set; }
		public decimal? net_water_consump_nom { get; set; }
		public decimal? net_water_consump_max { get; set; }
	}
	[Keyless]
	public class PumpsViewmodel : EquipmentsViewmodel
	{
		public string? manufacturer { get; set; }
		public decimal? capacity { get; set; }
		public decimal? pump_press { get; set; }
	}

	[Keyless]
	public class FiltersVPUViewmodel : EquipmentsViewmodel
	{
		public decimal? capacity { get; set; }
		public decimal? diameter { get; set; }
	}

	[Keyless]
	public class ClarifierVPUViewmodel : EquipmentsViewmodel
	{
		public decimal? capacity { get; set; }
		public decimal? volume { get; set; }
		public decimal? diameter { get; set; }
	}

	[Keyless]
	public class ReverseOsmosisInstalVPUViewmodel : EquipmentsViewmodel
	{
		public decimal? capacity { get; set; }
	}

	[Keyless]
	public class TanksVPUViewmodel : EquipmentsViewmodel
	{
		public decimal? volume { get; set; }
	}

	[Keyless]
	public class SmokePipesViewmodel : EquipmentsViewmodel
	{
		public decimal? pipe_heigh_ground_lvl { get; set; }
		public decimal? diameter { get; set; }
	}
}
