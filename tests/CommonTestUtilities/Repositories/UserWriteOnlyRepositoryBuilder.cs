using FitQuest.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserWriteOnlyRepositoryBuilder
    {
        public static IUserWriteOnlyRepository Build()
        {
            var mock = new Mock<IUserWriteOnlyRepository>();

            // Retorna a implementação "fake" da UnityOfWork
            return mock.Object;
        }
    }
}
