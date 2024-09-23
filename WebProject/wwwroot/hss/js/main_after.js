/* COMMON EVENT LISTENERS FOR ALL PAGES */

// preloader on load page
window.onload = () => {

    let hide_clocks = $('#hide_clocks').val();
    if (hide_clocks == 1) {
        hidePreloader();
    }
    
};

if (document.querySelector('#perspective_year') !== null) {   
    document.querySelector('#perspective_year').addEventListener('change', (event) => {
        if (!$("#perspective_year[multiple]").length) {
            GetListOnPage();
        }       
    });
}

/* Click on Add button */
$('.bttn-edit-data').on('click', (e) => {
    let activeTabId = $('[data-bs-toggle=tab].nav-link.active').attr('data-tab-tableid');
    if (activeTabId != undefined) {
        switch (activeTabId) {
            case 'tzProductionTable': OpenPopupHeatBalance(0, $('#perspective_year').val(), '/TSO/HeatBalance/OpenTZProduction', 'TZProductionDataPopup');
                break;
            case 'tzTransferTable': OpenPopupHeatBalance(0, $('#perspective_year').val(), '/TSO/HeatBalance/OpenTZTransfer', 'TZTransferDataPopup');
                break;
            case 'tzCalcCostsTable': OpenPopupCostsPrices(0, $('#perspective_year').val(), '/TSO/CostsAndPrices/OpenCostsPrices', 'TZCostsPricesDataPopup');
                break;
            case 'tzTransportCostsTable': OpenPopupCostsPrices(0, $('#perspective_year').val(), '/TSO/CostsAndPrices/OpenCostsPrices', 'TZCostsPricesDataPopup');
                break;
            case 'tzProductionCostsTable': OpenPopupCostsPrices(0, $('#perspective_year').val(), '/TSO/CostsAndPrices/OpenCostsPrices', 'TZCostsPricesDataPopup');
                break;
            case 'tzCalcCostsIpHsCaTable': OpenPopupCostsPrices(0, $('#perspective_year').val(), '/TSO/CostsAndPrices/OpenExpenses_IPHS_CA', 'TZCostsPricesDataPopup');
                break;
            case 'tzNVVTable': OpenPopupCostsPrices(0, $('#perspective_year').val(), '/TSO/CostsAndPrices/OpenNVV', 'TZCostsPricesDataPopup');
                break;
            case 'tzTariffTable': OpenPopupTariffConnection(0, $('#perspective_year').val(), '/TSO/TariffConnection/OpenTariffConnection', 'TZTariffConnectionDataPopup');
                break;
            case 'tzTariffSteamTable': OpenPopupTariffConnection(0, $('#perspective_year').val(), '/TSO/TariffConnection/OpenTariffConnection', 'TZTariffConnectionDataPopup');
                break;
            case 'tzIndividualFeeTable': OpenPopupIndividualFee(0, '', '/TSO/TariffConnection/OpenTPIndividualFee', 'TZIndividualFeeDataPopup');
                break;
            case 'tzHPStandardizedRatesTable': OpenPopupStandardizedRates(0, '', '/TSO/TariffConnection/OpenStandardizedRates', 'TZStandardizedRatesDataPopup');
                break;
            case 'tzPowerReservePaymentTable': OpenPopupPowerReservePayment(0, '', '/TSO/TariffConnection/OpenPowerReservePayment', 'TZPowerReservePaymentDataPopup');
                break;
            
        }
    }
    else {
        activeTabId = $('[data-bs-toggle=tab].dropdown-item.active').attr('data-tab-tableid');
        if (activeTabId != undefined) {
            switch (activeTabId) {
                case 'HplistTable': OpenPopupHeatPointAddRemove(0);
                    break;
                case 'HpConsumerslistTable': OpenPopupConsumersAddRemove(0);
                    break;
                case 'SteamTurbineTable': OpenPopup(0, 'OpenPopupSteamTurbine', 'SteamTurbineDataPopup');
                    break;
                case 'GasTurbineTable': OpenPopup(0, 'OpenPopupGasTurbine', 'GasTurbineDataPopup');
                    break;
                case 'SteamBoilersTable': OpenPopup(0, 'OpenPopupSteamBoiler', 'SteamBoilerDataPopup');
                    break;
                case 'HWBoilersTable': OpenPopup(0, 'OpenPopupHWBoiler', 'HWBoilerDataPopup');
                    break;
                case 'PistonInstallationsTable': OpenPopup(0, 'OpenPopupPistonInstallations', 'PistonInstallationsDataPopup');
                    break;
                case 'ROUTable': OpenPopup(0, 'OpenPopupROU', 'ROUDataPopup');
                    break;
                case 'WaterHeaterTable': OpenPopup(0, 'OpenPopupWaterHeater', 'WaterHeaterDataPopup');
                    break;
                case 'PumpsTable': OpenPopup(0, 'OpenPopupPumps', 'PumpsDataPopup');
                    break;
                case 'FiltersVPUTable': OpenPopup(0, 'OpenPopupFiltersVPU', 'FiltersVPUDataPopup');
                    break;
                case 'ClarifierVPUTable': OpenPopup(0, 'OpenPopupClarifierVPU', 'ClarifierVPUDataPopup');
                    break;
                case 'ReverseOsmosisTable': OpenPopup(0, 'OpenPopupReverseOsmosisInstalVPU', 'ReverseOsmosisInstalVPUDataPopup');
                    break;
                case 'NanoFiltrTable': OpenPopup(0, 'OpenPopupNanoFiltrationInstallVPU', 'NanoFiltrationInstallVPUDataPopup');
                    break;
                case 'DeaeratorsTable': OpenPopup(0, 'OpenPopupDeaerators', 'DeaeratorsDataPopup');
                    break;
                case 'TanksVPUTable': OpenPopup(0, 'OpenPopupTanksVPU', 'TanksVPUDataPopup');
                    break;
                case 'ComplexonsVPUTable': OpenPopup(0, 'OpenPopupComplexonsVPU', 'ComplexonsVPUDataPopup');
                    break;
                case 'SmokePipesTable': OpenPopup(0, 'OpenPopupSmokePipes', 'SmokePipesDataPopup');
                    break;               
            }
        }
        else {
            activeTabId = e.target.id
            switch (activeTabId) {
                case 'progdev': OpenPopupPerspectiveDevelopment(0);
                    break;
            }
        }
    }

});