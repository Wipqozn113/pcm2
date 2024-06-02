using System.Text.Json.Serialization;

namespace PCM.Server.APIQueryService.Models
{
    public class Session
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("session_date")]
        public string SessionDate { get; set; } = string.Empty;

        public DateOnly SessionDateOnly
        {
            get
            {
                var date = DateOnly.ParseExact(SessionDate, "yyyy-MM-dd");
  
                return date;
            }
        }  

        [JsonPropertyName("overview")]
        public string Overview { get; set; } = string.Empty;

        [JsonPropertyName("notes")]
        public string Notes { get; set; } = string.Empty;

        [JsonIgnore]
        public List<int> Loot
        {
            get
            {
                return SessionLoots.Select(l => l.LootId).ToList();
            }
        }

        [JsonPropertyName("loot")]
        public List<SessionLoot> SessionLoots { get; set; } = new List<SessionLoot>();

        [JsonIgnore]
        public List<int> Encounters
        {
            get
            {
                return SessionEncounters.Select(x => x.EncounterId).ToList();
            }
        }

        [JsonPropertyName("encounters")]
        public List<SessionEncounter> SessionEncounters { get; set; } = new List<SessionEncounter>();

        public class SessionEncounter
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("session_id")]
            public int SessionId { get; set; }

            [JsonPropertyName("encounter_id")]
            public int EncounterId { get; set; }
        }

        public class SessionLoot
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("session_id")]
            public int SessionId { get; set; }

            [JsonPropertyName("loot_id")]
            public int LootId { get; set; }
        }
    }
}
