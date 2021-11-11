$(document).ready(function () {
    $("#ClientInfoId").select2();
    $("#Area").select2();

    $.get("/api/Areas", function (data) {
        var $el = $("#Area");
        $el.empty(); // remove old options
        $el.append($("<option></option>")
            .attr("value", '').text(''));
        $.each(data, function (value, key) {
            $el.append($('<option>',
                {
                    value: key.id,
                    text: key.name
                }));
        });
    });

    refreshDataTable();

    var urlVar = getUrlVars();
    var id = urlVar["id"];

    if (id > 0) {
        getData(id);
    }
    

    // loadHistoryTable();
});

function refreshDataTable() {
    $("#reservationTable").DataTable().destroy();
    var t = $('#reservationTable').DataTable({
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
    
}

function initialTable() {
    $("#reservationTable").DataTable().destroy();
    var t = $('#reservationTable').DataTable({
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
    for (var i = 0; i < 31; i++) {
        t.row.add([
            0,
            $("#ClientInfoId").val(),
            count,
            "<input style='width: 100px' type='text' id='HalfKg" + i + "' name='HalfKg" + i + "' value= '0'>",
            "<input style='width: 100px' type='text' id='SevenAndHalfGm" + i + "' name='SevenAndHalfGm" + i + "' value='0'>",
            "<input style='width: 100px' type='text' id='OneKg" + i + "' name='OneKg" + i + "' value='0'>"
        ]).draw(false);
        ++count;
    }
}

var rowNumber = -1;
var number = -1;
var count1 = -1;
var dt = -1;
function editTable(clientId) {
    $("#reservationTable").DataTable().destroy();
    var t = $('#reservationTable').DataTable({
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
            url: "/api/Reservations/GetByClientId?clientId=" + clientId,
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
                data: "halfKg",
                render: function (data) {
                    ++rowNumber;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='HalfKg" + rowNumber + "' name='HalfKg" + rowNumber + "' value= " + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='HalfKg" + rowNumber + "' name='HalfKg" + rowNumber + "' value= '0'>";
                    }
                }
            },
            {
                data: "sevenAndHalfGm",
                render: function (data) {
                    ++number;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='SevenAndHalfGm" + number + "' name='SevenAndHalfGm" + number + "' value=" + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='SevenAndHalfGm" + number + "' name='SevenAndHalfGm" + number + "' value='0'>";
                    }
                }
            },
            {
                data: "oneKg",
                render: function (data) {
                    ++count1;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='OneKg" + count1 + "' name='OneKg" + count1 + "' value=" + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='OneKg" + count1 + "' name='OneKg" + count1 + "' value='0'>";
                    }
                }
            }
        ]
    });
}
$(document.body).on("change", "#Area", function () {
    // Refresh All Dropdown
    $("#ClientInfoId").empty();
    var area = $("#Area").val();
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
                        text: key.name
                    }));
                });
            }
        });
    }

});

$(document.body).on("change", "#ClientInfoId", function () {
   
    $("#reservationTable").DataTable().destroy();
    // Refresh All Dropdown
    var client= $("#ClientInfoId").val();
    $.get("/api/Reservations/GetByClientId?clientId="+ client,
        function (data) {
            console.log(data.length);
            if (data.length>0) {
                editTable(client);
            } else {
                initialTable();
            }
        });

    
});

$(document.body).on("click", "#btnCalculate", function () {
    var totalHalf = 0;
    var totalSevenHalf = 0;
    var totalOne = 0;
    var table = $("#reservationTable").DataTable();
    var reservation = table.rows().data();
    var client = $("#ClientInfoId").val();
    if (client=="") {
        toastr.warning("Please select the client field","!!Warning");
        return;
    }

    if (reservation.length>0 ) {
        for (var i = 0; i < reservation.length; i++) {
            //var tid = reservation.cell(i, 0).data();
            //var client = reservation.cell(i, 1).data();
            //var day = reservation.cell(i, 6).data();
            var halfId = 'HalfKg' + i;
            var svnId = 'SevenAndHalfGm' + i;
            var oneId = 'OneKg' + i;
            var half = reservation.$('input[name=' + halfId + '] ').val();
            var sevenFifty = reservation.$('input[name=' + svnId + '] ').val();
            var one = reservation.$('input[name=' + oneId + '] ').val();
            totalHalf += parseInt(half);
            totalSevenHalf += parseInt(sevenFifty);
            totalOne += parseInt(one);
            //var dt = parseInt(half) + parseInt(sevenFifty) + parseInt(one);
            //$("#DayTotal" + i + "").val(dt);
            //subTotal += dt;
        }   
    }
    $("#tHalf").text(totalHalf);
    $("#tSeven").text(totalSevenHalf);
    $("#tOne").text(totalOne);


});

$(document.body).on("click","#btnSubmit",function () {
    var dto = {
        reservations: []
    };
    var id = $("#Id").val();
    var table = $("#reservationTable").DataTable();
    var reservation = table.rows().data();
    var cl = $("#ClientInfoId").val();
    if (cl == "") {
        toastr.warning("Please select the client field","!!Warning");
        return;
    }
        
    for (var i = 0; i < reservation.length; i++) {
            if (i===0) {
                id = reservation.cell(i, 0).data();
            }
            var tid = reservation.cell(i, 0).data();
            var client = reservation.cell(i, 1).data();
            var day = reservation.cell(i, 2).data();
            var halfId = 'HalfKg' + i;
            var svnId = 'SevenAndHalfGm' + i;
            var oneId = 'OneKg' + i;
            var half = reservation.$('input[name=' + halfId + '] ').val();
            var sevenFifty = reservation.$('input[name=' + svnId + '] ').val();
            var one = reservation.$('input[name=' + oneId + '] ').val();
        dto.reservations.push({
                id: tid,
                 ClientInfoId: client,
                dayNumber: parseInt(day),
                halfKg: parseInt(half),
                sevenAndHalfGm: parseInt(sevenFifty),
                oneKg: parseInt(one)
            });
        }

        if (id === "" || id === 0 || id === null) {
            $.ajax({
                url: "/api/Reservations",
                data: dto,
                type: "POST",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Save Success", "Success!!!");
                        refreshForm();

                    } else {
                        toastr.warning("Save Fail", "Warning!!!");
                    }
                },
                error: function (request, status, error) {
                    var response = jQuery.parseJSON(request.responseText);
                    toastr.error(response.message, "Error");
                }
            });
        } else {
            $.ajax({
                url: "/api/Reservations/",
                data: dto,
                type: "PUT",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Info Update Success", "Success!!!");
                        
                        refreshForm();

                    } else {
                        toastr.warning("Save Fail", "Warning!!!");
                    }
                },
                error: function (request, status, error) {
                    var response = jQuery.parseJSON(request.responseText);
                    toastr.error(response.message, "Error");
                }
            });

        }
    });


function refreshForm() {
    $("#Id").val('');
    $("#ClientInfoId").empty();
    $('#reservationTable').DataTable().clear().draw();
}






