using DataBase.Models.Sources;
using DataBase.Models.TSO;
using DataBaseHSS.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProject.Areas.DictionaryTables.Models;
using WebProject.Areas.HeatPointsAndConsumers.Models;
using WebProject.Areas.HPConsumers.Models;
using WebProject.Areas.OutputForms.Models;
using WebProject.Areas.Sources.Models;
using WebProject.Areas.TSO.Models;
using WebProject.Models;
using static DataBase.Models.DictionaryTables.DataBaseDictionaryTablesModel;

namespace WebProject.Data
{
    public class HssDbContext : IdentityDbContext<IdentityUser>
    {
        public HssDbContext(DbContextOptions<HssDbContext> options)
            : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Контекст получает строку подключения.
        //    optionsBuilder.UseSqlServer(Constants.SqlConnectionIntegratedSecurity);
        //}

        // схема uploads
        #region
        public DbSet<TestUploadTSO> TestUploadTSO { get; set; } = null!;
        #endregion
        //

        // схема dbo
        #region
        public DbSet<EX_Logs> EX_Logs { get; set; } = null!;
        #endregion
        // dictionary

        // схема dictionary
        #region
        public DbSet<DataStatuses> DataStatuses { get; set; } = null!;
        public DbSet<PerspectiveYears> PerspectiveYears { get; set; } = null!;
        public DbSet<DistrictsValues_History> DistrictsValues_Histories { get; set; } = null!;
        public DbSet<DistrictsValues_Perspective> DistrictsValues_Perspectives { get; set; } = null!;
        public DbSet<Layers> Layers { get; set; } = null!;
        public DbSet<LayerStatuses> LayerStatuses { get; set; } = null!;
        public DbSet<LayerTypes> LayerTypes { get; set; } = null!;
        public DbSet<Districts> Districts { get; set; } = null!;
        public DbSet<Regions> Regions { get; set; } = null!;
        public DbSet<Months> Months { get; set; } = null!;
        public DbSet<UploadsTemplates> UploadsTemplates { get; set; } = null!;
        public DbSet<CoefCorrection> CoefCorrection { get; set; } = null!;
        public DbSet<CoefCorrection_Perspective> CoefCorrection_Perspective { get; set; } = null!;
        public DbSet<CoefInflationary> CoefInflationary { get; set; } = null!;
        public DbSet<CoefInflationary_Perspective> CoefInflationary_Perspective { get; set; } = null!;
        public DbSet<TemperatureGraphics> TemperatureGraphics { get; set; } = null!;
        public DbSet<Dict_NetLayingTypes> Dict_NetLayingTypes { get; set; } = null!;
        #endregion
        // dictionary

        // схема org
        #region
        public DbSet<Dict_ActivityTypes> Dict_ActivityTypes { get; set; } = null!;
        public DbSet<Dict_OrgStatuses> Dict_OrgStatuses { get; set; } = null!;
        public DbSet<Organisations> Organisations { get; set; } = null!;
        public DbSet<Org_History> Org_History { get; set; } = null!;
        #endregion
        // схема tso
        #region
        public DbSet<Dict_SendLettersTypes> Dict_SendLettersTypes { get; set; } = null!;
        public DbSet<Dict_TSOPropertyTypes> Dict_TSOPropertyTypes { get; set; } = null!;
        public DbSet<Dict_TSOTypes> Dict_TSOTypes { get; set; } = null!;
        public DbSet<ETO> ETO { get; set; } = null!;
        public DbSet<TSO_ETOMapps> TSO_ETOMapps { get; set; } = null!;
        public DbSet<TSO_History> TSO_History { get; set; } = null!;
        public DbSet<TSO_Perspective> TSO_Perspective { get; set; } = null!;
        public DbSet<ResponsiblePersons> ResponsiblePersons { get; set; } = null!;
        public DbSet<Sources> Sources { get; set; } = null!;
        public DbSet<S_Outputs> S_Outputs { get; set; } = null!;
        public DbSet<TSO_AdditionalValues> TSO_AdditionalValues { get; set; } = null!;
        public DbSet<ETO_DistrictsMapps> ETO_DistrictsMapps { get; set; } = null!;
        #endregion
        // tso

		// схема sources
		public DbSet<Dict_FuelTypes> Dict_FuelTypes { get; set; } = null!;
		public DbSet<HeatSupplySystem> HeatSupplySystems { get; set; } = null!;
		public DbSet<HSS_DistrictsMapps> HSS_DistrictsMapps { get; set; } = null!;
		public DbSet<S_History> S_Histories { get; set; } = null!;
		public DbSet<HSS_LayersMapping> HSS_LayersMappings { get; set; } = null!;
		public DbSet<S_Perspective> S_Perspectives { get; set; } = null!;
		public DbSet<Dict_SourceStatuses> Dict_SourceStatuses { get; set; } = null!;
		public DbSet<S_DocsPhotos_History> S_DocsPhotos_History { get; set; } = null!;
        public DbSet<Dict_HeatOutSchemeTypes> Dict_HeatOutSchemeTypes { get; set; } = null!;

        //

