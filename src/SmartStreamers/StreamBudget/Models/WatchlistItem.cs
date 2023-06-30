using System;
using System.Collections.Generic;

namespace StreamBudget.Models;

public partial class WatchlistItem
{
    public int Id { get; set; }

    public string ImdbId { get; set; } = null!;

    public int FirstAirYear { get; set; }

    public int? EpisodeRuntime { get; set; }

    public int? TotalEpisodeCount { get; set; }

    public int WatchlistId { get; set; }

    public virtual Watchlist Watchlist { get; set; } = null!;
}
