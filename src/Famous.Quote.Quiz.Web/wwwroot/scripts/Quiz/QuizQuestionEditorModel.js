var QuizQuestionEditorModel = (function () {
    function init() {
        initBootstrapSwitch();

        $('#save-quiz-question-answer').click(function () {
            var url = $(this).attr('data-url');
            var id = $('#answer-id').val();
            var answerText = $('#answer-text').val();
            var sortIndex = $('#sort-index').val();
            var isCorrect = $('#is-correct').is(':checked');

            $.ajax({
                url: url,
                method: 'POST',
                data: {
                    Id: id,
                    AnswerText: answerText,
                    SortIndex: sortIndex,
                    IsCorrect: isCorrect
                },
                success: function (response) {
                    location.reload();
                },
                error: function (response) {
                    toastrNotification.init(response.responseJSON.message, { notificationType: toastrNotification.types.error }).showMessage();
                }
            });
        });
    }

    function initBootstrapSwitch() {
        $(".checkbox-bootstrap-switch").bootstrapSwitch();
    }

    return {
        init: init
    }
})();