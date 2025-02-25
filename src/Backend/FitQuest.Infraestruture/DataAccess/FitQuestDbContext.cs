using Microsoft.EntityFrameworkCore;
using FitQuest.Domain.Entities;

namespace FitQuest.Infraestruture.DataAccess
{
    public class FitQuestDbContext : DbContext
    {
        public FitQuestDbContext(DbContextOptions options) : base(options)
        {

        }

        // DbSets são as tabelas que o ORM poderá modificar
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Faz com que o Entity use as configurações corretas, que estão nessa Classe
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitQuestDbContext).Assembly);
        }
    }
}