        // схема tarif_zone
        #region
        public DbSet<Dict_Periods> Dict_Periods { get; set; } = null!;
		public DbSet<TariffZone> TariffZone { get; set; } = null!;
		public DbSet<TZ_BalanceHeatEnergy_Fact> TZ_BalanceHeatEnergy_Fact { get; set; } = null!;
		public DbSet<TZ_BalanceHeatEnergy_Plan> TZ_BalanceHeatEnergy_Plan { get; set; } = null!;
		public DbSet<TZ_InOutBuyEnergy_Fact> TZ_InOutBuyEnergy_Fact { get; set; } = null!;
		public DbSet<TZ_InOutBuyEnergy_Plan> TZ_InOutBuyEnergy_Plan { get; set; } = null!;
		public DbSet<TZ_BalanceHeatEnergyTransfer_Fact> TZ_BalanceHeatEnergyTransfer_Fact { get; set; } = null!;
		public DbSet<TZ_BalanceHeatEnergyTransfer_Plan> TZ_BalanceHeatEnergyTransfer_Plan { get; set; } = null!;
		public DbSet<TZ_InOutTransferEnergy_Fact> TZ_InOutTransferEnergy_Fact { get; set; } = null!;
		public DbSet<TZ_InOutTransferEnergy_Plan> TZ_InOutTransferEnergy_Plan { get; set; } = null!;
		public DbSet<TZ_FuelTechNeeds_Fact> TZ_FuelTechNeeds_Fact { get; set; } = null!;
		public DbSet<TZ_FuelTechNeeds_Plan> TZ_FuelTechNeeds_Plan { get; set; } = null!;
		public DbSet<TZ_FuelNormConsump_Plan> TZ_FuelNormConsump_Plan { get; set; } = null!;
		public DbSet<TZ_WaterTechNeeds_Fact> TZ_WaterTechNeeds_Fact { get; set; } = null!;
		public DbSet<TZ_WaterTechNeeds_Plan> TZ_WaterTechNeeds_Plan { get; set; } = null!;
		public DbSet<TZ_CostFuelReserve_Fact> TZ_CostFuelReserve_Fact { get; set; } = null!;
		public DbSet<TZ_CostFuelReserve_Plan> TZ_CostFuelReserve_Plan { get; set; } = null!;
		public DbSet<TZ_HeatCarrierTechNeeds_Fact> TZ_HeatCarrierTechNeeds_Fact { get; set; } = null!;
		public DbSet<TZ_HeatCarrierTechNeeds_Plan> TZ_HeatCarrierTechNeeds_Plan { get; set; } = null!;
		public DbSet<TZ_CostHeatTransfer_Fact> TZ_CostHeatTransfer_Fact { get; set; } = null!;
		public DbSet<TZ_CostHeatTransfer_Plan> TZ_CostHeatTransfer_Plan { get; set; } = null!;
		public DbSet<TZ_ElectroEnergyAllNeeds_Fact> TZ_ElectroEnergyAllNeeds_Fact { get; set; } = null!;
		public DbSet<TZ_ElectroEnergyAllNeeds_Plan> TZ_ElectroEnergyAllNeeds_Plan { get; set; } = null!;
		public DbSet<TZ_StaffValues_Fact> TZ_StaffValues_Fact { get; set; } = null!;
		public DbSet<TZ_StaffValues_Plan> TZ_StaffValues_Plan { get; set; } = null!;
		public DbSet<Dict_HeatCarrierTypes> Dict_HeatCarrierTypes { get; set; } = null!;
		public DbSet<TZ_CostRawMaterials_Fact> TZ_CostRawMaterials_Fact { get; set; } = null!;
		public DbSet<TZ_CostRawMaterials_Plan> TZ_CostRawMaterials_Plan { get; set; } = null!;
		public DbSet<TZ_CostOtherOrg_Fact> TZ_CostOtherOrg_Fact { get; set; } = null!;
		public DbSet<TZ_CostOtherOrg_Plan> TZ_CostOtherOrg_Plan { get; set; } = null!;
		public DbSet<TZ_CostOrgOtherWorkService_Fact> TZ_CostOrgOtherWorkService_Fact { get; set; } = null!;
		public DbSet<TZ_CostOrgOtherWorkService_Plan> TZ_CostOrgOtherWorkService_Plan { get; set; } = null!;
		public DbSet<TZ_TariffsSingleHE_FactPlan> TZ_TariffsSingleHE_FactPlan { get; set; } = null!;
		public DbSet<TZ_CostRepairFunding_Fact> TZ_CostRepairFunding_Fact { get; set; } = null!;
		public DbSet<TZ_CostRepairFunding_Plan> TZ_CostRepairFunding_Plan { get; set; } = null!;
		public DbSet<TZ_CostDecomission_Fact> TZ_CostDecomission_Fact { get; set; } = null!;
		public DbSet<TZ_CostDecomission_Plan> TZ_CostDecomission_Plan { get; set; } = null!;
		public DbSet<TZ_CostOperatingsOther_Fact> TZ_CostOperatingsOther_Fact { get; set; } = null!;
		public DbSet<TZ_CostOperatingsOther_Plan> TZ_CostOperatingsOther_Plan { get; set; } = null!;
		public DbSet<TZ_CostRequiredTaxes_Fact> TZ_CostRequiredTaxes_Fact { get; set; } = null!;
		public DbSet<TZ_CostRequiredTaxes_Plan> TZ_CostRequiredTaxes_Plan { get; set; } = null!;
		public DbSet<TZ_Amortization_Fact> TZ_Amortization_Fact { get; set; } = null!;
		public DbSet<TZ_Amortization_Plan> TZ_Amortization_Plan { get; set; } = null!;
		public DbSet<TZ_CostRegOrgService_Fact> TZ_CostRegOrgService_Fact { get; set; } = null!;
		public DbSet<TZ_CostRegOrgService_Plan> TZ_CostRegOrgService_Plan { get; set; } = null!;
		public DbSet<TZ_CostIncomeTax_Fact> TZ_CostIncomeTax_Fact { get; set; } = null!;
		public DbSet<TZ_CostIncomeTax_Plan> TZ_CostIncomeTax_Plan { get; set; } = null!;
		public DbSet<TZ_CostUncontrolOther_Fact> TZ_CostUncontrolOther_Fact { get; set; } = null!;
		public DbSet<TZ_CostUncontrolOther_Plan> TZ_CostUncontrolOther_Plan { get; set; } = null!;
		public DbSet<TZ_CoefsСostDistribution_Fact> TZ_CoefsСostDistribution_Fact { get; set; } = null!;
		public DbSet<TZ_CoefsСostDistribution_Plan> TZ_CoefsСostDistribution_Plan { get; set; } = null!;
		public DbSet<TZ_DistrictsMapps> TZ_DistrictsMapps { get; set; } = null!;
		/*НВВ*/
		public DbSet<TZ_Profit_Fact> TZ_Profit_Fact { get; set; } = null!;
		public DbSet<TZ_Profit_Plan> TZ_Profit_Plan { get; set; } = null!;
		public DbSet<TZ_LostRevenue_Plan> TZ_LostRevenue_Plan { get; set; } = null!;
		public DbSet<TZ_ExcessFunds_Plan> TZ_ExcessFunds_Plan { get; set; } = null!;
		public DbSet<TZ_CapitalInvestment_Plan> TZ_CapitalInvestment_Plan { get; set; } = null!;
		public DbSet<TZ_CrossSubsidy_Plan> TZ_CrossSubsidy_Plan { get; set; } = null!;
		public DbSet<TZ_EconomyCosts_Plan> TZ_EconomyCosts_Plan { get; set; } = null!;
		public DbSet<TZ_IndexesNVV_Plan> TZ_IndexesNVV_Plan { get; set; } = null!;
		public DbSet<TZ_MethodIndexation_Plan> TZ_MethodIndexation_Plan { get; set; } = null!;
		public DbSet<TZ_MethodProfitInvest_Plan> TZ_MethodProfitInvest_Plan { get; set; } = null!;
		public DbSet<TZ_MethodAnaloguesComparison_Plan> TZ_MethodAnaloguesComparison_Plan { get; set; } = null!;
		public DbSet<TZ_CostFinanceCapInv_Fact> TZ_CostFinanceCapInv_Fact { get; set; } = null!;
		public DbSet<TZ_CostFinanceCapInvAddValues_Fact> TZ_CostFinanceCapInvAddValues_Fact { get; set; } = null!;
		public DbSet<TZ_History> TZ_History { get; set; } = null!;
		public DbSet<TZ_Perspective> TZ_Perspective { get; set; } = null!;
		#endregion
		// tso

        //networks
        #region
        public DbSet<Dict_Diameters_Consumptions> Dict_Diameters_Consumptions { get; set; } = null!;
        public DbSet<Dict_IzolTypes> Dict_IzolTypes { get; set; } = null!;
        public DbSet<Dict_NormLoss_History> Dict_NormLoss_History { get; set; } = null!;
        #endregion
        //Output Forms
        #region
        public DbSet<ReportSectionViewModel> ReportSection { get; set; }
        public DbSet<ReportsViewModel> Reports { get; set; }
        /// <summary>
        /// ТСО в ЕТО
        /// </summary>
        public DbSet<TSOinETOListViewModel> TSOinETOListViewModels { get; set; }
        /// <summary>
        /// Средняя тепловая мощность ЦТП
        /// </summary>
        public DbSet<CTPAvgHeatPowerViewModel> CTPAvgHeatPowerViewModels { get; set; }
        /// <summary>
        /// Средняя тепловая мощность ИТП
        /// </summary>
        public DbSet<ITPAvgHeatPowerViewModel> ITPAvgHeatPowerViewModels { get; set; }
        /// <summary>
        /// Схема присоединения ЦТП и ИТП
        /// </summary>
        public DbSet<SchemConnectHeatViewModel> SchemConnectHeatViewModels { get; set; }
        /// <summary>
        /// Приборы учета тепловой энергии, установленных на ЦТП (ИТП) ПАО «МОЭК»
        /// </summary>
        public DbSet<MeterDeviceViewModel> MeterDeviceViewModels { get; set; }
        /// <summary>
        /// Основнеые сведения по НПС
        /// </summary>
        public DbSet<NPSBasicInformationViewModel> NPSBasicInformationViewModels { get; set; }
        /// <summary>
        /// Характеристика оборудования насосных станций теплосетевой организации
        /// </summary>
        public DbSet<CharactNPSViewModel> CharactNPSViewModels { get; set; }
        /// <summary>
        /// Перечень потребителей тепловой энергии, подключенных к существующим тепловым сетям за период актуализации
        /// </summary>
        public DbSet<ConsumersConnectHeatNetworksViewModel> ConsumersConnectHeatNetworksViewModels { get; set; }
        /// <summary>
        /// Снос (вывод из эксплуатации) жилых зданий
        /// </summary>
        public DbSet<removebuildingViewModel> RemovebuildingViewModels { get; set; }
        /// <summary>
        /// Снос (вывод из эксплуатации) жилых зданий с фильтром
        /// </summary>
        public DbSet<removebuildingFilterViewModel> RemovebuildingFilterViewModels { get; set; }
        /// <summary>
        /// Регионы москвы
        /// </summary>
        public DbSet<RegionDistrictMoscowViewModel> RegionDistrictMoscowViewModels { get; set; }
        /// <summary>
        /// Тепловая нагрузка в поселении, городском округе, городе федерального значения за A-тый год актуализации схемы теплоснабжения
        /// </summary>
        public DbSet<HeatLoadViewModel> HeatLoadViewModels { get; set; }
        /// <summary>
        /// тепловая нагрузка на отопление и вентиляцию и гвс в проектируемых жилых зданиях и общественно-деловых зданиях на период разработки или актуализации схемы теплоснабжения, Гкал/ч
        /// </summary>
        public DbSet<IncreaseHeatLoadViewModel> IncreaseHeatLoadViewModels { get; set; }
        /// <summary>
        /// Перечень потребителей тепловой энергии, планируемых к подключению в следующую пятилетку
        /// </summary>
        public DbSet<PerspectiveConsumersViewModel> PerspectiveConsumersViewModels { get; set; }
        /// <summary>
        /// Расчетные нагрузки, предоставленные по территориальному делению, систем т/с, ЕТО, источникам и эксплутационным огранизациям 
        /// </summary>
        public DbSet<CalculateLoadsTerritorialDivisionViewModel> CalculateLoadsTerritorialDivisionViewModels { get; set; }
        /// <summary>
        /// Сводная таблица с расчетными нагрузками в паре и в гвс по типам источников, организациям и территориальному делению
        /// </summary>
        public DbSet<SummaryTableCalculateLoadsViewModel> SummaryTableCalculateLoadsViewModels { get; set; }
        #endregion

