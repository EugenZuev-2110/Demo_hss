using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Areas.TSO.Models
{
    [Keyless]
    public class TZCalcCostsDataViewModel
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
        public decimal costs_all_onrealize_fact { get; set; }
        public decimal costs_all_onrealize_plan { get; set; }
        public decimal costs_all_fact { get; set; }
        public decimal costs_all_plan { get; set; }
        public decimal costs_all_energy_resources_fact { get; set; }
        public decimal price_all_fuel_fact { get; set; }
        public decimal volume_all_fuel_fact { get; set; }
        public decimal avg_price_all_fuel_fact { get; set; }
        public string? fuels_fact { get; set; }
        public decimal spec_consump_fuel_calc_fact { get; set; }
        public decimal norm_consump_fuel_plan { get; set; }
        public decimal costs_all_elenergy_fact { get; set; }
        public decimal costs_low_voltage_fact { get; set; }
        public decimal volume_low_voltage_electricity_fact { get; set; }
        public decimal price_low_voltage_electricity_fact { get; set; }
        public decimal volume_low_voltage_el_power_fact { get; set; }
        public decimal price_low_voltage_el_power_fact { get; set; }
        public decimal costs_medium1_voltage_fact { get; set; }
        public decimal volume_medium1_voltage_electricity_fact { get; set; }
        public decimal price_medium1_voltage_electricity_fact { get; set; }
        public decimal volume_medium1_voltage_el_power_fact { get; set; }
        public decimal price_medium1_voltage_el_power_fact { get; set; }
        public decimal costs_medium2_voltage_fact { get; set; }
        public decimal volume_medium2_voltage_electricity_fact { get; set; }
        public decimal price_medium2_voltage_electricity_fact { get; set; }
        public decimal volume_medium2_voltage_el_power_fact { get; set; }
        public decimal price_medium2_voltage_el_power_fact { get; set; }
        public decimal costs_high_voltage_fact { get; set; }
        public decimal volume_high_voltage_electricity_fact { get; set; }
        public decimal price_high_voltage_electricity_fact { get; set; }
        public decimal volume_high_voltage_el_power_fact { get; set; }
        public decimal price_high_voltage_el_power_fact { get; set; }
        public decimal costs_heat_energy_all_fact { get; set; }
        public decimal costs_buy_he_collect_fact { get; set; }
        public decimal volume_buy_he_collect_fact { get; set; }
        public decimal price_buy_he_collect_fact { get; set; }
        public decimal costs_buy_he_network_fact { get; set; }
        public decimal volume_buy_he_network_fact { get; set; }
        public decimal price_buy_he_network_fact { get; set; }
        public decimal costs_heattrans_service_fact { get; set; }
        public decimal tso_transfer_volume_fact { get; set; }
        public decimal price_heattrans_fact { get; set; }
        public decimal costs_cold_water_fact { get; set; }
        public decimal volume_cold_water_fact { get; set; }
        public decimal price_cold_water_fact { get; set; }
        public decimal costs_water_dispos_fact { get; set; }
        public decimal volume_water_dispos_fact { get; set; }
        public decimal price_water_dispos_fact { get; set; }
        public decimal costs_heatcarrier_fact { get; set; }
        public decimal volume_heatcarrier_fact { get; set; }
        public decimal price_heatcarrier_fact { get; set; }
        public decimal costs_fuel_reserve_fact { get; set; }
        public decimal costs_all_operating_fact { get; set; }
        public decimal costs_all_salary_fact { get; set; }
        public decimal avg_count_workers_fact { get; set; }
        public decimal avg_salary_fact { get; set; }
        public decimal cost_materials_all_fact { get; set; }
        public decimal cost_org_service_all_fact { get; set; }
		public decimal costs_other_org_product_fact { get; set; }
		public decimal cost_org_other_service_fact { get; set; }
		public decimal cost_repair_fact { get; set; }
		public decimal cost_decomission_fact { get; set; }
		public decimal costs_other_operatings_fact { get; set; }
		public decimal costs_all_uncontroll_fact { get; set; }
		public decimal procent_social_deduction_fact { get; set; }
		public decimal cost_social_deduction_fact { get; set; }
		public decimal costs_amortization_fact { get; set; }
		public decimal cost_req_pay_all_fact { get; set; }
		public decimal cost_reg_org_service_fact { get; set; }
		public decimal cost_income_tax_fact { get; set; }
		public decimal cost_uncontrol_other_all_fact { get; set; }
		public decimal procent_costs_all_elenergy_fact { get; set; }
		public decimal procent_costs_all_operating_fact { get; set; }
		public decimal procent_costs_all_uncontroll_fact { get; set; }

		public decimal costs_all_energy_resources_plan { get; set; }
		public decimal price_all_fuel_plan { get; set; }
		public decimal volume_all_fuel_plan { get; set; }
		public decimal avg_price_all_fuel_plan { get; set; }
		public string? fuels_plan { get; set; }
		public decimal spec_consump_fuel_calc_plan { get; set; }
		public decimal costs_all_elenergy_plan { get; set; }
		public decimal costs_low_voltage_plan { get; set; }
		public decimal volume_low_voltage_electricity_plan { get; set; }
		public decimal price_low_voltage_electricity_plan { get; set; }
		public decimal volume_low_voltage_el_power_plan { get; set; }
		public decimal price_low_voltage_el_power_plan { get; set; }
		public decimal costs_medium1_voltage_plan { get; set; }
		public decimal volume_medium1_voltage_electricity_plan { get; set; }
		public decimal price_medium1_voltage_electricity_plan { get; set; }
		public decimal volume_medium1_voltage_el_power_plan { get; set; }
		public decimal price_medium1_voltage_el_power_plan { get; set; }
		public decimal costs_medium2_voltage_plan { get; set; }
		public decimal volume_medium2_voltage_electricity_plan { get; set; }
		public decimal price_medium2_voltage_electricity_plan { get; set; }
		public decimal volume_medium2_voltage_el_power_plan { get; set; }
		public decimal price_medium2_voltage_el_power_plan { get; set; }
		public decimal costs_high_voltage_plan { get; set; }
		public decimal volume_high_voltage_electricity_plan { get; set; }
		public decimal price_high_voltage_electricity_plan { get; set; }
		public decimal volume_high_voltage_el_power_plan { get; set; }
		public decimal price_high_voltage_el_power_plan { get; set; }
		public decimal costs_heat_energy_all_plan { get; set; }
		public decimal costs_buy_he_collect_plan { get; set; }
		public decimal volume_buy_he_collect_plan { get; set; }
		public decimal price_buy_he_collect_plan { get; set; }
		public decimal costs_buy_he_network_plan { get; set; }
		public decimal volume_buy_he_network_plan { get; set; }
		public decimal price_buy_he_network_plan { get; set; }
		public decimal costs_heattrans_service_plan { get; set; }
		public decimal tso_transfer_volume_plan { get; set; }
		public decimal price_heattrans_plan { get; set; }
		public decimal costs_cold_water_plan { get; set; }
		public decimal volume_cold_water_plan { get; set; }
		public decimal price_cold_water_plan { get; set; }
		public decimal costs_water_dispos_plan { get; set; }
		public decimal volume_water_dispos_plan { get; set; }
		public decimal price_water_dispos_plan { get; set; }
		public decimal costs_heatcarrier_plan { get; set; }
		public decimal volume_heatcarrier_plan { get; set; }
		public decimal price_heatcarrier_plan { get; set; }
		public decimal costs_fuel_reserve_plan { get; set; }
		public decimal costs_all_operating_plan { get; set; }
		public decimal costs_all_salary_plan { get; set; }
		public decimal avg_count_workers_plan { get; set; }
		public decimal avg_salary_plan { get; set; }
		public decimal cost_materials_all_plan { get; set; }
		public decimal cost_org_service_all_plan { get; set; }
		public decimal costs_other_org_product_plan { get; set; }
		public decimal cost_org_other_service_plan { get; set; }
		public decimal cost_repair_plan { get; set; }
		public decimal cost_decomission_plan { get; set; }
		public decimal costs_other_operatings_plan { get; set; }
		public decimal costs_all_uncontroll_plan { get; set; }
		public decimal procent_social_deduction_plan { get; set; }
		public decimal cost_social_deduction_plan { get; set; }
		public decimal costs_amortization_plan { get; set; }
		public decimal cost_req_pay_all_plan { get; set; }
		public decimal cost_reg_org_service_plan { get; set; }
		public decimal cost_income_tax_plan { get; set; }
		public decimal cost_uncontrol_other_all_plan { get; set; }
		public decimal procent_costs_all_elenergy_plan { get; set; }
		public decimal procent_costs_all_operating_plan { get; set; }
		public decimal procent_costs_all_uncontroll_plan { get; set; }

	}

	[Keyless]
	public class TZCalcCostsOnTransportDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public string? unom_tz { get; set; }
		public string? territory { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public string? activity_types { get; set; }
		public decimal costs_all_fact { get; set; }
		public decimal costs_all_plan { get; set; }
		
		public decimal costs_all_operating_fact { get; set; }
		public decimal costs_all_salary_fact { get; set; }
		public decimal avg_count_workers_fact { get; set; }
		public decimal avg_salary_fact { get; set; }
		public decimal cost_materials_all_fact { get; set; }
		public decimal cost_org_service_all_fact { get; set; }
		public decimal costs_other_org_product_fact { get; set; }
		public decimal cost_org_other_service_fact { get; set; }
		public decimal cost_repair_fact { get; set; }
		public decimal cost_decomission_fact { get; set; }
		public decimal costs_other_operatings_fact { get; set; }
		public decimal costs_all_uncontroll_fact { get; set; }
		public decimal procent_social_deduction_fact { get; set; }
		public decimal cost_social_deduction_fact { get; set; }
		public decimal costs_amortization_fact { get; set; }
		public decimal cost_req_pay_all_fact { get; set; }
		public decimal cost_reg_org_service_fact { get; set; }
		public decimal cost_income_tax_fact { get; set; }
		public decimal cost_uncontrol_other_all_fact { get; set; }
		public decimal procent_costs_all_operating_fact { get; set; }
		public decimal procent_costs_all_uncontroll_fact { get; set; }

		public decimal costs_all_operating_plan { get; set; }
		public decimal costs_all_salary_plan { get; set; }
		public decimal avg_count_workers_plan { get; set; }
		public decimal avg_salary_plan { get; set; }
		public decimal cost_materials_all_plan { get; set; }
		public decimal cost_org_service_all_plan { get; set; }
		public decimal costs_other_org_product_plan { get; set; }
		public decimal cost_org_other_service_plan { get; set; }
		public decimal cost_repair_plan { get; set; }
		public decimal cost_decomission_plan { get; set; }
		public decimal costs_other_operatings_plan { get; set; }
		public decimal costs_all_uncontroll_plan { get; set; }
		public decimal procent_social_deduction_plan { get; set; }
		public decimal cost_social_deduction_plan { get; set; }
		public decimal costs_amortization_plan { get; set; }
		public decimal cost_req_pay_all_plan { get; set; }
		public decimal cost_reg_org_service_plan { get; set; }
		public decimal cost_income_tax_plan { get; set; }
		public decimal cost_uncontrol_other_all_plan { get; set; }
		public decimal procent_costs_all_operating_plan { get; set; }
		public decimal procent_costs_all_uncontroll_plan { get; set; }

	}

	[Keyless]
	public class TZCalcCostsOnProductionDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public string? unom_tz { get; set; }
		public string? territory { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public string? activity_types { get; set; }
		public decimal costs_all_fact { get; set; }
		public decimal costs_all_plan { get; set; }
		public decimal costs_all_energy_resources_fact { get; set; }
		public decimal price_all_fuel_fact { get; set; }
		public decimal volume_all_fuel_fact { get; set; }
		public decimal avg_price_all_fuel_fact { get; set; }
		public string? fuels_fact { get; set; }
		public decimal spec_consump_fuel_calc_fact { get; set; }
		public decimal norm_consump_fuel_plan { get; set; }
		public decimal costs_all_elenergy_fact { get; set; }
		public decimal costs_low_voltage_fact { get; set; }
		public decimal volume_low_voltage_electricity_fact { get; set; }
		public decimal price_low_voltage_electricity_fact { get; set; }
		public decimal volume_low_voltage_el_power_fact { get; set; }
		public decimal price_low_voltage_el_power_fact { get; set; }
		public decimal costs_medium1_voltage_fact { get; set; }
		public decimal volume_medium1_voltage_electricity_fact { get; set; }
		public decimal price_medium1_voltage_electricity_fact { get; set; }
		public decimal volume_medium1_voltage_el_power_fact { get; set; }
		public decimal price_medium1_voltage_el_power_fact { get; set; }
		public decimal costs_medium2_voltage_fact { get; set; }
		public decimal volume_medium2_voltage_electricity_fact { get; set; }
		public decimal price_medium2_voltage_electricity_fact { get; set; }
		public decimal volume_medium2_voltage_el_power_fact { get; set; }
		public decimal price_medium2_voltage_el_power_fact { get; set; }
		public decimal costs_high_voltage_fact { get; set; }
		public decimal volume_high_voltage_electricity_fact { get; set; }
		public decimal price_high_voltage_electricity_fact { get; set; }
		public decimal volume_high_voltage_el_power_fact { get; set; }
		public decimal price_high_voltage_el_power_fact { get; set; }
		public decimal costs_heat_energy_all_fact { get; set; }
		public decimal costs_buy_he_collect_fact { get; set; }
		public decimal volume_buy_he_collect_fact { get; set; }
		public decimal price_buy_he_collect_fact { get; set; }
		public decimal costs_buy_he_network_fact { get; set; }
		public decimal volume_buy_he_network_fact { get; set; }
		public decimal price_buy_he_network_fact { get; set; }
		public decimal costs_heattrans_service_fact { get; set; }
		public decimal tso_transfer_volume_fact { get; set; }
		public decimal price_heattrans_fact { get; set; }
		public decimal costs_cold_water_fact { get; set; }
		public decimal volume_cold_water_fact { get; set; }
		public decimal price_cold_water_fact { get; set; }
		public decimal costs_water_dispos_fact { get; set; }
		public decimal volume_water_dispos_fact { get; set; }
		public decimal price_water_dispos_fact { get; set; }
		public decimal costs_heatcarrier_fact { get; set; }
		public decimal volume_heatcarrier_fact { get; set; }
		public decimal price_heatcarrier_fact { get; set; }
		public decimal costs_fuel_reserve_fact { get; set; }
		public decimal costs_all_operating_fact { get; set; }
		public decimal costs_all_salary_fact { get; set; }
		public decimal avg_count_workers_fact { get; set; }
		public decimal avg_salary_fact { get; set; }
		public decimal cost_materials_all_fact { get; set; }
		public decimal cost_org_service_all_fact { get; set; }
		public decimal costs_other_org_product_fact { get; set; }
		public decimal cost_org_other_service_fact { get; set; }
		public decimal cost_repair_fact { get; set; }
		public decimal cost_decomission_fact { get; set; }
		public decimal costs_other_operatings_fact { get; set; }
		public decimal costs_all_uncontroll_fact { get; set; }
		public decimal procent_social_deduction_fact { get; set; }
		public decimal cost_social_deduction_fact { get; set; }
		public decimal costs_amortization_fact { get; set; }
		public decimal cost_req_pay_all_fact { get; set; }
		public decimal cost_reg_org_service_fact { get; set; }
		public decimal cost_income_tax_fact { get; set; }
		public decimal cost_uncontrol_other_all_fact { get; set; }
		public decimal procent_costs_all_elenergy_fact { get; set; }
		public decimal procent_costs_all_operating_fact { get; set; }
		public decimal procent_costs_all_uncontroll_fact { get; set; }

		public decimal costs_all_energy_resources_plan { get; set; }
		public decimal price_all_fuel_plan { get; set; }
		public decimal volume_all_fuel_plan { get; set; }
		public decimal avg_price_all_fuel_plan { get; set; }
		public string? fuels_plan { get; set; }
		public decimal spec_consump_fuel_calc_plan { get; set; }
		public decimal costs_all_elenergy_plan { get; set; }
		public decimal costs_low_voltage_plan { get; set; }
		public decimal volume_low_voltage_electricity_plan { get; set; }
		public decimal price_low_voltage_electricity_plan { get; set; }
		public decimal volume_low_voltage_el_power_plan { get; set; }
		public decimal price_low_voltage_el_power_plan { get; set; }
		public decimal costs_medium1_voltage_plan { get; set; }
		public decimal volume_medium1_voltage_electricity_plan { get; set; }
		public decimal price_medium1_voltage_electricity_plan { get; set; }
		public decimal volume_medium1_voltage_el_power_plan { get; set; }
		public decimal price_medium1_voltage_el_power_plan { get; set; }
		public decimal costs_medium2_voltage_plan { get; set; }
		public decimal volume_medium2_voltage_electricity_plan { get; set; }
		public decimal price_medium2_voltage_electricity_plan { get; set; }
		public decimal volume_medium2_voltage_el_power_plan { get; set; }
		public decimal price_medium2_voltage_el_power_plan { get; set; }
		public decimal costs_high_voltage_plan { get; set; }
		public decimal volume_high_voltage_electricity_plan { get; set; }
		public decimal price_high_voltage_electricity_plan { get; set; }
		public decimal volume_high_voltage_el_power_plan { get; set; }
		public decimal price_high_voltage_el_power_plan { get; set; }
		public decimal costs_heat_energy_all_plan { get; set; }
		public decimal costs_buy_he_collect_plan { get; set; }
		public decimal volume_buy_he_collect_plan { get; set; }
		public decimal price_buy_he_collect_plan { get; set; }
		public decimal costs_buy_he_network_plan { get; set; }
		public decimal volume_buy_he_network_plan { get; set; }
		public decimal price_buy_he_network_plan { get; set; }
		public decimal costs_heattrans_service_plan { get; set; }
		public decimal tso_transfer_volume_plan { get; set; }
		public decimal price_heattrans_plan { get; set; }
		public decimal costs_cold_water_plan { get; set; }
		public decimal volume_cold_water_plan { get; set; }
		public decimal price_cold_water_plan { get; set; }
		public decimal costs_water_dispos_plan { get; set; }
		public decimal volume_water_dispos_plan { get; set; }
		public decimal price_water_dispos_plan { get; set; }
		public decimal costs_heatcarrier_plan { get; set; }
		public decimal volume_heatcarrier_plan { get; set; }
		public decimal price_heatcarrier_plan { get; set; }
		public decimal costs_fuel_reserve_plan { get; set; }
		public decimal costs_all_operating_plan { get; set; }
		public decimal costs_all_salary_plan { get; set; }
		public decimal avg_count_workers_plan { get; set; }
		public decimal avg_salary_plan { get; set; }
		public decimal cost_materials_all_plan { get; set; }
		public decimal cost_org_service_all_plan { get; set; }
		public decimal costs_other_org_product_plan { get; set; }
		public decimal cost_org_other_service_plan { get; set; }
		public decimal cost_repair_plan { get; set; }
		public decimal cost_decomission_plan { get; set; }
		public decimal costs_other_operatings_plan { get; set; }
		public decimal costs_all_uncontroll_plan { get; set; }
		public decimal procent_social_deduction_plan { get; set; }
		public decimal cost_social_deduction_plan { get; set; }
		public decimal costs_amortization_plan { get; set; }
		public decimal cost_req_pay_all_plan { get; set; }
		public decimal cost_reg_org_service_plan { get; set; }
		public decimal cost_income_tax_plan { get; set; }
		public decimal cost_uncontrol_other_all_plan { get; set; }
		public decimal procent_costs_all_elenergy_plan { get; set; }
		public decimal procent_costs_all_operating_plan { get; set; }
		public decimal procent_costs_all_uncontroll_plan { get; set; }

	}

	[Keyless]
    public class TZOneCalcCostsDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public string? tz_territory { get; set; }
		public string combine_prod_more25 { get; set; }
		public string combine_prod_less25 { get; set; }
		public string not_combine_prod { get; set; }
		public string transfer { get; set; }
		public string sale { get; set; }
		public string delivery_contract { get; set; }
		public short? tariff_differentiation { get; set; }
	}

	[Keyless]
	public class TZCalcAllCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal costs_all { get; set; }
		public decimal costs_all_energy_res { get; set; }
		public decimal costs_all_energy_res_percent { get; set; }
		public decimal costs_operating { get; set; }
		public decimal costs_operating_percent { get; set; }
		public decimal costs_non_control { get; set; }
		public decimal costs_non_control_percent { get; set; }
		public decimal amortization { get; set; }
		public decimal costs_on_realize { get; set; }
		public decimal useful_release_prod_all { get; set; }
		public decimal useful_release_on_realize { get; set; }
		public decimal coef_on_realize { get; set; }

	}

	[Keyless]
	public class TZCalcCostsDistributionViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal coef_cost_on_production_fact_3 { get; set; }
		public decimal coef_amortiz_on_production_fact_3 { get; set; }
		public decimal coef_cost_on_transport_fact_3 { get; set; }
		public decimal coef_amortiz_on_transport_fact_3 { get; set; }
		public decimal coef_cost_on_production_plan_3 { get; set; }
		public decimal coef_amortiz_on_production_plan_3 { get; set; }
		public decimal coef_cost_on_transport_plan_3 { get; set; }
		public decimal coef_amortiz_on_transport_plan_3 { get; set; }
		public decimal costs_onproduction_all_fact_1 { get; set; }
		public decimal costs_onproduction_all_fact_2 { get; set; }
		public decimal costs_onproduction_all_fact_3 { get; set; }
		public decimal costs_onproduction_all_plan_1 { get; set; }
		public decimal costs_onproduction_all_plan_2 { get; set; }
		public decimal costs_onproduction_all_plan_3 { get; set; }
		public decimal costs_ontransport_all_fact_1 { get; set; }
		public decimal costs_ontransport_all_fact_2 { get; set; }
		public decimal costs_ontransport_all_fact_3 { get; set; }
		public decimal costs_ontransport_all_plan_1 { get; set; }
		public decimal costs_ontransport_all_plan_2 { get; set; }
		public decimal costs_ontransport_all_plan_3 { get; set; }
		public decimal costs_energy_resources_fact_1 { get; set; }
		public decimal costs_energy_resources_fact_2 { get; set; }
		public decimal costs_energy_resources_fact_3 { get; set; }
		public decimal costs_energy_resources_plan_1 { get; set; }
		public decimal costs_energy_resources_plan_2 { get; set; }
		public decimal costs_energy_resources_plan_3 { get; set; }
		public decimal costs_operation_onproduction_fact_1 { get; set; }
		public decimal costs_operation_onproduction_fact_2 { get; set; }
		public decimal costs_operation_onproduction_fact_3 { get; set; }
		public decimal costs_operation_onproduction_plan_1 { get; set; }
		public decimal costs_operation_onproduction_plan_2 { get; set; }
		public decimal costs_operation_onproduction_plan_3 { get; set; }
		public decimal costs_operation_ontransport_fact_1 { get; set; }
		public decimal costs_operation_ontransport_fact_2 { get; set; }
		public decimal costs_operation_ontransport_fact_3 { get; set; }
		public decimal costs_operation_ontransport_plan_1 { get; set; }
		public decimal costs_operation_ontransport_plan_2 { get; set; }
		public decimal costs_operation_ontransport_plan_3 { get; set; }
		public decimal costs_uncontroll_onproduction_fact_1 { get; set; }
		public decimal costs_uncontroll_onproduction_fact_2 { get; set; }
		public decimal costs_uncontroll_onproduction_fact_3 { get; set; }
		public decimal costs_uncontroll_onproduction_plan_1 { get; set; }
		public decimal costs_uncontroll_onproduction_plan_2 { get; set; }
		public decimal costs_uncontroll_onproduction_plan_3 { get; set; }
		public decimal costs_uncontroll_ontransport_fact_1 { get; set; }
		public decimal costs_uncontroll_ontransport_fact_2 { get; set; }
		public decimal costs_uncontroll_ontransport_fact_3 { get; set; }
		public decimal costs_uncontroll_ontransport_plan_1 { get; set; }
		public decimal costs_uncontroll_ontransport_plan_2 { get; set; }
		public decimal costs_uncontroll_ontransport_plan_3 { get; set; }
		public decimal costs_amortization_onproduction_fact_1 { get; set; }
		public decimal costs_amortization_onproduction_fact_2 { get; set; }
		public decimal costs_amortization_onproduction_fact_3 { get; set; }
		public decimal costs_amortization_onproduction_plan_1 { get; set; }
		public decimal costs_amortization_onproduction_plan_2 { get; set; }
		public decimal costs_amortization_onproduction_plan_3 { get; set; }
		public decimal costs_amortization_ontransport_fact_1 { get; set; }
		public decimal costs_amortization_ontransport_fact_2 { get; set; }
		public decimal costs_amortization_ontransport_fact_3 { get; set; }
		public decimal costs_amortization_ontransport_plan_1 { get; set; }
		public decimal costs_amortization_ontransport_plan_2 { get; set; }
		public decimal costs_amortization_ontransport_plan_3 { get; set; }
	}

	[Keyless]
	public class TZFuelPriceDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? ghe_minus_hc_plan_1 { get; set; }
		public decimal? ghe_minus_hc_plan_2 { get; set; }
		public decimal? price_all_fuel_fact_1 { get; set; }
		public decimal? price_all_fuel_fact_2 { get; set; }
		public decimal? price_all_fuel_fact_3 { get; set; }
		public decimal? price_all_fuel_plan_1 { get; set; }
		public decimal? price_all_fuel_plan_2 { get; set; }
		public decimal? price_all_fuel_plan_3 { get; set; }
		public decimal? norm_consump_fuel_plan_1 { get; set; }
		public decimal? norm_consump_fuel_plan_2 { get; set; }
		public decimal? norm_consump_fuel_plan_3 { get; set; }
		public decimal? consump_fuel_calc_fact_1 { get; set; }
		public decimal? consump_fuel_calc_fact_2 { get; set; }
		public decimal? consump_fuel_calc_fact_3 { get; set; }
		public decimal? consump_fuel_calc_plan_1 { get; set; }
		public decimal? consump_fuel_calc_plan_2 { get; set; }
		public decimal? consump_fuel_calc_plan_3 { get; set; }
	}

	[Keyless]
	public class TZFuelReserveCostsDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_fuel_reserve_fact_1 { get; set; }
		public decimal? cost_fuel_reserve_fact_2 { get; set; }
		public decimal? cost_fuel_reserve_fact_3 { get; set; }
		public decimal? cost_fuel_reserve_plan_1 { get; set; }
		public decimal? cost_fuel_reserve_plan_2 { get; set; }
		public decimal? cost_fuel_reserve_plan_3 { get; set; }
	}

	public class TZFuelTechNeedFactDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public List<TZFuelTechNeedFactListViewModel> TZFuelTechNeedFactListViewModel { get; set; }
	}

	[Keyless]
	public class TZFuelTechNeedFactListViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public short report_period_id_old { get; set; }
		public short fuel_type_id { get; set; }
		public short fuel_type_id_old { get; set; }
		public decimal? consumption_natural_fuel_fact { get; set; }
		public decimal? consumption_natural_fuel_calc_fact { get; set; }
		//public decimal? norm_consump_fuel_calc_fact { get; set; }
		public decimal? lowestheat_combust_fuel_fact { get; set; }
		public decimal? price_fuel_fact { get; set; }
		public decimal? price_fuel_calc_fact { get; set; }
		public decimal? cost_fuel_calc_fact { get; set; }
	}

	public class TZFuelTechNeedPlanDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public List<TZFuelTechNeedPlanListViewModel> TZFuelTechNeedPlanListViewModel { get; set; }
	}

	[Keyless]
	public class TZFuelTechNeedPlanListViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public short report_period_id_old { get; set; }
		public short fuel_type_id { get; set; }
		public short fuel_type_id_old { get; set; }
		public decimal? consumption_natural_fuel_plan { get; set; }
		public decimal? consumption_natural_fuel_calc_plan { get; set; }
		public decimal? norm_consump_fuel_plan { get; set; }
		//public decimal? norm_consump_fuel_calc_plan { get; set; }
		public decimal? lowestheat_combust_fuel_plan { get; set; }
		public decimal? price_fuel_plan { get; set; }
		public decimal? price_fuel_calc_plan { get; set; }
		public decimal? cost_fuel_calc_plan { get; set; }
	}

	[Keyless]
	public class TZWaterNeedCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? costs_all_water_fact_1 { get; set; }
		public decimal? costs_all_water_fact_2 { get; set; }
		public decimal? costs_all_water_fact_3 { get; set; }
		public decimal? costs_all_water_plan_1 { get; set; }
		public decimal? costs_all_water_plan_2 { get; set; }
		public decimal? costs_all_water_plan_3 { get; set; }
		public decimal? costs_cold_water_fact_1 { get; set; }
		public decimal? costs_cold_water_fact_2 { get; set; }
		public decimal? costs_cold_water_fact_3 { get; set; }
		public decimal? costs_cold_water_plan_1 { get; set; }
		public decimal? costs_cold_water_plan_2 { get; set; }
		public decimal? costs_cold_water_plan_3 { get; set; }
		public decimal? costs_water_dispos_fact_1 { get; set; }
		public decimal? costs_water_dispos_fact_2 { get; set; }
		public decimal? costs_water_dispos_fact_3 { get; set; }
		public decimal? costs_water_dispos_plan_1 { get; set; }
		public decimal? costs_water_dispos_plan_2 { get; set; }
		public decimal? costs_water_dispos_plan_3 { get; set; }
		public decimal? volume_cold_water_fact_1 { get; set; }
		public decimal? volume_cold_water_fact_2 { get; set; }
		public decimal? volume_cold_water_fact_3 { get; set; }
		public decimal? volume_cold_water_plan_1 { get; set; }
		public decimal? volume_cold_water_plan_2 { get; set; }
		public decimal? volume_cold_water_plan_3 { get; set; }
		public decimal? price_cold_water_fact_1 { get; set; }
		public decimal? price_cold_water_fact_2 { get; set; }
		public decimal? avg_price_cold_water_fact_3 { get; set; }
		public decimal? price_cold_water_plan_1 { get; set; }
		public decimal? price_cold_water_plan_2 { get; set; }
		public decimal? avg_price_cold_water_plan_3 { get; set; }
		public decimal? volume_water_dispos_fact_1 { get; set; }
		public decimal? volume_water_dispos_fact_2 { get; set; }
		public decimal? volume_water_dispos_fact_3 { get; set; }
		public decimal? volume_water_dispos_plan_1 { get; set; }
		public decimal? volume_water_dispos_plan_2 { get; set; }
		public decimal? volume_water_dispos_plan_3 { get; set; }
		public decimal? price_water_dispos_fact_1 { get; set; }
		public decimal? price_water_dispos_fact_2 { get; set; }
		public decimal? avg_price_water_dispos_fact_3 { get; set; }
		public decimal? price_water_dispos_plan_1 { get; set; }
		public decimal? price_water_dispos_plan_2 { get; set; }
		public decimal? avg_price_water_dispos_plan_3 { get; set; }
	}

	[Keyless]
	public class TZHeatCarrierNeedCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_heatcarrier_fact_1 { get; set; }
		public decimal? cost_heatcarrier_fact_2 { get; set; }
		public decimal? cost_heatcarrier_fact_3 { get; set; }
		public decimal? cost_heatcarrier_plan_1 { get; set; }
		public decimal? cost_heatcarrier_plan_2 { get; set; }
		public decimal? cost_heatcarrier_plan_3 { get; set; }
		public decimal? volume_heatcarrier_fact_1 { get; set; }
		public decimal? volume_heatcarrier_fact_2 { get; set; }
		public decimal? volume_heatcarrier_fact_3 { get; set; }
		public decimal? volume_heatcarrier_plan_1 { get; set; }
		public decimal? volume_heatcarrier_plan_2 { get; set; }
		public decimal? volume_heatcarrier_plan_3 { get; set; }
		public decimal? price_heatcarrier_fact_1 { get; set; }
		public decimal? price_heatcarrier_fact_2 { get; set; }
		public decimal? price_heatcarrier_fact_3 { get; set; }
		public decimal? price_heatcarrier_plan_1 { get; set; }
		public decimal? price_heatcarrier_plan_2 { get; set; }
		public decimal? price_heatcarrier_plan_3 { get; set; }
	}

	[Keyless]
	public class TZEnergyNeedCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? costs_all_energy_fact_1 { get; set; }
		public decimal? costs_all_energy_fact_2 { get; set; }
		public decimal? costs_all_energy_fact_3 { get; set; }
		public decimal? costs_all_energy_plan_1 { get; set; }
		public decimal? costs_all_energy_plan_2 { get; set; }
		public decimal? costs_all_energy_plan_3 { get; set; }
		public decimal? costs_techneed_fact_1 { get; set; }
		public decimal? costs_techneed_fact_2 { get; set; }
		public decimal? costs_techneed_fact_3 { get; set; }
		public decimal? costs_techneed_plan_1 { get; set; }
		public decimal? costs_techneed_plan_2 { get; set; }
		public decimal? costs_techneed_plan_3 { get; set; }
		public decimal? costs_techneed_heat_energy_fact_1 { get; set; }
		public decimal? costs_techneed_heat_energy_fact_2 { get; set; }
		public decimal? costs_techneed_heat_energy_fact_3 { get; set; }
		public decimal? costs_techneed_heat_energy_plan_1 { get; set; }
		public decimal? costs_techneed_heat_energy_plan_2 { get; set; }
		public decimal? costs_techneed_heat_energy_plan_3 { get; set; }
		public decimal? costs_techneed_el_energy_fact_1 { get; set; }
		public decimal? costs_techneed_el_energy_fact_2 { get; set; }
		public decimal? costs_techneed_el_energy_fact_3 { get; set; }
		public decimal? costs_techneed_el_energy_plan_1 { get; set; }
		public decimal? costs_techneed_el_energy_plan_2 { get; set; }
		public decimal? costs_techneed_el_energy_plan_3 { get; set; }
		public decimal? costs_houseneed_fact_1 { get; set; }
		public decimal? costs_houseneed_fact_2 { get; set; }
		public decimal? costs_houseneed_fact_3 { get; set; }
		public decimal? costs_houseneed_plan_1 { get; set; }
		public decimal? costs_houseneed_plan_2 { get; set; }
		public decimal? costs_houseneed_plan_3 { get; set; }
		public decimal? costs_houseneed_heat_energy_fact_1 { get; set; }
		public decimal? costs_houseneed_heat_energy_fact_2 { get; set; }
		public decimal? costs_houseneed_heat_energy_fact_3 { get; set; }
		public decimal? costs_houseneed_heat_energy_plan_1 { get; set; }
		public decimal? costs_houseneed_heat_energy_plan_2 { get; set; }
		public decimal? costs_houseneed_heat_energy_plan_3 { get; set; }
		public decimal? costs_houseneed_el_energy_fact_1 { get; set; }
		public decimal? costs_houseneed_el_energy_fact_2 { get; set; }
		public decimal? costs_houseneed_el_energy_fact_3 { get; set; }
		public decimal? costs_houseneed_el_energy_plan_1 { get; set; }
		public decimal? costs_houseneed_el_energy_plan_2 { get; set; }
		public decimal? costs_houseneed_el_energy_plan_3 { get; set; }
	}

	[Keyless]
	public class TZHeatEnergyNeedCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? costs_all_heat_energy_fact_1 { get; set; }
		public decimal? costs_all_heat_energy_fact_2 { get; set; }
		public decimal? costs_all_heat_energy_fact_3 { get; set; }
		public decimal? costs_all_heat_energy_plan_1 { get; set; }
		public decimal? costs_all_heat_energy_plan_2 { get; set; }
		public decimal? costs_all_heat_energy_plan_3 { get; set; }
		public decimal? costs_buy_heat_energy_fact_1 { get; set; }
		public decimal? costs_buy_heat_energy_fact_2 { get; set; }
		public decimal? costs_buy_heat_energy_fact_3 { get; set; }
		public decimal? costs_buy_heat_energy_plan_1 { get; set; }
		public decimal? costs_buy_heat_energy_plan_2 { get; set; }
		public decimal? costs_buy_heat_energy_plan_3 { get; set; }
		public decimal? costs_heat_transfer_fact_1 { get; set; }
		public decimal? costs_heat_transfer_fact_2 { get; set; }
		public decimal? costs_heat_transfer_fact_3 { get; set; }
		public decimal? costs_heat_transfer_plan_1 { get; set; }
		public decimal? costs_heat_transfer_plan_2 { get; set; }
		public decimal? costs_heat_transfer_plan_3 { get; set; }
		public decimal? volume_buy_heat_energy_fact_1 { get; set; }
		public decimal? volume_buy_heat_energy_fact_2 { get; set; }
		public decimal? volume_buy_heat_energy_fact_3 { get; set; }
		public decimal? volume_buy_heat_energy_plan_1 { get; set; }
		public decimal? volume_buy_heat_energy_plan_2 { get; set; }
		public decimal? volume_buy_heat_energy_plan_3 { get; set; }
		public decimal? volume_buy_heat_energy_houseneed_fact_1 { get; set; }
		public decimal? volume_buy_heat_energy_houseneed_fact_2 { get; set; }
		public decimal? volume_buy_heat_energy_houseneed_fact_3 { get; set; }
		public decimal? volume_buy_heat_energy_houseneed_plan_1 { get; set; }
		public decimal? volume_buy_heat_energy_houseneed_plan_2 { get; set; }
		public decimal? volume_buy_heat_energy_houseneed_plan_3 { get; set; }
		public decimal? volume_buy_heat_energy_techneed_fact_1 { get; set; }
		public decimal? volume_buy_heat_energy_techneed_fact_2 { get; set; }
		public decimal? volume_buy_heat_energy_techneed_fact_3 { get; set; }
		public decimal? volume_buy_heat_energy_techneed_plan_1 { get; set; }
		public decimal? volume_buy_heat_energy_techneed_plan_2 { get; set; }
		public decimal? volume_buy_heat_energy_techneed_plan_3 { get; set; }
		public decimal? avg_price_buy_heat_energy_fact_1 { get; set; }
		public decimal? avg_price_buy_heat_energy_fact_2 { get; set; }
		public decimal? avg_price_buy_heat_energy_fact_3 { get; set; }
		public decimal? avg_price_buy_heat_energy_plan_1 { get; set; }
		public decimal? avg_price_buy_heat_energy_plan_2 { get; set; }
		public decimal? avg_price_buy_heat_energy_plan_3 { get; set; }
	}

	[Keyless]
	public class TZViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }

	}

	[Keyless]
	public class TZInputEnergyFactViewModel
	{
		public string tz_tso_name { get; set; }
		public string activity_types { get; set; }
		public string buy_place_name { get; set; }
		public string period_name { get; set; }
		public decimal? volume_buy_energy_fact { get; set; }
		public decimal? tariff_tso { get; set; }
		public decimal? costs_buy_energy_fact { get; set; }

	}

	[Keyless]
	public class TZInputEnergyPlanViewModel
	{
		public string tz_tso_name { get; set; }
		public string activity_types { get; set; }
		public string buy_place_name { get; set; }
		public string period_name { get; set; }
		public decimal? volume_buy_energy_plan { get; set; }
		public decimal? tariff_tso { get; set; }
		public decimal? costs_buy_energy_plan { get; set; }

	}

	[Keyless]
	public class TZElectroEnergyCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? costs_all_el_energy_fact_1 { get; set; }
		public decimal? costs_all_el_energy_fact_2 { get; set; }
		public decimal? costs_all_el_energy_fact_3 { get; set; }
		public decimal? costs_all_el_energy_plan_1 { get; set; }
		public decimal? costs_all_el_energy_plan_2 { get; set; }
		public decimal? costs_all_el_energy_plan_3 { get; set; }
		public decimal? costs_electricity_fact_1 { get; set; }
		public decimal? costs_electricity_fact_2 { get; set; }
		public decimal? costs_electricity_fact_3 { get; set; }
		public decimal? costs_electricity_plan_1 { get; set; }
		public decimal? costs_electricity_plan_2 { get; set; }
		public decimal? costs_electricity_plan_3 { get; set; }
		public decimal? costs_el_power_fact_1 { get; set; }
		public decimal? costs_el_power_fact_2 { get; set; }
		public decimal? costs_el_power_fact_3 { get; set; }
		public decimal? costs_el_power_plan_1 { get; set; }
		public decimal? costs_el_power_plan_2 { get; set; }
		public decimal? costs_el_power_plan_3 { get; set; }
		public decimal? avg_price_all_el_energy_fact_1 { get; set; }
		public decimal? avg_price_all_el_energy_fact_2 { get; set; }
		public decimal? avg_price_all_el_energy_fact_3 { get; set; }
		public decimal? avg_price_all_el_energy_plan_1 { get; set; }
		public decimal? avg_price_all_el_energy_plan_2 { get; set; }
		public decimal? avg_price_all_el_energy_plan_3 { get; set; }
		public decimal? volume_all_electricity_techneed_fact_1 { get; set; }
		public decimal? volume_all_electricity_techneed_fact_2 { get; set; }
		public decimal? volume_all_electricity_techneed_fact_3 { get; set; }
		public decimal? volume_all_electricity_techneed_plan_1 { get; set; }
		public decimal? volume_all_electricity_techneed_plan_2 { get; set; }
		public decimal? volume_all_electricity_techneed_plan_3 { get; set; }
		public decimal? volume_low_voltage_electricity_techneed_fact_1 { get; set; }
		public decimal? volume_low_voltage_electricity_techneed_fact_2 { get; set; }
		public decimal? volume_low_voltage_electricity_techneed_fact_3 { get; set; }
		public decimal? volume_low_voltage_electricity_techneed_plan_1 { get; set; }
		public decimal? volume_low_voltage_electricity_techneed_plan_2 { get; set; }
		public decimal? volume_low_voltage_electricity_techneed_plan_3 { get; set; }
		public decimal? volume_medium1_voltage_electricity_techneed_fact_1 { get; set; }
		public decimal? volume_medium1_voltage_electricity_techneed_fact_2 { get; set; }
		public decimal? volume_medium1_voltage_electricity_techneed_fact_3 { get; set; }
		public decimal? volume_medium1_voltage_electricity_techneed_plan_1 { get; set; }
		public decimal? volume_medium1_voltage_electricity_techneed_plan_2 { get; set; }
		public decimal? volume_medium1_voltage_electricity_techneed_plan_3 { get; set; }
		public decimal? volume_medium2_voltage_electricity_techneed_fact_1 { get; set; }
		public decimal? volume_medium2_voltage_electricity_techneed_fact_2 { get; set; }
		public decimal? volume_medium2_voltage_electricity_techneed_fact_3 { get; set; }
		public decimal? volume_medium2_voltage_electricity_techneed_plan_1 { get; set; }
		public decimal? volume_medium2_voltage_electricity_techneed_plan_2 { get; set; }
		public decimal? volume_medium2_voltage_electricity_techneed_plan_3 { get; set; }
		public decimal? volume_high_voltage_electricity_techneed_fact_1 { get; set; }
		public decimal? volume_high_voltage_electricity_techneed_fact_2 { get; set; }
		public decimal? volume_high_voltage_electricity_techneed_fact_3 { get; set; }
		public decimal? volume_high_voltage_electricity_techneed_plan_1 { get; set; }
		public decimal? volume_high_voltage_electricity_techneed_plan_2 { get; set; }
		public decimal? volume_high_voltage_electricity_techneed_plan_3 { get; set; }
		public decimal? volume_all_electricity_houseneed_fact_1 { get; set; }
		public decimal? volume_all_electricity_houseneed_fact_2 { get; set; }
		public decimal? volume_all_electricity_houseneed_fact_3 { get; set; }
		public decimal? volume_all_electricity_houseneed_plan_1 { get; set; }
		public decimal? volume_all_electricity_houseneed_plan_2 { get; set; }
		public decimal? volume_all_electricity_houseneed_plan_3 { get; set; }
		public decimal? volume_low_voltage_electricity_houseneed_fact_1 { get; set; }
		public decimal? volume_low_voltage_electricity_houseneed_fact_2 { get; set; }
		public decimal? volume_low_voltage_electricity_houseneed_fact_3 { get; set; }
		public decimal? volume_low_voltage_electricity_houseneed_plan_1 { get; set; }
		public decimal? volume_low_voltage_electricity_houseneed_plan_2 { get; set; }
		public decimal? volume_low_voltage_electricity_houseneed_plan_3 { get; set; }
		public decimal? volume_medium1_voltage_electricity_houseneed_fact_1 { get; set; }
		public decimal? volume_medium1_voltage_electricity_houseneed_fact_2 { get; set; }
		public decimal? volume_medium1_voltage_electricity_houseneed_fact_3 { get; set; }
		public decimal? volume_medium1_voltage_electricity_houseneed_plan_1 { get; set; }
		public decimal? volume_medium1_voltage_electricity_houseneed_plan_2 { get; set; }
		public decimal? volume_medium1_voltage_electricity_houseneed_plan_3 { get; set; }
		public decimal? volume_medium2_voltage_electricity_houseneed_fact_1 { get; set; }
		public decimal? volume_medium2_voltage_electricity_houseneed_fact_2 { get; set; }
		public decimal? volume_medium2_voltage_electricity_houseneed_fact_3 { get; set; }
		public decimal? volume_medium2_voltage_electricity_houseneed_plan_1 { get; set; }
		public decimal? volume_medium2_voltage_electricity_houseneed_plan_2 { get; set; }
		public decimal? volume_medium2_voltage_electricity_houseneed_plan_3 { get; set; }
		public decimal? volume_high_voltage_electricity_houseneed_fact_1 { get; set; }
		public decimal? volume_high_voltage_electricity_houseneed_fact_2 { get; set; }
		public decimal? volume_high_voltage_electricity_houseneed_fact_3 { get; set; }
		public decimal? volume_high_voltage_electricity_houseneed_plan_1 { get; set; }
		public decimal? volume_high_voltage_electricity_houseneed_plan_2 { get; set; }
		public decimal? volume_high_voltage_electricity_houseneed_plan_3 { get; set; }
		public decimal? avg_price_all_electricity_fact_1 { get; set; }
		public decimal? avg_price_all_electricity_fact_2 { get; set; }
		public decimal? avg_price_all_electricity_fact_3 { get; set; }
		public decimal? avg_price_all_electricity_plan_1 { get; set; }
		public decimal? avg_price_all_electricity_plan_2 { get; set; }
		public decimal? avg_price_all_electricity_plan_3 { get; set; }
		public decimal? price_low_voltage_electricity_fact_1 { get; set; }
		public decimal? price_low_voltage_electricity_fact_2 { get; set; }
		public decimal? price_low_voltage_electricity_fact_3 { get; set; }
		public decimal? price_low_voltage_electricity_plan_1 { get; set; }
		public decimal? price_low_voltage_electricity_plan_2 { get; set; }
		public decimal? price_low_voltage_electricity_plan_3 { get; set; }
		public decimal? price_medium1_voltage_electricity_fact_1 { get; set; }
		public decimal? price_medium1_voltage_electricity_fact_2 { get; set; }
		public decimal? price_medium1_voltage_electricity_fact_3 { get; set; }
		public decimal? price_medium1_voltage_electricity_plan_1 { get; set; }
		public decimal? price_medium1_voltage_electricity_plan_2 { get; set; }
		public decimal? price_medium1_voltage_electricity_plan_3 { get; set; }
		public decimal? price_medium2_voltage_electricity_fact_1 { get; set; }
		public decimal? price_medium2_voltage_electricity_fact_2 { get; set; }
		public decimal? price_medium2_voltage_electricity_fact_3 { get; set; }
		public decimal? price_medium2_voltage_electricity_plan_1 { get; set; }
		public decimal? price_medium2_voltage_electricity_plan_2 { get; set; }
		public decimal? price_medium2_voltage_electricity_plan_3 { get; set; }
		public decimal? price_high_voltage_electricity_fact_1 { get; set; }
		public decimal? price_high_voltage_electricity_fact_2 { get; set; }
		public decimal? price_high_voltage_electricity_fact_3 { get; set; }
		public decimal? price_high_voltage_electricity_plan_1 { get; set; }
		public decimal? price_high_voltage_electricity_plan_2 { get; set; }
		public decimal? price_high_voltage_electricity_plan_3 { get; set; }
		public decimal? volume_all_el_power_techneed_fact_1 { get; set; }
		public decimal? volume_all_el_power_techneed_fact_2 { get; set; }
		public decimal? volume_all_el_power_techneed_fact_3 { get; set; }
		public decimal? volume_all_el_power_techneed_plan_1 { get; set; }
		public decimal? volume_all_el_power_techneed_plan_2 { get; set; }
		public decimal? volume_all_el_power_techneed_plan_3 { get; set; }
		public decimal? volume_low_voltage_el_power_techneed_fact_1 { get; set; }
		public decimal? volume_low_voltage_el_power_techneed_fact_2 { get; set; }
		public decimal? volume_low_voltage_el_power_techneed_fact_3 { get; set; }
		public decimal? volume_low_voltage_el_power_techneed_plan_1 { get; set; }
		public decimal? volume_low_voltage_el_power_techneed_plan_2 { get; set; }
		public decimal? volume_low_voltage_el_power_techneed_plan_3 { get; set; }
		public decimal? volume_medium1_voltage_el_power_techneed_fact_1 { get; set; }
		public decimal? volume_medium1_voltage_el_power_techneed_fact_2 { get; set; }
		public decimal? volume_medium1_voltage_el_power_techneed_fact_3 { get; set; }
		public decimal? volume_medium1_voltage_el_power_techneed_plan_1 { get; set; }
		public decimal? volume_medium1_voltage_el_power_techneed_plan_2 { get; set; }
		public decimal? volume_medium1_voltage_el_power_techneed_plan_3 { get; set; }
		public decimal? volume_medium2_voltage_el_power_techneed_fact_1 { get; set; }
		public decimal? volume_medium2_voltage_el_power_techneed_fact_2 { get; set; }
		public decimal? volume_medium2_voltage_el_power_techneed_fact_3 { get; set; }
		public decimal? volume_medium2_voltage_el_power_techneed_plan_1 { get; set; }
		public decimal? volume_medium2_voltage_el_power_techneed_plan_2 { get; set; }
		public decimal? volume_medium2_voltage_el_power_techneed_plan_3 { get; set; }
		public decimal? volume_high_voltage_el_power_techneed_fact_1 { get; set; }
		public decimal? volume_high_voltage_el_power_techneed_fact_2 { get; set; }
		public decimal? volume_high_voltage_el_power_techneed_fact_3 { get; set; }
		public decimal? volume_high_voltage_el_power_techneed_plan_1 { get; set; }
		public decimal? volume_high_voltage_el_power_techneed_plan_2 { get; set; }
		public decimal? volume_high_voltage_el_power_techneed_plan_3 { get; set; }
		public decimal? volume_all_el_power_houseneed_fact_1 { get; set; }
		public decimal? volume_all_el_power_houseneed_fact_2 { get; set; }
		public decimal? volume_all_el_power_houseneed_fact_3 { get; set; }
		public decimal? volume_all_el_power_houseneed_plan_1 { get; set; }
		public decimal? volume_all_el_power_houseneed_plan_2 { get; set; }
		public decimal? volume_all_el_power_houseneed_plan_3 { get; set; }
		public decimal? volume_low_voltage_el_power_houseneed_fact_1 { get; set; }
		public decimal? volume_low_voltage_el_power_houseneed_fact_2 { get; set; }
		public decimal? volume_low_voltage_el_power_houseneed_fact_3 { get; set; }
		public decimal? volume_low_voltage_el_power_houseneed_plan_1 { get; set; }
		public decimal? volume_low_voltage_el_power_houseneed_plan_2 { get; set; }
		public decimal? volume_low_voltage_el_power_houseneed_plan_3 { get; set; }
		public decimal? volume_medium1_voltage_el_power_houseneed_fact_1 { get; set; }
		public decimal? volume_medium1_voltage_el_power_houseneed_fact_2 { get; set; }
		public decimal? volume_medium1_voltage_el_power_houseneed_fact_3 { get; set; }
		public decimal? volume_medium1_voltage_el_power_houseneed_plan_1 { get; set; }
		public decimal? volume_medium1_voltage_el_power_houseneed_plan_2 { get; set; }
		public decimal? volume_medium1_voltage_el_power_houseneed_plan_3 { get; set; }
		public decimal? volume_medium2_voltage_el_power_houseneed_fact_1 { get; set; }
		public decimal? volume_medium2_voltage_el_power_houseneed_fact_2 { get; set; }
		public decimal? volume_medium2_voltage_el_power_houseneed_fact_3 { get; set; }
		public decimal? volume_medium2_voltage_el_power_houseneed_plan_1 { get; set; }
		public decimal? volume_medium2_voltage_el_power_houseneed_plan_2 { get; set; }
		public decimal? volume_medium2_voltage_el_power_houseneed_plan_3 { get; set; }
		public decimal? volume_high_voltage_el_power_houseneed_fact_1 { get; set; }
		public decimal? volume_high_voltage_el_power_houseneed_fact_2 { get; set; }
		public decimal? volume_high_voltage_el_power_houseneed_fact_3 { get; set; }
		public decimal? volume_high_voltage_el_power_houseneed_plan_1 { get; set; }
		public decimal? volume_high_voltage_el_power_houseneed_plan_2 { get; set; }
		public decimal? volume_high_voltage_el_power_houseneed_plan_3 { get; set; }
		public decimal? avg_price_all_el_power_fact_1 { get; set; }
		public decimal? avg_price_all_el_power_fact_2 { get; set; }
		public decimal? avg_price_all_el_power_fact_3 { get; set; }
		public decimal? avg_price_all_el_power_plan_1 { get; set; }
		public decimal? avg_price_all_el_power_plan_2 { get; set; }
		public decimal? avg_price_all_el_power_plan_3 { get; set; }
		public decimal? price_low_voltage_el_power_fact_1 { get; set; }
		public decimal? price_low_voltage_el_power_fact_2 { get; set; }
		public decimal? price_low_voltage_el_power_fact_3 { get; set; }
		public decimal? price_low_voltage_el_power_plan_1 { get; set; }
		public decimal? price_low_voltage_el_power_plan_2 { get; set; }
		public decimal? price_low_voltage_el_power_plan_3 { get; set; }
		public decimal? price_medium1_voltage_el_power_fact_1 { get; set; }
		public decimal? price_medium1_voltage_el_power_fact_2 { get; set; }
		public decimal? price_medium1_voltage_el_power_fact_3 { get; set; }
		public decimal? price_medium1_voltage_el_power_plan_1 { get; set; }
		public decimal? price_medium1_voltage_el_power_plan_2 { get; set; }
		public decimal? price_medium1_voltage_el_power_plan_3 { get; set; }
		public decimal? price_medium2_voltage_el_power_fact_1 { get; set; }
		public decimal? price_medium2_voltage_el_power_fact_2 { get; set; }
		public decimal? price_medium2_voltage_el_power_fact_3 { get; set; }
		public decimal? price_medium2_voltage_el_power_plan_1 { get; set; }
		public decimal? price_medium2_voltage_el_power_plan_2 { get; set; }
		public decimal? price_medium2_voltage_el_power_plan_3 { get; set; }
		public decimal? price_high_voltage_el_power_fact_1 { get; set; }
		public decimal? price_high_voltage_el_power_fact_2 { get; set; }
		public decimal? price_high_voltage_el_power_fact_3 { get; set; }
		public decimal? price_high_voltage_el_power_plan_1 { get; set; }
		public decimal? price_high_voltage_el_power_plan_2 { get; set; }
		public decimal? price_high_voltage_el_power_plan_3 { get; set; }
	}

	[Keyless]
	public class TZSalaryCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? costs_all_salary_fact_1 { get; set; }
		public decimal? costs_all_salary_fact_2 { get; set; }
		public decimal? costs_all_salary_fact_3 { get; set; }
		public decimal? costs_all_salary_plan_1 { get; set; }
		public decimal? costs_all_salary_plan_2 { get; set; }
		public decimal? costs_all_salary_plan_3 { get; set; }
		public decimal? avg_count_workers_fact_1 { get; set; }
		public decimal? avg_count_workers_fact_2 { get; set; }
		public decimal? avg_count_workers_fact_3 { get; set; }
		public decimal? avg_count_workers_plan_1 { get; set; }
		public decimal? avg_count_workers_plan_2 { get; set; }
		public decimal? avg_count_workers_plan_3 { get; set; }
		public decimal? avg_salary_all_fact_1 { get; set; }
		public decimal? avg_salary_all_fact_2 { get; set; }
		public decimal? avg_salary_all_fact_3 { get; set; }
		public decimal? avg_salary_all_plan_1 { get; set; }
		public decimal? avg_salary_all_plan_2 { get; set; }
		public decimal? avg_salary_all_plan_3 { get; set; }
		public decimal? costs_prod_workers_salary_fact_1 { get; set; }
		public decimal? costs_prod_workers_salary_fact_2 { get; set; }
		public decimal? costs_prod_workers_salary_fact_3 { get; set; }
		public decimal? costs_prod_workers_salary_plan_1 { get; set; }
		public decimal? costs_prod_workers_salary_plan_2 { get; set; }
		public decimal? costs_prod_workers_salary_plan_3 { get; set; }
		public decimal? count_prod_workers_fact_1 { get; set; }
		public decimal? count_prod_workers_fact_2 { get; set; }
		public decimal? count_prod_workers_fact_3 { get; set; }
		public decimal? count_prod_workers_plan_1 { get; set; }
		public decimal? count_prod_workers_plan_2 { get; set; }
		public decimal? count_prod_workers_plan_3 { get; set; }
		public decimal? avg_salary_prod_workers_fact_1 { get; set; }
		public decimal? avg_salary_prod_workers_fact_2 { get; set; }
		public decimal? avg_salary_prod_workers_fact_3 { get; set; }
		public decimal? avg_salary_prod_workers_plan_1 { get; set; }
		public decimal? avg_salary_prod_workers_plan_2 { get; set; }
		public decimal? avg_salary_prod_workers_plan_3 { get; set; }
		public decimal? soc_deduction_prod_workers_fact_1 { get; set; }
		public decimal? soc_deduction_prod_workers_fact_2 { get; set; }
		public decimal? soc_deduction_prod_workers_fact_3 { get; set; }
		public decimal? soc_deduction_prod_workers_plan_1 { get; set; }
		public decimal? soc_deduction_prod_workers_plan_2 { get; set; }
		public decimal? soc_deduction_prod_workers_plan_3 { get; set; }
		public decimal? costs_repair_workers_salary_fact_1 { get; set; }
		public decimal? costs_repair_workers_salary_fact_2 { get; set; }
		public decimal? costs_repair_workers_salary_fact_3 { get; set; }
		public decimal? costs_repair_workers_salary_plan_1 { get; set; }
		public decimal? costs_repair_workers_salary_plan_2 { get; set; }
		public decimal? costs_repair_workers_salary_plan_3 { get; set; }
		public decimal? count_repair_workers_fact_1 { get; set; }
		public decimal? count_repair_workers_fact_2 { get; set; }
		public decimal? count_repair_workers_fact_3 { get; set; }
		public decimal? count_repair_workers_plan_1 { get; set; }
		public decimal? count_repair_workers_plan_2 { get; set; }
		public decimal? count_repair_workers_plan_3 { get; set; }
		public decimal? avg_salary_repair_workers_fact_1 { get; set; }
		public decimal? avg_salary_repair_workers_fact_2 { get; set; }
		public decimal? avg_salary_repair_workers_fact_3 { get; set; }
		public decimal? avg_salary_repair_workers_plan_1 { get; set; }
		public decimal? avg_salary_repair_workers_plan_2 { get; set; }
		public decimal? avg_salary_repair_workers_plan_3 { get; set; }
		public decimal? soc_deduction_repair_workers_fact_1 { get; set; }
		public decimal? soc_deduction_repair_workers_fact_2 { get; set; }
		public decimal? soc_deduction_repair_workers_fact_3 { get; set; }
		public decimal? soc_deduction_repair_workers_plan_1 { get; set; }
		public decimal? soc_deduction_repair_workers_plan_2 { get; set; }
		public decimal? soc_deduction_repair_workers_plan_3 { get; set; }
		public decimal? costs_shop_workers_salary_fact_1 { get; set; }
		public decimal? costs_shop_workers_salary_fact_2 { get; set; }
		public decimal? costs_shop_workers_salary_fact_3 { get; set; }
		public decimal? costs_shop_workers_salary_plan_1 { get; set; }
		public decimal? costs_shop_workers_salary_plan_2 { get; set; }
		public decimal? costs_shop_workers_salary_plan_3 { get; set; }
		public decimal? count_shop_workers_fact_1 { get; set; }
		public decimal? count_shop_workers_fact_2 { get; set; }
		public decimal? count_shop_workers_fact_3 { get; set; }
		public decimal? count_shop_workers_plan_1 { get; set; }
		public decimal? count_shop_workers_plan_2 { get; set; }
		public decimal? count_shop_workers_plan_3 { get; set; }
		public decimal? avg_salary_shop_workers_fact_1 { get; set; }
		public decimal? avg_salary_shop_workers_fact_2 { get; set; }
		public decimal? avg_salary_shop_workers_fact_3 { get; set; }
		public decimal? avg_salary_shop_workers_plan_1 { get; set; }
		public decimal? avg_salary_shop_workers_plan_2 { get; set; }
		public decimal? avg_salary_shop_workers_plan_3 { get; set; }
		public decimal? soc_deduction_shop_workers_fact_1 { get; set; }
		public decimal? soc_deduction_shop_workers_fact_2 { get; set; }
		public decimal? soc_deduction_shop_workers_fact_3 { get; set; }
		public decimal? soc_deduction_shop_workers_plan_1 { get; set; }
		public decimal? soc_deduction_shop_workers_plan_2 { get; set; }
		public decimal? soc_deduction_shop_workers_plan_3 { get; set; }
		public decimal? costs_adm_manag_workers_salary_fact_1 { get; set; }
		public decimal? costs_adm_manag_workers_salary_fact_2 { get; set; }
		public decimal? costs_adm_manag_workers_salary_fact_3 { get; set; }
		public decimal? costs_adm_manag_workers_salary_plan_1 { get; set; }
		public decimal? costs_adm_manag_workers_salary_plan_2 { get; set; }
		public decimal? costs_adm_manag_workers_salary_plan_3 { get; set; }
		public decimal? count_adm_manag_workers_fact_1 { get; set; }
		public decimal? count_adm_manag_workers_fact_2 { get; set; }
		public decimal? count_adm_manag_workers_fact_3 { get; set; }
		public decimal? count_adm_manag_workers_plan_1 { get; set; }
		public decimal? count_adm_manag_workers_plan_2 { get; set; }
		public decimal? count_adm_manag_workers_plan_3 { get; set; }
		public decimal? avg_salary_adm_manag_workers_fact_1 { get; set; }
		public decimal? avg_salary_adm_manag_workers_fact_2 { get; set; }
		public decimal? avg_salary_adm_manag_workers_fact_3 { get; set; }
		public decimal? avg_salary_adm_manag_workers_plan_1 { get; set; }
		public decimal? avg_salary_adm_manag_workers_plan_2 { get; set; }
		public decimal? avg_salary_adm_manag_workers_plan_3 { get; set; }
		public decimal? soc_deduction_adm_manag_workers_fact_1 { get; set; }
		public decimal? soc_deduction_adm_manag_workers_fact_2 { get; set; }
		public decimal? soc_deduction_adm_manag_workers_fact_3 { get; set; }
		public decimal? soc_deduction_adm_manag_workers_plan_1 { get; set; }
		public decimal? soc_deduction_adm_manag_workers_plan_2 { get; set; }
		public decimal? soc_deduction_adm_manag_workers_plan_3 { get; set; }
		public decimal? costs_other_workers_salary_fact_1 { get; set; }
		public decimal? costs_other_workers_salary_fact_2 { get; set; }
		public decimal? costs_other_workers_salary_fact_3 { get; set; }
		public decimal? costs_other_workers_salary_plan_1 { get; set; }
		public decimal? costs_other_workers_salary_plan_2 { get; set; }
		public decimal? costs_other_workers_salary_plan_3 { get; set; }
		public decimal? count_other_workers_fact_1 { get; set; }
		public decimal? count_other_workers_fact_2 { get; set; }
		public decimal? count_other_workers_fact_3 { get; set; }
		public decimal? count_other_workers_plan_1 { get; set; }
		public decimal? count_other_workers_plan_2 { get; set; }
		public decimal? count_other_workers_plan_3 { get; set; }
		public decimal? avg_salary_other_workers_fact_1 { get; set; }
		public decimal? avg_salary_other_workers_fact_2 { get; set; }
		public decimal? avg_salary_other_workers_fact_3 { get; set; }
		public decimal? avg_salary_other_workers_plan_1 { get; set; }
		public decimal? avg_salary_other_workers_plan_2 { get; set; }
		public decimal? avg_salary_other_workers_plan_3 { get; set; }
		public decimal? soc_deduction_other_workers_fact_1 { get; set; }
		public decimal? soc_deduction_other_workers_fact_2 { get; set; }
		public decimal? soc_deduction_other_workers_fact_3 { get; set; }
		public decimal? soc_deduction_other_workers_plan_1 { get; set; }
		public decimal? soc_deduction_other_workers_plan_2 { get; set; }
		public decimal? soc_deduction_other_workers_plan_3 { get; set; }

	}

	[Keyless]
	public class TZMaterialsCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_materials_all_fact_1 { get; set; }
		public decimal? cost_materials_all_fact_2 { get; set; }
		public decimal? cost_materials_all_fact_3 { get; set; }
		public decimal? cost_materials_all_plan_1 { get; set; }
		public decimal? cost_materials_all_plan_2 { get; set; }
		public decimal? cost_materials_all_plan_3 { get; set; }
		public decimal? cost_reagents_fact_1 { get; set; }
		public decimal? cost_reagents_fact_2 { get; set; }
		public decimal? cost_reagents_fact_3 { get; set; }
		public decimal? cost_reagents_plan_1 { get; set; }
		public decimal? cost_reagents_plan_2 { get; set; }
		public decimal? cost_reagents_plan_3 { get; set; }
		public decimal? cost_gsm_fact_1 { get; set; }
		public decimal? cost_gsm_fact_2 { get; set; }
		public decimal? cost_gsm_fact_3 { get; set; }
		public decimal? cost_gsm_plan_1 { get; set; }
		public decimal? cost_gsm_plan_2 { get; set; }
		public decimal? cost_gsm_plan_3 { get; set; }
		public decimal? cost_repair_fact_1 { get; set; }
		public decimal? cost_repair_fact_2 { get; set; }
		public decimal? cost_repair_fact_3 { get; set; }
		public decimal? cost_repair_plan_1 { get; set; }
		public decimal? cost_repair_plan_2 { get; set; }
		public decimal? cost_repair_plan_3 { get; set; }
		public decimal? cost_tech_service_fact_1 { get; set; }
		public decimal? cost_tech_service_fact_2 { get; set; }
		public decimal? cost_tech_service_fact_3 { get; set; }
		public decimal? cost_tech_service_plan_1 { get; set; }
		public decimal? cost_tech_service_plan_2 { get; set; }
		public decimal? cost_tech_service_plan_3 { get; set; }
		public decimal? cost_spec_clothes_fact_1 { get; set; }
		public decimal? cost_spec_clothes_fact_2 { get; set; }
		public decimal? cost_spec_clothes_fact_3 { get; set; }
		public decimal? cost_spec_clothes_plan_1 { get; set; }
		public decimal? cost_spec_clothes_plan_2 { get; set; }
		public decimal? cost_spec_clothes_plan_3 { get; set; }
		public decimal? cost_house_inventary_fact_1 { get; set; }
		public decimal? cost_house_inventary_fact_2 { get; set; }
		public decimal? cost_house_inventary_fact_3 { get; set; }
		public decimal? cost_house_inventary_plan_1 { get; set; }
		public decimal? cost_house_inventary_plan_2 { get; set; }
		public decimal? cost_house_inventary_plan_3 { get; set; }
		public decimal? cost_other_fact_1 { get; set; }
		public decimal? cost_other_fact_2 { get; set; }
		public decimal? cost_other_fact_3 { get; set; }
		public decimal? cost_other_plan_1 { get; set; }
		public decimal? cost_other_plan_2 { get; set; }
		public decimal? cost_other_plan_3 { get; set; }
	}

	[Keyless]
	public class TZOtherOrgCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_other_org_all_fact_1 { get; set; }
		public decimal? cost_other_org_all_fact_2 { get; set; }
		public decimal? cost_other_org_all_fact_3 { get; set; }
		public decimal? cost_other_org_all_plan_1 { get; set; }
		public decimal? cost_other_org_all_plan_2 { get; set; }
		public decimal? cost_other_org_all_plan_3 { get; set; }
		public decimal? cost_transport_fact_1 { get; set; }
		public decimal? cost_transport_fact_2 { get; set; }
		public decimal? cost_transport_fact_3 { get; set; }
		public decimal? cost_transport_plan_1 { get; set; }
		public decimal? cost_transport_plan_2 { get; set; }
		public decimal? cost_transport_plan_3 { get; set; }
		public decimal? cost_tech_regulation_fact_1 { get; set; }
		public decimal? cost_tech_regulation_fact_2 { get; set; }
		public decimal? cost_tech_regulation_fact_3 { get; set; }
		public decimal? cost_tech_regulation_plan_1 { get; set; }
		public decimal? cost_tech_regulation_plan_2 { get; set; }
		public decimal? cost_tech_regulation_plan_3 { get; set; }
		public decimal? cost_other_support_prod_fact_1 { get; set; }
		public decimal? cost_other_support_prod_fact_2 { get; set; }
		public decimal? cost_other_support_prod_fact_3 { get; set; }
		public decimal? cost_other_support_prod_plan_1 { get; set; }
		public decimal? cost_other_support_prod_plan_2 { get; set; }
		public decimal? cost_other_support_prod_plan_3 { get; set; }
		public decimal? cost_other_service_prod_fact_1 { get; set; }
		public decimal? cost_other_service_prod_fact_2 { get; set; }
		public decimal? cost_other_service_prod_fact_3 { get; set; }
		public decimal? cost_other_service_prod_plan_1 { get; set; }
		public decimal? cost_other_service_prod_plan_2 { get; set; }
		public decimal? cost_other_service_prod_plan_3 { get; set; }
		
	}

	[Keyless]
	public class TZOrgServiceOtherCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_org_other_service_all_fact_1 { get; set; }
		public decimal? cost_org_other_service_all_fact_2 { get; set; }
		public decimal? cost_org_other_service_all_fact_3 { get; set; }
		public decimal? cost_org_other_service_all_plan_1 { get; set; }
		public decimal? cost_org_other_service_all_plan_2 { get; set; }
		public decimal? cost_org_other_service_all_plan_3 { get; set; }
		public decimal? cost_connect_service_fact_1 { get; set; }
		public decimal? cost_connect_service_fact_2 { get; set; }
		public decimal? cost_connect_service_fact_3 { get; set; }
		public decimal? cost_connect_service_plan_1 { get; set; }
		public decimal? cost_connect_service_plan_2 { get; set; }
		public decimal? cost_connect_service_plan_3 { get; set; }
		public decimal? cost_security_service_fact_1 { get; set; }
		public decimal? cost_security_service_fact_2 { get; set; }
		public decimal? cost_security_service_fact_3 { get; set; }
		public decimal? cost_security_service_plan_1 { get; set; }
		public decimal? cost_security_service_plan_2 { get; set; }
		public decimal? cost_security_service_plan_3 { get; set; }
		public decimal? cost_communal_service_fact_1 { get; set; }
		public decimal? cost_communal_service_fact_2 { get; set; }
		public decimal? cost_communal_service_fact_3 { get; set; }
		public decimal? cost_communal_service_plan_1 { get; set; }
		public decimal? cost_communal_service_plan_2 { get; set; }
		public decimal? cost_communal_service_plan_3 { get; set; }
		public decimal? cost_consulting_service_fact_1 { get; set; }
		public decimal? cost_consulting_service_fact_2 { get; set; }
		public decimal? cost_consulting_service_fact_3 { get; set; }
		public decimal? cost_consulting_service_plan_1 { get; set; }
		public decimal? cost_consulting_service_plan_2 { get; set; }
		public decimal? cost_consulting_service_plan_3 { get; set; }
		public decimal? cost_legal_service_fact_1 { get; set; }
		public decimal? cost_legal_service_fact_2 { get; set; }
		public decimal? cost_legal_service_fact_3 { get; set; }
		public decimal? cost_legal_service_plan_1 { get; set; }
		public decimal? cost_legal_service_plan_2 { get; set; }
		public decimal? cost_legal_service_plan_3 { get; set; }
		public decimal? cost_inform_service_fact_1 { get; set; }
		public decimal? cost_inform_service_fact_2 { get; set; }
		public decimal? cost_inform_service_fact_3 { get; set; }
		public decimal? cost_inform_service_plan_1 { get; set; }
		public decimal? cost_inform_service_plan_2 { get; set; }
		public decimal? cost_inform_service_plan_3 { get; set; }
		public decimal? cost_audit_service_fact_1 { get; set; }
		public decimal? cost_audit_service_fact_2 { get; set; }
		public decimal? cost_audit_service_fact_3 { get; set; }
		public decimal? cost_audit_service_plan_1 { get; set; }
		public decimal? cost_audit_service_plan_2 { get; set; }
		public decimal? cost_audit_service_plan_3 { get; set; }
		public decimal? cost_strategic_manag_service_fact_1 { get; set; }
		public decimal? cost_strategic_manag_service_fact_2 { get; set; }
		public decimal? cost_strategic_manag_service_fact_3 { get; set; }
		public decimal? cost_strategic_manag_service_plan_1 { get; set; }
		public decimal? cost_strategic_manag_service_plan_2 { get; set; }
		public decimal? cost_strategic_manag_service_plan_3 { get; set; }
		public decimal? cost_prepare_develop_service_fact_1 { get; set; }
		public decimal? cost_prepare_develop_service_fact_2 { get; set; }
		public decimal? cost_prepare_develop_service_fact_3 { get; set; }
		public decimal? cost_prepare_develop_service_plan_1 { get; set; }
		public decimal? cost_prepare_develop_service_plan_2 { get; set; }
		public decimal? cost_prepare_develop_service_plan_3 { get; set; }
		public decimal? cost_targeted_funds_fact_1 { get; set; }
		public decimal? cost_targeted_funds_fact_2 { get; set; }
		public decimal? cost_targeted_funds_fact_3 { get; set; }
		public decimal? cost_targeted_funds_plan_1 { get; set; }
		public decimal? cost_targeted_funds_plan_2 { get; set; }
		public decimal? cost_targeted_funds_plan_3 { get; set; }
		public decimal? cost_additional_insure_fact_1 { get; set; }
		public decimal? cost_additional_insure_fact_2 { get; set; }
		public decimal? cost_additional_insure_fact_3 { get; set; }
		public decimal? cost_additional_insure_plan_1 { get; set; }
		public decimal? cost_additional_insure_plan_2 { get; set; }
		public decimal? cost_additional_insure_plan_3 { get; set; }
		public decimal? cost_other_work_service_fact_1 { get; set; }
		public decimal? cost_other_work_service_fact_2 { get; set; }
		public decimal? cost_other_work_service_fact_3 { get; set; }
		public decimal? cost_other_work_service_plan_1 { get; set; }
		public decimal? cost_other_work_service_plan_2 { get; set; }
		public decimal? cost_other_work_service_plan_3 { get; set; }

	}

	[Keyless]
	public class TZRepairFundingCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_repair_funding_fact_1 { get; set; }
		public decimal? cost_repair_funding_fact_2 { get; set; }
		public decimal? cost_repair_funding_fact_3 { get; set; }
		public decimal? cost_repair_funding_plan_1 { get; set; }
		public decimal? cost_repair_funding_plan_2 { get; set; }
		public decimal? cost_repair_funding_plan_3 { get; set; }

	}

	[Keyless]
	public class TZDecomissionCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_decomission_fact_1 { get; set; }
		public decimal? cost_decomission_fact_2 { get; set; }
		public decimal? cost_decomission_fact_3 { get; set; }
		public decimal? cost_decomission_plan_1 { get; set; }
		public decimal? cost_decomission_plan_2 { get; set; }
		public decimal? cost_decomission_plan_3 { get; set; }

	}

	[Keyless]
	public class TZOtherOperatingsCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_travel_fact_1 { get; set; }
		public decimal? cost_travel_fact_2 { get; set; }
		public decimal? cost_travel_fact_3 { get; set; }
		public decimal? cost_travel_plan_1 { get; set; }
		public decimal? cost_travel_plan_2 { get; set; }
		public decimal? cost_travel_plan_3 { get; set; }
		public decimal? cost_staff_train_fact_1 { get; set; }
		public decimal? cost_staff_train_fact_2 { get; set; }
		public decimal? cost_staff_train_fact_3 { get; set; }
		public decimal? cost_staff_train_plan_1 { get; set; }
		public decimal? cost_staff_train_plan_2 { get; set; }
		public decimal? cost_staff_train_plan_3 { get; set; }
		public decimal? cost_bank_service_fact_1 { get; set; }
		public decimal? cost_bank_service_fact_2 { get; set; }
		public decimal? cost_bank_service_fact_3 { get; set; }
		public decimal? cost_bank_service_plan_1 { get; set; }
		public decimal? cost_bank_service_plan_2 { get; set; }
		public decimal? cost_bank_service_plan_3 { get; set; }
		public decimal? rent_nonprod_obj_state_prop_fact_1 { get; set; }
		public decimal? rent_nonprod_obj_state_prop_fact_2 { get; set; }
		public decimal? rent_nonprod_obj_state_prop_fact_3 { get; set; }
		public decimal? rent_nonprod_obj_state_prop_plan_1 { get; set; }
		public decimal? rent_nonprod_obj_state_prop_plan_2 { get; set; }
		public decimal? rent_nonprod_obj_state_prop_plan_3 { get; set; }
		public decimal? rent_nonprod_obj_all_fact_1 { get; set; }
		public decimal? rent_nonprod_obj_all_fact_2 { get; set; }
		public decimal? rent_nonprod_obj_all_fact_3 { get; set; }
		public decimal? rent_nonprod_obj_all_plan_1 { get; set; }
		public decimal? rent_nonprod_obj_all_plan_2 { get; set; }
		public decimal? rent_nonprod_obj_all_plan_3 { get; set; }
		public decimal? rent_nonprod_obj_municipal_prop_fact_1 { get; set; }
		public decimal? rent_nonprod_obj_municipal_prop_fact_2 { get; set; }
		public decimal? rent_nonprod_obj_municipal_prop_fact_3 { get; set; }
		public decimal? rent_nonprod_obj_municipal_prop_plan_1 { get; set; }
		public decimal? rent_nonprod_obj_municipal_prop_plan_2 { get; set; }
		public decimal? rent_nonprod_obj_municipal_prop_plan_3 { get; set; }
		public decimal? rent_nonprod_obj_other_fact_1 { get; set; }
		public decimal? rent_nonprod_obj_other_fact_2 { get; set; }
		public decimal? rent_nonprod_obj_other_fact_3 { get; set; }
		public decimal? rent_nonprod_obj_other_plan_1 { get; set; }
		public decimal? rent_nonprod_obj_other_plan_2 { get; set; }
		public decimal? rent_nonprod_obj_other_plan_3 { get; set; }
		public decimal? leasing_nonprod_obj_without_own_rights_fact_1 { get; set; }
		public decimal? leasing_nonprod_obj_without_own_rights_fact_2 { get; set; }
		public decimal? leasing_nonprod_obj_without_own_rights_fact_3 { get; set; }
		public decimal? leasing_nonprod_obj_without_own_rights_plan_1 { get; set; }
		public decimal? leasing_nonprod_obj_without_own_rights_plan_2 { get; set; }
		public decimal? leasing_nonprod_obj_without_own_rights_plan_3 { get; set; }
		public decimal? cost_other_operatings_all_fact_1 { get; set; }
		public decimal? cost_other_operatings_all_fact_2 { get; set; }
		public decimal? cost_other_operatings_all_fact_3 { get; set; }
		public decimal? cost_other_operatings_all_plan_1 { get; set; }
		public decimal? cost_other_operatings_all_plan_2 { get; set; }
		public decimal? cost_other_operatings_all_plan_3 { get; set; }
		public decimal? cost_other_operating_house_fact_1 { get; set; }
		public decimal? cost_other_operating_house_fact_2 { get; set; }
		public decimal? cost_other_operating_house_fact_3 { get; set; }
		public decimal? cost_other_operating_house_plan_1 { get; set; }
		public decimal? cost_other_operating_house_plan_2 { get; set; }
		public decimal? cost_other_operating_house_plan_3 { get; set; }
		public decimal? cost_other_operating_fact_1 { get; set; }
		public decimal? cost_other_operating_fact_2 { get; set; }
		public decimal? cost_other_operating_fact_3 { get; set; }
		public decimal? cost_other_operating_plan_1 { get; set; }
		public decimal? cost_other_operating_plan_2 { get; set; }
		public decimal? cost_other_operating_plan_3 { get; set; }

	}

	[Keyless]
	public class TZTaxesCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_req_pay_all_fact_1 { get; set; }
		public decimal? cost_req_pay_all_fact_2 { get; set; }
		public decimal? cost_req_pay_all_fact_3 { get; set; }
		public decimal? cost_req_pay_all_plan_1 { get; set; }
		public decimal? cost_req_pay_all_plan_2 { get; set; }
		public decimal? cost_req_pay_all_plan_3 { get; set; }
		public decimal? cost_emissions_pollutant_fact_1 { get; set; }
		public decimal? cost_emissions_pollutant_fact_2 { get; set; }
		public decimal? cost_emissions_pollutant_fact_3 { get; set; }
		public decimal? cost_emissions_pollutant_plan_1 { get; set; }
		public decimal? cost_emissions_pollutant_plan_2 { get; set; }
		public decimal? cost_emissions_pollutant_plan_3 { get; set; }
		public decimal? cost_required_insure_fact_1 { get; set; }
		public decimal? cost_required_insure_fact_2 { get; set; }
		public decimal? cost_required_insure_fact_3 { get; set; }
		public decimal? cost_required_insure_plan_1 { get; set; }
		public decimal? cost_required_insure_plan_2 { get; set; }
		public decimal? cost_required_insure_plan_3 { get; set; }
		public decimal? cost_taxes_all_fact_1 { get; set; }
		public decimal? cost_taxes_all_fact_2 { get; set; }
		public decimal? cost_taxes_all_fact_3 { get; set; }
		public decimal? cost_taxes_all_plan_1 { get; set; }
		public decimal? cost_taxes_all_plan_2 { get; set; }
		public decimal? cost_taxes_all_plan_3 { get; set; }
		public decimal? cost_tax_org_property_fact_1 { get; set; }
		public decimal? cost_tax_org_property_fact_2 { get; set; }
		public decimal? cost_tax_org_property_fact_3 { get; set; }
		public decimal? cost_tax_org_property_plan_1 { get; set; }
		public decimal? cost_tax_org_property_plan_2 { get; set; }
		public decimal? cost_tax_org_property_plan_3 { get; set; }
		public decimal? cost_tax_land_fact_1 { get; set; }
		public decimal? cost_tax_land_fact_2 { get; set; }
		public decimal? cost_tax_land_fact_3 { get; set; }
		public decimal? cost_tax_land_plan_1 { get; set; }
		public decimal? cost_tax_land_plan_2 { get; set; }
		public decimal? cost_tax_land_plan_3 { get; set; }
		public decimal? cost_tax_transport_fact_1 { get; set; }
		public decimal? cost_tax_transport_fact_2 { get; set; }
		public decimal? cost_tax_transport_fact_3 { get; set; }
		public decimal? cost_tax_transport_plan_1 { get; set; }
		public decimal? cost_tax_transport_plan_2 { get; set; }
		public decimal? cost_tax_transport_plan_3 { get; set; }
		public decimal? cost_tax_water_fact_1 { get; set; }
		public decimal? cost_tax_water_fact_2 { get; set; }
		public decimal? cost_tax_water_fact_3 { get; set; }
		public decimal? cost_tax_water_plan_1 { get; set; }
		public decimal? cost_tax_water_plan_2 { get; set; }
		public decimal? cost_tax_water_plan_3 { get; set; }
		public decimal? cost_tax_other_fact_1 { get; set; }
		public decimal? cost_tax_other_fact_2 { get; set; }
		public decimal? cost_tax_other_fact_3 { get; set; }
		public decimal? cost_tax_other_plan_1 { get; set; }
		public decimal? cost_tax_other_plan_2 { get; set; }
		public decimal? cost_tax_other_plan_3 { get; set; }
		public decimal? cost_other_pay_fact_1 { get; set; }
		public decimal? cost_other_pay_fact_2 { get; set; }
		public decimal? cost_other_pay_fact_3 { get; set; }
		public decimal? cost_other_pay_plan_1 { get; set; }
		public decimal? cost_other_pay_plan_2 { get; set; }
		public decimal? cost_other_pay_plan_3 { get; set; }

	}

	[Keyless]
	public class TZAmortizationCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_amortization_all_fact_1 { get; set; }
		public decimal? cost_amortization_all_fact_2 { get; set; }
		public decimal? cost_amortization_all_fact_3 { get; set; }
		public decimal? cost_amortization_all_plan_1 { get; set; }
		public decimal? cost_amortization_all_plan_2 { get; set; }
		public decimal? cost_amortization_all_plan_3 { get; set; }
		public decimal? amortization_prod_equip_fact_1 { get; set; }
		public decimal? amortization_prod_equip_fact_2 { get; set; }
		public decimal? amortization_prod_equip_fact_3 { get; set; }
		public decimal? amortization_prod_equip_plan_1 { get; set; }
		public decimal? amortization_prod_equip_plan_2 { get; set; }
		public decimal? amortization_prod_equip_plan_3 { get; set; }
		public decimal? amortization_other_means_fact_1 { get; set; }
		public decimal? amortization_other_means_fact_2 { get; set; }
		public decimal? amortization_other_means_fact_3 { get; set; }
		public decimal? amortization_other_means_plan_1 { get; set; }
		public decimal? amortization_other_means_plan_2 { get; set; }
		public decimal? amortization_other_means_plan_3 { get; set; }

	}

	[Keyless]
	public class TZRegOrgServiceCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_reg_org_service_fact_1 { get; set; }
		public decimal? cost_reg_org_service_fact_2 { get; set; }
		public decimal? cost_reg_org_service_fact_3 { get; set; }
		public decimal? cost_reg_org_service_plan_1 { get; set; }
		public decimal? cost_reg_org_service_plan_2 { get; set; }
		public decimal? cost_reg_org_service_plan_3 { get; set; }

	}

	[Keyless]
	public class TZIncomeTaxCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_income_tax_fact_1 { get; set; }
		public decimal? cost_income_tax_fact_2 { get; set; }
		public decimal? cost_income_tax_fact_3 { get; set; }
		public decimal? cost_income_tax_plan_1 { get; set; }
		public decimal? cost_income_tax_plan_2 { get; set; }
		public decimal? cost_income_tax_plan_3 { get; set; }

	}

	[Keyless]
	public class TZUncontrolOtherCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_concession_fact_1 { get; set; }
		public decimal? cost_concession_fact_2 { get; set; }
		public decimal? cost_concession_fact_3 { get; set; }
		public decimal? cost_concession_plan_1 { get; set; }
		public decimal? cost_concession_plan_2 { get; set; }
		public decimal? cost_concession_plan_3 { get; set; }
		public decimal? cost_doubtful_debt_fact_1 { get; set; }
		public decimal? cost_doubtful_debt_fact_2 { get; set; }
		public decimal? cost_doubtful_debt_fact_3 { get; set; }
		public decimal? cost_doubtful_debt_plan_1 { get; set; }
		public decimal? cost_doubtful_debt_plan_2 { get; set; }
		public decimal? cost_doubtful_debt_plan_3 { get; set; }
		public decimal? cost_credit_contracts_fact_1 { get; set; }
		public decimal? cost_credit_contracts_fact_2 { get; set; }
		public decimal? cost_credit_contracts_fact_3 { get; set; }
		public decimal? cost_credit_contracts_plan_1 { get; set; }
		public decimal? cost_credit_contracts_plan_2 { get; set; }
		public decimal? cost_credit_contracts_plan_3 { get; set; }
		public decimal? rent_prod_obj_state_prop_fact_1 { get; set; }
		public decimal? rent_prod_obj_state_prop_fact_2 { get; set; }
		public decimal? rent_prod_obj_state_prop_fact_3 { get; set; }
		public decimal? rent_prod_obj_state_prop_plan_1 { get; set; }
		public decimal? rent_prod_obj_state_prop_plan_2 { get; set; }
		public decimal? rent_prod_obj_state_prop_plan_3 { get; set; }
		public decimal? rent_prod_obj_all_fact_1 { get; set; }
		public decimal? rent_prod_obj_all_fact_2 { get; set; }
		public decimal? rent_prod_obj_all_fact_3 { get; set; }
		public decimal? rent_prod_obj_all_plan_1 { get; set; }
		public decimal? rent_prod_obj_all_plan_2 { get; set; }
		public decimal? rent_prod_obj_all_plan_3 { get; set; }
		public decimal? rent_prod_obj_municipal_prop_fact_1 { get; set; }
		public decimal? rent_prod_obj_municipal_prop_fact_2 { get; set; }
		public decimal? rent_prod_obj_municipal_prop_fact_3 { get; set; }
		public decimal? rent_prod_obj_municipal_prop_plan_1 { get; set; }
		public decimal? rent_prod_obj_municipal_prop_plan_2 { get; set; }
		public decimal? rent_prod_obj_municipal_prop_plan_3 { get; set; }
		public decimal? rent_prod_obj_other_fact_1 { get; set; }
		public decimal? rent_prod_obj_other_fact_2 { get; set; }
		public decimal? rent_prod_obj_other_fact_3 { get; set; }
		public decimal? rent_prod_obj_other_plan_1 { get; set; }
		public decimal? rent_prod_obj_other_plan_2 { get; set; }
		public decimal? rent_prod_obj_other_plan_3 { get; set; }
		public decimal? leasing_obj_all_fact_1 { get; set; }
		public decimal? leasing_obj_all_fact_2 { get; set; }
		public decimal? leasing_obj_all_fact_3 { get; set; }
		public decimal? leasing_obj_all_plan_1 { get; set; }
		public decimal? leasing_obj_all_plan_2 { get; set; }
		public decimal? leasing_obj_all_plan_3 { get; set; }
		public decimal? leasing_prod_obj_fact_1 { get; set; }
		public decimal? leasing_prod_obj_fact_2 { get; set; }
		public decimal? leasing_prod_obj_fact_3 { get; set; }
		public decimal? leasing_prod_obj_plan_1 { get; set; }
		public decimal? leasing_prod_obj_plan_2 { get; set; }
		public decimal? leasing_prod_obj_plan_3 { get; set; }
		public decimal? leasing_nonprod_obj_with_own_rights_fact_1 { get; set; }
		public decimal? leasing_nonprod_obj_with_own_rights_fact_2 { get; set; }
		public decimal? leasing_nonprod_obj_with_own_rights_fact_3 { get; set; }
		public decimal? leasing_nonprod_obj_with_own_rights_plan_1 { get; set; }
		public decimal? leasing_nonprod_obj_with_own_rights_plan_2 { get; set; }
		public decimal? leasing_nonprod_obj_with_own_rights_plan_3 { get; set; }
		public decimal? cost_uncontrol_other_fact_1 { get; set; }
		public decimal? cost_uncontrol_other_fact_2 { get; set; }
		public decimal? cost_uncontrol_other_fact_3 { get; set; }
		public decimal? cost_uncontrol_other_plan_1 { get; set; }
		public decimal? cost_uncontrol_other_plan_2 { get; set; }
		public decimal? cost_uncontrol_other_plan_3 { get; set; }

	}

	[Keyless]
	public class TZSocialDeductionCostsViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? cost_social_deduction_all_fact_1 { get; set; }
		public decimal? cost_social_deduction_all_fact_2 { get; set; }
		public decimal? cost_social_deduction_all_fact_3 { get; set; }
		public decimal? cost_social_deduction_all_plan_1 { get; set; }
		public decimal? cost_social_deduction_all_plan_2 { get; set; }
		public decimal? cost_social_deduction_all_plan_3 { get; set; }
		public decimal? cost_social_deduction_prod_fact_1 { get; set; }
		public decimal? cost_social_deduction_prod_fact_2 { get; set; }
		public decimal? cost_social_deduction_prod_fact_3 { get; set; }
		public decimal? cost_social_deduction_prod_plan_1 { get; set; }
		public decimal? cost_social_deduction_prod_plan_2 { get; set; }
		public decimal? cost_social_deduction_prod_plan_3 { get; set; }
		public decimal? cost_social_deduction_repair_fact_1 { get; set; }
		public decimal? cost_social_deduction_repair_fact_2 { get; set; }
		public decimal? cost_social_deduction_repair_fact_3 { get; set; }
		public decimal? cost_social_deduction_repair_plan_1 { get; set; }
		public decimal? cost_social_deduction_repair_plan_2 { get; set; }
		public decimal? cost_social_deduction_repair_plan_3 { get; set; }
		public decimal? cost_social_deduction_shop_fact_1 { get; set; }
		public decimal? cost_social_deduction_shop_fact_2 { get; set; }
		public decimal? cost_social_deduction_shop_fact_3 { get; set; }
		public decimal? cost_social_deduction_shop_plan_1 { get; set; }
		public decimal? cost_social_deduction_shop_plan_2 { get; set; }
		public decimal? cost_social_deduction_shop_plan_3 { get; set; }
		public decimal? cost_social_deduction_manage_fact_1 { get; set; }
		public decimal? cost_social_deduction_manage_fact_2 { get; set; }
		public decimal? cost_social_deduction_manage_fact_3 { get; set; }
		public decimal? cost_social_deduction_manage_plan_1 { get; set; }
		public decimal? cost_social_deduction_manage_plan_2 { get; set; }
		public decimal? cost_social_deduction_manage_plan_3 { get; set; }
		public decimal? cost_social_deduction_other_fact_1 { get; set; }
		public decimal? cost_social_deduction_other_fact_2 { get; set; }
		public decimal? cost_social_deduction_other_fact_3 { get; set; }
		public decimal? cost_social_deduction_other_plan_1 { get; set; }
		public decimal? cost_social_deduction_other_plan_2 { get; set; }
		public decimal? cost_social_deduction_other_plan_3 { get; set; }
		public decimal? avg_count_workers_fact_1 { get; set; }
		public decimal? avg_count_workers_fact_2 { get; set; }
		public decimal? avg_count_workers_fact_3 { get; set; }
		public decimal? avg_count_workers_plan_1 { get; set; }
		public decimal? avg_count_workers_plan_2 { get; set; }
		public decimal? avg_count_workers_plan_3 { get; set; }
		public decimal? avg_salary_fact_1 { get; set; }
		public decimal? avg_salary_fact_2 { get; set; }
		public decimal? avg_salary_fact_3 { get; set; }
		public decimal? avg_salary_plan_1 { get; set; }
		public decimal? avg_salary_plan_2 { get; set; }
		public decimal? avg_salary_plan_3 { get; set; }
		public decimal? avg_procent_deduction_fact_1 { get; set; }
		public decimal? avg_procent_deduction_fact_2 { get; set; }
		public decimal? avg_procent_deduction_fact_3 { get; set; }
		public decimal? avg_procent_deduction_plan_1 { get; set; }
		public decimal? avg_procent_deduction_plan_2 { get; set; }
		public decimal? avg_procent_deduction_plan_3 { get; set; }
		

	}
}