
$(document).ready(function () {
    tableInit();
    selectInit();

    var table = $('#table-projects').DataTable();
    table.select.style('multi');

    FilteringProjects("self-project-row");

    table.on('user-select',
        function (e, dt, type, cell, originalEvent) {
            if (originalEvent.target.tagName === 'A') {
                e.preventDefault();
            }
        });

    $("#by-archive-projects, #by-projects, #by-unit-projects, #by-delete-projects").on("click",
        function () {
            var element = $(this);
            if (!element.hasClass("active")) {
                $(".tab > li").removeClass("active");
                element.addClass("active");

                var projectClass = element.data("projectClass");
                FilteringProjects(projectClass);

                $("#btn-archive-projects").closest('li').css("display", "block");
                $("#btn-delete-projects").text("Удалить");

                if (element.text() === "Архив") {

                    $("#btn-archive-projects").text("Разархивировать");
                } else if (element.text() === "Проекты") {
                    $("#btn-archive-projects").text("Архивировать");
                } else {
                    $("#btn-archive-projects").closest('li').css("display", "none");
                    $("#btn-delete-projects").text("Восстановить");
                }
            }
        });
});



//фильтрация строк таблицы по классу.
function FilteringProjects(projectClass) {
    var table = $('#table-projects').DataTable();
    table.rows().deselect();

    $.fn.dataTable.ext.search.pop();
    table.draw();

    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            var row = $(table.row(dataIndex).node());

            if (!row.hasClass(projectClass)) {
                return false;
            }
            return true;
        }
    );
    table.draw();
}


function delete_workgroup_row(rangeIndex) {
    $("#workgroup-row-" + rangeIndex).hide("slow", function () {
        $(this).remove();
    });
    $('#select-user li[rel="' + rangeIndex + '"]').css("display", "block");
}


function OnCreateProjectFailure() {
    alert('Произошла ошибка при создании проекта.');
};


function parseDate(input, format) {
    format = format || 'yyyy-mm-dd'; // default format
    var parts = input.match(/(\d+)/g),
        i = 0, fmt = {};
    // extract date-part indexes from the format
    format.replace(/(yyyy|dd|mm)/g, function (part) { fmt[part] = i++; });

    return new Date(parts[fmt['yyyy']], parts[fmt['mm']] - 1, parts[fmt['dd']]);
}


function OnCreateProjectSuccess(project) {

    var table = $('#table-projects').DataTable();

    var node = table.row.add([
        project.Name,
        project.Code,
        project.Description,
        project.LeaderName,
        project.ClientName,
        project.Tender,
        parseDate(project.StartDate, 'yyyy-mm-dd').format("dd.mm.yyyy"),
        parseDate(project.FinishDate, 'yyyy-mm-dd').format("dd.mm.yyyy"),
        project.UnitName,
        project.Type
    ]).node();

    $(node).data("projectId", project.Id);
    $(node).find('td:first-child').html("<a href='/Projects/ProjectInfo/" + project.Id + "'>" + project.Name + "</a>");
    $(node).addClass("self-project-row");
    table.draw();
    $("#modal-create-project").modal('toggle');
}


//инициализация таймпикера.
function selectInit() {
    $('.timepicker1').timepicker({
        minuteStep: 5,
        maxHours: 1000,
        defaultTime: false,
        showMeridian: false
    });
}


//инициализация и локализация таблицы на русский язык.
function tableInit() {
    $('table').DataTable({
        destroy: true,
        "language": {
            "processing": "Подождите...",
            "search": "Поиск:",
            "lengthMenu": "Показать _MENU_ записей",
            "info": "Записи с _START_ до _END_ из _TOTAL_ записей",
            "infoEmpty": "Записи с 0 до 0 из 0 записей",
            //"infoFiltered": "(отфильтровано из _MAX_ записей)",
            "infoFiltered": "",
            "infoPostFix": "",
            "loadingRecords": "Загрузка записей...",
            "zeroRecords": "Записи отсутствуют.",
            "emptyTable": "В таблице отсутствуют данные",
            "paginate": {
                "first": "Первая",
                "previous": "Предыдущая",
                "next": "Следующая",
                "last": "Последняя"
            },
            "aria": {
                "sortAscending": ": активировать для сортировки столбца по возрастанию",
                "sortDescending": ": активировать для сортировки столбца по убыванию"
            },
            "select": {
                "rows": "выбрано строк: %d "
            }
        }
    });
};