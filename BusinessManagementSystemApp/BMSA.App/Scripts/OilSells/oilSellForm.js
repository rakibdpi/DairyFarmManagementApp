$(document).ready(function () {

    refreshTable();

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

function DateTimeFormatChange(date) {
    var d = new Date(date.split("-").reverse().join("-"));
    var dd = d.getDate();
    var mm = d.getMonth() + 1;
    var yy = d.getFullYear();
    var newDate = yy + "/" + mm + "/" + dd;
    return newDate;
}
function refreshTable() {
    $("#OilSalesTable").DataTable().destroy();
    var t = $('#OilSalesTable').DataTable({
        retrieve: false,
        paging: false,
        searching: false,
        info: false,
        responsive: true,
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "visible": false,
                "searchable": false
            }
        ]
    });
}
var rowNumber = -1;
var number = -1;
var count = -1;


function monthLength(month) {
    if (month === "January" || month === "March" || month === "May" || month === "July" || month === "August" || month === "October" || month === "December") {
        return 31;
    }
    else if (month === "April" || month === "June" || month === "September" || month === "November") {
        return 30;
    } else {
        return 28;
    }
}
var dayInterval = 0;
var oneKg = 0;
var twoKg = 0;
var fiveKg = 0;

function initialTable() {
    $("#OilSalesTable").DataTable().destroy();
    var t = $('#OilSalesTable').DataTable({
        retrieve: false,
        paging: false,
        searching: false,
        info: false,
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "visible": false,
                "searchable": false
            }
        ]
    });
    var count = 1;
    var monthDuration = monthLength($("#SalesMonth").val());
    for (var i = 0; i < monthDuration; i++) {

        t.row.add([
            0,
            $("#ClientInfoId").val(),
            count,
            "<input style='width: 100px' type='text' id='OneKg" + i + "' name='OneKg" + i + "' value= '0'>",
            "<input style='width: 100px' type='text' id='TwoKg" + i + "' name='TwoKg" + i + "' value='0'>",
            "<input style='width: 100px' type='text' id='FiveKg" + i + "' name='FiveKg" + i + "' value='0'>"
        ]).draw(false);
        ++count;
    }
}

$(document.body).on("change", "#ClientInfoId", function () {
    $('#salesTable').DataTable().clear().draw();
    refreshTable();
});



$(document.body).on("click", "#btnLoad", function () {
    refreshTable();
    var client = $("#ClientInfoId").val();
    var month = $("#SalesMonth").val();

    if (client.length < 1 || month.length < 1) {
        toastr.warning("Please fill up all required fields", "Warning!!");
        return;
    }

    $.get("/api/OilSells/GetStatus", { clientId: client, month: month },
        function (data) {
            if (data) {
                toastr.warning("Oil already entry for this Client in this month", "!!Warning");
                $('#OilSalesTable').DataTable().clear().draw();
                return;
            } else {
                initialTable();
            }
        });
});


$(document.body).on("click", "#btnSubmit", function () {

    var clt = $("#ClientInfoId").val();
    var id = $("#Id").val();
    var month = $("#SalesMonth").val();

    var dto = {
        oilSaleDtos: []
    };
        var areaId = $("#AreaId").val();

    var dtlTable = $('#OilSalesTable').DataTable();
    var details = dtlTable.rows().data();
    if (details.length < 1) {
        toastr.warning("Please fill up the all required field", "Warning!!!");
        return;
    }
    for (var i = 0; i < details.length; i++) {

        var oneKgId = 'OneKg' + i;
        var twoKgId = 'TwoKg' + i;
        var fiveKgId = 'FiveKg' + i;

        var one = dtlTable.$('input[name=' + oneKgId + '] ').val();
        var two = dtlTable.$('input[name=' + twoKgId + '] ').val();
        var five = dtlTable.$('input[name=' + fiveKgId + '] ').val();

        var client = details.cell(i, 1).data();
        var day = details.cell(i, 2).data();
        dto.oilSaleDtos.push({
                areaId: areaId,
                dayNumber: day,
                salesMonth: month,
                clientInfoId: client,
                oneKg: parseFloat(one),
                twoKg: parseFloat(two),
                fiveKg: parseFloat(five)
            });
        }
        $.ajax({
            url: "/api/OilSells",
            data: dto,
            type: "POST",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Save Success", "Success!!!");
                    $('#OilSalesTable').DataTable().clear().draw();
                   // getNextClient(areaId, clt);
                } else {
                    toastr.warning("Save Fail", "Warning!!!");
                }
            },
            error: function (request, status, error) {
                var response = jQuery.parseJSON(request.responseText);
                toastr.error(response.message, "Error");
            }
        });
});

$(document.body).on("click", "#btnCancel", function () {
    ClearForm();
});

function ClearForm() {
    location.reload();
}

$(document.body).on("change", "#AreaId", function () {
    // Refresh All Dropdown
    $("#ClientInfoId").empty();
    var area = $("#AreaId").val();
    if (area > 0) {
        $.ajax({
            type: "GET",
            url: "/api/ClientInfos/GetClientByArea",
            contentType: "application/json; charset=utf-8",
            data: { areaId: area },
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