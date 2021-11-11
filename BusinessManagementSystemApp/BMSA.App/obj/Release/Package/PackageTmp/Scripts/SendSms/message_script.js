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


//$(document.body).on("click", "#btnSubmit", fusnction () {
//    var dto = {};
//    dto.areaId = $("#AreaId").val();
//    dto.message = $("#Message").val();

//    $.ajax({
//        url: "/api/Messages/SendSMS",
//        data: dto,
//        type: "POST",
//        success: function (e) {
//            if (e > 0) {
//                toastr.success("Message Successfully Sent", "Success!!!");
//            } else {
//                toastr.warning("Message sent failed", "Warning!!!");
//            }
//        },
//        error: function (request, status, error) {
//            var response = jQuery.parseJSON(request.responseText);
//            toastr.error(response.message, "Error");
//        }
//    }),
//});





$(document.body).on("click", "#btnSubmit", function () {
    var dto = {};
    dto.areaId = $("#AreaId").val();
    dto.message = $("#Message").val();

        $.ajax({
            url: "/api/Messages/SendSMS",
            data: dto,
            type: "POST",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Message Successfully Sent", "Success!!!");
                   // refreshForm();
                } else {
                    toastr.warning("Sending Fail", "Warning!!!");
                }
            },
            error: function (request, status, error) {
                var response = jQuery.parseJSON(request.responseText);
                toastr.error(response.message, "Error");
            }
        });
    
});
