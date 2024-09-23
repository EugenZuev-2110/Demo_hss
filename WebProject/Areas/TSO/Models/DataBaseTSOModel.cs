using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models.TSO
{
    [Table("Dict_ActivityTypes", Schema = "org")]
    public class Dict_ActivityTypes
    {
        [Key]
        public short Id { get; set; }
        public string? activity_type_name { get; set; }

    }

    [Table("Dict_OrgStatuses", Schema = "org")]
    public class Dict_OrgStatuses
    {
        [Key]
        public short Id { get; set; }
        public string? org_status_name { get; set; }

    }
    
    [Table("ResponsiblePersons", Schema = "org")]
    public class ResponsiblePersons
    {
        public int org_id { get; set; }
        public int data_status { get; set; }
        public int person_num { get; set; }
        public string? position_respons_pers { get; set; }
        public string? l_name_respons_pers { get; set; }
        public string? f_name_respons_pers { get; set; }
        public string? m_name_respons_pers { get; set; }
        public string? phones_respons_pers { get; set; }
        public string? emails_respons_pers { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("Organisations", Schema = "org")]
    public class Organisations
    {
        [Key]
        public int org_id { get; set; }
        public string? unom_org { get; set; }
		public short? org_activity_type_id { get; set; }
		public string? inn { get; set; }
        public string? kpp { get; set; }
        public string? ogrn { get; set; }
        public bool? org_struct { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("Org_History", Schema = "org")]
    public class Org_History
    {
        public int org_id { get; set; }
        public int data_status { get; set; }
        public short? org_status_id { get; set; }
        public string? full_name { get; set; }
        public string? short_name { get; set; }
        public string? pozition_head_manager { get; set; }
        public string? l_name_head_manager { get; set; }
        public string? f_name_head_manager { get; set; }
        public string? m_name_head_manager { get; set; }
        public string? org_contact_phones { get; set; }
        public string? org_emails { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("Dict_SendLettersTypes", Schema = "tso")]
    public class Dict_SendLettersTypes
    {
        [Key]
        public short Id { get; set; }
        public string? send_letters_type_name { get; set; }

    }

    [Table("Dict_TSOPropertyTypes", Schema = "tso")]
    public class Dict_TSOPropertyTypes
    {
        [Key]
        public short Id { get; set; }
        public string? org_property_type_name { get; set; }

    }

    [Table("Dict_TSOTypes", Schema = "tso")]
    public class Dict_TSOTypes
    {
        [Key]
        public short Id { get; set; }
        public string? org_type_name { get; set; }

    }

    [Table("ETO", Schema = "tso")]
    public class ETO
    {
        [Key]
        public int eto_id { get; set; }
        public string? unom_eto { get; set; }
		public string? territory { get; set; }
		public bool? is_liquidated { get; set; }
        public int? year_liquidation { get; set; }
        public string? reason_liquidation { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("ETO_DistrictsMapps", Schema = "tso")]
	public class ETO_DistrictsMapps
	{
		public int eto_id { get; set; }
		public int district_id { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("ETO_LayersMapping", Schema = "tso")]
    public class ETO_LayersMapping
    {
        public int eto_id { get; set; }
        public int layer_id { get; set; }
        public int layer_sys { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TSO_AdditionalValues", Schema = "tso")]
    public class TSO_AdditionalValues
    {
        public int tso_id { get; set; }
        public int data_status { get; set; }
        public decimal? fixed_assets_cost_prod { get; set; }
        public decimal? fixed_assets_wear { get; set; }
        public decimal? fixed_assets_cost_transfer { get; set; }
        public decimal? network_length { get; set; }
        public decimal? network_length_replaced { get; set; }
        public decimal? useful_supply_large_consumer { get; set; }
        public int? count_service_public { get; set; }
        public int? count_abonent { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TSO_ConnectCharge_Fact", Schema = "tso")]
    public class TSO_ConnectCharge_Fact
    {
        public int tso_id { get; set; }
        public int data_status { get; set; }
        public string? decision_num { get; set; }
        public DateTime? decision_date { get; set; }
        public string? protocol_num { get; set; }
        public DateTime? protocol_date { get; set; }
        public short? decision_charge_status_id { get; set; }
        public decimal? cost_connect_event { get; set; }
        public decimal? cost_hn_underground_ch_du250 { get; set; }
        public decimal? cost_hn_underground_ch_du251_400 { get; set; }
        public decimal? cost_hn_underground_ch_du401_550 { get; set; }
        public decimal? cost_hn_underground_ch_du551_700 { get; set; }
        public decimal? cost_hn_underground_ch_du700 { get; set; }
        public decimal? cost_hn_underground_ch_free_du250 { get; set; }
        public decimal? cost_hn_underground_ch_free_du251_400 { get; set; }
        public decimal? cost_hn_underground_ch_free_du401_550 { get; set; }
        public decimal? cost_hn_underground_ch_free_du551_700 { get; set; }
        public decimal? cost_hn_underground_ch_free_du700 { get; set; }
        public decimal? cost_hn_overground_du250 { get; set; }
        public decimal? cost_hn_overground_du251_400 { get; set; }
        public decimal? cost_hn_overground_du401_550 { get; set; }
        public decimal? cost_hn_overground_du551_700 { get; set; }
        public decimal? cost_hn_overground_du700 { get; set; }
        public decimal? cost_hp { get; set; }
        public decimal? profit_tax { get; set; }
        //public decimal? cost_service_before_hp { get; set; }
        //public decimal? cost_service_after_hp { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }
    
    [Table("TSO_ConnectChargePC_Fact", Schema = "tso")]
    public class TSO_ConnectChargePC_Fact
    {
        public int tso_id { get; set; }
        public int consumer_id { get; set; }
		public int dev_prog_id { get; set; }
		public int data_status { get; set; }
        public string? decision_num { get; set; }
        public DateTime? decision_date { get; set; }
        public string? protocol_num { get; set; }
        public DateTime? protocol_date { get; set; }
        public short? decision_charge_status_id { get; set; }
        public decimal? confirm_size_charge_connect { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }


	[Table("TSO_ReserveHPCharge_Fact", Schema = "tso")]
	public class TSO_ReserveHPCharge_Fact
	{
		public int tso_id { get; set; }
		public int data_status { get; set; }
		public string? decision_num { get; set; }
		public DateTime? decision_date { get; set; }
		public string? protocol_num { get; set; }
		public DateTime? protocol_date { get; set; }
		public short? decision_charge_status_id { get; set; }
        public decimal? cost_service_before_hp { get; set; }
        public decimal? cost_service_after_hp { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TSO_ETOMapps", Schema = "tso")]
    public class TSO_ETOMapps
    {
        public int tso_id { get; set; }
        public int data_status { get; set; }
        public int eto_id { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TSO_History", Schema = "tso")]
    public class TSO_History
    {
        public int org_id { get; set; }
        public int data_status { get; set; }
        public string? code_tso { get; set; }
		public short? org_property_type_id { get; set; }
        public int? eto_id { get; set; }
        public decimal? org_size_own_capital { get; set; }
        public DateTime? year_own_capital { get; set; }
        public bool? tso_nds_pay { get; set; }
        public short? send_letters_type_id { get; set; }
		public string? notes { get; set; }
		public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TSO_Perspective", Schema = "tso")]
    public class TSO_Perspective
    {
        public int org_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short? org_status_id { get; set; }
        public short? tso_type_id { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }
    }

    [Table("Dict_BuyPlace", Schema = "tarif_zone")]
    public class Dict_BuyPlace
    {
        [Key]
        public short Id { get; set; }
        public string? buy_place_name { get; set; }
        public string? buy_place_short { get; set; }

    }

    [Table("Dict_ConnectionChargeStatuses", Schema = "tarif_zone")]
    public class Dict_ConnectionChargeStatuses
    {
        [Key]
        public short Id { get; set; }
        public string? connection_status_name { get; set; }
        public string? connection_status_short { get; set; }

    }

    [Table("Dict_HeatCarrierTypes", Schema = "tarif_zone")]
    public class Dict_HeatCarrierTypes
    {
        [Key]
        public short Id { get; set; }
        public string? heat_carrier_type_name { get; set; }
        public string? heat_carrier_type_short { get; set; }

    }

    [Table("Dict_NeedsTypes", Schema = "tarif_zone")]
    public class Dict_NeedsTypes
    {
        [Key]
        public short Id { get; set; }
        public string? needs_type_name { get; set; }

    }

    [Table("Dict_Periods", Schema = "tarif_zone")]
    public class Dict_Periods
    {
        [Key]
        public short Id { get; set; }
        public string? period_name { get; set; }
        public string? period_short { get; set; }

    }

    [Table("Dict_PropertyTypes", Schema = "tarif_zone")]
    public class Dict_PropertyTypes
    {
        [Key]
        public short Id { get; set; }
        public string? obj_property_type_name { get; set; }
        public string? obj_property_type_short { get; set; }

    }

    [Table("Dict_StaffTypes", Schema = "tarif_zone")]
    public class Dict_StaffTypes
    {
        [Key]
        public short Id { get; set; }
        public string? staff_type_name { get; set; }

    }

    [Table("Dict_StationTypes", Schema = "tarif_zone")]
    public class Dict_StationTypes
    {
        [Key]
        public short Id { get; set; }
        public string? station_type_name { get; set; }

    }

    [Table("Dict_SteamParameters", Schema = "tarif_zone")]
    public class Dict_SteamParameters
    {
        [Key]
        public short Id { get; set; }
        public string? steam_parameter_name { get; set; }

    }

    [Table("Dict_VoltageTypes", Schema = "tarif_zone")]
    public class Dict_VoltageTypes
    {
        [Key]
        public short Id { get; set; }
        public string? voltage_type_name { get; set; }
        public string? voltage_type_short { get; set; }

    }

    [Table("Dict_TariffDifferentiationTypes", Schema = "tarif_zone")]
    public class Dict_TariffDifferentiationTypes
    {
        [Key]
        public short Id { get; set; }
        public string? tariff_differentiation_name { get; set; }

    }

    [Table("TariffZone", Schema = "tarif_zone")]
    public class TariffZone
    {
        [Key]
        public int tz_id { get; set; }
        public string? unom_tz { get; set; }
        public string? tz_name { get; set; }
		public string? territory { get; set; }
        public bool? combine_prod_more25 { get; set; }
        public bool? combine_prod_less25 { get; set; }
        public bool? not_combine_prod { get; set; }
        public bool? transfer { get; set; }
        public bool? sale { get; set; }
        public bool? delivery_contract { get; set; }
        public short? tariff_differentiation { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("TZ_DistrictsMapps", Schema = "tarif_zone")]
	public class TZ_DistrictsMapps
	{
		public int tz_id { get; set; }
		public int district_id { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_History", Schema = "tarif_zone")]
    public class TZ_History
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public string? tz_num { get; set; }
        public string? tz_num_plan { get; set; }
        public int? layer_id { get; set; }
        public int? layer_sys { get; set; }
		public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("TZ_Perspective", Schema = "tarif_zone")]
	public class TZ_Perspective
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public int tso_id { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_LayersMapping", Schema = "tarif_zone")]
    public class TZ_LayersMapping
    {
        public int tz_id { get; set; }
        public int layer_id { get; set; }
        public int layer_sys { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_Amortization_Fact", Schema = "tarif_zone")]
    public class TZ_Amortization_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public decimal? amortization_prod_equip_fact { get; set; }
        public decimal? amortization_other_means_fact { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_Amortization_Plan", Schema = "tarif_zone")]
    public class TZ_Amortization_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? amortization_prod_equip_plan { get; set; }
        public decimal? amortization_other_means_plan { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("TZ_CostRegOrgService_Fact", Schema = "tarif_zone")]
	public class TZ_CostRegOrgService_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_reg_org_service_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CostRegOrgService_Plan", Schema = "tarif_zone")]
	public class TZ_CostRegOrgService_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_reg_org_service_plan { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CostIncomeTax_Fact", Schema = "tarif_zone")]
	public class TZ_CostIncomeTax_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_income_tax_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CostIncomeTax_Plan", Schema = "tarif_zone")]
	public class TZ_CostIncomeTax_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_income_tax_plan { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_BalanceHeatEnergy_Fact", Schema = "tarif_zone")]
    public class TZ_BalanceHeatEnergy_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public decimal? generate_heat_energy_fact { get; set; }
        public decimal? heat_consump_fact { get; set; }
        public decimal? release_collect_ownprod_fact { get; set; }
        public decimal? release_collect_household_needs_fact { get; set; }
        public decimal? release_collect_buget_cons_fact { get; set; }
        public decimal? release_collect_public_fact { get; set; }
        public decimal? release_collect_other_cons_fact { get; set; }
        public decimal? release_collect_tso_salers_fact { get; set; }
        public decimal? release_collect_ownheatnet_fact { get; set; }
        //public decimal? accept_heatenrg_transport_fact { get; set; }
        public decimal? heatenrg_loss_heatnet_fact { get; set; }
        public decimal? useful_release_ownprod_fact { get; set; }
        public decimal? useful_release_household_needs_fact { get; set; }
        public decimal? useful_release_tso_salers_fact { get; set; }
        public decimal? useful_release_buget_cons_fact { get; set; }
        public decimal? useful_release_public_fact { get; set; }
        public decimal? useful_release_other_cons_fact { get; set; }
		public string? notes_fact { get; set; }
		public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_BalanceHeatEnergy_Plan", Schema = "tarif_zone")]
    public class TZ_BalanceHeatEnergy_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? generate_heat_energy_plan { get; set; }
        public decimal? heat_consump_plan { get; set; }
        public decimal? release_collect_ownprod_plan { get; set; }
        public decimal? release_collect_household_needs_plan { get; set; }
        public decimal? release_collect_buget_cons_plan { get; set; }
        public decimal? release_collect_public_plan { get; set; }
        public decimal? release_collect_other_cons_plan { get; set; }
        public decimal? release_collect_tso_salers_plan { get; set; }
        public decimal? release_collect_ownheatnet_plan { get; set; }
        //public decimal? accept_heatenrg_transport_plan { get; set; }
        public decimal? heatenrg_loss_heatnet_plan { get; set; }
        public decimal? useful_release_ownprod_plan { get; set; }
        public decimal? useful_release_household_needs_plan { get; set; }
        public decimal? useful_release_tso_salers_plan { get; set; }
        public decimal? useful_release_buget_cons_plan { get; set; }
        public decimal? useful_release_public_plan { get; set; }
        public decimal? useful_release_other_cons_plan { get; set; }
		public string? notes_plan { get; set; }
		public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("TZ_BalanceHeatEnergyTransfer_Fact", Schema = "tarif_zone")]
	public class TZ_BalanceHeatEnergyTransfer_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public decimal? loss_heatnet_fact { get; set; }
		public decimal? ownprod_fact { get; set; }
		public decimal? household_needs_fact { get; set; }
		public decimal? tso_fact { get; set; }
		public decimal? buget_consumers_fact { get; set; }
		public decimal? public_fact { get; set; }
		public decimal? other_consumers_fact { get; set; }
		public string? notes_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_BalanceHeatEnergyTransfer_Plan", Schema = "tarif_zone")]
	public class TZ_BalanceHeatEnergyTransfer_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? loss_heatnet_plan { get; set; }
		public decimal? ownprod_plan { get; set; }
		public decimal? household_needs_plan { get; set; }
		public decimal? tso_plan { get; set; }
		public decimal? buget_consumers_plan { get; set; }
		public decimal? public_plan { get; set; }
		public decimal? other_consumers_plan { get; set; }
		public string? notes_plan { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CapitalInvestment_Plan", Schema = "tarif_zone")]
    public class TZ_CapitalInvestment_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? volume_invest_prod_electroenergy { get; set; }
        public decimal? volume_invest_prod_heatenergy { get; set; }
        public decimal? volume_invest_prod_heatcarrier { get; set; }
        public decimal? volume_invest_transfer_heatenergy { get; set; }
        public decimal? volume_invest_other { get; set; }
        public decimal? amortisation_deduction_fonds { get; set; }
        public decimal? amortisation_deduction_nonmaterial_active { get; set; }
        public decimal? finance_by_nonused_funds { get; set; }
        public decimal? finance_by_profit { get; set; }
        public decimal? finance_by_federal_budjet { get; set; }
        public decimal? finance_by_local_budjet { get; set; }
        public decimal? finance_by_regional_budjet { get; set; }
        public decimal? finance_by_other_sources { get; set; }
        public decimal? finance_by_credits { get; set; }
        public decimal? finance_by_realize_securities { get; set; }
        public decimal? finance_by_connection_charge { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostFinanceCapInv_Fact", Schema = "tarif_zone")]
    public class TZ_CostFinanceCapInv_Fact
	{
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public short finance_type_id { get; set; }
        public decimal? profit_own_funds { get; set; }
        public decimal? amortization_finance_invest { get; set; }
        public decimal? amortization_deduction_on_credit { get; set; }
        public decimal? federal_budget { get; set; }
        public decimal? regional_budget { get; set; }
        public decimal? local_budget { get; set; }
        public decimal? credits { get; set; }
        public decimal? loans { get; set; }
        public decimal? other_funds { get; set; }
        public decimal? received_funds_securities { get; set; }
        public decimal? connection_charge { get; set; }
        public decimal? other_means { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostOperatingsOther_Fact", Schema = "tarif_zone")]
    public class TZ_CostOperatingsOther_Fact
	{
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
		public decimal? cost_travel_fact { get; set; }
        public decimal? cost_staff_train_fact { get; set; }
        public decimal? cost_bank_service_fact { get; set; }
		public decimal? rent_nonprod_obj_state_prop_fact { get; set; }
		public decimal? rent_nonprod_obj_municipal_prop_fact { get; set; }
		public decimal? rent_nonprod_obj_other_fact { get; set; }
		public decimal? leasing_nonprod_obj_without_own_rights_fact { get; set; }
		public decimal? cost_other_operating_house_fact { get; set; }
        public decimal? cost_other_operating_fact { get; set; }
		//public decimal? coef_cost_on_transport_fact { get; set; }
		//public decimal? coef_amortiz_on_transport_fact { get; set; }
		public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostOperatingsOther_Plan", Schema = "tarif_zone")]
    public class TZ_CostOperatingsOther_Plan
	{
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
		public decimal? cost_travel_plan { get; set; }
        public decimal? cost_staff_train_plan { get; set; }
        public decimal? cost_bank_service_plan { get; set; }
		public decimal? rent_nonprod_obj_state_prop_plan { get; set; }
		public decimal? rent_nonprod_obj_municipal_prop_plan { get; set; }
		public decimal? rent_nonprod_obj_other_plan { get; set; }
		public decimal? leasing_nonprod_obj_without_own_rights_plan { get; set; }
		public decimal? cost_other_operating_house_plan { get; set; }
        public decimal? cost_other_operating_plan { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("TZ_CostRepairFunding_Fact", Schema = "tarif_zone")]
	public class TZ_CostRepairFunding_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_repair_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CostRepairFunding_Plan", Schema = "tarif_zone")]
	public class TZ_CostRepairFunding_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_repair_plan { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CostDecomission_Fact", Schema = "tarif_zone")]
	public class TZ_CostDecomission_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_decomission_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CostDecomission_Plan", Schema = "tarif_zone")]
	public class TZ_CostDecomission_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_decomission_plan { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CostFuelReserve_Fact", Schema = "tarif_zone")]
	public class TZ_CostFuelReserve_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_fuel_reserve_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CostFuelReserve_Plan", Schema = "tarif_zone")]
	public class TZ_CostFuelReserve_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_fuel_reserve_plan { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CostOrgOtherWorkService_Fact", Schema = "tarif_zone")]
    public class TZ_CostOrgOtherWorkService_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public decimal? cost_connect_service_fact { get; set; }
        public decimal? cost_security_service_fact { get; set; }
        public decimal? cost_communal_service_fact { get; set; }
        public decimal? cost_consulting_service_fact { get; set; }
        public decimal? cost_legal_service_fact { get; set; }
        public decimal? cost_inform_service_fact { get; set; }
        public decimal? cost_audit_service_fact { get; set; }
        public decimal? cost_strategic_manag_service_fact { get; set; }
        public decimal? cost_prepare_develop_service_fact { get; set; }
        public decimal? cost_targeted_funds_fact { get; set; }
        public decimal? cost_additional_insure_fact { get; set; }
        public decimal? cost_other_work_service_fact { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostOrgOtherWorkService_Plan", Schema = "tarif_zone")]
    public class TZ_CostOrgOtherWorkService_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? cost_connect_service_plan { get; set; }
        public decimal? cost_security_service_plan { get; set; }
        public decimal? cost_communal_service_plan { get; set; }
        public decimal? cost_consulting_service_plan { get; set; }
        public decimal? cost_legal_service_plan { get; set; }
        public decimal? cost_inform_service_plan { get; set; }
        public decimal? cost_audit_service_plan { get; set; }
        public decimal? cost_strategic_manag_service_plan { get; set; }
        public decimal? cost_prepare_develop_service_plan { get; set; }
        public decimal? cost_targeted_funds_plan { get; set; }
        public decimal? cost_additional_insure_plan { get; set; }
        public decimal? cost_other_work_service_plan { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostOtherOrg_Fact", Schema = "tarif_zone")]
    public class TZ_CostOtherOrg_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public decimal? cost_transport_fact { get; set; }
        public decimal? cost_tech_regulation_fact { get; set; }
        public decimal? cost_other_support_prod_fact { get; set; }
        public decimal? cost_other_service_prod_fact { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostOtherOrg_Plan", Schema = "tarif_zone")]
    public class TZ_CostOtherOrg_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? cost_transport_plan { get; set; }
        public decimal? cost_tech_regulation_plan { get; set; }
        public decimal? cost_other_support_prod_plan { get; set; }
        public decimal? cost_other_service_prod_plan { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostRawMaterials_Fact", Schema = "tarif_zone")]
    public class TZ_CostRawMaterials_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public decimal? cost_reagents_fact { get; set; }
        public decimal? cost_gsm_fact { get; set; }
        public decimal? cost_repair_fact { get; set; }
        public decimal? cost_tech_service_fact { get; set; }
        public decimal? cost_spec_clothes_fact { get; set; }
        public decimal? cost_house_inventary_fact { get; set; }
        public decimal? cost_other_fact { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostRawMaterials_Plan", Schema = "tarif_zone")]
    public class TZ_CostRawMaterials_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? cost_reagents_plan { get; set; }
        public decimal? cost_gsm_plan { get; set; }
        public decimal? cost_repair_plan { get; set; }
        public decimal? cost_tech_service_plan { get; set; }
        public decimal? cost_spec_clothes_plan { get; set; }
        public decimal? cost_house_inventary_plan { get; set; }
        public decimal? cost_other_plan { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostRequiredTaxes_Fact", Schema = "tarif_zone")]
    public class TZ_CostRequiredTaxes_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public decimal? cost_emissions_pollutant_fact { get; set; }
        public decimal? cost_required_insure_fact { get; set; }
        public decimal? cost_tax_org_property_fact { get; set; }
        public decimal? cost_tax_land_fact { get; set; }
        public decimal? cost_tax_transport_fact { get; set; }
        public decimal? cost_tax_water_fact { get; set; }
        public decimal? cost_tax_other_fact { get; set; }
		public decimal? cost_other_fact { get; set; }
		public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostRequiredTaxes_Plan", Schema = "tarif_zone")]
    public class TZ_CostRequiredTaxes_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? cost_emissions_pollutant_plan { get; set; }
        public decimal? cost_required_insure_plan { get; set; }
        public decimal? cost_tax_org_property_plan { get; set; }
        public decimal? cost_tax_land_plan { get; set; }
        public decimal? cost_tax_transport_plan { get; set; }
        public decimal? cost_tax_water_plan { get; set; }
        public decimal? cost_tax_other_plan { get; set; }
		public decimal? cost_other_plan { get; set; }
		public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CrossSubsidy_Plan", Schema = "tarif_zone")]
    public class TZ_CrossSubsidy_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? cross_subs_between_activ { get; set; }
        public decimal? cross_subs_between_consumer { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_DinamicValueNVV_Plan", Schema = "tarif_zone")]
    public class TZ_DinamicValueNVV_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public int calc_year { get; set; }
        public decimal? gross_profit_required { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_EconomyCosts_Plan", Schema = "tarif_zone")]
    public class TZ_EconomyCosts_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? economy_operation_cost { get; set; }
        public decimal? total_economy_noncontrol_cost { get; set; }
        public decimal? economy_consump_heatenergy { get; set; }
        public decimal? economy_consump_electroenergy { get; set; }
        public decimal? economy_consump_fuel { get; set; }
        public decimal? economy_consump_coldwater { get; set; }
        public decimal? economy_consump_waterdisp { get; set; }
        public decimal? economy_consump_heat { get; set; }
        public decimal? economy_consump_hotwater { get; set; }
        public decimal? increase_economy_operation_cost { get; set; }
        public decimal? increase_economy_consump_heatenergy { get; set; }
        public decimal? increase_economy_consump_electroenergy { get; set; }
        public decimal? increase_economy_consump_fuel { get; set; }
        public decimal? increase_economy_consump_coldwater { get; set; }
        public decimal? increase_economy_consump_waterdisp { get; set; }
        public decimal? increase_economy_consump_heat { get; set; }
        public decimal? increase_economy_consump_hotwater { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_ElectroEnergyAllNeeds_Fact", Schema = "tarif_zone")]
    public class TZ_ElectroEnergyAllNeeds_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public short voltage_type_id { get; set; }
        public decimal? volume_electricity_techneed_fact { get; set; }
		public decimal? volume_power_techneed_fact { get; set; }
		public decimal? volume_electricity_houseneed_fact { get; set; }
		public decimal? volume_power_houseneed_fact { get; set; }
		public decimal? price_buy_electricity_fact { get; set; }
        public decimal? price_buy_power_fact { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_ElectroEnergyAllNeeds_Plan", Schema = "tarif_zone")]
    public class TZ_ElectroEnergyAllNeeds_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public short voltage_type_id { get; set; }
        public decimal? volume_electricity_techneed_plan { get; set; }
        public decimal? volume_power_techneed_plan { get; set; }
		public decimal? volume_electricity_houseneed_plan { get; set; }
		public decimal? volume_power_houseneed_plan { get; set; }
		public decimal? price_buy_electricity_plan { get; set; }
		public decimal? price_buy_power_plan { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostFinanceCapInvAddValues_Fact", Schema = "tarif_zone")]
    public class TZ_CostFinanceCapInvAddValues_Fact
	{
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public decimal? actual_grants_budget { get; set; }
        public decimal? actual_over_profit { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_FuelTechNeeds_Fact", Schema = "tarif_zone")]
    public class TZ_FuelTechNeeds_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public short fuel_type_id { get; set; }
        public decimal? consumption_natural_fuel_fact { get; set; }
        public decimal? lowestheat_combust_fuel_fact { get; set; }
        public decimal? price_fuel_fact { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_FuelTechNeeds_Plan", Schema = "tarif_zone")]
    public class TZ_FuelTechNeeds_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public short fuel_type_id { get; set; }
        public decimal? consumption_natural_fuel_plan { get; set; }
        public decimal? lowestheat_combust_fuel_plan { get; set; }
        public decimal? norm_consump_fuel_plan { get; set; }
        public decimal? price_fuel_plan { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("TZ_FuelNormConsump_Plan", Schema = "tarif_zone")]
	public class TZ_FuelNormConsump_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? norm_consump_fuel_plan { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_TariffsHE_History", Schema = "tarif_zone")]
	public class TZ_TariffsHE_History
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public string? decision_num { get; set; }
		public DateTime? decision_date { get; set; }
		public string? protocol_num { get; set; }
		public DateTime? protocol_date { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_TariffsHETransfer_FactPlan", Schema = "tarif_zone")]
	public class TZ_TariffsHETransfer_FactPlan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? tariff_weighted_invest { get; set; }
		public decimal? invest_component { get; set; }
		public decimal? tariff_org_needs { get; set; }
		public decimal? tariff_tso { get; set; }
		public decimal? tariff_budgetcons { get; set; }
		public decimal? tariff_public { get; set; }
		public decimal? tariff_other { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_TariffsSingleHE_FactPlan", Schema = "tarif_zone")]
	public class TZ_TariffsSingleHE_FactPlan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? tariff_econom_justified { get; set; }
		public decimal? tariff_weighted_invest { get; set; }
		public decimal? invest_component { get; set; }
		public decimal? tariff_tso { get; set; }
		public decimal? tariff_budgetcons { get; set; }
		public decimal? tariff_public { get; set; }
		public decimal? tariff_other { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_TariffsDoubleHE_Fact", Schema = "tarif_zone")]
	public class TZ_TariffsDoubleHE_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public short consumer_type_id { get; set; }
		public decimal? amount_grants_budget { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("TZ_TariffsDoubleHE_Plan", Schema = "tarif_zone")]
	public class TZ_TariffsDoubleHE_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public short consumer_type_id { get; set; }
		public decimal? convers_doublerate_to_singlerate { get; set; }
		public decimal? doublerate_heat_energy { get; set; }
		public decimal? volume_heat_energy { get; set; }
		public decimal? doublerate_heat_load { get; set; }
		public decimal? volume_heat_load { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	[Table("TZ_TariffsHeatCarrier_Fact", Schema = "tarif_zone")]
	public class TZ_TariffsHeatCarrier_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public decimal? tariff_hc_hw { get; set; }
		public decimal? tariff_hc_steam { get; set; }
		public decimal? tariff_single_component_hw { get; set; }
		public decimal? tariff_double_component_hc { get; set; }
		public decimal? tariff_double_component_he { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_TariffsOthers_Fact", Schema = "tarif_zone")]
	public class TZ_TariffsOthers_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public short consumer_type_id { get; set; }
		public decimal? tariff_eto { get; set; }
		public decimal? tariff_compensation { get; set; }
		public decimal? tariff_preferential { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_TariffsCrossSubsidy_Fact", Schema = "tarif_zone")]
	public class TZ_TariffsCrossSubsidy_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public decimal? volume_tso { get; set; }
		public decimal? volume_budgetcons { get; set; }
		public decimal? volume_public { get; set; }
		public decimal? volume_other { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_TariffsHESteam_FactPlan", Schema = "tarif_zone")]
	public class TZ_TariffsHESteam_FactPlan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public short steam_parameter_id { get; set; }
		public decimal? tariff_econom_justified { get; set; }
		public decimal? useful_release_ownprod_needs { get; set; }
		public decimal? useful_release_household_needs { get; set; }
		public decimal? tariff_weighted_invest { get; set; }
		public decimal? invest_component { get; set; }
		public decimal? tariff_tso { get; set; }
		public decimal? volume_he_tso { get; set; }
		public decimal? tariff_budgetcons { get; set; }
		public decimal? volume_he_budgetcons { get; set; }
		public decimal? tariff_public { get; set; }
		public decimal? volume_he_public { get; set; }
		public decimal? tariff_other { get; set; }
		public decimal? volume_he_other { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }
	}

	/*[Table("TZ_HeatEnergyTariffs_Fact", Schema = "tarif_zone")]
    public class TZ_HeatEnergyTariffs_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public short heat_carrier_type_id { get; set; }
        public short steam_parameter_id { get; set; }
        public string? decision_num { get; set; }
        public DateTime? decision_date { get; set; }
        public string? protocol_num { get; set; }
        public DateTime? protocol_date { get; set; }
        public decimal? amount_grants_budgetcons { get; set; }
        public decimal? specific_value_grants_budgetcons { get; set; }
        public decimal? amount_grants_budget_public { get; set; }
        public decimal? specific_value_grants_public { get; set; }
        public decimal? amount_grants_other { get; set; }
        public decimal? specific_value_grants_other { get; set; }
        public decimal? volume_cross_subsid_groupresellers { get; set; }
        public decimal? volume_cross_subsid_groupconsumers { get; set; }
        public decimal? volume_cross_subsid_grouppublic { get; set; }
        public decimal? volume_cross_subsid_groupother { get; set; }
        public decimal? eto_tariff_budgetcons { get; set; }
        public decimal? compensation_tariff_budgetcons { get; set; }
        public decimal? preferential_tariff_budgetcons { get; set; }
        public decimal? eto_tariff_public { get; set; }
        public decimal? compensation_tariff_public { get; set; }
        public decimal? preferential_tariff_public { get; set; }
        public decimal? eto_tariff_other { get; set; }
        public decimal? compensation_tariff_other { get; set; }
        public decimal? preferential_tariff_other { get; set; }
        public decimal? tariff_hc_source_hw { get; set; }
        public decimal? tariff_hc_source_steam { get; set; }
        public decimal? tariff_hc_boiler_hw { get; set; }
        public decimal? comp_hc_opscheme_budgetcons { get; set; }
        public decimal? comp_comp_he_opscheme_budgetcons { get; set; }
        public decimal? comp_hc_opscheme_public { get; set; }
        public decimal? comp_comp_he_opscheme_public { get; set; }
        public decimal? comp_hc_opscheme_other { get; set; }
        public decimal? comp_comp_he_opscheme_other { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_HeatEnergyTariffs_Plan", Schema = "tarif_zone")]
    public class TZ_HeatEnergyTariffs_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public short heat_carrier_type_id { get; set; }
        public short steam_parameter_id { get; set; }
        public decimal? economicaly_justified_tariff { get; set; }
        public decimal? weighted_tariff_invest { get; set; }
        public decimal? invest_component { get; set; }
        public decimal? tariff_transfer_org_needs { get; set; }
        public decimal? tariff_tso { get; set; }
        public decimal? tariff_transfer_tso { get; set; }
        public decimal? tariff_budgetcons { get; set; }
        public decimal? tariff_transfer_budgetcons { get; set; }
        public decimal? convers_twostage_singlestage { get; set; }
        public decimal? heatenergy_doublerate_budjetcons { get; set; }
        public decimal? heatload_doublerate_budjetcons { get; set; }
        public decimal? tariff_single_public { get; set; }
        public decimal? tariff_transfer_public { get; set; }
        public decimal? convers_twostage_singlestage_public { get; set; }
        public decimal? heatenergy_rate_doublerate_public { get; set; }
        public decimal? heatload_rate_twostage_public { get; set; }
        public decimal? tariff_single_other { get; set; }
        public decimal? tariff_transfer_other { get; set; }
        public decimal? convers_twostage_singlestage_other { get; set; }
        public decimal? heatenergy_doublerate_other { get; set; }
        public decimal? heatload_doublerate_other { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }*/

	[Table("TZ_InOutBuyEnergy_Fact", Schema = "tarif_zone")]
	public class TZ_InOutBuyEnergy_Fact
	{
		public int out_tz_id { get; set; }
		public int input_tz_id { get; set; }
		public short report_period_id { get; set; }
		public int source_id { get; set; }
		public int data_status { get; set; }
		public short buy_place_id { get; set; }
		public decimal? buy_energy_techneed_fact { get; set; }
		public decimal? buy_loss_techneed_fact { get; set; }
		public decimal? buy_energy_houseneed_fact { get; set; }
		public decimal? buy_loss_houseneed_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_InOutBuyEnergy_Plan", Schema = "tarif_zone")]
    public class TZ_InOutBuyEnergy_Plan
	{
		public int out_tz_id { get; set; }
		public int input_tz_id { get; set; }
		public short report_period_id { get; set; }
		public int source_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short buy_place_id { get; set; }
		public decimal? buy_energy_techneed_plan { get; set; }
		public decimal? buy_loss_techneed_plan { get; set; }
		public decimal? buy_energy_houseneed_plan { get; set; }
		public decimal? buy_loss_houseneed_plan { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("TZ_InOutTransferEnergy_Fact", Schema = "tarif_zone")]
	public class TZ_InOutTransferEnergy_Fact
	{
		public int data_status { get; set; }
		public int out_tz_id { get; set; }
		public int input_tz_id { get; set; }
		public short report_period_id { get; set; }
		public int source_id { get; set; }
		public decimal? loss_heatnet_fact { get; set; }
		public decimal? ownprod_fact { get; set; }
		public decimal? household_needs_fact { get; set; }
		public decimal? tso_fact { get; set; }
		public decimal? buget_consumers_fact { get; set; }
		public decimal? public_fact { get; set; }
		public decimal? other_consumers_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_InOutTransferEnergy_Plan", Schema = "tarif_zone")]
	public class TZ_InOutTransferEnergy_Plan
	{
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public int out_tz_id { get; set; }
		public int input_tz_id { get; set; }
		public int source_id { get; set; }
		public short report_period_id { get; set; }
		public decimal? loss_heatnet_plan { get; set; }
		public decimal? ownprod_plan { get; set; }
		public decimal? household_needs_plan { get; set; }
		public decimal? tso_plan { get; set; }
		public decimal? buget_consumers_plan { get; set; }
		public decimal? public_plan { get; set; }
		public decimal? other_consumers_plan { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

    [Table("TZ_LostRevenue_Plan", Schema = "tarif_zone")]
    public class TZ_LostRevenue_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? lost_revenue_pretrial_dispute { get; set; }
        public decimal? lost_revenue_disagree { get; set; }
        public decimal? cost_notaccounted_reg_body { get; set; }
        public decimal? cost_grow_price_product { get; set; }
        public decimal? cost_service_borrowed_funds { get; set; }
        public decimal? lost_revenue_from_connect_obj { get; set; }
        public decimal? lost_revenue_other { get; set; }
        public decimal? excess_funds { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("TZ_ExcessFunds_Plan", Schema = "tarif_zone")]
	public class TZ_ExcessFunds_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? excess_funds { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_IndexesNVV_Plan", Schema = "tarif_zone")]
	public class TZ_IndexesNVV_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? index_effective_cost { get; set; }
		public decimal? index_consumers_price { get; set; }
		public decimal? index_count_actives { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_MethodIndexation_Plan", Schema = "tarif_zone")]
    public class TZ_MethodIndexation_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? economically_justified_costs { get; set; }
        public decimal? profit_regulated_organisation { get; set; }
        public decimal? economy_consump_resources { get; set; }
        public decimal? correct_accounting_deviation { get; set; }
        public decimal? correct_safety_quality_product { get; set; }
        public decimal? correct_nvv_change_invest_prog { get; set; }
        public decimal? correct_subj_accounting_nvv { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_MethodProfitInvest_Plan", Schema = "tarif_zone")]
    public class TZ_MethodProfitInvest_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? full_value_invest { get; set; }
        public decimal? return_period_invest { get; set; }
        public decimal? initial_amount_invest { get; set; }
        public decimal? profit_initial_amount_invest { get; set; }
        public decimal? base_invest { get; set; }
        public decimal? pure_working_capital { get; set; }
        public decimal? norm_pure_working_capital { get; set; }
        public decimal? norm_profit { get; set; }
        public decimal? norm_profit_new_capital { get; set; }
        public decimal? norm_profit_old_capital { get; set; }
        public decimal? amortization { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_MethodAnaloguesComparison_Plan", Schema = "tarif_zone")]
    public class TZ_MethodAnaloguesComparison_Plan
	{
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? baseline_cost_reg_managed { get; set; }
        public decimal? correct_nvv_accounting_deviation { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_Profit_Fact", Schema = "tarif_zone")]
    public class TZ_Profit_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public decimal? profit_prod_develop { get; set; }
        public decimal? profit_social_develop { get; set; }
        public decimal? profit_stimul { get; set; }
        public decimal? profit_other_needs { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_Profit_Plan", Schema = "tarif_zone")]
    public class TZ_Profit_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? calc_enterprising_profit { get; set; }
        public decimal? funds_repayment_credit { get; set; }
        public decimal? cost_capital_invest { get; set; }
        public decimal? return_rate { get; set; }
        public decimal? cost_economic_pay { get; set; }
        public decimal? regular_lvl_profit { get; set; }
        public decimal? regular_lvl_profit_percent { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_CostUncontrolOther_Fact", Schema = "tarif_zone")]
    public class TZ_CostUncontrolOther_Fact
	{
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
		public decimal? cost_concession_fact { get; set; }
		public decimal? cost_doubtful_debt_fact { get; set; }
		public decimal? cost_credit_contracts_fact { get; set; }
		public decimal? rent_prod_obj_state_prop_fact { get; set; }
		public decimal? rent_prod_obj_municipal_prop_fact { get; set; }
		public decimal? rent_prod_obj_other_fact { get; set; }
		public decimal? leasing_prod_obj_fact { get; set; }
		public decimal? leasing_nonprod_obj_with_own_rights_fact { get; set; }
		public decimal? cost_uncontrol_other_fact { get; set; }
		public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("TZ_CostUncontrolOther_Plan", Schema = "tarif_zone")]
	public class TZ_CostUncontrolOther_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_concession_plan { get; set; }
		public decimal? cost_doubtful_debt_plan { get; set; }
		public decimal? cost_credit_contracts_plan { get; set; }
		public decimal? rent_prod_obj_state_prop_plan { get; set; }
		public decimal? rent_prod_obj_municipal_prop_plan { get; set; }
		public decimal? rent_prod_obj_other_plan { get; set; }
		public decimal? leasing_prod_obj_plan { get; set; }
		public decimal? leasing_nonprod_obj_with_own_rights_plan { get; set; }
		public decimal? cost_uncontrol_other_plan { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_StaffValues_Fact", Schema = "tarif_zone")]
	public class TZ_StaffValues_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public short staff_type_id { get; set; }
		public decimal? count_workers_fact { get; set; }
		public decimal? average_salary_fact { get; set; }
		public decimal? deduction_soc_need_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_StaffValues_Plan", Schema = "tarif_zone")]
    public class TZ_StaffValues_Plan
	{
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public short staff_type_id { get; set; }
        public decimal? count_workers_plan { get; set; }
        public decimal? average_salary_plan { get; set; }
        public decimal? deduction_soc_need_plan { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_WaterTechNeeds_Fact", Schema = "tarif_zone")]
    public class TZ_WaterTechNeeds_Fact
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public short report_period_id { get; set; }
        public decimal? volume_buy_coldwater_fact { get; set; }
        public decimal? price_coldwater_fact { get; set; }
        public decimal? volume_waterdispos_fact { get; set; }
        public decimal? price_waterdispos_fact { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

    [Table("TZ_WaterTechNeeds_Plan", Schema = "tarif_zone")]
    public class TZ_WaterTechNeeds_Plan
    {
        public int tz_id { get; set; }
        public int data_status { get; set; }
        public int perspective_year { get; set; }
        public short report_period_id { get; set; }
        public decimal? volume_buy_coldwater_plan { get; set; }
        public decimal? price_coldwater_plan { get; set; }
        public decimal? volume_waterdispos_plan { get; set; }
        public decimal? price_waterdispos_plan { get; set; }
        public bool? is_deleted { get; set; }
        public DateTime? create_date { get; set; }
        public DateTime? edit_date { get; set; }
        public int? user_id { get; set; }

    }

	[Table("TZ_HeatCarrierTechNeeds_Fact", Schema = "tarif_zone")]
	public class TZ_HeatCarrierTechNeeds_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public decimal? volume_buy_heat_carrier_fact { get; set; }
		public decimal? cost_heat_carrier_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_HeatCarrierTechNeeds_Plan", Schema = "tarif_zone")]
	public class TZ_HeatCarrierTechNeeds_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? volume_buy_heat_carrier_plan { get; set; }
		public decimal? cost_heat_carrier_plan { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CostHeatTransfer_Fact", Schema = "tarif_zone")]
	public class TZ_CostHeatTransfer_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_heattrans_service_fact { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}
	
    [Table("TZ_CostHeatTransfer_Plan", Schema = "tarif_zone")]
	public class TZ_CostHeatTransfer_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public short report_period_id { get; set; }
		public decimal? cost_heattrans_service_plan { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CoefsСostDistribution_Plan", Schema = "tarif_zone")]
	public class TZ_CoefsСostDistribution_Plan
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public int perspective_year { get; set; }
		public decimal? coef_cost_on_transport_plan { get; set; }
		public decimal? coef_amortiz_on_transport_plan { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public decimal? coef_cost_on_production_plan { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public decimal? coef_amortiz_on_production_plan { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

	[Table("TZ_CoefsСostDistribution_Fact", Schema = "tarif_zone")]
	public class TZ_CoefsСostDistribution_Fact
	{
		public int tz_id { get; set; }
		public int data_status { get; set; }
		public decimal? coef_cost_on_transport_fact { get; set; }
		public decimal? coef_amortiz_on_transport_fact { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public decimal? coef_cost_on_production_fact { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public decimal? coef_amortiz_on_production_fact { get; set; }
		public bool? is_deleted { get; set; }
		public DateTime? create_date { get; set; }
		public DateTime? edit_date { get; set; }
		public int? user_id { get; set; }

	}

    [Table("TestUploadTSO", Schema = "uploads")]
    public class TestUploadTSO
    {
		public int data_status { get; set; }
		public int batch_id { get; set; }
		public bool is_uploaded { get; set; }
		public int? Id { get; set; }
        public string? Name { get; set; }
        public string? INN { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }

    }
}