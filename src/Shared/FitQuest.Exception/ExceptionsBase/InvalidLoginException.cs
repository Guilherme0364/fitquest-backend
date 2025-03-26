namespace FitQuest.Exceptions.ExceptionsBase
{
    public class InvalidLoginException : FitQuestException
    {
        public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
        {
        }
    }
}
