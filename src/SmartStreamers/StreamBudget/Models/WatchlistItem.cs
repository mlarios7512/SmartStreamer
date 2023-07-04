using System;
using System.Collections.Generic;

namespace StreamBudget.Models;

public partial class WatchlistItem
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string ImdbId { get; set; } = null!;

    public int FirstAirYear { get; set; }

    public int? EpisodeRuntime { get; set; }

    public int? TotalEpisodeCount { get; set; }

    public int WatchlistId { get; set; }

    public virtual Watchlist Watchlist { get; set; } = null!;

    public bool WatchlistItemsAreEqual(ref WatchlistItem other) 
    {
        if(other.Id == this.Id
            && other.Title == this.Title
            && other.ImdbId == this.ImdbId
            && other.FirstAirYear == this.FirstAirYear
            && other.EpisodeRuntime == this.EpisodeRuntime
            && other.TotalEpisodeCount == this.TotalEpisodeCount
            && other.WatchlistId == this.WatchlistId) 
        {
            return true;
        }
        return false;
    }
}
