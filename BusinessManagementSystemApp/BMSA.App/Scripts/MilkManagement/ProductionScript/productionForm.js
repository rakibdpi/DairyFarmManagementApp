$(document).ready(function() {

    refreshTable();
    $.get("/api/CowSetups", function (data) {
        var $el = $("#CowSetupId");
        $el.empty(); // remove old options
        $el.append($("<option></option>")
            .attr("value", '').text(''));
        $.each(data, function (value, key) {
            $el.append($('<option>', {
                value: key.id,
                text: key.number
            }));
        });
    });



});


function refreshTable() {
    $("#productionTable").DataTable().destroy();
    var t = $('#productionTable').DataTable({
        retrieve: false,
        paging: false,
        searching: false,
        info: false,
        responsive: true,
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [1],
                "visible": false,
                "searchable": false
            }
        ]
    });
}
var rowNumber = -1;
var number = -1;
var count = -1;

function monthLength(month) {
    if (month === "January" || month === "March" || month === "May" || month === "July" || month === "August" || month === "October" || month === "December") {
        return 31;
    }
    else if (month === "April" || month === "June" || month === "September" || month === "November") {
        return 30;
    } else {
        return 29;
    }
}



function initialTable() {
    $("#productionTable").DataTable().destroy();
    var t = $('#productionTable').DataTable({
                retrieve: false,
                paging: false,
                searching: false,
                info: false,
                "columnDefs": [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    },
                    {
                        "targets": [1],
                        "visible": false,
                        "searchable": false
                    }
                ]
            });
            var count = 1;
            var monthDuration = monthLength($("#ProductionMonth").val());
            for (var i = 0; i < monthDuration; i++) {

                t.row.add([
                    0,
                    $("#CowSetupId").val(),
                    count,
                    "<input style='width: 100px' type='text' id='MorningQuantity" + i + "' name='MorningQuantity" + i + "' value= '0'>",
                    "<input style='width: 100px' type='text' id='AfterNoonQuantity" + i + "' name='AfterNoonQuantity" + i + "' value='0'>",
                    "<input style='width: 100px' type='text' id='NightQuantity" + i + "' name='NightQuantity" + i + "' value='0'>",
                    "<input style='width: 100px' type='text' id='OtherTime" + i + "' name='OtherTime" + i + "' value='0'>",
                ]).draw(false);
                ++count;
                }
}






$(document.body).on("click", "#btnLoad", function () {
    refreshTable();
    var client = $("#CowSetupId").val();
    var month = $("#ProductionMonth").val();
    if (client.length < 1 || month.length < 1) {
        toastr.warning("Please fill up all required fields", "Warning!!");
        return;
    }

    $.get("/api/Productions/GetStatus", { cowId: client, month: month },
        function (data) {
            if (data) {
                toastr.warning("Milk already entry for this cow in this month", "!!Warning");
                $('#productionTable').DataTable().clear().draw();
                return;
            } else {
                initialTable();
            }
        });
});



$(document.body).on("click", "#btnSubmit", function () {
    var dto = {
        dtoList: []
    };

    var clt = $("#CowSetupId").val();
    var id = $("#Id").val();
    var productionMonth = $("#ProductionMonth").val();
    var year = $("#Year").val();
        var dtlTable = $('#productionTable').DataTable();
        var details = dtlTable.rows().data();
        if (details.length < 1) {
            toastr.warning("Please fill up the all required field", "Warning!!!");
            return;
        }
        for (var i = 0; i < details.length; i++) {
            var morningQuantityId = 'MorningQuantity' + i;
            var afterNoonQuantityId = 'AfterNoonQuantity' + i;
            var nightQuantityId = 'NightQuantity' + i;
            var otherTimeId = 'OtherTime' + i;

            var morningQuantity = dtlTable.$('input[name=' + morningQuantityId + '] ').val();
            var afterNoonQuantity = dtlTable.$('input[name=' + afterNoonQuantityId + '] ').val();
            var nightQuantity = dtlTable.$('input[name=' + nightQuantityId + '] ').val();
            var otherTime = dtlTable.$('input[name=' + otherTimeId + '] ').val();

            var cow = details.cell(i, 1).data();
            var day = details.cell(i, 2).data();
            dto.dtoList.push({
                cowSetupId: cow,
                dayNumber: day,
                productionMonth: productionMonth,
                year: year,
                morningQuantity: parseFloat(morningQuantity),
                afterNoonQuantity: parseFloat(afterNoonQuantity),
                nightQuantity: parseFloat(nightQuantity),
                otherTime: parseFloat(otherTime),
            });
        }
        $.ajax({
            url: "/api/Productions",
            data: dto,
            type: "POST",
            success: function (e) {
                if (e > 0) {
                    toastr.success("Save Success", "Success!!!");
                    $("#CowSetupId").val("");
                    $('#productionTable').DataTable().clear().draw();
                } else {
                    toastr.warning("Save Fail", "Warning!!!");
                }
            },
            error: function (request, status, error) {
                var response = jQuery.parseJSON(request.responseText);
                toastr.error(response.message, "Error");
            }
        });
    
});


$(document.body).on("click", "#btnCancel", function () {
    location.reload();
});