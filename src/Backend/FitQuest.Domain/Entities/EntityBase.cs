namespace FitQuest.Domain.Entities
{
    public class EntityBase
    {
        // Todas as entidades vão ter esses atributos via herança
        public long Id { get; set; }
        public bool Active { get; set; } = true; // Para não vir false como default
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
