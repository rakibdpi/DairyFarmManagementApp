$(document).ready(function () {
    $("#ProductId").select2();
    $.ajax({
        type: "GET",
        url: "/api/products",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var $el = $("#ProductId");
            $el.empty(); // remove old options
            $el.append($("<option></option>")
                .attr("value", '').text(''));
            $.each(data, function (value, key) {
                $el.append($('<option>', {
                    value: key.id,
                    text: key.code
                }));
            });
        }
    });

});



$(document.body).on("change", "#ProductId", function () {
    // Refresh All Dropdown
    var productId = $("#ProductId").val();
    if (productId > 0) {
        $.ajax({
            type: "GET",
            url: "/api/Products",
            contentType: "application/json; charset=utf-8",
            data: { id: productId },
            success: function (data) {
                $("#code").text(" "+ data.code);
                $("#weight").text("ওজন : "+ data.weight);
                $("#color").text("রং : "+ data.color);
                $("#year").text("বয়স : "+ data.age);
            }
        });
    }

});
