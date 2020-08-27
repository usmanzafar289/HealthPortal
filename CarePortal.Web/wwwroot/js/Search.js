$(document).ready(function () {
    var oTable = $('#Doctors_Table').DataTable({
        data: [],
        filter: true,
        lengthChange: false,
        paging: true,
        info: false,
        autoWidth: false,
        pageLength: 5,
        aaSorting: [[1, 'asc']],
        language: {
            paginate: {
                next: "<i class='fa fa-caret-right'></i>",
                previous: "<i class='fa fa-caret-left'></i>"
            },
        },
        columns: [
            { title: "", width: "33%" },
            { title: "Name", width: "33%" },
            { title: "Email ID", width: "33%" }
        ],
        initComplete: function () {
            $("#Doctors_Table").toggleClass('cards')
            $("#Doctors_Table thead").toggle()
        }
    });
    GetDoctors();
    function GetDoctors() {
        LoadDoctors(doctors);
    }
    function LoadDoctors(doctors) {
        var html = "";
        for (i = 0; i < doctors.length; i++) {
            var item = doctors[i];
            var imageURL = "";
            if (!item.imageURL) {
                imageURL = "/images/user.jpg";
            }
            else {
                imageURL = item.imageURL;
            }
            html =
                '<tr id = ' + item.emailId + ' class="clickable-row" data-href="/Profile/Index?userId=' + item.userId + '"> '
                + '<td><img alt="" src="' + imageURL + '" /></td>'
                + '<td>' + item.firstName.toUpperCase() + " " + item.lastName.toUpperCase() + '</td>'
                + '<td><i class="fa fa-envelope-o"></i> ' + item.emailId + '</td>'
                + '</tr>';
            $('#Doctors_Table').DataTable().row.add($(html)).draw();
        }
    }

    $(".clickable-row").click(function () {
        window.location = $(this).data("href");
    });
});