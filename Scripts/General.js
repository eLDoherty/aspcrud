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
    if ($('.role_option').length > 0) {
        $('.role_option').on('change', function () {
            if ($(this).val() == "admin") {
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.addition').prop('disabled', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.addition').siblings('.hiddenAddition').prop('disabled', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.addition').prop('checked', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.edition').prop('disabled', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.edition').siblings('.hiddenEdition').prop('disabled', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.edition').prop('checked', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.deletion').prop('disabled', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.deletion').siblings('.hiddenDeletion').prop('disabled', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.deletion').prop('checked', true);
            } else if ($(this).val() == "editor") {
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.addition').prop('disabled', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.addition').siblings('.hiddenAddition').prop('disabled', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.addition').prop('checked', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.edition').prop('disabled', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.edition').siblings('.hiddenEdition').prop('disabled', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.edition').prop('checked', true);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.deletion').prop('disabled', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.deletion').siblings('.hiddenDeletion').prop('disabled', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.deletion').prop('checked', false);
            } else {
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.addition').prop('disabled', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.addition').siblings('.hiddenAddition').prop('disabled', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.addition').prop('checked', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.edition').prop('disabled', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.edition').siblings('.hiddenEdition').prop('disabled', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.edition').prop('checked', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.deletion').prop('disabled', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.deletion').siblings('.hiddenDeletion').prop('disabled', false);
                $(this).closest('.dropdown-wrapper').siblings('.checkbox-holder').find('.deletion').prop('checked', false);
            }
        })
    }

    // Checkbox handler
    if ($('.addition').length > 0) {
        $('.addition').on('change', function () {
            if ($(this).is(":checked")) {
                $(this).siblings('.hiddenAddition').prop('disabled', true);
            } else {
                $(this).siblings('.hiddenAddition').prop('disabled', false);
            }
        })
    }

    if ($('.edition').length > 0) {
        $('.edition').on('change', function () {
            if ($(this).is(":checked")) {
                $(this).siblings('.hiddenEdition').prop('disabled', true);
            } else {
                $(this).siblings('.hiddenEdition').prop('disabled', false);
            }
        })
    }

    if ($('.deletion').length > 0) {
        $('.deletion').on('change', function () {
            if ($(this).is(":checked")) {
                $(this).siblings('.hiddenDeletion').prop('disabled', true);
            } else {
                $(this).siblings('.hiddenDeletion').prop('disabled', false);
            }
        })
    }


})