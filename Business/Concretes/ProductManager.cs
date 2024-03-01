using Business.Abstracts;
using Business.Validations;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Security;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using DataAccess.Abstracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Concretes;

public class ProductManager : IProductService
{
    public readonly IProductRepository _productRepository;
    public readonly ProductValidations _productValidations;

    public ProductManager(IProductRepository productRepository, ProductValidations productValidations)
    {
        _productRepository = productRepository;
        _productValidations = productValidations;
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IProductService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Product added.")]
    [SecurityAspect]
    public Product Add(Product product)
    {
        return _productRepository.Add(product);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IProductService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Product added.")]
    [SecurityAspect]
    public async Task<Product> AddAsync(Product product)
    {
        return await _productRepository.AddAsync(product);
    }

    [CacheRemoveAspect("Business.Abstracts.IProductService.GetAllAsync")]
    [ValidationAspect(typeof(DeleteProductValidations))]
    [DebugWriteSuccessAspect(Message = "Product deleted.")]
    [SecurityAspect]
    public void DeleteById(Guid id)
    {
        var product = _productRepository.Get(p => p.Id == id);
        _productRepository.Delete(product);
    }

    [CacheRemoveAspect("Business.Abstracts.IProductService.GetAllAsync")]
    [ValidationAspect(typeof(DeleteProductValidations))]
    [DebugWriteSuccessAspect(Message = "Product deleted.")]
    [SecurityAspect]
    public async Task DeleteByIdAsync(Guid id)
    {
        var product = _productRepository.Get(p => p.Id == id);
        await _productRepository.DeleteAsync(product);
    }

    [CacheAspect(1)]
    [DebugWriteSuccessAspect(Message = "Product listing completed.")]
    [SecurityAspect]
    public IList<Product> GetAll()
    {
        return _productRepository.GetAll().ToList();
    }

    [CacheAspect(1)]

    [DebugWriteSuccessAspect(Message = "Product listing completed.")]
    [SecurityAspect]
    public async Task<IList<Product>> GetAllAsync()
    {
        var result = await _productRepository.GetAllAsync();
        return result.ToList();
    }

    [SecurityAspect]
    public IList<Product> GetAllWithCategory()
    {
        return _productRepository.GetAll(include: product => product.Include(p => p.Category)).ToList();
    }

    [SecurityAspect]
    public async Task<IList<Product>> GetAllWithCategoryAsync()
    {
        var result = await _productRepository.GetAllAsync(include: product => product.Include(p => p.Category));
        return result.ToList();
    }

    [SecurityAspect]
    public IList<Product> GetAllWithProductTransactions()
    {
        return _productRepository.GetAll(include: product => product.Include(p => p.ProductTransactions)).ToList();
    }

    [SecurityAspect]
    public async Task<IList<Product>> GetAllWithProductTransactionsAsync()
    {
        var result = await _productRepository.GetAllAsync(include: product => product.Include(p => p.ProductTransactions));
        return result.ToList();
    }

    [SecurityAspect]
    public Product? GetById(Guid id)
    {
        return _productRepository.Get(p => p.Id == id);
    }

    [SecurityAspect]
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _productRepository.GetAsync(p => p.Id == id);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IProductService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Product updated.")]
    [ValidationAspect(typeof(UpdateProductValidations))]
    [SecurityAspect]
    public Product Update(Product product)
    {
        return _productRepository.Update(product);
    }
    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IProductService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Product updated.")]
    [ValidationAspect(typeof(UpdateProductValidations))]
    [SecurityAspect]
    public async Task<Product> UpdateAsync(Product product)
    {
        return await _productRepository.UpdateAsync(product);
    }
}
