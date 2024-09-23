var openRowsTs;
var openRowsSrNotApproved;
var openRowsSrApproved;

function parseDate(input, format) {
    format = format || 'yyyy-mm-dd'; // default format
    var parts = input.match(/(\d+)/g),
        i = 0, fmt = {};
    // extract date-part indexes from the format
    format.replace(/(yyyy|dd|mm)/g, function (part) { fmt[part] = i++; });

    return new Date(parts[fmt['yyyy']], parts[fmt['mm']] - 1, parts[fmt['dd']]);
}

function nextWeek(projId) {
    var date = parseDate($("#date-arrow").val(), 'yyyy-mm-dd');
    date.setDate(date.getDate() + 7);
    var day = ("0" + date.getDate()).slice(-2);
    var month = ("0" + (date.getMonth() + 1)).slice(-2);
    date = date.getFullYear() + "-" + (month) + "-" + (day);

    $("#date-arrow").val(date);
    weekChange(date, projId);
    $("#ts-date").val("");
}

function beforeWeek(projId) {
    var date = parseDate($("#date-arrow").val(), 'yyyy-mm-dd');
    date.setDate(date.getDate() - 7);
    var day = ("0" + date.getDate()).slice(-2);
    var month = ("0" + (date.getMonth() + 1)).slice(-2);
    date = date.getFullYear() + "-" + (month) + "-" + (day);

    $("#date-arrow").val(date);
    weekChange(date, projId);
    $("#ts-date").val("");
}

function currentWeek(projId) {
    var date = new Date();
    var day = ("0" + date.getDate()).slice(-2);
    var month = ("0" + (date.getMonth() + 1)).slice(-2);
    date = date.getFullYear() + "-" + (month) + "-" + (day);

    $("#date-arrow").val(date);
    weekChange(date, projId);
    $("#ts-date").val("");
};

function caseType(type) {
    if (type === "ByUser") {
        return "/Projects/TsByUser";
    } else if (type === "ByProjects") {
        return "/Projects/TsByProjects";
    } else if (type === "ByUsers") {
        return "/Projects/TsByUsers";
    } else if (type === "ByProject") {
        return "/Projects/TsByProject";
    }
};

function weekChange(date, projId) {

    var type = $("#ts-type").val();
    var url = caseType(type);
    var searchText = $('#search').val();
    if (type != 'ByUser') {
        var openRows = saveWichRowOpen();
    }
    if (type === 'ByProjects') {
        var isNotApprovedPeriods = $("#notApproveTimeCheckbox")[0].checked;
        var isInsertPeriods = $("#approveTimeCheckbox")[0].checked;
    }
    $.ajax({
        type: 'POST',
        url: url,
        data: {
            date: date,
            projId: projId
        },
        dataType: 'html'
    }).done(function (result) {
        var div = $('#crm-tasks-table');
        div.html(result);
        setSubtotals();

        $(".chat-sidebar").removeClass("chat-on");
        $(".page-content").removeClass("righter");
        $("#topcontrol").removeClass("chat");

        if (type !== 'ByUser') {
            var headRow = $('.head-row');
            var headRowLevel2 = $('.head-row-level-2');
            if (type !== 'ByProject') {
                AddWeekHoursToProjectRow(headRow);
                AddWeekHoursToEsRow(headRowLevel2);
            }
            OpenRows(openRows);
        } else {
            cellsWithoutTimeRender();
        }
        if (searchText.length !== 0) {
            $('#search').val(searchText);  
            SearchTs(type);
        }
        if (type === 'ByProjects') {
            if (isNotApprovedPeriods === true) {
                $("#notApproveTimeCheckbox").prop('checked', true);
                $("#filterButton").removeClass("btn-default");
                $("#filterButton").addClass("btn-warning");
                SearchNotApprovedPeriods($('#search').val().toLowerCase());
            } else if (isInsertPeriods === true) {
                $("#approveTimeCheckbox").prop('checked', true);
                $("#filterButton").removeClass("btn-default");
                $("#filterButton").addClass("btn-warning");
                SearchInsertPeriods($('#search').val().toLowerCase());
            }
        }
    });
};


