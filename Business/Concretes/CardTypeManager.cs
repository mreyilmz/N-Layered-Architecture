using Business.Abstracts;
using Business.Validations;
using DataAccess.Abstracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes;

public class CardTypeManager:ICardTypeService
{
    public readonly ICardTypeRepository _cardTypeRepository;
    public readonly CardTypeValidations _cardTypeValidations;

    public CardTypeManager(ICardTypeRepository cardTypeRepository, CardTypeValidations cardTypeValidations)
    {
        _cardTypeRepository = cardTypeRepository;
        _cardTypeValidations = cardTypeValidations;
    }
    public CardType Add(CardType cardType)
    {
        return _cardTypeRepository.Add(cardType);
    }

    public async Task<CardType> AddAsync(CardType cardType)
    {
        return await _cardTypeRepository.AddAsync(cardType);
    }

    public void DeleteById(Guid id)
    {
        var cardType = _cardTypeRepository.Get(o => o.Id == id);
        _cardTypeValidations.CardTypeMustNotBeEmpty(cardType).Wait();
        _cardTypeRepository.Delete(cardType);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var cardType = _cardTypeRepository.Get(o => o.Id == id);
        await _cardTypeValidations.CardTypeMustNotBeEmpty(cardType);
        await _cardTypeRepository.DeleteAsync(cardType);
    }

    public IList<CardType> GetAll()
    {
        return _cardTypeRepository.GetAll().ToList();
    }

    public async Task<IList<CardType>> GetAllAsync()
    {
        var result = await _cardTypeRepository.GetAllAsync();
        return result.ToList();
    }

    public CardType? GetById(Guid id)
    {
        return _cardTypeRepository.Get(o => o.Id == id);
    }

    public async Task<CardType?> GetByIdAsync(Guid id)
    {
        return await _cardTypeRepository.GetAsync(o => o.Id == id);
    }

    public CardType Update(CardType cardType)
    {
        return _cardTypeRepository.Update(cardType);
    }

    public async Task<CardType> UpdateAsync(CardType cardType)
    {
        return await _cardTypeRepository.UpdateAsync(cardType);
    }
}
