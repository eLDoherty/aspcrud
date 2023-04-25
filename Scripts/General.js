jQuery(document).ready(function ($) {

    // Handle Media Page Toggler
    $('#add_media').click(function () {
        $(this).toggleClass('active');
        $('.error span').text('');
        $('#media-wrapper').slideToggle();
        if ($(this).hasClass('active')) {
            $(this).text('Cancel');
            $(this).removeClass('btn-success').addClass('btn-danger');
        } else {
            $(this).text('Add Media');
            $(this).removeClass('btn-danger').addClass('btn-success');
        }

    });

    // Handle User role setting
    if ($('#role').length > 0) {

        var optionSelected = $('#role').find("option:selected");
        var valueSelected = optionSelected.val();

        if (valueSelected == "admin") {
            $("#canCreate").prop("disabled", true);
            $("#canEdit").prop("disabled", true);
            $("#canDelete").prop("disabled", true);
        } else if (valueSelected == "editor") {
            $("#canCreate").prop("disabled", false);
            $("#canEdit").prop("disabled", true);
            $("#canDelete").prop("disabled", false);
        } else {
            $("#canCreate").prop("disabled", false);
            $("#canEdit").prop("disabled", false);
            $("#canDelete").prop("disabled", false);
        }

        $('#role').on('change', function () {
            var optionSelected = $(this).find("option:selected");
            var valueSelected = optionSelected.val();
            if (valueSelected == "admin") {
                $("#canCreate").prop("disabled", true);
                $("#canCreate").prop("checked", true);
                $("#canEdit").prop("disabled", true);
                $("#canEdit").prop("checked", true);
                $("#canDelete").prop("disabled", true); 
                $("#canDelete").prop("checked", true);
            } else if (valueSelected == "editor") {
                $("#canCreate").prop("disabled", false);
                $("#canCreate").prop("checked", false);
                $("#canEdit").prop("disabled", true);
                $("#canEdit").prop("checked", true);
                $("#canDelete").prop("disabled", false);
                $("#canDelete").prop("checked", false);
            } else {
                $("#canCreate").prop("disabled", false);
                $("#canCreate").prop("checked", false);
                $("#canEdit").prop("disabled", false);
                $("#canEdit").prop("checked", false);
                $("#canDelete").prop("disabled", false);
                $("#canDelete").prop("checked", false);
            }
        })

    }

})