function popupToggle(type, estId) {
    switch (type) {
    case 1:
        $("#message-box-success").toggle();
        break;
    case -1:
        $("#message-box-danger").toggle();
        break;
    case 0:
        var btnApprove = $(".btn-approve[data-est-id='" + estId + "']");

        if (btnApprove.prop("disabled")) {
            $("#message-box-danger").toggle();
        }
        else {
            $("#message-box-success").toggle();
        }
        break;
    }
};

function tsTabChange(el, url, type) {
    $(".tab li").removeClass("active");
    el.addClass("active");

    $.ajax({
        type: 'POST',
        url: url,
        data: {
            date: $("#date-arrow").val()
        },
        dataType: 'html'
    }).done(function (result) {
        var div = $('#crm-tasks-table');
        div.html(result);
        $("#ts-type").val(type);
        setSubtotals();

        $(".chat-sidebar").removeClass("chat-on");
        $(".page-content").removeClass("righter");
        $("#topcontrol").removeClass("chat");

        if (type != 'ByUser') {

            AddWeekHoursToProjectRow($('.head-row'));
            AddWeekHoursToEsRow($('.head-row-level-2'));
        } else {
            cellsWithoutTimeRender();
        }

    });
};

function tsTabsClick() {
    $("#by-projects").on("click", function () {
        tsTabChange($(this), "/Projects/TsByProjects", "ByProjects");
    });
    $("#by-user").on("click", function () {
        tsTabChange($(this), "/Projects/TsByUser", "ByUser");
    });
    $("#by-users").on("click", function () {
        tsTabChange($(this), "/Projects/TsByUsers", "ByUsers");
    });
    $("#by-project").on("click", function () {
        tsTabChange($(this), "/Projects/TsByProject", "ByProject");
    });
};


function tsRowsData(row) {
    var totals = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
    var dataRows = $(row).nextUntil(".subtotal");

    dataRows.each(function (i) {
        $(this).find('.ts-total').each(function (i) {
            var stringValue = $(this).html();
            if (stringValue !== '') {                
                var splitHours = stringValue.split(':');
                if (splitHours.length === 1) {
                    totals[i] += parseInt(splitHours[0]);
                } else {
                    var totalMin = Number(splitHours[0] * 60) + Number(splitHours[1]);
                    totals[i] += parseInt(totalMin); 
                }
            } else {
                totals[i] += 0;
            }
        });
    });
    return totals;
};

function setSubtotals() {

    $(".head-row").each(function () {
        var totals = tsRowsData($(this));

        var subTotalRow = $(this).nextUntil(".subtotal").last().next(".subtotal");
        subTotalRow.find(".ts-total").each(function (i) {
            if (i === 10) {
                $(this).html(totals[i]);
            } else {
                if (totals[i] !== 0) {
                    var hours = Math.floor(totals[i] / 60);
                    var minutes = totals[i] - Math.floor(totals[i] / 60) * 60;
                    if (minutes < 10) {
                        $(this).html(hours + ':' + '0' + minutes);
                    } else {
                        $(this).html(hours + ':' + minutes);
                    }
                } else {
                    $(this).html('0:00');
                } 
            }
        });
    });
};


function btnApproveClick() {
    $(document).on("click", ".btn-approve, .btn-disable", function () {
    
        var date = $('#date-arrow').val();
        var userId = $(this).data('userId');
        var estId = $(this).data("estId");
        var type = $(this).data('type');

        approved(userId, estId, date, type);
    });
};

