$(document).ready(function () {


    $("#AreaId").select2();
    $("#ClientInfoId").select2();

    $.get("/api/Areas", function (data) {
        var $el = $("#AreaId");
        $el.empty(); // remove old options
        $el.append($("<option></option>")
            .attr("value", '').text(''));
        $.each(data, function (value, key) {
            $el.append($('<option>', {
                value: key.id,
                text: key.name
            }));
        });
    });
});


$(document.body).on("change", "#AreaId", function () {
    // Refress All Dropdown
    $("#ClientInfoId").empty();

    var classId = $("#AreaId").val();
    if (classId > 0) {
        $.ajax({
            type: "GET",
            url: "/api/ClientInfos/GetClientByArea",
            contentType: "application/json; charset=utf-8",
            data: { areaId: classId },
            success: function (data) {
                var $el = $("#ClientInfoId");
                $el.empty(); // remove old options
                $el.append($("<option></option>")
                    .attr("value", '').text(''));
                $.each(data, function (value, key) {
                    $el.append($('<option>', {
                        value: key.id,
                        text: key.code + " " + key.name
                    }));
                });
            }
        });
    }
});

$(document.body).on("click", "#btnLoad", function () {
    var type = $("#Type").val();
    var areaId = $("#AreaId").val();
    var clientId = $("#ClientInfoId").val();
    var year = $("#Year").val();
    var month = $("#Month").val();

    if (areaId.length < 1 || year.length < 1 || month.length < 1) {
        toastr.warning("Please Select Required Fields", "Warning!!");
        return;
    }

    if (type == "Due") {
        loadDueData(areaId,year, month);
    }
    else {

    }
});

function loadDueData(areaId,  year, month) {
    $("#paymentsTable").DataTable().destroy();

    $("#paymentsTable").DataTable({
        retrieve: true,
        paging: true,
        searching: false,
        info: false,
        rowRecoder: {
            selector: 'td:nth-child(1)'
        },
        ajax: {
            url: "/api/DueBills/GetDueData",
            data: { areaId: areaId, year: year, month: month },
            dataSrc: ""
        },
        columns: [
            {
                data: "clientInfo.name"
            },
            {
                data: "clientInfo.code",
                render: function (data) {
                    if (data) {
                        return data;
                    } else {
                        return "";
                    }
                }
            },
            {
                data: "dueAmount",
                render: function (data) {
                    if (data) {
                        return data;
                    } else {
                        return "";
                    }
                }
            }
       
        ]

    });
}






function loadPaymentData(areaId, clientId, year, month) {



}