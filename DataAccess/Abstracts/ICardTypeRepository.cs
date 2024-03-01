using Core.Repository;
using Entities.Models;

namespace DataAccess.Abstracts;

public interface ICardTypeRepository : IAsyncRepository<CardType>, IRepository<CardType>
{
}