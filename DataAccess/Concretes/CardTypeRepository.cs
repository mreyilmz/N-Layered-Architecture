using Core.Repository;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Models;

namespace DataAccess.Concretes;

public class CardTypeRepository : Repository<CardType>, ICardTypeRepository
{
    public CardTypeRepository(BusinessDbContext context) : base(context)
    {
    }
}



