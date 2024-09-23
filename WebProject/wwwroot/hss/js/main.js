/*  PRELOADER */



// hide preloader
const hidePreloader = () => {
    const pl = document.querySelector('.preloader');
    pl.classList.add('preloader__hiding');
    setTimeout(function () {
        pl.classList.add('preloader__hide');
        pl.classList.remove('preloader__hiding');
    }, 500);
};


// show preloader
const showPreloader = () => {
    const pl = document.querySelector('.preloader');
    pl.classList.remove('preloader__hide');
    pl.classList.add('preloader__showing');
    setTimeout(function () {
        pl.classList.remove('preloader__showing');
    }, 100);
};

/* POPUP (MODAL) WINDOW */

// show modal
// parameter: modalName (popup element id)
const showModal = (modalName) => {
    let modalDiv = document.querySelector('#' + modalName);
    let modal = bootstrap.Modal.getOrCreateInstance(modalDiv);
    modal.show();
};

//hide modal
// parameter: modalName (popup element id)
const hideModal = (modalName) => {
    let modalDiv = document.querySelector('#' + modalName);
    let modal = bootstrap.Modal.getOrCreateInstance(modalDiv);
    modal.hide();
};

/* TABS */
/*
const triggerTabList = document.querySelectorAll('button[data-bs-toggle="tab"]');
triggerTabList.forEach(triggerEl => {
  const tabTrigger = new bootstrap.Tab(triggerEl);

  triggerEl.addEventListener('click', event => {
    event.preventDefault();
    showTab(tabTrigger);

  })
});


const showTab = (tabObj) => {
    console.log(tabObj)
    tabObj.show();
}*/


/* DATATABLES  */

// default options for dataTable tables

$.extend($.fn.dataTable.defaults, {
    //language: {
    //    url: 'https://cdn.datatables.net/plug-ins/1.13.1/i18n/ru.json'
    //},
    scrollX: true,
    scrollCollapse: true,
    processing: true,
    info: false,
    fixedColumns: true,
    //deferRender: true,
    language: {
        //url: 'https://cdn.datatables.net/plug-ins/1.13.1/i18n/ru.json',
        "lengthMenu": "Показать _MENU_ записей",
        "paginate": {
            "first": "В начало",
            "last": "В конец",
            "next": "Следующая",
            "previous": "Предыдущая"
        },
        searchBuilder: {
            "conditions": {
                "string": {
                    "startsWith": "начинается с",
                    "contains": "содержит",
                    "empty": "пусто",
                    "endsWith": "заканчивается на",
                    "equals": "равно",
                    "not": "не",
                    "notEmpty": "не пусто",
                    "notContains": "не содержит",
                    "notStartsWith": "не начинается на",
                    "notEndsWith": "не заканчивается на"
                },
                "date": {
                    "after": "после",
                    "before": "до",
                    "between": "между",
                    "empty": "пусто",
                    "equals": "равно",
                    "not": "не",
                    "notBetween": "не между",
                    "notEmpty": "не пусто"
                },
                "number": {
                    "empty": "пусто",
                    "equals": "равно",
                    "gt": "больше чем",
                    "gte": "больше или равно чем",
                    "lt": "меньше чем",
                    "lte": "меньше или равно чем",
                    "not": "не",
                    "notEmpty": "не пусто",
                    "between": "между",
                    "notBetween": "не между"
                },
                "array": {
                    "equals": "равно",
                    "empty": "пусто",
                    "contains": "содержит",
                    "not": "не равно",
                    "notEmpty": "не пусто",
                    "without": "не содержит"
                }
            },
            title: {
                0: 'Конструктор поиска',
                _: 'Конструктор поиска (%d)'
            },
            button: {
                0: '<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 512 512"><path d="M35.4 87.12l168.65 196.44A16.07 16.07 0 01208 294v119.32a7.93 7.93 0 005.39 7.59l80.15 26.67A7.94 7.94 0 00304 440V294a16.07 16.07 0 014-10.44L476.6 87.12A14 14 0 00466 64H46.05A14 14 0 0035.4 87.12z" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32"/></svg>',
                _: '<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 512 512"><path d="M35.4 87.12l168.65 196.44A16.07 16.07 0 01208 294v119.32a7.93 7.93 0 005.39 7.59l80.15 26.67A7.94 7.94 0 00304 440V294a16.07 16.07 0 014-10.44L476.6 87.12A14 14 0 00466 64H46.05A14 14 0 0035.4 87.12z" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32"/></svg><span class="filter-num">%d</span>'
            },
            add: '<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 512 512"><title>Добавить условие</title><path fill="#5f2fe7" d="M256 48C141.31 48 48 141.31 48 256s93.31 208 208 208 208-93.31 208-208S370.69 48 256 48zm80 224h-64v64a16 16 0 01-32 0v-64h-64a16 16 0 010-32h64v-64a16 16 0 0132 0v64h64a16 16 0 010 32z"/></svg>',
            condition: 'Условие',
            clearAll: 'Отменить всё',
            delete: '<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 512 512"><title>Удалить условие</title><path fill="#e72f73" d="M256 48C141.31 48 48 141.31 48 256s93.31 208 208 208 208-93.31 208-208S370.69 48 256 48zm75.31 260.69a16 16 0 11-22.62 22.62L256 278.63l-52.69 52.68a16 16 0 01-22.62-22.62L233.37 256l-52.68-52.69a16 16 0 0122.62-22.62L256 233.37l52.69-52.68a16 16 0 0122.62 22.62L278.63 256z"/></svg>',
            data: 'Столбец',
            logicAnd: 'и',
            logicOr: 'или',
            value: 'Значение',
            valueJoiner: 'и'
        }
        //processing: "<img src='loading.gif'>"
    },
    buttons: [
        {
            extend: 'colvis',
            text: '<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 512 512"><path d="M255.66 112c-77.94 0-157.89 45.11-220.83 135.33a16 16 0 00-.27 17.77C82.92 340.8 161.8 400 255.66 400c92.84 0 173.34-59.38 221.79-135.25a16.14 16.14 0 000-17.47C428.89 172.28 347.8 112 255.66 112z" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32"/><circle cx="256" cy="256" r="80" fill="none" stroke="currentColor" stroke-miterlimit="10" stroke-width="32"/></svg>',
            titleAttr: 'Видимость столбцов',
            className: 'bttn-square',
            columnText: function (dt, idx, title) {
                return dt.column(idx).header().innerText;
            }
        },
        {
            extend: 'searchBuilder',
            titleAttr: 'Конструктор поиска',
            className: 'bttn-filter',
            config: {
                depthLimit: 1
            }
        },
        {
            extend: 'excelHtml5',
            className: 'exportExcelFull',
            exportOptions: {
                columns: ':visible',
                format: {
                    body: function (data, row, column, node) {
                        return data.replace(',', '.');
                    }
                }
            }
        }
    ]
});

