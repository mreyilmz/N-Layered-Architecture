using Core.Repository;
using Entities.Models;

namespace DataAccess.Abstracts;

public interface ICategoryRepository : IAsyncRepository<Category>, IRepository<Category>
{
}



