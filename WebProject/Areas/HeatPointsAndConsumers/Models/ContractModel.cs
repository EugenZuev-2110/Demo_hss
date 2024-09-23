using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Areas.HeatPointsAndConsumers.Models
{
	[Table("Contracts", Schema = "consumers")]
	public class Contracts
	{
		public int contract_id { get; set; }
		public int? tso_id { get; set; }
		public string? contract_num { get; set; }
		public DateTime? contract_date { get; set; }
		public DateTime? contract_valid_date { get; set; }
		public string? org_name { get; set; }
		public string? org_inn { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("Contracts_History", Schema = "consumers")]
	public class Contracts_History
	{
		public int contract_id { get; set; }
		public int data_status { get; set; }
		public short? contract_status_id { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}


	[Keyless]
	public class ContractListViewModel 
	{
		public int contract_id { get; set; }
		public int data_status { get; set; }
		public string? short_name { get; set; }
		public string? contract_num { get; set; }
		public DateTime? contract_date { get; set; }
		public DateTime? contract_valid_date { get; set; }
		public string? org_name { get; set; }
		public string? org_inn { get; set; }
		public string? contract_status_name { get; set; }
	}

	[Keyless]
	public class ContractOneDataViewModel
	{
		public int contract_id { get; set; }
		public int data_status { get; set; }
		public int? tso_id { get; set; }
		public string? contract_num { get; set; }
		public DateTime? contract_date { get; set; }
		public DateTime? contract_valid_date { get; set; }
		public string? org_name { get; set; }
		public string? org_inn { get; set; }
		public short? contract_status_id { get; set; }
	}

	[Keyless]
	public class ContractConsumerListViewModel
	{
		public int consumer_id { get; set; }
		public string? unom_pp { get; set; }
		public string? address { get; set; }
		public decimal? ctr_hl_heat_hw { get; set; }
		public decimal? ctr_hl_vent_hw { get; set; }
		public decimal? ctr_hl_gvss_hw { get; set; }
		public decimal? ctr_hl_tech_hw { get; set; }
		public decimal? ctr_hl_other_hw { get; set; }
		public decimal? ctr_hl_cond_hw { get; set; }
		public decimal? ctr_hl_curtains_hw { get; set; }
		public decimal? ctr_hl_heat_steam { get; set; }
		public decimal? ctr_hl_vent_steam { get; set; }
		public decimal? ctr_hl_gvss_steam { get; set; }
		public decimal? ctr_hl_tech_steam { get; set; }
		public decimal? ctr_hl_other_steam { get; set; }
		public decimal? ctr_hl_cond_steam { get; set; }
		public decimal? ctr_hl_curtains_steam { get; set; }

	}

}
