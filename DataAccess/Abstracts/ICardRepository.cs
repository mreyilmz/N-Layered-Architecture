using Core.Repository;
using Entities.Models;

namespace DataAccess.Abstracts;

public interface ICardRepository : IAsyncRepository<Card>, IRepository<Card>
{
}



