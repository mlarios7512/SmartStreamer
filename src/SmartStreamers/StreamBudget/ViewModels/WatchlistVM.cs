using StreamBudget.Models.DTO.StreamAvail;
using StreamBudget.Models.Other;

namespace StreamBudget.ViewModels
{
    public class WatchlistVM
    {
        public IEnumerable<SearchResultDTO> SearchResults { get; set; } = new List<SearchResultDTO>();
        public List<CompletionTime> CompletionTimes { get; set; } = new List<CompletionTime>();

        public WatchlistVM()
        {

        }
    }
}
