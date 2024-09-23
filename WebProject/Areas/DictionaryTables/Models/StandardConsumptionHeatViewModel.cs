using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class BuildingCharacteristicsViewModel
	{
			public short floor_id { get; set; }
			public int data_status { get; set; }
			public string floor { get; set; }
			public short purpose_build_type_id { get; set; }
			public decimal? NormBasicCharact { get; set; }
			public decimal? NormHWConsumption { get; set; }
			public decimal? AvgFloorHeight { get; set; }
			public decimal? AvgBuildProvision { get; set; }
			public decimal? CalcAirTemp { get; set; }
			public decimal? TempHW { get; set; }
	}

	[Keyless]
	public class CalcCoefViewModel
	{
		public int data_status { get; set; }
		[Precision(8, 3)]
		public decimal? coef_rad { get; set; }
		[Precision(8, 3)]
		public decimal? coef_hw_heat_decrease { get; set; }
		[Precision(8, 3)] 
		public decimal? coef_hw_vent_decrease { get; set; }
		[Precision(8, 3)]
		public decimal? coef_hw_gvs_decrease { get; set; }
		[Precision(8, 3)]
		public decimal? coef_hw_tech_decrease { get; set; }
		[Precision(8, 3)]
		public decimal? coef_steam_heat_decrease { get; set; }
		[Precision(8, 3)]
		public decimal? coef_steam_vent_decrease { get; set; }
		[Precision(8, 3)]
		public decimal? coef_steam_gvs_decrease { get; set; }
		[Precision(8, 3)]
		public decimal? coef_steam_tech_decrease { get; set; }
		[Precision(8, 3)]
		public decimal? percent_devition_specific_heat_consumption { get; set; }
		public decimal? coef_heat_loss_heat_network { get; set; }
		public decimal? coef_heat_loss_steam_network { get; set; }
		public decimal? coef_heat_loss_systems_gvs_insulated_risers_gvs { get; set; }
		public decimal? coef_heat_loss_systems_gvs_insulated_risers_heated_towel_rail_gvs { get; set; }
		public decimal? coef_heat_loss_systems_gvs_uninsulated_risers_heated_towel_rail_gvs { get; set; }
		public decimal? coef_heat_loss_systems_gvs_insulated_risers { get; set; }
		public decimal? coef_heat_loss_systems_gvs_insulated_risers_heated_towel_rail { get; set; }
		public decimal? coef_heat_loss_systems_gvs_uninsulated_risers_heated_towel_rail { get; set; }
	}

	[Keyless]
	public class CoefRelationHeatVentLoadViewModel
	{ 
		public short Id { get; set; }
		public int data_status { get; set; }
		public string? cpurp_type_name { get; set; }
		[Precision(8, 3)]
		public decimal? coefRelationHeat {  get; set; }
		[Precision(8, 3)]
		public decimal? coefRelationVent {  get; set; }
	}

	[Keyless]
	public class CoefEnergyEfficiencyViewModel
	{ 
		public short coef_id { get; set; }
		public int data_status { get; set;}
		public short? type_purpose_id { get; set; }
		[Precision(8, 3)]
		public decimal? coef_value { get; set; }
		public int perspective_year { get; set; }
	}

	[Keyless]
	public class CalcConsumptionViewModel
	{
		public short calcPurp_id { get; set; }
		public string? cpurp_type_name { get; set; }
		public short floors_id { get; set; }
		public string? floor_name { get; set; }
		public double result { get; set; }
	}

	[Keyless]
	public class CalcConsumptionGVSViewModel
	{
		public short calcPurp_id { get; set; }
		public string? cpurp_type_name { get; set; }
		public double result1_1m { get; set; }
		public double result2_1m { get; set; }
		public double result3_1m { get; set; }
		public double result4_1m { get; set; }
		public double result5_1m { get; set; }
		public double result6_1m { get; set; }
		public double result1_year { get; set; }
		public double result2_year { get; set; }
		public double result3_year { get; set; }
		public double result4_year { get; set; }
		public double result5_year { get; set; }
		public double result6_year { get; set; }
	}

	[Table("CoefBuildConsumer_History", Schema = "consumers")]
	public class CoefBuildConsumer_History
	{
		public short coef_id { get; set; }
		public int data_status { get; set; }
		public short purpose_build_type_id { get; set; }
		public short floors_id { get; set; }
		[Precision(8,3)]
		public decimal? coef_value { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("CoefHeatLoss_History", Schema = "consumers")]
	public class CoefHeatLoss_History
	{
		public short coef_id { get; set; }
		public int data_status { get; set; }
		[Precision(8, 3)]
		public decimal? coef_value { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("CoefHeatConsumption_Perspective", Schema = "consumers")]
	public class CoefHeatConsumption_Perspective
	{
		public short coef_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short type_purpose_id { get; set; }
		[Precision(8, 3)]
		public decimal? coef_value { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}
}
