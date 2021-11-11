$(document).ready(function () {

    var urlVar = getUrlVars();

    var id = urlVar["id"];

    if (id > 0) {
        getData(id);
    }

    getAllInfo();


});

$(document.body).on("click", "#btnSubmitForProduct", function () {
    var dto = {};
    var id = $("#Id").val();
    dto.type = $("#Type").val();
    dto.code = $("#Code").val();
    dto.color = $("#Color").val();
    dto.weight = $("#Weight").val();
    dto.age = $("#Age").val();
    dto.status = "IN";

    if (id == "" || id == 0 || id == null) {
        $.ajax({
            url: "/api/Products",
            data: dto,
            type: "POST",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Save Success", "Success!!!");
                    refresh();
                    getAllInfo();

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
            url: "/api/Products/" + id,
            data: dto,
            type: "PUT",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Save Success", "Success!!!");
                    refresh();
                    getAllInfo();
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


function refresh() {
    $("#Id").val("");
  //  $("#Type").val("");
    $("#Code").val("");
    $("#Color").val("");
    $("#Weight").val("");
    $("#Age").val("");
}

function getAllInfo() {
    $("#productHistory").DataTable().destroy();

    $("#productHistory").DataTable({
        rowRecoder: {
            selector: 'td:nth-child(1)'
        },
        responsive: true,
        dom: ' <l Bfrtip><>',
        buttons: [
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                },
                title: 'Bull Cow List'
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                },
                title: 'Bull Cow List'
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4]
                },
                text: '<u>P</u>rint',
                key: {
                    key: 'p',
                    altKey: true
                },
                title: 'Bull Cow List'
            },
        ],

        ajax: {
            url: "/api/Products",
            dataSrc: ""
        },
        columns: [
            {
                data: "code"

            },
            {
                data: "color"

            },
            {
                data: "age"
            },
            {
                data: "weight"
            },
            {
                data: "price"
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
        refresh();
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
                    url: "/api/Products/" + id,
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
    $.get("/api/Products/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#Type").val(data.type);
            $("#Code").val(data.code);
            $("#Color").val(data.color);
            $("#Weight").val(data.weight);
            $("#Age").val(data.age);
        });
}




$(document.body).on("change", "#ChageType", function () {

    var type = $("#ChageType").val();
    if (type > 0) {

        $("#productHistory").DataTable().destroy();

        $("#productHistory").DataTable({
            rowRecoder: {
                selector: 'td:nth-child(1)'
            },
            responsive: true,
            dom: ' <l Bfrtip><>',
            buttons: [
                {
                    extend: 'pdfHtml5',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    title: 'Bull Cow List'
                },
                {
                    extend: 'excelHtml5',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    title: 'Bull Cow List'
                },
                {
                    extend: 'print',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    text: '<u>P</u>rint',
                    key: {
                        key: 'p',
                        altKey: true
                    },
                    title: 'Bull Cow List'
                },
            ],

            ajax: {
                url: "/api/Products/GetByType?type="+ type,
                dataSrc: ""
            },
            columns: [
                {
                    data: "code"

                },
                {
                    data: "color"

                },
                {
                    data: "age"
                },
                {
                    data: "weight"
                },
                {
                    data: "price"
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
});