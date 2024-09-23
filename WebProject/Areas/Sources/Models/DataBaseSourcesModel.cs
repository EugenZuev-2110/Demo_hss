using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using WebProject.Data;

namespace DataBase.Models.Sources
{
    [Table("Dict_FuelTypes", Schema = "sources")]
    public class Dict_FuelTypes
	{
        [Key]
        public short Id { get; set; }
        public string? fuel_type_name { get; set; }
		public string? fuel_type_short { get; set; }
		public decimal? calc_low_combus_ht { get; set; }

	}

	[Table("HeatSupplySystem", Schema = "sources")]
	public class HeatSupplySystem
	{
		[Key]
		public int hss_id { get; set; }
		public int? eto_id { get; set; }
		public string? territory { get; set; }
		public string? unom_hss { get; set; }
		public bool? is_liquidated { get; set; }
		public int? year_liquidation { get; set; }
		public string? reason_liquidation { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("HSS_DistrictsMapps", Schema = "sources")]
	public class HSS_DistrictsMapps
	{
		[Key]
		public int hss_id { get; set; }
		public int district_id { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("HSS_LayersMapping", Schema = "sources")]
	public class HSS_LayersMapping
	{
		public int hss_id { get; set; }
		public int layer_id { get; set; }
		public int layer_sys { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("S_History", Schema = "sources")]
	public class S_History
	{
		public int data_status { get; set; }
		public int source_id { get; set; }
		public int? tz_id { get; set; }
		public short? source_type_id { get; set; }
		public string? source_name { get; set; }
		public int? org_owner_id { get; set; }
		public bool? obj_vacant_prop { get; set; }
		public bool? obj_old_prop { get; set; }
		public double? phys_wear { get; set; }
		public double? amortis_wear { get; set; }
		public int? start_year_expl { get; set; }
		public double? size_sanitary_zone { get; set; }
		public double? temp_supply_fact_T1 { get; set; }
		public double? temp_reverse_fact_T2 { get; set; }
        public double? temp_supply_fact_T3 { get; set; }
        public double? temp_reverse_fact_T4 { get; set; }
        public double? temp_supply_fact_T5 { get; set; }
        public double? temp_reverse_fact_T6 { get; set; }
		public double? temp_steam_consumers { get; set; }
		public double? pressure_steam_consumers { get; set; }
		public string? muid { get; set; }
		public string? lotus_id { get; set; }
		public string? kasu_composite_id { get; set; }
		public string? kasu_id { get; set; }
		public string? kasu_map_id { get; set; }
		public bool? avail_condens_pipe {  get; set; }
        public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("S_Perspective", Schema = "sources")]
	public class S_Perspective
	{
		public int source_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public int? tso_id { get;set; }
		public int? hssn_id { get; set; }
		public short? source_status_id { get; set; }
		public short? temp_plan_id { get; set; }
		public short? temp_plan_heat_id { get; set; }
		public short? temp_plan_gvs_id { get; set; }
		public short? hn_heat_out_scheme_type_id { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
		
	}

	[Table("Dict_SourceStatuses", Schema = "sources")]
	public class Dict_SourceStatuses
	{
		public short Id { get; set; }
		public string? source_status_name { get; set; }
    }

    [Table("S_DocsPhotos_History", Schema = "sources")]
    public class S_DocsPhotos_History
    {
        public int data_status { get; set; }
        public int source_id { get; set; }
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

	[Table("Dict_HeatOutSchemeTypes" , Schema = "sources")]
	public class Dict_HeatOutSchemeTypes
	{
		public short Id { get; set; }
		public string? scheme_heat_out_type_name { get; set; }
    }
}