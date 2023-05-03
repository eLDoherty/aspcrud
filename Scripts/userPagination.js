jQuery(document).ready(function ($) {

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
        if ($('#sortByName').find('.arrow').hasClass('active')) {
            $('#sortByName').attr('data-sorting', 'ASC');
            $('#sortByName').find('.arrow').removeClass('active');
            $('#sortByName').find('.arrow').removeClass('rotated');
        }

        if ($('#sortByEmail').find('.arrow').hasClass('active')) {
            $('#sortByEmail').attr('data-sorting', 'ASC');
            $('#sortByEmail').find('.arrow').removeClass('active');
            $('#sortByEmail').find('.arrow').removeClass('rotated');
        }

        if ($('#sortById').find('.arrow').hasClass('active')) {
            $('#sortById').attr('data-sorting', 'ASC');
            $('#sortById').find('.arrow').removeClass('active');
            $('#sortById').find('.arrow').removeClass('rotated');
        }

        if ($('#sortByRole').find('.arrow').hasClass('active')) {
            $('#sortByRole').attr('data-sorting', 'ASC');
            $('#sortByRole').find('.arrow').removeClass('active');
            $('#sortByRole').find('.arrow').removeClass('rotated');
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
                var users = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";
                if ($('.user_can_delete').length > 0 && $('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_delete').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <span></span>
                                </td>
                            </tr>`;
                    });
                }
                parent.append(item);
            }
        });
    });

    // Handle user pagination step
    $(document).on('click', '.pagination-button', function () {
        var userListURLstep = $('.user_list_endpoint_step').attr('href');
        $('.pagination-button').removeClass('active');
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
                var users = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";
                if ($('.user_can_delete').length > 0 && $('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_delete').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <span></span>
                                </td>
                            </tr>`;
                    });
                }
                parent.append(item);
            }
        });
    });

    // Set data to ajax
    // an id = "pagination_order" data - sorting="ASC" data - name="DESC" ></span >
    var dataForOrder = $('#pagination_order');

    // Sort by ID
    $('#sortById').on('click', function () {
        $(this).attr('data-sorting') == 'ASC' ? $(this).attr('data-sorting', 'DESC') : $(this).attr('data-sorting', 'ASC');
        var name = $(this).attr('data-name');
        var sorting = $(this).attr('data-sorting');
        var rows = $('#userPagination').val();
        var endpoint = $('.user_pagination_id').attr('href');
        dataForOrder.attr('data-sorting', $(this).attr('data-sorting'));
        dataForOrder.attr('data-name', name);
        $(this).find('.arrow').addClass('active');
        $(this).find('.arrow').toggleClass('rotated');$(this).find('.arrow').addClass('active');

        // Remove curremt change on another sorting event
        if ($('#sortByName').find('.arrow').hasClass('active')) {
            $('#sortByName').attr('data-sorting', 'ASC');
            $('#sortByName').find('.arrow').removeClass('active');
            $('#sortByName').find('.arrow').removeClass('rotated');
        }

        if ($('#sortByEmail').find('.arrow').hasClass('active')) {
            $('#sortByEmail').attr('data-sorting', 'ASC');
            $('#sortByEmail').find('.arrow').removeClass('active');
            $('#sortByEmail').find('.arrow').removeClass('rotated');
        }  

        if ($('#sortByRole').find('.arrow').hasClass('active')) {
            $('#sortByRole').attr('data-sorting', 'ASC');
            $('#sortByRole').find('.arrow').removeClass('active');
            $('#sortByRole').find('.arrow').removeClass('rotated');
        }

        // Set step pagination from begining again
        $('#step_pagination li').find('.pagination-button').removeClass('active');
        $('#step_pagination li:first-child').find('.pagination-button').toggleClass('active');

        $.ajax({
            url: endpoint,
            type: 'POST',
            data: {
                sorting: sorting,
                rows: rows
            },
            success: function (data) {
                var users = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";
                if ($('.user_can_delete').length > 0 && $('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_delete').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <span></span>
                                </td>
                            </tr>`;
                    });
                }
                parent.append(item);
            }
        });
    });

    // Sort by name
    $('#sortByName').on('click', function () {
        $(this).attr('data-sorting') == 'ASC' ? $(this).attr('data-sorting', 'DESC') : $(this).attr('data-sorting', 'ASC');
        var name = $(this).attr('data-name');
        var sorting = $(this).attr('data-sorting');
        var rows = $('#userPagination').val();
        var endpoint = $('.user_pagination_name').attr('href');
        dataForOrder.attr('data-sorting', $(this).attr('data-sorting'));
        dataForOrder.attr('data-name', name);
        $(this).find('.arrow').addClass('active');
        $(this).find('.arrow').toggleClass('rotated');

        // Remove curremt change on another sorting event
        if ($('#sortById').find('.arrow').hasClass('active')) {
            $('#sortById').attr('data-sorting', 'ASC');
            $('#sortById').find('.arrow').removeClass('active');
            $('#sortById').find('.arrow').removeClass('rotated');
        }

        if ($('#sortByEmail').find('.arrow').hasClass('active')) {
            $('#sortByEmail').attr('data-sorting', 'ASC');
            $('#sortByEmail').find('.arrow').removeClass('active');
            $('#sortByEmail').find('.arrow').removeClass('rotated');
        }  

        if ($('#sortByRole').find('.arrow').hasClass('active')) {
            $('#sortByRole').attr('data-sorting', 'ASC');
            $('#sortByRole').find('.arrow').removeClass('active');
            $('#sortByRole').find('.arrow').removeClass('rotated');
        }

        // Set step pagination from begining again
        $('#step_pagination li').find('.pagination-button').removeClass('active');
        $('#step_pagination li:first-child').find('.pagination-button').toggleClass('active');

        $.ajax({
            url: endpoint,
            type: 'POST',
            data: {
                sorting: sorting,
                rows: rows
            },
            success: function (data) {
                var users = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";
                if ($('.user_can_delete').length > 0 && $('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_delete').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <span></span>
                                </td>
                            </tr>`;
                    });
                }
                parent.append(item);
            }
        });
    });

    // Sort by email
    $('#sortByEmail').on('click', function () {
        $(this).attr('data-sorting') == 'ASC' ? $(this).attr('data-sorting', 'DESC') : $(this).attr('data-sorting', 'ASC');
        var name = $(this).attr('data-name');
        var sorting = $(this).attr('data-sorting');
        var rows = $('#userPagination').val();
        var endpoint = $('.user_pagination_email').attr('href');
        dataForOrder.attr('data-sorting', $(this).attr('data-sorting'));
        dataForOrder.attr('data-name', name);
        $(this).find('.arrow').addClass('active');
        $(this).find('.arrow').toggleClass('rotated');

        // Set step pagination from begining again
        $('#step_pagination li').find('.pagination-button').removeClass('active');
        $('#step_pagination li:first-child').find('.pagination-button').toggleClass('active');

        // Remove curremt change on another sorting event
        if ($('#sortById').find('.arrow').hasClass('active')) {
            $('#sortById').attr('data-sorting', 'ASC');
            $('#sortById').find('.arrow').removeClass('active');
            $('#sortById').find('.arrow').removeClass('rotated');
        }

        if ($('#sortByName').find('.arrow').hasClass('active')) {
            $('#sortByName').attr('data-sorting', 'ASC');
            $('#sortByName').find('.arrow').removeClass('active');
            $('#sortByName').find('.arrow').removeClass('rotated');
        }   

        if ($('#sortByRole').find('.arrow').hasClass('active')) {
            $('#sortByRole').attr('data-sorting', 'ASC');
            $('#sortByRole').find('.arrow').removeClass('active');
            $('#sortByRole').find('.arrow').removeClass('rotated');
        }

        $.ajax({
            url: endpoint,
            type: 'POST',
            data: {
                sorting: sorting,
                rows: rows
            },
            success: function (data) {
                var users = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";
                if ($('.user_can_delete').length > 0 && $('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_delete').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <span></span>
                                </td>
                            </tr>`;
                    });
                }
                parent.append(item);
            }
        });
    });

    // Sort by Role
    $('#sortByRole').on('click', function () {
        $(this).attr('data-sorting') == 'ASC' ? $(this).attr('data-sorting', 'DESC') : $(this).attr('data-sorting', 'ASC');
        var name = $(this).attr('data-name');
        var sorting = $(this).attr('data-sorting');
        var rows = $('#userPagination').val();
        var endpoint = $('.user_pagination_role').attr('href');
        dataForOrder.attr('data-sorting', $(this).attr('data-sorting'));
        dataForOrder.attr('data-name', name);
        $(this).find('.arrow').addClass('active');
        $(this).find('.arrow').toggleClass('rotated');

        // Set step pagination from begining again
        $('#step_pagination li').find('.pagination-button').removeClass('active');
        $('#step_pagination li:first-child').find('.pagination-button').toggleClass('active');

        // Remove curremt change on another sorting event
        if ($('#sortById').find('.arrow').hasClass('active')) {
            $('#sortById').attr('data-sorting', 'ASC');
            $('#sortById').find('.arrow').removeClass('active');
            $('#sortById').find('.arrow').removeClass('rotated');
        }

        if ($('#sortByName').find('.arrow').hasClass('active')) {
            $('#sortByName').attr('data-sorting', 'ASC');
            $('#sortByName').find('.arrow').removeClass('active');
            $('#sortByName').find('.arrow').removeClass('rotated');
        }

        if ($('#sortByEmail').find('.arrow').hasClass('active')) {
            $('#sortByEmail').attr('data-sorting', 'ASC');
            $('#sortByEmail').find('.arrow').removeClass('active');
            $('#sortByEmail').find('.arrow').removeClass('rotated');
        }

        $.ajax({
            url: endpoint,
            type: 'POST',
            data: {
                sorting: sorting,
                rows: rows
            },
            success: function (data) {
                var users = JSON.parse(data);
                var parent = $('#user_holder');
                parent.html("");
                var item = "";

                if ($('.user_can_delete').length > 0 && $('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                    <span>|</span>
                                    <a class="btn btn-info" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_delete').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/DeleteUser/${user.id}">Delete</a>
                                </td>
                            </tr>`;
                    });
                } else if ($('.user_can_update').length > 0) {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <a class="btn btn-danger" href="/Admin/Edit/${user.id}">Edit</a>
                                </td>
                            </tr>`;
                    });
                } else {
                    $.each(users, function (key, user) {
                        item += `
                            <tr>
                                <td>${user.id}</td>
                                <td>${user.username}</td>
                                <td>${user.email}</td>
                                <td>${user.role}</td>
                                <td>
                                    <span></span>
                                </td>
                            </tr>`;
                    });
                }
                parent.append(item);
            }
        });
    });

});