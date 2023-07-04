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
    public class WatchlistRepository_Tests
    {

        private Mock<SBDbContext> _mockContext;
        private Mock<DbSet<Watchlist>> _mockWatchlistDbSet;
        private List<Watchlist> _watchlists;

        [SetUp]
        public void Setup()
        {          
            _watchlists = new List<Watchlist>
            {
                new Watchlist { Id = 1, Name = "Supposedly good anime", StreamingPlatform = "Hulu", SelectedStreamingCost = 7, OwnerId = 1},
                new Watchlist { Id = 2, Name = "Rewatchable series", StreamingPlatform = "Crunchyroll" , SelectedStreamingCost = 7, OwnerId = 1},
                new Watchlist { Id = 3, Name = "HBO Exclusives", StreamingPlatform = "HBO" , SelectedStreamingCost = 10, OwnerId = 5}
            };

            _mockContext = new Mock<SBDbContext>();
            _mockWatchlistDbSet = MockHelpers.GetMockDbSet(_watchlists.AsQueryable());
            _mockContext.Setup(ctx => ctx.Watchlists).Returns(_mockWatchlistDbSet.Object);

            _mockContext.Setup(ctx => ctx.Set<Watchlist>()).Returns(_mockWatchlistDbSet.Object);
        }


        [Test]
        public void DoesUserOwnWatchlist_WithValidOwnerAsUser_ReturnsTrue()
        {
            //Arrange
            IWatchlistRepository WatchlistRepo = new WatchlistRepository(_mockContext.Object);

            //Act
            bool UserOwnsWatchlist = WatchlistRepo.DoesUserOwnWatchlist(1, 1);

            //Assert
           Assert.That(UserOwnsWatchlist, Is.True);
        }

        [Test]
        public void DoesUserOwnWatchlist_WithInvalidOwnerAsUser_ReturnsTrue()
        {
            //Arrange
            IWatchlistRepository WatchlistRepo = new WatchlistRepository(_mockContext.Object);

            //Act
            bool UserOwnsWatchlist = WatchlistRepo.DoesUserOwnWatchlist(1, 5);

            //Assert
            Assert.That(UserOwnsWatchlist, Is.False);
        }

        [Test]
        public void DoesUserOwnWatchlist_WithInvalidOwnerAsUser_ReturnsTrux()
        {
            //Arrange
            IWatchlistRepository WatchlistRepo = new WatchlistRepository(_mockContext.Object);

            Watchlist ExpectedWatchlistOne = new Watchlist();
            ExpectedWatchlistOne.Id = 1;
            ExpectedWatchlistOne.Name = "Supposedly good anime";
            ExpectedWatchlistOne.StreamingPlatform = "Hulu";
            ExpectedWatchlistOne.SelectedStreamingCost = 7;
            ExpectedWatchlistOne.OwnerId = 1;

            Watchlist ExpectedWatchlistTwo = new Watchlist();
            ExpectedWatchlistTwo.Id = 2;
            ExpectedWatchlistTwo.Name = "Rewatchable series";
            ExpectedWatchlistTwo.StreamingPlatform = "Crunchyroll";
            ExpectedWatchlistTwo.SelectedStreamingCost = 7;
            ExpectedWatchlistTwo.OwnerId = 1;

            //Act
            List<Watchlist> WatchlistsRetrieved = WatchlistRepo.GetAllWatchlistsForUser(1).ToList();


            //Assert
            Assert.That(WatchlistsRetrieved.ElementAt(0).Name == ExpectedWatchlistOne.Name);
            Assert.That(WatchlistsRetrieved.ElementAt(0).StreamingPlatform == ExpectedWatchlistOne.StreamingPlatform);
            Assert.That(WatchlistsRetrieved.ElementAt(0).SelectedStreamingCost == ExpectedWatchlistOne.SelectedStreamingCost);
            Assert.That(WatchlistsRetrieved.ElementAt(0).OwnerId == ExpectedWatchlistOne.OwnerId);

            Assert.That(WatchlistsRetrieved.ElementAt(1).Name == ExpectedWatchlistTwo.Name);
            Assert.That(WatchlistsRetrieved.ElementAt(1).StreamingPlatform == ExpectedWatchlistTwo.StreamingPlatform);
            Assert.That(WatchlistsRetrieved.ElementAt(1).SelectedStreamingCost == ExpectedWatchlistTwo.SelectedStreamingCost);
            Assert.That(WatchlistsRetrieved.ElementAt(1).OwnerId == ExpectedWatchlistTwo.OwnerId);


            Assert.That(WatchlistsRetrieved.Count == 2);
        }


    }
}
