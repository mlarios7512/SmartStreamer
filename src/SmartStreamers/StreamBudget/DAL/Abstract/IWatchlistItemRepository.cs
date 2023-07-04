using StreamBudget.Models;

namespace StreamBudget.DAL.Abstract
{
    public interface IWatchlistItemRepository: IRepository<WatchlistItem>
    {
        public void DeleteWatchlistItemBySeriesId(int watchlistId, string imdbId);

        public bool DoesItemAlreadyExistInWatchlist(string newWatchlistItemImdbId, int watchlistId);

        List<WatchlistItem> GetWatchlistItemByWatchlistId(int watchlistId);
    }
}
