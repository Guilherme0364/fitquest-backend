using FitQuest.Domain.Repositories;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UnityOfWorkBuilder
    {
        public static IUnityOfWork Build()
        {
            var mock = new Mock<IUnityOfWork>();

            // Retorna a implementação "fake" da UnityOfWork
            return mock.Object;
        }
    }
}
