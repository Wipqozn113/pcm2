using PCM.Server.APIQueryService.Models;
using PCM.Server.APIQueryService.Service;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace PCM.Server.Models
{
    public class EncounterAPIModel
    {
        private EncounterAPIModel() { }
        private static MemoryCache Cache { get; set; }
            = new MemoryCache(new MemoryCacheOptions());

        public static EncounterAPIModel? Create(QueryService apiQueryService, int encounterId)
        {
            // Return a cached version if it exists
            var cachedEncounter = Cache.Get<EncounterAPIModel>(encounterId);
            if(cachedEncounter is not null)
                return cachedEncounter;

            var encounter = apiQueryService.GetEncounter(encounterId);

            // Encounter not found
            if (encounter is null)
                return null;

            var enc = new EncounterAPIModel()
            {
                Id = encounter.Id,
                Gold = encounter.Gold,
                PartyLevel = encounter.PartyLevel,
                Name = encounter.Name,
                _difficulty = encounter.Difficulty,
                DashboardExport = encounter.DashboardExport
            };

            // Populate list of monsters
            foreach(var monster in encounter.Monsters)
            {
                enc.Monsters.Add
                    (
                        new Monster()
                        {
                            Name = monster.Name,
                            Link = monster.Link,
                            Quantity = monster.Quantity
                        }
                    );
            }

            // Populate list of loot
            foreach(var lootId in encounter.Loot)
            { 
                var loot = LootAPIModel.Create(apiQueryService, lootId);
                
                if (loot is not null)
                    enc.Loot.Add(loot);
            }

            Cache.Set(encounterId, enc);

            return enc;
        }

        public int Id { get; set; }

        public int Gold { get; set; }

        public int PartyLevel { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Difficulty
        {
            get
            {
                return  CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_difficulty.ToLower());
            }
            set
            {
                _difficulty = value;
            }
        }

        private string _difficulty; 

        public string DashboardExport { get; set; } = string.Empty;

        public List<Monster> Monsters { get; set; } = new List<Monster>();

        public List<LootAPIModel> Loot { get; set; } = new List<LootAPIModel>();



        public class Monster
        {
            public string Name { get; set; } = string.Empty;

            public string Link { get; set; } = string.Empty;

            public int Quantity { get; set; }
        }
    }
}
