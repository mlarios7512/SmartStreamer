using System;
using System.Collections.Generic;

namespace StreamBudget.Models;

public partial class Watchlist
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? StreamingPlatform { get; set; }

    public decimal? SelectedStreamingCost { get; set; }

    public int FirstAirYear { get; set; }

    public int? EpisodeRuntime { get; set; }

    public int? TotalEpisodeCount { get; set; }

    public int OwnerId { get; set; }

    public virtual Person Owner { get; set; } = null!;
}
