using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Areas.TSO.Models
{
    [Keyless]
    public class TSOListViewModel
    {
        public int Id { get; set; }
        public string? unom_org { get; set; }
        public string? code_tso { get; set; }
        public string? short_name { get; set; }
        public string? inn { get; set; }
        public string? ogrn { get; set; }
        public string? kpp { get; set; }
        public string? org_type_name { get; set; }
        public string? org_status_name { get; set; }
        public string? unom_eto { get; set; }
        public string? unom_eto_list { get; set; }
        public string? unom_tz_list { get; set; }
        public string? combine_prod_more25 { get; set; }
        public string? combine_prod_less25 { get; set; }
        public string? not_combine_prod { get; set; }
        public string? transfer { get; set; }
        public string? sale { get; set; }
        public string? delivery_contract { get; set; }
        public string? org_property_type_name { get; set; }
        public decimal? org_size_own_capital { get; set; }
        public string? year_own_capital { get; set; }
        public string? tso_nds_pay { get; set; }
        public string? pozition_head_manager { get; set; }
        public string? fullname_head_manager { get; set; }
        public string? org_contact_phones { get; set; }
        public string? org_emails { get; set; }
        public string? send_letters_type_name { get; set; }
    }

    [Keyless]
    public class TSOViewModel
    {
        public int Id { get; set; }
		public int data_status { get; set; }
		public string? short_name { get; set; }
        public string? full_name { get; set; }
        public string? code_tso { get; set; }
		public string? notes { get; set; }
		public string? inn { get; set; }
        public string? ogrn { get; set; }
        public string? kpp { get; set; }
        public bool org_struct { get; set; }
        public bool tso_nds_pay { get; set; }
        public decimal? org_size_own_capital { get; set; }
        public DateTime? year_own_capital { get; set; }
        public string? pozition_head_manager { get; set; }
        public string? l_name_head_manager { get; set; }
        public string? f_name_head_manager { get; set; }
        public string? m_name_head_manager { get; set; }
        public short? org_property_type_id { get; set; }
        public string? org_contact_phones { get; set; }
        public string? org_emails { get; set; }
        public int? eto_id { get; set; }
        public int[] etos { get; set; }
        public List<ETOViewModel> eto_list { get; set; }
		public List<ETOViewModel> eto_list_status { get; set; }
		public short? send_letters_type_id { get; set; }
    }

    [Keyless]
    public class ETOViewModel
    {
        public int eto_id { get; set; }
        public string? unom_eto { get; set; }
    }

	public class TSOPerspectiveViewModel
	{
		public int Id { get; set; }
		public int data_status { get; set; }
		public List<TSOPerspectiveListViewModel> TSOPerspectiveList { get; set; }
	}

	[Keyless]
	public class TSOPerspectiveListViewModel
	{
		public int perspective_year { get; set; }
		public string? perspective_year_dt { get; set; }
		public short? org_status_id { get; set; }
		public short? tso_type_id { get; set; }
		public string? unom_tz_list { get; set; }
	}

	public class TSOResponsiblePersonsViewModel
	{
		public int tso_Id { get; set; }
		public int data_status { get; set; }
		public List<TSOResponsiblePersonsListViewModel> TSOResponsiblePersonsList { get; set; }
	}
	[Keyless]
	public class TSOResponsiblePersonsListViewModel
	{
		public int person_num { get; set; }
		public string? position_respons_pers { get; set; }
		public string? l_name_respons_pers { get; set; }
		public string? f_name_respons_pers { get; set; }
		public string? m_name_respons_pers { get; set; }
		public string? phones_respons_pers { get; set; }
		public string? emails_respons_pers { get; set; }
		public bool is_deleted { get; set; }
	}

	[Keyless]
	public class TZSourcesViewModel
	{
		public int value_id { get; set; }
		public string? value_name { get; set; }

	}

    [Keyless]
    public class TSOUploadFileViewModel
    {
        public int data_status { get; set; }
        public string upload_file { get; set; }

    }

	[Keyless]
	public class TSOUploadedListViewModel
	{
		//public int data_status { get; set; }
		//public int batch_id { get; set; }
		//public bool is_uploaded { get; set; }
		public int? Id { get; set; }
		public string? Name { get; set; }
		public string? INN { get; set; }

	}
}