jQuery(document).ready(function ($) {

    // Create product validation
    function validateForm(elm) {
        $(elm).validate({
            submitHandler: function (form) {
                $("#name_error").text("");
                $("#price_error").text("");
                var errors = {};
                var numbs = /\d+/g;

                // Name cant be empty
                $('.add_product_name').val().replace(/ /g, "").length == 0 ? errors["name"] =  { msg: "Product name should'nt empty"} : {};

                // Only numbers
                $('.add_product_price').val().match(numbs) ? [] : errors["price"] = { msg: "Price only filled by number" };

                // Price cant be empty
                $('.add_product_price').val().replace(/ /g, "").length == 0 ? errors["price"] = { msg: "Price can't be empty" } : {};

                if (Object.keys(errors).length === 0) {
                    $(form).submit();
                } else {
                    errors.name && $('#name_error').text(errors.name.msg);
                    errors.price && $('#price_error').text(errors.price.msg);
                }
            }
        });
    }

    // Validate create form -- should put this into event click / submit button?
    validateForm("#create_product_form");

    // Validate edit form -- should put this into event click / submit button?
    validateForm("#edit_product_form");
  
});