// init table as DataTable by table id and named options set
// Available values of oType: 'simple-scroll', 'full', ...
const initDataTable = (tId, oType, height = '55vh', colsDefs) => {
    let table;
    switch (oType) {
        case 'simple-scroll': // simple table with scrolling
            table = $('#' + tId).DataTable({
                scrollY: height,
                ordering: false,
                searching: false,
                paging: false,
                footerCallback: function (row, data, start, end, display) {
                    var api = this.api();
                    all_cnt_rows = api.column(0, { page: 'current' }).data().count();

                    if (tId == 'hpSwitchTable' || tId == 'ConsumersTable') {
                        $('#all_cnt_rows_popup').html(all_cnt_rows);
                    }

                    var intVal = function (i) {
                        return typeof i === 'string' ? i.replace(/[\$,]/g, '') * 1 : typeof i === 'number' ? i : 0;
                    };

                    var sumVal = function (i, x = 2) {

                        let footer_value_sum = api.column(i, { page: 'current' }).data().reduce(function (a, b) {
                            return (intVal(a.toString().replace(",", ".")) + intVal(b.toString().replace(",", ".")));
                        }, 0);

                        return footer_value_sum.toFixed(x)
                    };

                    if (tId === 'hpSwitchTable') {
                        q_sum_before = sumVal(2);
                        $('#q_sum_before').html(q_sum_before);
                    }
                }
                //dom: 'r'
            });
            break;
        case 'full':
            table = $('#' + tId).DataTable({
                scrollY: height,
                stateSave: true,
                select: true,
                orderCellsTop: true,
                paging: true,
                //columnDefs: [

                //    { targets: '_all', visible: true }
                //],
                //ordering: false,
                //searching: false,
                dom: 'B<r<t>lp>', // 'Blrtp',
                lengthMenu: [
                    [10, 100],
                    [10, 100]
                ],
                initComplete: function () {
                    var $buttons = $('.dt-buttons');
                    $('.exportExcelFull').hide();
                    $('#excelFull').on('click',
                        function () {
                            $buttons.find('.exportExcelFull').click();
                        });

                    this.api()
                        .columns()
                        .every(function () {
                            var that = this;
                            $('#' + tId + '_wrapper .filter-col-' + this.index()).on('keyup change clear', function () {
                            //$('.filter-col-' + this.index()).on('keyup change clear', function () {
                                if (that.search() !== this.value) {
                                    that.search(this.value).draw();
                                }
                            });
                        });
                    if (tId == 'tsoTable' || tId == 'tsoUploadTable') {
                        $("#tso_div_table").show();
                    }
                    else if (tId == 'tsoServTableSummary' || tId == 'tsoServTableSources' || tId == 'tsoServTableHP' || tId == 'tsoServTableAdditional') {
                        $("#tsoServTab").show();
                    }
                    else if (tId == 'tzSourcesTable' || tId == 'tzNetworksTable' || tId == 'tzPSTable' || tId == 'tzHPTable') {
                        $("#zoneObjListTab").show();
                    }
                    else if (tId == 'tzProductionTable' || tId == 'tzTransferTable') {
                        $("#tzHeatBalanceTab").show();
                    }
                    else if (tId == 'tzCalcCostsTable' || tId == 'tzTransportCostsTable' || tId == 'tzProductionCostsTable' || tId == 'tzNVVTable' || tId == 'tzCalcCostsIpHsCaTable') {
                        $("#tzCalcCostsTab").show();
                    }
                    else if (tId == 'tzTariffTable' || tId == 'tzTariffSteamTable' || tId == 'tzIndividualFeeTable' || tId == 'tzHPStandardizedRatesTable' || tId == 'tzPowerReservePaymentTable') {
                        $("#tzTariffTableTab").show();
                    }
                    else if (tId == 'ecoTable') {
                        $("#eco_div_table").show();
                    }
                    else if (tId == 'TarifZoneTable') {
                        $("#TarifZoneTable_div").show();
                    }
                    else if (tId == 'terrDivisionTable') {
                        $("#terrDivision_div_table").show();
                    }
                    else if (tId == 'OutPutsTable') {
                        $("#OutPutsTable_div").show();
                    }
                    else if (tId == 'ETOZonesTable') {
                        $("#ZoneETOTable_div").show();
                    }
                    else if (tId == 'HSSTable') {
                        $("#HSSTable_div").show();
                    }
                    else if (tId == 'HpConsumerslistTable') {
                        $("#hpConsumersDiv").show();
                    }
                    
                    hidePreloader();
                },
                footerCallback: function (row, data, start, end, display) {
                    var api = this.api();
                    all_cnt_rows = api.column(0, { search: 'applied' }).data().count();

                    //current_cnt_rows = api.column(0, { search: 'applied' }).data().count();

                    if (tId == 'tzSourcesTable' || tId == 'tzNetworksTable' || tId == 'tzPSTable' || tId == 'tzHPTable' || tId == 'ConsumerTable' || tId == 'ConsumersBuildTable') {
                        $('#all_cnt_rows_popup').html(all_cnt_rows);
                    }
                    else {
                        $('#all_cnt_rows').html(all_cnt_rows);
                        //$('#current_cnt_rows').html(current_cnt_rows);
                    }

                    var intVal = function (i) {
                        return typeof i === 'string' ? i.replace(/[\$,]/g, '') * 1 : typeof i === 'number' ? i : 0;
                    };

                    var sumVal = function (i, x = 2) {
                        let footer_value_sum = api.column(i, { search: 'applied' }).data().reduce(function (a, b) {
                            return (intVal(a.toString().replace(",", ".")) + intVal(b.toString().replace(",", ".")));
                        }, 0);

                        return footer_value_sum.toFixed(x)
                    };

                    var cntVal = function (i) {

                        let footer_value_cnt = api.column(i, { search: 'applied' }).data().reduce(function (a, b) {
                                return intVal(a) + intVal(b);
                            }, 0);

                        return footer_value_cnt
                    };

                    var cntFilterVal = function (i, filter_val) {

                        let footer_value_cnt_filter = api.column(i, { search: 'applied' }).data().filter(function (value, index) {
                                return value === filter_val ? true : false;
                            }).length;

                        return footer_value_cnt_filter
                    };

                    var cntFilterValues = function (i, filter_values) {

                        let footer_value_cnt_filter = api.column(i, { search: 'applied' }).data().filter(function (value, index) {
                            return filter_values.indexOf(value) !== -1 ? true : false;
                        }).length;

                        return footer_value_cnt_filter
                    };

                    var sumByFilterVal = function (i, filter_val, y, x = 2) {
                        let footer_value_sum_filter = api.column(y, { search: 'applied' }).data()
                            .reduce(function (a, b, cur_index) {
                                if (api.column(i, { search: 'applied' }).data()[cur_index] === filter_val) {
                                    return (intVal(a.toString().replace(",", ".")) + intVal(b.toString().replace(",", ".")));
                                }
                                else { return (intVal(a.toString().replace(",", "."))); }
                            }, 0)

                        return footer_value_sum_filter.toFixed(x);
                    };

                    var sumByFilterValues = function (i, filter_values, y, x = 2) {
                        let footer_value_sum_filter = api.column(y, { search: 'applied' }).data()
                            .reduce(function (a, b, cur_index) {
                                if (filter_values.indexOf(api.column(i, { search: 'applied' }).data()[cur_index]) !== -1) {
                                    return (intVal(a.toString().replace(",", ".")) + intVal(b.toString().replace(",", ".")));
                                }
                                else { return (intVal(a.toString().replace(",", "."))); }
                            }, 0)

                        return footer_value_sum_filter.toFixed(x);
                    };

                    var sumForDyNetworks = function (lc, ds, dr, x = 2) {
                        let footer_value_dy = api.column(lc, { search: 'applied' }).data()
                            .reduce(function (a, b, cur_index) {

                                ds_val = api.column(ds, { search: 'applied' }).data()[cur_index];
                                dr_val = api.column(dr, { search: 'applied' }).data()[cur_index];

                                ret_sum = intVal(a.toString().replace(",", "."));
                                next_val = intVal(b.toString().replace(",", "."));

                                return (ret_sum + (next_val * (intVal(ds_val) + intVal(dr_val))));
                            }, 0)

                        return footer_value_dy.toFixed(x);
                    };

                    //сумма с вычитанием или сложением двух столбцов с плавающей точкой
                    var sumForMathAction = function (main_col, second_col, math_action, x = 2) {
                        let footer_value_s = api.column(main_col, { search: 'applied' }).data()
                            .reduce(function (a, b, cur_index) {

                                second_col_val = api.column(second_col, { search: 'applied' }).data()[cur_index];
                                second_col_val = intVal(second_col_val.toString().replace(",", "."));

                                ret_sum = intVal(a.toString().replace(",", "."));
                                next_val = intVal(b.toString().replace(",", "."));

                                if (math_action === 'subtraction')
                                    return (ret_sum + (next_val - second_col_val));
                                else if (math_action === 'deduction')
                                    return (ret_sum + (next_val + second_col_val));
                                
                            }, 0)

                        return footer_value_s.toFixed(x);
                    };

                    //количество строк, в которых значения в одном из двух столбцов равны заданному строковому значению
                    var cntFilterTwoColsOneStr = function (main_col, second_col, match_name) {
                        count = 0;
                        let footer_value_s = api.column(main_col, { search: 'applied' }).data()
                            .reduce(function (a, b, cur_index) {
                                first_col_val = api.column(main_col, { search: 'applied' }).data()[cur_index];
                                second_col_val = api.column(second_col, { search: 'applied' }).data()[cur_index];              
                                
                                if (match_name === 'зависимая')
                                    if (first_col_val === match_name || second_col_val === match_name)
                                        count++;

                                if (match_name === 'смешанная')
                                    if ((first_col_val === match_name && second_col_val != 'зависимая') || (second_col_val === match_name && first_col_val != 'зависимая'))
                                        count++;

                                if (match_name === 'независимая')
                                    if (first_col_val === match_name && second_col_val === match_name)
                                        count++;

                            }, 0)

                        return count;
                    };


                    if (tId === 'terrDivisionTable') {
                        var count = $('#cnt_years_input').val();
                        for (var i = 0; i < count; i++) {
                            var id = "#population_cnt_" + i;
                            population_cnt = cntVal(17 + i);
                            $(id).html(population_cnt);
                        }
                          
                        

                    }

                    if (tId === 'tsoTable') {
                        reg_cnt_rows = cntFilterVal(6, 'регулируемая');
                        not_reg_cnt_rows = cntFilterVal(6, 'нерегулируемая');
                        contract_delivery_cnt_rows = cntFilterVal(6, 'договор поставки');
                        comb_prod_more_25_cnt_rows = cntFilterVal(11, 'Да');
                        comb_prod_less_25_cnt_rows = cntFilterVal(12, 'Да');
                        not_comb_prod_cnt_rows = cntFilterVal(13, 'Да');
                        transfer_cnt_rows = cntFilterVal(14, 'Да');
                        sales_cnt_rows = cntFilterVal(15, 'Да');

                        // Update footer
                        $('#reg_cnt_rows').html(reg_cnt_rows);
                        $('#not_reg_cnt_rows').html(not_reg_cnt_rows);
                        $('#contract_delivery_cnt_rows').html(contract_delivery_cnt_rows);
                        $('#comb_prod_more_25_cnt_rows').html(comb_prod_more_25_cnt_rows);
                        $('#comb_prod_less_25_cnt_rows').html(comb_prod_less_25_cnt_rows);
                        $('#not_comb_prod_cnt_rows').html(not_comb_prod_cnt_rows);
                        $('#transfer_cnt_rows').html(transfer_cnt_rows);
                        $('#sales_cnt_rows').html(sales_cnt_rows);
                    }

                    if (tId === 'tsoServTableAdditional') {

                        fixed_assets_cost_prod = sumVal(2);
                        fixed_assets_wear = sumVal(3);
                        fixed_assets_cost_transfer = sumVal(4);
                        count_service_public = sumVal(8,0);
                        count_abonent = sumVal(9,0);

                        $('#fixed_assets_cost_prod').html(fixed_assets_cost_prod);
                        $('#fixed_assets_wear').html(fixed_assets_wear);
                        $('#fixed_assets_cost_transfer').html(fixed_assets_cost_transfer);
                        $('#count_service_public').html(count_service_public);
                        $('#count_abonent').html(count_abonent);

                    }

                    if (tId === 'tsoServTableSummary') {
                        cnt_sources = cntVal(5);
                        inst_elec_power_s = sumVal(6);
                        inst_heat_power_s = sumVal(7);
                        sum_length_net_channel_all = sumVal(10);
                        sum_volume_all_net = sumVal(19);
                        cnt_pump_stations = cntVal(20);
                        inst_heat_power_all_hp = sumVal(21);
                        cnt_hp_all = cntVal(24);
                        sum_pump_capacity_ps = sumVal(27);

                        $('#cnt_sources').html(cnt_sources);
                        $('#cnt_hp_all').html(cnt_hp_all);
                        $('#sum_length_net_channel_all').html(sum_length_net_channel_all);
                        $('#cnt_pump_stations').html(cnt_pump_stations);
                        $('#inst_heat_power_s').html(inst_heat_power_s);
                        $('#inst_elec_power_s').html(inst_elec_power_s);
                        $('#inst_heat_power_all_hp').html(inst_heat_power_all_hp);
                        $('#sum_volume_all_net').html(sum_volume_all_net);
                        $('#sum_pump_capacity_ps').html(sum_pump_capacity_ps);
                    }

                    if (tId === 'tsoServTableSources') {
                        all_inst_elec_power = sumVal(13);
                        all_inst_heat_power = sumVal(14);
                        cnt_comb_sources = cntFilterValues(9, ["ТЭЦ", "ПГУ ТЭС", "ГТЭС", "Мини ТЭС", "Энергокомплекс", "Энергоцентр"]);
                        cnt_boil_sources = cntFilterValues(9, ["РТС", "КТС", "МК", "ПК", "АИТ", "КОТ", "ВЕДКОТ"]);
                        comb_inst_elec_power = sumByFilterValues(9, ["ТЭЦ", "ПГУ ТЭС", "ГТЭС", "Мини ТЭС", "Энергокомплекс", "Энергоцентр"], 13);
                        comb_inst_heat_power = sumByFilterValues(9, ["ТЭЦ", "ПГУ ТЭС", "ГТЭС", "Мини ТЭС", "Энергокомплекс", "Энергоцентр"], 14);
                        boil_inst_heat_power = sumByFilterValues(9, ["РТС", "КТС", "МК", "ПК", "АИТ", "КОТ", "ВЕДКОТ"], 14);

                        $('#all_inst_heat_power').html(all_inst_heat_power);
                        $('#all_inst_elec_power').html(all_inst_elec_power);
                        $('#cnt_comb_sources').html(cnt_comb_sources);
                        $('#comb_inst_heat_power').html(comb_inst_heat_power);
                        $('#comb_inst_elec_power').html(comb_inst_elec_power);
                        $('#cnt_boil_sources').html(cnt_boil_sources);
                        $('#boil_inst_heat_power').html(boil_inst_heat_power);
                    }

                    if (tId === 'tsoServTableHP') {
                        all_inst_heat_power = sumVal(13);
                        cnt_chp = cntFilterVal(10, 'индивидуальный (ИТП)');
                        cnt_ihp = cntFilterVal(10, 'центральный (ЦТП)');
                        chp_inst_heat_power = sumByFilterVal(10, 'центральный (ЦТП)', 14);
                        ihp_inst_heat_power = sumByFilterVal(10, 'индивидуальный (ИТП)', 14);

                        $('#all_inst_heat_power').html(all_inst_heat_power);
                        $('#cnt_chp').html(cnt_chp);
                        $('#chp_inst_heat_power').html(chp_inst_heat_power);
                        $('#cnt_ihp').html(cnt_ihp);
                        $('#ihp_inst_heat_power').html(ihp_inst_heat_power);
                    }
                    else if (tId === 'tzSourcesTable' || tId == 'tzHPTable') {
                        inst_heat_power = sumVal(9);
                        $('#inst_heat_power').html(inst_heat_power);

                        $('#pump_capacity_ps').html("0");
                        $('#volume_net').html("0");
                        $('#aver_diam_net').html("0");
                        $('#mat_char_net').html("0");
                    }
                    else if (tId === 'tzPSTable') {
                        pump_capacity_ps = sumVal(8);
                        $('#pump_capacity_ps').html(pump_capacity_ps);

                        $('#inst_heat_power').html("0");
                        $('#volume_net').html("0");
                        $('#aver_diam_net').html("0");
                        $('#mat_char_net').html("0");
                    }
                    else if (tId === 'tzNetworksTable') {
                        mat_char_net = sumVal(17);
                        volume_net = sumVal(18);
                        sum_length_net = sumVal(13);
                        if (sum_length_net > 0)
                            aver_diam_net = sumForDyNetworks(13, 11, 12) / sum_length_net * 2;
                        else
                            aver_diam_net = 0;

                        $('#pump_capacity_ps').html("0");
                        $('#inst_heat_power').html("0");
                        $('#volume_net').html(volume_net);
                        $('#aver_diam_net').html(aver_diam_net);
                        $('#mat_char_net').html(mat_char_net);
                    }
                    else if (tId === 'tzProductionTable') {

                        release_collect_brutto_plan = sumForMathAction(6, 8, "subtraction");
                        release_collect_brutto_fact = sumForMathAction(5, 7, "subtraction");
                        release_heat_energy_network_plan = sumVal(46);
                        release_heat_energy_network_fact = sumVal(45);
                        heatenrg_loss_heatnet_plan = sumVal(48);
                        heatenrg_loss_heatnet_fact = sumVal(47);
                        useful_release_plan = sumVal(58);
                        useful_release_fact = sumVal(49);

                        $('#release_collect_brutto_plan').html(release_collect_brutto_plan);
                        $('#release_collect_brutto_fact').html(release_collect_brutto_fact);
                        $('#release_heat_energy_network_plan').html(release_heat_energy_network_plan);
                        $('#release_heat_energy_network_fact').html(release_heat_energy_network_fact);
                        $('#heatenrg_loss_heatnet_plan').html(heatenrg_loss_heatnet_plan);
                        $('#heatenrg_loss_heatnet_fact').html(heatenrg_loss_heatnet_fact);
                        $('#useful_release_plan').html(useful_release_plan);
                        $('#useful_release_fact').html(useful_release_fact);
                    }
                    else if (tId === 'tzTransferTable') {

                        accept_heatenrg_transport_plan = sumVal(6);
                        accept_heatenrg_transport_fact = sumVal(5);
                        useful_release_company_needs_plan = sumVal(19);
                        useful_release_company_needs_fact = sumVal(10);
                        heatenrg_loss_heatnet_plan = sumVal(8);
                        heatenrg_loss_heatnet_fact = sumVal(7);
                        useful_release_others_plan = sumForMathAction(22, 23, "deduction");
                        useful_release_others_fact = sumForMathAction(13, 14, "deduction");

                        $('#accept_heatenrg_transport_plan').html(accept_heatenrg_transport_plan);
                        $('#accept_heatenrg_transport_fact').html(accept_heatenrg_transport_fact);
                        $('#useful_release_company_needs_plan').html(useful_release_company_needs_plan);
                        $('#useful_release_company_needs_fact').html(useful_release_company_needs_fact);
                        $('#heatenrg_loss_heatnet_plan').html(heatenrg_loss_heatnet_plan);
                        $('#heatenrg_loss_heatnet_fact').html(heatenrg_loss_heatnet_fact);
                        $('#useful_release_others_plan').html(useful_release_others_plan);
                        $('#useful_release_others_fact').html(useful_release_others_fact);
                    }
                    else if (tId === 'tzCalcCostsTable') {
                        sum_all_costs_fact = sumVal(15);
                        sum_all_costs_plan = sumVal(16);
                        energy_costs_fact = sumVal(17);
                        energy_costs_plan = sumVal(87);
                        operation_costs_fact = sumVal(65);
                        operation_costs_plan = sumVal(135);
                        uncontrol_costs_fact = sumVal(76);
                        uncontrol_costs_plan = sumVal(146);

                        $('#sum_all_costs_fact').html(sum_all_costs_fact);
                        $('#sum_all_costs_plan').html(sum_all_costs_plan);
                        $('#energy_costs_fact').html(energy_costs_fact);
                        $('#energy_costs_plan').html(energy_costs_plan);
                        $('#operation_costs_fact').html(operation_costs_fact);
                        $('#operation_costs_plan').html(operation_costs_plan);
                        $('#uncontrol_costs_fact').html(uncontrol_costs_fact);
                        $('#uncontrol_costs_plan').html(uncontrol_costs_plan);
                    }
                    else if (tId === 'tzTransportCostsTable') {
                        sum_all_costs_fact = sumVal(5);
                        sum_all_costs_plan = sumVal(6);
                        operation_costs_fact = sumVal(7);
                        operation_costs_plan = sumVal(28);
                        uncontrol_costs_fact = sumVal(18);
                        uncontrol_costs_plan = sumVal(39);

                        $('#sum_all_costs_fact').html(sum_all_costs_fact);
                        $('#sum_all_costs_plan').html(sum_all_costs_plan);
                        $('#operation_costs_fact').html(operation_costs_fact);
                        $('#operation_costs_plan').html(operation_costs_plan);
                        $('#uncontrol_costs_fact').html(uncontrol_costs_fact);
                        $('#uncontrol_costs_plan').html(uncontrol_costs_plan);
                    }
                    else if (tId === 'tzCalcCostsIpHsCaTable') {
                        sum_costs_all = sumForMathAction(5, 12, "deduction");
                        sum_costs_iphs = sumVal(5);
                        sum_costs_ca = sumVal(12);
                        sum_grants_over_profit = sumForMathAction(19, 20, "deduction");

                        $('#sum_costs_all').html(sum_costs_all);
                        $('#sum_costs_iphs').html(sum_costs_iphs);
                        $('#sum_costs_ca').html(sum_costs_ca);
                        $('#sum_grants_over_profit').html(sum_grants_over_profit);
                    }
                    if (tId === 'HplistTable') {
                        heat_exchange_sum = sumVal(41);
                        ownerless_hp_cnt = cntFilterVal(39, 'Да');
                        dilapidated_hp_cnt = cntFilterVal(40, 'Да');
                        agreement_heatload_sum = sumVal(46);
                        calculate_heatload_sum = sumVal(57);

                        // Update footer
                        $('#heat_exchange_sum').html(heat_exchange_sum);
                        $('#ownerless_hp_cnt').html(ownerless_hp_cnt);
                        $('#dilapidated_hp_cnt').html(dilapidated_hp_cnt);
                        $('#agreement_heatload_sum').html(agreement_heatload_sum);
                        $('#calculate_heatload_sum').html(calculate_heatload_sum);
                    }
                    if (tId === 'LoadExpensiveTable') {
                        var count = intVal($('#cnt_years_loadsExpensives').val()) + 1;
                        for (var i = 0; i < count; i++) {
                            var le_heat_load_id = "#le_heat_load_" + i;
                            var le_heat_carrier_id = "#le_heat_carrier_" + i;
                            le_heat_load = sumVal((21 + i * 27), 8);
                            le_heat_carrier = sumVal((32 + i * 27), 8);
                            $(le_heat_load_id).html(le_heat_load);
                            $(le_heat_carrier_id).html(le_heat_carrier);
                        }
                    }
                    if (tId === 'equipmentTab') {
                        sum_equip_heat_power = sumVal(17);
                        sum_equip_pump_capasity = sumVal(53);
                        equip_heat_exchange_count = sumVal(31);
                        //equip_heat_exchange_count_heat = sumVal(37) + sumVal(39);
                        //equip_heat_exchange_count_vent = sumVal(57);
                        //equip_heat_exchange_count_hw = sumVal(57);

                        equip_pump_count = sumVal(52);
                        //equip_pump_count_heat = sumVal(46);
                        //equip_pump_count_vent = sumVal(57);
                        //equip_pump_count_hw = sumVal(57);

                        // Update footer
                        $('#sum_equip_heat_power').html(sum_equip_heat_power);
                        $('#sum_equip_pump_capasity').html(sum_equip_pump_capasity);
                        $('#equip_heat_exchange_count').html(equip_heat_exchange_count);
                        //$('#equip_heat_exchange_count_heat').html(equip_heat_exchange_count_heat);
                        //$('#equip_heat_exchange_count_vent').html(equip_heat_exchange_count_vent);
                        //$('#equip_heat_exchange_count_hw').html(equip_heat_exchange_count_hw);
                        $('#equip_pump_count').html(equip_pump_count);
                        //$('#equip_pump_count_heat').html(equip_pump_count_heat);
                        //$('#equip_pump_count_vent').html(equip_pump_count_vent);
                        //$('#equip_pump_count_hw').html(equip_pump_count_hw);
                    }
                    if (tId === 'loadsSchemaTable') {                        
                        //sum_load_attach_sch_count_dep_hp = cntFilterVal(17, 'зависимая');
                        //sum_load_attach_sch_count_dep_vent = cntFilterVal(18, 'зависимая');
                        sum_load_attach_sch_count_dep = cntFilterTwoColsOneStr(17, 18, 'зависимая');                        
                        //sum_load_attach_sch_count_mixed_hp = cntFilterVal(17, 'смешанная');
                        //sum_load_attach_sch_count_mixed_vent = cntFilterVal(18, 'смешанная');
                        sum_load_attach_sch_count_mixed = cntFilterTwoColsOneStr(17, 18, 'смешанная');   
                        sum_load_attach_sch_count_hp_open = cntFilterVal(19, 'открытая');
                        sum_load_attach_sch_count_indep = cntFilterTwoColsOneStr(17, 18, 'независимая'); 

                        // Update footer
                        $('#sum_load_attach_sch_count_dep').html(sum_load_attach_sch_count_dep);
                        //$('#sum_load_attach_sch_count_dep_hp').html(sum_load_attach_sch_count_dep_hp);
                        //$('#sum_load_attach_sch_count_dep_vent').html(sum_load_attach_sch_count_dep_vent);
                        $('#sum_load_attach_sch_count_mixed').html(sum_load_attach_sch_count_mixed);
                        //$('#sum_load_attach_sch_count_mixed_hp').html(sum_load_attach_sch_count_mixed_hp);
                        //$('#sum_load_attach_sch_count_mixed_vent').html(sum_load_attach_sch_count_mixed_vent);
                        $('#sum_load_attach_sch_count_hp_open').html(sum_load_attach_sch_count_hp_open);
                        $('#sum_load_attach_sch_count_indep').html(sum_load_attach_sch_count_indep);
                    }
                    if (tId === 'automationTable') {
                        sum_automatization_count_auto = cntFilterVal(18, 'Да');
                        sum_automatization_count_auto_dep = cntFilterVal(20, 'Да');
                        sum_automatization_count_hw_exp_limit = cntFilterVal(17, 'Да');

                        // Update footer
                        $('#sum_automatization_count_auto').html(sum_automatization_count_auto);
                        $('#sum_automatization_count_auto_dep').html(sum_automatization_count_auto_dep);
                        $('#sum_automatization_count_hw_exp_limit').html(sum_automatization_count_hw_exp_limit);
                    }
                    if (tId === 'resourcesTable') {
                        hp_en_res_acc_count_UUTE_heat_input = sumVal(18, 0);
                        hp_en_res_acc_count_UUTE_heat_vent = sumVal(20, 0);
                        hp_en_res_acc_count_UUTE_gvs = sumVal(22, 0);
                        hp_en_res_acc_count_cold_water_acc = sumVal(24, 0);
                        hp_en_res_acc_count_energy_acc = sumVal(26, 0);

                        // Update footer
                        $('#hp_en_res_acc_count_UUTE_heat_input').html(hp_en_res_acc_count_UUTE_heat_input);
                        $('#hp_en_res_acc_count_UUTE_heat_vent').html(hp_en_res_acc_count_UUTE_heat_vent);
                        $('#hp_en_res_acc_count_UUTE_gvs').html(hp_en_res_acc_count_UUTE_gvs);
                        $('#hp_en_res_acc_count_energy_acc').html(hp_en_res_acc_count_energy_acc);
                        $('#hp_en_res_acc_count_cold_water_acc').html(hp_en_res_acc_count_cold_water_acc);
                    }

                    else if (tId === 'tzNVVTable') {

                    }

                    if (tId === 'organizationTable') {
                        cnt_type_org = cntFilterVal(3, 'Теплоснабжающая или теплосетевая');
                        $('#cnt_type_org').html(cnt_type_org);
                      
                    }
                    if (tId === 'ConsumersTable') {
                        ctr_hl_heat_hw = sumVal(2);
                        ctr_hl_vent_hw = sumVal(3);
                        ctr_hl_gvss_hw = sumVal(4);
                        ctr_hl_tech_hw = sumVal(5);
                        ctr_hl_other_hw = sumVal(6);
                        ctr_hl_cond_hw = sumVal(7);
                        ctr_hl_curtains_hw = sumVal(8);
                        ctr_hl_heat_steam = sumVal(9);
                        ctr_hl_vent_steam = sumVal(10);
                        ctr_hl_gvss_steam = sumVal(11);
                        ctr_hl_tech_steam = sumVal(12);
                        ctr_hl_other_steam = sumVal(13);
                        ctr_hl_cond_steam = sumVal(14);
                        ctr_hl_curtains_steam = sumVal(15);

                        $('#ctr_hl_heat_hw').html(ctr_hl_heat_hw);
                        $('#ctr_hl_vent_hw').html(ctr_hl_vent_hw);
                        $('#ctr_hl_gvss_hw').html(ctr_hl_gvss_hw);
                        $('#ctr_hl_tech_hw').html(ctr_hl_tech_hw);
                        $('#ctr_hl_other_hw').html(ctr_hl_other_hw);
                        $('#ctr_hl_cond_hw').html(ctr_hl_cond_hw);
                        $('#ctr_hl_curtains_hw').html(ctr_hl_curtains_hw);
                        $('#ctr_hl_heat_steam').html(ctr_hl_heat_steam);
                        $('#ctr_hl_vent_steam').html(ctr_hl_vent_steam);
                        $('#ctr_hl_gvss_steam').html(ctr_hl_gvss_steam);
                        $('#ctr_hl_tech_steam').html(ctr_hl_tech_steam);
                        $('#ctr_hl_other_steam').html(ctr_hl_other_steam);
                        $('#ctr_hl_cond_steam').html(ctr_hl_cond_steam);
                        $('#ctr_hl_curtains_steam').html(ctr_hl_curtains_steam);
                    }
                }
            });

            table.buttons().container().appendTo('.' + tId + '-buttons');
            break;
        case 'scroll-colvis': // simple table with scrolling and adjusting visible columns
            table = $('#' + tId).DataTable({
                scrollY: height,
                ordering: false,
                searching: false,
                stateSave: true,
                searchBuilder: null,
                dom: 'Brt'
            });
            //console.log(table.buttons().pop());
            //console.log(table.buttons().container().children()[1]);
            table.buttons().container().children()[1].remove();
            table.buttons().container().appendTo('.' + tId + '-buttons');
            break;
    }
};

