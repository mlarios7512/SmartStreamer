using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StreamBudget.Models;

public partial class WatchlistItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(64, MinimumLength = 1, ErrorMessage = "Title must be between {1} and {2} characters.")]
    public string Title { get; set; } = null!;

    //See "Better Call Saul Employee Training (2017)".
    //It does NOT have an ImdbId, so:
    //---A new scheme will need to be used for the page responsbile for generating full watchlist stats.
    //---A new scheme will need to be used for deleting (and possibliy for adding) an item from a watchlist.
    [StringLength(64, MinimumLength = 1, ErrorMessage = "ImdbId must be between {1} and {2} characters.")]
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
