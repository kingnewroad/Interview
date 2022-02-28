using CanWeFixItService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CanWeFixItApi.Controllers
{
    [ApiController]
    [Route("v1/valuations")]
    public class ValuationController : ControllerBase
    {
        private readonly IDatabaseService _database;

        public ValuationController(IDatabaseService database)
        {
            _database = database;
        }

        // GET
        public async Task<ActionResult<IEnumerable<MarketValuation>>> Get()
        {
            var marketData = await _database.MarketData();
            var result = new List<MarketValuation>()
            {
                new MarketValuation()
                {
                    Total = marketData.Sum(m => m.DataValue).Value
                }
            };
            return Ok(result);
        }
    }
}
