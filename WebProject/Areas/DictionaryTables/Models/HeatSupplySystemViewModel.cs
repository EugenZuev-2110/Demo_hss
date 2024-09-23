using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class HeatSupplySystemViewModel
	{
		public int hss_id { get; set; }
		public string? unom_hss { get; set; }
		public string? territory { get; set; }
		public string? is_liquidated { get; set; }
		public int? year_liquidation { get; set; }
		public string? reason_liquidation { get; set; }
		public int? layer_id { get; set; }
		public int? layer_sys { get; set; }
	}

	[Keyless]
	public class HeatSupplySystemOneDataViewModel
	{
		public int hss_id { get; set; }
		public string? unom_hss { get; set; }
		public int? eto_id { get; set; }
		public string? territory { get; set; }
		public bool is_liquidated { get; set; }
		public int? year_liquidation { get; set; }
		public string? reason_liquidation { get; set; }
		public int? layer_id { get; set; }
		public int? layer_sys { get; set; }
		[NotMapped]
		public int[] hss_distr { get; set; }
		public List<District> districts { get; set; }
		public List<DistrictListViewModel> HSSDistrictList { get; set; }
	}
}
