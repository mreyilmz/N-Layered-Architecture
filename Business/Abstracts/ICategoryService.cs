using Core.Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts;

public interface ICategoryService
{
    Category? GetById(Guid id);
    Task<Category?> GetByIdAsync(Guid id);
    IList<Category> GetAll();
    Task<IList<Category>> GetAllAsync();
    IList<Category> GetAllWithProducts();
    Task<IList<Category>> GetAllWithProductsAsync();
    Category Add(Category category);
    Category Update(Category category);
    void DeleteById(Guid id);
    Task<Category> AddAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task DeleteByIdAsync(Guid id);
}
