using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class NormLossListViewModel
	{
		public int Id { get; set; }
		public int? cond_ht_net_diam { get; set; }
		public string? temp_graph_name { get; set; }
		public string? net_laying_type_name { get; set; }
		public decimal? norm_density { get; set; }
	}
	[Keyless]
	public class NormLossOneDataViewModel
	{
		public int Id { get; set; }
		public int data_status { get; set; }
		public short? net_diam_id { get; set; }
		public short? temp_graph_id { get; set; }
		public short? net_laying_type_id { get; set; }
		public decimal? norm_density { get; set; }
	}

	[Table("Dict_NetLayingTypes", Schema = "networks")]
	public class Dict_NetLayingTypes
	{
		public short Id { get; set; }
		public string? net_laying_type_name { get; set; }
		public string? net_laying_type_short { get; set; }
	}

	[Table("Dict_NormLoss_History", Schema = "networks")]
	public class Dict_NormLoss_History
	{
		public int Id { get; set; }
		public int data_status { get; set; }
		public short? net_diam_id { get; set; }
		public short? temp_graph_id { get; set; }
		public short? net_laying_type_id { get; set; }
		public decimal? norm_density { get; set; }
	}
}
