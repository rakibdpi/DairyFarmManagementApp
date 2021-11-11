$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/api/DataTypes",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $el = $("#DataTypeId");
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
        var id = $("#Id").val();
        var dataTypeId = $("#DataTypeId").val();
        var value = $("#Value").val();

        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/DataTransections?dataTypeId=" + dataTypeId +"&value="+value,
                //data: JSON.stringify({ id: id, dataTypeId: dataTypeId, value: value }),
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
            //dto.id = id;
            $.ajax({
                url: "/api/DataTransections/" + id,
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
    $("#DataType").val('');
    $("#Value").val('');

}

function getData(id) {
    $.get("/api/DataTransections/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#DataType").val(data.dataTypeId);
            $("#Value").val(data.value);
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

//$(document.body).on("click", ".js-delete", function () {
//    var button = $(this);
//    var id = button.attr("data-id");
//    bootbox.confirm("Are You Delete This Data?",
//        function (result) {
//            if (result) {
//                $.ajax({
//                    url: "/api/CowSetups/" + id,
//                    method: "DELETE",
//                    success: function () {
//                        button.parents("tr").remove();
//                        toastr.success("Delete Success");
//                    },
//                    error: function (request, status, error) {
//                        var response = jQuery.parseJSON(request.responseText);
//                        toastr.error(response.message, "Error");
//                    }
//                });
//            }
//        });
//});

function loadHistoryTable() {

    $("#history").DataTable().destroy();

    $("#history").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/DataTransections",
            dataSrc: ""
        },
        columns: [
            {
                data: "dataType.name"
            },
            {
                data: "value"
            },
            {
                data: "id",
                render: function (data) {
                    return "<a class='btn btn-info btn-sm js-edit' data-id=" + data + " ><i class='fa fa-pencil-square fa-2x ' aria-hidden='false'></i></a>";
                }
            }
        ]
    });
}