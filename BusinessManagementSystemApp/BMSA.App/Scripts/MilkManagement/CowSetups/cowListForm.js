$(document).ready(function () {
    loadHistoryTable();
});

function loadHistoryTable() {

    $("#list").DataTable().destroy();

    $("#list").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/CowSetups",
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
                    return "<a class='btn btn-info btn-sm js-edit' href='/CowSetups/CowEntryForm?id=" + data + "'  ><i class='fa fa-pencil-square fa-2x ' aria-hidden='false'></i></a>";
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

$(document.body).on("click", ".js-delete", function () {
    var button = $(this);
    var id = button.attr("data-id");
    bootbox.confirm("Are you sure you want to delete this info?",
        function (result) {
            if (result) {
                $.ajax({
                    url: "/api/CowSetups/" + id,
                    method: "DELETE",
                    success: function () {
                        button.parents("tr").remove();
                        toastr.success("Info Remove Successfully");
                    },
                    error: function (request, status, error) {
                        var response = jQuery.parseJSON(request.responseText);
                        toastr.error(response.message, "Error");
                    }
                });
            }
        });
});