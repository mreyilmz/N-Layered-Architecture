using Core.Repository;
using Entities.Models;

namespace DataAccess.Abstracts;

public interface IAccountTransactionRepository : IAsyncRepository<AccountTransaction>, IRepository<AccountTransaction>
{
}



