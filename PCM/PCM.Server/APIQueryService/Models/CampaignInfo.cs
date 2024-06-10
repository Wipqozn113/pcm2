using System.Text.Json.Serialization;

namespace PCM.Server.APIQueryService.Models
{
    public class CampaignInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("party_level")]
        public int PartyLevel { get; set; }

        [JsonPropertyName("experience")]
        public int Experience { get; set; }

        [JsonPropertyName("gold_awarded_this_level")]
        public int GoldAwardedThisLevel { get; set; }

        [JsonPropertyName("total_gold_for_this_level")]
        public int TotalGoldForThisLevel { get; set; }
    }
}
