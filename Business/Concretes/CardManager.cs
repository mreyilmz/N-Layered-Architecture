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

public class CardManager:ICardService
{
    public readonly ICardRepository _cardRepository;
    public readonly CardValidations _cardValidations;

    public CardManager(ICardRepository cardRepository, CardValidations cardValidations)
    {
        _cardRepository = cardRepository;
        _cardValidations = cardValidations;
    }

    [ValidationAspect(typeof(AddCardValidations))]
    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.ICardService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Card added.")]
    [SecurityAspect]
    public Card Add(Card card)
    {
        return _cardRepository.Add(card);
    }

    [ValidationAspect(typeof(AddCardValidations))]
    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.ICardService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Card added.")]
    [SecurityAspect]
    public async Task<Card> AddAsync(Card card)
    {
        return await _cardRepository.AddAsync(card);
    }

    [CacheRemoveAspect("Business.Abstracts.ICardService.GetAllAsync")]
    [ValidationAspect(typeof(DeleteValidations))]
    [DebugWriteSuccessAspect(Message = "Card deleted.")]
    [SecurityAspect]
    public void DeleteById(Guid id)
    {
        var card = _cardRepository.Get(c => c.Id == id);
        _cardValidations.CardMustNotBeEmpty(card).Wait();
        _cardRepository.Delete(card);
    }

    [CacheRemoveAspect("Business.Abstracts.ICardService.GetAllAsync")]
    [ValidationAspect(typeof(DeleteValidations))]
    [DebugWriteSuccessAspect(Message = "Card deleted.")]
    [SecurityAspect]
    public async Task DeleteByIdAsync(Guid id)
    {
        var card = _cardRepository.Get(c => c.Id == id);
        await _cardValidations.CardMustNotBeEmpty(card);
        await _cardRepository.DeleteAsync(card);
    }

    [CacheAspect(1)]
    [DebugWriteSuccessAspect(Message = "Card listing completed.")]
    [SecurityAspect]
    public IList<Card> GetAll()
    {
        return _cardRepository.GetAll().ToList();
    }

    [CacheAspect(1)]
    [DebugWriteSuccessAspect(Message = "Card listing completed.")]
    [SecurityAspect]
    public async Task<IList<Card>> GetAllAsync()
    {
        var result = await _cardRepository.GetAllAsync();
        return result.ToList();
    }

    [SecurityAspect]
    public IList<Card> GetAllWithUser()
    {
        return _cardRepository.GetAll(include: card => card.Include(c => c.User)).ToList();
    }

    [SecurityAspect]
    public async Task<IList<Card>> GetAllWithUserAsync()
    {
        var result = await _cardRepository.GetAllAsync(include: card => card.Include(c => c.User));
        return result.ToList();
    }

    [SecurityAspect]
    public IList<Card> GetAllWithCardType()
    {
        return _cardRepository.GetAll(include: card => card.Include(c => c.CardType)).ToList();
    }

    [SecurityAspect]
    public async Task<IList<Card>> GetAllWithCardTypeAsync()
    {
        var result = await _cardRepository.GetAllAsync(include: card => card.Include(c => c.CardType));
        return result.ToList();
    }

    [SecurityAspect]
    public Card? GetById(Guid id)
    {
        return _cardRepository.Get(c => c.Id == id);
    }

    [SecurityAspect]
    public async Task<Card?> GetByIdAsync(Guid id)
    {
        return await _cardRepository.GetAsync(c => c.Id == id);
    }


    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.ICardService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Card updated.")]
    [SecurityAspect]
    public Card Update(Card card)
    {
        return _cardRepository.Update(card);
    }

    [TransactionScopeAspect]
    [CacheRemoveAspect("Business.Abstracts.ICardService.GetAllAsync")]
    [DebugWriteSuccessAspect(Message = "Card updated.")]
    [SecurityAspect]
    public async Task<Card> UpdateAsync(Card card)
    {
        return await _cardRepository.UpdateAsync(card);
    }
}
