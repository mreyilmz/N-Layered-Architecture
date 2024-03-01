using Core.Repository;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Models;

namespace DataAccess.Concretes;

public class ProductTransactionRepository : Repository<ProductTransaction>, IProductTransactionRepository
{
    public ProductTransactionRepository(BusinessDbContext context) : base(context)
    {
    }
}









