using Core.Repository;
using Entities.Models;

namespace DataAccess.Abstracts;

public interface IOrderRepository: IAsyncRepository<Order>, IRepository<Order>
{
}