        // схема heat_points
        #region heat_points models
        public DbSet<HP_Perspective> HP_Perspective { get; set; } = null!;
        public DbSet<HP_History> HP_History { get; set; } = null!;
        public DbSet<HeatPoint> HeatPoint { get; set; } = null!;
        public DbSet<HP_TankBatteryAvailables> HP_TankBatteryAvailables { get; set; } = null!;
        public DbSet<HP_HeatExchangerMapps> HP_HeatExchangerMapps { get; set; } = null!;
        public DbSet<HP_PumpMapps> HP_PumpMapps { get; set; } = null!;
        public DbSet<Dict_HtExchangerEquipTypes> Dict_HtExchangerEquipTypes { get; set; } = null!;
        public DbSet<Dict_PumpTypes> Dict_PumpTypes { get; set; } = null!;
        public DbSet<Equipments> Equipments { get; set; } = null!;
        public DbSet<Dict_HeatExchangerTypes> Dict_HeatExchangerTypes { get; set; } = null!;
        public DbSet<Dict_StageGVSSchemes> Dict_StageGVSSchemes { get; set; } = null!;
        public DbSet<Dict_HPSchemes> Dict_HPSchemes { get; set; } = null!;
        public DbSet<Dict_ConnectTypesHeat> Dict_ConnectTypesHeats { get; set; } = null!;
        public DbSet<Dict_ConnectTypesVent> Dict_ConnectTypesVents { get; set; } = null!;
        public DbSet<Dict_ConnectTypesHW> Dict_ConnectTypesHWs { get; set; } = null!;
        public DbSet<HP_Automatization> HP_Automatization { get; set; } = null!;
        public DbSet<Dict_LoadTypes> Dict_LoadTypes { get; set; } = null!;

        #endregion

        #region consumers
        public DbSet<Contracts> Contracts { get; set; } = null!;
        public DbSet<Contracts_History> Contracts_Histories { get; set; } = null!;
        public DbSet<Buildings> Buildings { get; set; } = null!;
        public DbSet<Buildings_History> buildings_Histories { get; set; } = null!;
        public DbSet<Dict_Floors> Dict_Floors { get; set; } = null!;
        public DbSet<Dict_CalcMethodTypes> Dict_CalcMethodTypes { get; set; } = null!;
        public DbSet<Dict_Statuses> Dict_Statuses { get; set; } = null!;
        public DbSet<BuildingCharacteristicsViewModel> BuildingCharacteristicsViewModels { get; set; }
        public DbSet<CoefBuildConsumer_History> CoefBuildConsumer_Histories { get; set; } = null!;
        public DbSet<DevProgramms> DevProgramms { get; set; } = null!;
        public DbSet<DevProgrammsConsumers> DevProgrammsConsumers { get; set; } = null!;
        public DbSet<CoefHeatLoss_History> CoefHeatLoss_Histories { get; set; } = null!;
        public DbSet<CoefHeatConsumption_Perspective> CoefHeatConsumption_Perspectives { get; set; } = null!;
        #endregion
        // виртуальные классы
        #region
        public virtual DbSet<TSOListViewModel> TSOListViewModel { get; set; }
        public virtual DbSet<TSOPerspectiveListViewModel> TSOPerspectiveListViewModel { get; set; }
        public virtual DbSet<TSOResponsiblePersonsListViewModel> TSOResponsiblePersonsListViewModel { get; set; }
        public virtual DbSet<TSOSummaryDataListViewModel> TSOSummaryDataListViewModel { get; set; }
        public virtual DbSet<TSOSourcesDataListViewModel> TSOSourcesDataListViewModel { get; set; }
        public virtual DbSet<TZOneSourcesDataListViewModel> TZOneSourcesDataListViewModel { get; set; }
        public virtual DbSet<TSOHPDataListViewModel> TSOHPDataListViewModel { get; set; }
        public virtual DbSet<TZOneHPDataListViewModel> TZOneHPDataListViewModel { get; set; }
        public virtual DbSet<TZOneNetworksDataListViewModel> TZOneNetworksDataListViewModel { get; set; }
        public virtual DbSet<TZOnePSDataListViewModel> TZOnePSDataListViewModel { get; set; }
        public virtual DbSet<TSOAdditionalDataListViewModel> TSOAdditionalDataListViewModel { get; set; }
        public virtual DbSet<TZObjectsDataViewModel> TZObjectsDataViewModel { get; set; }
        //Тепловой баланс
        public virtual DbSet<TZProductionDataViewModel> TZProductionDataViewModel { get; set; }
        public virtual DbSet<TZOneProductionDataViewModel> TZOneProductionDataViewModel { get; set; }
        public virtual DbSet<TZInOutEnergyPlanListViewModel> TZInOutEnergyPlanListViewModel { get; set; }
        public virtual DbSet<TZOutputEnergyDataViewModel> TZOutputEnergyDataViewModel { get; set; }
        public virtual DbSet<TZOutputEnergySourcesListDataViewModel> TZOutputEnergySourcesListDataViewModel { get; set; }
        public virtual DbSet<TZOutputEnergySourceOneDataViewModel> TZOutputEnergySourceOneDataViewModel { get; set; }
        public virtual DbSet<TZTransferDataViewModel> TZTransferDataViewModel { get; set; }
        public virtual DbSet<TZOneTransferDataViewModel> TZOneTransferDataViewModel { get; set; }
        public virtual DbSet<TZOutputTransferEnergyPlanListViewModel> TZOutputTransferEnergyPlanListViewModel { get; set; }
        public virtual DbSet<TZOutputTransferEnergyDataViewModel> TZOutputTransferEnergyDataViewModel { get; set; }
        public virtual DbSet<TZOutputTransferEnergySourcesListDataViewModel> TZOutputTransferEnergySourcesListDataViewModel { get; set; }
        public virtual DbSet<TZOutputTransferEnergySourceOneDataViewModel> TZOutputTransferEnergySourceOneDataViewModel { get; set; }
        //Затраты и цены
        public virtual DbSet<TZCalcCostsDataViewModel> TZCalcCostsDataViewModel { get; set; }
        public virtual DbSet<TZCalcCostsOnTransportDataViewModel> TZCalcCostsOnTransportDataViewModel { get; set; }
        public virtual DbSet<TZCalcCostsOnProductionDataViewModel> TZCalcCostsOnProductionDataViewModel { get; set; }
        public virtual DbSet<TZOneCalcCostsDataViewModel> TZOneCalcCostsDataViewModel { get; set; }
        public virtual DbSet<TZCalcAllCostsViewModel> TZCalcAllCostsViewModel { get; set; }
        public virtual DbSet<TZCalcCostsDistributionViewModel> TZCalcCostsDistributionViewModel { get; set; }
        public virtual DbSet<TZFuelPriceDataViewModel> TZFuelPriceDataViewModel { get; set; }
        public virtual DbSet<TZFuelTechNeedFactListViewModel> TZFuelTechNeedFactListViewModel { get; set; }
        public virtual DbSet<TZFuelTechNeedPlanListViewModel> TZFuelTechNeedPlanListViewModel { get; set; }
        public virtual DbSet<TZWaterNeedCostsViewModel> TZWaterNeedCostsViewModel { get; set; }
        public virtual DbSet<TZFuelReserveCostsDataViewModel> TZFuelReserveCostsDataViewModel { get; set; }
        public virtual DbSet<TZHeatCarrierNeedCostsViewModel> TZHeatCarrierNeedCostsViewModel { get; set; }
        public virtual DbSet<TZEnergyNeedCostsViewModel> TZEnergyNeedCostsViewModel { get; set; }
        public virtual DbSet<TZHeatEnergyNeedCostsViewModel> TZHeatEnergyNeedCostsViewModel { get; set; }
        public virtual DbSet<TZInputEnergyFactViewModel> TZInputEnergyFactViewModel { get; set; }
        public virtual DbSet<TZInputEnergyPlanViewModel> TZInputEnergyPlanViewModel { get; set; }
        public virtual DbSet<TZElectroEnergyCostsViewModel> TZElectroEnergyCostsViewModel { get; set; }
        public virtual DbSet<TZSalaryCostsViewModel> TZSalaryCostsViewModel { get; set; }
        public virtual DbSet<TZMaterialsCostsViewModel> TZMaterialsCostsViewModel { get; set; }
        public virtual DbSet<TZOtherOrgCostsViewModel> TZOtherOrgCostsViewModel { get; set; }
        public virtual DbSet<TZOrgServiceOtherCostsViewModel> TZOrgServiceOtherCostsViewModel { get; set; }
        public virtual DbSet<TZRepairFundingCostsViewModel> TZRepairFundingCostsViewModel { get; set; }
        public virtual DbSet<TZDecomissionCostsViewModel> TZDecomissionCostsViewModel { get; set; }
        public virtual DbSet<TZOtherOperatingsCostsViewModel> TZOtherOperatingsCostsViewModel { get; set; }
        public virtual DbSet<TZTaxesCostsViewModel> TZTaxesCostsViewModel { get; set; }
        public virtual DbSet<TZAmortizationCostsViewModel> TZAmortizationCostsViewModel { get; set; }
        public virtual DbSet<TZRegOrgServiceCostsViewModel> TZRegOrgServiceCostsViewModel { get; set; }
        public virtual DbSet<TZIncomeTaxCostsViewModel> TZIncomeTaxCostsViewModel { get; set; }
        public virtual DbSet<TZUncontrolOtherCostsViewModel> TZUncontrolOtherCostsViewModel { get; set; }
        public virtual DbSet<TZSocialDeductionCostsViewModel> TZSocialDeductionCostsViewModel { get; set; }
        /*НВВ*/
        public virtual DbSet<TZCalcCostsNVVDataViewModel> TZCalcCostsNVVDataViewModel { get; set; }
        public virtual DbSet<TZProfitNVVViewModel> TZProfitNVVViewModel { get; set; }
        public virtual DbSet<TZLostProfitOutCostsViewModel> TZLostProfitOutCostsViewModel { get; set; }
        public virtual DbSet<TZExcessFundsViewModel> TZExcessFundsViewModel { get; set; }
        public virtual DbSet<TZCapitalInvestmentViewModel> TZCapitalInvestmentViewModel { get; set; }
        public virtual DbSet<TZCrossSubsidyViewModel> TZCrossSubsidyViewModel { get; set; }
        public virtual DbSet<TZEconomyCostsViewModel> TZEconomyCostsViewModel { get; set; }
        public virtual DbSet<TZIndexesNVVViewModel> TZIndexesNVVViewModel { get; set; }
        public virtual DbSet<TZMethodIndexationViewModel> TZMethodIndexationViewModel { get; set; }
        public virtual DbSet<TZMethodProfitInvestViewModel> TZMethodProfitInvestViewModel { get; set; }
        public virtual DbSet<TZMethodAnaloguesComparisonViewModel> TZMethodAnaloguesComparisonViewModel { get; set; }
        /*НВВ*/
        /*ИП ТС и КС*/
        public virtual DbSet<TZCalcCostsIpHsCaDataViewModel> TZCalcCostsIpHsCaDataViewModel { get; set; }
        public virtual DbSet<TZExpenses_IPHS_CA_ViewModel> TZExpenses_IPHS_CA_ViewModel { get; set; }
        public virtual DbSet<TZExpensesAddValues_ViewModel> TZExpensesAddValues_ViewModel { get; set; }
        /*ИП ТС и КС*/
        //Тарифы
        public virtual DbSet<TZHeaderTariffDataViewModel> TZHeaderTariffDataViewModel { get; set; }
        public virtual DbSet<CrossSubsidizationAmount> CrossSubsidizationAmount { get; set; }
        public virtual DbSet<HeatEnergyTariffSteam> HeatEnergyTariffSteam { get; set; }
        public virtual DbSet<TwoStageTariffUnit> TwoStageTariffUnit { get; set; }
        public virtual DbSet<SingleStageTariffUnit> SingleStageTariffUnit { get; set; }
        public virtual DbSet<IndividualFeeTable> IndividualFeeTable { get; set; }
        public virtual DbSet<OpenIndividualFeeTable> OpenIndividualFeeTable { get; set; }
        public virtual DbSet<StandardizedRatesTable> StandardizedRatesTable { get; set; }
        public virtual DbSet<OpenStandardizedRatesTable> OpenStandardizedRatesTable { get; set; }
        public virtual DbSet<PowerReservePaymentTable> PowerReservePaymentTable { get; set; }
        public virtual DbSet<OpenPowerReservePaymentTable> OpenPowerReservePaymentTable { get; set; }
        public virtual DbSet<TSO_ConnectCharge_Fact> TSO_ConnectCharge_Fact { get; set; }
        public virtual DbSet<HeatEnergyTariffWater> HeatEnergyTariffWater { get; set; }
        public virtual DbSet<HeatEnergyTariffSteamHotWater> HeatEnergyTariffSteamHotWater { get; set; }
        public virtual DbSet<ETO_Amount_Preference> ETO_Amount_Preference { get; set; }
        public virtual DbSet<HeatEnergyTransfer> HeatEnergyTransfer { get; set; }
        public virtual DbSet<TZ_TariffsDoubleHE_Fact> TZ_TariffsDoubleHE_Fact { get; set; }
        public virtual DbSet<TZ_TariffsDoubleHE_Plan> TZ_TariffsDoubleHE_Plan { get; set; }
        public virtual DbSet<TZ_TariffsHETransfer_FactPlan> TZ_TariffsHETransfer_FactPlan { get; set; }
        public virtual DbSet<TZ_TariffsOthers_Fact> TZ_TariffsOthers_Fact { get; set; }
        public virtual DbSet<TZ_TariffsHESteam_FactPlan> TZ_TariffsHESteam_FactPlan { get; set; }
        public virtual DbSet<TZ_TariffsHeatCarrier_Fact> TZ_TariffsHeatCarrier_Fact { get; set; }
        public virtual DbSet<TZ_TariffsCrossSubsidy_Fact> TZ_TariffsCrossSubsidy_Fact { get; set; }
        public virtual DbSet<TZ_TariffsHE_History> TZ_TariffsHE_History { get; set; }
        public virtual DbSet<TZTariffHotWaterTabModel> TZTariffHotWaterTabModel { get; set; }
        public virtual DbSet<TZTariffSteamTabModel> TZTariffSteamTabModel { get; set; }
        public virtual DbSet<TSO_ConnectChargePC_Fact> TSO_ConnectChargePC_Fact { get; set; }
        public virtual DbSet<TSO_ReserveHPCharge_Fact> TSO_ReserveHPCharge_Fact { get; set; }

