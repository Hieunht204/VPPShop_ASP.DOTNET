$(document).ready(function () {
    $("#SamePerson").change(function () {
        if ($(this).prop("checked")) {
            $(this).val(true);
            $(".delivery-info").addClass("d-none");
        } else {
            $(this).val(false);
            $(".delivery-info").removeClass("d-none");
        }
    });
});
