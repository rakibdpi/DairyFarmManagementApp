$(document).ready(function() {
    $("#areaDiv").show();
    $("#customerDiv").hide();
    $("#MobileDiv").hide();
    $("#QuantityDiv").hide();
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
    if (month === "January" || month === "March" || month === "May" || month === "July" || month === "August" || month === "October" || month ==="December") {
        return 31;
    }
    else if (month === "April" || month === "June" || month === "September" || month ==="November") {
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
    $.get("/api/ClientInfos?id=" + $("#ClientInfoId").val(),
        function (data) {
            if (data) {
                half = data.halfKg >0 ? data.halfKg : 0;
                sevenHalf = parseInt(data.sevenAndHalfGm) > 0 ? data.sevenAndHalfGm : 0;
                one = parseInt(data.oneKg) > 0 ? data.oneKg: 0;
                dayInterval = parseInt(data.dayInterval) > 0 ? data.dayInterval: 0;
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
            var monthDuration = monthLength($("#SalesMonth").val());
            for (var i = 0; i < monthDuration; i++) {
                
                var startDate = $("#StartDate").val();
                if (parseInt(startDate) === count) {
                    t.row.add([
                        0,
                        $("#ClientInfoId").val(),
                        count,
                        "<input style='width: 100px' type='text' id='HalfKg" + i + "' name='HalfKg" + i + "' value= " + half + ">",
                        "<input style='width: 100px' type='text' id='SevenAndHalfGm" + i + "' name='SevenAndHalfGm" + i + "' value=" + sevenHalf + ">",
                        "<input style='width: 100px' type='text' id='OneKg" + i + "' name='OneKg" + i + "' value=" + one + ">"
                    ]).draw(false);
                    ++count;
                    interval = 0;
                } else {
                    var check = parseInt(startDate) + parseInt(dayInterval);
                    if (parseInt(dayInterval) === interval && count >  check  ) {
                        t.row.add([
                            0,
                            $("#ClientInfoId").val(),
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
                            $("#ClientInfoId").val(),
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
$(document.body).on("change", "#ClientType", function () {
    var type = $("#ClientType").val();

    if (parseInt(type) === 1) {
        $("#areaDiv").show();
        $("#clientDiv").show();
        $("#customerDiv").hide();
        $("#MobileDiv").hide();
        $("#QuantityDiv").hide();
        $("#salesTable").show();
    }
    if (parseInt(type) === 2) {
        $("#areaDiv").hide();
        $("#customerDiv").show();
        $("#MobileDiv").show();
        $("#QuantityDiv").show();
        $("#salesTable").hide();
        $("#clientDiv").hide();
    }

});
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
                        text: key.code+" "+ key.name
                    }));
                });
            }
        });
    }

});
$(document.body).on("change", "#ClientInfoId", function () {
    $('#salesTable').DataTable().clear().draw();
    refreshTable();
});
$(document.body).on("change", "#SalesMonth", function () {
    var client = $("#ClientInfoId").val();
    var month = $("#SalesMonth").val();
    if (client.length < 1 || month.length < 1) {
        toastr.warning("Please fill up all required fields", "Warning!!");
        return;
    }
    isSalesExist(client, month);
});

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
    var client = $("#ClientInfoId").val();
    var month = $("#SalesMonth").val();
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
    var clt = $("#ClientInfoId").val();
    var type = $("#ClientType").val();
    var id = $("#Id").val();
    var month = $("#SalesMonth").val();
    if (parseInt(type) === 1) {
        var dto = {
            packetSaleDtos: []
        };
        var areaId = $("#AreaId").val();

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
                if (e >0) {
                    toastr.success("Save Success", "Success!!!");
                    getNextClient(areaId, clt);
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

    if (parseInt(type) === 2) {
        var model = {};
        model.customerName = $("#CustomerName").val();
        model.mobileNo = $("#MobileNo").val();
        model.quantity = $("#Quantity").val();
        model.salesMonth = month;

        if (id === 0 || id === null || id==="") {
            $.ajax({
                url: "/api/RowSales",
                data: model,
                type: "POST",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Save Success", "Success!!!");
                        ClearForm();

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
    }

});
$(document.body).on("click", "#btnCancel", function() {
    ClearForm();
});
$(document.body).on("click", "#btnPrevious", function () {
    $('#salesTable').DataTable().clear().draw();
    var client = $("#ClientInfoId").val();
    var areaId = $("#AreaId").val();
    if (client.length < 1) {
        toastr.warning("Please Select Client First");
        return;
    }
    $.ajax({
        url: "/api/ClientInfos/GetNextClientByNumber",
        data: { areaId: areaId, clientId: client, number: -1 },
        type: "GET",
        success: function (e) {
            if (e) {
                getClient(e.id);
                dayInterval = parseInt(e.dayInterval) > 0 ? e.dayInterval : 0;
            }
        },
        error: function (request, status, error) {
            var response = jQuery.parseJSON(request.responseText);
            toastr.error(response.message, "Error");
        }
    });
});
$(document.body).on("click", "#btnNext", function () {
    $('#salesTable').DataTable().clear().draw();
    var client = $("#ClientInfoId").val();
    var areaId = $("#AreaId").val();
    if (client.length < 1) {
        toastr.warning("Please Select Client First");
        return;
    }
    $.ajax({
        url: "/api/ClientInfos/GetNextClientByNumber",
        data: { areaId: areaId, clientId: client, number: 1 },
        type: "GET",
        success: function (e) {
            if (e) {
                getClient(e.id);
                dayInterval = parseInt(e.dayInterval) > 0 ? e.dayInterval : 0;
            } 
        },
        error: function (request, status, error) {
            var response = jQuery.parseJSON(request.responseText);
            toastr.error(response.message, "Error");
        }
    });
});
function ClearForm() {
    location.reload();
}
function getNextClient(area, recentClient) {
    $("#CustomerName").val("");
    $("#MobileNo").val("");
    $("#Quantity").val("");
    $("#StartDate").val("");
    $.get("/api/ClientInfos/GetNextClient", { areaId: area, clientId: recentClient },
        function (data) {
            if (data) {
                getClient(data.id);
                dayInterval = parseInt(data.dayInterval) > 0 ? data.dayInterval : 0;
            }
        });
    $('#salesTable').DataTable().clear().draw();
}
function getClient(clientId) {
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
                $el.val(clientId);
            }
        });   
    }
}

