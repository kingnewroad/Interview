using CanWeFixItService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CanWeFixItApi.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestApi
{
    [TestClass]
    public class TestValuationController
    {
        IDatabaseService _database;

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
        public void TestGet_ShouldReturn()
        {
            // arrange
            var controller = new ValuationController(_database);
            // act
            var result = controller.Get();
            var okResult = result.Result.Result as OkObjectResult;
            // assert
            Assert.AreEqual(okResult.StatusCode,200);
        }
    }
}
