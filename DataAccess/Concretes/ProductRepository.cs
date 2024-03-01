using Core.Repository;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Models;

namespace DataAccess.Concretes;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(BusinessDbContext context) : base(context)
    {
    }
}









