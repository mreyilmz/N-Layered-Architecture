using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs;

public class AddOrderDto
{
    public Guid UserId { get; set; }
    public IList<ProductTransaction> ProductTransactions { get; set; }
    public AddOrderDto()
    {
        ProductTransactions = new List<ProductTransaction>();
    }
}
