using StreamBudget.Models.DTO.StreamAvail;
using StreamBudget.ViewModels.Other;

namespace StreamBudget.ViewModels
{
    public class SeriesSearchVM
    {
        public string CurUserUsername { get; set; }
        public int WatchlistId { get; set; }

        public IEnumerable<SearchResultDTO> SearchResults { get; set; } = new List<SearchResultDTO>();
        public List<SeriesWatchtimeEstimate> CompletionTimes { get; set; } = new List<SeriesWatchtimeEstimate>();

        public SeriesSearchVM()
        {

        }
    }
}
