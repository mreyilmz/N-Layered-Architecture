using Business.Tools.Exceptions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations;

public class CardTypeValidations
{
    public async Task CardTypeMustNotBeEmpty(CardType? cardType)
    {
        if (cardType == null)
        {
            throw new ValidationException("Card type must not be empty.", 500);
        }
        await Task.CompletedTask;
    }
}
