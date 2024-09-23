using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBaseHSS.Models
{
    [Table("DataStatuses", Schema = "dictionary")]
    public class DataStatuses
    {
        [Key]
        public int data_status { get; set; }
        public bool is_active { get; set; }
        public DateTime? closed_date { get; set; }
        public int? last_perspective_year { get; set; }
        public string? data_status_dt { get; set; }
    }

    [Table("PerspectiveYears", Schema = "dictionary")]
    public class PerspectiveYears
    {
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public string? perspective_year_dt { get; set; }
    }

	public class EX_Logs
	{
		public int Id { get; set; }
		public string? method_name { get; set; }
		public string? parameters { get; set; }
		public string? exception { get; set; }
		public DateTime? create_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("Layers", Schema = "dictionary")]
	public class Layers
    {
        public int Id { get; set; }
        public string? layer_unom { get; set; }
        public short? layer_status_id { get; set; }
        public short? layer_type_id { get; set; }
        public int? layer_data_status { get; set; }
        public int? layer_perspective_year { get; set; }
        public string? layer_filename { get; set; }
        public string? layer_filepath { get; set; }
        public string? layer_description { get; set; }
        public int? layer_delete_year { get; set; }
		public string? layer_delete_reason { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("LayerStatuses", Schema = "dictionary")]
	public class LayerStatuses
	{
		[Key]
		public short Id { get; set; }
		public string? layer_status_name { get; set; }
	}

	[Table("LayerTypes", Schema = "dictionary")]
	public class LayerTypes
	{
		[Key]
		public short Id { get; set; }
		public string? layer_type_name { get; set; }
	}
}