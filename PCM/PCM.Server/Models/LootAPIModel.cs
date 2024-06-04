using PCM.Server.APIQueryService.Models;
using PCM.Server.APIQueryService.Service;
using System.Xml.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace PCM.Server.Models
{
    public class LootAPIModel
    {
        private int _id { get; set; }

        private static MemoryCache Cache { get; set; }
            = new MemoryCache(new MemoryCacheOptions());

        private LootAPIModel() { }

        public static LootAPIModel? Create(QueryService apiQueryService, int lootId)
        {
            // If a cached entry exists, just return it
            var cachedLoot = Cache.Get<LootAPIModel>(lootId);
            if (cachedLoot is not null)
                return cachedLoot;

            var loot = apiQueryService.GetLoot(lootId);

            // Couldn't find Loot for this ID
            if (loot is null)
                return null;

            var lt =new LootAPIModel()
            {
                Id = loot.Id,
                Name = loot.Name ?? String.Empty,
                ItemLevel = loot.Item_level,
                Awarded = loot.Awarded,
                Consumable = loot.Consumable,
                AoNUrl = loot.Url
            };

            Cache.Set(lootId, lt);

            return lt;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ItemLevel { get; set; }
        public bool Awarded { get; set; }
        public bool Consumable { get; set; }
        public string AoNUrl { get; set; } = string.Empty;

        public string EditUrl => $"{ConfigSettings.DirectusContentUrl}/loot/{Id}";
    }
}
