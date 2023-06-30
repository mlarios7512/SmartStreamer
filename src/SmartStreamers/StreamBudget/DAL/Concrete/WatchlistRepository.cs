using Microsoft.EntityFrameworkCore;
using StreamBudget.DAL.Abstract;
using StreamBudget.Models;

namespace StreamBudget.DAL.Concrete
{
    public class WatchlistRepository: Repository<Watchlist>, IWatchlistRepository
    {
        public WatchlistRepository(DbContext ctx) : base(ctx)
        {
        }


    }
}
