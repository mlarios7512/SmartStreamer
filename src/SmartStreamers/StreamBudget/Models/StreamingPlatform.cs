using System;
using System.Collections.Generic;

namespace StreamBudget.Models;

public partial class StreamingPlatform
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal? SelectedStreamingCost { get; set; }

    public int? FullWatchlistTime { get; set; }
}
