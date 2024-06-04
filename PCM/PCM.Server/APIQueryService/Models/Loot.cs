namespace PCM.Server.APIQueryService.Models
{
    public class Loot
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Item_level { get; set; }
        public bool Awarded { get; set; }
        public bool Consumable { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