        //Спаравочные таблицы
        //Территориальное деление
        public virtual DbSet<TerritorialDivisionViewModel> TerritorialDivisionViewModels { get; set; }
        public virtual DbSet<TerritorialDivisionClimaticDataOneViewModel> TerritorialDivisionClimaticDataOneViewModels { get; set; }
        public virtual DbSet<TerritorialDivisionGeneralInfoDataOneViewModel> TerritorialDivisionGeneralInfoDataOneViewModels { get; set; }
        public virtual DbSet<TerritorialDivisionPopulationListViewModel> TerritorialDivisionPopulationDataOneViewModels { get; set; }
        public virtual DbSet<TerritorialDivisionUpdateValue> TerritorialDivisionUpdateValues { get; set; }
        //Тарифные зоны
        public virtual DbSet<TariffZoneListViewModel> TariffZoneListViewModels { get; set; }
        public virtual DbSet<TariffZoneOneDataViewModel> TariffZoneOneDataViewModels { get; set; }
        public virtual DbSet<DistrictListViewModel> DistrictViewModals { get; set; }
        public virtual DbSet<TariffZoneTsoListViewModel> TariffZoneTsoViewModals { get; set; }
        //Зоны деятельности ЕТО
        public virtual DbSet<ActiveZoneETOViewModel> ActiveZoneETOViewModels { get; set; }
        public virtual DbSet<ActiveZoneETOOneDataViewModel> ActiveZoneETOOneDataViewModels { get; set; }
        //Системы теплоснабжения
        public virtual DbSet<HeatSupplySystemViewModel> HeatSupplySystemViewModels { get; set; }
        public virtual DbSet<HeatSupplySystemOneDataViewModel> HeatSupplySystemOneDataViewModels { get; set; }
        //Организации
        public virtual DbSet<OrganizationViewModel> OrganizationViewModels { get; set; }
        public virtual DbSet<OrganizationOneDataViewModel> OrganizationOneDataViewModels { get; set; }
        //Головные выводы источника
        public virtual DbSet<OutPutsSourcesListViewModel> OutPutsSourcesListViewModels { get; set; }
        public virtual DbSet<OutPutsSourcesOnetViewModel> OutPutsSourcesOnetViewModels { get; set; }
        //Типы схем тепловых пунктов
        public virtual DbSet<HPSchemesListViewModel> HPSchemesListViewModels { get; set; }
        public virtual DbSet<HPSchemesViewModel> HPSchemesViewModels { get; set; }
        //Водоподлогреватель
        public virtual DbSet<WaterHeaterViewmodel> WaterHeaterViewmodels { get; set; }
        //Поправочные коэффициенты на климат
        public virtual DbSet<CoefCorrectionClimatePerspectiveViewModel> CoefCorrectionClimatePerspectiveViewModels { get; set; }
        public virtual DbSet<CoefCorrectionClimateViewModel> CoefCorrectionClimateViewModels { get; set; }
        //Нормы плотности теплового потока
        public virtual DbSet<NormLossListViewModel> NormLossListViewModels { get; set; }
        public virtual DbSet<NormLossOneDataViewModel> NormLossOneDataViewModels { get; set; }
        //Программы развития города (поселения)
        public virtual DbSet<PerspectiveDevelopment_MainListViewModel> PerspectiveDevelopment_MainListViewModel { get; set; }
        public virtual DbSet<PerspectiveDevelopmentHeaderViewModel> PerspectiveDevelopmentHeaderViewModel { get; set; }
        public virtual DbSet<PerspectiveDevelopment_GenInfoViewModel> PerspectiveDevelopment_GenInfoViewModel { get; set; }
        public virtual DbSet<PerspectiveDevelopmentFacility_GenInfoViewModel> PerspectiveDevelopmentFacility_GenInfoViewModel { get; set; }
        public virtual DbSet<PerspectiveDevelopment_FacilityListViewModel> PerspectiveDevelopment_FacilityListViewModel { get; set; }
        public virtual DbSet<PerspectiveDevelopment_FacilitiesHeaderViewModel> PerspectiveDevelopment_FacilitiesHeaderViewModel { get; set; }
        public virtual DbSet<Dict_DevProgTypes> Dict_DevProgTypes { get; set; }
        public virtual DbSet<PerspectiveDevelopment_Facilities_DestRelCatHSViewModel> PerspectiveDevelopment_Facilities_DestRelCatHSViewModel { get; set; }
        public virtual DbSet<PerspectiveDevelopment_Facilities_BuildingVolumeHeatLoadsViewModel> PerspectiveDevelopment_Facilities_BuildingVolumeHeatLoadsViewModel { get; set; }
        //Тепловые пункты и потребители
        //Тепловые пункты
        public virtual DbSet<HP_SwitchableUnit> HP_SwitchableUnit { get; set; }
        public virtual DbSet<HP_NameListUnit> HP_NameListUnit { get; set; }
        public virtual DbSet<HP_MainListUnit> HP_MainListUnit { get; set; }
        public virtual DbSet<HPAddRemoveGenInfoDataViewModel> HPAddRemoveGenInfoDataViewModel { get; set; }
        public virtual DbSet<HPHeaderAddRemoveDataViewModel> HPHeaderAddRemoveDataViewModel { get; set; }
        public virtual DbSet<HPAddRemoveYearImplementSchemeParamDataViewModel> HPAddRemoveYearImplementSchemeParamDataViewModel { get; set; }
        public virtual DbSet<HPAddRemoveHP_HeatExchangerMappsDataViewModel> HPAddRemoveHP_HeatExchangerMappsDataViewModel { get; set; }
        public virtual DbSet<HPAddRemove_AutomatizationViewModel> HPAddRemove_AutomatizationViewModel { get; set; }
        public virtual DbSet<HPAddRemoveHP_PumpMappsDataViewModel> HPAddRemoveHP_PumpMappsDataViewModel { get; set; }
        public virtual DbSet<HPAddRemoveHP_EquipmentByMarkViewModel> HPAddRemoveHP_EquipmentByMarkViewModel { get; set; }
        public virtual DbSet<Dict_AccResourcesTypes> Dict_AccResourcesTypes { get; set; }
        public virtual DbSet<HP_AccResources> HP_AccResources { get; set; }
        public virtual DbSet<HPAddRemove_AccResourcesViewModel> HPAddRemove_AccResourcesViewModel { get; set; }
        public virtual DbSet<HPAddRemove_NumSignOtherDbViewModel> HPAddRemove_NumSignOtherDbViewModel { get; set; }
        public virtual DbSet<HPAddRemove_DocsFootageViewModel> HPAddRemove_DocsFootageViewModel { get; set; }
        public virtual DbSet<DocumentTypes> DocumentTypes { get; set; }
        public virtual DbSet<HP_DocsPhotos_History> HP_DocsPhotos_History { get; set; }

