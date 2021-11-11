var dtlRowCount = 1;
$(document).ready(function () {

    $("#CustomerId").select2();
    $("#ProductId").select2();

    $.get("/api/Customers", function (data) {
        var $el = $("#CustomerId");
        $el.empty(); // remove old options
        $el.append($("<option></option>")
            .attr("value", '').text(''));
        $.each(data, function (value, key) {
            $el.append($('<option>', {
                value: key.id,
                text: key.name + "  " + key.code
            }));
        });
    });
    generateVoucher();
    getEmptyDtlTable();
    lastInvoiceNo();
});


$(document.body).on("change", "#ProductId", function () {
    // Refresh All Dropdown
    var productId = $("#ProductId").val();
    var typeId = $("#Type").val();
    if (productId > 0) {
        $.ajax({
            type: "GET",
            url: "/api/Products",
            contentType: "application/json; charset=utf-8",
            data: { id: productId },
            success: function (data) {

                $("#Weight").val(data.weight);
                if (typeId == 1) {
                    var cow = data.weight * 420;
                    $("#UnitPrice").val(cow);
                } else {
                    var goat = data.weight * 650;
                    $("#UnitPrice").val(goat);
                }
            }
        });
    }

});


$(document.body).on("change", "#Type", function () {
    // Refresh All Dropdown
    $("#weight").val("");
    $("#UnitPrice").val("");


    var type = $("#Type").val();
    if (type == 1) {

        $.get("/api/Products/GetByType?type="+ type, function (data) {
            var $el = $("#ProductId");
            $el.empty(); // remove old options
            $el.append($("<option></option>")
                .attr("value", '').text(''));
            $.each(data, function (value, key) {
                $el.append($('<option>', {
                    value: key.id,
                    text: key.code
                }));
            });
        });

    } else {
        $.get("/api/Products/GetByType?type=" + type, function (data) {
            var $el = $("#ProductId");
            $el.empty(); // remove old options
            $el.append($("<option></option>")
                .attr("value", '').text(''));
            $.each(data, function (value, key) {
                $el.append($('<option>', {
                    value: key.id,
                    text: key.code
                }));
            });
        });
    }

});




$(document.body).on("change", "#CustomerId", function () {
    // Refresh All Dropdown
    var customerId = $("#CustomerId").val();
    if (customerId > 0) {
        $.ajax({
            type: "GET",
            url: "/api/Customers",
            contentType: "application/json; charset=utf-8",
            data: { id: customerId },
            success: function (data) {
                $("#SearchName").val(data.name);
                $("#SearchPhoneNo").val(data.contact);
            }
        });
    }

});



//Save


$(document.body).on("click", "#btnSubmit", function () {
    var dto = {
        salesDetails: []
    };
    var id = $("#Id").val();
    dto.invoiceNo = $("#InvoiceNo").val();
    dto.customerId = $("#CustomerId").val();
    dto.salesDate = $("#SalesDate").val();
    dto.totalBill = $("#TotalBill").val();
    dto.transportCost = $("#TransportCost").val();
    dto.discount = $("#Discount").val();
    dto.payable = $("#Payable").val();
    dto.payAmount = $("#PayAmount").val();

    // dtls
    var dtlTable = $('#dtlTable').DataTable();
    var dtls = dtlTable.rows().data();

    for (var r = 0; r < dtls.length; r++) {
        dto.salesDetails.push({
            id: dtls.cell(r, 1).data(),
            productId: dtls.cell(r, 2).data(),
            weight: dtls.cell(r, 4).data(),
            unitPrice: dtls.cell(r, 5).data()
        });
    }


    if (id == "" || id == 0 || id == null) {
        $.ajax({
            url: "/api/Sales",
            data: dto,
            type: "POST",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Data Save Success", "Success!!!");
                    refreshForm();
                    getEmptyDtlTable();
                    generateVoucher();
                    lastInvoiceNo();
                } else {
                    toastr.warning("Data Save Fail.", "Warning!!!");
                }
            },
            error: function (request, status, error) {
                var response = jQuery.parseJSON(request.responseText);
                toastr.error(response.errorMassage, "Error");
            }
        });
    } else {
        dto.id = id;

        $.ajax({
            url: "/api/Sales/" + id,
            data: dto,
            type: "PUT",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Data Update Success", "Success!!!");
                    refreshForm();
                    getEmptyDtlTable();
                    generateVoucher();
                    lastInvoiceNo();
                } else {
                    toastr.warning("Data Update Fail.", "Warning!!!");
                }
            },
            error: function (request, status, error) {
                var response = jQuery.parseJSON(request.responseText);
                toastr.error(response.errorMassage, "Error");
            }
        });
    }
});



