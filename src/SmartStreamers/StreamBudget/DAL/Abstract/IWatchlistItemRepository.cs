using StreamBudget.Models;

namespace StreamBudget.DAL.Abstract
{
    public interface IWatchlistItemRepository: IRepository<WatchlistItem>
    {
        public void DeleteWatchlistItemBySeriesId(int watchlistId, string imdbId);
    }
}
