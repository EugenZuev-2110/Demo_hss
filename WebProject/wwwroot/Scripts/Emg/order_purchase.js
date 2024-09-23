function CreateNewOrderPurchase() {
    $.ajax({
            type: 'POST',
            url: '/OrderPurchase/CreateOrderPurchaseView',
            dataType: 'html'
        })
        .done(function (result) {
            var div = $('#div-create-OPO');
            div.html(result);
            $("#modal-create-OP").modal('toggle');
        });
};

function DevareOrderPurchases() {
    var devareRowsId = [];
    $(".selected").each(function () {
        devareRowsId.push($(this).attr('id'));
    });
    $.ajax({
        type: 'POST',
        url: '/OrderPurchase/DevareOp',
        data: {
            idList: devareRowsId
        },
        dataType: 'json',
        success: function () {
            var table = $('#OrderPurchases_tabel').DataTable();
            $(document).find(".selected").each(function () {
                table.row($(this)).remove().draw();
            });

        },
        error: function () {
            alert("Не удалось удалить проекты!");
        }
    });
};

function OPSupplyModify(modifyButton) {
    var rowId = $(modifyButton).closest('tr').attr('id');
    $(modifyButton).closest('tr').removeClass('selected');
    $.ajax({
            type: 'POST',
            url: '/OrderPurchase/CreateOPSupplyView',
            data: { OrderPurchase_Id: $('#OrderPurchase_Id').val(), orderPurchaseSupplyId: rowId },
            dataType: 'html'
        })
        .done(function (result) {
            var div = $('#div-create-OPSupply');
            div.html(result);
            $("#modal-create-OPSupply").modal('toggle');
        });

}
function DevareOPSupply() {
    var rowsId = [];
    var table = $('#OrderPurchases-tabl').DataTable();
    $(".selected").each(function() {
        rowsId.push($(this).attr('id'));
    });
    
    $.ajax({
        type: 'POST',
        url: '/OrderPurchase/DevareOPSupply',
        data: {
            idList: rowsId
        },
        dataType: 'json',
        success: function() {
            $(".selected").each(function() {
                table.row($(this)).remove().draw();
            });

        },
        error: function() {
            alert("Не удалось удалить проекты!");
        }
    });
};

function parseJsonDate(input) {
    var d = /\/Date\((\d*)\)\//.exec(input);
    return (d) ? new Date(+d[1]) : input;
};

function OrderPurchaseCreateSuccess(orderPurchase) {
    var table = $(document).find("#OrderPurchases_tabel").DataTable();
    var node = table.row.add([
        orderPurchase.Id,
        parseJsonDate(orderPurchase.CreateDate).format("dd.mm.yyyy"),
        orderPurchase.ContractorName,
        orderPurchase.BuyerName,
        orderPurchase.ProjectManagerName,
        orderPurchase.csdName,
        orderPurchase.OPManagerName,
        orderPurchase.SumWithoutNDS,
        orderPurchase.SumWithNDS,
        "С " + parseJsonDate(orderPurchase.DeliveryDateStart).format("dd.mm.yyyy") + " по " + parseJsonDate(orderPurchase.DeliveryDateEnd).format("dd.mm.yyyy")
    ]).draw().node();
    $(node).find('td:first-child').html("<a href='/OrderPurchase/SingleOrderPurchase/" + orderPurchase.Id + "'>" + orderPurchase.Id + "</a>");
    $(document).find('tr:last').attr('id', orderPurchase.Id);
    $(node).find('td').each(function () {
        $(this).css('text-align', 'center');
    });
    $("#modal-create-OP").modal('toggle');
}

function validationCreateOPS() {
    var ContractorIdList = $(document).find('#ContractorId_List');
    var BuyerIdIdList = $(document).find('#BuyerId_List');
    var ProjectManagerList = $(document).find('#ProjectManager_List');
    var OPManagerList = $(document).find('#OPManager_List');
    var ProjectIdList = $(document).find('#ProjectId_List');
    

    hideErrorsCreateOP();
    var flag = true;
    if ($(ContractorIdList).find(".selected").attr("rel") === "0") {
        $(document).find("#ContractorId_Error").show();
        flag = false;
    }
    if ($(BuyerIdIdList).find(".selected").attr("rel") === "0") {
        $(document).find("#BuyerId_Error").show();
        flag = false;
    }
    if ($(ProjectManagerList).find(".selected").attr("rel") === "0") {
        $(document).find("#ProjectManager_Error").show();
        flag = false;
    }
    if ($(OPManagerList).find(".selected").attr("rel") === "0") {
        $(document).find("#OPManager_Error").show();
        flag = false;
    }   
    if ($(ProjectIdList).find(".selected").attr("rel") === "0") {
        $(document).find("#ProjectId_Error").show();
        flag = false;
    }   
   

    return flag;
}

function hideErrorsCreateOP() {
    $(document).find("#ContractorId_Error").hide();
    $(document).find("#BuyerId_Error").hide();
    $(document).find("#ProjectManager_Error").hide();
    $(document).find("#OPManager_Error").hide();
    $(document).find("#ProjectId_Error").hide();
}
function hideErrorsCreateOPSupply() {
    $(document).find("#Quantity_Error").hide();
    $(document).find("#OPSupplyName_Error").hide();
    $(document).find("#Measure_Error").hide();
    $(document).find("#PriceWithoutNDS_Error").hide();
}
function validationCreateOPSupply() {
    var QuantityList = $(document).find('#Quantity_List');
    var OPSupplyName = $(document).find('#OpSupply_Name');
    var MeasureName = $(document).find('#OpSupply_Measure');
    var PriceWithoutNDSName = $(document).find('#OpSupply_PriceWithoutNDS');
    var Discount = $(document).find('#OPSupply_Discount');
    hideErrorsCreateOPSupply();
    var flag = true;
    if ($(QuantityList).find(".selected").attr("rel") === "0") {
        $(document).find("#Quantity_Error").show();
        flag = false;
    }
    if ($(OPSupplyName).val() === "") {
        $(document).find("#OPSupplyName_Error").show();
        flag = false;
    }
    
    if ($(MeasureName).val() < 0) {
        $(document).find("#Measure_Error").show();
        flag = false;
    }
    if ($(Discount).val() < 0) {
        $(document).find("#Discount_Error").show();
        flag = false;
    }
    if ($(PriceWithoutNDSName).val() <0) {
        $(document).find("#PriceWithoutNDS_Error").show();
        flag = false;
    }
    return flag;
}
