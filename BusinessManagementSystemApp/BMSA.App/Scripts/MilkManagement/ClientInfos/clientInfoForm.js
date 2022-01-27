$(document).ready(function () {

    $.ajax({
        type: "GET",
        url: "/api/Areas",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $el = $("#AreaId");
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

    $(document.body).on("change", "#AreaId", function () {
        var area = $("#AreaId").val();


        $.get("/api/Areas/GetByArea", { areaId: area },
            function (data) {
                $("#Code").val(data.codeNo);
            });

            

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
        var dto = {};
        var id = $("#Id").val();
        dto.code = $("#Code").val();
        dto.name = $("#Name").val();
        dto.areaId = $("#AreaId").val();
        var mobile = $("#PhoneNo").val();
        if (mobile.length > 0) {
            if (mobile.length != 11) {
                toastr.warning("Pleas give valid phone number");
                return;
            }
        } 

        dto.phoneNo = mobile;              
        dto.halfKg = $("#HalfKg").val();
        dto.sevenAndHalfGm = $("#SevenAndHalfGm").val();
        dto.oneKg = $("#OneKg").val();
        dto.address = $("#Address").val();
        dto.dayInterval = $("#DayInterval").val();

        if ($("#IsActive:checked").length > 0) {
            dto.isActive = true;
        } else {
            dto.isActive = false;
        }

        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/ClientInfos",
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
                url: "/api/ClientInfos/" + id,
                data: dto,
                type: "PUT",
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

        }
    });


function refressForm() {
    $("#Id").val('');
    $("#Code").val('');
    $("#Name").val('');
    $("#PhoneNo").val('');
    $("#AreaId").val('');
    $("#HalfKg").val('');
    $("#SevenAndHalfGm").val('');
    $("#OneKg").val('');
    $("#Address").val('');
    $("#DayInterval").val('');

    if (!($("#IsActive:checked").length > 0)) {
        $("#IsActive").prop('checked', true);
        $(".icheckbox_minimal-blue").addClass('checked').attr('aria-checked', 'true');
    }
}

function getData(id) {
    $.get("/api/ClientInfos/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#AreaId").val(data.areaId);
            $("#Code").val(data.code);
            $("#Name").val(data.name);
            $("#PhoneNo").val(data.phoneNo);
            $("#HalfKg").val(data.halfKg);
            $("#SevenAndHalfGm").val(data.sevenAndHalfGm);
            $("#OneKg").val(data.oneKg);
            $("#Address").val(data.address);
            $("#DayInterval").val(data.dayInterval);


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
                    url: "/api/ClientInfos/" + id,
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

    $("#clientInfoHistory").DataTable().destroy();

    $("#clientInfoHistory").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/ClientInfos",
            dataSrc: ""
        },
        columns: [
            {
                data: "area.name"
            },
            {
                data: "code",
                render: function(data) {
                    if (data) {
                        return data;
                    } else {
                        return "";
                    }
                }
            },
            {
                data: "name"
            },
            {
                data: "phoneNo"
            },
            {
                data: "address"
            },
            {
                data: "dayInterval",
                render: function (data) {
                    if (data) {
                        return data;
                    } else {
                        return "";
                    }
                }
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