function approved(userId, estId, date, type) {
    $(".btn-confirm-yes").prop("disabled", false);
    $("#cancel-desc").off("keyup");

    $("#message-box-success").removeClass('message-box-neutral').addClass('message-box-success');
    $("#message-box-danger").removeClass('message-box-neutral').addClass('message-box-danger');

    $("#confirm-by-leader").val('');
    $("#cancel-desc").val('');

    popupToggle(type, estId);
    if (type === -1) {
        $("#cancel-desc").css("display", "block");
        $("#cancel-tytle").text("Отклонить часы");
        $("#cancel-content").text("Вы уверены, что хотите не принять часы по данному проекту? Уведомление об этом будет отправлено сотруднику на почту.");
        $(".btn-confirm-yes").prop("disabled", true);

        $("#cancel-desc").on("keyup", function() {
           if ($(this).val() !== '') {
               $(".btn-confirm-yes").prop("disabled", false);
           } else {
               $(".btn-confirm-yes").prop("disabled", true);
           }
        });

    } else if (type == 0) {
        $("#message-box-success").removeClass('message-box-success').addClass('message-box-neutral');
        $("#message-box-danger").removeClass('message-box-danger').addClass('message-box-neutral');

        $("#cancel-desc").css("display", "none");
        $("#div-leader-confirm").css("display", "none");
        $("#success-tytle").text("Снять отмену часов");
        $("#cancel-tytle").text("Снять подтверждение часов");
        $("#cancel-content").text("Вы уверены, что хотите отменить подтверждение часов по данному проекту?");
        $("#success-content").text("Вы уверены, что хотите снять отмену часов по данному проекту?");
    }
    else {

        $('.tp').timepicker({ minuteStep: 5, maxHours: 1000, defaultTime: false, showMeridian: false });
        $("#div-leader-confirm").css("display", "block");
        $("#success-tytle").text("Принять часы");
        $("#success-content").text("Вы уверены, что хотите подтвердить часы по данному проекту?");
        var row = $("#row-" + userId + "-" + estId);
        var totalByWeek = row.find("td[name='weekSummaru']").text();
        $("#confirm-by-leader").val(totalByWeek);
        $("#total-minutes-by-week").text(totalByWeek);
    }
    $(".btn-confirm-yes").click(function () {
        var timeString = $("#confirm-by-leader").val();
        var confirmByLeader = 0;
        if (timeString !== '') {
            var split = timeString.split(':');
            confirmByLeader = (Number(split[0]) * 60) + Number(split[1]);
        }
        $.ajax({
            type: 'POST',
            url: '/Projects/ApprovedPeriodToggle',
            data: {
                estId: estId,
                userId: userId,
                date: date,
                type: type, // 1 - подтвердить, 0 - отменить, -1 - отклонить.
                confirmByLeader: confirmByLeader
            },
            dataType: 'json',
            success: function (response) {
                if (response.success) {
                    var upd = true;
                    var row = $("#row-" + userId + "-" + estId);

                    var btnApprove = $(".btn-approve[data-est-id='" + estId + "'][data-user-id='" + userId + "']");
                    var btnCancel = $(".btn-disable[data-est-id='" + estId + "'][data-user-id='" + userId + "']");


                    btnApprove.data('type', 1);

                    if (type === 1) {
                        btnApprove.prop('disabled', true);
                        btnCancel.prop('disabled', false);
                        row.addClass("success");

                        btnCancel.data('type', 0);
                    } else {
                        btnCancel.data('type', -1);

                        if (Number(type) == -1) {
                            btnApprove.data('type', 0);
                            upd = false;
                            btnCancel.prop('disabled', true);
                            row.addClass("canceled");
                        }
                        else {
                            if (btnCancel.prop("disabled")) {
                                upd = false;
                                btnCancel.prop('disabled', false);
                                row.removeClass("canceled");
                            }
                            else {
                                btnApprove.prop('disabled', false);
                                row.removeClass("success");
                            }
                        }
                    }
                    if (upd) {
                        UpdateFactHoursInOldBase(userId, estId, date, type);
                    }
                    if (type == -1) {
                        var desc = $("#cancel-desc").val();
                        CancelApproved(desc, userId, estId, date);
                        var a = row.find("td:last");
                        var b = row.find("td:last div");
                        row.find("td:last div").append('<i class="fa fa-comments" style="cursor: pointer"></i>');
                    } else {
                        row.find("td:last div i").remove();
                    }

                    row.find("td.approve-hours").text(timeString);
                    var stringConfirm = response.hours;
                    var realmin = response.hours % 60;
                    var hours = Math.floor(response.hours / 60);
                    stringConfirm = hours + ":";
                    if (realmin < 10) {
                        stringConfirm = stringConfirm + "0";
                    }
                    stringConfirm = stringConfirm + realmin;
                    row.find("td.projApprove").text(stringConfirm);
                    cellsWithoutTimeRender();
                } else {
                    alert("Не удалось подтвердить/отменить период.");
                }
            }
        });
        popupToggle(type, estId);
        $(".btn-confirm-no").off("click");
        $(".btn-confirm-yes").off("click");
    });


    $(".btn-confirm-no").click(function () {
        popupToggle(type, estId);
        $(".btn-confirm-no").off("click");
        $(".btn-confirm-yes").off("click");
    });
};





