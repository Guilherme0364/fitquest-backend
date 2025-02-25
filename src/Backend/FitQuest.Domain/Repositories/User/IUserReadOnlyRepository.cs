namespace FitQuest.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistisActiveUserWithEmail(string email);
    }
}
