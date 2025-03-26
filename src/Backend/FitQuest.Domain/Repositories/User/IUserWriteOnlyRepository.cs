namespace FitQuest.Domain.Repositories.User
{
    // Uma interface do tipo WriteOnly é responsável por atualizar os dados de uma entidade
    public interface IUserWriteOnlyRepository
    {
        public Task Add(Entities.User user);
    }
}
