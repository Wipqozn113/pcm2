using PCM.Server.APIQueryService.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PCM.Server.APIQueryService.Service
{
    public class QueryService
    {
        private string _baseUrl { get; set; }

        public QueryService()
        {
            _baseUrl = ConfigSettings.DirectusUrl;
        }

        private List<T> GetItems<T>(string itemName, DirectusQuery query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = $"/items/{itemName}{query.GetQueryString()}";
                var response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                var root = JsonSerializer.Deserialize<Root<T>>(response.Content.ReadAsStream(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return root?.Data ?? new List<T>();
            }
        }

        public Loot? GetLoot(int lootId)
        {
            var query = new DirectusQuery();
            query.IsEqual("id", lootId);

            return GetItems<Loot>("loot", query).FirstOrDefault();
        }

        public List<Loot> GetLootForLevel(int partyLevel)
        {
            var query = new DirectusQuery();
            query.IsEqual("player_level", partyLevel);

            return GetItems<Loot>("loot", query);
        }

        public Encounter? GetEncounter(int encounterId)
        {
            var query = new DirectusQuery();
            query.IsEqual("id", encounterId);

            return GetItems<Encounter>("encounter", query).FirstOrDefault();
        }
    }
}
