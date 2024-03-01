using Business.Abstracts;
using Business.Validations;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Security;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Business.Concretes;

public class CategoryManager : ICategoryService
{
    public readonly ICategoryRepository _categoryRepository;
    public readonly CategoryValidations _categoryValidations;

    public CategoryManager(ICategoryRepository categoryRepository, CategoryValidations categoryValidations)
    {
        _categoryRepository = categoryRepository;
        _categoryValidations = categoryValidations;
    }

    [TransactionScopeAspect]
    [ValidationAspect(typeof(AddCategoryValidations))]
    [CacheRemoveAspect("Business.Abstracts.ICategoryService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Category added.")]
    [SecurityAspect]
    public Category Add(Category category)
    {
        return _categoryRepository.Add(category);
    }

    [TransactionScopeAspect]
    [ValidationAspect(typeof(AddCategoryValidations))]
    [CacheRemoveAspect("Business.Abstracts.ICategoryService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Category added.")]
    [SecurityAspect]
    public async Task<Category> AddAsync(Category category)
    {
        return await _categoryRepository.AddAsync(category);
    }

    [CacheRemoveAspect("Business.Abstracts.ICategoryService.GetAllAsync")]
    [ValidationAspect(typeof(DeleteCategoryValidations))]
    [DebugWriteSuccessAspect(Message = "Category deleted.")]
    [SecurityAspect]

    public void DeleteById(Guid id)
    {
        var category = _categoryRepository.Get(c => c.Id == id);
        _categoryRepository.Delete(category);
    }

    [CacheRemoveAspect("Business.Abstracts.ICategoryService.GetAllAsync")]
    [ValidationAspect(typeof(DeleteCategoryValidations))]
    [DebugWriteSuccessAspect(Message = "Category deleted.")]
    [SecurityAspect]
    public async Task DeleteByIdAsync(Guid id)
    {
        var category = _categoryRepository.Get(c => c.Id == id);
        await _categoryRepository.DeleteAsync(category);
    }

    [CacheAspect(1)]
    [DebugWriteSuccessAspect(Message = "Category listing completed.")]
    [SecurityAspect]
    public IList<Category> GetAll()
    {
        return _categoryRepository.GetAll().ToList();
    }

    [CacheAspect(1)]
    [DebugWriteSuccessAspect(Message = "Category listing completed.")]
    [SecurityAspect]
    public async Task<IList<Category>> GetAllAsync()
    {
        var result = await _categoryRepository.GetAllAsync();
        return result.ToList();
    }

    [SecurityAspect]
    public IList<Category> GetAllWithProducts()
    {
        return _categoryRepository.GetAll(include: category => category.Include(c => c.Products)).ToList();

    }

    [SecurityAspect]
    public async Task<IList<Category>> GetAllWithProductsAsync()
    {
        var result = await _categoryRepository.GetAllAsync(include: category => category.Include(c => c.Products));
        return result.ToList();
    }

    [SecurityAspect]
    [ValidationAspect(typeof(CategoryValidations))]
    public Category? GetById(Guid id)
    {
        return _categoryRepository.Get(c => c.Id == id);
    }

    [SecurityAspect]
    [ValidationAspect(typeof(CategoryValidations))]
    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _categoryRepository.GetAsync(c => c.Id == id);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.ICategoryService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Category updated.")]
    [SecurityAspect]
    public Category Update(Category category)
    {
        return _categoryRepository.Update(category);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.ICategoryService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Category updated.")]
    [SecurityAspect]
    public async Task<Category> UpdateAsync(Category category)
    {
        return await _categoryRepository.UpdateAsync(category);
    }
}
