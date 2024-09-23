using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class ActiveZoneETOViewModel
	{
		public int? eto_id { get; set; }
		public string? unom_eto { get; set; }
		public string? territory { get; set; }
		public string? is_liquidated { get; set; }
		public int? year_liquidation { get; set; }
		public string? reason_liquidation { get; set; }
		public int? layer_id { get; set; }
		public int? layer_sys { get; set; }
	}

	[Keyless]
	public class ActiveZoneETOOneDataViewModel
	{
		public int eto_id { get; set; }
		public int? hss_id { get; set; }
		public string? unom_eto { get; set; }
		public string? territory { get; set; }
		public bool is_liquidated { get; set; }
		public int? year_liquidation { get; set; }
		public string? reason_liquidation { get; set; }
		public int? layer_id { get; set; }
		public int? layer_sys { get; set; }
		[NotMapped]
		public int[] eto_distr { get; set; }
		[NotMapped]
		public int[] eto_hss { get; set; }
		public List<UnomHSSListViewModel> unomHSS { get; set; }
		public List<District> districts { get; set; }
		public List<DistrictListViewModel> ActiveZoneETODistrictList { get; set; }
		public List<ActiveZoneETOHssListViewModel> ActiveZoneETOHssList { get; set; }
	}

	[Keyless]
	public class ActiveZoneETODistrictListViewModel
	{
		public string? territories { get; set; }
		public string? region { get; set; }
	}

	[Keyless]
	public class ActiveZoneETOHssListViewModel
	{
		public int? hss_id { get; set; }
		public string? unom_hss { get; set; }
	}

	[Keyless]
	public class UnomHSSListViewModel
	{
		public int? hss_id { get; set; }
		public string? unom_hss { get; set; }
	}
}
