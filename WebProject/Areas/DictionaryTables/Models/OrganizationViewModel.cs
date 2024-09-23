using Microsoft.EntityFrameworkCore;

namespace WebProject.Areas.DictionaryTables.Models
{
	[Keyless]
	public class OrganizationViewModel
	{
		public int org_id { get; set; }
		public string? unom_org { get; set; }
		public string? full_name { get; set; }
		public string? short_name { get; set; }
		public string? activity_type_name { get; set; }
		public string? org_status_name { get; set; }
		public string? org_struct { get; set; }
		public string? pozition_head_manager { get; set; }
		public string? l_name_head_manager { get; set; }
		public string? f_name_head_manager { get; set; }
		public string? m_name_head_manager { get; set; }
		public string? ogrn { get; set; }
		public string? inn { get; set; }
		public string? kpp { get; set; }
		public string? org_contact_phones { get; set; }
		public string? org_emails { get; set; }
	}

	[Keyless]
	public class OrganizationOneDataViewModel
	{
		public int org_id { get; set; }
		public string? unom_org { get; set; }
		public int data_status { get; set; }
		public string? full_name { get; set; }
		public string? short_name { get; set; }
		public short? activity_type_id { get; set; }
		public short? org_status_id { get; set; }
		public bool org_struct { get; set; }
		public string? pozition_head_manager { get; set; }
		public string? l_name_head_manager { get; set; }
		public string? f_name_head_manager { get; set; }
		public string? m_name_head_manager { get; set; }
		public string? ogrn { get; set; }
		public string? inn { get; set; }
		public string? kpp { get; set; }
		public string? org_contact_phones { get; set; }
		public string? org_emails { get; set; }
	}
}
