$(document).ready(function () {
    loadHistoryTable();
});

function loadHistoryTable() {

    $("#activeClientInfoHistory").DataTable().destroy();

    $("#activeClientInfoHistory").DataTable({
        retrieve: true,
        paging: true,
        ajax: {
            url: "/api/ClientInfos/GetAllDeActive",
            dataSrc: ""
        },
        columns: [
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
                data: "isActive",
                render: function (data) {
                    if (data) {
                        return "Active";
                    } else {
                        return "DeActive";
                    }
                }
            }
        ]
    });
}