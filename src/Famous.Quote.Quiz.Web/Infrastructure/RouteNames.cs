namespace Famous.Quote.Quiz.Web.Infrastructure
{
    public struct RouteNames
    {
        public struct Account
        {
            public const string Login = "LoginPage";
            public const string Logout = "LogoutPage";
        }

        public struct Home
        {
            public const string HomePage = "HomePage";
        }

        public struct User
        {
            public const string Users = "UsersPage";
            public const string Update = "UsersUpdate";
            public const string Create = "UsersCreate";
            public const string Delete = "UsersDelete";
            public const string Disable = "UsersDisable";
            public const string Activate = "UsersActivate";
        }

        public struct Quiz
        {
            public const string Quizzes = "QuizzesPage";
            public const string Update = "QuizzesUpdate";
            public const string Create = "QuizzesCreate";
            public const string Delete = "QuizzesDelete";

            public const string Questions = "QuizQuestions";
            public const string QuestionsUpdate = "QuizQuestionsUpdate";
            public const string QuestionsCreate = "QuizQuestionsCreate";
            public const string QuestionsDelete = "QuizQuestionsDelete";

            public const string QuestionAnswerCreate = "QuizQuestionAnswerCreate";
            public const string QuestionAnswerUpdate = "QuizQuestionAnswerUpdate";
            public const string QuestionAnswerDelete = "QuizQuestionAnswerDelete";

        }

        public struct Setting
        {
            public const string Settings = "SettingsPage";

        }
    }
}
