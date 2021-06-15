using Famous.Quote.Quiz.Application.Services;
using Famous.Quote.Quiz.Application.Services.DataModels;
using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Famous.Quote.Quiz.Domain.Exceptions;
using Famous.Quote.Quiz.Web.Infrastructure;
using Famous.Quote.Quiz.Web.Models.Quizzes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Web.Controllers
{
    public class QuizzesController : BaseController
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        [Route("quizzes", Name = RouteNames.Quiz.Quizzes)]
        public async Task<IActionResult> Quizzes()
        {
            var isPublished = User.IsInRole(UserRole.User.ToString()) ? true : default(bool?);

            var quizzes = await _quizService.GetQuizzesAsync(isPublished);

            return View(new QuizViewModel { Quizzes = quizzes });
        }

        [HttpGet]
        [Route("quizzes/create", Name = RouteNames.Quiz.Create)]
        public IActionResult Create()
        {
            return View(new QuizEditorModel { Quiz = new QuizDto() });
        }

        [HttpPost]
        [Route("quizzes/create", Name = RouteNames.Quiz.Create)]
        public async Task<IActionResult> Create(QuizDto quizDto)
        {
            if (!ModelState.IsValid)
            {
                return View(new QuizEditorModel { Quiz = quizDto });
            }

            try
            {
                var id = await _quizService.AddNewQuizAsync(quizDto);

                InitSuccessMessage();

                return RedirectToRoute(RouteNames.Quiz.Update, new { id = id });
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                InitErrorMessage(ex.Message);
                return View(new QuizEditorModel { Quiz = quizDto });
            }
            catch (Exception)
            {
                InitErrorMessage();
                return View(new QuizEditorModel { Quiz = quizDto });
            }
        }

        [HttpGet]
        [Route("quizzes/{id}/update", Name = RouteNames.Quiz.Update)]
        public async Task<IActionResult> Update(int id)
        {
            var quizDto = await _quizService.GetSingleQuizByIdAsync(id);

            if (quizDto == default)
            {
                return NotFound();
            }

            return View(new QuizEditorModel { Quiz = quizDto });
        }

        [HttpPost]
        [Route("quizzes/{id}/update", Name = RouteNames.Quiz.Update)]
        public async Task<IActionResult> Update(QuizDto quizDto)
        {
            if (!ModelState.IsValid)
            {
                return View(new QuizEditorModel { Quiz = quizDto });
            }

            try
            {
                await _quizService.UpdateQuizAsync(quizDto);

                InitSuccessMessage();

                return RedirectToRoute(RouteNames.Quiz.Update, new { id = quizDto.Id });
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                InitErrorMessage(ex.Message);
                return View(new QuizEditorModel { Quiz = quizDto });
            }
            catch (Exception)
            {
                InitErrorMessage();
                return View(new QuizEditorModel { Quiz = quizDto });
            }
        }

        [HttpPost]
        [Route("quizzes/{id}/delete", Name = RouteNames.Quiz.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _quizService.DeleteQuizAsync(id);

                return Ok(new { message = "Quiz deleted successfully" });
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = _defaultErrorMessage });
            }
        }

        [HttpGet]
        [Route("quizzes/{id}/questions", Name = RouteNames.Quiz.Questions)]
        public async Task<IActionResult> QuizQuestions(int id)
        {
            var quizDto = await _quizService.GetQuizWithQuestionsAsync(id);

            if (quizDto == default)
            {
                return NotFound();
            }

            return View(new QuizEditorModel { Quiz = quizDto });
        }

        [HttpPost]
        [Route("quizzes/{id}/questions/create", Name = RouteNames.Quiz.QuestionsCreate)]
        public async Task<IActionResult> CreateQuestion(int id, QuestionDto questionDto)
        {
            if (string.IsNullOrEmpty(questionDto.QuestionText))
            {
                return BadRequest(new { message = "Please fill 'Question Text' field" });
            }
            try
            {
                await _quizService.AddNewQuizQuestionAsync(id, questionDto);

                return Ok();
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = _defaultErrorMessage });
            }
        }

        [HttpPost]
        [Route("quizzes/{quizId}/questions/{id}/delete", Name = RouteNames.Quiz.QuestionsDelete)]
        public async Task<IActionResult> DeleteQuestion(int quizId, int id)
        {
            try
            {
                await _quizService.DeleteQuizQuestionAsync(quizId, id);

                return Ok(new { message = _defaultSuccessMessage });
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = _defaultErrorMessage });
            }
        }

        [HttpGet]
        [Route("quizzes/{quizId}/questions/{id}/update", Name = RouteNames.Quiz.QuestionsUpdate)]
        public async Task<IActionResult> UpdateQuestion(int quizId, int id)
        {
            var (quiz, question) = await _quizService.GetQuizQuestionWithQuizAsync(quizId, id);

            if (quiz == default || question == default)
            {
                return NotFound();
            }

            return View(new QuizQuestionEditorModel { Quiz = quiz, Question = question });
        }

        [HttpPost]
        [Route("quizzes/{quizId}/questions/{id}/update", Name = RouteNames.Quiz.QuestionsUpdate)]
        public async Task<IActionResult> UpdateQuestion(int quizId, int id, QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                var quiz = await _quizService.GetSingleQuizByIdAsync(quizId);

                return View(new QuizQuestionEditorModel { Quiz = quiz, Question = questionDto });
            }

            try
            {
                await _quizService.UpdateQuizQuestionAsync(quizId, questionDto);
                InitSuccessMessage();

                return RedirectToRoute(RouteNames.Quiz.QuestionsUpdate, new { quizId, id });
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                var quiz = await _quizService.GetSingleQuizByIdAsync(quizId);
                InitErrorMessage(ex.Message);
                return View(new QuizQuestionEditorModel { Quiz = quiz, Question = questionDto });
            }
            catch (Exception)
            {
                var quiz = await _quizService.GetSingleQuizByIdAsync(quizId);
                InitErrorMessage();
                return View(new QuizQuestionEditorModel { Quiz = quiz, Question = questionDto });
            }

        }

        [HttpPost]
        [Route("quizzes/{quizId}/questions/{questionId}/answers/create", Name = RouteNames.Quiz.QuestionAnswerCreate)]
        public async Task<IActionResult> CreateQuestionAnswer(int quizId, int questionId, AnswerDto answerDto)
        {
            try
            {
                await _quizService.CreateQuestionAnswerAsync(quizId, questionId, answerDto);

                return Ok(new { message = _defaultSuccessMessage });
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = _defaultErrorMessage });
            }
        }
    }
}
