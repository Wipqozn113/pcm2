using Microsoft.AspNetCore.Mvc;
using PCM.Server.APIQueryService.Service;
using PCM.Server.APIQueryService.Models;
using PCM.Server.Models;

namespace PCM.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignInfoController : Controller
    {
        private QueryService _apiQueryService { get; set; }

        private readonly ILogger<CampaignInfoController> _logger;

        public CampaignInfoController(ILogger<CampaignInfoController> logger)
        {
            _logger = logger;
            _apiQueryService = new QueryService();
        }

        [HttpGet(Name = "GetCampaignInfo")]
        public CampaignInfoAPIModel Get()
        {
            return CampaignInfoAPIModel.GetCampaignInfo(_apiQueryService);
        }
    }
}
