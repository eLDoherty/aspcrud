jQuery(document).ready(function ($) {
    // Custom validation for user 
    function simpleValidateForm(formId, event) {

        event.preventDefault();
        event.stopPropagation();

        // Remove current error
        $("#username_error").text("");
        $("#email_error").text("");
        $("#password_error").text("");

        // Variable
        var errors = {};
        var emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

       /*
        * Validation schema
        */
        
        // Username Cant be empty

        $('#username').length > 0 && $('#username').val().replace(/ /g, "").length == 0 ? errors["username"] = { msg: "Username name can't be empty" } : {};

        // Email type validation
        $('#email').length > 0 && $('#email').val().match(emailPattern) ? [] : errors["email"] = { msg: "Should be valid email address" };

        // Password cant be empty
        $('#password').length > 0 && $('#password').val().replace(/ /g, "").length == 0 ? errors["password"] = { msg: "Password cant be empty" } : {};  


        if (Object.keys(errors).length === 0) {
            // Submit if no error left
            $(formId).unbind('submit').submit();
        } else {
            // Show error on frontend
            $('#username').length > 0 && errors.username && $('#username_error').text(errors.username.msg);
            $('#email').length > 0 && errors.email && $('#email_error').text(errors.email.msg);
            $('#password').length > 0 && errors.password && $('#password_error').text(errors.password.msg);
        }
    }

    $('#edit_user').click(function (e) {
        simpleValidateForm('#edit_user_form', e);
    });

    $('#save_media').click(function (e) {

        e.preventDefault();
        e.stopPropagation();

        // Remove current error
        $("#name_error").text("");

        // Variable
        var errors = {};

        /*
         * Validation schema
         */

        // Name cant be empty -- media
        $('#name').length > 0 && $('#name').val().replace(/ /g, "").length == 0 ? errors["name"] = { msg: "Image name cant be empty" } : {};
        $('#uri').length > 0 && $('#uri').val().replace(/ /g, "").length == 0 ? errors["uri"] = { msg: "Image cant be empty" } : {};

        if (Object.keys(errors).length === 0) {

            $('#form_upload_images').unbind('submit').submit();

        } else {
            errors.name && $('#name_error').text(errors.name.msg);
            errors.uri && $('#uri_error').text(errors.uri.msg);
        }
    });

    $('#button_login').click(function (e) {
        simpleValidateForm('#login_form', e);
    });

    $('#button_register').click(function (e) {
        simpleValidateForm('#register_form', e);
    })

});