        //Тепловые пункты и потребители
        //Потребители
        public virtual DbSet<ConsumerHeaderAddRemoveDataViewModel> ConsumerHeaderAddRemoveDataViewModel { get; set; }
        public virtual DbSet<Consumer_MainListUnit> Consumer_MainListUnit { get; set; }
        public virtual DbSet<ConsumerAddRemoveGenInfoDataViewModel> ConsumerAddRemoveGenInfoDataViewModel { get; set; }
        public virtual DbSet<Consumers_DestRelCatHSDataViewModel> Consumers_DestRelCatHSDataViewModel { get; set; }
        public virtual DbSet<Consumers_HeatLoadsSupplyContractDataViewModel> Consumers_HeatLoadsSupplyContractDataViewModel { get; set; }
        public virtual DbSet<Consumers_YearParamsCalcLoadViewModel> Consumers_YearParamsCalcLoadViewModel { get; set; }
        public virtual DbSet<Consumers_YearHeatConsumptionViewModel_Fact> Consumers_YearHeatConsumptionViewModel_Fact { get; set; }
        public virtual DbSet<Consumers_YearHeatConsumptionViewModel_Perspective> Consumers_YearHeatConsumptionViewModel_Perspective { get; set; }
        public virtual DbSet<ConsumersHistory> ConsumersHistory { get; set; }
        public virtual DbSet<Consumers> Consumers { get; set; }
        public virtual DbSet<Dict_ReliabilityCategories> Dict_ReliabilityCategories { get; set; }
        public virtual DbSet<Dict_CalcPurposeTypes> Dict_CalcPurposeTypes { get; set; }
        public virtual DbSet<Dict_ProdSupplyType> Dict_ProdSupplyType { get; set; }
        public virtual DbSet<Dict_MainPurposeTypes> Dict_MainPurposeTypes { get; set; }
        public virtual DbSet<ContractLoads> ContractLoads { get; set; }
        public virtual DbSet<ContractConsumers> ContractConsumers { get; set; }
        public virtual DbSet<LoadsCalculates> LoadsCalculates { get; set; }
        public virtual DbSet<HeatWaterConsumptions_Fact> HeatWaterConsumptions_Fact { get; set; }
        public virtual DbSet<HeatConsumptions_Perspective> HeatConsumptions_Perspective { get; set; }
        public virtual DbSet<Consumers_NumSignOtherDBViewModel> Consumers_NumSignOtherDBViewModel { get; set; }
        public virtual DbSet<Consumers_PerspectiveDev_SubPartialViewModel> Consumers_PerspectiveDev_SubPartialViewModel { get; set; }
        public virtual DbSet<Consumers_PerspectiveDev_YearDynamicViewModel> Consumers_PerspectiveDev_YearDynamicViewModel { get; set; }
        public virtual DbSet<Consumers_PerspectiveDevelopmentViewModel> Consumers_PerspectiveDevelopmentViewModel { get; set; }
        public virtual DbSet<Dict_CalcHeatLoadsTypes> Dict_CalcHeatLoadsTypes { get; set; }
        public virtual DbSet<Dict_CalcAreaTypes> Dict_CalcAreaTypes { get; set; }
        public virtual DbSet<DevProgrammsAreaCalcCoefs> DevProgrammsAreaCalcCoefs { get; set; }
        public virtual DbSet<DevProgrammsLoadsCalcCoefs> DevProgrammsLoadsCalcCoefs { get; set; }
        public virtual DbSet<DevProgrammsLoadsContract> DevProgrammsLoadsContract { get; set; }
        public virtual DbSet<DevProgrammsLoadsDeclared> DevProgrammsLoadsDeclared { get; set; }
        public virtual DbSet<DevProgrammsLoadsCalculated> DevProgrammsLoadsCalculated { get; set; }

        //Нагрузки и расходы
        public virtual DbSet<LoadsAndExpensivesMainList> LoadsAndExpensivesMainList { get; set; }
        public virtual DbSet<CalcCoefViewModel> CalcCoefViewModels { get; set; }
        public virtual DbSet<CoefRelationHeatVentLoadViewModel> CoefRelationHeatVentLoadViewModels { get; set; }
        public virtual DbSet<CoefEnergyEfficiencyViewModel> CoefEnergyEfficiencyViewModels { get; set; }
        public virtual DbSet<CalcConsumptionViewModel> CalcConsumptionViewModels { get; set; }
        public virtual DbSet<CalcConsumptionGVSViewModel> CalcConsumptionGVSViewModels { get; set; }
        public virtual DbSet<LoadsAndExpensivesPerspectiveMainList> LoadsAndExpensivesPerspectiveMainList { get; set; }

        //Оборудование
        public virtual DbSet<HeatPointsEquipment> HeatPointsEquipment { get; set; }

        //Схемы присоединения нагрузок
        public virtual DbSet<LoadAttachmentSchemasModel> LoadAttachmentSchemasModel { get; set; }

        //Автоматизация
        public virtual DbSet<AutomatizationModel> AutomatizationModel { get; set; }

        //Учёт энергетических ресурсов
        public virtual DbSet<EnergyResourceAccountingModel> EnergyResourceAccountingModel { get; set; }

        //Договора
        public virtual DbSet<ContractListViewModel> ContractListViewModels { get; set; }
        public virtual DbSet<ContractOneDataViewModel> ContractOneDataViewModels { get; set; }
        public virtual DbSet<ContractConsumerListViewModel> ContractConsumerListViewModels { get; set; }

        //Здания
        public virtual DbSet<BuildingListViewModel> BuildingListViewModels { get; set; }
        public virtual DbSet<BuildingOneDataViewModel> BuildingOneDataViewModels { get; set; }
        public virtual DbSet<BuildingConsumerViewModel> BuildingConsumerViewModels { get; set; }

		//Источники
		public virtual DbSet<SourcesViewModel> SourcesViewModels { get; set; }
		public virtual DbSet<SourceGeneralInfoViewModel> SourceGeneralInfoViewModels  { get; set; }
		public virtual DbSet<SourcePerspectiveViewModel> SourcePerspectiveViewModels { get; set; }
        public virtual DbSet<Source_DocsFootageViewModel> Source_DocsFootageViewModels { get; set; }
        public virtual DbSet<SourceHeatPowerOutputViewModel> SourceHeatPowerOutputViewModels { get; set; }
        public virtual DbSet<SourcesEquipTurbineViewModel> SourcesEquipTurbineViewModels { get; set; }
		#endregion

