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


$(document.body).on("change", "#ClientInfoId", function () {
    $('#OilSalesTable').DataTable().clear().draw();
    refreshTable();
});


$(document.body).on("click", "#btnLoad", function () {
    refreshTable();
    var client = $("#ClientInfoId").val();
    var month = $("#SalesMonth").val();
    //var year = $("#Year").val();
    if (client.length < 1 || month.length < 1) {
        toastr.warning("Please fill up all required fields", "Warning!!");
        return;
    }

    $.get("/api/OilSells/GetByClientIdAndMonth", { clientId: client, month: month },
        function (data) {
            console.log(data.length);
            if (data.length > 0) {
                editTable(client, month);
            } else {
                toastr.warning("Not sale yet for this cOW in this date", "Warning!!");
                $('#OilSalesTable').DataTable().clear().draw();
                refreshTable();
            }
        });
});


var rowNumber = -1;
var number = -1;
var count = -1;
var other = -1;


function editTable(clientId, month) {
    $("#OilSalesTable").DataTable().destroy();
    $('#OilSalesTable').DataTable({
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
        ],
        ajax: {
            url: "/api/OilSells/GetByClientIdAndMonth",
            data: { clientId: clientId, month: month },
            dataSrc: ""
        },
        columns: [
            {
                data: "id"
            },
            {
                data: "clientInfoId"
            },
            {
                data: "dayNumber"
            },
            {
                data: "oneKg",
                render: function (data) {
                    ++rowNumber;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='OneKg" + rowNumber + "' name='OneKg" + rowNumber + "' value= " + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='OneKg" + rowNumber + "' name='OneKg" + rowNumber + "' value= '0'>";
                    }
                }
            },
            {
                data: "twoKg",
                render: function (data) {
                    ++number;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='TwoKg" + number + "' name='TwoKg" + number + "' value=" + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='TwoKg" + number + "' name='TwoKg" + number + "' value='0'>";
                    }
                }
            },
            {
                data: "fiveKg",
                render: function (data) {
                    ++other;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='FiveKg" + other + "' name='FiveKg" + other + "' value=" + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='FiveKg" + other + "' name='FiveKg" + other + "' value='0'>";
                    }
                }
            }
        ]
    });
}



$(document.body).on("click", "#btnSubmit", function () {

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
            if (i === 0) {
                id = details.cell(i, 0).data();
            }

            var oneKgId = 'OneKg' + i;
            var twoKgId = 'TwoKg' + i;
            var fiveKgId = 'FiveKg' + i;

            var one = dtlTable.$('input[name=' + oneKgId + '] ').val();
            var two = dtlTable.$('input[name=' + twoKgId + '] ').val();
            var five = dtlTable.$('input[name=' + fiveKgId + '] ').val()

            var sid = details.cell(i, 0).data();
            var client = details.cell(i, 1).data();
            var day = details.cell(i, 2).data();

            dto.oilSaleDtos.push({
                id: sid,
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
            type: "PUT",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Info Update Success", "Success!!!");
                    ClearForm();
                } else {
                    toastr.warning("Info Update Fail", "Warning!!!");
                }
            },
            error: function (request, status, error) {
                var response = jQuery.parseJSON(request.responseText);
                toastr.error(response.message, "Error");
            }
        });

    
});


function ClearForm() {
    $('#OilSalesTable').DataTable().clear().draw();
}