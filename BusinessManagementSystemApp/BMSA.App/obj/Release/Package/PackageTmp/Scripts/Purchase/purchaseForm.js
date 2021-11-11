$(document).ready(function () {

    var urlVar = getUrlVars();

    var id = urlVar["id"];

    //if (id > 0) {
    //    getData(id);
    //}
    //loadHistoryTable();

    $.ajax({
        type: "GET",
        url: "/api/Suppliers",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $el = $("#SupplierId");
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

    $.ajax({
        type: "GET",
        url: "/api/Products",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $el = $("#ProductId");
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


    $('#purchaseHistory').DataTable({
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }

        ],
        "searching": false,
        "paging": false,
        select: true
    });

});


$(document.body).on("change", "#ProductId", function() {

    $("#PreviousMrpTk").val("");
    $("#PreviousCostPrize").val("");
    $("#Code").val("");


    var productId = $("#ProductId").val();

    if (productId > 0) {

        $.get("/api/Products", { id: productId })
            .done(function (data) {
                if (data != null) {
                    $("#Code").val(data.code);
                }
            });

        $.get("/api/Purchase/GetPreviousPriceProduct", { productId: productId })
            .done(function (data) {
                if (data != null) {
                    $("#PreviousMrpTk").val(data.mrpTk);
                    $("#PreviousCostPrize").val(data.unitPrize);
                }
            });
    }
});


$("#UnitPrize").change(function () {

    var quantity = $("#Quantity").val();
    var unitPrice = $("#UnitPrize").val();

    var totalPrice = quantity * unitPrice;

    $("#TotalTk").val(totalPrice);

    var profit = (unitPrice * 25) / 100;

    var newMrp = parseInt(profit) + parseInt(unitPrice);

    $("#MrpTk").val(newMrp);

});



$(document.body).on("click", "#btnAdd", function () {
    var t = $("#purchaseHistory").DataTable();


    var productId = $("#ProductId").val();
    var product = $("#ProductId option:selected").text();
    var code = $("#Code").val();
    var manufacturedDate = $("#ManufacturedDate").val();
    var expireDate = $("#ExpireDate").val();
    var quantity = $("#Quantity").val();
    var unitPrize = $("#UnitPrize").val();
    var totalTk = $("#TotalTk").val();
    var mrpTk = $("#MrpTk").val();


    if (productId < 0 ) {
        toastr.error("Please Select Product.");
    }
    else if (manufacturedDate == "" && manufacturedDate === null) {
        toastr.error("Manufactured Date Field Is Required");
    }
    else if (expireDate == "" && expireDate == null) {
        toastr.error("Expire Date Field Is Required");
    }
    else if (quantity == "" && quantity == null) {
        toastr.error("Quantity Field Is Required");
    }
    else if (unitPrize == "" && unitPrize == null) {
        toastr.error("UnitPrize Field Is Required");
    }
    else if (mrpTk == "" && mrpTk == null) {
        toastr.error("New Mrp Field Is Required");
    } else {
        t.row.add([
            productId,
            code,
            manufacturedDate,
            expireDate,
            quantity,
            unitPrize,
            totalTk,
            mrpTk

        ]).draw(false);

        $("#ProductId option[value='" + productId + "']").remove();
        $("#Code").val("");
        $("#ManufacturedDate").val("");
        $("#ExpireDate").val("");
        $("#Quantity").val("");
        $("#UnitPrize").val("");
        $("#TotalTk").val("");
        $("#MrpTk").val("");
    }
});



// Save Button Click
$(document.body).on("click",
    "#btnSubmit",
    function () {

        var purchaseHistoryTable = $('#purchaseHistory').DataTable();
        var purchaseHistories = purchaseHistoryTable.rows().data();

        var count = 0;
        for (var i = 0; i < purchaseHistories.length; i++) {
            count += parseInt(purchaseHistories.cell(i, 6).data());
        }
    
            // Center Model
            var dto = {
                purchaseDetails: []
            };

            dto.purchaseDate = $("#PurchaseDate").val();
            dto.billOrInvoiceNo = $("#BillOrInvoiceNo").val();
            dto.code = $("#Code").val();
            dto.supplierId = $("#SupplierId").val();



            for (var r = 0; r < purchaseHistories.length; r++) {
                dto.purchaseDetails.push({
                    ProductId: purchaseHistories.cell(r, 0).data(),
                    Code: purchaseHistories.cell(r, 1).data(),
                    ManufacturedDate: purchaseHistories.cell(r, 2).data(),
                    ExpireDate: purchaseHistories.cell(r, 3).data(),
                    Quantity: purchaseHistories.cell(r, 4).data(),
                    UnitPrize: purchaseHistories.cell(r, 5).data(),
                    TotalTk: purchaseHistories.cell(r, 6).data(),
                    MrpTk: purchaseHistories.cell(r, 7).data()
                });
            }

            $.ajax({
                url: "/api/Purchase",
                method: "post",
                data: dto,
                success: function (data) {
                    if (data > 0) {
                        toastr.success("Purchase successfully recorded.");
                        location.reload(true);
                    } else {
                        toastr.error("Purchase Save Fail.");
                    }
                },
                error: function (data) {
                    toastr.error("Purchase Save Fail.");
                }
            });
    });



