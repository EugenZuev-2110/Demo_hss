function updateStatus(confirmStep, VacationType) {
    var element = $(event.target).closest("button");
    var comment = "";

    var vacationId = $(element).data("vacation-id");
    var isApprove= ($(element).data("act") === "approve") ? true : false;
    if (!isApprove) {
        comment = $('#cancel-comment').val();
    }
     
    var hasScan = $(element).data('hasScan');
    if ((confirmStep !== 0 && confirmStep !== -1) || (VacationType !== 1 && VacationType !== 2 && VacationType!==3)) {
        hasScan = "True";
    }

    if (hasScan === "True") {
        $.ajax({
            url: '/OperatingClaims/UpdateApproveVacation/',
            type: 'POST',
            data: { 'vacationId': vacationId, 'isApprove': isApprove, 'comment': comment }
        }).done(function (data) {
            var currentRow = $("#" + vacationId);

            if (!element.hasClass("btn-success") || currentRow.css("background-color") === "rgb(222, 184, 135)") {
                var table = $('#table-hr').DataTable();
                table.row($("#" + vacationId)).remove().draw();
            } else {
                element.prop("disabled", true);
                currentRow.find("#confirm-step").text(data);
            }

        });
    } else {
        $('#no-scan-error').show();
    }

}

function showDisapprobeMessage() {
    var element = $(event.target);

    var dataTypeMessage = $(element).data("act");
    var dataVacationId = $(element).data("vacation-id");

    $('#message-box-notconfirm').show();
    $('#btn-notconfirm-yes').data("act", dataTypeMessage);
    $('#btn-notconfirm-yes').data("vacation-id", dataVacationId);

}




function createVacationModal(vacationSectionId) {
    $.ajax({
            type: 'POST',
            url: '/OperatingClaims/GetVacationById',
            data: {
                sectionId: vacationSectionId
            },
            dataType: 'html'
     }).done(function (result) {
            var div = $('#div-create-vacation');
            div.html(result);
            $("#modal-create-vacation").modal('toggle');
        });
};

function changeArchiveType(vacationSectionId) {
    var isArchive = $(event.target).data('isarchive');
    $.ajax({
            type: 'POST',
            url: '/OperatingClaims/GetVacations',
            data: {
                vacationTypeSection: vacationSectionId,
                isArchive: isArchive
            },
            dataType: 'html'
        })
        .done(function (result) {
            var div = $('.panel-body');
            div.html(result);

            if (isArchive === true) {
                $('#NotArchiveVacations').parent().removeClass('active');
                $('#ArchiveVacations').parent().addClass('active');

            } else {
                $('#ArchiveVacations').parent().removeClass('active');
                $('#NotArchiveVacations').parent().addClass('active');

            }
        });
}

function getVacationById(vacationId) {
    $.ajax({
            type: 'POST',
            url: '/OperatingClaims/GetVacationById',
            data: {
                id: vacationId
            },
            dataType: 'html'
        })
        .done(function (result) {
            var div = $('#div-create-vacation');
            div.html(result);
            $("#modal-create-vacation").modal('toggle');
        });
}

function confirmDeleteVacations() {
    var vacationId = [];
    $(".selected").each(function () {
        vacationId.push($(this).data("vacationid"));
    });
    $.ajax({
        type: 'POST',
        url: '/OperatingClaims/DeleteVacations/',
        data: {
            vacationId: vacationId
        },

        success: function () {
            var table = $('#table-hr').DataTable();
            $(".selected").each(function () {
                table.row($(this)).remove().draw();
            });
        }
    });
};