namespace FitQuest.Domain.Repositories.User
{
    // Uma interface do tipo ReadOnly não atualiza nenhum tipo de dado da entidade em questão
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistisActiveUserWithEmail(string email);

        public Task<Entities.User?> GetByEmailAndPassword(string email, string password); // Caso alguma variavel esteja errada, o usuário devolvido será nulo, por isso a interrogação
    }
}
