$(document).ready(function() {
    $("#AreaId").select2();
    $("#ClientId").select2();

    refreshTable();
});

$(document.body).on("click", "#btnGenerate", function () {
    var model = {};
    var clientId = $("#ClientId").val();
    var month = $("#MonthId").val();
    var year = $("#Year").val();


    if (clientId === "" || month === "" || year == "") {
        toastr.warning("Pleas Select Area and Client");
        return;
    }
    model.ClientId = parseInt(clientId);
    model.areaId = $("#AreaId").val();
    model.MonthId = month;
    model.year = year;
    model.amountType = $("#AmountType").val();
    model.dueAmount = $("#DueAmount").val();

    model.oneFourthKg = $("#OneFourthKg").val();
    model.halfKg = $("#HalfKg").val();
    model.oneKg = $("#OneKg").val();

    model.oilOneKg = $("#OilOneKg").val();
    model.oilTwoKg = $("#OilTwoKg").val();
    model.oilFiveKg = $("#OilFiveKg").val();

    model.muriHalfKg = $("#MuriHalfKg").val();
    model.muriOneKg = $("#MuriOneKg").val(); 




    if (model.dueAmount > 0) {
        $.ajax({
            url: "/api/DueBills",
            data: model,
            type: "POST",
            success: function (d) {
                if (d > 0) {
                    // toastr.success("Save Success", "Success!!!");
                    $.ajax({
                        url: "/Reports/BillReportForm",
                        data: model,
                        type: 'POST',
                        success: function (e) {
                            if (e != "" && e != null) {
                                $("#OneFourthKg").val("");
                                $("#HalfKg").val("");
                                $("#OneKg").val("");
                                $("#OilOneKg").val("");
                                $("#OilTwoKg").val("");
                                $("#OilFiveKg").val("");
                                $("#MuriHalfKg").val("");
                                $("#MuriOneKg").val(""); 


                                $("#DueAmount").val("");

                                setTimeout(function () {
                                    $("#pdf").attr("href", e);
                                    var reportBox = $("#pdf").fancybox({
                                        'frameWidth': 100,
                                        'frameHeight': 495,
                                        'overlayShow': true,
                                        'hideOnContentClick': false,
                                        'type': 'iframe',
                                        helpers: {
                                            // prevents closing when clicking OUTSIDE fancybox
                                            overlay: { closeClick: false }
                                        }
                                    }).trigger('click');
                                }, 1000);
                            }
                        }
                    });

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
            url: "/Reports/BillReportForm",
            data: model,
            type: 'POST',
            success: function (e) {
                if (e != "" && e != null) {

                    $("#OneFourthKg").val("");
                    $("#HalfKg").val("");
                    $("#OneKg").val("");


                    $("#OilOneKg").val("");
                    $("#OilTwoKg").val("");
                    $("#OilFiveKg").val("");

                    $("#DueAmount").val("");

                    setTimeout(function () {
                        $("#pdf").attr("href", e);
                        var reportBox = $("#pdf").fancybox({
                            'frameWidth': 100,
                            'frameHeight': 495,
                            'overlayShow': true,
                            'hideOnContentClick': false,
                            'type': 'iframe',
                            helpers: {
                                // prevents closing when clicking OUTSIDE fancybox
                                overlay: { closeClick: false }
                            }
                        }).trigger('click');
                    }, 1000);
                }
            }
        });
    }
 
});

$(document.body).on("change", "#AreaId", function () {
    // Refress All Dropdown
    $("#ClientId").empty();

    var classId = $("#AreaId").val();
    if (classId > 0) {
        $.ajax({
            type: "GET",
            url: "/api/ClientInfos/GetClientByArea",
            contentType: "application/json; charset=utf-8",
            data: { areaId: classId },
            success: function (data) {
                var $el = $("#ClientId");
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




//For Milk Sale

function refreshTable() {
    $("#salesTable").DataTable().destroy();
    var t = $('#salesTable').DataTable({
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
var half = 0;
var sevenHalf = 0;
var one = 0;
function initialTable() {
    $.get("/api/ClientInfos?id=" + $("#ClientId").val(),
        function (data) {
            if (data) {
                half = data.halfKg > 0 ? data.halfKg : 0;
                sevenHalf = parseInt(data.sevenAndHalfGm) > 0 ? data.sevenAndHalfGm : 0;
                one = parseInt(data.oneKg) > 0 ? data.oneKg : 0;
                dayInterval = parseInt(data.dayInterval) > 0 ? data.dayInterval : 0;
            }

            $("#salesTable").DataTable().destroy();
            var t = $('#salesTable').DataTable({
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
            var interval = 0;
            var monthDuration = monthLength($("#MonthId").val());
            for (var i = 0; i < monthDuration; i++) {

                var startDate = $("#StartDate").val();
                if (parseInt(startDate) === count) {
                    t.row.add([
                        0,
                        $("#ClientId").val(),
                        count,
                        "<input style='width: 100px' type='text' id='HalfKg" + i + "' name='HalfKg" + i + "' value= " + half + ">",
                        "<input style='width: 100px' type='text' id='SevenAndHalfGm" + i + "' name='SevenAndHalfGm" + i + "' value=" + sevenHalf + ">",
                        "<input style='width: 100px' type='text' id='OneKg" + i + "' name='OneKg" + i + "' value=" + one + ">"
                    ]).draw(false);
                    ++count;
                    interval = 0;
                } else {
                    var check = parseInt(startDate) + parseInt(dayInterval);
                    if (parseInt(dayInterval) === interval && count > check) {
                        t.row.add([
                            0,
                            $("#ClientId").val(),
                            count,
                            "<input style='width: 100px' type='text' id='HalfKg" + i + "' name='HalfKg" + i + "' value= " + half + ">",
                            "<input style='width: 100px' type='text' id='SevenAndHalfGm" + i + "' name='SevenAndHalfGm" + i + "' value=" + sevenHalf + ">",
                            "<input style='width: 100px' type='text' id='OneKg" + i + "' name='OneKg" + i + "' value=" + one + ">"
                        ]).draw(false);
                        ++count;
                        interval = 0;
                    } else {
                        ++interval;
                        t.row.add([
                            0,
                            $("#ClientId").val(),
                            count,
                            "<input style='width: 100px' type='text' id='HalfKg" + i + "' name='HalfKg" + i + "' value= '0'>",
                            "<input style='width: 100px' type='text' id='SevenAndHalfGm" + i + "' name='SevenAndHalfGm" + i + "' value='0'>",
                            "<input style='width: 100px' type='text' id='OneKg" + i + "' name='OneKg" + i + "' value='0'>"
                        ]).draw(false);
                        ++count;
                    }
                }

            }

        });


}
function editTable(clientId) {
    $("#salesTable").DataTable().destroy();
    var t = $('#salesTable').DataTable({
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
            url: "/api/PacketSales/GetByClientId?clientId=" + clientId,
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
                    ++count;
                    if (data) {
                        return "<input style='width: 100px' type='text' id='OneKg" + count + "' name='OneKg" + count + "' value=" + data + ">";
                    } else {
                        return "<input style='width: 100px' type='text' id='OneKg" + count + "' name='OneKg" + count + "' value='0'>";
                    }
                }
            }
        ]
    });
}

$(document.body).on("change", "#ClientId", function () {
    $('#salesTable').DataTable().clear().draw();
    refreshTable();
});


//$(document.body).on("change", "#MonthId", function () {
//    var client = $("#ClientId").val();
//    var month = $("#MonthId").val();
//    if (client.length < 1 || month.length < 1) {
//        toastr.warning("Please fill up all required fields", "Warning!!");
//        return;
//    }
//    isSalesExist(client, month);
//});


function isSalesExist(client, month) {
    $.get("/api/PacketSales/GetStatus", { clientId: client, month: month },
        function (data) {
            if (data) {
                toastr.warning("Sales already exist for this customer in this month", "!!Warning");
                $('#salesTable').DataTable().clear().draw();
                refreshTable();
                return;
            }
        });
}


$(document.body).on("click", "#btnLoad", function () {
    refreshTable();
    var client = $("#ClientId").val();
    var month = $("#MonthId").val();
    var startDate = $("#StartDate").val();
    if (client.length < 1 || month.length < 1 || startDate.length < 1) {
        toastr.warning("Please fill up all required fields", "Warning!!");
        return;
    }

    $.get("/api/PacketSales/GetStatus", { clientId: client, month: month },
        function (data) {
            if (data) {
                toastr.warning("Sales already exist for this customer in this month", "!!Warning");
                $('#salesTable').DataTable().clear().draw();
                return;
            } else {
                initialTable();
            }
        });


    //$.get("/api/Reservations/GetByClientId", { clientId: client },
    //    function (data) {
    //        console.log(data.length);
    //        if (data.length > 0) {
    //            initialTable(client);
    //        } else {
    //            toastr.warning("Reservation not yet for this customer", "Warning!!");
    //            $('#salesTable').DataTable().clear().draw();
    //            //$("#salesTable").DataTable().destroy();
    //            refreshTable();
    //        }
    //    });
});

$(document.body).on("click", "#btnSubmit", function () {
    var clt = $("#ClientId").val();
   // var id = $("#Id").val();
    var month = $("#MonthId").val();
        var dto = {
            packetSaleDtos: []
        };
        var areaId = $("#AreaId").val();
        var year = $("#Year").val();


        var dtlTable = $('#salesTable').DataTable();
        var details = dtlTable.rows().data();
        if (details.length < 1) {
            toastr.warning("Please fill up the all required field", "Warning!!!");
            return;
        }
        for (var i = 0; i < details.length; i++) {
            var halfId = 'HalfKg' + i;
            var svnId = 'SevenAndHalfGm' + i;
            var oneId = 'OneKg' + i;
            var half = dtlTable.$('input[name=' + halfId + '] ').val();
            var sevenFifty = dtlTable.$('input[name=' + svnId + '] ').val();
            var one = dtlTable.$('input[name=' + oneId + '] ').val();
            var client = details.cell(i, 1).data();
            var day = details.cell(i, 2).data();
            dto.packetSaleDtos.push({
                areaId: areaId,
                dayNumber: day,
                salesMonth: month,
                year: year,
                clientInfoId: client,
                halfKg: parseFloat(half),
                sevenAndHalfGm: parseFloat(sevenFifty),
                oneKg: parseFloat(one)
            });
        }
        $.ajax({
            url: "/api/PacketSales",
            data: dto,
            type: "POST",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Save Success", "Success!!!");
                    $("#StartDate").val("");
                    $('#salesTable').DataTable().clear().draw();

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