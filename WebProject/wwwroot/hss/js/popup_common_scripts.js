function ValidationFormEvent() {
    var forms = document.querySelectorAll('.needs-validation');
    Array.from(forms).forEach(form => {
        if (form.getAttribute('listener') !== 'true') {
            form.setAttribute('listener', 'true');
            form.addEventListener('submit', event => {
                if (!form.checkValidity()) {
                    event.preventDefault(false);
                    event.stopPropagation();
                }
                form.classList.add('is-validated');
                if (form.classList.contains('form-row')) {
                    event.submitter.closest('tr').classList.add('is-validated');
                }
            }, false);

        }
    });

    $(function () {
        $('[data-bs-toggle="tooltip"]').tooltip();
    });
}

function inputDoubleKeyDown(e) {
    if (e.ctrlKey && (e.which === 90 || e.which === 89 || e.which === 67 || e.which === 86)) { // Check for the Ctrl key being pressed, and if the key = [Z],[Y],[C],[V]
        return;
    }
    if ((e.which >= 96 && e.which <= 105) || e.which === 9) {
        return;
    }

    var theEvent = e || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    if (key.length == 0) return;
    var regex = /^[0-9.,\b]+$/;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }

    var keyCode = e.which ? e.which : event.keyCode;
    if (keyCode == 110 || keyCode == 190 || keyCode == 191) {
        e.preventDefault();
        var value = $(this).val();
        if (value.includes(","))
            $(this).val(value + "");
        else
            $(this).val(value + ",");
    }
}

function inputDoubleWithDothKeyDown(e) {

    if (e.ctrlKey && (e.which === 90 || e.which === 89 || e.which === 67 || e.which === 86)) { // Check for the Ctrl key being pressed, and if the key = [Z],[Y],[C],[V]
        return;
    }
    if ((e.which >= 96 && e.which <= 105) || e.which === 9) {
        return;
    }

    var theEvent = e || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    if (key.length == 0) return;
    var regex = /^[0-9.,\b]+$/;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }

    var keyCode = e.which ? e.which : event.keyCode;
    if (keyCode == 110 || keyCode == 190 || keyCode == 191) {
        e.preventDefault();
        var value = $(this).val();
        if (value.includes("."))
            $(this).val(value + "");
        else
            $(this).val(value + ".");
    }

}

function inputIntKeyDown(e) {

    // Разрешаем: backspace, delete, tab и escape
    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 ||
        // Разрешаем: Ctrl+A
        (event.keyCode == 65 && event.ctrlKey === true) ||
        // Разрешаем: home, end, влево, вправо 
        (event.keyCode >= 35 && event.keyCode <= 39)) {

        // Ничего не делаем
        return;
    }
    if (event.keyCode == 186 || event.keyCode == 32 || event.keyCode == 188 || (event.shiftKey && event.keyCode == 52)) {
        var value = $(this).val();
        $(this).val(value + ";");
        value = $(this).val();
        var lastChar = value.slice(-1);
        if (lastChar === ";" && value.slice(-2)[0] === ";") {
            $(this).val(value.slice(0, -1))
        }
        event.preventDefault();

    }
    else {
        // Запрещаем все, кроме цифр на основной клавиатуре, а так же Num-клавиатуре
        if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105) || event.shiftKey) {
            event.preventDefault();
        }
    }

}

function InitAllEventsInPopup() {
    //добавление валидации на форму вместе с тултипом
    ValidationFormEvent();

    //проверка на ввод данных в числовые инпуты.
    let inputsDouble = document.querySelectorAll("input.double[type=text]");
    inputsDouble.forEach(function (elem) {
        if (elem.getAttribute('listener_keydown') !== 'true') {
            elem.setAttribute('listener_keydown', 'true');
            elem.addEventListener("keydown", inputDoubleKeyDown);
        }
    });

    let inputsDoubleWithDot = document.querySelectorAll("input.doubleWithDot[type=text]");
    inputsDoubleWithDot.forEach(function (elem) {
        if (elem.getAttribute('listener_keydown') !== 'true') {
            elem.setAttribute('listener_keydown', 'true');
            elem.addEventListener("keydown", inputDoubleWithDothKeyDown);
        }
    });

    let inputsInt = document.querySelectorAll("input.int[type=text]");
    inputsInt.forEach(function (elem) {
        if (elem.getAttribute('listener_keydown') !== 'true') {
            elem.setAttribute('listener_keydown', 'true');
            elem.addEventListener("keydown", inputIntKeyDown);
        }
    });
}


