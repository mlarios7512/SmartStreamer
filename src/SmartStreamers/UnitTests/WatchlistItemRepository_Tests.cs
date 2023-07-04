using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using StreamBudget.DAL.Abstract;
using StreamBudget.DAL.Concrete;
using StreamBudget.Models;
using NuGet.ContentModel;
using RemindersTest;

namespace UnitTests
{
    public class WatchlistItemRepository_Tests
    {

        private Mock<SBDbContext> _mockContext;
        private Mock<DbSet<WatchlistItem>> _mockWatchlistItemDbSet;
        private List<WatchlistItem> _watchlistItems;

        [SetUp]
        public void Setup()
        {          
            _watchlistItems = new List<WatchlistItem>
            {
                new WatchlistItem{ Id = 1, Title = "The Winter", ImdbId = "tt_not_real_1", FirstAirYear = 2000, EpisodeRuntime = 10, TotalEpisodeCount = 12, WatchlistId = 1},
                new WatchlistItem{ Id = 2, Title = "The Summer", ImdbId = "tt_not_real_2", FirstAirYear = 1998, EpisodeRuntime = 8, TotalEpisodeCount = 10, WatchlistId = 1},
                new WatchlistItem{ Id = 3, Title = "The Spring", ImdbId = "tt_not_real_3", FirstAirYear = 1996, EpisodeRuntime = 6, TotalEpisodeCount = 8, WatchlistId = 2}
            };

            _mockContext = new Mock<SBDbContext>();
            _mockWatchlistItemDbSet = MockHelpers.GetMockDbSet(_watchlistItems.AsQueryable());
            _mockContext.Setup(ctx => ctx.WatchlistItems).Returns(_mockWatchlistItemDbSet.Object);

            _mockContext.Setup(ctx => ctx.Set<WatchlistItem>()).Returns(_mockWatchlistItemDbSet.Object);
        }


        [Test]
        public void DoesItemAlreadyExistInWatchlist_WithExistingItemInWatchlist_ReturnsTrue()
        {
            //Arrange
            IWatchlistItemRepository WatchlistRepo = new WatchlistItemRepository(_mockContext.Object);
            const int WatchlistToSearchID = 1;
            const string ImdbIDToSearchFor = "tt_not_real_1";

            //Act
            bool ItemExistsInWatchlist = WatchlistRepo.DoesItemAlreadyExistInWatchlist(ImdbIDToSearchFor, WatchlistToSearchID);

            //Assert
            Assert.That(ItemExistsInWatchlist, Is.True);
        }

        [Test]
        public void DoesItemAlreadyExistInWatchlist_WithNoMatchingOrExistingItemInWatchlist_ReturnsFalse()
        {
            //Arrange
            IWatchlistItemRepository WatchlistRepo = new WatchlistItemRepository(_mockContext.Object);
            const int WatchlistToSearchID = 1;
            const string ImdbIDToSearchFor = "tt_not_real_3";

            //Act
            bool ItemExistsInWatchlist = WatchlistRepo.DoesItemAlreadyExistInWatchlist(ImdbIDToSearchFor, WatchlistToSearchID);

            //Assert
            Assert.That(ItemExistsInWatchlist, Is.False);
        }

        [Test]
        public void GetWatchlistItemByWatchlistId_WithWatchlistContaining2Items_Returns2Items()
        {
            //Arrange
            IWatchlistItemRepository WatchlistItemRepo = new WatchlistItemRepository(_mockContext.Object);
            const int WatchlistToSearchID = 1;

            List<WatchlistItem> ExpectedWatchListItems = new List<WatchlistItem> 
            {
                new WatchlistItem{ Id = 1, Title = "The Winter", ImdbId = "tt_not_real_1", FirstAirYear = 2000, EpisodeRuntime = 10, TotalEpisodeCount = 12, WatchlistId = 1},
                new WatchlistItem{ Id = 2, Title = "The Summer", ImdbId = "tt_not_real_2", FirstAirYear = 1998, EpisodeRuntime = 8, TotalEpisodeCount = 10, WatchlistId = 1},
            };

            //Act
            List<WatchlistItem> ActualWatchlistItems = WatchlistItemRepo.GetWatchlistItemByWatchlistId(WatchlistToSearchID);

            //Assert
            int i = 0;
            WatchlistItem CurItem = null;
            foreach (WatchlistItem item in ActualWatchlistItems) 
            {
                CurItem = ExpectedWatchListItems.ElementAt(i);
                Assert.That(item.WatchlistItemsAreEqual(ref CurItem),Is.True);

                i++;
            }

            Assert.That(ActualWatchlistItems.Count == 2);
        }

        [Test]
        public void DeleteWatchlistItemBySeriesId_WithWatchlistItemThatMatchesID_ShouldRemoveItemFromDB()
        {
            //Arrange
            IWatchlistItemRepository WatchlistItemRepo = new WatchlistItemRepository(_mockContext.Object);
            const int WatchlistToDeleteFromID = 1;

            WatchlistItem ExpectedToRemoveWatchlistItem = new WatchlistItem
            {
                Id = 1,
                Title = "The Winter",
                ImdbId = "tt_not_real_1",
                FirstAirYear = 2000,
                EpisodeRuntime = 10,
                TotalEpisodeCount = 12,
                WatchlistId = 1
            };    

            //Act
            WatchlistItemRepo.DeleteWatchlistItemBySeriesId(WatchlistToDeleteFromID,"tt_not_real_1");

            //Assert
            Assert.That(WatchlistItemRepo.FindById(1), Is.Null);
        }

    }
}
