using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class TariffZoneListViewModel
	{
		public int? tz_id { get; set; }
		public string? unom_tz { get; set; }
		public string? tso_name { get; set; }
		public string? tz_name { get; set; }
		public string? tz_num { get; set; }
		public string? tz_num_plan { get; set; }
		public string? combine_prod_more25 { get; set;}
		public string? combine_prod_less25 { get; set; }
		public string? not_combine_prod { get; set; }
		public string? transfer { get; set; }
		public string? sale { get; set; }
		public string? delivery_contract { get; set; }
		public int? layer_id { get; set;}
		public int? layer_sys { get; set; }
		public string? territory { get; set;}
	}

	[Keyless]
	public class TariffZoneOneDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public string? unom_tz { get; set; }
		public string? tz_name { get; set; }
		public string? tz_num { get; set; }
		public string? tz_num_plan { get; set; }
		public bool combine_prod_more25 { get; set; }
		public bool combine_prod_less25 { get; set; }
		public bool not_combine_prod { get; set; }
		public bool transfer { get; set; }
		public bool sale { get; set; }
		public bool delivery_contract { get; set; }
		public short? tariff_differentiation { get; set; }
		public int? layer_id { get; set; }
		public int? layer_sys { get; set; }
		[NotMapped]
		public int[] tz_distr { get; set; }
		public List<District> districts { get; set; }
		public List<TariffZoneTsoListViewModel> TariffZoneTsoList { get; set; }
		public List<DistrictListViewModel> TariffZoneDistrictList { get; set; }
	}

	[Keyless]
	public class TariffZoneTsoViewModel
	{
		public int? tz_id { get; set; }
		public int? data_status { get; set; }
		public List<TariffZoneTsoListViewModel> TariffZoneTsoList { get; set; }
	}

	[Keyless]
	public class TariffZoneTsoListViewModel 
	{
		public int? tso_id { get; set; }
		public int? perspective_year { get; set; }
	}

	[Keyless]
	public class TariffZoneDistrictViewModal
	{
		public int? tz_id { get; set; }
		public int? data_status { get; set; }
		public List<DistrictListViewModel> TariffZoneDistrictList { get; set; }
	}

}
