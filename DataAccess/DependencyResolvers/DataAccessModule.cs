using Core.Utilities.Tools;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using DataAccess.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DependencyResolvers;

public class DataAccessModule : ICoreModule
{
    public void Load(IServiceCollection services)
    {
        services.AddDbContext<BusinessDbContext>();
        services.AddScoped<IClaimRepository, ClaimRepository>();
        services.AddScoped<IUserClaimRepository, UserClaimRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductTransactionRepository, ProductTransactionRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ICardTypeRepository, CardTypeRepository>();
    }
}