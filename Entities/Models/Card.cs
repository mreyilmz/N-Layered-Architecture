﻿using Core.Entities;
using Core.Repository;

namespace Entities.Models;

public class Card : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid CardTypeId { get; set; }
    public long CardUID { get; set; }
    public decimal Limit { get; set; }
    public virtual User User { get; set; }
    public virtual CardType CardType { get; set; }
}