using Microsoft.EntityFrameworkCore;
using WebProject.Areas.TSO.Models;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class TerritorialDivisionViewModel
	{
		public int Id { get; set; }
		public string? unom_p { get; set; }
		public int? layer_id { get; set; }
		public int? layer_sys { get; set; }
		public string? region_short { get; set; }
		public string? region_name { get; set; }
		public string? district_name { get; set; }
		public string? coldest_month { get; set; }
		public decimal? calc_air_temp_ht_vent { get; set; }
		public decimal? aver_temp_coldest_month { get; set; }
		public decimal? aver_temp_heat_period { get; set; }
		public int? length_heat_period { get; set; }
		public decimal? aver_temp_tap_water_heat_period { get; set; }
		public decimal? aver_temp_tap_water_notheat_period { get; set; }
		public decimal? calc_air_temp_after_calc_coldest { get; set; }
		public decimal? fact_aver_temp_heat_period { get; set; }
		public int? fact_length_heat_period { get; set; }
		public int? length_year_prevent_off_system { get; set; }
		//public List<TerritorialDivisionPopulationListViewModel> TerritorialDivisionPopulationList { get; set; }
	}

	[Keyless]
	public class TerritorialDivisionMainViewModel
	{
		public List<TerritorialDivisionViewModel> TerritorialDivision { get; set; }
		public List<TerritorialDivisionPopulationListViewModel> TerritorialDivisionPopulationList { get; set; }
	}

	[Keyless]
	public class TerritorialDivisionClimaticDataOneViewModel
	{
		public int Id { get; set; }
		public int data_status { get; set; }
		public decimal? calc_air_temp_ht_vent { get; set; }
		public decimal? aver_temp_coldest_month { get; set; }
		public decimal? aver_temp_heat_period { get; set; }
		public int? length_heat_period { get; set; }
		public decimal? aver_temp_tap_water_heat_period { get; set; }
		public decimal? aver_temp_tap_water_notheat_period { get; set; }
		public decimal? calc_air_temp_after_calc_coldest { get; set; }
		public decimal? fact_aver_temp_heat_period { get; set; }
		public int? fact_length_heat_period { get; set; }
		public short? coldest_month_id { get; set; }
		public int? length_year_prevent_off_system { get; set; }
	}

	[Keyless]
	public class TerritorialDivisionGeneralInfoDataOneViewModel
	{
		public int Id { get; set; }
		public int data_status { get; set; }
		public string? unom_p { get; set; }
		public short? district_num { get; set; }
		public int? layer_id { get; set; }
		public int? layer_sys { get; set; }
		public short? region_id { get; set; }
		public string? region_short { get; set; }
		public string? region_name { get; set; }
		public string? district_name { get; set; }
					   
	}

	[Keyless]
	public class TerritorialDivisionPopulationListViewModel
	{
		//public int Id { get; set; }
		//public int data_status { get; set; }
		public int? district_id { get; set; }
		public int perspective_year { get; set; }
		public int? populate_size { get; set; }
	}

	[Keyless]
	public class TerritorialDivisionPopulationViewModel
	{
		public int Id { get; set; }
		public int data_status { get; set; }
		//public int? perspective_year { get; set; }
		//public int? populate_size { get; set; }
		public List<TerritorialDivisionPopulationListViewModel> TerritorialDivisionPopulationList { get; set; }
	}

	[Keyless]
	public class TerritorialDivisionUpdateValue
	{
	 public string col_name { get; set; }
	 public decimal col_value { get; set; }
	}

	[Keyless]
	public class TerritorialDivisionListViewModel
	{
		public Dictionary<string, List<string>> DynamicFields { get; set; }
	}
}
