using StreamBudget.Models;
using StreamBudget.Models.DTO.StreamAvail;
using StreamBudget.Models.Other;

namespace StreamBudget.ViewModels
{
    public class WatchlistVM
    {
        public int WatchlistId { get; set; }
        public IEnumerable<WatchlistItem> WatchlistItems { get; set; } = new List<WatchlistItem>();
        public List<CompletionTime> CompletionTimes { get; set; } = new List<CompletionTime>();

        public WatchlistVM()
        {

        }
    }
}
