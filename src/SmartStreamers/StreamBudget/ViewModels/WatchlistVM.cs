using StreamBudget.Models;
using StreamBudget.Models.DTO.StreamAvail;
using StreamBudget.Models.Other;

namespace StreamBudget.ViewModels
{
    public class WatchlistVM
    {
        public int WatchlistId { get; set; }
        public decimal? WatchlistPlatformPrice { get; set; } = null;
        public IEnumerable<WatchlistItem> WatchlistItems { get; set; } = new List<WatchlistItem>();
        public List<SeriesWatchtimeEstimate> CompletionTimes { get; set; } = new List<SeriesWatchtimeEstimate>();

        public WatchlistVM()
        {

        }
    }
}
