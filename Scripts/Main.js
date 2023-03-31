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
    
    // Handle delete button
    $(".delete_button").click(function (e) {
        e.preventDefault();
        var cnfrm = confirm("Are you sure?");
        var url = $(this).attr("href");
        if (cnfrm) {
            window.location = url;
        }
    });

    // Pagination with ajax
    $("#load_more_product").click(function () {

        $(this).val(parseInt($(this).val()) + 1);

        $.ajax({
            url: $(".endpoint_pagination_request").attr("href"),
            type: 'POST',
            data: {
                page: $(this).val()
            },
            success: function (res) {
                var data = JSON.parse(res);
                var item = "";
                var edit_product_link = $('.edit_product_link').attr("href");
                var delete_product_link = $('.delete_product_link').attr("href");
                console.log(data);
                $.each(data, function (key, val) {
                    item += `<div class="col-md-4">
                                <div class="card-wrapper">
                                    <div class="card-thumbnail">
                                        <img src="/Uploads/${val.thumbnail}" alt="${val.name}" />
                                    </div>
                                        <h2 class="card-title">${val.name}</h2>
                                        <p class="card-price">${val.price}</p>
                                        <div class="card-action">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <a href="${edit_product_link}/${val.id}" class="btn btn-info">Edit</a>
                                       
                                                </div>
                                                <div class="col-md-6">
                                                     <a href="${delete_product_link}/${val.id}" class="btn btn-danger delete_button">Delete</a>
                                      
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>`;
                });
                $("#main_page").append(item);
            },
        })
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

    // Test
    $('#trending').on("change", function () {
        console.log($(this).val());
    })

});