﻿using FitQuest.Domain.Repositories.User;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder() 
        { 
            _repository = new Mock<IUserReadOnlyRepository>();
        }

        public void ExistActiveUserWithEmail(string email)
        {
            // Ela da acesso às funções da interface
            _repository.Setup(repository => repository.ExistisActiveUserWithEmail(email)).ReturnsAsync(true);
        }

        public IUserReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
