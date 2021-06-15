var QuizQuestionModel = (function () {
    function init() {
        $('#quiz-questions-table').on('click', '.delete-btn', function (e) {
            e.preventDefault();
            var _this = $(this);

            var url = $(this).attr('data-url');

            bootbox.confirm({
                message: "Do you really want to delete quiz question?",
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

        $('#save-quiz').click(function () {
            var url = $(this).attr('data-url');

            var questionText = $('#question-text').val();
            var sortIndex = $('#sort-index').val();
            var isActive = $('#is-active').is(':checked');

            $.ajax({
                url: url,
                method: 'POST',
                data: {
                    QuestionText: questionText,
                    SortIndex: sortIndex,
                    IsActive: isActive
                },
                success: function (response) {
                    location.reload();
                },
                error: function (response) {
                    console.log(response);
                    toastrNotification.init(response.responseJSON.message, { notificationType: toastrNotification.types.error }).showMessage();
                }
            });
        });

        initBootstrapSwitch();
    }

    function initBootstrapSwitch() {
        $(".checkbox-bootstrap-switch").bootstrapSwitch();
    }

    return {
        init: init
    }
})();