using Core.Repository;
using Entities.Models;

namespace DataAccess.Abstracts;

public interface IProductTransactionRepository : IAsyncRepository<ProductTransaction>, IRepository<ProductTransaction>
{
}


