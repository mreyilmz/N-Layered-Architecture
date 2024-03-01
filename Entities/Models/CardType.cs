using Core.Repository;

namespace Entities.Models;

public class CardType : Entity<Guid>
{
    public string Name { get; set; }
}