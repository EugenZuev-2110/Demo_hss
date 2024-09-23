using DocumentFormat.OpenXml.Vml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace WebProject.Areas.TSO.Models
{
	[Keyless]
	public class EcologyIndicatorsViewModel
	{
		public int? Id { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public decimal? fact_all { get; set; }
		public decimal fact_nitrogen_oxides { get; set; }
		public decimal fact_oxide_carbon { get; set; }
		public decimal fact_benzo_a_pyrene { get; set; }
		public decimal fact_sulfur_dioxide { get; set; }
		public decimal fact_oil_ash { get; set; }
		public decimal fact_coal_ash { get; set; }
		public decimal fact_inorganic_dust { get; set; }
		public decimal fact_others { get; set; }
		public decimal? plan_all { get; set; }
		public decimal? plan_nitrogen_dioxide { get; set; }
		public decimal plan_nitrogen_oxides { get; set; }
		public decimal plan_oxide_carbon { get; set; }
		public decimal plan_benzo_a_pyrene { get; set; }
		public decimal plan_sulfur_dioxide { get; set; }
		public decimal plan_oil_ash { get; set; }
		public decimal plan_coal_ash { get; set; }
		public decimal plan_inorganic_dust { get; set; }
		public decimal plan_others { get; set; }
		public decimal greenhouse_gas_emissions { get; set; }
		public decimal current_total_costs { get; set; }
		public decimal current_costs { get; set; }
		public decimal current_costs_own_funds { get; set; }
		public decimal pay_nature_services { get; set; }
		public decimal overhaul_costs { get; set; }
		public decimal depreciation_cost_restoration { get; set; }
		public decimal revenue_bu_products { get; set; }
		public decimal pay_pollution { get; set; }
		public decimal pay_discharges { get; set; }
		public decimal pay_waste { get; set; }
		public decimal pay_disturbance_low { get; set; }
	}
}