function cellsWithoutTimeRender() {
    var cell;
    var type = $("#ts-type").val();
    if (type == 'ByUser') {
        var index = 0;
        $('.subtotal td').each(function () {
            if ($(this).hasClass('dayOfWeek')) {
                if ($(this).html() == '' || $(this).html() == '0:00') {
                    $('[id^=row-]').each(function () {

                        if (!$(this).hasClass('success') && !$(this).hasClass('canceled')) {
                            cell = $(this).find('td').eq(index);
                            cell.css("background-color", "antiquewhite");
                        } else {
                            cell = $(this).find('td').eq(index);
                            cell.css("background-color", "");
                        };
                    });
                } else {
                    $('[id^=row-]').each(function () {
                        cell = $(this).find('td').eq(index);
                        cell.css("background-color", "");
                    });
                };
            }
            index++;
        });
    }
};

function UpdateFactHoursInOldBase(userId, estId, date, type) {
    $.ajax({
        type: 'POST',
        url: '/Projects/UpdateFactHoursInOldBase',
        data: {
            estId: estId,
            userId: userId,
            date: date,
            type: type // 1 - подтвердить, 0 - отменить.
        },
        dataType: 'json',
        success: function (response) {
            if (!response.success) {
                alert("Не удалось обновить фактические часы.");
            }
        }
    });
}

function CancelApproved(desc, userId, estId, date) {
    $.ajax({
        type: 'POST',
        url: '/Projects/CancelApproved',
        data: {
            desc: desc,
            estId: estId,
            userId: userId,
            date: date
        },
        dataType: 'json',
        success: function (response) {
            if (!response.success) {
                alert("Не удалось доставить сообщение.");
            }
        }
    });
}


$(document).ready(function () {

    $('#loadingDiv').hide().ajaxStart(function () {
        $(this).toggle();
    }).ajaxStop(function () {
        $(this).toggle();
    });

    tsTabsClick();
    btnApproveClick();
});

function ConvertTime(minutes) {
    var hours = Math.floor(minutes / 60);
    var minute = minutes % 60;
    var string = hours + ':';
    if (minute < 10) {
        string = string + '0';
    }
    string = string + minute;
    return string;
};

function AddWeekHoursToProjectRow(row) {

    $(row).each(function () {
        var headTd = $(this).find('td:first').children().first();
        headTd.parent().attr('colspan', 1);
        var row = headTd.parent().parent();
        var cells = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        $(row).nextUntil(".head-row").each(function () {
            if ($(this).hasClass("subtotal")) {
                for (var i = 1; i < 11; i++) {
                    var hoursString = $(this).children().eq(i).html();
                    if (hoursString == '') {
                        hoursString = '0:00';
                    }
                    if (hoursString.indexOf(':') == -1) {
                        cells[i - 1] = cells[i - 1] + parseInt(hoursString * 60, 10);
                    } else {
                        var hoursArray = hoursString.split(':');
                        cells[i - 1] = cells[i - 1] +
                            parseInt(hoursArray[0] * 60, 10) +
                            parseInt(hoursArray[1], 10);
                    }
                }
                var val = $(this).children().eq(11).html();
                if (val == '') {
                    val = '0';
                }
                cells[11] = cells[11] + parseInt(val);
            }
        });
        //7 class="dayOfWeek"
        for (var i = 0; i < 10; i++) {
            row.html(row.html() + '<td class="dayOfWeekProj">' + ConvertTime(cells[i]) + '</td>');
        }
        row.html(row.html() + '<td class>' + cells[11] + '</td>');

        for (var i = 0; i < 2; i++) {
            row.html(row.html() + '<td class></td>');
        }
    });
}

