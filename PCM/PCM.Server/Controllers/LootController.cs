using Microsoft.AspNetCore.Mvc;
using PCM.Server.APIQueryService.Service;
using PCM.Server.APIQueryService.Models;

namespace PCM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LootController : Controller
    {
        private QueryService _apiQueryService { get; set; }

        private readonly ILogger<LootController> _logger;

        public LootController(ILogger<LootController> logger)
        {
            _logger = logger;
            _apiQueryService = new QueryService();
        }

        [HttpGet(Name = "GetLoot")]
        public IEnumerable<Loot> Get()
        {
            return _apiQueryService.GetLoot();
        }
    }
}
