$(function () {

    $(document).on("click", '#btnEncrypt', function () {

        $.post("/Home/Encrypt", { value: $("#WORD_ENCRY").val() }).success(function (data) {
            $("#txtresult").html(data.Data);
        }).error(function (data) {
            $("#txtresult").html(data.Data);
        });
    });

});