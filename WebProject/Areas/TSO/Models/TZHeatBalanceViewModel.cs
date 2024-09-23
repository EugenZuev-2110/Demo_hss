using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Areas.TSO.Models
{
    [Keyless]
    public class TZProductionDataViewModel
    {
        public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
        public string? unom_tz { get; set; }
		public string? territory { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public string? activity_types { get; set; }
		public decimal generate_heat_energy_fact { get; set; }
		public decimal generate_heat_energy_plan { get; set; }
		public decimal heat_consump_fact { get; set; }
		public decimal heat_consump_plan { get; set; }
		public decimal release_collect_fact { get; set; }
		public decimal release_collect_tech_need_fact { get; set; }
		public decimal release_collect_ownprod_fact { get; set; }
		public decimal release_collect_household_needs_fact { get; set; }
		public decimal release_collect_buget_cons_fact { get; set; }
		public decimal release_collect_public_fact { get; set; }
		public decimal release_collect_other_cons_fact { get; set; }
		public decimal release_collect_tso_salers_fact { get; set; }
		public decimal release_collect_ownheatnet_fact { get; set; }
		public decimal release_collect_plan { get; set; }
		public decimal release_collect_tech_need_plan { get; set; }
		public decimal release_collect_ownprod_plan { get; set; }
		public decimal release_collect_household_needs_plan { get; set; }
		public decimal release_collect_buget_cons_plan { get; set; }
		public decimal release_collect_public_plan { get; set; }
		public decimal release_collect_other_cons_plan { get; set; }
		public decimal release_collect_tso_salers_plan { get; set; }
		public decimal release_collect_ownheatnet_plan { get; set; }
		public decimal buy_energy_fact { get; set; }
		public decimal buy_energy_collect_fact { get; set; }
		public decimal buy_energy_collect_more25_fact { get; set; }
		public decimal buy_energy_collect_less25_fact { get; set; }
		public decimal buy_energy_collect_boiler_fact { get; set; }
		public decimal buy_energy_heatnet_fact { get; set; }
		public decimal buy_energy_heatnet_more25_fact { get; set; }
		public decimal buy_energy_heatnet_less25_fact { get; set; }
		public decimal buy_energy_heatnet_boiler_fact { get; set; }
		public decimal buy_energy_plan { get; set; }
		public decimal buy_energy_collect_plan { get; set; }
		public decimal buy_energy_collect_more25_plan { get; set; }
		public decimal buy_energy_collect_less25_plan { get; set; }
		public decimal buy_energy_collect_boiler_plan { get; set; }
		public decimal buy_energy_heatnet_plan { get; set; }
		public decimal buy_energy_heatnet_more25_plan { get; set; }
		public decimal buy_energy_heatnet_less25_plan { get; set; }
		public decimal buy_energy_heatnet_boiler_plan { get; set; }
		public decimal release_heat_energy_network_fact { get; set; }
		public decimal release_heat_energy_network_plan { get; set; }
		public decimal heatenrg_loss_heatnet_fact { get; set; }
		public decimal heatenrg_loss_heatnet_plan { get; set; }
		public decimal useful_release_fact { get; set; }
		public decimal useful_release_company_needs_fact { get; set; }
		public decimal useful_release_ownprod_fact { get; set; }
		public decimal useful_release_household_needs_fact { get; set; }
		public decimal useful_release_tso_salers_fact { get; set; }
		public decimal useful_release_cons_groups_fact { get; set; }
		public decimal useful_release_buget_cons_fact { get; set; }
		public decimal useful_release_public_fact { get; set; }
		public decimal useful_release_other_cons_fact { get; set; }
		public decimal useful_release_plan { get; set; }
		public decimal useful_release_company_needs_plan { get; set; }
		public decimal useful_release_ownprod_plan { get; set; }
		public decimal useful_release_household_needs_plan { get; set; }
		public decimal useful_release_tso_salers_plan { get; set; }
		public decimal useful_release_cons_groups_plan { get; set; }
		public decimal useful_release_buget_cons_plan { get; set; }
		public decimal useful_release_public_plan { get; set; }
		public decimal useful_release_other_cons_plan { get; set; }
		public decimal volume_notbalance_heat_fact { get; set; }
		public string? notes_fact { get; set; }
		public decimal volume_notbalance_heat_plan { get; set; }
		public string? notes_plan { get; set; }

	}

	[Keyless]
	public class TZTransferDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public string? unom_tz { get; set; }
		public string? territory { get; set; }
		public string? code_tso { get; set; }
		public string? short_name { get; set; }
		public string? activity_types { get; set; }
		public decimal accept_heatenrg_transport_fact { get; set; }
		public decimal accept_heatenrg_transport_plan { get; set; }
		public decimal heatenrg_loss_heatnet_fact { get; set; }
		public decimal heatenrg_loss_heatnet_plan { get; set; }
		public decimal useful_release_fact { get; set; }
		public decimal useful_release_company_needs_fact { get; set; }
		public decimal useful_release_ownprod_fact { get; set; }
		public decimal useful_release_household_needs_fact { get; set; }
		public decimal useful_release_tso_salers_fact { get; set; }
		public decimal useful_release_cons_groups_fact { get; set; }
		public decimal useful_release_buget_cons_fact { get; set; }
		public decimal useful_release_public_fact { get; set; }
		public decimal useful_release_other_cons_fact { get; set; }
		public decimal useful_release_plan { get; set; }
		public decimal useful_release_company_needs_plan { get; set; }
		public decimal useful_release_ownprod_plan { get; set; }
		public decimal useful_release_household_needs_plan { get; set; }
		public decimal useful_release_tso_salers_plan { get; set; }
		public decimal useful_release_cons_groups_plan { get; set; }
		public decimal useful_release_buget_cons_plan { get; set; }
		public decimal useful_release_public_plan { get; set; }
		public decimal useful_release_other_cons_plan { get; set; }
		public decimal volume_notbalance_heat_fact { get; set; }
		public string? notes_fact { get; set; }
		public decimal volume_notbalance_heat_plan { get; set; }
		public string? notes_plan { get; set; }

	}

	[Keyless]
	public class TZOneProductionDataViewModel
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
		public decimal? generate_heat_energy_fact_1 { get; set; }
		public decimal? generate_heat_energy_fact_2 { get; set; }
		public decimal? generate_heat_energy_fact_3 { get; set; }
		public decimal? generate_heat_energy_plan_1 { get; set; }
		public decimal? generate_heat_energy_plan_2 { get; set; }
		public decimal? generate_heat_energy_plan_3 { get; set; }
		public decimal? heat_consump_fact_1 { get; set; }
		public decimal? heat_consump_fact_2 { get; set; }
		public decimal? heat_consump_fact_3 { get; set; }
		public decimal? heat_consump_plan_1 { get; set; }
		public decimal? heat_consump_plan_2 { get; set; }
		public decimal? heat_consump_plan_3 { get; set; }
		public decimal? release_collect_fact_1 { get; set; }
		public decimal? release_collect_fact_2 { get; set; }
		public decimal? release_collect_fact_3 { get; set; }
		public decimal? release_collect_plan_1 { get; set; }
		public decimal? release_collect_plan_2 { get; set; }
		public decimal? release_collect_plan_3 { get; set; }
		public decimal? release_collect_tech_need_fact_1 { get; set; }
		public decimal? release_collect_tech_need_fact_2 { get; set; }
		public decimal? release_collect_tech_need_fact_3 { get; set; }
		public decimal? release_collect_tech_need_plan_1 { get; set; }
		public decimal? release_collect_tech_need_plan_2 { get; set; }
		public decimal? release_collect_tech_need_plan_3 { get; set; }
		public decimal? release_collect_ownprod_fact_1 { get; set; }
		public decimal? release_collect_ownprod_fact_2 { get; set; }
		public decimal? release_collect_ownprod_fact_3 { get; set; }
		public decimal? release_collect_ownprod_plan_1 { get; set; }
		public decimal? release_collect_ownprod_plan_2 { get; set; }
		public decimal? release_collect_ownprod_plan_3 { get; set; }
		public decimal? release_collect_household_needs_fact_1 { get; set; }
		public decimal? release_collect_household_needs_fact_2 { get; set; }
		public decimal? release_collect_household_needs_fact_3 { get; set; }
		public decimal? release_collect_household_needs_plan_1 { get; set; }
		public decimal? release_collect_household_needs_plan_2 { get; set; }
		public decimal? release_collect_household_needs_plan_3 { get; set; }
		public decimal? release_collect_tso_salers_fact_1 { get; set; }
		public decimal? release_collect_tso_salers_fact_2 { get; set; }
		public decimal? release_collect_tso_salers_fact_3 { get; set; }
		public decimal? release_collect_tso_salers_plan_1 { get; set; }
		public decimal? release_collect_tso_salers_plan_2 { get; set; }
		public decimal? release_collect_tso_salers_plan_3 { get; set; }
		public decimal? release_collect_consumers_fact_1 { get; set; }
		public decimal? release_collect_consumers_fact_2 { get; set; }
		public decimal? release_collect_consumers_fact_3 { get; set; }
		public decimal? release_collect_consumers_plan_1 { get; set; }
		public decimal? release_collect_consumers_plan_2 { get; set; }
		public decimal? release_collect_consumers_plan_3 { get; set; }
		public decimal? release_collect_buget_cons_fact_1 { get; set; }
		public decimal? release_collect_buget_cons_fact_2 { get; set; }
		public decimal? release_collect_buget_cons_fact_3 { get; set; }
		public decimal? release_collect_buget_cons_plan_1 { get; set; }
		public decimal? release_collect_buget_cons_plan_2 { get; set; }
		public decimal? release_collect_buget_cons_plan_3 { get; set; }
		public decimal? release_collect_public_fact_1 { get; set; }
		public decimal? release_collect_public_fact_2 { get; set; }
		public decimal? release_collect_public_fact_3 { get; set; }
		public decimal? release_collect_public_plan_1 { get; set; }
		public decimal? release_collect_public_plan_2 { get; set; }
		public decimal? release_collect_public_plan_3 { get; set; }
		public decimal? release_collect_other_cons_fact_1 { get; set; }
		public decimal? release_collect_other_cons_fact_2 { get; set; }
		public decimal? release_collect_other_cons_fact_3 { get; set; }
		public decimal? release_collect_other_cons_plan_1 { get; set; }
		public decimal? release_collect_other_cons_plan_2 { get; set; }
		public decimal? release_collect_other_cons_plan_3 { get; set; }
		public decimal? release_collect_ownheatnet_fact_1 { get; set; }
		public decimal? release_collect_ownheatnet_fact_2 { get; set; }
		public decimal? release_collect_ownheatnet_fact_3 { get; set; }
		public decimal? release_collect_ownheatnet_plan_1 { get; set; }
		public decimal? release_collect_ownheatnet_plan_2 { get; set; }
		public decimal? release_collect_ownheatnet_plan_3 { get; set; }
		public decimal? buy_energy_collect_fact_1 { get; set; }
		public decimal? buy_energy_collect_fact_2 { get; set; }
		public decimal? buy_energy_collect_fact_3 { get; set; }
		public decimal? buy_energy_collect_plan_1 { get; set; }
		public decimal? buy_energy_collect_plan_2 { get; set; }
		public decimal? buy_energy_collect_plan_3 { get; set; }
		public decimal? buy_loss_collect_fact_1 { get; set; }
		public decimal? buy_loss_collect_fact_2 { get; set; }
		public decimal? buy_loss_collect_fact_3 { get; set; }
		public decimal? buy_loss_collect_plan_1 { get; set; }
		public decimal? buy_loss_collect_plan_2 { get; set; }
		public decimal? buy_loss_collect_plan_3 { get; set; }
		public decimal? buy_energy_heatnet_fact_1 { get; set; }
		public decimal? buy_energy_heatnet_fact_2 { get; set; }
		public decimal? buy_energy_heatnet_fact_3 { get; set; }
		public decimal? buy_energy_heatnet_plan_1 { get; set; }
		public decimal? buy_energy_heatnet_plan_2 { get; set; }
		public decimal? buy_energy_heatnet_plan_3 { get; set; }
		public decimal? buy_loss_heatnet_fact_1 { get; set; }
		public decimal? buy_loss_heatnet_fact_2 { get; set; }
		public decimal? buy_loss_heatnet_fact_3 { get; set; }
		public decimal? buy_loss_heatnet_plan_1 { get; set; }
		public decimal? buy_loss_heatnet_plan_2 { get; set; }
		public decimal? buy_loss_heatnet_plan_3 { get; set; }
		public decimal? release_heat_energy_network_fact_1 { get; set; }
		public decimal? release_heat_energy_network_fact_2 { get; set; }
		public decimal? release_heat_energy_network_fact_3 { get; set; }
		public decimal? release_heat_energy_network_plan_1 { get; set; }
		public decimal? release_heat_energy_network_plan_2 { get; set; }
		public decimal? release_heat_energy_network_plan_3 { get; set; }
		public decimal? heatenrg_loss_heatnet_fact_1 { get; set; }
		public decimal? heatenrg_loss_heatnet_fact_2 { get; set; }
		public decimal? heatenrg_loss_heatnet_fact_3 { get; set; }
		public decimal? heatenrg_loss_heatnet_plan_1 { get; set; }
		public decimal? heatenrg_loss_heatnet_plan_2 { get; set; }
		public decimal? heatenrg_loss_heatnet_plan_3 { get; set; }
		public decimal? useful_release_fact_1 { get; set; }
		public decimal? useful_release_fact_2 { get; set; }
		public decimal? useful_release_fact_3 { get; set; }
		public decimal? useful_release_plan_1 { get; set; }
		public decimal? useful_release_plan_2 { get; set; }
		public decimal? useful_release_plan_3 { get; set; }
		public decimal? useful_release_company_needs_fact_1 { get; set; }
		public decimal? useful_release_company_needs_fact_2 { get; set; }
		public decimal? useful_release_company_needs_fact_3 { get; set; }
		public decimal? useful_release_company_needs_plan_1 { get; set; }
		public decimal? useful_release_company_needs_plan_2 { get; set; }
		public decimal? useful_release_company_needs_plan_3 { get; set; }
		public decimal? useful_release_ownprod_fact_1 { get; set; }
		public decimal? useful_release_ownprod_fact_2 { get; set; }
		public decimal? useful_release_ownprod_fact_3 { get; set; }
		public decimal? useful_release_ownprod_plan_1 { get; set; }
		public decimal? useful_release_ownprod_plan_2 { get; set; }
		public decimal? useful_release_ownprod_plan_3 { get; set; }
		public decimal? useful_release_household_needs_fact_1 { get; set; }
		public decimal? useful_release_household_needs_fact_2 { get; set; }
		public decimal? useful_release_household_needs_fact_3 { get; set; }
		public decimal? useful_release_household_needs_plan_1 { get; set; }
		public decimal? useful_release_household_needs_plan_2 { get; set; }
		public decimal? useful_release_household_needs_plan_3 { get; set; }
		public decimal? useful_release_tso_salers_fact_1 { get; set; }
		public decimal? useful_release_tso_salers_fact_2 { get; set; }
		public decimal? useful_release_tso_salers_fact_3 { get; set; }
		public decimal? useful_release_tso_salers_plan_1 { get; set; }
		public decimal? useful_release_tso_salers_plan_2 { get; set; }
		public decimal? useful_release_tso_salers_plan_3 { get; set; }
		public decimal? useful_release_cons_groups_fact_1 { get; set; }
		public decimal? useful_release_cons_groups_fact_2 { get; set; }
		public decimal? useful_release_cons_groups_fact_3 { get; set; }
		public decimal? useful_release_cons_groups_plan_1 { get; set; }
		public decimal? useful_release_cons_groups_plan_2 { get; set; }
		public decimal? useful_release_cons_groups_plan_3 { get; set; }
		public decimal? useful_release_buget_cons_fact_1 { get; set; }
		public decimal? useful_release_buget_cons_fact_2 { get; set; }
		public decimal? useful_release_buget_cons_fact_3 { get; set; }
		public decimal? useful_release_buget_cons_plan_1 { get; set; }
		public decimal? useful_release_buget_cons_plan_2 { get; set; }
		public decimal? useful_release_buget_cons_plan_3 { get; set; }
		public decimal? useful_release_public_fact_1 { get; set; }
		public decimal? useful_release_public_fact_2 { get; set; }
		public decimal? useful_release_public_fact_3 { get; set; }
		public decimal? useful_release_public_plan_1 { get; set; }
		public decimal? useful_release_public_plan_2 { get; set; }
		public decimal? useful_release_public_plan_3 { get; set; }
		public decimal? useful_release_other_cons_fact_1 { get; set; }
		public decimal? useful_release_other_cons_fact_2 { get; set; }
		public decimal? useful_release_other_cons_fact_3 { get; set; }
		public decimal? useful_release_other_cons_plan_1 { get; set; }
		public decimal? useful_release_other_cons_plan_2 { get; set; }
		public decimal? useful_release_other_cons_plan_3 { get; set; }
		public string? notes_fact { get; set; }
		public string? notes_plan { get; set; }

	}

	[Keyless]
	public class TZInOutEnergyPlanListViewModel
	{
		public int tz_id { get; set; }
		public int perspective_year { get; set; }
		public string perspective_year_dt { get; set; }
		public string? unom_tz { get; set; }
		public string? buy_place_short { get; set; }
		public string? tso_name { get; set; }
		public decimal? buy_energy_plan { get; set; }

	}

	[Keyless]
	public class TZOutputEnergyDataViewModel
	{
		public int tz_id { get; set; }
		public string tz_tso_name { get; set; }
		public int perspective_year { get; set; }
		public int data_status { get; set; }
		public decimal? release_collect_tso_salers_fact_1 { get; set; }
		public decimal? release_collect_tso_salers_fact_2 { get; set; }
		public decimal? release_collect_tso_salers_fact_3 { get; set; }
		public decimal? release_collect_tso_salers_plan_1 { get; set; }
		public decimal? release_collect_tso_salers_plan_2 { get; set; }
		public decimal? release_collect_tso_salers_plan_3 { get; set; }
		public decimal? release_heatnet_tso_salers_fact_1 { get; set; }
		public decimal? release_heatnet_tso_salers_fact_2 { get; set; }
		public decimal? release_heatnet_tso_salers_fact_3 { get; set; }
		public decimal? release_heatnet_tso_salers_plan_1 { get; set; }
		public decimal? release_heatnet_tso_salers_plan_2 { get; set; }
		public decimal? release_heatnet_tso_salers_plan_3 { get; set; }
		public decimal? release_tso_salers_sum_fact_1 { get; set; }
		public decimal? release_tso_salers_sum_fact_2 { get; set; }
		public decimal? release_tso_salers_sum_fact_3 { get; set; }
		public decimal? release_tso_salers_sum_plan_1 { get; set; }
		public decimal? release_tso_salers_sum_plan_2 { get; set; }
		public decimal? release_tso_salers_sum_plan_3 { get; set; }

	}

	[Keyless]
	public class TZOutputEnergySourcesListDataViewModel
	{
		public int output_tz_id { get; set; }
		public int input_tz_id { get; set; }
		//public string? input_unom_tz { get; set; }
		//public string tso_short_name { get; set; }
		public string? tz_tso_name { get; set; }
		public int source_id { get; set; }
		public string unom_source_name { get; set; }
		public short buy_place_id { get; set; }
		public string buy_place_name { get; set; }
		public int perspective_year { get; set; }
		public int data_status { get; set; }
		public decimal? buy_energy_fact_1 { get; set; }
		public decimal? buy_energy_fact_2 { get; set; }
		public decimal? buy_energy_fact_3 { get; set; }
		public decimal? buy_energy_plan_1 { get; set; }
		public decimal? buy_energy_plan_2 { get; set; }
		public decimal? buy_energy_plan_3 { get; set; }

	}

	[Keyless]
	public class TZOutputEnergySourceOneDataViewModel
	{
		public short db_action { get; set; } // 0 - insert, 1 - update
		public int output_tz_id { get; set; }
		public int input_tz_id { get; set; }
		public int old_input_tz_id { get; set; }
		public int source_id { get; set; }
		public int old_source_id { get; set; }
		public short buy_place_id { get; set; }
		public short old_buy_place_id { get; set; }
		public int perspective_year { get; set; }
		public int data_status { get; set; }
		public decimal? buy_energy_fact_1 { get; set; }
		public decimal? buy_energy_fact_2 { get; set; }
		public decimal? buy_energy_fact_3 { get; set; }
		public decimal? buy_energy_plan_1 { get; set; }
		public decimal? buy_energy_plan_2 { get; set; }
		public decimal? buy_energy_plan_3 { get; set; }
		public decimal? buy_energy_techneed_fact_1 { get; set; }
		public decimal? buy_energy_techneed_fact_2 { get; set; }
		public decimal? buy_energy_techneed_fact_3 { get; set; }
		public decimal? buy_energy_techneed_plan_1 { get; set; }
		public decimal? buy_energy_techneed_plan_2 { get; set; }
		public decimal? buy_energy_techneed_plan_3 { get; set; }
		public decimal? buy_loss_techneed_fact_1 { get; set; }
		public decimal? buy_loss_techneed_fact_2 { get; set; }
		public decimal? buy_loss_techneed_fact_3 { get; set; }
		public decimal? buy_loss_techneed_plan_1 { get; set; }
		public decimal? buy_loss_techneed_plan_2 { get; set; }
		public decimal? buy_loss_techneed_plan_3 { get; set; }
		public decimal? buy_energy_houseneed_fact_1 { get; set; }
		public decimal? buy_energy_houseneed_fact_2 { get; set; }
		public decimal? buy_energy_houseneed_fact_3 { get; set; }
		public decimal? buy_energy_houseneed_plan_1 { get; set; }
		public decimal? buy_energy_houseneed_plan_2 { get; set; }
		public decimal? buy_energy_houseneed_plan_3 { get; set; }
		public decimal? buy_loss_houseneed_fact_1 { get; set; }
		public decimal? buy_loss_houseneed_fact_2 { get; set; }
		public decimal? buy_loss_houseneed_fact_3 { get; set; }
		public decimal? buy_loss_houseneed_plan_1 { get; set; }
		public decimal? buy_loss_houseneed_plan_2 { get; set; }
		public decimal? buy_loss_houseneed_plan_3 { get; set; }

	}

	[Keyless]
	public class TZOneTransferDataViewModel
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public string? tz_territory { get; set; }
		public string? notes_fact { get; set; }
		public string? notes_plan { get; set; }
		public decimal? all_transfer_heat_energy_fact_1 { get; set; }
		public decimal? all_transfer_heat_energy_fact_2 { get; set; }
		public decimal? all_transfer_heat_energy_fact_3 { get; set; }
		public decimal? all_transfer_heat_energy_plan_1 { get; set; }
		public decimal? all_transfer_heat_energy_plan_2 { get; set; }
		public decimal? all_transfer_heat_energy_plan_3 { get; set; }
		public decimal? loss_heatnet_fact_1 { get; set; }
		public decimal? loss_heatnet_fact_2 { get; set; }
		public decimal? loss_heatnet_fact_3 { get; set; }
		public decimal? loss_heatnet_plan_1 { get; set; }
		public decimal? loss_heatnet_plan_2 { get; set; }
		public decimal? loss_heatnet_plan_3 { get; set; }
		public decimal? all_release_heat_energy_fact_1 { get; set; }
		public decimal? all_release_heat_energy_fact_2 { get; set; }
		public decimal? all_release_heat_energy_fact_3 { get; set; }
		public decimal? all_release_heat_energy_plan_1 { get; set; }
		public decimal? all_release_heat_energy_plan_2 { get; set; }
		public decimal? all_release_heat_energy_plan_3 { get; set; }
		public decimal? company_need_fact_1 { get; set; }
		public decimal? company_need_fact_2 { get; set; }
		public decimal? company_need_fact_3 { get; set; }
		public decimal? company_need_plan_1 { get; set; }
		public decimal? company_need_plan_2 { get; set; }
		public decimal? company_need_plan_3 { get; set; }
		public decimal? ownprod_fact_1 { get; set; }
		public decimal? ownprod_fact_2 { get; set; }
		public decimal? ownprod_fact_3 { get; set; }
		public decimal? ownprod_plan_1 { get; set; }
		public decimal? ownprod_plan_2 { get; set; }
		public decimal? ownprod_plan_3 { get; set; }
		public decimal? household_needs_fact_1 { get; set; }
		public decimal? household_needs_fact_2 { get; set; }
		public decimal? household_needs_fact_3 { get; set; }
		public decimal? household_needs_plan_1 { get; set; }
		public decimal? household_needs_plan_2 { get; set; }
		public decimal? household_needs_plan_3 { get; set; }
		public decimal? tso_fact_1 { get; set; }
		public decimal? tso_fact_2 { get; set; }
		public decimal? tso_fact_3 { get; set; }
		public decimal? tso_plan_1 { get; set; }
		public decimal? tso_plan_2 { get; set; }
		public decimal? tso_plan_3 { get; set; }
		public decimal? all_consumers_fact_1 { get; set; }
		public decimal? all_consumers_fact_2 { get; set; }
		public decimal? all_consumers_fact_3 { get; set; }
		public decimal? all_consumers_plan_1 { get; set; }
		public decimal? all_consumers_plan_2 { get; set; }
		public decimal? all_consumers_plan_3 { get; set; }
		public decimal? buget_consumers_fact_1 { get; set; }
		public decimal? buget_consumers_fact_2 { get; set; }
		public decimal? buget_consumers_fact_3 { get; set; }
		public decimal? buget_consumers_plan_1 { get; set; }
		public decimal? buget_consumers_plan_2 { get; set; }
		public decimal? buget_consumers_plan_3 { get; set; }
		public decimal? public_fact_1 { get; set; }
		public decimal? public_fact_2 { get; set; }
		public decimal? public_fact_3 { get; set; }
		public decimal? public_plan_1 { get; set; }
		public decimal? public_plan_2 { get; set; }
		public decimal? public_plan_3 { get; set; }
		public decimal? other_consumers_fact_1 { get; set; }
		public decimal? other_consumers_fact_2 { get; set; }
		public decimal? other_consumers_fact_3 { get; set; }
		public decimal? other_consumers_plan_1 { get; set; }
		public decimal? other_consumers_plan_2 { get; set; }
		public decimal? other_consumers_plan_3 { get; set; }
		
	}

	[Keyless]
	public class TZOutputTransferEnergyPlanListViewModel
	{
		public int tz_id { get; set; }
		public int perspective_year { get; set; }
		public string perspective_year_dt { get; set; }
		public string? unom_tz { get; set; }
		public string? tso_name { get; set; }
		public decimal? transfer_energy_plan { get; set; }

	}

	[Keyless]
	public class TZOutputTransferEnergyDataViewModel
	{
		public int tz_id { get; set; }
		public string tz_tso_name { get; set; }
		public int perspective_year { get; set; }
		public int data_status { get; set; }
		public decimal? all_transfer_heat_energy_fact_1 { get; set; }
		public decimal? all_transfer_heat_energy_fact_2 { get; set; }
		public decimal? all_transfer_heat_energy_fact_3 { get; set; }
		public decimal? all_transfer_heat_energy_plan_1 { get; set; }
		public decimal? all_transfer_heat_energy_plan_2 { get; set; }
		public decimal? all_transfer_heat_energy_plan_3 { get; set; }

	}

	[Keyless]
	public class TZOutputTransferEnergySourcesListDataViewModel
	{
		public int output_tz_id { get; set; }
		public int input_tz_id { get; set; }
		//public string? input_unom_tz { get; set; }
		//public string tso_short_name { get; set; }
		public string? tz_tso_name { get; set; }
		public int source_id { get; set; }
		public string unom_source_name { get; set; }
		public int perspective_year { get; set; }
		public int data_status { get; set; }
		public decimal? source_transfer_heat_energy_fact_1 { get; set; }
		public decimal? source_transfer_heat_energy_fact_2 { get; set; }
		public decimal? source_transfer_heat_energy_fact_3 { get; set; }
		public decimal? source_transfer_heat_energy_plan_1 { get; set; }
		public decimal? source_transfer_heat_energy_plan_2 { get; set; }
		public decimal? source_transfer_heat_energy_plan_3 { get; set; }

	}

	[Keyless]
	public class TZOutputTransferEnergySourceOneDataViewModel
	{
		public short db_action { get; set; } // 0 - insert, 1 - update
		public int output_tz_id { get; set; }
		public int input_tz_id { get; set; }
		public int old_input_tz_id { get; set; }
		public int source_id { get; set; }
		public int old_source_id { get; set; }
		public int perspective_year { get; set; }
		public int data_status { get; set; }
		public decimal? source_transfer_heat_energy_fact_1 { get; set; }
		public decimal? source_transfer_heat_energy_fact_2 { get; set; }
		public decimal? source_transfer_heat_energy_fact_3 { get; set; }
		public decimal? source_transfer_heat_energy_plan_1 { get; set; }
		public decimal? source_transfer_heat_energy_plan_2 { get; set; }
		public decimal? source_transfer_heat_energy_plan_3 { get; set; }
		public decimal? source_loss_heatnet_fact_1 { get; set; }
		public decimal? source_loss_heatnet_fact_2 { get; set; }
		public decimal? source_loss_heatnet_fact_3 { get; set; }
		public decimal? source_loss_heatnet_plan_1 { get; set; }
		public decimal? source_loss_heatnet_plan_2 { get; set; }
		public decimal? source_loss_heatnet_plan_3 { get; set; }
		public decimal? source_company_need_fact_1 { get; set; }
		public decimal? source_company_need_fact_2 { get; set; }
		public decimal? source_company_need_fact_3 { get; set; }
		public decimal? source_company_need_plan_1 { get; set; }
		public decimal? source_company_need_plan_2 { get; set; }
		public decimal? source_company_need_plan_3 { get; set; }
		public decimal? source_ownprod_fact_1 { get; set; }
		public decimal? source_ownprod_fact_2 { get; set; }
		public decimal? source_ownprod_fact_3 { get; set; }
		public decimal? source_ownprod_plan_1 { get; set; }
		public decimal? source_ownprod_plan_2 { get; set; }
		public decimal? source_ownprod_plan_3 { get; set; }
		public decimal? source_household_needs_fact_1 { get; set; }
		public decimal? source_household_needs_fact_2 { get; set; }
		public decimal? source_household_needs_fact_3 { get; set; }
		public decimal? source_household_needs_plan_1 { get; set; }
		public decimal? source_household_needs_plan_2 { get; set; }
		public decimal? source_household_needs_plan_3 { get; set; }
		public decimal? source_tso_fact_1 { get; set; }
		public decimal? source_tso_fact_2 { get; set; }
		public decimal? source_tso_fact_3 { get; set; }
		public decimal? source_tso_plan_1 { get; set; }
		public decimal? source_tso_plan_2 { get; set; }
		public decimal? source_tso_plan_3 { get; set; }
		public decimal? source_buget_consumers_fact_1 { get; set; }
		public decimal? source_buget_consumers_fact_2 { get; set; }
		public decimal? source_buget_consumers_fact_3 { get; set; }
		public decimal? source_buget_consumers_plan_1 { get; set; }
		public decimal? source_buget_consumers_plan_2 { get; set; }
		public decimal? source_buget_consumers_plan_3 { get; set; }
		public decimal? source_public_fact_1 { get; set; }
		public decimal? source_public_fact_2 { get; set; }
		public decimal? source_public_fact_3 { get; set; }
		public decimal? source_public_plan_1 { get; set; }
		public decimal? source_public_plan_2 { get; set; }
		public decimal? source_public_plan_3 { get; set; }
		public decimal? source_other_consumers_fact_1 { get; set; }
		public decimal? source_other_consumers_fact_2 { get; set; }
		public decimal? source_other_consumers_fact_3 { get; set; }
		public decimal? source_other_consumers_plan_1 { get; set; }
		public decimal? source_other_consumers_plan_2 { get; set; }
		public decimal? source_other_consumers_plan_3 { get; set; }

	}
}