function refreshForm() {
    $("#CustomerId").val("");
    $("#SalesDate").val("");
    $("#CustomerName").val("");
    $("#PhoneNo").val("");
    $("#TotalBill").val("");
    $("#Discount").val("");
    $("#TransportCost").val("");
    $("#Payable").val("");
    $("#PayAmount").val("");
}






//  Dtl Table
function getEmptyDtlTable() {
    $("#dtlTable").DataTable().destroy();
    var t = $('#dtlTable').DataTable({
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
            },
            {
                "targets": [2],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [6],
                "data": null,
                "defaultContent":
                    "<a class='btn btn-info btn-sm js-dtlEdit'><i class='fa fa-edit' aria-hidden='true'></i></a>" +
                    "<a class='btn btn-danger btn-sm js-dtlDelete'><i class='fa fa-trash' aria-hidden='true'></i></a>"
            }
        ],
        "searching": false,
        "paging": false,
        "ordering": false,
        "info": false
    });
    t.clear().draw();
}

$(document.body).on("click",
    "#btnAdd",
    function () {
        var rowId = $("#dtlRowHiden").val();
        var dtlId = $("#dtlIdHiden").val();
        var productId = $("#ProductId").val();
        var weight = $("#Weight").val();
        var productNo = $("#ProductId option:selected").text();
        var unitPrice = $("#UnitPrice").val();
        if (productId > 0 ) {

            var t = $('#dtlTable').DataTable();

            if (rowId > 0) {
                // row update
                var temp = t.row(rowId - 1).data();
                temp[1] = dtlId;
                temp[2] = productId;
                temp[3] = productNo;
                temp[4] = weight;
                temp[5] = unitPrice;
                $('#dtlTable').dataTable().fnUpdate(temp, rowId - 1, undefined, false);
            }
            else {
                // row add
                t.row.add([
                    dtlRowCount,
                    dtlId,
                    productId,
                    productNo,
                    weight,
                    unitPrice
                ]).draw(false);

                dtlRowCount++;
            }
            refreshDtlInputFiled();

            var totalAmount = t.column(5).data().sum();
            $("#TotalBill").val(totalAmount);
            $("#Payable").val(totalAmount);

        }
        else {
            toastr.warning("Please Select Cow / Goat.", "Warning!!!");
            $("#ProductId").focus();
        }
    });


$(document.body).on("click", ".js-dtlEdit", function () {
    var t = $('#dtlTable').DataTable();
    var data = t.row($(this).parents('tr')).data();

    $("#dtlRowHiden").val(data[0]);
    $("#dtlIdHiden").val(data[1]);

    var productId = data[2];
    $("#ProductId").val(productId);

    $("#Weight").val(data[4]);
    $("#UnitPrice").val(data[5]);

    $.get("/api/Products?id=" + productId)
        .done(function (data) {
            $("#Type").val(data.type);
        });

});

$(document.body).on("click", ".js-dtlDelete", function () {
    var button = $(this);
    bootbox.confirm("Are you sure to delete this data?", function (result) {
        if (result) {
            var t = $('#dtlTable').DataTable();
            t.row(button.parents("tr")).remove().draw(false);
            toastr.success("Data successfully delete");
        }
    });
});

function refreshDtlInputFiled() {
    $("#dtlRowHiden").val("");
    $("#dtlIdHiden").val(0);
    $("#ProductId").val("");
    $("#ProductId option:selected").text("");
    $("#Weight").val("");
    $("#UnitPrice").val("");
}


function generateVoucher() {
    $.get("/api/Sales/GetVoucherNumber")
        .done(function (data) {
            $("#InvoiceNo").val(data);
        });
}


function lastInvoiceNo() {
    $.get("/api/Sales/LastInvoiceNo")
        .done(function (data) {
            $("#lastInvoiceNo").val(data);
        });
}





//Discount Key Up

$("#TransportCost").on("keyup change",
    function (e) {
      
        var transport = parseFloat($("#TransportCost").val());
        payable(transport);
    });

function payable(transport) {
    var totalBill = parseFloat($("#TotalBill").val());
    if (transport > 0) {
        totalBill = totalBill + transport;
    }
    $("#Payable").val(totalBill);
};

//For Report
$(document.body).on("click", "#btnPrint", function () {

    var invoiceNo = $("#lastInvoiceNo").val();

    if (invoiceNo !== "") {

        $.ajax({
            url: "/CowSales/BillReportForm?invoiceNo="+ invoiceNo,
            type: 'POST',
            success: function (e) {
                if (e != "" && e != null) {
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

