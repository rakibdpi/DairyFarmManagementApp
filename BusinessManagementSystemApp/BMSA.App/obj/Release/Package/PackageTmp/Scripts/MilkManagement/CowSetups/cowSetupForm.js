$(document).ready(function () {


    var urlVar = getUrlVars();

    var id = urlVar["id"];

    if (id > 0) {
        getData(id);
    }
    loadHistoryTable();
});

$(document.body).on("click",
    "#btnSubmit",
    function () {
        var dto = {};
        var id = $("#Id").val();
        dto.number = $("#Number").val();
        dto.color = $("#Color").val();

        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/CowSetups",
                data: dto,
                type: "POST",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Save Success", "Success!!!");
                        refreshForm();
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
                url: "/api/CowSetups/" + id,
                data: dto,
                type: "PUT",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Info Update Success", "Success!!!");
                        refreshForm();
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

        }
    });


function refreshForm() {
    $("#Id").val('');
    $("#Number").val('');
    $("#Color").val('');

}

function getData(id) {
    $.get("/api/CowSetups/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#Number").val(data.number);
            $("#Color").val(data.color);
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
                    url: "/api/CowSetups/" + id,
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

function loadHistoryTable() {

    $("#history").DataTable().destroy();

    $("#history").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/CowSetups/GetHistory",
            dataSrc: ""
        },
        columns: [
            {
                data: "number"
            },
            {
                data: "color"
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