function AddWeekHoursToEsRow(row) {

    $(row).each(function () {
        var headTd = $(this).find('td:first').children().first();
        headTd.parent().attr('colspan', 1);
        var row = headTd.parent().parent();
        var cells = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        $(row).nextUntil(".head-row-level-2").each(function () {
            if ($(this).hasClass("subtotal")) {
                for (var i = 1; i < 11; i++) {
                    var hoursString = $(this).children().eq(i).html();
                    if (hoursString == '') {
                        hoursString = '0:00';
                    }
                    if (hoursString.indexOf(':') == -1) {
                        cells[i - 1] = cells[i - 1] + parseInt(hoursString * 60, 10);
                    } else {
                        var hoursArray = hoursString.split(':');
                        cells[i - 1] = cells[i - 1] +
                            parseInt(hoursArray[0] * 60, 10) +
                            parseInt(hoursArray[1], 10);
                    }
                }
                var val = $(this).children().eq(11).html();
                if (val == '') {
                    val = '0';
                }
                cells[11] = cells[11] + parseInt(val);
            }


        });
        //7 class="dayOfWeek"
        for (var i = 0; i < 10; i++) {
            row.html(row.html() + '<td class="dayOfWeekEst">' + ConvertTime(cells[i]) + '</td>');
        }
        row.html(row.html() + '<td class>' + cells[11]+ '</td>');
        for (var i = 0; i < 2; i++) {
            row.html(row.html() + '<td class></td>');
        }
    });
}

function DeleteWeekHours(headTd) {
    headTd.parent().attr('colspan', 14);
    var row = headTd.parent().parent();
    var html = row.html().trim();
    html = html.slice(0, html.indexOf('<td', 3) - 1);
    row.html(html);
};


function saveWichRowOpen() {

    var tableRows = [];
    $('#timesheet-table tbody tr').each(function () {
        var type = $(this).hasClass('head-row') ? "head-row" : ($(this).hasClass('head-row-level-2') ? "head-row-level-2" : "row");
        var isOpen = true;
        var isWithTime = false;
        var isVisible = false;
        if (type === "head-row" || type === "head-row-level-2") {
            var sign = $(this).children().eq(0).children().eq(0)[0];
            if ($(sign).hasClass('fa-plus-square')) {
                isWithTime = true;
                isOpen = false;
            }
        }
        if ($(this).css('display') !== 'none') {
            isVisible = true;
        }
        tableRows.push(new TsRowNoClass($(this), type, isVisible, isOpen, isWithTime));
    });
    return tableRows;
};



function OpenRows(openRows) {
    $(openRows).each(function () {
        var oldValue = $(this);
        var newObject = $(document.getElementById($(oldValue[0].jqObject).attr('id')));
        if (newObject != null) {


            var newObjectSign = $(newObject).children().eq(0).children().eq(0)[0];

            switch (oldValue[0].type) {
            case 'head-row':
                if (oldValue[0].isVisible) {                                      
                    newObject.css("display", "");

                    if (oldValue[0].isOpen) {
                        if ($(newObjectSign).hasClass('fa-plus-square')) {
                            AddMinusSign($(newObjectSign));
                            DeleteWeekHours($(newObjectSign));
                        } else if ($(newObject).children().eq(0).prop('colSpan') === 1) {
                            DeleteWeekHours($(newObjectSign));
                        }

                    } else {
                        if ($(newObjectSign).hasClass('fa-minus-square')) {
                            AddPlusSign($(newObjectSign));

                        };
                        if ($(newObject).children().eq(0).prop('colSpan') === 14) {
                            AddWeekHoursToProjectRow($(newObject));
                        }
                    }
                } else {
                    newObject.css("display", "none");
                }
                break;
            case 'head-row-level-2':
                if (oldValue[0].isVisible) {
                    newObject.css("display", "");
                    if (oldValue[0].isOpen) {
                        if ($(newObjectSign).hasClass('fa-plus-square')) {
                            AddMinusSign($(newObjectSign));
                            DeleteWeekHours($(newObjectSign));

                        };
                    } else {
                        if ($(newObjectSign).hasClass('fa-minus-square')) {
                            AddPlusSign($(newObjectSign));
                            AddWeekHoursToEsRow($(newObject));
                        };
                    }
                } else {
                    $(newObject).css("display", "none");
                }
                break;
            case 'row':
                if (oldValue[0].isVisible) {
                    $(newObject).css("display", "");
                } else {
                    $(newObject).css("display", "none");
                }
                break;
            }

        }
    });
};


