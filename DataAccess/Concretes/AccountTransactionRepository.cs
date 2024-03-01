using Core.Repository;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Models;

namespace DataAccess.Concretes;

public class AccountTransactionRepository : Repository<AccountTransaction>, IAccountTransactionRepository
{
    public AccountTransactionRepository(BusinessDbContext context) : base(context)
    {
    }
}