$(document).ready(function () {

    InitAllEventsInPopup();
      // Functions for work with form's tooltips
    $.fn.showErrorTooltip = function () {
        let el = $(this);
        if (el.hasClass('data-value')) {
            el.addClass('tooltip-show');
        }
    };

    $.fn.hideErrorTooltip = function () {
        let el = $(this);
        if (el.hasClass('data-value')) {
            el.removeClass('tooltip-show');
            el.setErrorText(); // Возвращаем тексту значение по умолчанию
        }
    };

    $.fn.setErrorText = function (errText) {
        let el = $(this);
        let tooltip = el.next('.invalid-tooltip.error');

        if (el.hasClass('data-value') && tooltip !== null) {
            let tooltipObj = bootstrap.Tooltip.getInstance(tooltip);
            errText = errText !== undefined ? errText : tooltip.attr('data-bs-title'); // если текста нет, то берём его изначальное значение из элемента DOM
            tooltipObj.setContent({ '.tooltip-inner': errText }); // меняем текст непосредственно у объекта bootstrap

        }
    };

    //расчёт годовых показателей на основании I и II полугодий.
    $("input.double[type=text]").keyup(function (e) {
        if (e.target.hasAttribute("data-double")) {
            var data_double = event.target.getAttribute("data-double");

            let val_id = e.target.id;
            let s_name = val_id.substring(0, val_id.length - 6);
            let s_fp_period = val_id.substring(val_id.length - 6, val_id.length);
            let s_fp = val_id.substring(val_id.length - 6, val_id.length - 1);

            //Вода (водотведение) на технологические и хозяйственные цели
            if (data_double === "water_costs") {

                if (s_name !== "price_water_dispos_" && s_name !== "price_cold_water_") {
                    SetYearValueSum(s_name, s_fp);
                }
                //затраты на водоотведение
                let pwd = $('#price_water_dispos_' + s_fp_period).val();
                let vwd = $('#volume_water_dispos_' + s_fp_period).val();
                let cwd = Number(pwd.replace(",", ".")) * Number(vwd.replace(",", "."));
                $('#costs_water_dispos_' + s_fp_period).val(cwd.toFixed(2).toString().replace(".", ","));
                SetYearValueSum("costs_water_dispos_", s_fp);
                //затраты на водоотведение

                //затраты на холодную воду
                let pcw = $('#price_cold_water_' + s_fp_period).val();
                let vcw = $('#volume_cold_water_' + s_fp_period).val();
                let ccw = Number(pcw.replace(",", ".")) * Number(vcw.replace(",", "."));
                $('#costs_cold_water_' + s_fp_period).val(ccw.toFixed(2).toString().replace(".", ","));
                SetYearValueSum("costs_cold_water_", s_fp);
                //затраты на холодную воду

                //всего затраты, тыс. руб. без НДС
                let caw = cwd + ccw;
                $('#costs_all_water_' + s_fp_period).val(caw.toFixed(2).toString().replace(".", ","));
                SetYearValueSum("costs_all_water_", s_fp);
                //всего затраты, тыс. руб. без НДС

                if (val_id.indexOf("cold_water") !== -1) {
                    //средняя цена холодной воды за год
                    let ccw_f3 = $('#costs_cold_water_' + s_fp + '3').val();
                    let vcw_f3 = $('#volume_cold_water_' + s_fp + '3').val();
                    if (Number(vcw_f3.replace(",", ".")) > 0) {
                        let avg = Number(ccw_f3.replace(",", ".")) / Number(vcw_f3.replace(",", "."));
                        $('#avg_price_cold_water_' + s_fp + '3').val(avg.toFixed(2).toString().replace(".", ","));
                    }
                    //средняя цена холодной воды за год
                }
                else if (val_id.indexOf("water_dispos") !== -1) {
                    //средняя цена водоотведения за год
                    let ccw_f3 = $('#costs_water_dispos_' + s_fp + '3').val();
                    let vcw_f3 = $('#volume_water_dispos_' + s_fp + '3').val();
                    if (Number(vcw_f3.replace(",", ".")) > 0) {
                        let avg = Number(ccw_f3.replace(",", ".")) / Number(vcw_f3.replace(",", "."));
                        $('#avg_price_water_dispos_' + s_fp + '3').val(avg.toFixed(2).toString().replace(".", ","));
                    }
                    //средняя цена водоотведения за год
                }

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(1);
                }
            }
            //Расходы, связанные с созданием нормативных запасов топлива
            else if (data_double === "fuel_reserve") {

                SetYearValueSum(s_name, s_fp);

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(1);
                }
            }
            //Расходы на покупку теплоносителя
            else if (data_double === "heat_carrier_costs") {

                SetYearValueSum(s_name, s_fp);

                //Цена (тариф) на теплоноситель
                let vhc = $('#volume_heatcarrier_' + s_fp_period).val();
                let chc = $('#cost_heatcarrier_' + s_fp_period).val();
                if (Number(vhc.replace(",", ".")) > 0) {
                    let phc = Number(chc.replace(",", ".")) / Number(vhc.replace(",", "."));
                    $('#price_heatcarrier_' + s_fp_period).val(phc.toFixed(2).toString().replace(".", ","));
                }
                else {
                    $('#price_heatcarrier_' + s_fp_period).val("0,00");
                }

                let vhc_3 = $('#volume_heatcarrier_' + s_fp + '3').val();
                let chc_3 = $('#cost_heatcarrier_' + s_fp + '3').val();
                if (Number(vhc_3.replace(",", ".")) > 0) {
                    let phc_3 = Number(chc_3.replace(",", ".")) / Number(vhc_3.replace(",", "."));
                    $('#price_heatcarrier_' + s_fp + '3').val(phc_3.toFixed(2).toString().replace(".", ","));
                }
                //Цена (тариф) на теплоноситель

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(1);
                }
            }
            //Электроэнергия (мощность) на технологические и хозяйственные нужды
            else if (data_double === "electro_energy") {

                let type_energy = $('#' + val_id).attr('data-type-energy');
                let volt = $('#' + val_id).attr('data-voltage');
                let need = $('#' + val_id).attr('data-type-need');

                let vlvept = Number($('#volume_low_voltage_' + type_energy + '_techneed_' + s_fp_period).val().replace(",", "."));
                let vm1vept = Number($('#volume_medium1_voltage_' + type_energy + '_techneed_' + s_fp_period).val().replace(",", "."));
                let vm2vept = Number($('#volume_medium2_voltage_' + type_energy + '_techneed_' + s_fp_period).val().replace(",", "."));
                let vhvept = Number($('#volume_high_voltage_' + type_energy + '_techneed_' + s_fp_period).val().replace(",", "."));
                let vlveph = Number($('#volume_low_voltage_' + type_energy + '_houseneed_' + s_fp_period).val().replace(",", "."));
                let vm1veph = Number($('#volume_medium1_voltage_' + type_energy + '_houseneed_' + s_fp_period).val().replace(",", "."));
                let vm2veph = Number($('#volume_medium2_voltage_' + type_energy + '_houseneed_' + s_fp_period).val().replace(",", "."));
                let vhveph = Number($('#volume_high_voltage_' + type_energy + '_houseneed_' + s_fp_period).val().replace(",", "."));
                let plvep = Number($('#price_low_voltage_' + type_energy + '_' + s_fp_period).val().replace(",", "."));
                let pm1vep = Number($('#price_medium1_voltage_' + type_energy + '_' + s_fp_period).val().replace(",", "."));
                let pm2vep = Number($('#price_medium2_voltage_' + type_energy + '_' + s_fp_period).val().replace(",", "."));
                let phvep = Number($('#price_high_voltage_' + type_energy + '_' + s_fp_period).val().replace(",", "."));

                //сумма за год
                if (s_name.indexOf("price") === -1) {
                    SetYearValueSum(s_name, s_fp);
                }

                //объем на хозяйственные нужды
                if (s_name.indexOf("houseneed") !== -1) {
                    let vaeph = vlveph + vm1veph + vm2veph + vhveph;
                    $('#volume_all_' + type_energy + '_houseneed_' + s_fp_period).val(vaeph.toFixed(2).toString().replace(".", ","));
                    SetYearValueSum('volume_all_' + type_energy + '_houseneed_', s_fp);
                }

                //объем на технологические цели
                if (s_name.indexOf("techneed") !== -1) {
                    let vaept = vlvept + vm1vept + vm2vept + vhvept;
                    $('#volume_all_' + type_energy + '_techneed_' + s_fp_period).val(vaept.toFixed(2).toString().replace(".", ","));
                    SetYearValueSum('volume_all_' + type_energy + '_techneed_', s_fp);
                }

                //Средняя цена (тариф)
                let vaeph = Number($('#volume_all_' + type_energy + '_houseneed_' + s_fp_period).val().replace(",", "."));
                let vaept = Number($('#volume_all_' + type_energy + '_techneed_' + s_fp_period).val().replace(",", "."));

                if ((vaeph + vaept) > 0) {
                    let apaep = (((vlveph + vlvept) * plvep) + ((vm1veph + vm1vept) * pm1vep) + ((vm2veph + vm2vept) * pm2vep) + ((vhveph + vhvept) * phvep)) / (vaeph + vaept);
                    $('#avg_price_all_' + type_energy + '_' + s_fp_period).val(apaep.toFixed(2).toString().replace(".", ","));
                }

                //цена за год
                let vveph_fp1 = Number($('#volume_' + volt + '_voltage_' + type_energy + '_houseneed_' + s_fp + '1').val().replace(",", "."));
                let vvept_fp1 = Number($('#volume_' + volt + '_voltage_' + type_energy + '_techneed_' + s_fp + '1').val().replace(",", "."));
                let vveph_fp2 = Number($('#volume_' + volt + '_voltage_' + type_energy + '_houseneed_' + s_fp + '2').val().replace(",", "."));
                let vvept_fp2 = Number($('#volume_' + volt + '_voltage_' + type_energy + '_techneed_' + s_fp + '2').val().replace(",", "."));
                let pvep_fp1 = Number($('#price_' + volt + '_voltage_' + type_energy + '_' + s_fp + '1').val().replace(",", "."));
                let pvep_fp2 = Number($('#price_' + volt + '_voltage_' + type_energy + '_' + s_fp + '2').val().replace(",", "."));

                let vve_fp3 = vveph_fp1 + vveph_fp2 + vvept_fp1 + vvept_fp2;
                if (vve_fp3 > 0) {
                    let pvep_fp3 = (((vveph_fp1 + vvept_fp1) * pvep_fp1) + ((vveph_fp2 + vvept_fp2) * pvep_fp2)) / vve_fp3;
                    $('#price_' + volt + '_voltage_' + type_energy + '_' + s_fp + '3').val(pvep_fp3.toFixed(2).toString().replace(".", ","));
                }

                //средняя цена (тариф) за ГОД
                let vaeph_3 = Number($('#volume_all_' + type_energy + '_houseneed_' + s_fp + '3').val().replace(",", "."));
                let vaept_3 = Number($('#volume_all_' + type_energy + '_techneed_' + s_fp + '3').val().replace(",", "."));

                let vlveph_3 = Number($('#volume_low_voltage_' + type_energy + '_houseneed_' + s_fp + '3').val().replace(",", "."));
                let vm1veph_3 = Number($('#volume_medium1_voltage_' + type_energy + '_houseneed_' + s_fp + '3').val().replace(",", "."));
                let vm2veph_3 = Number($('#volume_medium2_voltage_' + type_energy + '_houseneed_' + s_fp + '3').val().replace(",", "."));
                let vhveph_3 = Number($('#volume_high_voltage_' + type_energy + '_houseneed_' + s_fp + '3').val().replace(",", "."));

                let vlvept_3 = Number($('#volume_low_voltage_' + type_energy + '_techneed_' + s_fp + '3').val().replace(",", "."));
                let vm1vept_3 = Number($('#volume_medium1_voltage_' + type_energy + '_techneed_' + s_fp + '3').val().replace(",", "."));
                let vm2vept_3 = Number($('#volume_medium2_voltage_' + type_energy + '_techneed_' + s_fp + '3').val().replace(",", "."));
                let vhvept_3 = Number($('#volume_high_voltage_' + type_energy + '_techneed_' + s_fp + '3').val().replace(",", "."));

                let plvep_3 = Number($('#price_low_voltage_' + type_energy + '_' + s_fp + '3').val().replace(",", "."));
                let pm1vep_3 = Number($('#price_medium1_voltage_' + type_energy + '_' + s_fp + '3').val().replace(",", "."));
                let pm2vep_3 = Number($('#price_medium2_voltage_' + type_energy + '_' + s_fp + '3').val().replace(",", "."));
                let phvep_3 = Number($('#price_high_voltage_' + type_energy + '_' + s_fp + '3').val().replace(",", "."));

                if ((vaeph_3 + vaept_3) > 0) {
                    let apaep_3 = (((vlveph_3 + vlvept_3) * plvep_3) + ((vm1veph_3 + vm1vept_3) * pm1vep_3) + ((vm2veph_3 + vm2vept_3) * pm2vep_3) + ((vhveph_3 + vhvept_3) * phvep_3)) / (vaeph_3 + vaept_3);
                    $('#avg_price_all_' + type_energy + '_' + s_fp + '3').val(apaep_3.toFixed(2).toString().replace(".", ","));
                }

                //всего затраты по отдельности
                let costs = ((vlveph + vlvept) * plvep) + ((vm1veph + vm1vept) * pm1vep) + ((vm2veph + vm2vept) * pm2vep) + ((vhveph + vhvept) * phvep);
                $('#costs_' + type_energy + '_' + s_fp_period).val(costs.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_' + type_energy + '_', s_fp);

                //всего затраты
                let costs_el = Number($('#costs_electricity_' + s_fp_period).val().replace(",", "."));
                let costs_ep = Number($('#costs_el_power_' + s_fp_period).val().replace(",", "."));
                let costs_all = costs_el + costs_ep;
                $('#costs_all_el_energy_' + s_fp_period).val(costs_all.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_all_el_energy_', s_fp);

                //средняя цена (тариф) покупной электроэнергии с учётом мощности
                let vaelh = Number($('#volume_all_electricity_houseneed_' + s_fp_period).val().replace(",", "."));
                let vaelt = Number($('#volume_all_electricity_techneed_' + s_fp_period).val().replace(",", "."));

                if ((vaelh + vaelt) > 0) {
                    let apaee = costs_all / (vaelh + vaelt);
                    $('#avg_price_all_el_energy_' + s_fp_period).val(apaee.toFixed(2).toString().replace(".", ","));
                }

                //за ГОД
                let vaeh_3 = Number($('#volume_all_electricity_houseneed_' + s_fp + '3').val().replace(",", "."));
                let vaet_3 = Number($('#volume_all_electricity_techneed_' + s_fp + '3').val().replace(",", "."));
                let costs_el_3 = Number($('#costs_electricity_' + s_fp + '3').val().replace(",", "."));
                let costs_ep_3 = Number($('#costs_el_power_' + s_fp + '3').val().replace(",", "."));
                if ((vaeh_3 + vaet_3) > 0) {
                    let apaee_3 = (costs_el_3 + costs_ep_3) / (vaeh_3 + vaet_3);
                    $('#avg_price_all_el_energy_' + s_fp + '3').val(apaee_3.toFixed(2).toString().replace(".", ","));
                }

                //затраты на электроэнергию
                let vlve_hneed = Number($('#volume_low_voltage_electricity_houseneed_' + s_fp_period).val().replace(",", "."));
                let vm1ve_hneed = Number($('#volume_medium1_voltage_electricity_houseneed_' + s_fp_period).val().replace(",", "."));
                let vm2ve_hneed = Number($('#volume_medium2_voltage_electricity_houseneed_' + s_fp_period).val().replace(",", "."));
                let vhve_hneed = Number($('#volume_high_voltage_electricity_houseneed_' + s_fp_period).val().replace(",", "."));

                let vlvep_hneed = Number($('#volume_low_voltage_el_power_houseneed_' + s_fp_period).val().replace(",", "."));
                let vm1vep_hneed = Number($('#volume_medium1_voltage_el_power_houseneed_' + s_fp_period).val().replace(",", "."));
                let vm2vep_hneed = Number($('#volume_medium2_voltage_el_power_houseneed_' + s_fp_period).val().replace(",", "."));
                let vhvep_hneed = Number($('#volume_high_voltage_el_power_houseneed_' + s_fp_period).val().replace(",", "."));

                let vlve_tneed = Number($('#volume_low_voltage_electricity_techneed_' + s_fp_period).val().replace(",", "."));
                let vm1ve_tneed = Number($('#volume_medium1_voltage_electricity_techneed_' + s_fp_period).val().replace(",", "."));
                let vm2ve_tneed = Number($('#volume_medium2_voltage_electricity_techneed_' + s_fp_period).val().replace(",", "."));
                let vhve_tneed = Number($('#volume_high_voltage_electricity_techneed_' + s_fp_period).val().replace(",", "."));

                let vlvep_tneed = Number($('#volume_low_voltage_el_power_techneed_' + s_fp_period).val().replace(",", "."));
                let vm1vep_tneed = Number($('#volume_medium1_voltage_el_power_techneed_' + s_fp_period).val().replace(",", "."));
                let vm2vep_tneed = Number($('#volume_medium2_voltage_el_power_techneed_' + s_fp_period).val().replace(",", "."));
                let vhvep_tneed = Number($('#volume_high_voltage_el_power_techneed_' + s_fp_period).val().replace(",", "."));

                let plve_need = Number($('#price_low_voltage_electricity_' + s_fp_period).val().replace(",", "."));
                let pm1ve_need = Number($('#price_medium1_voltage_electricity_' + s_fp_period).val().replace(",", "."));
                let pm2ve_need = Number($('#price_medium2_voltage_electricity_' + s_fp_period).val().replace(",", "."));
                let phve_need = Number($('#price_high_voltage_electricity_' + s_fp_period).val().replace(",", "."));

                let plvep_need = Number($('#price_low_voltage_el_power_' + s_fp_period).val().replace(",", "."));
                let pm1vep_need = Number($('#price_medium1_voltage_el_power_' + s_fp_period).val().replace(",", "."));
                let pm2vep_need = Number($('#price_medium2_voltage_el_power_' + s_fp_period).val().replace(",", "."));
                let phvep_need = Number($('#price_high_voltage_el_power_' + s_fp_period).val().replace(",", "."));

                //на хозяйственные цели
                let chnneed_el = (vlve_hneed * plve_need + vm1ve_hneed * pm1ve_need + vm2ve_hneed * pm2ve_need + vhve_hneed * phve_need)
                    + (vlvep_hneed * plvep_need + vm1vep_hneed * pm1vep_need + vm2vep_hneed * pm2vep_need + vhvep_hneed * phvep_need) * 6
                $('#costs_houseneed_el_energy_' + s_fp_period).val(chnneed_el.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_houseneed_el_energy_', s_fp);
                //на технологические цели
                let ctnneed_el = (vlve_tneed * plve_need + vm1ve_tneed * pm1ve_need + vm2ve_tneed * pm2ve_need + vhve_tneed * phve_need)
                    + (vlvep_tneed * plvep_need + vm1vep_tneed * pm1vep_need + vm2vep_tneed * pm2vep_need + vhvep_tneed * phvep_need) * 6
                $('#costs_techneed_el_energy_' + s_fp_period).val(ctnneed_el.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_techneed_el_energy_', s_fp);

                //всего затраты на технологические цели
                let c_tn_he = Number($('#costs_techneed_heat_energy_' + s_fp_period).val().replace(",", "."));
                let ctnneed = ctnneed_el + c_tn_he;
                $('#costs_techneed_' + s_fp_period).val(ctnneed.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_techneed_', s_fp);
                //всего затраты на хозяйственные цели
                let c_hn_he = Number($('#costs_houseneed_heat_energy_' + s_fp_period).val().replace(",", "."));
                let chnneed = chnneed_el + c_hn_he;
                $('#costs_houseneed_' + s_fp_period).val(chnneed.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_houseneed_', s_fp);

                //ВСЕГО затраты
                let costs_all_tn_hn = ctnneed + chnneed;
                $('#costs_all_energy_' + s_fp_period).val(costs_all_tn_hn.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_all_energy_', s_fp);

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(1);
                }
            }
            else if (data_double === "transfer_heat_energy") {
                SetYearValueSum(s_name, s_fp);

                //Всего затраты, тыс. руб. без НДС
                let cbhe = $('#costs_buy_heat_energy_' + s_fp_period).val();
                let cht = $('#costs_heat_transfer_' + s_fp_period).val();
                let cahe = Number(cbhe.replace(",", ".")) + Number(cht.replace(",", "."));
                $('#costs_all_heat_energy_' + s_fp_period).val(cahe.toFixed(2).toString().replace(".", ","));
                SetYearValueSum("costs_all_heat_energy_", s_fp);
                //Всего затраты, тыс. руб. без НДС

                //Средняя цена покупной тепловой энергии
                let vbhe = $('#volume_buy_heat_energy_' + s_fp_period).val();
                if (Number(vbhe.replace(",", ".")) > 0) {
                    let apbhe = cahe * 1000 / Number(vbhe.replace(",", "."));
                    $('#avg_price_buy_heat_energy_' + s_fp_period).val(apbhe.toFixed(2).toString().replace(".", ","));
                    SetYearValueSum("avg_price_buy_heat_energy_", s_fp);
                }
                //Средняя цена покупной тепловой энергии

                //затраты на тепловую энергию на хозяйственные цели
                let avgp_he = Number($('#avg_price_buy_heat_energy_' + s_fp_period).val().replace(",", "."));
                let vb_he_hn = Number($('#volume_buy_heat_energy_houseneed_' + s_fp_period).val().replace(",", "."));

                let costs_hn_he = vb_he_hn * avgp_he;
                $('#costs_houseneed_heat_energy_' + s_fp_period).val(costs_hn_he.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_houseneed_heat_energy_', s_fp);

                //всего затраты на хозяйственные цели (плюс электроэнергия)
                let chnneed_el = Number($('#costs_houseneed_el_energy_' + s_fp_period).val().replace(",", "."));
                let costs_hn = costs_hn_he + chnneed_el;
                $('#costs_houseneed_' + s_fp_period).val(costs_hn.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_houseneed_', s_fp);

                //затраты на тепловую энергию на технологические цели
                let vb_he_tn = Number($('#volume_buy_heat_energy_techneed_' + s_fp_period).val().replace(",", "."));
                let costs_tn_he = vb_he_tn * avgp_he;
                $('#costs_techneed_heat_energy_' + s_fp_period).val(costs_tn_he.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_techneed_heat_energy_', s_fp);
                //всего затраты на хозяйственные цели (плюс электроэнергия)
                let ctnneed_el = Number($('#costs_techneed_el_energy_' + s_fp_period).val().replace(",", "."));
                let costs_tn = costs_tn_he + ctnneed_el;
                $('#costs_techneed_' + s_fp_period).val(costs_tn.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_techneed_', s_fp);

                //ВСЕГО затраты (технологические + хозяйственные цели)
                let costs_all_tn_hn = costs_tn + costs_hn;
                $('#costs_all_energy_' + s_fp_period).val(costs_all_tn_hn.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_all_energy_', s_fp);

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(1);
                }
            }
            //расходы на приобретение сырья и материалов
            else if (data_double === "materials_costs") {

                SetYearValueSum(s_name, s_fp);

                let c_rea = Number($('#cost_reagents_' + s_fp_period).val().replace(",", "."));
                let c_gsm = Number($('#cost_gsm_' + s_fp_period).val().replace(",", "."));
                let c_rep = Number($('#cost_repair_' + s_fp_period).val().replace(",", "."));
                let c_ts = Number($('#cost_tech_service_' + s_fp_period).val().replace(",", "."));
                let c_sc = Number($('#cost_spec_clothes_' + s_fp_period).val().replace(",", "."));
                let c_hi = Number($('#cost_house_inventary_' + s_fp_period).val().replace(",", "."));
                let c_o = Number($('#cost_other_' + s_fp_period).val().replace(",", "."));

                //всего расходов
                let cost_mat = c_rea + c_gsm + c_rep + c_ts + c_sc + c_hi + c_o;
                $('#cost_materials_all_' + s_fp_period).val(cost_mat.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('cost_materials_all_', s_fp);
                //всего расходов

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(2);
                }
            }
            //расходы на оплату работ и услуг производственного характера, выполн¤емых по договорам со сторонними организаци¤ми
            else if (data_double === "other_org_costs") {
                SetYearValueSum(s_name, s_fp);

                let c_tran = Number($('#cost_transport_' + s_fp_period).val().replace(",", "."));
                let c_tech = Number($('#cost_tech_regulation_' + s_fp_period).val().replace(",", "."));
                let c_sup = Number($('#cost_other_support_prod_' + s_fp_period).val().replace(",", "."));
                let c_serv = Number($('#cost_other_service_prod_' + s_fp_period).val().replace(",", "."));

                //всего расходов
                let cost_oo_all = c_tran + c_tech + c_sup + c_serv;
                $('#cost_other_org_all_' + s_fp_period).val(cost_oo_all.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('cost_other_org_all_', s_fp);
                //всего расходов

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(2);
                }
            }
            //расходы на оплату иных работ и услуг, выполн¤емых по договорам с организаци¤ми
            else if (data_double === "org_serv_other_costs") {
                SetYearValueSum(s_name, s_fp);

                let c_con_s = Number($('#cost_connect_service_' + s_fp_period).val().replace(",", "."));
                let c_sec_s = Number($('#cost_security_service_' + s_fp_period).val().replace(",", "."));
                let c_com_s = Number($('#cost_communal_service_' + s_fp_period).val().replace(",", "."));
                let c_cons_s = Number($('#cost_consulting_service_' + s_fp_period).val().replace(",", "."));
                let c_leg_s = Number($('#cost_legal_service_' + s_fp_period).val().replace(",", "."));
                let c_inf_s = Number($('#cost_inform_service_' + s_fp_period).val().replace(",", "."));
                let c_aud_s = Number($('#cost_audit_service_' + s_fp_period).val().replace(",", "."));
                let c_strman_s = Number($('#cost_strategic_manag_service_' + s_fp_period).val().replace(",", "."));
                let c_prepdev_s = Number($('#cost_prepare_develop_service_' + s_fp_period).val().replace(",", "."));
                let c_targf_s = Number($('#cost_targeted_funds_' + s_fp_period).val().replace(",", "."));
                let c_addins_s = Number($('#cost_additional_insure_' + s_fp_period).val().replace(",", "."));
                let c_otherwork_s = Number($('#cost_other_work_service_' + s_fp_period).val().replace(",", "."));

                //всего расходов
                let cost_oo_all = c_con_s + c_sec_s + c_com_s + c_cons_s + c_leg_s + c_inf_s + c_aud_s + c_strman_s + c_prepdev_s + c_targf_s + c_addins_s + c_otherwork_s;
                $('#cost_org_other_service_all_' + s_fp_period).val(cost_oo_all.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('cost_org_other_service_all_', s_fp);
                //всего расходов

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(2);
                }
            }
            //расходы на оплату труда
            else if (data_double === "salary_costs") {
                let type_workers = $('#' + val_id).attr('data-type');

                let sd = Number($('#soc_deduction_' + type_workers + '_workers_' + s_fp_period).val().replace(",", "."));
                let salary = Number($('#avg_salary_' + type_workers + '_workers_' + s_fp_period).val().replace(",", "."));
                let count = Number($('#count_' + type_workers + '_workers_' + s_fp_period).val().replace(",", "."));

                //затраты на оплату труда без отчислений
                let costs = count * salary * 6 / 1000;
                $('#costs_' + type_workers + '_workers_salary_' + s_fp_period).val(costs.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_' + type_workers + '_workers_salary_', s_fp);
                //затраты на оплату труда без отчислений

                let sd_fp1 = Number($('#soc_deduction_' + type_workers + '_workers_' + s_fp + '1').val().replace(",", "."));
                let salary_fp1 = Number($('#avg_salary_' + type_workers + '_workers_' + s_fp + '1').val().replace(",", "."));
                let count_fp1 = Number($('#count_' + type_workers + '_workers_' + s_fp + '1').val().replace(",", "."));

                let sd_fp2 = Number($('#soc_deduction_' + type_workers + '_workers_' + s_fp + '2').val().replace(",", "."));
                let salary_fp2 = Number($('#avg_salary_' + type_workers + '_workers_' + s_fp + '2').val().replace(",", "."));
                let count_fp2 = Number($('#count_' + type_workers + '_workers_' + s_fp + '2').val().replace(",", "."));

                let count_salary = (salary_fp1 * count_fp1) + (salary_fp2 * count_fp2);

                if (count_salary > 0) {
                    //отчисления на соц. нужды
                    let sd_fp3 = ((salary_fp1 * count_fp1 * sd_fp1) + (salary_fp2 * count_fp2 * sd_fp2)) / ((count_fp1 * salary_fp1) + (count_fp2 * salary_fp2));
                    $('#soc_deduction_' + type_workers + '_workers_' + s_fp + '3').val(sd_fp3.toFixed(2).toString().replace(".", ","));
                    //отчисления на соц. нужды

                    if (count_fp1 + count_fp2 > 0) {
                        //среднемесячная оплата труда
                        let salary_fp3 = count_salary / (count_fp1 + count_fp2);
                        $('#avg_salary_' + type_workers + '_workers_' + s_fp + '3').val(salary_fp3.toFixed(2).toString().replace(".", ","));
                        //среднемесячная оплата труда

                        //численность персонала
                        let count_fp3 = ((salary_fp1 * count_fp1 * 6) + (salary_fp2 * count_fp2 * 6)) / (salary_fp3 * 12);
                        $('#count_' + type_workers + '_workers_' + s_fp + '3').val(count_fp3.toFixed(2).toString().replace(".", ","));
                        //численность персонала
                    }
                }

                let costs_other = Number($('#costs_other_workers_salary_' + s_fp_period).val().replace(",", "."));
                let costs_adm = Number($('#costs_adm_manag_workers_salary_' + s_fp_period).val().replace(",", "."));
                let costs_shop = Number($('#costs_shop_workers_salary_' + s_fp_period).val().replace(",", "."));
                let costs_repair = Number($('#costs_repair_workers_salary_' + s_fp_period).val().replace(",", "."));
                let costs_prod = Number($('#costs_prod_workers_salary_' + s_fp_period).val().replace(",", "."));

                //расходы на оплату труда без отчислений (общее)
                let costs_all = costs_other + costs_adm + costs_shop + costs_repair + costs_prod;
                $('#costs_all_salary_' + s_fp_period).val(costs_all.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('costs_all_salary_', s_fp);
                //расходы на оплату труда без отчислений (общее)

                let count_other = Number($('#count_other_workers_' + s_fp_period).val().replace(",", "."));
                let count_adm = Number($('#count_adm_manag_workers_' + s_fp_period).val().replace(",", "."));
                let count_shop = Number($('#count_shop_workers_' + s_fp_period).val().replace(",", "."));
                let count_repair = Number($('#count_repair_workers_' + s_fp_period).val().replace(",", "."));
                let count_prod = Number($('#count_prod_workers_' + s_fp_period).val().replace(",", "."));

                //средн¤¤ списочна¤ численность персонала
                let count_all = count_other + count_adm + count_shop + count_repair + count_prod;
                $('#avg_count_workers_' + s_fp_period).val(count_all.toFixed(2).toString().replace(".", ","));
                //средн¤¤ списочна¤ численность персонала

                //среднемес¤чна¤ оплата труда
                if (count_all > 0) {
                    let salary_all = (costs_all * 1000) / (count_all * 6);
                    $('#avg_salary_all_' + s_fp_period).val(salary_all.toFixed(2).toString().replace(".", ","));
                }
                //среднемес¤чна¤ оплата труда

                let avg_count_fp1 = Number($('#avg_count_workers_' + s_fp + '1').val().replace(",", "."));
                let costs_all_fp1 = Number($('#costs_all_salary_' + s_fp + '1').val().replace(",", "."));
                let avg_count_fp2 = Number($('#avg_count_workers_' + s_fp + '2').val().replace(",", "."));
                let costs_all_fp2 = Number($('#costs_all_salary_' + s_fp + '2').val().replace(",", "."));

                if (avg_count_fp1 + avg_count_fp2 > 0) {
                    //среднемесячная оплата труда (√ќƒ)
                    let avg_salary_fp3 = ((costs_all_fp1 + costs_all_fp2) * 1000) / ((avg_count_fp1 + avg_count_fp2) * 6)
                    $('#avg_salary_all_' + s_fp + '3').val(avg_salary_fp3.toFixed(2).toString().replace(".", ","));
                    //среднемесячная оплата труда (√ќƒ)

                    if (avg_salary_fp3 > 0) {
                        //средняя списочная численность персонала (√ќƒ)
                        let avg_count_fp3 = ((costs_all_fp1 + costs_all_fp2) * 1000) / (avg_salary_fp3 * 12)
                        $('#avg_count_workers_' + s_fp + '3').val(avg_count_fp3.toFixed(2).toString().replace(".", ","));
                        //средняя списочная¤ численность персонала (√ќƒ)
                    }
                }

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(2);
                }
            }
            //Расходы на ремонт основных средств, выполняемый подрядным способом / Расходы на вывод из эксплуатации (в том числе на консервацию) и вывод из консервации
            else if (data_double === "repair_fund_costs" || data_double === "decomission_costs") {
                SetYearValueSum(s_name, s_fp);

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(2);
                }
            }
            //Прочие операционные расходы
            else if (data_double === "other_operatings_costs") {
                SetYearValueSum(s_name, s_fp);

                //арендная плата непроизводственных объектов
                if (s_name.indexOf("rent") !== -1) {
                    let rnosp = GetNumValueWithPoint('rent_nonprod_obj_state_prop_' + s_fp_period);
                    let rnomp = GetNumValueWithPoint('rent_nonprod_obj_municipal_prop_' + s_fp_period);
                    let rnoo = GetNumValueWithPoint('rent_nonprod_obj_other_' + s_fp_period);

                    let rent_no_all = rnosp + rnomp + rnoo;
                    $('#rent_nonprod_obj_all_' + s_fp_period).val(rent_no_all.toFixed(2).toString().replace(".", ","));
                    SetYearValueSum('rent_nonprod_obj_all_', s_fp);
                }
                //арендная плата непроизводственных объектов

                //прочие операционные расходы
                if (s_name.indexOf("other_operating") !== -1) {
                    let cooh = GetNumValueWithPoint('cost_other_operating_house_' + s_fp_period);
                    let coo = GetNumValueWithPoint('cost_other_operating_' + s_fp_period);

                    let cost_oo_all = cooh + coo;
                    $('#cost_other_operatings_all_' + s_fp_period).val(cost_oo_all.toFixed(2).toString().replace(".", ","));
                    SetYearValueSum('cost_other_operatings_all_', s_fp);
                }
                //прочие операционные расходы

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(2);
                }
            }
            //Расходы на уплату налогов, сборов и других обязательных платежей
            else if (data_double === "taxes_costs") {
                SetYearValueSum(s_name, s_fp);

                //Расходы на уплату налогов, тыс. руб.
                if (s_name.indexOf("tax") !== -1) {
                    let ctop = GetNumValueWithPoint('cost_tax_org_property_' + s_fp_period);
                    let ctl = GetNumValueWithPoint('cost_tax_land_' + s_fp_period);
                    let ctt = GetNumValueWithPoint('cost_tax_transport_' + s_fp_period);
                    let ctw = GetNumValueWithPoint('cost_tax_water_' + s_fp_period);
                    let cto = GetNumValueWithPoint('cost_tax_other_' + s_fp_period);

                    let cost_taxes_all = ctop + ctl + ctt + ctw + cto;
                    $('#cost_taxes_all_' + s_fp_period).val(cost_taxes_all.toFixed(2).toString().replace(".", ","));
                    SetYearValueSum('cost_taxes_all_', s_fp);
                }
                //Расходы на уплату налогов, тыс. руб.

                //Всего расходов
                let cep = GetNumValueWithPoint('cost_emissions_pollutant_' + s_fp_period);
                let cri = GetNumValueWithPoint('cost_required_insure_' + s_fp_period);
                let cop = GetNumValueWithPoint('cost_other_pay_' + s_fp_period);
                let cta = GetNumValueWithPoint('cost_taxes_all_' + s_fp_period);

                let cost_rp_all = cta + cep + cri + cop;
                $('#cost_req_pay_all_' + s_fp_period).val(cost_rp_all.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('cost_req_pay_all_', s_fp);
                //Всего расходов

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(3);
                }
            }
            // Тариф на тепловую энергию в горячей воде. Одноставочный тариф
            else if (data_double === "he_hw_singlerate_tariff") {

                let type = $('#' + val_id).attr('data-type');
                SetYearValueSingleStageTariffUnit(s_name, s_fp, type);
            }
            // Тариф на тепловую энергию в горячей воде. Двухставочный тариф
            else if (data_double === "he_hw_doublerate_tariff") {

                let type = $('#' + val_id).attr('data-type');
                SetYearValueDoubleStageTariffUnit(s_name, s_fp, type);
            }
            // Тариф на тепловую энергию в паре (одноставочный), руб./Гкал без НДС. Параметр пара 1
            else if (data_double === "he_steam_singlerate_tariff_1") {

                let type = $('#' + val_id).attr('data-type');
                SetYearValueSinglerateTariffSteam(type, 1);
            }
            // Тариф на тепловую энергию в паре (одноставочный), руб./Гкал без НДС. Параметр пара 2
            else if (data_double === "he_steam_singlerate_tariff_2") {

                let type = $('#' + val_id).attr('data-type');
                SetYearValueSinglerateTariffSteam(type, 2);
            }
            // Тариф на тепловую энергию в паре (одноставочный), руб./Гкал без НДС. Параметр пара 3
            else if (data_double === "he_steam_singlerate_tariff_3") {

                let type = $('#' + val_id).attr('data-type');
                SetYearValueSinglerateTariffSteam(type, 3);
            }
            // Тариф на тепловую энергию в паре (одноставочный), руб./Гкал без НДС. Параметр пара 4  
            else if (data_double === "he_steam_singlerate_tariff_4") {

                let type = $('#' + val_id).attr('data-type');
                SetYearValueSinglerateTariffSteam(type, 4);
            }
            // Тариф ЕТО / Тариф с субсидир. / Льготный тариф, руб./Гкал без НДС. Бюджетные потребители
            else if (data_double === "eto_amount_preference_1") {

                let type = $('#' + val_id).attr('data-type');
                CalculateEmoAmountPreferenceTariff(type, 'buget');
            }
            // Тариф ЕТО / Тариф с субсидир. / Льготный тариф, руб./Гкал без НДС. Население
            else if (data_double === "eto_amount_preference_2") {

                let type = $('#' + val_id).attr('data-type');
                CalculateEmoAmountPreferenceTariff(type, 'public');
            }
            // Тариф ЕТО / Тариф с субсидир. / Льготный тариф, руб./Гкал без НДС. Прочие
            else if (data_double === "eto_amount_preference_3") {

                let type = $('#' + val_id).attr('data-type');
                CalculateEmoAmountPreferenceTariff(type, 'other');
            }
            // Тариф на теплоноситель (горячая вода / пар) и тарифы на горячую воду, без НДС    
            else if (data_double === "he_tariff_steam_hotwater") {

                let type = $('#' + val_id).attr('data-type');
                SetYearHeatCarrierSteamHotWater(type);
            }
            // Тариф на передачу тепловой энергии, руб./Гкал без НДС
            else if (data_double === "he_transfer") {

                let type = $('#' + val_id).attr('data-type');
                SetHeatEnergyTransfer(type);
            }
            // Тариф на тепловую энергию в горячей воде. Главное окно
            else if (data_double === "he_hw_main_tariff") {
                let type = $('#' + val_id).attr('data-type');
                SetYearValueMainTariffUnit(type);
            }
            // Объем перекрестного субсидирования
            else if (data_double === "cross_subsidization_amount") {
                let type = $('#' + val_id).attr('data-type');
                CalculateCrossSubsidizationAmount(type);
            }
            //Амортизация основных средств и нематериальных активов
            else if (data_double === "amortization_costs") {
                SetYearValueSum(s_name, s_fp);

                //Всего амортизационные начисления
                let ape = GetNumValueWithPoint('amortization_prod_equip_' + s_fp_period);
                let aom = GetNumValueWithPoint('amortization_other_means_' + s_fp_period);

                let amort_all = ape + aom;
                $('#cost_amortization_all_' + s_fp_period).val(amort_all.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('cost_amortization_all_', s_fp);
                //Всего амортизационные начисления

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(3);
                }
            }
            //Расходы на оплату услуг, оказываемых организациями, осуществляющими регулируемые виды деятельности / Налог на прибыль
            else if (data_double === "reg_org_service_costs" || data_double === "income_tax_costs") {
                SetYearValueSum(s_name, s_fp);

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(3);
                }
            }
            //Прочие неподконтрольные расходы
            else if (data_double === "uncontrol_other_costs") {
                SetYearValueSum(s_name, s_fp);

                //Арендная плата производственных объектов
                let rposp = GetNumValueWithPoint('rent_prod_obj_state_prop_' + s_fp_period);
                let rpomp = GetNumValueWithPoint('rent_prod_obj_municipal_prop_' + s_fp_period);
                let rpoo = GetNumValueWithPoint('rent_prod_obj_other_' + s_fp_period);

                let rent_po_all = rposp + rpomp + rpoo;
                $('#rent_prod_obj_all_' + s_fp_period).val(rent_po_all.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('rent_prod_obj_all_', s_fp);
                //Арендная плата производственных объектов

                //Лизинговый платеж
                let lpo = GetNumValueWithPoint('leasing_prod_obj_' + s_fp_period);
                let lnpowor = GetNumValueWithPoint('leasing_nonprod_obj_with_own_rights_' + s_fp_period);

                let lo_all = lpo + lnpowor;
                $('#leasing_obj_all_' + s_fp_period).val(lo_all.toFixed(2).toString().replace(".", ","));
                SetYearValueSum('leasing_obj_all_', s_fp);
                //Лизинговый платеж

                if (s_fp.indexOf("plan") !== -1) {
                    SumCostsEnergy(3);
                }
            }
            //нормативный удельный расход топлива на отпуск тепла (это уже не актуально, т.к. расчёт поменялся)
            //else if (data_double === "norm_cons_fuel") {
            //    let ncfp_1 = GetNumValueWithPoint('norm_consump_fuel_plan_1');
            //    let ncfp_2 = GetNumValueWithPoint('norm_consump_fuel_plan_2');

            //    let gmhp_1 = GetNumValueWithPoint('ghe_minus_hc_plan_1');
            //    let gmhp_2 = GetNumValueWithPoint('ghe_minus_hc_plan_2');

            //    if ((gmhp_1 + gmhp_2) > 0) {
            //        let ncfp_3 = (ncfp_1 * gmhp_1 + ncfp_2 * gmhp_2) / (gmhp_1 + gmhp_2);
            //        $('#norm_consump_fuel_plan_3').val(ncfp_3.toFixed(2).toString().replace(".", ","));
            //    }
            //}
            //Фактические расходы на финансирование в соответствии с ИП ТС и КС
            else if (data_double === "iphc" || data_double === "conc_aggree") {
                SetYearValueSum(s_name, s_fp);

                if (s_name.indexOf("amortization") !== -1) {
                    let afi = GetNumValueWithPoint('amortization_finance_invest_' + data_double + '_' + s_fp_period);
                    let adoc = GetNumValueWithPoint('amortization_deduction_on_credit_' + data_double + '_' + s_fp_period);

                    let aa_set = afi + adoc;
                    SetNumValueWithComma('amortization_all_' + data_double + '_' + s_fp_period, aa_set);
                    SetYearValueSum('amortization_all_' + data_double + '_', s_fp);
                }
                else if (s_name.indexOf("budget") !== -1) {
                    let fb = GetNumValueWithPoint('federal_budget_' + data_double + '_' + s_fp_period);
                    let rb = GetNumValueWithPoint('regional_budget_' + data_double + '_' + s_fp_period);
                    let lb = GetNumValueWithPoint('local_budget_' + data_double + '_' + s_fp_period);

                    let ba_set = fb + rb + lb;
                    SetNumValueWithComma('budget_all_' + data_double + '_' + s_fp_period, ba_set);
                    SetYearValueSum('budget_all_' + data_double + '_', s_fp);
                }
                else if (s_name.indexOf("attracted") !== -1) {
                    let atc = GetNumValueWithPoint('attracted_credits_' + data_double + '_' + s_fp_period);
                    let atl = GetNumValueWithPoint('attracted_loans_' + data_double + '_' + s_fp_period);
                    let atof = GetNumValueWithPoint('attracted_other_funds_' + data_double + '_' + s_fp_period);
                    let atrfs = GetNumValueWithPoint('attracted_received_funds_securities_' + data_double + '_' + s_fp_period);

                    let atfa_set = atc + atl + atof + atrfs;
                    SetNumValueWithComma('attracted_funds_all_' + data_double + '_' + s_fp_period, atfa_set);
                    SetYearValueSum('attracted_funds_all_' + data_double + '_', s_fp);
                }

                let pof = GetNumValueWithPoint('profit_own_funds_' + data_double + '_' + s_fp_period);
                let aa_get = GetNumValueWithPoint('amortization_all_' + data_double + '_' + s_fp_period);
                let ba_get = GetNumValueWithPoint('budget_all_' + data_double + '_' + s_fp_period);
                let atfa_get = GetNumValueWithPoint('attracted_funds_all_' + data_double + '_' + s_fp_period);
                let cc = GetNumValueWithPoint('connection_charge_' + data_double + '_' + s_fp_period);
                let om = GetNumValueWithPoint('other_means_' + data_double + '_' + s_fp_period);

                let ea = pof + aa_get + ba_get + atfa_get + cc + om;
                SetNumValueWithComma('expenses_all_' + data_double + '_' + s_fp_period, ea);
                SetYearValueSum('expenses_all_' + data_double + '_', s_fp);
            }
            //Фактический объем дотаций и полученные доходы сверх реализации по утвержденным тарифам
            //Избыток средств, полученный за отчётные периоды регулирования
            //Экономия операционных и неподконтрольных расходов
            else if (data_double === "exp_add_values" || data_double === "excess_funds_nvv" || data_double === "economy_costs_nvv"
                || data_double === "method_analogues_comparison") {
                SetYearValueSum(s_name, s_fp);
            }
            //Перекрёстное субсидирование
            else if (data_double === "cross_subsidy_nvv") {
                SetYearValueSum(s_name, s_fp);

                //Всего
                let csa = GetNumValueWithPoint('cross_subs_between_activ_' + s_fp_period) + GetNumValueWithPoint('cross_subs_between_consumer_' + s_fp_period);
                SetNumValueWithComma('cross_subsidy_all_' + s_fp_period, csa);
                SetYearValueSum('cross_subsidy_all_', s_fp);
                //Всего
            }
            //Недополученные доходы / Выпадающие расходы
            else if (data_double === "lost_revenue_nvv") {
                SetYearValueSum(s_name, s_fp);

                //Всего недополученных доходов и выпадающих расходов
                let lra = GetNumValueWithPoint('lost_revenue_pretrial_dispute_' + s_fp_period) + GetNumValueWithPoint('lost_revenue_disagree_' + s_fp_period)
                    + GetNumValueWithPoint('lost_revenue_disagree_' + s_fp_period) + GetNumValueWithPoint('cost_notaccounted_reg_body_' + s_fp_period)
                    + GetNumValueWithPoint('cost_grow_price_product_' + s_fp_period) + GetNumValueWithPoint('cost_service_borrowed_funds_' + s_fp_period)
                    + GetNumValueWithPoint('lost_revenue_from_connect_obj_' + s_fp_period) + GetNumValueWithPoint('lost_revenue_other_' + s_fp_period);
                SetNumValueWithComma('lost_revenue_all_' + s_fp_period, lra);
                SetYearValueSum('lost_revenue_all_', s_fp);
                //Всего недополученных доходов и выпадающих расходов
            }
            //Объём и финансирование капитальных вложений
            else if (data_double === "cap_inv_nvv") {
                SetYearValueSum(s_name, s_fp);

                //Капитальные вложения всего
                if (s_name.indexOf("volume") !== -1) {
                    let cia = GetNumValueWithPoint('volume_invest_prod_electroenergy_' + s_fp_period) + GetNumValueWithPoint('volume_invest_prod_heatenergy_' + s_fp_period)
                        + GetNumValueWithPoint('volume_invest_prod_heatcarrier_' + s_fp_period) + GetNumValueWithPoint('volume_invest_transfer_heatenergy_' + s_fp_period)
                        + GetNumValueWithPoint('volume_invest_other_' + s_fp_period);
                    SetNumValueWithComma('capital_investment_all_' + s_fp_period, cia);
                    SetYearValueSum('capital_investment_all_', s_fp);
                }
                //Капитальные вложения всего
                else {
                    //Финансирование капитальных вложений всего
                    let fcia = GetNumValueWithPoint('amortisation_deduction_fonds_' + s_fp_period) + GetNumValueWithPoint('finance_by_nonused_funds_' + s_fp_period)
                        + GetNumValueWithPoint('finance_by_profit_' + s_fp_period) + GetNumValueWithPoint('finance_by_federal_budjet_' + s_fp_period)
                        + GetNumValueWithPoint('finance_by_local_budjet_' + s_fp_period) + GetNumValueWithPoint('finance_by_regional_budjet_' + s_fp_period)
                        + GetNumValueWithPoint('finance_by_other_sources_' + s_fp_period) + GetNumValueWithPoint('finance_by_credits_' + s_fp_period)
                        + GetNumValueWithPoint('finance_by_realize_securities_' + s_fp_period) + GetNumValueWithPoint('finance_by_connection_charge_' + s_fp_period);
                    SetNumValueWithComma('finance_capital_investment_all_' + s_fp_period, fcia);
                    SetYearValueSum('finance_capital_investment_all_', s_fp);
                    //Финансирование капитальных вложений всего
                }
            }
            //Метод индексации (НВВ)
            else if (data_double === "method_indexation") {
                SetYearValueSum(s_name, s_fp);

                if (s_name.indexOf("correct") !== -1) {

                    let tcn = GetNumValueWithPoint('correct_accounting_deviation_' + s_fp_period) + GetNumValueWithPoint('correct_safety_quality_product_' + s_fp_period)
                        + GetNumValueWithPoint('correct_nvv_change_invest_prog_' + s_fp_period) + GetNumValueWithPoint('correct_subj_accounting_nvv_' + s_fp_period);
                    SetNumValueWithComma('total_correction_nvv_' + s_fp_period, tcn);
                    SetYearValueSum('total_correction_nvv_', s_fp);
                }
                else {

                    let rabrp = GetNumValueWithPoint('economically_justified_costs_' + s_fp_period) + GetNumValueWithPoint('profit_regulated_organisation_' + s_fp_period)
                        + GetNumValueWithPoint('economy_consump_resources_' + s_fp_period);
                    SetNumValueWithComma('result_activity_before_reg_price_' + s_fp_period, rabrp);
                    SetYearValueSum('result_activity_before_reg_price_', s_fp);
                }

                let total = GetNumValueWithPoint('result_activity_before_reg_price_' + s_fp_period) + GetNumValueWithPoint('total_correction_nvv_' + s_fp_period);
                SetNumValueWithComma('total_correction_nvv_mA_' + s_fp_period, total);
                SetYearValueSum('total_correction_nvv_mA_', s_fp);
            }
            //Распределение расходов на производство и транспорт
            else if (data_double === "costs_distribution") {

                if (s_name.indexOf("cost") !== -1) {
                    let ccp = 1 - GetNumValueWithPoint('coef_cost_on_transport_' + s_fp_period);
                    SetNumValueWithComma('coef_cost_on_production_' + s_fp_period, ccp);
                }
                else {
                    let cca = 1 - GetNumValueWithPoint('coef_amortiz_on_transport_' + s_fp_period);
                    SetNumValueWithComma('coef_amortiz_on_production_' + s_fp_period, cca);
                }
            }
        }
    });

});

var onFailedForm = function (xhr) {
    hidePreloader();
    showMessage(xhr.responseText, 'modal', 'fault');
};

var onBeginForm = function (xhr) {
    showPreloader();
};

var onSuccessForm = function (xhr) {
    hidePreloader();
    if (xhr.success == true)
        showMessage('Данные сохранены.', 'modal', 'success');
    else
        showMessage('Произошла ошибка. Обратитесь к администратору.', 'modal', 'fault');
};

var onBeginFormTZ = function (xhr) {
    showPreloader();
};

var onFailedFormTZ = function (xhr) {
    hidePreloader();
    showMessage('Произошла ошибка.', 'modal', 'fault');
};

var onSuccessFormTZ = function (xhr) {
    hidePreloader();
    if (xhr.isDelete == true) {
        $("#refresh_table").val(1);
        showMessage('Данные удалены.', 'modal', 'success');
    }
    else if (xhr.isDelete == false) {
        $("#refresh_table").val(1);
        showMessage('Произошла ошибка при удалении.', 'modal', 'success');
    }
    else if (xhr.success == true) {
        $("#refresh_table").val(1);
        showMessage('Данные сохранены.', 'modal', 'success');           
        if (xhr.isModified == 0) {            
            if (xhr.method_name == 'standardized_rates') {
                OpenPopupPowerReservePayment(xhr.tso_id, xhr.decision_num, '/TSO/TariffConnection/OpenStandardizedRates', 'TZStandardizedRatesDataPopup', 1)
            }
            else if (xhr.method_name == 'power_reserve_payment') {
                OpenPopupStandardizedRates(xhr.tso_id, xhr.decision_num, '/TSO/TariffConnection/OpenPowerReservePayment', 'TZPowerReservePaymentDataPopup', 1)
            }
            else if (xhr.method_name == 'individual_fee') {
                OpenPopupIndividualFee(xhr.consumer_id, xhr.decision_num, '/TSO/TariffConnection/OpenTPIndividualFee', 'TZIndividualFeeDataPopup', 1)
            }           
        }
    }
    else {
        showMessage(xhr.error, 'modal', 'fault');
    }
};

var onDeleteTableRowFormTZ = function (xhr) {
    hidePreloader();
    if (xhr.success == true) {
        $("#refresh_table").val(1);
        showMessage('Данные удалены.', 'modal', 'success');
    }
    else {
        showMessage(xhr.error, 'modal', 'fault');
    }
};



var onSuccessFormSource1 = function (xhr) {
    debugger
    let is_success = 1;
    if (xhr.success == true) {
        $("#refresh_sourcelist_table").val(1);
        let select_id = "source_unom_list";
        if (xhr.is_per_save == true) {
            SelectValue();
            if (document.getElementById('Source_HeatPowerOutput').checkValidity() === true) {
                document.getElementById('Source_HeatPowerOutput').submit();
            }
            else {
                showMessage(xhr.message, 'modal', 'fault');
            }

        }
        if (xhr.is_new == true) {
            $('#' + select_id).append('<option value=' + xhr.sources_id + ' selected>' + xhr.unom_source + '</option>');
            RefreshSelectByIdWithVal(select_id, xhr.sources_id);
            $("#YearImplementSchemeParam_Save").prop('disabled', false);

            $("#source_persp_id").val(xhr.sources_id);
            $("#source_p_id").val(xhr.sources_id);

        }
        else if (xhr.is_new_per == true) {
            $("#SourceHeatPowerOutput_Save").prop('disabled', false);
        }

        else {

            $("select[id=source_unom_list] option[value=" + $('#source_id').val() + "]").text(xhr.unom_source);
            RefreshSelectById('source_unom_list');
        }
    }
    else {
        if (xhr.message === undefined) {
            xhr.message = 'Ошибка сохранения данных'
        }
        is_success = 0;
        showMessage(xhr.message, 'modal', 'fault');
    }

    //if (is_success == 1)
    //    showMessage('Данные сохранены.', 'modal', 'success');

    hidePreloader();
};

function GetNumValueWithPoint(input_id) {
    return Number($('#' + input_id).val().replace(",", "."));
}

function SetNumValueWithComma(input_id, val) {
    $('#' + input_id).val(val.toFixed(2).toString().replace(".", ","));
}

function SetYearValueSum(input_id, s_fp) {
    let fp_1 = GetNumValueWithPoint(input_id + s_fp + '1');
    let fp_2 = GetNumValueWithPoint(input_id + s_fp + '2');
    let fp_3 = fp_1 + fp_2;
    $('#' + input_id + s_fp + '3').val(fp_3.toFixed(2).toString().replace(".", ","));
}

function SetYearValueSingleStageTariffUnit(input_id, s_fp, type) {
    let fp_1 = GetNumValueWithPoint(input_id + s_fp + '1');
    let fp_2 = GetNumValueWithPoint(input_id + s_fp + '2');
    let mul_1 = 0;
    let mul_2 = 0;

    if (type === 'tso') {
        mul_1 = GetNumValueWithPoint('useful_release_tso_salers_plan' + s_fp + 1);
        mul_2 = GetNumValueWithPoint('useful_release_tso_salers_plan' + s_fp + 2);
    }
    else if (type === 'budgetcons') {
        mul_1 = GetNumValueWithPoint('useful_release_buget_cons_plan' + s_fp + 1);
        mul_2 = GetNumValueWithPoint('useful_release_buget_cons_plan' + s_fp + 2);
    }    
    else if (type === 'public') {
        mul_1 = GetNumValueWithPoint('useful_release_public_plan' + s_fp + 1);
        mul_2 = GetNumValueWithPoint('useful_release_public_plan' + s_fp + 2);
    }      
    else if (type === 'other') {
        mul_1 = GetNumValueWithPoint('useful_release_other_cons_plan' + s_fp + 1);
        mul_2 = GetNumValueWithPoint('useful_release_other_cons_plan' + s_fp + 2);
    }      

    let fp_3 = (fp_1 * mul_1 + mul_2 * fp_2) / (mul_1 + mul_2);
    $('#' + input_id + s_fp + '3').val(fp_3.toFixed(2).toString().replace(".", ","));
}

function SetYearValueDoubleStageTariffUnit(input_id, s_fp, type) {
    let fp_1 = GetNumValueWithPoint(input_id + s_fp + '1');
    let fp_2 = GetNumValueWithPoint(input_id + s_fp + '2');
    let mul_1 = 0;
    let mul_2 = 0;
    let outer_1 = 0;
    let outer_2 = 0; 
    let ans_1 = 0;
    let ans_2 = 0;
    let ans_3 = 0;
    let ans_7_1 = 0;
    let ans_7_2 = 0;
    let ans_7_3 = 0;

    if (type === 'budgetcons_1' || type === 'budgetcons_2' || type === 'budgetcons_3') {
        mul_1 = GetNumValueWithPoint('heatenergy_doublerate_volume_budget_per_1');
        mul_2 = GetNumValueWithPoint('heatenergy_doublerate_volume_budget_per_2');
        if (type === 'budgetcons_3') {
            let cur_val_1_1 = GetNumValueWithPoint('convers_twostage_to_singlestage_budget_per_1');
            let cur_val_1_2 = GetNumValueWithPoint('convers_twostage_to_singlestage_budget_per_2');
            let cur_val_2_1 = GetNumValueWithPoint('heatenergy_doublerate_rate_budget_per_1');
            let cur_val_2_2 = GetNumValueWithPoint('heatenergy_doublerate_rate_budget_per_2');
            ans_1 = (cur_val_1_1 * mul_1 + mul_2 * cur_val_1_2) / (mul_1 + mul_2);
            ans_2 = (cur_val_2_1 * mul_1 + mul_2 * cur_val_2_2) / (mul_1 + mul_2);
            ans_3 = mul_1 + mul_2;
            $('#' + 'convers_twostage_to_singlestage_budget_per_3').val(ans_1.toFixed(2).toString().replace(".", ","));
            $('#' + 'heatenergy_doublerate_rate_budget_per_3').val(ans_2.toFixed(2).toString().replace(".", ","));
        }
        else{
            ans_3 = (fp_1 * mul_1 + mul_2 * fp_2) / (mul_1 + mul_2);
        }
    }
    else if (type === 'budgetcons_4' || type === 'budgetcons_5') {
        mul_1 = GetNumValueWithPoint('heatload_doublerate_value_budget_per_1');
        mul_2 = GetNumValueWithPoint('heatload_doublerate_value_budget_per_2');
        if (type === 'budgetcons_4') {
            ans_3 = (fp_1 * mul_1 + mul_2 * fp_2) / ((mul_1 + mul_2) / 2)
        }
        else {
            ans_3 = (mul_1 + mul_2) / 2;
        }
    }
    else if (type === 'budgetcons_6') {
        mul_1 = GetNumValueWithPoint('amount_grants_budget_per_1');
        mul_2 = GetNumValueWithPoint('amount_grants_budget_per_2');
        ans_3 = mul_1 + mul_2;
        outer_1 = GetNumValueWithPoint('twostage_useful_release_buget_cons_plan_per_1');
        outer_2 = GetNumValueWithPoint('twostage_useful_release_buget_cons_plan_per_2');
        ans_7_1 = mul_1 * 1000 / outer_1;
        ans_7_2 = mul_2 * 1000 / outer_2;
        ans_7_3 = (mul_1 + mul_2) * 1000 / (outer_1 + outer_2);
        $('#' + 'specific_value_grants_budget_per_1').val(ans_7_1.toFixed(2).toString().replace(".", ","));
        $('#' + 'specific_value_grants_budget_per_2').val(ans_7_2.toFixed(2).toString().replace(".", ","));
        $('#' + 'specific_value_grants_budget_per_3').val(ans_7_3.toFixed(2).toString().replace(".", ","));
    }

    if (type === 'public_1' || type === 'public_2' || type === 'public_3') {
        mul_1 = GetNumValueWithPoint('heatenergy_doublerate_volume_public_per_1');
        mul_2 = GetNumValueWithPoint('heatenergy_doublerate_volume_public_per_2');
        if (type === 'public_3') {
            let cur_val_1_1 = GetNumValueWithPoint('convers_twostage_to_singlestage_public_per_1');
            let cur_val_1_2 = GetNumValueWithPoint('convers_twostage_to_singlestage_public_per_2');
            let cur_val_2_1 = GetNumValueWithPoint('heatenergy_doublerate_rate_public_per_1');
            let cur_val_2_2 = GetNumValueWithPoint('heatenergy_doublerate_rate_public_per_2');
            ans_1 = (cur_val_1_1 * mul_1 + mul_2 * cur_val_1_2) / (mul_1 + mul_2);
            ans_2 = (cur_val_2_1 * mul_1 + mul_2 * cur_val_2_2) / (mul_1 + mul_2);
            ans_3 = mul_1 + mul_2;
            $('#' + 'convers_twostage_to_singlestage_public_per_3').val(ans_1.toFixed(2).toString().replace(".", ","));
            $('#' + 'heatenergy_doublerate_rate_public_per_3').val(ans_2.toFixed(2).toString().replace(".", ","));
        }
        else {
            ans_3 = (fp_1 * mul_1 + mul_2 * fp_2) / (mul_1 + mul_2);
        }
    }
    else if (type === 'public_4' || type === 'public_5') {
        mul_1 = GetNumValueWithPoint('heatload_doublerate_value_public_per_1');
        mul_2 = GetNumValueWithPoint('heatload_doublerate_value_public_per_2');
        if (type === 'public_4') {
            ans_3 = (fp_1 * mul_1 + mul_2 * fp_2) / ((mul_1 + mul_2) / 2)
        }
        else {
            ans_3 = (mul_1 + mul_2) / 2;
        }
    }
    else if (type === 'public_6') {
        mul_1 = GetNumValueWithPoint('amount_grants_public_per_1');
        mul_2 = GetNumValueWithPoint('amount_grants_public_per_2');
        ans_3 = mul_1 + mul_2;

        outer_1 = GetNumValueWithPoint('twostage_useful_release_public_plan_per_1');
        outer_2 = GetNumValueWithPoint('twostage_useful_release_public_plan_per_2');
        ans_7_1 = mul_1 * 1000 / outer_1;
        ans_7_2 = mul_2 * 1000 / outer_2;
        ans_7_3 = (mul_1 + mul_2) * 1000 / (outer_1 + outer_2);
        $('#' + 'specific_value_grants_public_per_1').val(ans_7_1.toFixed(2).toString().replace(".", ","));
        $('#' + 'specific_value_grants_public_per_2').val(ans_7_2.toFixed(2).toString().replace(".", ","));
        $('#' + 'specific_value_grants_public_per_3').val(ans_7_3.toFixed(2).toString().replace(".", ","));
    }

    if (type === 'other_1' || type === 'other_2' || type === 'other_3') {
        mul_1 = GetNumValueWithPoint('heatenergy_doublerate_volume_other_per_1');
        mul_2 = GetNumValueWithPoint('heatenergy_doublerate_volume_other_per_2');
        if (type === 'other_3') {
            let cur_val_1_1 = GetNumValueWithPoint('convers_twostage_to_singlestage_other_per_1');
            let cur_val_1_2 = GetNumValueWithPoint('convers_twostage_to_singlestage_other_per_2');
            let cur_val_2_1 = GetNumValueWithPoint('heatenergy_doublerate_rate_other_per_1');
            let cur_val_2_2 = GetNumValueWithPoint('heatenergy_doublerate_rate_other_per_2');
            ans_1 = (cur_val_1_1 * mul_1 + mul_2 * cur_val_1_2) / (mul_1 + mul_2);
            ans_2 = (cur_val_2_1 * mul_1 + mul_2 * cur_val_2_2) / (mul_1 + mul_2);
            ans_3 = mul_1 + mul_2;
            $('#' + 'convers_twostage_to_singlestage_other_per_3').val(ans_1.toFixed(2).toString().replace(".", ","));
            $('#' + 'heatenergy_doublerate_rate_other_per_3').val(ans_2.toFixed(2).toString().replace(".", ","));
        }
        else {
            ans_3 = (fp_1 * mul_1 + mul_2 * fp_2) / (mul_1 + mul_2);
        }
    }
    else if (type === 'other_4' || type === 'other_5') {
        mul_1 = GetNumValueWithPoint('heatload_doublerate_value_other_per_1');
        mul_2 = GetNumValueWithPoint('heatload_doublerate_value_other_per_2');
        if (type === 'other_4') {
            ans_3 = (fp_1 * mul_1 + mul_2 * fp_2) / ((mul_1 + mul_2) / 2)
        }
        else {
            ans_3 = (mul_1 + mul_2) / 2;
        }
    }
    else if (type === 'other_6') {
        mul_1 = GetNumValueWithPoint('amount_grants_other_per_1');
        mul_2 = GetNumValueWithPoint('amount_grants_other_per_2');
        ans_3 = mul_1 + mul_2;

        outer_1 = GetNumValueWithPoint('twostage_useful_release_other_cons_plan_per_1');
        outer_2 = GetNumValueWithPoint('twostage_useful_release_other_cons_plan_per_2');
        ans_7_1 = mul_1 * 1000 / outer_1;
        ans_7_2 = mul_2 * 1000 / outer_2;
        ans_7_3 = (mul_1 + mul_2) * 1000 / (outer_1 + outer_2);
        $('#' + 'specific_value_grants_other_per_1').val(ans_7_1.toFixed(2).toString().replace(".", ","));
        $('#' + 'specific_value_grants_other_per_2').val(ans_7_2.toFixed(2).toString().replace(".", ","));
        $('#' + 'specific_value_grants_other_per_3').val(ans_7_3.toFixed(2).toString().replace(".", ","));
    }

    $('#' + input_id + s_fp + '3').val(ans_3.toFixed(2).toString().replace(".", ","));  
}

function SetYearValueSinglerateTariffSteam(type, parameter) {
    let param_1 = 'useful_release_ownprod_needs_p' + parameter +'_per_';
    let param_2 = 'useful_release_household_needs_p' + parameter + '_per_';
    let param_3 = 'volume_he_tso_p' + parameter + '_per_';
    let param_4 = 'volume_he_budgetcons_p' + parameter + '_per_';
    let param_5 = 'volume_he_public_p' + parameter + '_per_';
    let param_6 = 'volume_he_other_p' + parameter + '_per_';
    if (type === 'econom_justified') {
        CalculateEconomJustified(parameter);
    }
    else if (type === 'weighted_invest') {
        CalculateWeightedTariff(parameter);
    }
    else if (type === 'invest_component') {
        CalculateInvestComponent(parameter);
    } 
    else if (type === 'ownprod_needs' || type === 'household_needs') {
        CalculateNeeds(type, parameter);
        CalculateHeatEnergyVolumeSteamAllYear(param_1, param_2, param_3, param_4, param_5, param_6, parameter);
    }
    else if (type === 'tariff_tso') {
        CalculateHeatEnergyTariff('tso', parameter);
    }
    else if (type === 'volume_he_tso') {
        CalculateHeatEnergyVolume('tso', parameter);
        CalculateHeatEnergyVolumeSteamAllYear(param_1, param_2, param_3, param_4, param_5, param_6, parameter);
        CalculateEconomJustified(parameter);
        CalculateHeatEnergyTariff('tso', parameter);
    }
    else if (type === 'tariff_budgetcons') {
        CalculateHeatEnergyTariff('budgetcons', parameter);
    }
    else if (type === 'volume_he_budgetcons') {
        CalculateHeatEnergyVolume('budgetcons', parameter);
        CalculateHeatEnergyVolumeSteamAllYear(param_1, param_2, param_3, param_4, param_5, param_6, parameter);
        CalculateEconomJustified(parameter);
        CalculateWeightedTariff(parameter);
        CalculateInvestComponent(parameter);
        CalculateHeatEnergyTariff('budgetcons', parameter);
    }
    else if (type === 'tariff_public') {
        CalculateHeatEnergyTariff('public', parameter);
    }
    else if (type === 'volume_he_public') {
        CalculateHeatEnergyVolume('public', parameter);
        CalculateHeatEnergyVolumeSteamAllYear(param_1, param_2, param_3, param_4, param_5, param_6, parameter);
        CalculateEconomJustified(parameter);
        CalculateWeightedTariff(parameter);
        CalculateInvestComponent(parameter);
        CalculateHeatEnergyTariff('public', parameter);
    }
    else if (type === 'tariff_other') {
        CalculateHeatEnergyTariff('other', parameter);
    }
    else if (type === 'volume_he_other') {
        CalculateHeatEnergyVolume('other', parameter);
        CalculateHeatEnergyVolumeSteamAllYear(param_1, param_2, param_3, param_4, param_5, param_6, parameter);
        CalculateEconomJustified(parameter);
        CalculateWeightedTariff(parameter);
        CalculateInvestComponent(parameter);
        CalculateHeatEnergyTariff('other', parameter);
    }
}

function CalculateHeatEnergyVolumeSteam(ownprod, household, volume_he_tso, volume_he_budget, volume_he_public, volume_he_other, period, steamParam) {
    let curr_ans = GetNumValueWithPoint(ownprod + period) + GetNumValueWithPoint(household + period) + GetNumValueWithPoint(volume_he_tso + period) +
        GetNumValueWithPoint(volume_he_budget + period) + GetNumValueWithPoint(volume_he_public + period) + GetNumValueWithPoint(volume_he_other + period);
    $('#' + 'volume_he_p' + steamParam + '_per_' + period).val(curr_ans.toFixed(2).toString().replace(".", ","));
    return curr_ans;
}

function CalculateHeatEnergyVolumeSteamAllYear(ownprod, household, volume_he_tso, volume_he_budget, volume_he_public, volume_he_other, steamParam) {
    let curr_ans_1 = CalculateHeatEnergyVolumeSteam(ownprod, household, volume_he_tso, volume_he_budget, volume_he_public, volume_he_other, 1, steamParam);
    let curr_ans_2 = CalculateHeatEnergyVolumeSteam(ownprod, household, volume_he_tso, volume_he_budget, volume_he_public, volume_he_other, 2, steamParam);
    let curr_ans = curr_ans_1 + curr_ans_2;
    $('#' + 'volume_he_p' + steamParam + '_per_3').val(curr_ans.toFixed(2).toString().replace(".", ","));
}

function CalculateEconomJustified(steamParam) {
    let ans_1_1 = GetNumValueWithPoint('volume_he_tso_p' + steamParam + '_per_1');
    let ans_1_2 = GetNumValueWithPoint('volume_he_budgetcons_p' + steamParam + '_per_1');
    let ans_1_3 = GetNumValueWithPoint('volume_he_public_p' + steamParam + '_per_1');
    let ans_1_4 = GetNumValueWithPoint('volume_he_other_p' + steamParam + '_per_1');

    let ans_2_1 = GetNumValueWithPoint('volume_he_tso_p' + steamParam + '_per_2');
    let ans_2_2 = GetNumValueWithPoint('volume_he_budgetcons_p' + steamParam + '_per_2');
    let ans_2_3 = GetNumValueWithPoint('volume_he_public_p' + steamParam + '_per_2');
    let ans_2_4 = GetNumValueWithPoint('volume_he_other_p' + steamParam + '_per_2');

    let outer_1 = GetNumValueWithPoint('tariff_econom_justified_p' + steamParam + '_per_1');
    let outer_2 = GetNumValueWithPoint('tariff_econom_justified_p' + steamParam + '_per_2');
    let ans_finally = (outer_1 * (ans_1_1 + ans_1_2 + ans_1_3 + ans_1_4) + outer_2 * (ans_2_1 + ans_2_2 + ans_2_3 + ans_2_4)) / (ans_1_1 + ans_1_2 + ans_1_3 + ans_1_4 + ans_2_1 + ans_2_2 + ans_2_3 + ans_2_4);
    $('#' + 'tariff_econom_justified_p' + steamParam + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateWeightedTariff(steamParam) {
    let ans_1_2 = GetNumValueWithPoint('volume_he_budgetcons_p' + steamParam + '_per_1');
    let ans_1_3 = GetNumValueWithPoint('volume_he_public_p' + steamParam + '_per_1');
    let ans_1_4 = GetNumValueWithPoint('volume_he_other_p' + steamParam + '_per_1');

    let ans_2_2 = GetNumValueWithPoint('volume_he_budgetcons_p' + steamParam + '_per_2');
    let ans_2_3 = GetNumValueWithPoint('volume_he_public_p' + steamParam + '_per_2');
    let ans_2_4 = GetNumValueWithPoint('volume_he_other_p' + steamParam + '_per_2');

    let outer_1 = GetNumValueWithPoint('tariff_weighted_invest_p' + steamParam + '_per_1');
    let outer_2 = GetNumValueWithPoint('tariff_weighted_invest_p' + steamParam + '_per_2');
    let ans_finally = (outer_1 * (ans_1_2 + ans_1_3 + ans_1_4) + outer_2 * (ans_2_2 + ans_2_3 + ans_2_4)) / (ans_1_2 + ans_1_3 + ans_1_4 + ans_2_2 + ans_2_3 + ans_2_4);
    $('#' + 'tariff_weighted_invest_p' + steamParam + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateInvestComponent(steamParam) {
    let ans_1_2 = GetNumValueWithPoint('volume_he_budgetcons_p' + steamParam + '_per_1');
    let ans_1_3 = GetNumValueWithPoint('volume_he_public_p' + steamParam + '_per_1');
    let ans_1_4 = GetNumValueWithPoint('volume_he_other_p' + steamParam + '_per_1');

    let ans_2_2 = GetNumValueWithPoint('volume_he_budgetcons_p' + steamParam + '_per_2');
    let ans_2_3 = GetNumValueWithPoint('volume_he_public_p' + steamParam + '_per_2');
    let ans_2_4 = GetNumValueWithPoint('volume_he_other_p' + steamParam + '_per_2');

    let outer_1 = GetNumValueWithPoint('invest_component_p' + steamParam + '_per_1');
    let outer_2 = GetNumValueWithPoint('invest_component_p' + steamParam + '_per_2');
    let ans_finally = (outer_1 * (ans_1_2 + ans_1_3 + ans_1_4) + outer_2 * (ans_2_2 + ans_2_3 + ans_2_4)) / (ans_1_2 + ans_1_3 + ans_1_4 + ans_2_2 + ans_2_3 + ans_2_4);
    $('#' + 'invest_component_p' + steamParam + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateHeatEnergyTariff(consumerType, steamParam) {
    let outer_1 = GetNumValueWithPoint('tariff_' + consumerType +'_p' + steamParam + '_per_1');
    let outer_2 = GetNumValueWithPoint('tariff_' + consumerType + '_p' + steamParam + '_per_2');
    let ans_1_1 = GetNumValueWithPoint('volume_he_' + consumerType + '_p' + steamParam + '_per_1');
    let ans_2_1 = GetNumValueWithPoint('volume_he_' + consumerType + '_p' + steamParam + '_per_2');
    let ans_finally = (outer_1 * ans_1_1 + outer_2 * ans_2_1) / (ans_1_1 + ans_2_1);
    $('#' + 'tariff_' + consumerType + '_p' + steamParam + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateHeatEnergyVolume(consumerType, steamParam) {
    let outer_1 = GetNumValueWithPoint('volume_he_' + consumerType + '_p' + steamParam + '_per_1');
    let outer_2 = GetNumValueWithPoint('volume_he_' + consumerType + '_p' + steamParam + '_per_2');
    let ans_finally = outer_1 + outer_2;
    $('#' + 'volume_he_' + consumerType + '_p' + steamParam + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateNeeds(type, steamParam) {
    let outer_1 = GetNumValueWithPoint('useful_release_' + type + '_p' + steamParam + '_per_1');
    let outer_2 = GetNumValueWithPoint('useful_release_' + type + '_p' + steamParam + '_per_2');
    let ans_finally = outer_1 + outer_2;
    $('#' + 'useful_release_' + type + '_p' + steamParam + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateEmoAmountPreferenceTariff(consumerType, multyplexType) {
    let outer_1 = GetNumValueWithPoint('tariff_' + consumerType + '_per_1');
    let outer_2 = GetNumValueWithPoint('tariff_' + consumerType + '_per_2');
    let mul_1 = GetNumValueWithPoint('eto_amount_preference_twostage_useful_release_' + multyplexType + '_cons_plan' + '_per_1');
    let mul_2 = GetNumValueWithPoint('eto_amount_preference_twostage_useful_release_' + multyplexType + '_cons_plan' + '_per_2');
    let ans_finally = (outer_1 * mul_1 + outer_2 * mul_2) / (mul_1 + mul_2);
    $('#' + 'tariff_' + consumerType + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function SetYearHeatCarrierSteamHotWater(consumerType) {
    if (consumerType === 'hot_water_heat_carrier_tariff' || consumerType === 'steam_heat_carrier_tariff') {
        CalculateSteamHotWater(consumerType);
    }
    else if (consumerType === 'hot_water_tariff') {
        CalculateSingleComponentTariffHotWater(consumerType);
        CalculateSpecificConsumptionAllYear(consumerType);
        CalculateSpecificConsumption('hot_water_tariff', 1);
        CalculateSpecificConsumption('hot_water_tariff', 2);
    }
    else if (consumerType === 'heat_carrier_component') {
        CalculateSingleComponentTariffHotWater(consumerType);
    }
    else if (consumerType === 'heat_energy_component') {
        CalculateHeatEnergyComponent();
    }
}

function CalculateSteamHotWater(consumerType) {
    let outer_1 = GetNumValueWithPoint(consumerType + '_per_1');
    let outer_2 = GetNumValueWithPoint(consumerType + '_per_2');
    let mul_1 = GetNumValueWithPoint('he_tariff_steam_hotwater_volume_buy_heat_carrier_plan_per_1');
    let mul_2 = GetNumValueWithPoint('he_tariff_steam_hotwater_volume_buy_heat_carrier_plan_per_2');
    let ans_finally = (outer_1 * mul_1 + outer_2 * mul_2) / (mul_1 + mul_2);
    $('#' + consumerType + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateSingleComponentTariffHotWater(consumerType) {
    let outer_1 = GetNumValueWithPoint(consumerType + '_per_1');
    let outer_2 = GetNumValueWithPoint(consumerType + '_per_2');
    let ans_finally = (outer_1 + outer_2) / 2;
    $('#' + consumerType + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
    return ans_finally;
}

function CalculateSpecificConsumptionAllYear(consumerType) {
    let outer_1 = CalculateSingleComponentTariffHotWater(consumerType);
    let subdiv = GetNumValueWithPoint('he_tariff_steam_hotwater_volume_buy_coldwater_plan_subdivider');
    let maindiv = GetNumValueWithPoint('he_tariff_steam_hotwater_volume_buy_coldwater_plan_divider_main');
    let ans_finally = (outer_1 - subdiv) / maindiv;
    $('#' + 'heat_energy_specific_consumption_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateSpecificConsumption(consumerType, period) {
    let outer_1 = GetNumValueWithPoint(consumerType + '_per_' + period);
    let outer_2 = GetNumValueWithPoint('he_tariff_steam_hotwater_price_coldwater_plan_div_per_' + period);
    let div = GetNumValueWithPoint('he_tariff_steam_hotwater_tariff_other_per_' + period);
    let ans_finally = (outer_1 - outer_2) / div;
    $('#' + 'heat_energy_specific_consumption_per_' + period).val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateHeatEnergyComponent() {
    let outer_1 = GetNumValueWithPoint('heat_energy_component_per_1');
    let outer_2 = GetNumValueWithPoint('heat_energy_component_per_2');
    let mul_1_1 = GetNumValueWithPoint('he_tariff_steam_hotwater_useful_release_buget_cons_plan_per_1');
    let mul_1_2 = GetNumValueWithPoint('he_tariff_steam_hotwater_useful_release_public_cons_plan_per_1');
    let mul_1_3 = GetNumValueWithPoint('he_tariff_steam_hotwater_useful_release_other_cons_plan_per_1');
    let mul_2_1 = GetNumValueWithPoint('he_tariff_steam_hotwater_useful_release_buget_cons_plan_per_2');
    let mul_2_2 = GetNumValueWithPoint('he_tariff_steam_hotwater_useful_release_public_cons_plan_per_2');
    let mul_2_3 = GetNumValueWithPoint('he_tariff_steam_hotwater_useful_release_other_cons_plan_per_2');
    let ans_finally = (outer_1 * (mul_1_1 + mul_1_2 + mul_1_3) + outer_2 * (mul_2_1 + mul_2_2 + mul_2_3)) / (mul_1_1 + mul_1_2 + mul_1_3 + mul_2_1 + mul_2_2 + mul_2_3);
    $('#' + 'heat_energy_component_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function SetHeatEnergyTransfer(consumerType) {
    if (consumerType === 'tariff_weighted_invest' || consumerType === 'invest_component') {
        CalculateHeatEnergyTransferTariffWeightedInvest(consumerType);
    }
    else if (consumerType === 'household_needs_tariff') {
        CalculateOrganizationTransfer();
    }
    else if (consumerType === 'tariff_tso') {
        CalculateTsoBudgetPublicOther(consumerType, 'tso_salers')
    }
    else if (consumerType === 'tariff_budgetcons') {
        CalculateTsoBudgetPublicOther(consumerType, 'buget_cons')
    }
    else if (consumerType === 'tariff_public') {
        CalculateTsoBudgetPublicOther(consumerType, 'public')
    }
    else if (consumerType === 'tariff_other') {
        CalculateTsoBudgetPublicOther(consumerType, 'other_cons')
    }
}

function CalculateHeatEnergyTransferTariffWeightedInvest(type) {
    let outer_1 = GetNumValueWithPoint('he_transfer_' + type + '_per_1');
    let outer_2 = GetNumValueWithPoint('he_transfer_' + type + '_per_2');
    let mul_1_1 = GetNumValueWithPoint('he_transfer_useful_release_buget_cons_plan_per_1');
    let mul_1_2 = GetNumValueWithPoint('he_transfer_useful_release_public_plan_per_1');
    let mul_1_3 = GetNumValueWithPoint('he_transfer_useful_release_other_cons_plan_per_1');
    let mul_2_1 = GetNumValueWithPoint('he_transfer_useful_release_buget_cons_plan_per_2');
    let mul_2_2 = GetNumValueWithPoint('he_transfer_useful_release_public_plan_per_2');
    let mul_2_3 = GetNumValueWithPoint('he_transfer_useful_release_other_cons_plan_per_2');
    let ans_finally = (outer_1 * (mul_1_1 + mul_1_2 + mul_1_3) + outer_2 * (mul_2_1 + mul_2_2 + mul_2_3)) / (mul_1_1 + mul_1_2 + mul_1_3 + mul_2_1 + mul_2_2 + mul_2_3);
    $('#' + 'he_transfer_' + type + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateOrganizationTransfer() {
    let outer_1 = GetNumValueWithPoint('he_transfer_household_needs_tariff_per_1');
    let outer_2 = GetNumValueWithPoint('he_transfer_household_needs_tariff_per_2');
    let mul_1_1 = GetNumValueWithPoint('he_transfer_useful_release_ownprod_plan_per_1');
    let mul_1_2 = GetNumValueWithPoint('he_transfer_useful_release_household_needs_plan_per_1');
    let mul_2_1 = GetNumValueWithPoint('he_transfer_useful_release_ownprod_plan_per_2');
    let mul_2_2 = GetNumValueWithPoint('he_transfer_useful_release_household_needs_plan_per_2');
    let ans_finally = (outer_1 * (mul_1_1 + mul_1_2) + outer_2 * (mul_2_1 + mul_2_2)) / (mul_1_1 + mul_1_2 + mul_2_1 + mul_2_2);
    $('#' + 'he_transfer_household_needs_tariff_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateTsoBudgetPublicOther(consumerType, argumentType) {
    let outer_1 = GetNumValueWithPoint('he_transfer_' + consumerType + '_per_1');
    let outer_2 = GetNumValueWithPoint('he_transfer_' + consumerType + '_per_2');
    let mul_1 = GetNumValueWithPoint('he_transfer_useful_release_' + argumentType + '_plan_per_1');
    let mul_2 = GetNumValueWithPoint('he_transfer_useful_release_' + argumentType + '_plan_per_2');
    let ans_finally = (outer_1 * mul_1 + mul_2 * outer_2) / (mul_1 + mul_2);
    $('#' + 'he_transfer_' + consumerType + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function SetYearValueMainTariffUnit(type) {
    let outer_1 = GetNumValueWithPoint('heat_energy_tariff_water_' + type + '_per_1');
    let outer_2 = GetNumValueWithPoint('heat_energy_tariff_water_' + type + '_per_2');
    let mul_1_1 = GetNumValueWithPoint('heat_energy_tariff_water_useful_release_tso_salers_plan_per_1');
    let mul_1_2 = GetNumValueWithPoint('heat_energy_tariff_water_useful_release_buget_cons_plan_per_1');
    let mul_1_3 = GetNumValueWithPoint('heat_energy_tariff_water_useful_release_public_plan_per_1');
    let mul_1_4 = GetNumValueWithPoint('heat_energy_tariff_water_useful_release_other_cons_plan_per_1');
    let mul_2_1 = GetNumValueWithPoint('heat_energy_tariff_water_useful_release_tso_salers_plan_per_2');
    let mul_2_2 = GetNumValueWithPoint('heat_energy_tariff_water_useful_release_buget_cons_plan_per_2');
    let mul_2_3 = GetNumValueWithPoint('heat_energy_tariff_water_useful_release_public_plan_per_2');
    let mul_2_4 = GetNumValueWithPoint('heat_energy_tariff_water_useful_release_other_cons_plan_per_2');
    let ans_finally;

    if (type === 'econom_justified') {
        ans_finally = (outer_1 * (mul_1_1 + mul_1_2 + mul_1_3 + mul_1_4) + outer_2 * (mul_2_1 + mul_2_2 + mul_2_3 + mul_2_4)) / (mul_1_1 + mul_1_2 + mul_1_3 + mul_1_4 + mul_2_1 + mul_2_2 + mul_2_3 + mul_2_4);
    }
    else if (type === 'invest_component') {
        ans_finally = (outer_1 * (mul_1_2 + mul_1_3 + mul_1_4) + outer_2 * (mul_2_2 + mul_2_3 + mul_2_4)) / (mul_1_2 + mul_1_3 + + mul_1_4 + mul_2_2 + mul_2_3 + mul_2_4);
    }
    else if (type === 'weighted_invest') {
        let mux_1_1 = GetNumValueWithPoint('heat_energy_tariff_water_convers_doublerate_to_singlerate_budjet_per_1');
        let multy_1_1 = GetNumValueWithPoint('heat_energy_tariff_water_volume_heat_energy_budjet_per_1');

        let mux_1_2 = GetNumValueWithPoint('heat_energy_tariff_water_convers_doublerate_to_singlerate_public_per_1');
        let multy_1_2 = GetNumValueWithPoint('heat_energy_tariff_water_volume_heat_energy_public_per_1');

        let mux_1_3 = GetNumValueWithPoint('heat_energy_tariff_water_convers_doublerate_to_singlerate_other_per_1');
        let multy_1_3 = GetNumValueWithPoint('heat_energy_tariff_water_volume_heat_energy_other_per_1');

        let mux_2_1 = GetNumValueWithPoint('heat_energy_tariff_water_convers_doublerate_to_singlerate_budjet_per_2');
        let multy_2_1 = GetNumValueWithPoint('heat_energy_tariff_water_volume_heat_energy_budjet_per_2');

        let mux_2_2 = GetNumValueWithPoint('heat_energy_tariff_water_convers_doublerate_to_singlerate_public_per_2');
        let multy_2_2 = GetNumValueWithPoint('heat_energy_tariff_water_volume_heat_energy_public_per_2');

        let mux_2_3 = GetNumValueWithPoint('heat_energy_tariff_water_convers_doublerate_to_singlerate_other_per_2');
        let multy_2_3 = GetNumValueWithPoint('heat_energy_tariff_water_volume_heat_energy_other_per_2');

        ans_finally = (outer_1 * (mul_1_2 + mul_1_3 + mul_1_4 + mux_1_1 * multy_1_1 + mux_1_2 * multy_1_2 + mux_1_3 * multy_1_3)
            + outer_2 * (mul_2_2 + mul_2_3 + mul_2_4 + mux_2_1 * multy_2_1 + mux_2_2 * multy_2_2 + mux_2_3 * multy_2_3)) / (mul_1_2 + mul_1_3 + mul_1_4 + mul_2_2 + mul_2_3 + mul_2_4);
    }
    $('#' + 'heat_energy_tariff_water_' + type + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}

function CalculateCrossSubsidizationAmount(type) {
    let outer_1 = GetNumValueWithPoint('cross_subsidization_amount_' + type + '_per_1');
    let outer_2 = GetNumValueWithPoint('cross_subsidization_amount_' + type + '_per_2');
    let ans_finally = outer_1 + outer_2;
    $('#' + 'cross_subsidization_amount_' + type + '_per_3').val(ans_finally.toFixed(2).toString().replace(".", ","));
}