using Core.Repository;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Models;

namespace DataAccess.Concretes;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(BusinessDbContext context) : base(context)
    {
    }
}