const loadFilterFromState = (tId) => {
    let table = $('#' + tId).DataTable();
    let state = table.state.loaded();
    if (state) {
        table.columns().every(function (colIdx) {
            let colSearch = state.columns[colIdx].search;
            if (colSearch.search) {
                let filter = colSearch.search.replace(/[\^\$]/g, '');
                //$('.filter-col-' + colIdx).val(filter);
                $('#' + tId + '_wrapper .filter-col-' + colIdx).val(filter);
            }
        });

        table.draw();
    }
};

// adjust dataTable columns width by table id
//need call after table bacome visibile, example pass on tab or show modal window
const adjustColumns = (tId) => {
    let table = $('#' + tId).DataTable();
    table.columns.adjust().draw();
};

// execution all functions and addind eventListeners to page tables, defining in the 'pageTables' object
/*
    const pageTables = [
    {
      name: '<table id without "Table">', for example for table 'tsoTable' name equals 'tso',
      type: 'full' or 'simple-scroll'
      columns: [],
      modal: '<modal window id>',
      tab: true or false
      firstTab: true or false
    },...]
*/
//const initPageTables = (pageTables) => {
//    pageTables.forEach(tbl => {
//        let tblId = tbl.name + 'Table';
//        initDataTable(tblId, tbl.type, tbl.height);
//        if (tbl.type == 'full') {
//            loadFilterFromState(tblId);
//        };
//        if (tbl.tab) {
//            // adjust table columns after showing tab for edit data
//            let tabEl = document.querySelector('button[data-bs-target="#' + tbl.name + 'Tab"');
//            tabEl.addEventListener('shown.bs.tab', event => {
//                adjustColumns(tblId);
//            });
//        };
//        if (tbl.modal == '' && tbl.firstTab) {
//            adjustColumns(tblId);
//        };
//        if (tbl.accordionId && tbl.accordionId != '') {
//          // adjust table columns after showing tab for edit data
//          let accEl = document.querySelector('#' + tbl.accordionId);
//          accEl.addEventListener('shown.bs.collapse', event => {
//          adjustColumns(tblId);
//          });
//        };
//        if (tbl.modal != '' && tbl.firstTab) {
//            // adjust table columns after showing modal window for edit data
//            let mdlEl = document.getElementById(tbl.modal);
//            mdlEl.addEventListener('shown.bs.modal', event => {
//                adjustColumns(tblId);
//            });
//        };
//    })
//}

