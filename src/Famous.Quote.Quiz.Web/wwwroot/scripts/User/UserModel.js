var UserModel = (function() {
    function init() {
        $('#users-table').on('click', '.delete-btn', function (e) {
            e.preventDefault();
            var _this = $(this);

            var url = $(this).attr('data-url');

            bootbox.confirm({
                message: "Do you really want to delete user?",
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-success'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-danger'
                    }
                },
                callback: function (result) {
                    if (result) {
                        $.ajax({
                            url: url,
                            method: 'POST',
                            success: function (response) {
                                _this.closest('tr').remove();
                                toastrNotification.init(response.message).showMessage();
                            },
                            error: function (response) {
                                toastrNotification.init(response.responseJSON.message, { notificationType: toastrNotification.types.error }).showMessage();
                            }
                        });
                    }
                }
            });

        });

        $('#users-table').on('click', '.disable-btn', function (e) {
            e.preventDefault();
            var _this = $(this);

            var url = $(this).attr('data-url');
            
            $.ajax({
                url: url,
                method: 'POST',
                success: function (response) {
                    _this.addClass('hidden');
                    _this.parent().find('.activate-btn').removeClass('hidden');
                    toastrNotification.init(response.message).showMessage();
                },
                error: function (response) {
                    toastrNotification.init(response.responseJSON.message, { notificationType: toastrNotification.types.error }).showMessage();
                }
            });
        });

        $('#users-table').on('click', '.activate-btn', function (e) {
            e.preventDefault();
            var _this = $(this);

            var url = $(this).attr('data-url');

            $.ajax({
                url: url,
                method: 'POST',
                success: function (response) {
                    _this.addClass('hidden');
                    _this.parent().find('.disable-btn').removeClass('hidden');
                    toastrNotification.init(response.message).showMessage();
                },
                error: function (response) {
                    toastrNotification.init(response.responseJSON.message, { notificationType: toastrNotification.types.error }).showMessage();
                }
            });
        });
    }

    return {
        init: init
    }
})();