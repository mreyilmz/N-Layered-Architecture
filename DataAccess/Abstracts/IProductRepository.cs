using Core.Repository;
using Entities.Models;

namespace DataAccess.Abstracts;

public interface IProductRepository : IAsyncRepository<Product>, IRepository<Product>
{
}


