jQuery(document).ready(function ($) {

    // Create product validation
    function validateForm(elm) {
        $(elm).validate({
            submitHandler: function (form) {
                $("#name_error").text("");
                $("#price_error").text("");

                var errors = [];
                var numbs = /\d+/g;

                // Name cant be empty
                $('.add_product_name').val().replace(/ /g, "").length == 0 ? errors["name"] = "Product name should'nt empty" : [];

                // Only numbers
                $('.add_product_price').val().match(numbs) ? [] : errors["price"] = "Price only filled by number";

                // Price cant be empty
                $('.add_product_price').val().replace(/ /g, "").length == 0 ? errors["price"] = "Price can't be empty" : [];

                if (errors === undefined || errors.length == 0) {
                    errors["name"] && $("#name_error").text(errors["name"]);
                    errors["price"] && $("#price_error").text(errors["price"]);
                } else {
                    $(form).submit();
                }
            }
        });
    }

    // Validate Create Form
    validateForm("#create_product_form");

    // Validate Edit Form
    validateForm("#edit_product_form");
    
});