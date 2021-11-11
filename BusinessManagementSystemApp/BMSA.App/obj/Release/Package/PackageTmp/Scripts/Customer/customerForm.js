$(document).ready(function () {

    var urlVar = getUrlVars();

    var id = urlVar["id"];

    if (id > 0) {
        getData(id);
    }
    loadHistoryTable();

    generateCode();
});

function generateCode() {
    $.get("/api/ClientInfos/GetClientCode")
        .done(function (data) {
            $("#Code").val(data);
        });
}



$(document.body).on("click", "#btnSubmit", function() {
    var dto = {};
    var id = $("#Id").val();
    dto.name = $("#Name").val();
    dto.code = $("#Code").val();
    dto.address = $("#Address").val();
    dto.contact = $("#Contact").val();

    if (id == "" || id == 0 || id == null) {
        $.ajax({
            url: "/api/Customers",
            data: dto,
            type: "POST",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Save Success", "Success!!!");
                    refressForm();
                    loadHistoryTable();
                    generateCode();
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
            url: "/api/Customers/" + id,
            data: dto,
            type: "PUT",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Save Success", "Success!!!");
                    refressForm();
                    loadHistoryTable();
                    generateCode();

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

function refressForm() {
    $("#Id").val("");
    $("#Code").val("");
    $("#Name").val("");
    $("#Address").val("");
    $("#Contact").val("");
}

function loadHistoryTable() {
    $("#customerHisotryTable").DataTable().destroy();

    $("#customerHisotryTable").DataTable({
        rowRecoder: {
            selector: 'td:nth-child(1)'
        },
        responsive: true,
        ajax: {
            url: "/api/Customers",
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
                data: "contact"
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
                    url: "/api/Customers/" + id,
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
    $.get("/api/Customers/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#Name").val(data.name);
            $("#Code").val(data.code);
            $("#Address").val(data.address);
            $("#Contact").val(data.contact);
        });
}






