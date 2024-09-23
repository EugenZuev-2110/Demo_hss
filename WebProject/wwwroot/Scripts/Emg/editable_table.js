function editTable(clickedTd, e) {

    var cell = clickedTd;
    var table = cell.closest('table');
    var row = cell.parent();
    var tar = e.target || e.srcElement;
    var elmName = tar.tagName.toLowerCase();
    var elCol = table.find('th').eq(cell.index());

    if (elmName === 'input' || !$(elCol).hasClass("editable") || $(e.target).hasClass('HourConfirm')) {
        return false;
    }

    var oldVal = cell.html();

    
    var inputWidth = '';
    var tableId = table.attr("id");
    if (tableId === 'timesheet-table' || tableId === 'fin-claim-table') {
        var tdWidth = elCol.width();
        inputWidth = 'style="width: ' + tdWidth + 'px;"';
    }
    var code = '<input type="text"'+ inputWidth +' id="edit" value="' + oldVal + '" />';

    //удаляем содержимое ячейки, вставляем в нее сформированное поле
    clickedTd.empty().append(code);
    var editInput = $("#edit");

    editInput.focus();
    editInput[0].selectionStart = editInput[0].selectionEnd = editInput.val().length;

    editInput.blur(function () {
        if ($("#edit").hasClass("value-error")) {
            $(this).parent().empty().html(oldVal);
        } else {
            //получаем то, что в поле находится
            var val = $(this).val();
            //находим ячейку, опустошаем, вставляем значение из поля
            $(this).parent().empty().html(val);
        }
    });

    editInput.keydown(function (event) {
        if (!elCol.hasClass("numeric")) {
            return;
        }
        if (event.keyCode === 46 ||
            event.keyCode === 8 ||
            event.keyCode === 9 ||
            event.keyCode === 27 ||
            event.keyCode === 13 ||
            (event.keyCode === 65 && event.ctrlKey === true) ||
            (event.keyCode >= 35 && event.keyCode <= 39)) {
            return;
        } else {
            //если не число, останавливаем нажатие.
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }
    });

    $(window).keydown(function (event) {
        //снимаем фокус с поля ввода, если это Enter.
        if (event.keyCode === 13) {
            $('#edit').blur();
        }
    });
};