using System;
using System.Collections.Generic;

namespace StreamBudget.Models;

public partial class Person
{
    public int Id { get; set; }

    public string AspnetIdentityId { get; set; } = null!;
}
