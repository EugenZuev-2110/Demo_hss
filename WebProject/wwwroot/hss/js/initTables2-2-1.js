$(document).ready(function () {

    let table = $('#tso_table').DataTable({
    	language: {
            url: 'https://cdn.datatables.net/plug-ins/1.13.1/i18n/ru.json'
        },
        scrollY: '500px',
        scrollX: true,
        scrollCollapse: true,
        stateSave: true,
        paging: false,
        info: false,
        select: true,
        language: {
            searchBuilder: {
                title: {
                    0: 'Конструктор поиска',
                    _: 'Конструктор поиска (%d)'
                },
                button: {
                    0: '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 512 512"><path d="M35.4 87.12l168.65 196.44A16.07 16.07 0 01208 294v119.32a7.93 7.93 0 005.39 7.59l80.15 26.67A7.94 7.94 0 00304 440V294a16.07 16.07 0 014-10.44L476.6 87.12A14 14 0 00466 64H46.05A14 14 0 0035.4 87.12z" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32"/></svg>',
                    _: '<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 512 512"><path d="M35.4 87.12l168.65 196.44A16.07 16.07 0 01208 294v119.32a7.93 7.93 0 005.39 7.59l80.15 26.67A7.94 7.94 0 00304 440V294a16.07 16.07 0 014-10.44L476.6 87.12A14 14 0 00466 64H46.05A14 14 0 0035.4 87.12z" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32"/></svg><span class="filter-num">(%d)</span>'
                }
            }
        },
        
        buttons: [
            {
                extend:'colvis',
                text:'<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 512 512"><path d="M255.66 112c-77.94 0-157.89 45.11-220.83 135.33a16 16 0 00-.27 17.77C82.92 340.8 161.8 400 255.66 400c92.84 0 173.34-59.38 221.79-135.25a16.14 16.14 0 000-17.47C428.89 172.28 347.8 112 255.66 112z" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="32"/><circle cx="256" cy="256" r="80" fill="none" stroke="currentColor" stroke-miterlimit="10" stroke-width="32"/></svg>',
                titleAttr: 'Видимость столбцов'
            },
            {
            	extend: 'searchBuilder',
                titleAttr: 'Конструктор поиска',
            	config: {
                    depthLimit: 1
                }
            },
            {
                extend: 'excelHtml5',
                className: 'exportExcelFull'
            }
        ],

        dom: 'Brt',
        fixedColumns: true,
        //"columnDefs": [
        //    { "visible": false, "targets": 3 }
        //],
        initComplete: function () {
            var $buttons = $('.dt-buttons');
            $('.exportExcelFull').hide();
            $('#excelFull').on('click',
                function () {
                    $buttons.find('.exportExcelFull').click();
                });
        },
        footerCallback: function (row, data, start, end, display) {
            var api = this.api();

            // Total over this page
            all_cnt_rows = api.column(0, { page: 'current' }).data().count();

            reg_cnt_rows = api.column(6, { page: 'current' }).data().filter(
                function (value, index) {
                    return value === 'регулируемая' ? true : false;
                }).length;

            not_reg_cnt_rows = api.column(6, { page: 'current' }).data().filter(
                function (value, index) {
                    return value === 'нерегулируемая' ? true : false;
                }).length;

            contract_delivery_cnt_rows = api.column(6, { page: 'current' }).data().filter(
                function (value, index) {
                    return value === 'договор поставки' ? true : false;
                }).length;

            comb_prod_more_25_cnt_rows = api.column(11, { page: 'current' }).data().filter(
                function (value, index) {
                    return value === 'Да' ? true : false;
                }).length;

            comb_prod_less_25_cnt_rows = api.column(12, { page: 'current' }).data().filter(
                function (value, index) {
                    return value === 'Да' ? true : false;
                }).length;

            not_comb_prod_cnt_rows = api.column(13, { page: 'current' }).data().filter(
                function (value, index) {
                    return value === 'Да' ? true : false;
                }).length;

            transfer_cnt_rows = api.column(14, { page: 'current' }).data().filter(
                function (value, index) {
                    return value === 'Да' ? true : false;
                }).length;

            sales_cnt_rows = api.column(15, { page: 'current' }).data().filter(
                function (value, index) {
                    return value === 'Да' ? true : false;
                }).length;

            // Update footer
            $('#all_cnt_rows').html(all_cnt_rows);
            $('#reg_cnt_rows').html(reg_cnt_rows);
            $('#not_reg_cnt_rows').html(not_reg_cnt_rows);
            $('#contract_delivery_cnt_rows').html(contract_delivery_cnt_rows);
            $('#comb_prod_more_25_cnt_rows').html(comb_prod_more_25_cnt_rows);
            $('#comb_prod_less_25_cnt_rows').html(comb_prod_less_25_cnt_rows);
            $('#not_comb_prod_cnt_rows').html(not_comb_prod_cnt_rows);
            $('#transfer_cnt_rows').html(transfer_cnt_rows);
            $('#sales_cnt_rows').html(sales_cnt_rows);
        }
    });

    table.buttons().container().appendTo('.main-table .buttons-right');

    //обработка события на двойной клик по строке
    $('#tso_table tbody').on('dblclick', 'tr', function () {
        let data1 = table.row(this).data();
        alert('You clicked on ' + data1[0] + " row");
    });
});