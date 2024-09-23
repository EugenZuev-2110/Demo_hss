namespace WebProject.Areas.DictionaryTables.Models
{
	public class LayerViewModel
	{
		public int Id { get; set; }
		public string? layer_unom { get; set; }
		public string? layer_status { get; set; }
		public string? layer_type { get; set; }
		public int? layer_data_status { get; set; }
		public int? layer_perspective_year { get; set; }
		public string? layer_filename { get; set; }
		public string? layer_filepath { get; set; }
		public string? layer_description { get; set; }
		public int? layer_delete_year { get; set; }
		public string? layer_delete_reason { get; set; }
	}
}
