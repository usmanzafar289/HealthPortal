$('.summernote').summernote({
    height: 150,   //set editable area's height
    codemirror: { // codemirror options
        theme: 'monokai'
    }
});

GetDoctorInformation($("#DoctorInformation").val());

$("#DoctorInformation").change(function () {
    GetDoctorInformation(this.value)
});

function GetDoctorInformation(userId) {
    var dataContract = {
        userId: userId,
    };
    $.ajax({
        type: "POST",
        url: AppUrl + "/Question/GetDoctorInformation/",
        dataType: "json",
        data: dataContract,
        success: function (resp) {
            if (!IsNullOrEmpty(resp.user.picture))
                $("#imageDoctor").attr("src", resp.user.picture);
            else
                $("#imageDoctor").attr("src", "images/user.jpg");

            $("#nameDoctor").text(resp.user.firstName + " " + resp.user.lastName);
            $("#descriptionDoctor").text(resp.user.description);
            $("#emailDoctor").text(resp.user.email);
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log("error");
        }
    });
}


$("#btnSubmitQuestion").click(function () {
    var title = $("#titleQuestion").val();
    var data = $('#summernote').summernote('code');
    var b64 = encode(data);
    var doctorId = $("#DoctorInformation").val();
    var dataContract = {
        doctorId: doctorId,
        patientId: UserId,
        title, title,
        message: b64,
    };
    $.ajax({
        type: "POST",
        url: AppUrl + "/Question/AddQuestion/",
        dataType: "json",
        data: dataContract,
        success: function (resp) {
            alert("Question has been submitted!");
            $('#summernote').summernote('reset');
            $("#titleQuestion").val("");
        },
        failure: function (resp) {
            console.log("failure");
        },
        error: function (resp) {
            console.log("error");
        }
    });
});