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
    function() {
        var dto = {};
        var id = $("#Id").val();
        dto.name = $("#Name").val();
        dto.code = $("#Code").val();

        if ($("#IsActive:checked").length > 0) {
            dto.isActive = true;
        } else {
            dto.isActive = false;
        }

        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/Categories",
                data: dto,
                type: "POST",
                success: function(e) {
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
                url: "/api/Categories/" + id,
                data: dto,
                type: "PUT",
                success: function(e) {
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

        }
    });


function refressForm() {
    $("#Id").val('');
    $("#Name").val('');
    $("#Code").val('');


    if (!($("#IsActive:checked").length > 0)) {
        $("#IsActive").prop('checked', true);
        $(".icheckbox_minimal-blue").addClass('checked').attr('aria-checked', 'true');
    }
}

function getData(id) {
    $.get("/api/Categories/" + id)
        .done(function(data) {
            $("#Id").val(data.id);
            $("#Name").val(data.name);
            $("#Code").val(data.code);

            if (data.isActive == 1) {
                $("#IsActive").prop('checked', true);
                $(".icheckbox_minimal-blue").addClass('checked').attr('aria-checked', 'true');
            }
            else {
                $("#IsActive").prop('checked', false);
                $(".icheckbox_minimal-blue").removeClass('checked').attr('aria-checked', 'false');
            }
        });
}

$(document.body).on("click",
    ".js-edit",
    function() {
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
                    url: "/api/Categories/" + id,
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

    $("#categoryHistory").DataTable().destroy();

    $("#categoryHistory").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/Categories",
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
                data: "isActive",
                render: function (data) {
                    if (data) {
                        return "Active";
                    } else {
                        return "DeActive";
                    }
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<a class='btn btn-info btn-sm js-edit' data-id=" +data +" ><i class='fa fa-pencil-square fa-2x ' aria-hidden='false'></i></a>";
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