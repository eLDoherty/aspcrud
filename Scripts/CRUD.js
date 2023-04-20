
jQuery(document).ready(function ($) {
    // Handle delete button
    $(".delete_button").click(function (e) {
        e.preventDefault();
        var cnfrm = confirm("Are you sure?");
        var url = $(this).attr("href");
        if (cnfrm) {
            window.location = url;
        }
    });

    function simpleValidateForm(formId, event) {

        event.preventDefault();
        event.stopPropagation();

        // Remove current error
        $("#name_error").text("");
        $("#price_error").text("");
        //  $("#description_error").text("");
        $("#status_error").text("");

        // Variable
        var errors = {};
        var numbs = /\d+/g;

        // Select all choosed category
        $('#choosed_category option').prop('selected', true);

        // Validate
        // Name cant be empty
        $('.add_product_name').val().replace(/ /g, "").length == 0 ? errors["name"] = { msg: "Product name should'nt empty" } : {};

        // Only numbers
        $('.add_product_price').val().match(numbs) ? [] : errors["price"] = { msg: "Price only filled by number" };

        // Price cant be empty
        $('.add_product_price').val().replace(/ /g, "").length == 0 ? errors["price"] = { msg: "Price can't be empty" } : {};

        // Description cant be empty
        //$('#description').val().trim().length > 0 ? {} : errors["description"] = { msg: "Product description can't be empty" };

        // Status product should be choosed
        $('#status').val() === "none" ? errors["status"] = { msg: "Choose your product status" } : {};

        if (Object.keys(errors).length === 0) {
            // Submit if no error left
            $(formId).unbind('submit').submit();
        } else {
            // Show the error
            errors.name && $('#name_error').text(errors.name.msg);
            errors.price && $('#price_error').text(errors.price.msg);
            // errors.description && $('#description_error').text(errors.description.msg);
            errors.status && $('#status_error').text(errors.status.msg);
        }
    }

    $('#button_create_product').click(function (e) {
        simpleValidateForm('#create_product_form', e);
    })

    $('#button_edit_product').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        simpleValidateForm('#edit_product_form', e);
    })

    // Show image before upload
    if ($("#image-previewer").length > 0) {
        $("#image-previewer").attr("src").length > 0 ? $("#image-previewer").show() : $("#image-previewer").hide();
    }

    $("#thumbnail").change(function () {
        const file = this.files[0];
        if (file) {
            let reader = new FileReader();
            $("#image-previewer").show();
            reader.onload = function (event) {
                $("#image-previewer").attr("src", event.target.result);
            };
            reader.readAsDataURL(file);
        }
    });

    // Handle Toggler Category
    $('.insert_category').click(function (e) {
        e.preventDefault();
        var category = $(this).parent().siblings('.option-category').text();
        var id = $(this).val();
        var exists = $('#choosed_category option').filter(function () { return $(this).val() == id; }).length;
        if (!exists) {
            $('#choosed_category').append(`<option value="${$(this).val()}">${category}</option>`);
        } else {
            alert("Category has been added");
        }
    });

    $('#add_existing_category').click(function (e) {
        e.preventDefault();
        $('.category-list').slideToggle();
    });

    $('#remove_added_category').click(function () {
        $('#choosed_category').find('option:first-child').remove();
    });

    // Select2
    $('#country').select2();

    // Modal handler 
    $('.add_thumbnail').click(function (e) {
        e.preventDefault();
        $('#image-previewer').show();
        var img = $(this).val();
        console.log(img);
        $('#thumbnail').val(img);
        $('#image-previewer').attr('src', $('#base_url_image').val() + img);
    });

    // Media form handler
    $('#add_media').click(function (e) {
        e.preventDefault();
        console.log('From media')
    })
});
