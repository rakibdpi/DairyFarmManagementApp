$(document).ready(function () {

    $("#billDetails").hide();


    $("#recentBill").hide();

    $("#totalBill").text('');
    $("#collectionBill").text('');
    $("#dueBill").text('');
    $("#recentBill").text('');



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



    $("#paymentsTable").DataTable().destroy();

    $("#paymentsTable").DataTable({
        retrieve: true,
        paging: true,
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
    var areaId = $("#AreaId").val();
    var clientId = $("#ClientInfoId").val();
    var year = $("#Year").val();
    var month = $("#Month").val();

    if (areaId.length < 1 || year.length < 1 || month.length < 1) {
        toastr.warning("Please Select Required Fields", "Warning!!");
        return;
    }
   
    loadTable(areaId, clientId, year, month);
});
var count = -1;
function loadTable(areaId, clientId, year, month) {

    $("#billDetails").hide();
    $("#recentBill").hide();

    $("#totalBill").text('');
    $("#collectionBill").text('');
    $("#dueBill").text('');
    $("#recentBill").text('');
    


    $("#paymentsTable").DataTable().destroy();

    $("#paymentsTable").DataTable({
        retrieve: false,
        paging: false,
        searching: false,
        info: false,
        rowRecoder: {
            selector: 'td:nth-child(1)'
        },
        dom: ' <l Bfrtip><>',
        buttons: [
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                },
                title: 'Due Bill List'
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6]
                },
                title: 'Due Bill List'
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: [2, 3, 4, 5, 6]
                },
                text: '<u>P</u>rint',
                key: {
                    key: 'p',
                    altKey: true
                },
                title: 'Due Bill List'
            },
        ],
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
            url: "/Payments/GetClients",
            data: { areaId: areaId, year: year, month: month, clientId: clientId },
            dataSrc: ""
        },
        columns: [
            {
                data: "areaId"
            },
            {
                data: "id"
            },
            {
                data: "areaName"
            },
            {
                data: "code",
                render: function(data) {
                    if (data) {
                        return data;
                    } else {
                        return "";
                    }
                }
            },
            {
                data: "name"
            },
            {
                data: "phoneNo",
                render: function (data) {
                    if (data) {
                        return data;
                    } else {
                        return "";
                    }
                }
            },
            {
                data: "billAmount",
                render: function (data) {
                    if (data) {
                        return data;
                    } else {
                        return "";
                    }
                }
            },
            {
                data: "id",
                render: function (data) {
                    ++count;
                    return "<input style='width: 100px' type='text' id='Amount" +
                        count +
                        "' name='Amount" +
                        count +
                        "' value= " +
                        0 +
                        ">";
                }
            }
        ]
        
    });
}




//Details Bill


$(document.body).on("click", "#btnDetails", function () {
    var areaId = $("#AreaId").val();
    var clientId = $("#ClientInfoId").val();
    var year = $("#Year").val();
    var month = $("#Month").val();

    if (areaId.length < 1 || year.length < 1 || month.length < 1) {
        toastr.warning("Please Select Required Fields", "Warning!!");
        return;
    }


    $("#billDetails").show();

    $("#totalBill").text('');
    $("#collectionBill").text('');
    $("#dueBill").text('');
    $("#recentBill").text('');
    



    GetBillHistory(areaId, year, month);
});





//Get Bill History
function GetBillHistory(areaId, year, month) {

    $.get("/Payments/GetBillInfo", { areaId: areaId, year: year, month: month })
        .done(function (data) {
            $("#totalBill").text('Total Bill : ' + data.totalBill);
            $("#collectionBill").text('Total Collection : ' + data.collectionBill);
            $("#dueBill").text('Total Due : ' + data.dueBill);
            $("#recentBill").text("");
            
        });

}



//Sms send

$(document.body).on("click", "#btnDueBill", function () {

    var dto = {};
    dto.areaId = $("#AreaId").val();
    dto.clientId = $("#ClientInfoId").val();
    dto.year = $("#Year").val();
    dto.month = $("#Month").val();

    if (dto.areaId.length < 1 || dto.year.length < 1 || dto.month.length < 1) {
        toastr.warning("Please Select Required Fields", "Warning!!");
        return;
    }

    $.ajax({
        url: "/Payments/DueBillSms",
        data: dto,
        type: "POST",
        success: function (e) {
            if (e > 0) {
                toastr.success("Message Successfully Sent", "Success!!!");
            } else {
                toastr.warning("Message sent failed", "Warning!!!");
            }
        },
        error: function (request, status, error) {
            var response = jQuery.parseJSON(request.responseText);
            toastr.error(response.message, "Error");
        }
    });




});





$(document.body).on("click", "#btnSubmit", function () {
    var dto = {
        paymentDtos: []
    };
    var year = $("#Year").val();
    var month = $("#Month").val();

    if (year.length < 1 || month.length < 1) {
        toastr.warning("Please select year and month", "Warning!!!");
        return;
    }

    var dtlTable = $('#paymentsTable').DataTable();
    var details = dtlTable.rows().data();
    if (details.length < 1) {
        toastr.warning("Please fill up the all required field", "Warning!!!");
        return;
    }

    for (var i = 0; i < details.length; i++) {
        var amountId = 'Amount' + i;
        var amount = dtlTable.$('input[name=' + amountId + '] ').val();
        var areaId = details.cell(i, 0).data();
        var clientId = details.cell(i, 1).data();
        if (parseFloat(amount) > 1) {
            dto.paymentDtos.push({
                month: month,
                year: year,
                areaId: areaId,
                clientInfoId: clientId,
                billAmount: parseFloat(amount)
            });
        }
        
    }

    $.ajax({
        url: "/api/Payments",
        data: dto,
        type: "POST",
        success: function (e) {
            if (e > 0) {
                $('#paymentsTable').DataTable().clear().draw();
                toastr.success("Bill Receive Successfully", "Success!!!");
                $("#totalBill").text('');
                $("#collectionBill").text('');
                $("#dueBill").text('');

                $("#recentBill").show();

                $("#recentBill").text("Recent Collect : " + e);
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
    location.reload();
});



$(document.body).on("click", "#btnSummary", function () {
    var dto = {};
     dto.year = $("#Year").val();
     dto.month = $("#Month").val();


    if (dto.year.length > 0 && dto.month.length > 0) {
        $.ajax({
            url: "/Payments/BillReport",
            data: dto,
            type: "POST",
            success: function (data) {
                if (data != "" && data != null) {
                    setTimeout(function () {
                        $("#pdf").attr("href", data);
                        var reportBox = $("#pdf").fancybox({
                            'frameWidth': 85,
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
        toastr.warning("Select Year AND Month", "Warning!!!");
    }

});