		// табличные функции
		#region
		public IQueryable<Org_List> fnt_GetOrgList(int data_status, short org_activity_type_id) => FromExpression(() => fnt_GetOrgList(data_status, org_activity_type_id));
		public IQueryable<ETOViewModel> fnt_GetETOStatusList(int tso_id) => FromExpression(() => fnt_GetETOStatusList(tso_id));
		public IQueryable<TZNames_List> fnt_GetTZTSOList(int data_status, int perspective_year, string activity_type) => FromExpression(() => fnt_GetTZTSOList(data_status, perspective_year, activity_type));
		public IQueryable<TZTerritoryViewModel> fnt_GetTZTerritoryList(int tz_id) => FromExpression(() => fnt_GetTZTerritoryList(tz_id));
		public IQueryable<TZSourcesViewModel> fnt_GetSourcesByTZList(int data_status, int tz_id) => FromExpression(() => fnt_GetSourcesByTZList(data_status, tz_id));
		public IQueryable<HPSourcesViewModel> fnt_GetSourcesByHPList(int data_status) => FromExpression(() => fnt_GetSourcesByHPList(data_status));
		public IQueryable<Values_List> fnt_GetUnomPPListByChars(string chars, int data_status) => FromExpression(() => fnt_GetUnomPPListByChars(chars, data_status));
		public IQueryable<Values_List> fnt_GetUnomTpAddressListByChars(string chars, int data_status) => FromExpression(() => fnt_GetUnomTpAddressListByChars(chars, data_status));
        public IQueryable<Values_List> fnt_GetSupplyContractsByChars(string chars) => FromExpression(() => fnt_GetSupplyContractsByChars(chars));
        public IQueryable<Values_List> fnt_GetUnomConsumerAddressListByChars(string chars, int data_status) => FromExpression(() => fnt_GetUnomConsumerAddressListByChars(chars, data_status));
        public IQueryable<Values_List> fnt_GetUnomConsumerAddressListByCharsForObjDevProg(string unom_cons_string, int consumer_id, int building_id, int dev_prog_id, int data_status) => FromExpression(() => fnt_GetUnomConsumerAddressListByCharsForObjDevProg(unom_cons_string, consumer_id, building_id, dev_prog_id, data_status));
        public IQueryable<Values_List> fnt_GetUnomPerspectiveDevelopmentList() => FromExpression(() => fnt_GetUnomPerspectiveDevelopmentList());
        public IQueryable<Values_List> fnt_GetFacilityNumbersPerspectiveDevelopmentList(int dev_prog_id) => FromExpression(() => fnt_GetFacilityNumbersPerspectiveDevelopmentList(dev_prog_id));
        public IQueryable<ConsumerDistrictUnom> fnt_GetDistrictUnomAddress(int consumer_id, int data_status) => FromExpression(() => fnt_GetDistrictUnomAddress(consumer_id, data_status));
        public IQueryable<Values_List> fnt_GetBildingUnomConsumerIdOne(int data_status, int consumer_id) => FromExpression(() => fnt_GetBildingUnomConsumerIdOne(data_status, consumer_id));
        public IQueryable<Values_List> fnt_GetOrgOwnerInnListByChars(string chars, int data_status) => FromExpression(() => fnt_GetOrgOwnerInnListByChars(chars, data_status));
        public IQueryable<Values_List> fnt_GetHpLocations() => FromExpression(() => fnt_GetHpLocations());
        public IQueryable<Values_List> fnt_GetHpTypeLocations() => FromExpression(() => fnt_GetHpTypeLocations());
        public IQueryable<Values_List> fnt_GetEquipmentMarkList() => FromExpression(() => fnt_GetEquipmentMarkList());
        public IQueryable<Values_List> fnt_GetAutomaticList() => FromExpression(() => fnt_GetAutomaticList());
        public IQueryable<Values_List> fnt_GetPumpMarkList() => FromExpression(() => fnt_GetPumpMarkList());
        public IQueryable<Values_List> fnt_GetHpStatusesForFilter() => FromExpression(() => fnt_GetHpStatusesForFilter());
        public IQueryable<Values_List> fnt_GetHpDistricts() => FromExpression(() => fnt_GetHpDistricts());
        public IQueryable<TZNames_List> fnt_GetTZUnomList(int data_status) => FromExpression(() => fnt_GetTZUnomList(data_status));
        public IQueryable<Values_List> fnt_GetDevProgramsList(int consumer_id) => FromExpression(() => fnt_GetDevProgramsList(consumer_id));
        public IQueryable<Values_List> fnt_GetUnomOutputsList(int source_id) => FromExpression(() => fnt_GetUnomOutputsList(source_id));
        public IQueryable<HPAddRemoveHP_EquipmentByMarkViewModel> fnt_GetHP_EquipmentByMark(int equip_id) => FromExpression(() => fnt_GetHP_EquipmentByMark(equip_id));
        public IQueryable<HPAddRemoveHP_PumpByMarkViewModel> fnt_GetHP_PumpByMark(int equip_id) => FromExpression(() => fnt_GetHP_PumpByMark(equip_id));
        public IQueryable<HpDiamHtSupply> fnt_GetHpDiamHtSupplyIdList(int data_status, int heat_network_id) => FromExpression(() => fnt_GetHpDiamHtSupplyIdList(data_status, heat_network_id));
        public IQueryable<Values_List> fnt_GetSourceUnomByUnomHpList(int data_status, int heat_point_id) => FromExpression(() => fnt_GetSourceUnomByUnomHpList(data_status, heat_point_id));
        public IQueryable<Values_List> fnt_GetDevProgramSelected(int consumer_id, string? decision_num) => FromExpression(() => fnt_GetDevProgramSelected(consumer_id, decision_num));
        public IQueryable<Values_List> fnt_GetDevProgramSelectedStandardized(int tso_id, string? decision_num) => FromExpression(() => fnt_GetDevProgramSelectedStandardized(tso_id, decision_num));
        public IQueryable<Values_List> fnt_GetDevProgramSelectedPowerReserve(int tso_id, string? decision_num) => FromExpression(() => fnt_GetDevProgramSelectedPowerReserve(tso_id, decision_num));
        public IQueryable<IndividualFeeApplNameObject> fnt_GetApplName(int consumer_id, int dev_prog_id) => FromExpression(() => fnt_GetApplName(consumer_id, dev_prog_id));
        public IQueryable<Values_List> fnt_GetDecisionStatusList() => FromExpression(() => fnt_GetDecisionStatusList());
        //public virtual DbSet<Org_List> Org_List { get; set; }
        public IQueryable<Values_List> GetReportSectionList() => FromExpression(() => GetReportSectionList());
        public IQueryable<Values_List> GetReportsList(int secion_id) => FromExpression(() => GetReportsList(secion_id));
        public IQueryable<Values_List> fnt_GetHpStatusList() => FromExpression(() => fnt_GetHpStatusList());
        public IQueryable<Values_List> fnt_GetUnomSourceOutputList(int data_status) => FromExpression(() => fnt_GetUnomSourceOutputList(data_status));
        public IQueryable<Values_List> fnt_GetHpTempHtPlanList() => FromExpression(() => fnt_GetHpTempHtPlanList());
        public IQueryable<Values_List> fnt_GetHpTempHtTypeSchemeList() => FromExpression(() => fnt_GetHpTempHtTypeSchemeList());
        public IQueryable<Values_List> fnt_GetHpConnectTypesTechList() => FromExpression(() => fnt_GetHpConnectTypesTechList());
        public IQueryable<Values_List> fnt_GetHpConnectTypesHeatList() => FromExpression(() => fnt_GetHpConnectTypesHeatList());
        public IQueryable<Values_List> fnt_GetHpConnectTypesVentList() => FromExpression(() => fnt_GetHpConnectTypesVentList());
        public IQueryable<Values_List> fnt_GetHpConnectTypesHWList() => FromExpression(() => fnt_GetHpConnectTypesHWList());
        public IQueryable<Values_List> fnt_GetContractUnomList(int data_status) => FromExpression(() => fnt_GetContractUnomList(data_status));
        public IQueryable<Values_List> fnt_GetBildingUnomList(int data_status) => FromExpression(() => fnt_GetBildingUnomList(data_status));
        public IQueryable<Values_ListShort> fnt_GetSourcesTypeList() => FromExpression(() => fnt_GetSourcesTypeList());
        public IQueryable<Values_List> fnt_GetSourcesUnomList(int data_status) => FromExpression(() => fnt_GetSourcesUnomList(data_status));