/* COMMON EVENT LISTENERS FOR ALL PAGES */

// preloader on load page
//window.onload = () => {

//    let hide_clocks = $('#hide_clocks').val();
//    if (hide_clocks == 1) {
//        hidePreloader();
//    }
    
//};

//const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
//const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

//show message window
// parameters: message - message text
//             type - message type ['info'(default), 'success', 'fault']
const showMessage = (message, modal_num = '', type = 'info') => {
    let messageDiv = document.querySelector('#infoMessage .border-block');
    let messageType = messageDiv.classList[1];
    if (messageType != type) {
        messageDiv.classList.remove(messageType);
        messageDiv.classList.add(type);
    }
    let modalDiv = document.querySelector('#infoMessage');
    if (modal_num === '') {
        modalDiv.classList.remove("modal-2");
    }
    else {
        modalDiv.classList.add("modal-2");
    }
    let messageText = messageDiv.lastElementChild.innerText = message;
    showModal('infoMessage');
}

function changeSelectByOtherSelect(newSelect, select_id) {
    var select = $('#' + select_id);
    $(select).find('option').remove();
    if (newSelect.length !== 0) {
        for (var i = 0; i < newSelect.length; i++) {
            $(select).append('<option value=' + newSelect[i].value_id + '>' + newSelect[i].value_name + '</option>');
        }
    } else {
        $(select).attr("placeholder", "Отсутствуют записи");
    }
    $(select).selectpicker('destroy');
    $(select).selectpicker();
}

