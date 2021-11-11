$(document).ready(function() {

    var urlVar = getUrlVars();

    var id = urlVar["id"];

    if (id > 0) {
        getData(id);
    }
    //LOAD SUPPLIER
    $.get("/api/MilkSuppliers",
        function (data) {
            var $el = $("#MilkSuppliersId");
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

    // loadHistoryTable();
});


function DateTimeFormatChange(date) {
    var d = new Date(date.split("-").reverse().join("-"));
    var dd = d.getDate();
    var mm = d.getMonth() + 1;
    var yy = d.getFullYear();
    var newDate = yy + "/" + mm + "/" + dd;
    return newDate;
}

$(document.body).on("click",
    "#btnSubmit",
    function () {
        var dto = {};
        var id = $("#Id").val();
        dto.milkSuppliersId = $("#MilkSuppliersId").val();
        dto.milkQuantity = $("#MilkQuantity").val();
        dto.PurchaseDate = $("#PurchaseDate").val();

        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/MilkPurchases",
                data: dto,
                type: "POST",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Save Success", "Success!!!");
                        loadHistoryTable(vm.milkSuppliersId);
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
            dto.id = id;
            $.ajax({
                url: "/api/MilkPurchases/" + id,
                data: dto,
                type: "PUT",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Info Update Success", "Success!!!");
                        loadHistoryTable(vm.milkSuppliersId);
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
    $("#MilkSuppliersId").val('');
    $("#MilkQuantity").val('');
    $("#PurchaseDate").val('');
}

function getData(id) {
    $.get("/api/MilkPurchases/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#MilkSuppliersId").val(data.milkSuppliersId);
            $("#MilkQuantity").val(data.milkQuantity);
            $("#PurchaseDate").val(data.purchaseDate);
        });
}

$(document.body).on("click",
    ".js-edit",
    function () {
        refreshForm();
        var button = $(this);
        var id = button.attr("data-id");
        getData(id);
    });

$(document.body).on("click", ".js-delete", function () {
    var button = $(this);
    var id = button.attr("data-id");
    bootbox.confirm("Are You Delete This Data?",
        function (result) {
            if (result) {
                $.ajax({
                    url: "/api/MilkPurchases/" + id,
                    method: "DELETE",
                    success: function () {
                        button.parents("tr").remove();
                        toastr.success("Delete Success");
                    },
                    error: function (request, status, error) {
                        var response = jQuery.parseJSON(request.responseText);
                        toastr.error(response.message, "Error");
                    }
                });
            }
        });
});

function loadHistoryTable(supplierId) {

    $("#milkPurchaseHistory").DataTable().destroy();

    $("#milkPurchaseHistory").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/MilkPurchases/GetHistory/?supplierId=" + supplierId,
            dataSrc: ""
        },
        columns: [
            {
                data: "milkSuppliers.name"
            },
            {
                data: "milkQuantity"
            },
            {
                data: "purchaseDate",
                render: function (data) {
                    if (data != "01-01-0001") {
                        return data;
                    } else {
                        return "";
                    }
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<a class='btn btn-info btn-sm js-edit' data-id=" + data + " ><i class='fa fa-pencil-square fa-2x ' aria-hidden='false'></i></a>";
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<a class='btn-link js-delete'  data-id=" + data + "><i class='fa fa-trash fa-2x' aria-hidden='true' style='color: #d9534f;'></i></a>";
                }
            }
        ]
    });
}


$(document.body).on("change", "#MilkSuppliersId", function () {

   var suppliersId = $("#MilkSuppliersId").val();
    if (suppliersId > 0) {
        loadHistoryTable(suppliersId);
    }
});
