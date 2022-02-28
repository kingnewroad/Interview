using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CanWeFixItService;
using Microsoft.AspNetCore.Mvc;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/marketdata")]
    public class MarketDataController : ControllerBase
    {
        private readonly IDatabaseService _database;

        public MarketDataController(IDatabaseService database)
        {
            _database = database;
        }

        // GET
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> Get()
        {
            var marketData =  await _database.MarketData();
            var instruments = await _database.Instruments();
            var result = from m in marketData
                         join i in instruments on m.Sedol equals i.Sedol
                         where m.Active = true
                         select new MarketDataDto()
                         {
                             Id = m.Id,
                             Active = m.Active,
                             DataValue = m.DataValue,
                             InstrumentId = i.Id
                         };
            return Ok(result);
        }
    }
}