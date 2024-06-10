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

        private T? GetSingleton<T>(string itemName) where T : class
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = $"/items/{itemName}";
                var response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                var test = response.Content.ReadAsStringAsync().Result;

                var root = JsonSerializer.Deserialize<RootSingleton<T>>(response.Content.ReadAsStream(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return root?.Data;
            }
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
                var test = response.Content.ReadAsStringAsync().Result;

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
            query.IsEqual("id", encounterId).IncludeRelatedFields();

            return GetItems<Encounter>("encounter", query).FirstOrDefault();
        }

        public Session? GetSession(int sessionId)
        {
            var query = new DirectusQuery();
            query.IsEqual("id", sessionId).IncludeRelatedFields();

            return GetItems<Session>("session", query).FirstOrDefault();
        }

        public Session? GetNextSession()
        {
            var query = new DirectusQuery();
            query.IncludeRelatedFields();

            var sessions = GetItems<Session>("session", query);
            if (sessions is null)
                return null;

           return sessions
                .Where(x => DateOnly.FromDateTime(DateTime.Now) <= x.SessionDateOnly)
                .OrderBy(x => x.SessionDateOnly)
                .FirstOrDefault();
        }

        public CampaignInfo GetCampaignInfo()
        {
            var info = GetSingleton<CampaignInfo>("campaign_information");
            return info ?? new CampaignInfo();
        }
    }
}
