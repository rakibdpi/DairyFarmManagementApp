$(document).ready(function () {

    $("#AreaId").select2();
    $.get("/api/Areas", function (data) {
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
    });
});






$(document.body).on("click", "#btnSubmit", function () {
        var dto = {};
        var id = $("#Id").val();
        dto.phoneNumber = $("#PhoneNumber").val();
        dto.message = $("#Description").val();
        dto.which = 1;
        var radioValue = $("input[name='SmsType']:checked").val();
    if (radioValue == "Yes") {
        dto.smsType = true;
        } else {
            dto.smsType = false;
            if (radioValue == "All") {
                dto.smsType = 1;
            } else if (radioValue == "Active") {
                dto.smsType = 2;
            } else if (radioValue == "DeActive") {
                dto.smsType = 3;
            } else if (radioValue == "No") {
                dto.smsType = 4;
            } else if (radioValue == "AreaWise") {
                dto.areaId = $("#AreaId").val();
                dto.smsType = 5;
            }
        }
        if (id == "" || id == 0 || id == null) {
            $.ajax({
                url: "/api/Messages/NormalSms",
                data: dto,
                type: "POST",
                success: function (e) {
                    if (e > 0) {
                        toastr.success("Message Successfully Sent", "Success!!!");
                        refreshForm();
                    } else {
                        toastr.warning("Sending Fail", "Warning!!!");
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
    $("#Id").val("");
    $("#PhoneNumber").val("");
    $("#AreaId").val("");
    $("#Description").val("");
}