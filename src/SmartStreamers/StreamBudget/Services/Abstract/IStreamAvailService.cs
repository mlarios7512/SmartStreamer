using StreamBudget.Models.DTO.StreamAvail;

namespace StreamBudget.Services.Abstract
{
    public interface IStreamAvailService
    {
        public Task<IEnumerable<WatchlistItemDTO>> GetBasicSearch(string titleName);

        public Task<WatchlistItemDTO> GetSeriesDetails(string imdbId);
    }
}
