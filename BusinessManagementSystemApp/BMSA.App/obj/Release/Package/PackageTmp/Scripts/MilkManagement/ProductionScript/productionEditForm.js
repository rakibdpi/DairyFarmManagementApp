$(document).ready(function () {

    refreshTable();
    $.get("/api/CowSetups", function (data) {
        var $el = $("#CowSetupId");
        $el.empty(); // remove old options
        $el.append($("<option></option>")
            .attr("value", '').text(''));
        $.each(data, function (value, key) {
            $el.append($('<option>', {
                value: key.id,
                text: key.number
            }));
        });
    });

});


function refreshTable() {
    $("#productionTable").DataTable().destroy();
    var t = $('#productionTable').DataTable({
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


$(document.body).on("change", "#CowSetupId", function () {
    $('#productionTable').DataTable().clear().draw();
    refreshTable();
});





$(document.body).on("click", "#btnLoad", function () {
    refreshTable();
    var client = $("#CowSetupId").val();
    var month = $("#ProductionMonth").val();
    var year = $("#Year").val();
    if (client.length < 1 || month.length < 1 || year.length < 1) {
        toastr.warning("Please fill up all required fields", "Warning!!");
        return;
    }

    $.get("/api/Productions/GetByClientIdAndMonth", { cowId: client, month: month, year: year },
        function (data) {
            console.log(data.length);
            if (data.length > 0) {
                editTable(client, month, year);
            } else {
                toastr.warning("Not sale yet for this cOW in this date", "Warning!!");
                $('#productionTable').DataTable().clear().draw();
                refreshTable();
            }
        });
});



$(document.body).on("click", "#btnSubmit", function () {
    var dto = {
        dtoList: []
    };

    var clt = $("#CowSetupId").val();
    var id = $("#Id").val();
    var productionMonth = $("#ProductionMonth").val();
    var year = $("#Year").val();
    var dtlTable = $('#productionTable').DataTable();
    var details = dtlTable.rows().data();
    if (details.length < 1) {
        toastr.warning("Please fill up the all required field", "Warning!!!");
        return;
    }
    for (var i = 0; i < details.length; i++) {
        var morningQuantityId = 'MorningQuantity' + i;
        var afterNoonQuantityId = 'AfterNoonQuantity' + i;
        var nightQuantityId = 'NightQuantity' + i;
        var otherTimeId = 'OtherTime' + i;

        var morningQuantity = dtlTable.$('input[name=' + morningQuantityId + '] ').val();
        var afterNoonQuantity = dtlTable.$('input[name=' + afterNoonQuantityId + '] ').val();
        var nightQuantity = dtlTable.$('input[name=' + nightQuantityId + '] ').val();
        var otherTime = dtlTable.$('input[name=' + otherTimeId + '] ').val();
        var sid = details.cell(i, 0).data();
        var cow = details.cell(i, 1).data();
        var day = details.cell(i, 2).data();
        dto.dtoList.push({
            id: sid,
            cowSetupId: cow,
            dayNumber: day,
            productionMonth: productionMonth,
            year: year,
            morningQuantity: parseFloat(morningQuantity),
            afterNoonQuantity: parseFloat(afterNoonQuantity),
            nightQuantity: parseFloat(nightQuantity),
            otherTime: parseFloat(otherTime),
        });
    }
    $.ajax({
        url: "/api/Productions",
        data: dto,
        type: "PUT",
        success: function (e) {
            if (e > 0) {
                toastr.success("Update Success", "Success!!!");
                $("#CowSetupId").val("");
                $('#productionTable').DataTable().clear().draw();
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

var rowNumber = -1;
var number = -1;
var count = -1;
var other = -1;

function editTable(clientId, month, year) {
    $("#productionTable").DataTable().destroy();
    $('#productionTable').DataTable({
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
            url: "/api/Productions/GetByClientIdAndMonth",
            data: { cowId: clientId, month: month, year: year },
            dataSrc: ""
        },
        columns: [
            {
                data: "id"
            },
            {
                data: "cowSetupId"
            },
            {
                data: "dayNumber"
            },
            {
                data: "morningQuantity",
                render: function (data) {
                    ++rowNumber;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='MorningQuantity" + rowNumber + "' name='MorningQuantity" + rowNumber + "' value= " + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='MorningQuantity" + rowNumber + "' name='MorningQuantity" + rowNumber + "' value= '0'>";
                    }
                }
            },
            {
                data: "afterNoonQuantity",
                render: function (data) {
                    ++number;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='AfterNoonQuantity" + number + "' name='AfterNoonQuantity" + number + "' value=" + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='AfterNoonQuantity" + number + "' name='AfterNoonQuantity" + number + "' value='0'>";
                    }
                }
            },
            {
                data: "nightQuantity",
                render: function (data) {
                    ++count;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='NightQuantity" + count + "' name='NightQuantity" + count + "' value=" + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='NightQuantity" + count + "' name='NightQuantity" + count + "' value='0'>";
                    }
                }
            },
            {
                data: "otherTime",
                render: function (data) {
                    ++other;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='OtherTime" + other + "' name='OtherTime" + other + "' value=" + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='OtherTime" + other + "' name='OtherTime" + other + "' value='0'>";
                    }
                }
            }
        ]
    });
}

$(document.body).on("click", "#btnCancel", function () {
    location.reload();
});