using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Areas.TSO.Models
{
	[Keyless]
	public class TZCalcCostsNVVDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public string? unom_tz { get; set; }
		public string? territory { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public string? activity_types { get; set; }
		public decimal useful_release_heat_energy_fact { get; set; }
		public decimal useful_release_heat_energy_plan { get; set; }
		public decimal useful_release_heat_energy_realize_fact { get; set; }
		public decimal useful_release_heat_energy_realize_plan { get; set; }
		public decimal coef_realize_consumers_fact { get; set; }
		public decimal coef_realize_consumers_plan { get; set; }
		public decimal useful_release_heat_energy_realize_without_sale_fact { get; set; }
		public decimal useful_release_heat_energy_realize_without_sale_plan { get; set; }
		public decimal costs_all_with_profit_fact { get; set; }
		public decimal costs_all_realize_fact { get; set; }
		public decimal costs_all_onproduct_realize_fact { get; set; }
		public decimal costs_all_ontransport_realize_fact { get; set; }
		public decimal costs_all_fact { get; set; }
		public decimal costs_all_onproduct_fact { get; set; }
		public decimal costs_all_ontransport_fact { get; set; }
		public decimal costs_all_energy_resources_fact { get; set; }
		public decimal costs_all_energy_resources_onproduct_fact { get; set; }
		public string costs_all_energy_resources_ontransport_fact { get; set; }
		public decimal costs_all_operating_fact { get; set; }
		public decimal costs_all_operating_onproduct_fact { get; set; }
		public decimal costs_all_operating_ontransport_fact { get; set; }
		public decimal costs_all_uncontroll_fact { get; set; }
		public decimal costs_all_uncontroll_onproduct_fact { get; set; }
		public decimal costs_all_uncontroll_ontransport_fact { get; set; }
		public decimal loss_fact { get; set; }
		public decimal profit_all_fact { get; set; }
		public decimal calc_profit_fact { get; set; }
		public decimal profit_prod_develop_fact { get; set; }
		public decimal profit_social_develop_fact { get; set; }
		public decimal profit_stimul_fact { get; set; }
		public decimal profit_other_needs_fact { get; set; }
		public decimal profitability_fact { get; set; }
		public decimal nvv_onrealize_plan { get; set; }
		public decimal nvv_all_plan { get; set; }
		public decimal costs_all_realize_plan { get; set; }
		public decimal costs_all_onproduct_realize_plan { get; set; }
		public decimal costs_all_ontransport_realize_plan { get; set; }
		public decimal costs_all_plan { get; set; }
		public decimal costs_all_onproduct_plan { get; set; }
		public decimal costs_all_ontransport_plan { get; set; }
		public decimal costs_all_energy_resources_plan { get; set; }
		public decimal costs_all_energy_resources_onproduct_plan { get; set; }
		public string costs_all_energy_resources_ontransport_plan { get; set; }
		public decimal costs_all_operating_plan { get; set; }
		public decimal costs_all_operating_onproduct_plan { get; set; }
		public decimal costs_all_operating_ontransport_plan { get; set; }
		public decimal costs_all_uncontroll_plan { get; set; }
		public decimal costs_all_uncontroll_onproduct_plan { get; set; }
		public decimal costs_all_uncontroll_ontransport_plan { get; set; }
		public decimal lost_revenue_all_plan { get; set; }
		public decimal excess_funds_plan { get; set; }
		public decimal cross_subsidy_all_plan { get; set; }
		public decimal profit_all_plan { get; set; }
		public decimal calc_norm_profit_plan { get; set; }
		public decimal calc_enterprising_profit_plan { get; set; }
		public decimal regular_lvl_profit_percent_plan { get; set; }
		public decimal regular_lvl_profit_plan { get; set; }
		public decimal regulatory_profit_plan { get; set; }
		public decimal funds_repayment_credit_plan { get; set; }
		public decimal return_rate_plan { get; set; }
		public decimal cost_capital_invest_plan { get; set; }
		public decimal cost_economic_pay_plan { get; set; }
		public decimal capital_investment_all_plan { get; set; }
		public decimal finance_capital_investment_all_plan { get; set; }
		public decimal amortisation_deduction_fonds_plan { get; set; }
		public decimal full_recovery_fonds { get; set; }
		public decimal amortisation_deduction_nonmaterial_active_plan { get; set; }
		public decimal finance_by_nonused_funds_plan { get; set; }
		public decimal finance_by_profit_plan { get; set; }
		public decimal finance_budjet_all_plan { get; set; }
		public decimal finance_by_other_sources_plan { get; set; }
		public decimal finance_by_credits_plan { get; set; }
		public decimal finance_by_realize_securities_plan { get; set; }
		public decimal finance_by_connection_charge_plan { get; set; }
		public decimal economy_costs_all { get; set; }
		public decimal economy_all_plan { get; set; }
		public decimal increase_economy_all_plan { get; set; }
		public decimal economy_operation_cost_plan { get; set; }
		public decimal increase_economy_operation_cost_plan { get; set; }
		public decimal total_economy_noncontrol_cost_plan { get; set; }
		public decimal total_correction_nvv_all_methods_plan { get; set; }
		public decimal index_effective_cost { get; set; }
		public decimal index_consumers_price { get; set; }
		public decimal index_count_actives { get; set; }
		public decimal result_activity_before_reg_price_plan { get; set; }
		public decimal total_correction_nvv_plan { get; set; }
		public decimal correct_accounting_deviation_plan { get; set; }
		public decimal correct_safety_quality_product_plan { get; set; }
		public decimal correct_nvv_change_invest_prog_plan { get; set; }
		public decimal correct_subj_accounting_nvv_plan { get; set; }
		public decimal total_correction_nvv_mB_plan { get; set; }
		public decimal return_capital_plan { get; set; }
		public decimal full_value_invest_plan { get; set; }
		public decimal return_period_invest_plan { get; set; }
		public decimal profit_on_invest_capital_plan { get; set; }
		public decimal initial_amount_invest_plan { get; set; }
		public decimal profit_initial_amount_invest_plan { get; set; }
		public decimal base_invest_plan { get; set; }
		public decimal pure_working_capital_plan { get; set; }
		public decimal norm_pure_working_capital_plan { get; set; }
		public decimal norm_profit_plan { get; set; }
		public decimal amortization_plan { get; set; }
		public decimal correct_nvv_accounting_deviation_plan { get; set; }
		public decimal baseline_cost_reg_managed_plan { get; set; }
		public decimal profitability_plan { get; set; }

	}

	[Keyless]
	public class TZProfitNVVViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? return_rate_plan_1 { get; set; }
		public decimal? return_rate_plan_2 { get; set; }
		public decimal? return_rate_plan_3 { get; set; }
		public decimal? regular_lvl_profit_percent_plan_1 { get; set; }
		public decimal? regular_lvl_profit_percent_plan_2 { get; set; }
		public decimal? regular_lvl_profit_percent_plan_3 { get; set; }
		public decimal? regular_lvl_profit_plan_1 { get; set; }
		public decimal? regular_lvl_profit_plan_2 { get; set; }
		public decimal? regular_lvl_profit_plan_3 { get; set; }
		public decimal? profit_all_fact_1 { get; set; }
		public decimal? profit_all_fact_2 { get; set; }
		public decimal? profit_all_fact_3 { get; set; }
		public decimal? profit_all_plan_1 { get; set; }
		public decimal? profit_all_plan_2 { get; set; }
		public decimal? profit_all_plan_3 { get; set; }
		public decimal? profit_prod_develop_fact_1 { get; set; }
		public decimal? profit_prod_develop_fact_2 { get; set; }
		public decimal? profit_prod_develop_fact_3 { get; set; }
		public decimal? profit_social_develop_fact_1 { get; set; }
		public decimal? profit_social_develop_fact_2 { get; set; }
		public decimal? profit_social_develop_fact_3 { get; set; }
		public decimal? profit_stimul_fact_1 { get; set; }
		public decimal? profit_stimul_fact_2 { get; set; }
		public decimal? profit_stimul_fact_3 { get; set; }
		public decimal? profit_other_needs_fact_1 { get; set; }
		public decimal? profit_other_needs_fact_2 { get; set; }
		public decimal? profit_other_needs_fact_3 { get; set; }
		public decimal? calc_enterprising_profit_plan_1 { get; set; }
		public decimal? calc_enterprising_profit_plan_2 { get; set; }
		public decimal? calc_enterprising_profit_plan_3 { get; set; }
		public decimal? regulatory_profit_plan_1 { get; set; }
		public decimal? regulatory_profit_plan_2 { get; set; }
		public decimal? regulatory_profit_plan_3 { get; set; }
		public decimal? funds_repayment_credit_plan_1 { get; set; }
		public decimal? funds_repayment_credit_plan_2 { get; set; }
		public decimal? funds_repayment_credit_plan_3 { get; set; }
		public decimal? cost_capital_invest_plan_1 { get; set; }
		public decimal? cost_capital_invest_plan_2 { get; set; }
		public decimal? cost_capital_invest_plan_3 { get; set; }
		public decimal? cost_economic_pay_plan_1 { get; set; }
		public decimal? cost_economic_pay_plan_2 { get; set; }
		public decimal? cost_economic_pay_plan_3 { get; set; }
	}

	[Keyless]
	public class TZLostProfitOutCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? lost_revenue_all_plan_1 { get; set; }
		public decimal? lost_revenue_all_plan_2 { get; set; }
		public decimal? lost_revenue_all_plan_3 { get; set; }
		public decimal? lost_revenue_pretrial_dispute_plan_1 { get; set; }
		public decimal? lost_revenue_pretrial_dispute_plan_2 { get; set; }
		public decimal? lost_revenue_pretrial_dispute_plan_3 { get; set; }
		public decimal? lost_revenue_disagree_plan_1 { get; set; }
		public decimal? lost_revenue_disagree_plan_2 { get; set; }
		public decimal? lost_revenue_disagree_plan_3 { get; set; }
		public decimal? cost_notaccounted_reg_body_plan_1 { get; set; }
		public decimal? cost_notaccounted_reg_body_plan_2 { get; set; }
		public decimal? cost_notaccounted_reg_body_plan_3 { get; set; }
		public decimal? cost_grow_price_product_plan_1 { get; set; }
		public decimal? cost_grow_price_product_plan_2 { get; set; }
		public decimal? cost_grow_price_product_plan_3 { get; set; }
		public decimal? cost_service_borrowed_funds_plan_1 { get; set; }
		public decimal? cost_service_borrowed_funds_plan_2 { get; set; }
		public decimal? cost_service_borrowed_funds_plan_3 { get; set; }
		public decimal? lost_revenue_from_connect_obj_plan_1 { get; set; }
		public decimal? lost_revenue_from_connect_obj_plan_2 { get; set; }
		public decimal? lost_revenue_from_connect_obj_plan_3 { get; set; }
		public decimal? lost_revenue_other_plan_1 { get; set; }
		public decimal? lost_revenue_other_plan_2 { get; set; }
		public decimal? lost_revenue_other_plan_3 { get; set; }
		
	}

	[Keyless]
	public class TZExcessFundsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? excess_funds_plan_1 { get; set; }
		public decimal? excess_funds_plan_2 { get; set; }
		public decimal? excess_funds_plan_3 { get; set; }

	}

	[Keyless]
	public class TZCapitalInvestmentViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? capital_investment_all_plan_1 { get; set; }
		public decimal? capital_investment_all_plan_2 { get; set; }
		public decimal? capital_investment_all_plan_3 { get; set; }
		public decimal? volume_invest_prod_electroenergy_plan_1 { get; set; }
		public decimal? volume_invest_prod_electroenergy_plan_2 { get; set; }
		public decimal? volume_invest_prod_electroenergy_plan_3 { get; set; }
		public decimal? volume_invest_prod_heatenergy_plan_1 { get; set; }
		public decimal? volume_invest_prod_heatenergy_plan_2 { get; set; }
		public decimal? volume_invest_prod_heatenergy_plan_3 { get; set; }
		public decimal? volume_invest_prod_heatcarrier_plan_1 { get; set; }
		public decimal? volume_invest_prod_heatcarrier_plan_2 { get; set; }
		public decimal? volume_invest_prod_heatcarrier_plan_3 { get; set; }
		public decimal? volume_invest_transfer_heatenergy_plan_1 { get; set; }
		public decimal? volume_invest_transfer_heatenergy_plan_2 { get; set; }
		public decimal? volume_invest_transfer_heatenergy_plan_3 { get; set; }
		public decimal? volume_invest_other_plan_1 { get; set; }
		public decimal? volume_invest_other_plan_2 { get; set; }
		public decimal? volume_invest_other_plan_3 { get; set; }
		public decimal? finance_capital_investment_all_plan_1 { get; set; }
		public decimal? finance_capital_investment_all_plan_2 { get; set; }
		public decimal? finance_capital_investment_all_plan_3 { get; set; }
		public decimal? amortisation_deduction_fonds_plan_1 { get; set; }
		public decimal? amortisation_deduction_fonds_plan_2 { get; set; }
		public decimal? amortisation_deduction_fonds_plan_3 { get; set; }
		public decimal? amortisation_deduction_nonmaterial_active_plan_1 { get; set; }
		public decimal? amortisation_deduction_nonmaterial_active_plan_2 { get; set; }
		public decimal? amortisation_deduction_nonmaterial_active_plan_3 { get; set; }
		public decimal? finance_by_nonused_funds_plan_1 { get; set; }
		public decimal? finance_by_nonused_funds_plan_2 { get; set; }
		public decimal? finance_by_nonused_funds_plan_3 { get; set; }
		public decimal? finance_by_profit_plan_1 { get; set; }
		public decimal? finance_by_profit_plan_2 { get; set; }
		public decimal? finance_by_profit_plan_3 { get; set; }
		public decimal? finance_by_federal_budjet_plan_1 { get; set; }
		public decimal? finance_by_federal_budjet_plan_2 { get; set; }
		public decimal? finance_by_federal_budjet_plan_3 { get; set; }
		public decimal? finance_by_local_budjet_plan_1 { get; set; }
		public decimal? finance_by_local_budjet_plan_2 { get; set; }
		public decimal? finance_by_local_budjet_plan_3 { get; set; }
		public decimal? finance_by_regional_budjet_plan_1 { get; set; }
		public decimal? finance_by_regional_budjet_plan_2 { get; set; }
		public decimal? finance_by_regional_budjet_plan_3 { get; set; }
		public decimal? finance_by_other_sources_plan_1 { get; set; }
		public decimal? finance_by_other_sources_plan_2 { get; set; }
		public decimal? finance_by_other_sources_plan_3 { get; set; }
		public decimal? finance_by_credits_plan_1 { get; set; }
		public decimal? finance_by_credits_plan_2 { get; set; }
		public decimal? finance_by_credits_plan_3 { get; set; }
		public decimal? finance_by_realize_securities_plan_1 { get; set; }
		public decimal? finance_by_realize_securities_plan_2 { get; set; }
		public decimal? finance_by_realize_securities_plan_3 { get; set; }
		public decimal? finance_by_connection_charge_plan_1 { get; set; }
		public decimal? finance_by_connection_charge_plan_2 { get; set; }
		public decimal? finance_by_connection_charge_plan_3 { get; set; }
	}

	[Keyless]
	public class TZCrossSubsidyViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cross_subsidy_all_plan_1 { get; set; }
		public decimal? cross_subsidy_all_plan_2 { get; set; }
		public decimal? cross_subsidy_all_plan_3 { get; set; }
		public decimal? cross_subs_between_activ_plan_1 { get; set; }
		public decimal? cross_subs_between_activ_plan_2 { get; set; }
		public decimal? cross_subs_between_activ_plan_3 { get; set; }
		public decimal? cross_subs_between_consumer_plan_1 { get; set; }
		public decimal? cross_subs_between_consumer_plan_2 { get; set; }
		public decimal? cross_subs_between_consumer_plan_3 { get; set; }
		
	}

	[Keyless]
	public class TZEconomyCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? economy_operation_cost_plan_1 { get; set; }
		public decimal? economy_operation_cost_plan_2 { get; set; }
		public decimal? economy_operation_cost_plan_3 { get; set; }
		public decimal? total_economy_noncontrol_cost_plan_1 { get; set; }
		public decimal? total_economy_noncontrol_cost_plan_2 { get; set; }
		public decimal? total_economy_noncontrol_cost_plan_3 { get; set; }
		public decimal? economy_consump_heatenergy_plan_1 { get; set; }
		public decimal? economy_consump_heatenergy_plan_2 { get; set; }
		public decimal? economy_consump_heatenergy_plan_3 { get; set; }
		public decimal? economy_consump_electroenergy_plan_1 { get; set; }
		public decimal? economy_consump_electroenergy_plan_2 { get; set; }
		public decimal? economy_consump_electroenergy_plan_3 { get; set; }
		public decimal? economy_consump_fuel_plan_1 { get; set; }
		public decimal? economy_consump_fuel_plan_2 { get; set; }
		public decimal? economy_consump_fuel_plan_3 { get; set; }
		public decimal? economy_consump_coldwater_plan_1 { get; set; }
		public decimal? economy_consump_coldwater_plan_2 { get; set; }
		public decimal? economy_consump_coldwater_plan_3 { get; set; }
		public decimal? economy_consump_waterdisp_plan_1 { get; set; }
		public decimal? economy_consump_waterdisp_plan_2 { get; set; }
		public decimal? economy_consump_waterdisp_plan_3 { get; set; }
		public decimal? economy_consump_heat_plan_1 { get; set; }
		public decimal? economy_consump_heat_plan_2 { get; set; }
		public decimal? economy_consump_heat_plan_3 { get; set; }
		public decimal? economy_consump_hotwater_plan_1 { get; set; }
		public decimal? economy_consump_hotwater_plan_2 { get; set; }
		public decimal? economy_consump_hotwater_plan_3 { get; set; }
		public decimal? increase_economy_operation_cost_plan_1 { get; set; }
		public decimal? increase_economy_operation_cost_plan_2 { get; set; }
		public decimal? increase_economy_operation_cost_plan_3 { get; set; }
		public decimal? increase_economy_consump_heatenergy_plan_1 { get; set; }
		public decimal? increase_economy_consump_heatenergy_plan_2 { get; set; }
		public decimal? increase_economy_consump_heatenergy_plan_3 { get; set; }
		public decimal? increase_economy_consump_electroenergy_plan_1 { get; set; }
		public decimal? increase_economy_consump_electroenergy_plan_2 { get; set; }
		public decimal? increase_economy_consump_electroenergy_plan_3 { get; set; }
		public decimal? increase_economy_consump_fuel_plan_1 { get; set; }
		public decimal? increase_economy_consump_fuel_plan_2 { get; set; }
		public decimal? increase_economy_consump_fuel_plan_3 { get; set; }
		public decimal? increase_economy_consump_coldwater_plan_1 { get; set; }
		public decimal? increase_economy_consump_coldwater_plan_2 { get; set; }
		public decimal? increase_economy_consump_coldwater_plan_3 { get; set; }
		public decimal? increase_economy_consump_waterdisp_plan_1 { get; set; }
		public decimal? increase_economy_consump_waterdisp_plan_2 { get; set; }
		public decimal? increase_economy_consump_waterdisp_plan_3 { get; set; }
		public decimal? increase_economy_consump_heat_plan_1 { get; set; }
		public decimal? increase_economy_consump_heat_plan_2 { get; set; }
		public decimal? increase_economy_consump_heat_plan_3 { get; set; }
		public decimal? increase_economy_consump_hotwater_plan_1 { get; set; }
		public decimal? increase_economy_consump_hotwater_plan_2 { get; set; }
		public decimal? increase_economy_consump_hotwater_plan_3 { get; set; }

	}

	[Keyless]
	public class TZIndexesNVVViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? index_effective_cost_plan_1 { get; set; }
		public decimal? index_effective_cost_plan_2 { get; set; }
		public decimal? index_effective_cost_plan_3 { get; set; }
		public decimal? index_consumers_price_plan_1 { get; set; }
		public decimal? index_consumers_price_plan_2 { get; set; }
		public decimal? index_consumers_price_plan_3 { get; set; }
		public decimal? index_count_actives_plan_1 { get; set; }
		public decimal? index_count_actives_plan_2 { get; set; }
		public decimal? index_count_actives_plan_3 { get; set; }

	}

	[Keyless]
	public class TZMethodIndexationViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? total_correction_nvv_mA_plan_1 { get; set; }
		public decimal? total_correction_nvv_mA_plan_2 { get; set; }
		public decimal? total_correction_nvv_mA_plan_3 { get; set; }
		public decimal? result_activity_before_reg_price_plan_1 { get; set; }
		public decimal? result_activity_before_reg_price_plan_2 { get; set; }
		public decimal? result_activity_before_reg_price_plan_3 { get; set; }
		public decimal? economically_justified_costs_plan_1 { get; set; }
		public decimal? economically_justified_costs_plan_2 { get; set; }
		public decimal? economically_justified_costs_plan_3 { get; set; }
		public decimal? profit_regulated_organisation_plan_1 { get; set; }
		public decimal? profit_regulated_organisation_plan_2 { get; set; }
		public decimal? profit_regulated_organisation_plan_3 { get; set; }
		public decimal? economy_consump_resources_plan_1 { get; set; }
		public decimal? economy_consump_resources_plan_2 { get; set; }
		public decimal? economy_consump_resources_plan_3 { get; set; }
		public decimal? total_correction_nvv_plan_1 { get; set; }
		public decimal? total_correction_nvv_plan_2 { get; set; }
		public decimal? total_correction_nvv_plan_3 { get; set; }
		public decimal? correct_accounting_deviation_plan_1 { get; set; }
		public decimal? correct_accounting_deviation_plan_2 { get; set; }
		public decimal? correct_accounting_deviation_plan_3 { get; set; }
		public decimal? correct_safety_quality_product_plan_1 { get; set; }
		public decimal? correct_safety_quality_product_plan_2 { get; set; }
		public decimal? correct_safety_quality_product_plan_3 { get; set; }
		public decimal? correct_nvv_change_invest_prog_plan_1 { get; set; }
		public decimal? correct_nvv_change_invest_prog_plan_2 { get; set; }
		public decimal? correct_nvv_change_invest_prog_plan_3 { get; set; }
		public decimal? correct_subj_accounting_nvv_plan_1 { get; set; }
		public decimal? correct_subj_accounting_nvv_plan_2 { get; set; }
		public decimal? correct_subj_accounting_nvv_plan_3 { get; set; }

	}

	[Keyless]
	public class TZMethodProfitInvestViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? total_correction_nvv_mB_plan_1 { get; set; }
		public decimal? total_correction_nvv_mB_plan_2 { get; set; }
		public decimal? total_correction_nvv_mB_plan_3 { get; set; }
		public decimal? return_capital_plan_1 { get; set; }
		public decimal? return_capital_plan_2 { get; set; }
		public decimal? return_capital_plan_3 { get; set; }
		public decimal? full_value_invest_plan_1 { get; set; }
		public decimal? full_value_invest_plan_2 { get; set; }
		public decimal? full_value_invest_plan_3 { get; set; }
		public decimal? return_period_invest_plan_1 { get; set; }
		public decimal? return_period_invest_plan_2 { get; set; }
		public decimal? return_period_invest_plan_3 { get; set; }
		public decimal? profit_on_invest_capital_plan_1 { get; set; }
		public decimal? profit_on_invest_capital_plan_2 { get; set; }
		public decimal? profit_on_invest_capital_plan_3 { get; set; }
		public decimal? initial_amount_invest_plan_1 { get; set; }
		public decimal? initial_amount_invest_plan_2 { get; set; }
		public decimal? initial_amount_invest_plan_3 { get; set; }
		public decimal? profit_initial_amount_invest_plan_1 { get; set; }
		public decimal? profit_initial_amount_invest_plan_2 { get; set; }
		public decimal? profit_initial_amount_invest_plan_3 { get; set; }
		public decimal? base_invest_plan_1 { get; set; }
		public decimal? base_invest_plan_2 { get; set; }
		public decimal? base_invest_plan_3 { get; set; }
		public decimal? pure_working_capital_plan_1 { get; set; }
		public decimal? pure_working_capital_plan_2 { get; set; }
		public decimal? pure_working_capital_plan_3 { get; set; }
		public decimal? norm_pure_working_capital_plan_1 { get; set; }
		public decimal? norm_pure_working_capital_plan_2 { get; set; }
		public decimal? norm_pure_working_capital_plan_3 { get; set; }
		public decimal? norm_profit_plan_1 { get; set; }
		public decimal? norm_profit_plan_2 { get; set; }
		public decimal? norm_profit_plan_3 { get; set; }
		public decimal? norm_profit_new_capital_plan_1 { get; set; }
		public decimal? norm_profit_new_capital_plan_2 { get; set; }
		public decimal? norm_profit_new_capital_plan_3 { get; set; }
		public decimal? norm_profit_old_capital_plan_1 { get; set; }
		public decimal? norm_profit_old_capital_plan_2 { get; set; }
		public decimal? norm_profit_old_capital_plan_3 { get; set; }
		public decimal? amortization_plan_1 { get; set; }
		public decimal? amortization_plan_2 { get; set; }
		public decimal? amortization_plan_3 { get; set; }

	}

	[Keyless]
	public class TZMethodAnaloguesComparisonViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? baseline_cost_reg_managed_plan_1 { get; set; }
		public decimal? baseline_cost_reg_managed_plan_2 { get; set; }
		public decimal? baseline_cost_reg_managed_plan_3 { get; set; }
		public decimal? correct_nvv_accounting_deviation_plan_1 { get; set; }
		public decimal? correct_nvv_accounting_deviation_plan_2 { get; set; }
		public decimal? correct_nvv_accounting_deviation_plan_3 { get; set; }

	}
}