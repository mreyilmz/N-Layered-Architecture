using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts;

public interface IProductTransactionService
{
    ProductTransaction? GetById(Guid id);
    Task<ProductTransaction?> GetByIdAsync(Guid id);
    IList<ProductTransaction> GetAll();
    Task<IList<ProductTransaction>> GetAllAsync();
    IList<ProductTransaction> GetAllWithProduct();
    Task<IList<ProductTransaction>> GetAllWithProductAsync();
    ProductTransaction Add(ProductTransaction productTransaction);
    ProductTransaction Update(ProductTransaction productTransaction);
    void DeleteById(Guid id);
    Task<ProductTransaction> AddAsync(ProductTransaction productTransaction);
    Task<ProductTransaction> UpdateAsync(ProductTransaction productTransaction);
    Task DeleteByIdAsync(Guid id);
}
