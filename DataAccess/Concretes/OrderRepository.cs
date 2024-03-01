using Core.Repository;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Models;

namespace DataAccess.Concretes;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(BusinessDbContext context) : base(context)
    {
    }
}









