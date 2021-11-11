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
});


$(document.body).on("click",
    "#btnSubmit",
    function () {

        $("#history").DataTable().destroy();
        var dataTypeId = $("#DataTypeId").val();
        var formDate = $("#FormDate").val();
        var toDate = $("#ToDate").val();


        GetData(formDate, toDate, dataTypeId);

    });


function GetData(formDate, toDate, dataTypeId ) {
    var table = $("#history").DataTable({
        rowReorder: {
            selector: 'td:nth-child(2)'
        },
        responsive: true,

        ajax: {
            url: "/api/DataTransections/GetReport",
            data: { fromDate: formDate, toDate: toDate, dataTypeId: dataTypeId },
            dataSrc: ""
        },
        columns: [
            {
                data: "dataType.name"

            },
            {
                data: "dateTime"

            },
            {
                data: "value"
            }
        ]
    });
}

