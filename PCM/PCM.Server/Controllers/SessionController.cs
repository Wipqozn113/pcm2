using Microsoft.AspNetCore.Mvc;
using PCM.Server.APIQueryService.Service;
using PCM.Server.APIQueryService.Models;
using PCM.Server.Models;

namespace PCM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : Controller
    {
        private QueryService _apiQueryService { get; set; }

        private readonly ILogger<SessionController> _logger;

        public SessionController(ILogger<SessionController> logger)
        {
            _logger = logger;
            _apiQueryService = new QueryService();
        }

        [HttpGet(Name = "GetSession")]
        public SessionAPIModel? Get(int SessionId)
        {
            return SessionAPIModel.Create(_apiQueryService, SessionId);
        }

        /*[HttpGet(Name = "GetNextSession")]
        public SessionAPIModel? GetNext()
        {
            return SessionAPIModel.GetNextSession(_apiQueryService);
        }*/
    }
}
