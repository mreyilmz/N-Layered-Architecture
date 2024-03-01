using Core.Repository;
using Entities.Models;

namespace DataAccess.Abstracts;

public interface IOrderDetailRepository : IAsyncRepository<OrderDetail>, IRepository<OrderDetail>
{
}


