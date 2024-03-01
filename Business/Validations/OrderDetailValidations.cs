using Business.Tools.Exceptions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations;

public class OrderDetailValidations
{
    public async Task OrderDetailMustNotBeEmpty(OrderDetail? orderDetail)
    {
        if (orderDetail == null)
        {
            throw new ValidationException("Order detail must not be empty.", 500);
        }
        await Task.CompletedTask;
    }
}
