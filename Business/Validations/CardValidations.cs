using Business.Tools.Exceptions;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;
using Entities.Models;


namespace Business.Validations;

public class CardValidations : BaseValidation
{
    protected readonly ICardRepository _cardRepository;
    public CardValidations(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task CardMustNotBeEmpty(Card? card)
    {
        if (card == null)
        {
            throw new ValidationException("Card must not be empty.", 500);
        }
        await Task.CompletedTask;
    }
}

public class AddCardValidations : CardValidations
{
    public AddCardValidations(ICardRepository cardRepository) : base(cardRepository)
    {
    }

}
public class UpdateCardValidations : CardValidations
{
    public UpdateCardValidations(ICardRepository cardRepository) : base(cardRepository)
    {
    }
}

public class DeleteCardValidations : BaseValidation
{
    protected readonly ICardRepository _cardRepository;
    public DeleteCardValidations(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    public async Task CardNotFound(Guid id)
    {
        var dbCard = await _cardRepository.GetAsync(c => c.Id == id);
        if (dbCard == null)
        {
            throw new ValidationException("Card not found.", 400);
        }
        await Task.CompletedTask;
    }
}



