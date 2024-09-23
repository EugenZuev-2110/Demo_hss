using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Areas.HeatPointsAndConsumers.Models
{
	[Keyless]
	public class BuildingListViewModel
	{
		public int building_id { get; set; }
		public string? address { get; set; }
		public string? district_name { get; set; }
		public string? floor_name { get; set; }
		public string? kadnum_zu { get; set; }
		public string? kadnum_oks { get; set; }
		public int? fias_build { get; set; }
		public int? fias_zu { get; set; }
		public int? func_num_zone { get; set; }
	}
	[Keyless]
	public class BuildingOneDataViewModel
	{
		public int building_id { get; set; }
		public int data_status { get; set; }
		public string? address { get; set; }
		public short? floor_id { get; set; }
		public int? district_id { get; set; }
		public long? bti_build_unom { get; set; }
		public string? kadnum_zu { get; set; }
		public string? kadnum_oks { get; set; }
		public int? fias_build { get; set; }
		public int? fias_zu { get; set; }
		public int? func_num_zone { get; set; }
	}
	[Keyless]
	public class BuildingConsumerViewModel
	{
		public int consumer_id { get; set; }
		public int? building_id { get; set; }
		public string? unom_pp { get; set; }
	}

	[Table("Buildings", Schema = "consumers")]
	public class Buildings
	{
		public int building_id { get; set; }
		public int? district_id { get; set; }
		public string? unom_building { get; set; }
		public string? kadnum_zu { get; set; }
		public string? kadnum_oks { get; set; }
        public long? bti_build_unom { get; set; }
        public int? fias_build { get; set; }
		public int? fias_zu { get; set; }
		public int? func_num_zone { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}
	[Table("Buildings_History", Schema = "consumers")]
	public class Buildings_History
	{
		public int data_status { get; set; }
		public int building_id { get;set; }
		public short? floor_id { get;set; }
		public string? address { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("Dict_Floors", Schema = "consumers")]
	public class Dict_Floors
	{
		public short Id { get; set; }
		public string? floor_name { get; set; }
	}

    [Table("Dict_CalcMethodTypes", Schema = "consumers")]
    public class Dict_CalcMethodTypes
    {
        public short Id { get; set; }
        public string? mcalc_type_name { get; set; }
        public string? mcalc_type_short { get; set; }
    }

    [Table("Dict_Statuses", Schema = "consumers")]
    public class Dict_Statuses
    {
        public short Id { get; set; }
        public string? status_name { get; set; }
        public string? status_short { get; set; }
    }
}
