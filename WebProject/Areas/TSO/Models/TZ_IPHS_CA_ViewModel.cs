using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Areas.TSO.Models
{
	[Keyless]
	public class TZCalcCostsIpHsCaDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public string? unom_tz { get; set; }
		public string? territory { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public string? activity_types { get; set; }
		public decimal expenses_all_iphs_fact { get; set; }
		public decimal profit_own_funds_iphs_fact { get; set; }
		public decimal amortization_all_iphs_fact { get; set; }
		public decimal budget_all_iphs_fact { get; set; }
		public decimal attracted_funds_all_iphs_fact { get; set; }
		public decimal connection_charge_iphs_fact { get; set; }
		public decimal other_means_iphs_fact { get; set; }
		public decimal expenses_all_ca_fact { get; set; }
		public decimal profit_own_funds_ca_fact { get; set; }
		public decimal amortization_all_ca_fact { get; set; }
		public decimal budget_all_ca_fact { get; set; }
		public decimal attracted_funds_all_ca_fact { get; set; }
		public decimal connection_charge_ca_fact { get; set; }
		public decimal other_means_ca_fact { get; set; }
		public decimal actual_grants_budget_fact { get; set; }
		public decimal actual_over_profit_fact { get; set; }

	}

	[Keyless]
	public class TZExpenses_IPHS_CA_ViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short finance_type_id { get; set; }
		public decimal? expenses_all_fact_1 { get; set; }
		public decimal? expenses_all_fact_2 { get; set; }
		public decimal? expenses_all_fact_3 { get; set; }
		public decimal? profit_own_funds_fact_1 { get; set; }
		public decimal? profit_own_funds_fact_2 { get; set; }
		public decimal? profit_own_funds_fact_3 { get; set; }
		public decimal? amortization_all_fact_1 { get; set; }
		public decimal? amortization_all_fact_2 { get; set; }
		public decimal? amortization_all_fact_3 { get; set; }
		public decimal? amortization_finance_invest_fact_1 { get; set; }
		public decimal? amortization_finance_invest_fact_2 { get; set; }
		public decimal? amortization_finance_invest_fact_3 { get; set; }
		public decimal? amortization_deduction_on_credit_fact_1 { get; set; }
		public decimal? amortization_deduction_on_credit_fact_2 { get; set; }
		public decimal? amortization_deduction_on_credit_fact_3 { get; set; }
		public decimal? budget_all_fact_1 { get; set; }
		public decimal? budget_all_fact_2 { get; set; }
		public decimal? budget_all_fact_3 { get; set; }
		public decimal? federal_budget_fact_1 { get; set; }
		public decimal? federal_budget_fact_2 { get; set; }
		public decimal? federal_budget_fact_3 { get; set; }
		public decimal? regional_budget_fact_1 { get; set; }
		public decimal? regional_budget_fact_2 { get; set; }
		public decimal? regional_budget_fact_3 { get; set; }
		public decimal? local_budget_fact_1 { get; set; }
		public decimal? local_budget_fact_2 { get; set; }
		public decimal? local_budget_fact_3 { get; set; }
		public decimal? attracted_funds_all_fact_1 { get; set; }
		public decimal? attracted_funds_all_fact_2 { get; set; }
		public decimal? attracted_funds_all_fact_3 { get; set; }
		public decimal? attracted_credits_fact_1 { get; set; }
		public decimal? attracted_credits_fact_2 { get; set; }
		public decimal? attracted_credits_fact_3 { get; set; }
		public decimal? attracted_loans_fact_1 { get; set; }
		public decimal? attracted_loans_fact_2 { get; set; }
		public decimal? attracted_loans_fact_3 { get; set; }
		public decimal? attracted_other_funds_fact_1 { get; set; }
		public decimal? attracted_other_funds_fact_2 { get; set; }
		public decimal? attracted_other_funds_fact_3 { get; set; }
		public decimal? attracted_received_funds_securities_fact_1 { get; set; }
		public decimal? attracted_received_funds_securities_fact_2 { get; set; }
		public decimal? attracted_received_funds_securities_fact_3 { get; set; }
		public decimal? connection_charge_fact_1 { get; set; }
		public decimal? connection_charge_fact_2 { get; set; }
		public decimal? connection_charge_fact_3 { get; set; }
		public decimal? other_means_fact_1 { get; set; }
		public decimal? other_means_fact_2 { get; set; }
		public decimal? other_means_fact_3 { get; set; }
	}

	[Keyless]
	public class TZExpensesAddValues_ViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public decimal? actual_grants_budget_fact_1 { get; set; }
		public decimal? actual_grants_budget_fact_2 { get; set; }
		public decimal? actual_grants_budget_fact_3 { get; set; }
		public decimal? actual_over_profit_fact_1 { get; set; }
		public decimal? actual_over_profit_fact_2 { get; set; }
		public decimal? actual_over_profit_fact_3 { get; set; }
		
	}
}