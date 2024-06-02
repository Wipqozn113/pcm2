using Microsoft.AspNetCore.Mvc;
using PCM.Server.APIQueryService.Service;
using PCM.Server.APIQueryService.Models;
using PCM.Server.Models;

namespace PCM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncounterController : Controller
    {
        private QueryService _apiQueryService { get; set; }

        private readonly ILogger<EncounterController> _logger;

        public EncounterController(ILogger<EncounterController> logger)
        {
            _logger = logger;
            _apiQueryService = new QueryService();
        }

        [HttpGet(Name = "GetEncounter")]
        public EncounterAPIModel? Get(int encounterId)
        {
            return EncounterAPIModel.Create(_apiQueryService, encounterId);
        }
    }
}
