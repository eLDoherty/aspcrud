jQuery(document).ready(function ($) {
    // Custom validation for user 
    function simpleValidateForm(formId, event) {

        event.preventDefault();
        event.stopPropagation();

        // Remove current error
        $("#username_error").text("");
        $("#email_error").text("");

        // Variable
        var errors = {};
        var numbs = /\d+/g;
        var emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

       /*
        * Validation schema
        */
        
        // Username Cant be empty

        $('#username').length > 0 && $('#username').val().replace(/ /g, "").length == 0 ? errors["username"] = { msg: "Username name can't be empty" } : {};

        // Email type validation
        $('#email').length > 0 && $('#email').val().match(emailPattern) ? [] : errors["email"] = { msg: "Should be valid email address" };

        if (Object.keys(errors).length === 0) {
            // Submit if no error left
            $(formId).unbind('submit').submit();
        } else {
            // Show error on frontend
            errors.username && $('#username_error').text(errors.username.msg);
            errors.email && $('#email_error').text(errors.email.msg);
        }
    }

    $('#edit_user').click(function (e) {
        simpleValidateForm('#edit_user_form', e);
    });

    $('#button_login').click(function (e) {
        simpleValidateForm('#login_form', e);
    });

    $('#button_register').click(function (e) {
        simpleValidateForm('#register_form', e);
    })

});