//новая функция - взамен RefreshSelectById
function RefreshSelectByIdWithVal(select_id, value) {
    $('#' + select_id).val(value);
    $('#' + select_id).selectpicker("refresh");
    $('#' + select_id).selectpicker('destroy');
    $('#' + select_id).selectpicker();
}

//эту можно использовать, если нужно обновить только текст выбранного значения в select
function RefreshSelectById(select_id) {
    $('#' + select_id).selectpicker("refresh");
    $('#' + select_id).selectpicker('destroy');
    $('#' + select_id).selectpicker();
}

function CB_Change(cb) {
    let cb_name = cb.name;
    $('input[name="' + cb_name + '"]').not(':checked').val(false);
    $('input[name="' + cb_name + '"]:checked').val(true);
};

function TableInit(table_id, table_type, height = '48vh', adjust = 1) {

    initDataTable(table_id, table_type, height);
    if (adjust === 1) {
        adjustColumns(table_id);
    }
    if (table_type == 'full') {
        loadFilterFromState(table_id);
        document.querySelectorAll('.table-buttons button').forEach((el) => {
            el.addEventListener('click', (e) => {

                let pop = document.querySelector('.table-buttons .dt-button-collection');
                let buttonBox = el.getBoundingClientRect();
                let availBottom = window.innerHeight - buttonBox.top - el.offsetHeight;
                pop.style.marginTop = "0px";
                pop.style.right = window.innerWidth - buttonBox.right + "px";

                if (pop.offsetHeight < availBottom) {
                    pop.style.top = buttonBox.bottom + "px";
                }
                else {
                    pop.style.top = buttonBox.top - pop.offsetHeight + "px";
                }
            })
        });
    }
}

