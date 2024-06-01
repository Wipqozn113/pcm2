using System.Text.Json;
using System.Text.Json.Serialization;

namespace PCM.Server.APIQueryService.Models
{
    public class Encounter
    {
        public int Id { get; set; }

        public List<Monster> Monsters { get; set; } = new List<Monster>();

        public int Gold { get; set; }

        [JsonPropertyName("party_level")]
        public int PartyLevel { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Difficulty { get; set; } = string.Empty;

        [JsonPropertyName("dashboard_export")]
        public string DashboardExport { get; set; } = string.Empty;

        public List<int> Loot { get; set; } = new List<int>();
    }

    public class Monster
    {
        public string Name { get; set; } = string.Empty;

        public string Link { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
