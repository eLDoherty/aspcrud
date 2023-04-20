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

    if ($('#role').length > 0) {
       var optionSelected = $('#role').find("option:selected");
       var valueSelected = optionSelected.val();
       if (valueSelected == "admin") {
            $('#admin_setting').show();
       } else {
            $('#admin_setting').hide();
        }

        $('#role').on('change', function () {
            var optionSelected = $(this).find("option:selected");
            var valueSelected = optionSelected.val();
            if (valueSelected == "admin") {
                $('#admin_setting').slideToggle();
            } else {
                $('#admin_setting').slideToggle();
            }
        })
    }

})