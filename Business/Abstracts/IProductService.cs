using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts;

public interface IProductService
{
    Product? GetById(Guid id);
    Task<Product?> GetByIdAsync(Guid id);
    IList<Product> GetAll();
    Task<IList<Product>> GetAllAsync();
    IList<Product> GetAllWithCategory();
    Task<IList<Product>> GetAllWithCategoryAsync();
    IList<Product> GetAllWithProductTransactions();
    Task<IList<Product>> GetAllWithProductTransactionsAsync();
    Product Add(Product product);
    Product Update(Product product);
    void DeleteById(Guid id);
    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task DeleteByIdAsync(Guid id);
}
