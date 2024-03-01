using Business.Abstracts;
using Business.Validations;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Security;
using Core.Aspects.Autofac.Transaction;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes;

public class ProductTransactionManager : IProductTransactionService
{
    public readonly IProductTransactionRepository _productTransactionRepository;
    public readonly ProductTransactionValidations _productTransactionValidations;

    public ProductTransactionManager(IProductTransactionRepository productTransactionRepository, ProductTransactionValidations productTransactionValidations)
    {
        _productTransactionRepository = productTransactionRepository;
        _productTransactionValidations = productTransactionValidations;
    }
    public ProductTransaction Add(ProductTransaction productTransaction)
    {
        return _productTransactionRepository.Add(productTransaction);
    }

    public async Task<ProductTransaction> AddAsync(ProductTransaction productTransaction)
    {
        return await _productTransactionRepository.AddAsync(productTransaction);
    }

    public void DeleteById(Guid id)
    {
        var productTransaction = _productTransactionRepository.Get(pt => pt.Id == id);
        _productTransactionValidations.ProductTransactionMustNotBeEmpty(productTransaction).Wait();
        _productTransactionRepository.Delete(productTransaction);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var productTransaction = _productTransactionRepository.Get(pt => pt.Id == id);
        await _productTransactionValidations.ProductTransactionMustNotBeEmpty(productTransaction);
        await _productTransactionRepository.DeleteAsync(productTransaction);
    }

    public IList<ProductTransaction> GetAll()
    {
        return _productTransactionRepository.GetAll().ToList();
    }

    public async Task<IList<ProductTransaction>> GetAllAsync()
    {
        var result = await _productTransactionRepository.GetAllAsync();
        return result.ToList();
    }

    public IList<ProductTransaction> GetAllWithProduct()
    {
        return _productTransactionRepository.GetAll(include: productTransaction => productTransaction.Include(pt => pt.Product)).ToList();
    }

    public async Task<IList<ProductTransaction>> GetAllWithProductAsync()
    {
        var result = await _productTransactionRepository.GetAllAsync(include: productTransaction => productTransaction.Include(pt => pt.Product));
        return result.ToList();
    }

    public ProductTransaction? GetById(Guid id)
    {
        return _productTransactionRepository.Get(pt => pt.Id == id);
    }

    public async Task<ProductTransaction?> GetByIdAsync(Guid id)
    {
        return await _productTransactionRepository.GetAsync(pt => pt.Id == id);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IProductTransactionService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Product transaction updated.")]
    [SecurityAspect]
    public ProductTransaction Update(ProductTransaction productTransaction)
    {
        return _productTransactionRepository.Update(productTransaction);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.IProductTransactionService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Product transaction updated.")]
    [SecurityAspect]
    public async Task<ProductTransaction> UpdateAsync(ProductTransaction productTransaction)
    {
        return await _productTransactionRepository.UpdateAsync(productTransaction);
    }
}
