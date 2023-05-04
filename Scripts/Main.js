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
                if ($('.appear_is_admin').length > 0) {
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
                } else if ($('.is_editor').length > 0) {
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
                                                    </div>
                                                </div>
                                          </div>
                                    </div>`;
                            }
                    });
                } else if ($('.is_deletion').length > 0) {
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
                                                            <a href="${delete_product_link}/${val.id}" class="btn btn-danger delete_button">Edit</a>                                    
                                                        </div>
                                                    </div>
                                                </div>
                                          </div>
                                    </div>`;
                        }
                    });
                }
                else {
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
                                        </div>
                                    </div>`;
                        }
                    });
                }

                if (data.length <= 3) {
                    $('#load_more_product').hide();
                }
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
                if ($('.appear_is_admin').length > 0) {
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
                } else if ($('.is_editor').length > 0) {
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
                                                    </div>
                                                </div>
                                          </div>
                                    </div>`;
                        }
                    });
                } else if ($('.is_deletion').length > 0) {
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
                                                            <a href="${delete_product_link}/${val.id}" class="btn btn-danger delete_button">Edit</a>                                    
                                                        </div>
                                                    </div>
                                                </div>
                                          </div>
                                    </div>`;
                        }
                    });
                } else {
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
                                        </div>
                                    </div>`;
                        }
                    });
                }
                $("#main_page").html(item);
            }
        })
    });

    // Modal edit category handler
    $(document).on('click', '.button_edit_category',function (e) {
        e.preventDefault();
        var categoryName = $(this).parent().siblings('.category-name').text();
        $('#catId').val($(this).attr('id'));
        $('.edit-category-wrapper').slideDown();
        $('#catName').val(categoryName);
    });

    $('#close-edit-category-modal').on('click', function () {
        $('.edit-category-wrapper').slideUp();
    });

    // Category pagination
    // Steps
    var totalRecord = $('#total_user').text();
    var postPerPage = $('#userPagination').val();
    var steps = Math.ceil(parseInt(totalRecord) / parseInt(postPerPage));

    $('#step_pagination').html('');

    var step = '';

    for (var i = 1; i <= steps; i++) {
        if (i == 1) {
            step += `<li><button class="pagination-button active" value="${i}">${i}</button></li>`
        } else {
            step += `<li><button class="pagination-button" value="${i}">${i}</button></li>`
        }
    }

    $('#step_pagination').append(step);

    // Handle user pagination per page
    $('#userPagination').on('change', function (e) {
        // Steps
        var totalRecord = $('#total_user').text();
        var postPerPage = $(this).val();
        var steps = Math.ceil(parseInt(totalRecord) / parseInt(postPerPage));

        if ($(this).val() > parseInt($('#total_user').text())) {
            $('#step_pagination').hide();
        }

        if ($(this).val() < parseInt($('#total_user').text())) {
            $('#step_pagination').show();
        }

        // Remove curremt change on another sorting event
        if ($('#sortById').find('.arrow').hasClass('active')) {
            $('#sortById').attr('data-sorting', 'ASC');
            $('#sortById').find('.arrow').removeClass('active');
            $('#sortById').find('.arrow').removeClass('rotated');
        }

        if ($('#sortByCategory').find('.arrow').hasClass('active')) {
            $('#sortByCategory').attr('data-sorting', 'ASC');
            $('#sortByCategory').find('.arrow').removeClass('active');
            $('#sortByCategory').find('.arrow').removeClass('rotated');
        }

        $('#step_pagination').html('');

        var step = '';

        for (var i = 1; i <= steps; i++) {
            if (i == 1) {
                step += `<li><button class="pagination-button active" value="${i}">${i}</button></li>`
            } else {
                step += `<li><button class="pagination-button" value="${i}">${i}</button></li>`
            }
        }

        $('#step_pagination').append(step);

        var userListURL = $('.user_list_endpoint').attr('href');

        // Ajax request total post per page
        $.ajax({
            url: userListURL,
            type: 'POST',
            data: {
                totalDisplay: $(this).val()
            },
            success: function (data) {
                var categories = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";
                $.each(categories, function (key, cat) {
                    item += `
                            <tr>
                                <td class="col-md-1">${cat.id}</td>
                                <td class="col-md-5 category-name">${cat.category}</td>
                                <td class="col-md-3">
                                    <a class="btn btn-danger" href="/Product/DeleteCategory/${cat.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info button_edit_category" href="/Poduct/EditCategory/${cat.id}" id="${cat.id}">Edit</a>
                                </td>
                            </tr>`;
                });    
                parent.append(item);
            }

        });
    });

    // Handle user pagination step -- Category
    $(document).on('click', '.pagination-button', function () {
        var userListURLstep = $('.user_list_endpoint_step').attr('href');
        $('.pagination-button').removeClass('active');
        $('.sorter_name').attr('data-page', $(this).val());
        $(this).addClass('active');
        var page = $(this).val();
        var rows = $('#userPagination').val();
        var sorting = $('#pagination_order').attr('data-sorting');
        var name = $('#pagination_order').attr('data-name');

        var data = {
            page: page,
            rows: rows,
            sorting: sorting,
            name: name,
        }

        $.ajax({
            url: userListURLstep,
            type: 'POST',
            data: data,

            success: function (data) {
                var categories = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";
                $.each(categories, function (key, cat) {
                    item += `
                            <tr>
                                <td class="col-md-1">${cat.id}</td>
                                <td class="col-md-5 category-name">${cat.category}</td>
                                <td class="col-md-3">
                                    <a class="btn btn-danger" href="/Product/DeleteCategory/${cat.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info button_edit_category" href="/Product/EditCategory/${cat.id}" id="${cat.id}">Edit</a>
                                </td>
                            </tr>`;
                });
                parent.append(item);
            }
        });
    });

    var dataForOrder = $('#pagination_order');

    // Sort by category id
    $('#sortById').on('click', function () {
        $(this).attr('data-sorting') == 'ASC' ? $(this).attr('data-sorting', 'DESC') : $(this).attr('data-sorting', 'ASC');
        var name = $(this).attr('data-name');
        var sorting = $(this).attr('data-sorting');
        var rows = $('#userPagination').val();
        var page = $(this).attr('data-page');
        var endpoint = $('.category_pagination_id').attr('href');
        dataForOrder.attr('data-sorting', $(this).attr('data-sorting'));
        dataForOrder.attr('data-name', name);
        $(this).find('.arrow').addClass('active');
        $(this).find('.arrow').toggleClass('rotated'); $(this).find('.arrow').addClass('active');

        // Remove curremt change on another sorting event
        if ($('#sortByCategory').find('.arrow').hasClass('active')) {
            $('#sortByCategory').attr('data-sorting', 'ASC');
            $('#sortByCategory').find('.arrow').removeClass('active');
            $('#sortByCategory').find('.arrow').removeClass('rotated');
        }

        $.ajax({
            url: endpoint,
            type: 'POST',
            data: {
                sorting: sorting,
                rows: rows,
                page: page
            },
            success: function (data) {
                var categories = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";
                $.each(categories, function (key, cat) {
                    item += `
                            <tr>
                                <td class="col-md-1">${cat.id}</td>
                                <td class="col-md-5 category-name">${cat.category}</td>
                                <td class="col-md-3">
                                    <a class="btn btn-danger" href="/Product/DeleteCategory/${cat.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info button_edit_category" href="/Product/EditCategory/${cat.id}">Edit</a>
                                </td>
                            </tr >`;
                });
                parent.append(item);
            }
        });
    });

    // Sort by category
    $('#sortByCategory').on('click', function () {
        $(this).attr('data-sorting') == 'ASC' ? $(this).attr('data-sorting', 'DESC') : $(this).attr('data-sorting', 'ASC');
        var name = $(this).attr('data-name');
        var sorting = $(this).attr('data-sorting');
        var rows = $('#userPagination').val();
        var page = $(this).attr('data-page');
        var endpoint = $('.category_pagination_name').attr('href');
        dataForOrder.attr('data-sorting', $(this).attr('data-sorting'));
        dataForOrder.attr('data-name', name);
        $(this).find('.arrow').addClass('active');
        $(this).find('.arrow').toggleClass('rotated'); $(this).find('.arrow').addClass('active');

        // Remove curremt change on another sorting event
        if ($('#sortById').find('.arrow').hasClass('active')) {
            $('#sortById').attr('data-sorting', 'ASC');
            $('#sortById').find('.arrow').removeClass('active');
            $('#sortById').find('.arrow').removeClass('rotated');
        }

        $.ajax({
            url: endpoint,
            type: 'POST',
            data: {
                sorting: sorting,
                rows: rows,
                page: page
            },
            success: function (data) {
                var categories = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";
                $.each(categories, function (key, cat) {
                    item += `
                            <tr>
                                <td class="col-md-1">${cat.id}</td>
                                <td class="col-md-5 category-name">${cat.category}</td>
                                <td class="col-md-3">
                                    <a class="btn btn-danger" href="/Product/DeleteCategory/${cat.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info button_edit_category" href="/Product/EditCategory/${cat.id}" id="${cat.id}">Edit</a>
                                </td>
                            </tr >`;
                });
                parent.append(item);
            }
        });
    });

    // Search user ajaxify
    $('#search-category').on('keyup', function () {
        var key = $(this).val();
        var endpoint = $('.search_category').attr('href');
        $('.user-pagination-wrapper').hide();
        if ($(this).val().length == 0) {
            location.reload(true);
        }

        console.log(key);
        $.ajax({
            url: endpoint,
            type: 'POST',
            data: {
                key: key
            },
            success: function (data) {
                var categories = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";

                if (categories.length == 0) {
                    $('#empty-category').text('Categories not found');
                } else {
                    $('#empty-category').text('');
                }
                $.each(categories, function (key, cat) {
                    item += `
                            <tr>
                                <td class="col-md-1">${cat.id}</td>
                                <td class="col-md-5 category-name">${cat.category}</td>
                                <td class="col-md-3">
                                    <a class="btn btn-danger" href="/Product/DeleteCategory/${cat.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info button_edit_category" href="/Product/EditCategory/${cat.id}" id="${cat.id}">Edit</a>
                                </td>
                            </tr >`;
                });
                parent.append(item);
            }
        });
    });

});

