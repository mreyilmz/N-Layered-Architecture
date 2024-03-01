using Business.Tools.Exceptions;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;
using Entities.Models;

namespace Business.Validations;

public class ProductValidations:BaseValidation
{
    protected readonly IProductRepository _productRepository;
    public ProductValidations(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task ProductMustNotBeEmpty(Product? product)
    {
        if (product == null)
        {
            throw new ValidationException("Product must not be empty.", 500);
        }
        await Task.CompletedTask;
    }
}

public class AddProductValidations : ProductValidations
{
    public AddProductValidations(IProductRepository productRepository) : base(productRepository)
    {
    }

}
public class UpdateProductValidations : ProductValidations
{
    public UpdateProductValidations(IProductRepository productRepository) : base(productRepository)
    {
    }

}


public class DeleteProductValidations : BaseValidation
{
    protected readonly IProductRepository _productRepository;
    public DeleteProductValidations(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task ProductNotFound(Guid id)
    {
        var dbProduct = await _productRepository.GetAsync(p => p.Id == id);
        if (dbProduct == null)
        {
            throw new ValidationException("Product not found.", 400);
        }
        await Task.CompletedTask;
    }
}