function ChangeSelectValuePerspective(event, perspective_year, select_name) {

    var elements = document.getElementsByName(select_name + '_select');
    var select_value = event.target.value;
    for (var i = 0; i < elements.length; i++) {
        var year = elements[i].getAttribute('data-' + select_name);
        if (year >= perspective_year) {
            var select_id = elements[i].getAttribute('id');
            $('#' + select_name + '_id_' + year).val(select_value);
            $('#' + select_id).val(select_value);
            RefreshSelectById(select_id);
        }
    }
}

function TabChange(event) {
    var tab_action = event.target.getAttribute("data-tab-action");
    var tab_tableid = event.target.getAttribute("data-tab-tableid");
    $('#cur_action_name').val(tab_action);
    $('#cur_table_name').val(tab_tableid);
    GetListOnPage();
}

function TabChangePopup(event) {
    var tab_action = event.target.getAttribute("data-tab-action");
    var tab_tableid = event.target.getAttribute("data-tab-tableid");
    $('#cur_action_name_pop').val(tab_action);
    $('#cur_table_name_pop').val(tab_tableid);
    GetDataOnTabPopup();
}

//if (document.querySelector('#perspective_year') !== null) {   
//    document.querySelector('#perspective_year').addEventListener('change', (event) => {
//        if (!$("#perspective_year[multiple]").length) {
//            GetListOnPage();
//        }       
//    });
//}

