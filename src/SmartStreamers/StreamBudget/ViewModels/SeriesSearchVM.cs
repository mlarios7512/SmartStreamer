using StreamBudget.Models.DTO.StreamAvail;
using StreamBudget.Models.Other;

namespace StreamBudget.ViewModels
{
    public class SeriesSearchVM
    {
        public string CurUserUsername { get; set; }

        public IEnumerable<SearchResultDTO> SearchResults { get; set; } = new List<SearchResultDTO>();
        public List<CompletionTime> CompletionTimes { get; set; } = new List<CompletionTime>();

        public SeriesSearchVM()
        {

        }
    }
}
