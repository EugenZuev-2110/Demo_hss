using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models
{
    [Keyless]
    public class Org_List
    {
        public int org_id { get; set; }
        public string? org_name { get; set; }
    }

	[Keyless]
	public class TZNames_List
	{
		public int tz_id { get; set; }
		public string? tz_name { get; set; }
	}

	[Keyless]
	public class Values_List
	{
		public int value_id { get; set; }
		public string? value_name { get; set; }
	}

    [Keyless]
    public class District_List
    {
        public int distr_id { get; set; }
        public string? distr_name { get; set; }
    }

	[Keyless]
	public class Region_List
	{
		public short region_id { get; set; }
		public string? region_name { get; set; }
		public string? region_short { get; set; }
	}

	[Keyless]
	public class TSO_List
	{
		public int tso_id { get; set; }
		public string? tso_name { get; set; }
	}

	[Keyless]
	public class HpDiamHtSupply
	{
		public int supply_per_year { get; set; }
		public int supply_diam { get; set; }
	}

    [Keyless]
    public class ConsumerDistrictUnom
    {
        public string? district { get; set; }
        public string? unom_addr { get; set; }
    }

	[Keyless]
	public class Values_ListShort
	{
		public short value_id { get; set; }
		public string? value_name { get; set; }
	}
}