/* Click on Add button */
//$('.bttn-edit-data').on('click', (e) => {
//    debugger
//    let activeTabId = $('[data-bs-toggle=tab].nav-link.active').attr('data-tab-tableid');
//    if (activeTabId != undefined) {
//        switch (activeTabId) {
//            case 'tzProductionTable': OpenPopupHeatBalance(0, $('#perspective_year').val(), '/TSO/HeatBalance/OpenTZProduction', 'TZProductionDataPopup');
//                break;
//            case 'tzTransferTable': OpenPopupHeatBalance(0, $('#perspective_year').val(), '/TSO/HeatBalance/OpenTZTransfer', 'TZTransferDataPopup');
//                break;
//            case 'tzCalcCostsTable': OpenPopupCostsPrices(0, $('#perspective_year').val(), '/TSO/CostsAndPrices/OpenCostsPrices', 'TZCostsPricesDataPopup');
//                break;
//            case 'tzTransportCostsTable': OpenPopupCostsPrices(0, $('#perspective_year').val(), '/TSO/CostsAndPrices/OpenCostsPrices', 'TZCostsPricesDataPopup');
//                break;
//            case 'tzProductionCostsTable': OpenPopupCostsPrices(0, $('#perspective_year').val(), '/TSO/CostsAndPrices/OpenCostsPrices', 'TZCostsPricesDataPopup');
//                break;
//            case 'tzCalcCostsIpHsCaTable': OpenPopupCostsPrices(0, $('#perspective_year').val(), '/TSO/CostsAndPrices/OpenExpenses_IPHS_CA', 'TZCostsPricesDataPopup');
//                break;
//            case 'tzNVVTable': OpenPopupCostsPrices(0, $('#perspective_year').val(), '/TSO/CostsAndPrices/OpenNVV', 'TZCostsPricesDataPopup');
//                break;
//            case 'tzTariffTable': OpenPopupTariffConnection(0, $('#perspective_year').val(), '/TSO/TariffConnection/OpenTariffConnection', 'TZTariffConnectionDataPopup');
//                break;
//            case 'tzTariffSteamTable': OpenPopupTariffConnection(0, $('#perspective_year').val(), '/TSO/TariffConnection/OpenTariffConnection', 'TZTariffConnectionDataPopup');
//                break;
//            case 'tzIndividualFeeTable': OpenPopupIndividualFee(0, '', '/TSO/TariffConnection/OpenTPIndividualFee', 'TZIndividualFeeDataPopup');
//                break;
//            case 'tzHPStandardizedRatesTable': OpenPopupStandardizedRates(0, '', '/TSO/TariffConnection/OpenStandardizedRates', 'TZStandardizedRatesDataPopup');
//                break;
//            case 'tzPowerReservePaymentTable': OpenPopupPowerReservePayment(0, '', '/TSO/TariffConnection/OpenPowerReservePayment', 'TZPowerReservePaymentDataPopup');
//                break;
            
