using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Models.DictionaryTables
{
	public class DataBaseDictionaryTablesModel
	{
		[Table("DistrictsValues_History", Schema = "dictionary")]
		public class DistrictsValues_History
		{
			public int district_id { get; set; }
			public int data_status { get; set; }
			public int? layer_id { get; set; }
			public int? layer_sys { get; set; }
			public decimal? calc_air_temp_ht_vent { get; set; }
			public decimal? aver_temp_coldest_month { get; set; }
			public decimal? aver_temp_tap_water_heat_period { get; set; }
			public decimal? aver_temp_tap_water_notheat_period { get; set; }
			public decimal? calc_air_temp_after_calc_coldest { get; set; }
			public decimal? fact_aver_temp_heat_period { get; set; }
			public decimal? aver_temp_heat_period { get; set; }
			public int? fact_length_heat_period { get; set; }
			public int? length_heat_period { get; set; }
			public short? coldest_month_id { get; set; }
			public int? length_year_prevent_off_system { get; set; }
			public bool? is_deleted { get; set; }
			public DateTime? create_date { get; set; }
			public DateTime? edit_date { get; set; }
			public int? user_id { get; set; }

		}

		[Table("DistrictsValues_Perspective", Schema = "dictionary")]
		public class DistrictsValues_Perspective
		{
			public int district_id { get; set; }
			public int data_status { get; set; }
			public int? perspective_year { get; set; }
			public int? populate_size { get; set; }
			public DateTime? create_date { get; set; }
			public DateTime? edit_date { get; set; }
			public int? user_id { get; set; }
		}

		[Table("Months", Schema = "dictionary")]
		public class Months
		{
			public short? Id { get; set; }
			public string? month_name { get; set; }
		}

		[Table("Districts", Schema = "dictionary")]
		public class Districts
		{
			public int Id { get; set; }
			public short? region_id { get; set; }
			public short? district_num { get; set; }
			public string? district_name { get; set; }
			public string? unom_district { get; set; }
		}

		[Table("Regions", Schema = "dictionary")]
		public class Regions
		{
			public short Id { get; set; }
			public string? region_name { get; set; }
			public string? region_short { get; set; }
			public short? group_type { get; set; }
			public int? cnt_districts { get; set; }
		}

		[Table("Dict_Diameters_Consumptions", Schema = "networks")]
		public class Dict_Diameters_Consumptions
		{
			public short Id { get; set; }
			public decimal? ht_net_in_diam { get; set; }
			public decimal? ht_net_out_diam { get; set; }
			public int? cond_ht_net_diam { get; set; }
			public decimal? consumption { get; set; }
		}

		[Table("Dict_IzolTypes", Schema = "networks")]
		public class Dict_IzolTypes
		{
			public short Id { get; set; }
			public string? izol_type_name { get; set; }
			public double? ht_conduct_coef { get; set; }
			public double? ht_trasfer_coef { get; set; }
		}

		[Keyless]
		public class DistrictListViewModel
		{
			public string? territories { get; set; }
			public string? region { get; set; }
		}

		[Keyless]
		public class District
		{
			public int? id { get; set; }
			public string? distr_name { get; set; }
		}

		[Table("UploadsTemplates", Schema = "dictionary")]
		public class UploadsTemplates
		{
			public int Id { get; set; }
			public string? template_name { get; set; }
			public string? template_path { get; set; }
			public string? description { get; set; }
		}
		[Table("TemperatureGraphics", Schema = "dictionary")]
		public class TemperatureGraphics
		{
			public int temp_graph_id { get; set; }
			public string? temp_graph_unom { get; set; }
			public string? temp_graph_name { get; set; }
			public short? system_type_id { get; set; }
			public DateTime? create_date { get; set; }
			public DateTime? edit_date { get; set; }
			public int? user_id { get; set; }
		}

	}
}
