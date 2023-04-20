jQuery(document).ready(function ($) {

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
                    if (val.status == "publish") {
                        item += `<div class="card-container__item">
                                    <div class="card-wrapper">
                                        ${val.trending == "1" ? "<span class='best-seller'>Best Seller</span>" : ""}
                                        <div class="card-thumbnail">
                                            <img src="/Uploads/${val.thumbnail}" alt="${val.name}" />
                                        </div>
                                            <h2 class="card-title">${val.name}</h2>
                                            <p class="card-price">${val.price}</p>
                                            <p class="short-description">${val.description}</p>
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
                    }
                });
                $("#main_page").append(item);
            },
        })
    })

    // Initialitation Select2
    $('#filter_product').select2();

    // Filter product
    $('#filter_product').on('change', function () {
        var country = $(this).val();

        if (country !== "all") {
            $('.load-more-wrapper').hide();
        } else {
            $('.load-more-wrapper').show();
        }

        $.ajax({
            url: $(".endpoint_filter_request").attr("href"),
            type: 'POST',
            data: {
                country: country
            },
            success: function (res) {
                var data = JSON.parse(res);
                var item = "";
                var edit_product_link = $('.edit_product_link').attr("href");
                var delete_product_link = $('.delete_product_link').attr("href");

                $.each(data, function (key, val) {
                    if (val.status == "publish") {
                        item += `<div class="card-container__item">
                                    <div class="card-wrapper">
                                        ${val.trending == "1" ? "<span class='best-seller'>Best Seller</span>" : ""}
                                        <div class="card-thumbnail">
                                            <img src="/Uploads/${val.thumbnail}" alt="${val.name}" />
                                        </div>
                                            <h2 class="card-title">${val.name}</h2>
                                            <p class="card-price">${val.price}</p>
                                            <p class="short-description">${val.description}</p>
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
                    }
                });
                $("#main_page").html(item);
            }
        })
    })
});