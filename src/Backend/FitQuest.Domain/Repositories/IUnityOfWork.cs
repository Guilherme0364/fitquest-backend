namespace FitQuest.Domain.Repositories
{
    public interface IUnityOfWork
    {
        public Task Commit();
    }
}
