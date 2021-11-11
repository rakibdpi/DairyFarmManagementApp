$(document).ready(function () {

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

});

function loadHistoryTable(supplierId) {

    $("#milkPurchaseHistory").DataTable().destroy();

    $("#milkPurchaseHistory").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/MilkPurchases/GetMilkPurchaseBySupplier/?supplierId=" + supplierId,
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
                    return "<a class='btn btn-info btn-sm js-edit' href='/MilkPurchases/MilkPurchaseForm?id=" + data + "'  ><i class='fa fa-pencil-square fa-2x ' aria-hidden='false'></i></a>";
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