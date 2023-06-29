using System;
using System.Collections.Generic;

namespace StreamBudget.Models;

public partial class Person
{
    public int Id { get; set; }

    public string AspnetIdentityId { get; set; } = null!;

    public virtual ICollection<Watchlist> Watchlists { get; set; } = new List<Watchlist>();
}
