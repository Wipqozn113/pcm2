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

        [JsonIgnore]
        public List<int> Loot
        {
            get
            {
                return EncounterLoots.Select(x => x.LootId).ToList();
            }
        }

        [JsonPropertyName("loot")]
        public List<EncounterLoot> EncounterLoots { get; set; } = new List<EncounterLoot>();

        public class EncounterLoot
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("encounter_id")]
            public int EncounterId { get; set; }

            [JsonPropertyName("loot_id")]
            public int LootId { get; set; }
        }
    }

    public class Monster
    {
        public string Name { get; set; } = string.Empty;

        public string Link { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }
}
