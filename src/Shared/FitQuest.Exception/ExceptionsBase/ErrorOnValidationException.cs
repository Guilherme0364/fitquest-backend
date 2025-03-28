﻿namespace FitQuest.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : FitQuestException
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessage) : base(string.Empty)
        {
            ErrorMessages = errorMessage;
        }
    }
}
