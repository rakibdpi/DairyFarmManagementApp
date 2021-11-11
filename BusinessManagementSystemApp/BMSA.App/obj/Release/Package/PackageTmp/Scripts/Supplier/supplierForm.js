$(document).ready(function () {

    var urlVar = getUrlVars();

    var id = urlVar["id"];

    if (id > 0) {
        getData(id);
    }
    loadHistoryTable();
});


$(document.body).on("click", "#btnSubmit", function () {
    var dto = {};
    var id = $("#Id").val();
    dto.name = $("#Name").val();
    dto.code = $("#Code").val();
    dto.address = $("#Address").val();
    dto.email = $("#Email").val();
    dto.contact = $("#Contact").val();
    dto.contactPerson = $("#ContactPerson").val();
    dto.imagePath = $("#ImagePath").val();


    if (id == "" || id == 0 || id == null) {
        $.ajax({
            url: "/api/Suppliers",
            data: dto,
            type: "POST",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Save Success", "Success!!!");
                    refressForm();
                    loadHistoryTable();

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
            url: "/api/Suppliers/" + id,
            data: dto,
            type: "PUT",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Update Success", "Success!!!");
                    refressForm();
                    loadHistoryTable();

                } else {
                    toastr.warning("Update Fail", "Warning!!!");
                }
            },
            error: function (request, status, error) {
                var response = jQuery.parseJSON(request.responseText);
                toastr.error(response.message, "Error");
            }
        });

    }


});

function refressForm() {
    $("#Id").val("");
    $("#Code").val("");
    $("#Name").val("");
    $("#Address").val("");
    $("#Email").val("");
    $("#Contact").val("");
    $("#ContactPerson").val("");
    $("#ImagePath").val("");
}

function loadHistoryTable() {
    $("#supplierHisotryTable").DataTable().destroy();

    $("#supplierHisotryTable").DataTable({
        rowRecoder: {
            selector: 'td:nth-child(1)'
        },
        responsive: true,
        ajax: {
            url: "/api/Suppliers",
            dataSrc: ""
        },
        columns: [
            {
                data: "code"

            },
            {
                data: "name"

            },
            {
                data: "address"
            },
            {
                data: "email"
            },
            {
                data: "contact"
            },
            {
                data: "contactPerson"
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



$(document.body).on("click",
    ".js-edit",
    function () {
        refressForm();
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
                    url: "/api/Suppliers/" + id,
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

function getData(id) {
    $.get("/api/Suppliers/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#Name").val(data.name);
            $("#Code").val(data.code);
            $("#Address").val(data.address);
            $("#Email").val(data.email);
            $("#Contact").val(data.contact);
            $("#ContactPerson").val(data.contactPerson);
            $("#ImagePath").val(data.imagePath);
        });
}
