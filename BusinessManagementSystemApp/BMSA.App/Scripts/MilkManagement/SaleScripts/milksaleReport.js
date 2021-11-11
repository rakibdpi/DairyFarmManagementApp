
$(document.body).on("click", "#btnSubmit", function () {
    var dto = {
        paymentDtos: []
    };
    var year = $("#Year").val();
    var month = $("#Month").val();

    if (year.length < 1 || month.length < 1) {
        toastr.warning("Please select year and month", "Warning!!!");
        return;
    }


    //$("#paymentsTable").DataTable({
    //    retrieve: true,
    //    paging: true,
    //    ajax: {
    //        url: "/api/PacketSales/GetSalesReport?year=" + year + "&month=" + month,
    //        dataSrc: "",
    //    },
    //    columns: [
    //        {
    //            data: "areaName"
    //        },
    //        {
    //            data: "totalHalf"
    //        },
    //        {
    //            data: "totalSevenHalf"
    //        },
    //        {
    //            data: "totalOne"
    //        }
    //    ]
    //});



    $("#paymentsTable").DataTable().destroy();

    $("#paymentsTable").DataTable({
        rowRecoder: {
            selector: 'td:nth-child(1)'
        },
        responsive: true,
        dom: ' <l Bfrtip><>',
        buttons: [
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                title: 'Milk Sales Report Area Wise'
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                title: 'Milk Sales Report Area Wise'
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                text: '<u>P</u>rint',
                key: {
                    key: 'p',
                    altKey: true
                },
                title: 'Milk Sales Report Area Wise'
            },
        ],
        ajax: {
            url: "/api/PacketSales/GetSalesReport?year=" + year + "&month=" + month,
            dataSrc: "",
        },
        columns: [
            {
                data: "areaName"
            },
            {
                data: "totalHalf"
            },
            {
                data: "totalSevenHalf"
            },
            {
                data: "totalOne"
            },
            {
                data: "totalAmount"
            }
        
        ]
    });









});