function SearchTs(TsType) {

    var tableRows = [];
    var searchText = $('#search').val().toLowerCase();
    $('#timesheet-table tbody tr').each(function () {
        var type = $(this).hasClass('head-row') ? "head-row" : ($(this).hasClass('head-row-level-2') ? "head-row-level-2" : "row");
        tableRows.push(new TsRowNoClass($(this), type, false, true));
    });

    if (searchText.length === 0) {
        if (openRowsTs != null) {
            //возврат в исходное состояние, до поиска
            $(tableRows).each(function () {
                var row = $(this)[0];
                if (row.type === 'head-row') {
                    row.jqObject.css("display", "");
                    var sign = row.jqObject.children().eq(0).children().eq(0);
                    if (sign.hasClass('fa-minus-square')) {
                        AddPlusSign(sign);
                        AddWeekHoursToProjectRow(row.jqObject);
                    };
                } else

                if (row.type === 'head-row-level-2') {
                    row.jqObject.css("display", "none");
                    var sign = row.jqObject.children().eq(0).children().eq(0);
                    if (sign.hasClass('fa-minus-square')) {
                        AddPlusSign(sign);
                        AddWeekHoursToEsRow(row.jqObject);

                    };
                } else {
                    row.jqObject.css("display", "none");
                }
            });
            OpenRows(openRowsTs);
            openRowsTs = null;
        }
    } else {
        //если первый поиск, то сохраняем открытые строки
        if (openRowsTs == null) {
            openRowsTs = saveWichRowOpen();
        }
        //поиск
        var isHeadRowShow = false;
        var isHeadRowLevel2Show = false;
        var index = 0;
        $(tableRows).each(function () {
            var value = $(this)[0];
            if (value.type === 'head-row') {
                isHeadRowShow = false;
            }
            if (value.type === 'head-row-level-2') {
                isHeadRowLevel2Show = false;
            }
            var cells = $(value.jqObject).find('td');
            for (var i = 0; i < 1; i++) {
                var cell = cells[i];
                if (!value.jqObject.hasClass('subtotal')) {
                    if (value.type === 'head-row') {
                        if ($(cell).text().toLowerCase().indexOf(searchText) >= 0) {
                            value.isVisible = true;
                            isHeadRowShow = true;
                        } else {
                            value.isVisible = false;
                        }
                    } else if (value.type === 'head-row-level-2') {
                        if ($(cell).text().toLowerCase().indexOf(searchText) >= 0 || isHeadRowShow === true) {
                            value.isVisible = true;
                            isHeadRowLevel2Show = true;
                            for (var x = index; x >= 0; x--) {
                                if (tableRows[x].type === 'head-row') {
                                    tableRows[x].open = true;
                                    tableRows[x].isVisible = true;
                                    tableRows[x].isWithTime = false;
                                    break;
                                }
                            }
                        } else {
                            value.isVisible = false;
                        }
                    } else if (value.type === 'row') {
                        if ($(cell).text().toLowerCase().indexOf(searchText) >= 0 ||
                            (isHeadRowShow === true && (TsType === "ByUsers" || TsType === "ByProject")) ||
                            isHeadRowLevel2Show) {
                            value.isVisible = true;
                            for (var x = index; x >= 0; x--) {
                                if (tableRows[x].type === 'head-row') {
                                    tableRows[x].open = true;
                                    tableRows[x].isVisible = true;
                                    tableRows[x].isWithTime = false;
                                    break;
                                }
                            }
                            for (var x = index; x >= 0; x--) {
                                if (tableRows[x].type === 'head-row-level-2') {
                                    tableRows[x].open = true;
                                    tableRows[x].isVisible = true;
                                    tableRows[x].isWithTime = false;
                                    break;
                                }
                            }
                        } else {
                            value.isVisible = false;
                        }
                    }
                };
            }
            index++;
        });
        OpenRows(tableRows);
    }
};



