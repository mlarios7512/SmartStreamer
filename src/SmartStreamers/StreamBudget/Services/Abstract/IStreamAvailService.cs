using StreamBudget.Models.DTO.StreamAvail;

namespace StreamBudget.Services.Abstract
{
    public interface IStreamAvailService
    {
        public Task<IEnumerable<SearchResultDTO>> GetBasicSearch(string titleName);

        public Task<SearchResultDTO> GetSeriesDetails(string imdbId);
    }
}
