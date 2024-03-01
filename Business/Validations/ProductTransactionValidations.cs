using Business.Tools.Exceptions;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;
using Entities.Models;

namespace Business.Validations;

public class ProductTransactionValidations : BaseValidation
{
    protected readonly IProductTransactionRepository _productTransactionRepository;
    public ProductTransactionValidations(IProductTransactionRepository productTransactionRepository)
    {
        _productTransactionRepository = productTransactionRepository;
    }

    public async Task ProductTransactionMustNotBeEmpty(ProductTransaction? productTransaction)
    {
        if (productTransaction == null)
        {
            throw new ValidationException("Product transaction must not be empty.", 500);
        }
        await Task.CompletedTask;
    }
}

public class AddProductTransactionValidations : ProductTransactionValidations
{
    public AddProductTransactionValidations(IProductTransactionRepository productTransactionRepository) : base(productTransactionRepository)
    {
    }

}
public class UpdateProductTransactionValidations : ProductTransactionValidations
{
    public UpdateProductTransactionValidations(IProductTransactionRepository productTransactionRepository) : base(productTransactionRepository)
    {
    }

}


public class DeleteProductTransactionValidations : BaseValidation
{
    protected readonly IProductTransactionRepository _productTransactionRepository;
    public DeleteProductTransactionValidations(IProductTransactionRepository productTransactionRepository)
    {
        _productTransactionRepository = productTransactionRepository;
    }
    public async Task ProductTransactionNotFound(Guid id)
    {
        var dbProductTransaction = await _productTransactionRepository.GetAsync(p => p.Id == id);
        if (dbProductTransaction == null)
        {
            throw new ValidationException("Product transaction not found.", 400);
        }
        await Task.CompletedTask;
    }
}



