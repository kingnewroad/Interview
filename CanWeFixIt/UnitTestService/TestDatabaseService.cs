using CanWeFixItService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestService
{
    [TestClass]
    public class TestDatabaseService
    {
        DatabaseService _database;

        [TestInitialize()]
        public void Initialize()
        {
            _database = new DatabaseService();
            _database.SetupDatabase();
        }

        [TestCleanup()]
        public void Cleanup()
        {
            _database.CloseDatabase();
            GC.Collect();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestCanGetAllActiveMarketDataAsync()
        {
            // Act
            var marketData = await _database.MarketData();
            var activemarketData = from m in marketData
                                   where m.Active == true
                                    select m;
            // Assert
            Assert.AreEqual(marketData.Count(), activemarketData.Count());
        }

        [TestMethod]
        public async System.Threading.Tasks.Task TestCanGetAllActiveInstrumentsAsync()
        {
            // Act
            var instruments = await _database.Instruments();
            var activeInstruments = from i in instruments
                                    where i.Active == true
                                    select i;
            // Assert
            Assert.AreEqual(instruments.Count(), activeInstruments.Count());
        }
    }
}
