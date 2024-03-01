using Business.Tools.Exceptions;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;
using Entities.Models;


namespace Business.Validations;

public class CategoryValidations : BaseValidation
{
    protected readonly ICategoryRepository _categoryRepository;
    public CategoryValidations(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task CategoryMustNotBeEmpty(Category? category)
    {
        if (category == null)
        {
            throw new ValidationException("Category must not be empty.", 500);
        }
        await Task.CompletedTask;
    }
}

public class AddCategoryValidations : CategoryValidations
{
    public AddCategoryValidations(ICategoryRepository categoryRepository) : base(categoryRepository)
    {
    }

}
public class UpdateCategoryValidations : CategoryValidations
{
    public UpdateCategoryValidations(ICategoryRepository categoryRepository) : base(categoryRepository)
    {
    }
}

public class DeleteCategoryValidations : BaseValidation
{
    protected readonly ICategoryRepository _categoryRepository;
    public DeleteCategoryValidations(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task CategoryNotFound(Guid id)
    {
        var dbCategory = await _categoryRepository.GetAsync(c => c.Id == id);
        if (dbCategory == null)
        {
            throw new ValidationException("Category not found.", 400);
        }
        await Task.CompletedTask;
    }
}
