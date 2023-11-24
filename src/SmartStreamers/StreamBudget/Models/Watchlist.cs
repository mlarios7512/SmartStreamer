using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StreamBudget.Models;

public partial class Watchlist
{
    public int Id { get; set; }

    [Required (ErrorMessage = "Watchlist must have a name.")]
    [StringLength(64, MinimumLength = 1, ErrorMessage = "Watchlist name must be between {1} and {2} characters.")]
    public string Name { get; set; } = null!;

    [StringLength(64, MinimumLength = 1, ErrorMessage = "Watchlist name must be between {1} and {2} characters")]
    public string? StreamingPlatform { get; set; }

    [Range(0, 50, ErrorMessage = "Value for subscription cost must be between {1} and {2}.")]
    [RegularExpression(@"^\d{1,2}(\.\d{1,2})?$")]
    public decimal? SelectedStreamingCost { get; set; }

    public int OwnerId { get; set; }

    public virtual Person Owner { get; set; } = null!;

    public virtual ICollection<WatchlistItem> WatchlistItems { get; set; } = new List<WatchlistItem>();

    public bool WatchlistsAreEqual(ref Watchlist other)
    {
        if(other.Id == this.Id 
            && other.Name == this.Name 
            && other.StreamingPlatform == this.StreamingPlatform
            && other.SelectedStreamingCost == this.SelectedStreamingCost
            && other.OwnerId == this.OwnerId) 
        {
            return true;
        }
        return false;
    }
}