//        }
//    }
//    else {
//        activeTabId = $('[data-bs-toggle=tab].dropdown-item.active').attr('data-tab-tableid');
//        if (activeTabId != undefined) {
//            switch (activeTabId) {
//                case 'HplistTable': OpenPopupHeatPointAddRemove(0);
//                    break;
//                case 'HpConsumerslistTable': OpenPopupConsumersAddRemove(0);
//                    break;
//                case 'SteamTurbineTable': OpenPopup(0, 'OpenPopupSteamTurbine', 'SteamTurbineDataPopup');
//                    break;
//                case 'GasTurbineTable': OpenPopup(0, 'OpenPopupGasTurbine', 'GasTurbineDataPopup');
//                    break;
//                case 'SteamBoilersTable': OpenPopup(0, 'OpenPopupSteamBoiler', 'SteamBoilerDataPopup');
//                    break;
//                case 'HWBoilersTable': OpenPopup(0, 'OpenPopupHWBoiler', 'HWBoilerDataPopup');
//                    break;
//                case 'PistonInstallationsTable': OpenPopup(0, 'OpenPopupPistonInstallations', 'PistonInstallationsDataPopup');
//                    break;
//                case 'ROUTable': OpenPopup(0, 'OpenPopupROU', 'ROUDataPopup');
//                    break;
//                case 'WaterHeaterTable': OpenPopup(0, 'OpenPopupWaterHeater', 'WaterHeaterDataPopup');
//                    break;
//                case 'PumpsTable': OpenPopup(0, 'OpenPopupPumps', 'PumpsDataPopup');
//                    break;
//                case 'FiltersVPUTable': OpenPopup(0, 'OpenPopupFiltersVPU', 'FiltersVPUDataPopup');
//                    break;
//                case 'ClarifierVPUTable': OpenPopup(0, 'OpenPopupClarifierVPU', 'ClarifierVPUDataPopup');
//                    break;
//                case 'ReverseOsmosisTable': OpenPopup(0, 'OpenPopupReverseOsmosisInstalVPU', 'ReverseOsmosisInstalVPUDataPopup');
//                    break;
//                case 'NanoFiltrTable': OpenPopup(0, 'OpenPopupNanoFiltrationInstallVPU', 'NanoFiltrationInstallVPUDataPopup');
//                    break;
//                case 'DeaeratorsTable': OpenPopup(0, 'OpenPopupDeaerators', 'DeaeratorsDataPopup');
//                    break;
//                case 'TanksVPUTable': OpenPopup(0, 'OpenPopupTanksVPU', 'TanksVPUDataPopup');
//                    break;
//                case 'ComplexonsVPUTable': OpenPopup(0, 'OpenPopupComplexonsVPU', 'ComplexonsVPUDataPopup');
//                    break;
//                case 'SmokePipesTable': OpenPopup(0, 'OpenPopupSmokePipes', 'SmokePipesDataPopup');
//                    break;               
//            }
//        }
//        else {
//            activeTabId = e.target.id
//            switch (activeTabId) {
//                case 'progdev': OpenPopupPerspectiveDevelopment(0);
//                    break;
//            }
//        }
//    }

//});

function GetAjaxRequest() {

}

const setEnableElements = (checkId, type = 'check', radioName) => {
    let selector;
    if (type == 'check') {
        selector = '#' + checkId;
    } else if (type == 'radio') {
        selector = `input[name='${radioName}']`;
    }

    $(selector).on('change', function () {
        let check;
        if (type == 'check') {
            check = this.checked;
        } else if (type == 'radio') {
            check = $('#' + checkId)[0].checked;
        }

        $('.' + checkId).each(function () {
            if ($(this).hasClass('selectpicker')) {
                setDisabledSelect(this, !check);
                // $(this).prop('disabled', !check);
                // $(this).selectpicker('destroy');
                //  $(this).selectpicker();
                // $(this).addClass('selectpicker');
            }
            else {
                this.disabled = !check;
            }
        });
    });
};

// select -  HTMLElement <select>!!! not JQuery object
const setDisabledSelect = (select, value) => {
    //console.log(typeof select);

    let jqSelect = $(select);
    jqSelect.prop('disabled', value);
    jqSelect.selectpicker('destroy');
    jqSelect.selectpicker();
    jqSelect.addClass('selectpicker');
}

function DownloadTemplates(template_id) {
    showPreloader();
    $.ajax({
        type: 'GET',
        url: '/HSS/DownloadTemplate',
        data: {
            template_id: template_id
        },
        dataType: 'json'
    })
        .done(function (result) {
            hidePreloader();
            if (result.success == true) {
                var url = result.filename;
                var win = window.open(url, '_blank');
                win.focus();
            }
            else {
                showMessage("Произошла ошибка. Попробуйте позднее.", 'modal', 'fault');
            }
        }).fail(function (result) {
            showMessage("Произошла ошибка. Попробуйте позднее.", 'modal', 'fault');
            hidePreloader();
        });
}

//$(document).ready(function () {



//    let table1 = $('#tso_table').DataTable({
//    	language: {
//            url: 'https://cdn.datatables.net/plug-ins/1.13.1/i18n/ru.json'
//        },
//        scrollY: '500px',
//        scrollX: true,
//        scrollCollapse: true,
//      //stateSave: false,
//        paging: false,
//        info: false,
//        select: true,
//        searchBuilder: {
//	        depthLimit: 1
//	    },
//        buttons: [
//            'colvis',
//            {
//            	extend: 'searchBuilder',
//            	config: {
//                    depthLimit: 1
//                }
//            },
//            {
//                extend: 'excelHtml5',
//                className: 'exportExcelFull'
//            }
//        ],
//        dom: 'Blfrtip',
//        initComplete: function () {
//            var $buttons = $('.dt-buttons');
//            $('.exportExcelFull').hide();
//            $('#excelFull').on('click',
//                function () {
//                    $buttons.find('.exportExcelFull').click();
//                });
//        },
//        footerCallback: function (row, data, start, end, display) {
//            var api = this.api();

//            reg_total = api.column(1, { page: 'current' }).data().filter(
//                function (value, index) {
//                    return value === 'ТСО 2' ? true : false;
//                }).length;

//            // Total over this page
//            total = api.column(0, { page: 'current' }).data().count();

//            // Update footer
//            $('#all_cnt_rows').html(total);
//            $('#reg_cnt_rows').html(reg_total);
//        }
//    });

//    //обработка события на двойной клик по строке
//    $('#tso_table tbody').on('dblclick', 'tr', function () {
//        let data1 = table1.row(this).data();
//        alert('You clicked on ' + data1[0] + " row");
//    });
//});