        //Справочные таблицы
        public IQueryable<District_List> fnt_GetDistrictList() => FromExpression(() => fnt_GetDistrictList());
        public IQueryable<Region_List> fnt_GetRegionsList() => FromExpression(() => fnt_GetRegionsList());
        public IQueryable<TSO_List> fnt_GetTSOName(int data_status) => FromExpression(() => fnt_GetTSOName(data_status));
        public IQueryable<Values_List> fnt_GetDistrictTZ(int tz_id) => FromExpression(() => fnt_GetDistrictTZ(tz_id));
        public IQueryable<Values_List> fnt_GetUnomNormLossList(int data_status, int id) => FromExpression(() => fnt_GetUnomNormLossList(data_status, id));
        public IQueryable<Values_ListShort> fnt_PurporseTypeBuildList() => FromExpression(() => fnt_PurporseTypeBuildList());
        //public IQueryable<UnomHSSListViewModel> fn_GetUnomHssList() => FromExpression(() => fn_GetUnomHssList());
        public IQueryable<Values_List> fnt_GetDistrictRegionList() => FromExpression(() => fnt_GetDistrictRegionList());


        #endregion
        //

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PerspectiveYears>().HasKey(u => new { u.data_status, u.perspective_year });

            modelBuilder.Entity<EX_Logs>().HasKey(u => new { u.Id });

