using Business.Tools.Exceptions;
using Core.CrossCuttingConcerns.Validation;
using Core.Entities;
using DataAccess.Abstracts;
using Entities.Models;

namespace Business.Validations;

public class ClaimValidations : BaseValidation
{
    protected readonly IClaimRepository _claimRepository;
    public ClaimValidations(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository;
    }

    public async Task ClaimMustNotBeEmpty(Claim? claim)
    {
        if (claim == null)
        {
            throw new ValidationException("Claim must not be empty.", 500);
        }
        await Task.CompletedTask;
    }
}

public class AddClaimValidations : ClaimValidations
{
    public AddClaimValidations(IClaimRepository claimRepository) : base(claimRepository)
    {
    }

}
public class UpdateClaimValidations : ClaimValidations
{
    public UpdateClaimValidations(IClaimRepository claimRepository) : base(claimRepository)
    {
    }
}

public class DeleteClaimValidations : BaseValidation
{
    protected readonly IClaimRepository _claimRepository;
    public DeleteClaimValidations(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository;
    }
    public async Task ClaimNotFound(Guid id)
    {
        var dbClaim = await _claimRepository.GetAsync(c => c.Id == id);
        if (dbClaim == null)
        {
            throw new ValidationException("Claim not found.", 400);
        }
        await Task.CompletedTask;
    }
}
