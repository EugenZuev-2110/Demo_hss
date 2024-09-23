using Microsoft.EntityFrameworkCore;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class OutPutsSourcesListViewModel
	{
		public int source_output_id { get; set; }
		public string? unom_output { get; set; }
		public string? source_name { get; set; }
		public string? output_name { get; set; }
	}

	[Keyless]
	public class OutPutsSourcesOnetViewModel
	{
		public int source_output_id { get; set; }
		public int? data_status { get; set; }
		public string? unom_output { get; set; }
		public string? output_name { get; set; }
		public int? value_id { get; set; }
	}
}
