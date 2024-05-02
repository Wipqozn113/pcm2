using PCM.Server.APIQueryService.Models;

namespace PCM.Server.Models
{
    public class LootAPIModel
    {
        private int _id { get; set; }

        public LootAPIModel(Loot loot) 
        { 
            _id = loot.Id;
            Name = loot.Name ?? "";
            ItemLevel = loot.Item_level;
            Awarded = loot.Awarded;
            Consumable = loot.Consumable;
        }

        public string Name { get; set; }
        public int ItemLevel { get; set; }
        public bool Awarded { get; set; }
        public bool Consumable { get; set; }

        public string EditUrl => $"{ConfigSettings.DirectusContentUrl}/loot/{_id}";
    }
}
