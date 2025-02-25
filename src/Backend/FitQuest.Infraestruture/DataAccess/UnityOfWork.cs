using FitQuest.Domain.Repositories;
using FitQuest.Infraestruture.DataAccess;

namespace FitQuest.Infraestructure.DataAccess
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly FitQuestDbContext _dbContext;

        public UnityOfWork(FitQuestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
