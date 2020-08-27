$(document).ready(function () {

    $('.js-example-basic-multiple').select2({
        maximumSelectionLength: 3,
        placeholder: 'Select specialities'
    }); 

    $("#profileImage").click(function (e) {
        $("#imageUpload").click();
    });

    function readFile() {
        if (this.files && this.files[0]) {
            $('#profileImage').attr('src', window.URL.createObjectURL(this.files[0]));
            var FR = new FileReader();
            FR.addEventListener("load", function (e) {
                document.getElementById("b64").innerHTML = e.target.result;
                $('#base64Picture').val($('#b64').text());
            });
            FR.readAsDataURL(this.files[0]);
        }
    }
    document.getElementById("imageUpload").addEventListener("change", readFile);

});