function SearchNotApprovedPeriods(searchText) {

    if (searchText == null) {
        searchText = '';
    }
    
    if (openRowsSrNotApproved == null) {
        openRowsSrNotApproved = saveWichRowOpen();
    }
    var tableRows = [];
    $('#timesheet-table tbody tr').each(function () {
        var type = $(this).hasClass('head-row') ? "head-row" : ($(this).hasClass('head-row-level-2') ? "head-row-level-2" : "row");
        tableRows.push(new TsRowNoClass($(this), type, false, true));
    });
    var index = 0;
    $(tableRows).each(function () {
        var value = $(this)[0];
        if (value.type === 'row') {
            var cells = $(value.jqObject).find('td');
            var cellWithButtons = cells[13];
            var approveButton = $(cellWithButtons).find('.btn-approve');
            if ($(approveButton).is(':enabled')) {
                if (cells[8].textContent !== "0:00" && cells[0].textContent.toLowerCase().indexOf(searchText)>=0) {
                    value.isVisible = true;
                    for (var x = index; x >= 0; x--) {
                        if (tableRows[x].type === 'head-row') {
                            tableRows[x].open = true;
                            tableRows[x].isVisible = true;
                            tableRows[x].isWithTime = false;
                            break;
                        }
                    }
                    for (var x = index; x >= 0; x--) {
                        if (tableRows[x].type === 'head-row-level-2') {
                            tableRows[x].open = true;
                            tableRows[x].isVisible = true;
                            tableRows[x].isWithTime = false;
                            break;
                        }
                    }
                }
            }
        }
        index++;
    });
    OpenRows(tableRows);
};

function SearchInsertPeriods(searchText) {

    if (searchText == null) {
        searchText = '';
    }

    if (openRowsSrApproved == null) {
        openRowsSrApproved = saveWichRowOpen();
    }
    var tableRows = [];
    $('#timesheet-table tbody tr').each(function () {
        var type = $(this).hasClass('head-row') ? "head-row" : ($(this).hasClass('head-row-level-2') ? "head-row-level-2" : "row");
        tableRows.push(new TsRowNoClass($(this), type, false, true));
    });
    var index = 0;
    $(tableRows).each(function () {
        var value = $(this)[0];
        if (value.type === 'row') {
            var cells = $(value.jqObject).find('td');
            var cellWithButtons = cells[13];
            var approveButton = $(cellWithButtons).find('.btn-approve');
            if (approveButton.length!==0) {
                if (cells[8].textContent !== "0:00" && cells[0].textContent.toLowerCase().indexOf(searchText) >= 0) {
                    value.isVisible = true;
                    for (var x = index; x >= 0; x--) {
                        if (tableRows[x].type === 'head-row') {
                            tableRows[x].open = true;
                            tableRows[x].isVisible = true;
                            tableRows[x].isWithTime = false;
                            break;
                        }
                    }
                    for (var x = index; x >= 0; x--) {
                        if (tableRows[x].type === 'head-row-level-2') {
                            tableRows[x].open = true;
                            tableRows[x].isVisible = true;
                            tableRows[x].isWithTime = false;
                            break;
                        }
                    }
                }
            }
        }
        index++;
    });
    OpenRows(tableRows);
};
function DisableSearchNotApprovedPeriods() {
    if (openRowsSrNotApproved !== null) {
        OpenRows(openRowsSrNotApproved);
        openRowsSrNotApproved = null;
    }
};
function DisableSearchInsertPeriods() {
    if (openRowsSrApproved !== null) {
        OpenRows(openRowsSrApproved);
        openRowsSrApproved = null;
    }
};
function AddMinusSign(sign) {
    sign.removeClass("fa fa-plus-square");
    sign.addClass("fa fa-minus-square");
};

function AddPlusSign(sign) {
    sign.removeClass("fa fa-minus-square");
    sign.addClass("fa fa-plus-square");
};

//class TsRow {
//    constructor(jqObject, type, isVisible, isOpen, isWithTime) {
//        this.jqObject = jqObject;
//        this.type = type;
//        this.isVisible = isVisible;
//        this.isOpen = isOpen;
//        this.isWithTime = isWithTime;
//    }
//};

function TsRowNoClass(jqObject, type, isVisible, isOpen, isWithTime) {
    var item = {
        'jqObject': jqObject,
        'type': type,
        'isVisible': isVisible,
        'isOpen': isOpen,
        'isWithTime': isWithTime
    };
    return item;
}
