using Microsoft.Extensions.Caching.Memory;
using PCM.Server.APIQueryService.Models;
using PCM.Server.APIQueryService.Service;

namespace PCM.Server.Models
{
    public class CampaignInfoAPIModel
    {
        private CampaignInfoAPIModel() { }

        private static MemoryCache Cache { get; set; }
            = new MemoryCache(new MemoryCacheOptions());

        public static CampaignInfoAPIModel GetCampaignInfo(QueryService apiQueryService)
        {
            // Return a cached version if it exists
            var cachedInfo = Cache.Get<CampaignInfoAPIModel>("info");
            if (cachedInfo is not null)
                return cachedInfo;

            var info = apiQueryService.GetCampaignInfo();
            var campaignInfo = new CampaignInfoAPIModel()
            {
                PartyLevel = info.PartyLevel,
                Experience = info.Experience,
                GoldAwardedThisLevel = info.GoldAwardedThisLevel,
                TotalGoldForThisLevel = info.TotalGoldForThisLevel
            };

            return campaignInfo;
        }

        private int Id { get; set; }

        public int PartyLevel { get; set; }

        public int Experience { get; set; }

        public int GoldAwardedThisLevel { get; set; }

        public int TotalGoldForThisLevel { get; set; }
    }
}
