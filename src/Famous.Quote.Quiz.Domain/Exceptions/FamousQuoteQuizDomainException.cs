using System;

namespace Famous.Quote.Quiz.Domain.Exceptions
{
    public class FamousQuoteQuizDomainException : Exception
    {
        public FamousQuoteQuizDomainException()
        {
        }

        public FamousQuoteQuizDomainException(string message) : base(message)
        {
        }

        public FamousQuoteQuizDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
