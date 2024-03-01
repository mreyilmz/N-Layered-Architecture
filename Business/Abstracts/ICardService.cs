using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts;

public interface ICardService
{
    Card? GetById(Guid id);
    Task<Card?> GetByIdAsync(Guid id);
    IList<Card> GetAll();
    Task<IList<Card>> GetAllAsync();
    IList<Card> GetAllWithUser();
    Task<IList<Card>> GetAllWithUserAsync();
    IList<Card> GetAllWithCardType();
    Task<IList<Card>> GetAllWithCardTypeAsync();
    Card Add(Card card);
    Card Update(Card card);
    void DeleteById(Guid id);
    Task<Card> AddAsync(Card card);
    Task<Card> UpdateAsync(Card card);
    Task DeleteByIdAsync(Guid id);
}
