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
});


$(document.body).on("click", "#btnSubmit", function () {


    var clientId = $("#AreaId").val();

    if (clientId === "" ) {
        toastr.warning("Pleas Select Area and Client");
        return;
    }


    var reportType = $("#AmountType").val();
    var type = $("#CustomerType").val();

    if (reportType == "pdf") {
        $.ajax({
            url: "/Reports/ClientReport?areaId=" + clientId + "&type="+ type,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "" && data != null) {
                    setTimeout(function () {
                        $("#pdf").attr("href", data);
                        var reportBox = $("#pdf").fancybox({
                            'frameWidth': 85,
                            'frameHeight': 495,
                            'overlayShow': true,
                            'hideOnContentClick': false,
                            'type': 'iframe',
                            helpers: {
                                // prevents closing when clicking OUTSIDE fancybox
                                overlay: { closeClick: false }
                            }
                        }).trigger('click');
                    }, 1000);
                }
            }
        });

    }

    if (reportType == "word") {
        $.ajax({
            url: "/Reports/ClientReportWord?areaId=" + clientId + "&type=" + type,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != "" && data != null) {
                    setTimeout(function () {
                        $("#pdf").attr("href", data);
                        var reportBox = $("#pdf").fancybox({
                            'frameWidth': 85,
                            'frameHeight': 495,
                            'overlayShow': true,
                            'hideOnContentClick': false,
                            'type': 'iframe',
                            helpers: {
                                // prevents closing when clicking OUTSIDE fancybox
                                overlay: { closeClick: false }
                            }
                        }).trigger('click');
                    }, 1000);
                }
            }
        });

    }

 

});
