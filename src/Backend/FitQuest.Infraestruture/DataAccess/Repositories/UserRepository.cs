using FitQuest.Domain.Entities;
using FitQuest.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace FitQuest.Infraestruture.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly FitQuestDbContext _dbContext;

        public UserRepository(FitQuestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.Users.AddAsync(user);
        }

        public async Task<bool> ExistisActiveUserWithEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
        }
    }
}