            modelBuilder.Entity<HP_DocsPhotos_History>().HasKey(u => new { u.data_status, u.heat_point_id, u.doc_num });
            modelBuilder.Entity<HP_AccResources>().HasKey(u => new { u.data_status, u.heat_point_id, u.device_num });
            modelBuilder.Entity<Dict_ReliabilityCategories>().HasKey(u => new { u.Id });
            modelBuilder.Entity<Dict_CalcPurposeTypes>().HasKey(u => new { u.Id });
            modelBuilder.Entity<Dict_ProdSupplyType>().HasKey(u => new { u.Id });
            modelBuilder.Entity<Dict_MainPurposeTypes>().HasKey(u => new { u.Id });
            modelBuilder.Entity<Dict_CalcHeatLoadsTypes>().HasKey(u => new { u.Id });
            modelBuilder.Entity<Dict_CalcAreaTypes>().HasKey(u => new { u.Id });
            modelBuilder.Entity<Dict_DevProgTypes>().HasKey(u => new { u.Id });
            modelBuilder.Entity<ConsumersHistory>().HasKey(u => new { u.data_status, u.consumer_id });
            modelBuilder.Entity<Consumers>().HasKey(u => new { u.consumer_id });
            modelBuilder.Entity<ContractLoads>().HasKey(u => new { u.data_status, u.consumer_id });
            modelBuilder.Entity<ContractConsumers>().HasKey(u => new { u.data_status, u.consumer_id });
            modelBuilder.Entity<LoadsCalculates>().HasKey(u => new { u.data_status, u.consumer_id, u.perspective_year });
            modelBuilder.Entity<HeatWaterConsumptions_Fact>().HasKey(u => new { u.data_status, u.consumer_id });
            modelBuilder.Entity<DevProgrammsConsumers>().HasKey(u => new { u.consumer_id, u.dev_prog_id });
            modelBuilder.Entity<DevProgrammsAreaCalcCoefs>().HasKey(u => new { u.consumer_id, u.dev_prog_id });
            modelBuilder.Entity<DevProgrammsLoadsCalcCoefs>().HasKey(u => new { u.consumer_id, u.dev_prog_id });
            modelBuilder.Entity<DevProgrammsLoadsContract>().HasKey(u => new { u.consumer_id, u.dev_prog_id });
            modelBuilder.Entity<DevProgrammsLoadsDeclared>().HasKey(u => new { u.consumer_id, u.dev_prog_id });
            modelBuilder.Entity<DevProgrammsLoadsCalculated>().HasKey(u => new { u.consumer_id, u.dev_prog_id, u.perspective_year });
            modelBuilder.Entity<DevProgramms>().HasKey(u => new { u.dev_prog_id });
            modelBuilder.Entity<HeatConsumptions_Perspective>().HasKey(u => new { u.data_status, u.consumer_id, u.perspective_year });
            modelBuilder.Entity<HP_PumpMapps>().HasKey(u => new { u.data_status, u.heat_point_id, u.pump_num });
            modelBuilder.Entity<HP_HeatExchangerMapps>().HasKey(u => new { u.data_status, u.heat_point_id, u.ht_exch_num });
            modelBuilder.Entity<Equipments>().HasKey(u => new { u.equip_id });
            modelBuilder.Entity<Dict_LoadTypes>().HasKey(u => new { u.Id });
            modelBuilder.Entity<HeatPoint>().HasKey(u => new { u.heat_point_id });
            modelBuilder.Entity<HP_History>().HasKey(u => new { u.data_status, u.heat_point_id });
            modelBuilder.Entity<HP_TankBatteryAvailables>().HasKey(u => new { u.data_status, u.heat_point_id });
            modelBuilder.Entity<HP_Automatization>().HasKey(u => new { u.data_status, u.heat_point_id });
            modelBuilder.Entity<Dict_HeatExchangerTypes>().HasKey(u => new { u.Id });
            modelBuilder.Entity<Dict_StageGVSSchemes>().HasKey(u => new { u.Id });
            modelBuilder.Entity<HP_HeatExchangerMapps>().HasKey(u => new { u.data_status, u.heat_point_id, u.ht_exch_num });
            modelBuilder.Entity<HP_Perspective>().HasKey(u => new { u.data_status, u.heat_point_id, u.perspective_year });
            modelBuilder.Entity<Sources>().HasKey(u => new { u.source_id });
            modelBuilder.Entity<S_Outputs>().HasKey(u => new { u.source_output_id });
            modelBuilder.Entity<ResponsiblePersons>().HasKey(u => new { u.org_id, u.data_status, u.person_num });
            modelBuilder.Entity<Org_History>().HasKey(u => new { u.org_id, u.data_status });
            modelBuilder.Entity<ETO_DistrictsMapps>().HasKey(u => new { u.eto_id, u.district_id });
            modelBuilder.Entity<ETO_LayersMapping>().HasKey(u => new { u.eto_id, u.layer_id, u.layer_sys });
            modelBuilder.Entity<TSO_AdditionalValues>().HasKey(u => new { u.tso_id, u.data_status });
            modelBuilder.Entity<TSO_ConnectCharge_Fact>().HasKey(u => new { u.tso_id, u.decision_num });
            modelBuilder.Entity<TSO_ConnectChargePC_Fact>().HasKey(u => new { u.consumer_id, u.decision_num });
            modelBuilder.Entity<TSO_ReserveHPCharge_Fact>().HasKey(u => new { u.tso_id, u.decision_num });
            modelBuilder.Entity<TSO_ETOMapps>().HasKey(u => new { u.tso_id, u.data_status, u.eto_id });
            modelBuilder.Entity<TSO_History>().HasKey(u => new { u.org_id, u.data_status });
            modelBuilder.Entity<TSO_Perspective>().HasKey(u => new { u.org_id, u.data_status, u.perspective_year });
            modelBuilder.Entity<TZ_DistrictsMapps>().HasKey(u => new { u.tz_id, u.district_id });
            modelBuilder.Entity<TZ_History>().HasKey(u => new { u.tz_id, u.data_status });
            modelBuilder.Entity<TZ_Perspective>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year });
            modelBuilder.Entity<TZ_Amortization_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_Amortization_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostRegOrgService_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostRegOrgService_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostIncomeTax_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostIncomeTax_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_BalanceHeatEnergy_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_BalanceHeatEnergy_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_BalanceHeatEnergyTransfer_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_BalanceHeatEnergyTransfer_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CapitalInvestment_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostFinanceCapInv_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id, u.finance_type_id });
            modelBuilder.Entity<TZ_CostFinanceCapInvAddValues_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostOperatingsOther_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostOperatingsOther_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostRepairFunding_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostRepairFunding_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostDecomission_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostDecomission_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostFuelReserve_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostFuelReserve_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostOrgOtherWorkService_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostOrgOtherWorkService_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostOtherOrg_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostOtherOrg_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostRawMaterials_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostRawMaterials_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostRequiredTaxes_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostRequiredTaxes_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CrossSubsidy_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_DinamicValueNVV_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id, u.calc_year });
            modelBuilder.Entity<TZ_EconomyCosts_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_ElectroEnergyAllNeeds_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id, u.voltage_type_id });
            modelBuilder.Entity<TZ_ElectroEnergyAllNeeds_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id, u.voltage_type_id });
            modelBuilder.Entity<TZ_FuelTechNeeds_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id, u.fuel_type_id });
            modelBuilder.Entity<TZ_FuelTechNeeds_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id, u.fuel_type_id });
            modelBuilder.Entity<TZ_FuelNormConsump_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_InOutBuyEnergy_Fact>().HasKey(u => new { u.input_tz_id, u.out_tz_id, u.report_period_id, u.source_id, u.data_status, u.buy_place_id });
            modelBuilder.Entity<TZ_InOutBuyEnergy_Plan>().HasKey(u => new { u.input_tz_id, u.out_tz_id, u.report_period_id, u.source_id, u.data_status, u.perspective_year, u.buy_place_id });
            modelBuilder.Entity<TZ_InOutTransferEnergy_Fact>().HasKey(u => new { u.input_tz_id, u.out_tz_id, u.report_period_id, u.source_id, u.data_status });
            modelBuilder.Entity<TZ_InOutTransferEnergy_Plan>().HasKey(u => new { u.input_tz_id, u.out_tz_id, u.report_period_id, u.source_id, u.data_status, u.perspective_year });
            modelBuilder.Entity<TZ_CostUncontrolOther_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostUncontrolOther_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CoefsСostDistribution_Fact>().HasKey(u => new { u.tz_id, u.data_status });
            modelBuilder.Entity<TZ_CoefsСostDistribution_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year });
            modelBuilder.Entity<TZ_LostRevenue_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_ExcessFunds_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_IndexesNVV_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_MethodIndexation_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_MethodProfitInvest_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_MethodAnaloguesComparison_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_Profit_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_Profit_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_StaffValues_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id, u.staff_type_id });
            modelBuilder.Entity<TZ_StaffValues_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id, u.staff_type_id });
            modelBuilder.Entity<TZ_WaterTechNeeds_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_WaterTechNeeds_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_HeatCarrierTechNeeds_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_HeatCarrierTechNeeds_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_CostHeatTransfer_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_CostHeatTransfer_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_TariffsHE_History>().HasKey(u => new { u.tz_id, u.data_status });
            modelBuilder.Entity<TZ_TariffsHETransfer_FactPlan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_TariffsSingleHE_FactPlan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id });
            modelBuilder.Entity<TZ_TariffsDoubleHE_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id, u.consumer_type_id });
            modelBuilder.Entity<TZ_TariffsDoubleHE_Plan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id, u.consumer_type_id });
            modelBuilder.Entity<TZ_TariffsHeatCarrier_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_TariffsOthers_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id, u.consumer_type_id });
            modelBuilder.Entity<TZ_TariffsCrossSubsidy_Fact>().HasKey(u => new { u.tz_id, u.data_status, u.report_period_id });
            modelBuilder.Entity<TZ_TariffsHESteam_FactPlan>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year, u.report_period_id, u.steam_parameter_id });
            modelBuilder.Entity<DistrictsValues_History>().HasKey(u => new { u.district_id, u.data_status });
            modelBuilder.Entity<DistrictsValues_Perspective>().HasKey(u => new { u.district_id, u.data_status, u.perspective_year });
            modelBuilder.Entity<TZ_History>().HasKey(u => new { u.tz_id, u.data_status });
            modelBuilder.Entity<TZ_Perspective>().HasKey(u => new { u.tz_id, u.data_status, u.perspective_year });
            modelBuilder.Entity<HSS_DistrictsMapps>().HasKey(u => new { u.hss_id, u.district_id });
            modelBuilder.Entity<S_History>().HasKey(u => new { u.source_id, u.data_status });
            modelBuilder.Entity<HSS_LayersMapping>().HasKey(u => new { u.hss_id, u.layer_id, u.layer_sys });
            modelBuilder.Entity<Dict_HPSchemes>().HasKey(u => new { u.scheme_id });
            modelBuilder.Entity<Contracts>().HasKey(u => new { u.contract_id });
            modelBuilder.Entity<Contracts_History>().HasKey(u => new { u.contract_id, u.data_status });
            modelBuilder.Entity<Buildings>().HasKey(u => new { u.building_id });
            modelBuilder.Entity<Buildings_History>().HasKey(u => new { u.building_id, u.data_status });
            modelBuilder.Entity<CoefCorrection>().HasKey(u => new { u.coef_id });
            modelBuilder.Entity<CoefCorrection_Perspective>().HasKey(u => new { u.coef_id, u.data_status, u.perspective_year });
            modelBuilder.Entity<CoefInflationary>().HasKey(u => new { u.coef_id });
            modelBuilder.Entity<CoefInflationary_Perspective>().HasKey(u => new { u.coef_id, u.data_status, u.perspective_year });
            modelBuilder.Entity<TemperatureGraphics>().HasKey(u => new { u.temp_graph_id });
            modelBuilder.Entity<Dict_NetLayingTypes>().HasKey(u => new { u.Id });
            modelBuilder.Entity<Dict_NormLoss_History>().HasKey(u => new { u.Id, u.data_status });
            modelBuilder.Entity<CoefBuildConsumer_History>().HasKey(u => new { u.coef_id, u.data_status, u.purpose_build_type_id, u.floors_id });
            modelBuilder.Entity<CoefHeatLoss_History>().HasKey(u => new { u.coef_id, u.data_status });
            modelBuilder.Entity<CoefHeatConsumption_Perspective>().HasKey(u => new { u.coef_id, u.data_status, u.type_purpose_id, u.perspective_year });
            modelBuilder.Entity<S_Perspective>().HasKey(u => new { u.source_id, u.data_status, u.perspective_year });
            modelBuilder.Entity<Dict_SourceStatuses>().HasKey(u => new { u.Id });
			modelBuilder.Entity<S_DocsPhotos_History>().HasKey(u => new { u.data_status, u.source_id, u.doc_num });


            modelBuilder.HasDbFunction(() => fnt_GetOrgList(default, default)).HasSchema("org");
			modelBuilder.HasDbFunction(() => fnt_GetETOStatusList(default)).HasSchema("tso");
			modelBuilder.HasDbFunction(() => fnt_GetTZTSOList(default, default, "")).HasSchema("tarif_zone");
			modelBuilder.HasDbFunction(() => fnt_GetTZTerritoryList(default)).HasSchema("tarif_zone");
			modelBuilder.HasDbFunction(() => fnt_GetSourcesByTZList(default, default)).HasSchema("tarif_zone");
			modelBuilder.HasDbFunction(() => fnt_GetSourcesByHPList(default)).HasSchema("heat_points");
			modelBuilder.HasDbFunction(() => fnt_GetUnomPPListByChars(default, default)).HasSchema("consumers");
			modelBuilder.HasDbFunction(() => fnt_GetUnomTpAddressListByChars(default, default)).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetSupplyContractsByChars(default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetUnomConsumerAddressListByChars(default, default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetUnomConsumerAddressListByCharsForObjDevProg(default, default, default, default, default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetUnomPerspectiveDevelopmentList()).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetFacilityNumbersPerspectiveDevelopmentList(default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetDistrictUnomAddress(default, default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetBildingUnomConsumerIdOne(default, default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetOrgOwnerInnListByChars(default, default)).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetDevProgramsList(default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetUnomOutputsList(default)).HasSchema("sources");
            modelBuilder.HasDbFunction(() => fnt_GetHP_EquipmentByMark(default)).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetHP_PumpByMark(default)).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetHpDiamHtSupplyIdList(default, default)).HasSchema("networks");
            modelBuilder.HasDbFunction(() => fnt_GetSourceUnomByUnomHpList(default, default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetDevProgramSelected(default, default)).HasSchema("tso");
            modelBuilder.HasDbFunction(() => fnt_GetDevProgramSelectedStandardized(default, default)).HasSchema("tso");
            modelBuilder.HasDbFunction(() => fnt_GetDevProgramSelectedPowerReserve(default, default)).HasSchema("tso");
            modelBuilder.HasDbFunction(() => fnt_GetApplName(default, default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetDecisionStatusList()).HasSchema("tso");
            modelBuilder.HasDbFunction(() => GetReportsList(default)).HasSchema("dictionary");
            modelBuilder.HasDbFunction(() => fnt_GetDistrictList()).HasSchema("dictionary");
            modelBuilder.HasDbFunction(() => fnt_GetRegionsList()).HasSchema("dictionary");
            modelBuilder.HasDbFunction(() => fnt_GetHpLocations()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetHpTypeLocations()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetEquipmentMarkList()).HasSchema("dictionary");
            modelBuilder.HasDbFunction(() => fnt_GetAutomaticList()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetPumpMarkList()).HasSchema("dictionary");
            modelBuilder.HasDbFunction(() => fnt_GetHpStatusesForFilter()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetHpDistricts()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetTZUnomList(default)).HasSchema("tarif_zone");
            modelBuilder.HasDbFunction(() => fnt_GetTZUnomList(default)).HasSchema("tarif_zone");
            modelBuilder.HasDbFunction(() => fnt_GetDistrictTZ(default)).HasSchema("tarif_zone");
            modelBuilder.HasDbFunction(() => fnt_GetTSOName(default)).HasSchema("tso");
            modelBuilder.HasDbFunction(() => fnt_GetHpStatusList()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetUnomSourceOutputList(default)).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetHpTempHtPlanList()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetHpTempHtTypeSchemeList()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetHpConnectTypesTechList()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetHpConnectTypesHeatList()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetHpConnectTypesVentList()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetHpConnectTypesHWList()).HasSchema("heat_points");
            modelBuilder.HasDbFunction(() => fnt_GetContractUnomList(default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetBildingUnomList(default)).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetUnomNormLossList(default, default)).HasSchema("networks");
            modelBuilder.HasDbFunction(() => fnt_PurporseTypeBuildList()).HasSchema("consumers");
            modelBuilder.HasDbFunction(() => fnt_GetDistrictRegionList()).HasSchema("dictionary");
            modelBuilder.HasDbFunction(() => fnt_GetSourcesTypeList()).HasSchema("sources");
            modelBuilder.HasDbFunction(() => fnt_GetSourcesUnomList(default)).HasSchema("sources");


        }
    }

}