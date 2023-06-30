using Microsoft.EntityFrameworkCore;
using StreamBudget.DAL.Abstract;
using StreamBudget.Models;

namespace StreamBudget.DAL.Concrete
{
    public class WatchlistItemRepository: Repository<WatchlistItem>, IWatchlistItemRepository
    {
        public WatchlistItemRepository(DbContext ctx) : base(ctx)
        {
        }

        public void DeleteWatchlistItemBySeriesId(int watchlistId,string imdbId)
        {
            try
            {
                WatchlistItem itemToRemove = _dbSet.SingleOrDefault(i => i.WatchlistId == watchlistId && i.ImdbId == imdbId);
                _dbSet.Remove(itemToRemove);
            }
            catch (InvalidOperationException) 
            {
               
            }
         
        }
    }
}
