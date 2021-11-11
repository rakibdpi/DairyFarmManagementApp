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
        dto.name = $("#Name").val();
        dto.occupation = $("#Occupation").val();
        dto.mobileNo = $("#MobileNo").val();
        dto.email = $("#Email").val();
        dto.nidNo = $("#NidNo").val();
        dto.age = $("#Age").val();
        dto.education = $("#Education").val();
        dto.presentAddress = $("#PresentAddress").val();
        dto.permanentAddress = $("#PermanentAddress").val();
        dto.fatherName = $("#FatherName").val();
        dto.motherName = $("#MotherName").val();
        var radioValue = $("input[name='Gender']:checked").val();
        if (radioValue === "Yes") {
            dto.gender = true;
        } else {
            dto.gender = false;
        }



        if ($("#IsActive:checked").length > 0) {
            dto.isActive = true;
        } else {
            dto.isActive = false;
        }

        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/BusinessPartnerInfos",
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
                url: "/api/BusinessPartnerInfos/" + id,
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
    $("#Name").val("");
    $("#Occupation").val("");
    $("#MobileNo").val("");
    $("#Email").val("");
    $("#NidNo").val("");
    $("#Age").val("");
    $("#Education").val("");
    $("#PresentAddress").val("");
    $("#PermanentAddress").val("");
    $("#FatherName").val("");
    $("#MotherName").val("");


    if (!($("#IsActive:checked").length > 0)) {
        $("#IsActive").prop('checked', true);
        $(".icheckbox_minimal-blue").addClass('checked').attr('aria-checked', 'true');
    }

    $('input[name="Gender"]').prop('checked', false);
    $(".iradio_minimal-blue").removeClass("checked").attr("aria-checked", "false");
}


function getData(id) {
    $.get("/api/BusinessPartnerInfos/" + id)
        .done(function (data) {
            $("#Id").val(data.id);
            $("#Name").val(data.name);
            $("#Occupation").val(data.occupation);
            $("#MobileNo").val(data.mobileNo);
            $("#Email").val(data.email);
            $("#NidNo").val(data.nidNo);
            $("#Age").val(data.age);
            $("#Education").val(data.education);
            $("#PresentAddress").val(data.presentAddress);
            $("#PermanentAddress").val(data.permanentAddress);
            $("#FatherName").val(data.fatherName);
            $("#MotherName").val(data.motherName);

            if (data.isActive == 1) {
                $("#IsActive").prop('checked', true);
                $(".icheckbox_minimal-blue").addClass('checked').attr('aria-checked', 'true');
            }
            else {
                $("#IsActive").prop('checked', false);
                $(".icheckbox_minimal-blue").removeClass('checked').attr('aria-checked', 'false');
            }

            var isGender = data.gender;
            if (isGender) {
                $("#MaleGenderButton").prop('checked', true);
                $("#MaleGenderButton").parent().addClass('checked');
            } else {
                $("#FemaleGenderButton").prop('checked', true);
                $("#FemaleGenderButton").parent().addClass('checked');
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
                    url: "/api/BusinessPartnerInfos/" + id,
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

    $("#personalInfoList").DataTable().destroy();

    $("#personalInfoList").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/BusinessPartnerInfos",
            dataSrc: ""
        },
        columns: [
            {
                data: "name"
            },
            {
                data: "mobileNo"
            },
            {
                data: "email"
            },
            {
                data: "nidNo"
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