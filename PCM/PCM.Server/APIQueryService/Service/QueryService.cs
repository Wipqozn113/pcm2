using PCM.Server.APIQueryService.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PCM.Server.APIQueryService.Service
{
    public class QueryService
    {
        private string _baseUrl { get; set; }

        public QueryService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        private List<T> GetItems<T>(string itemName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync($"/items/{itemName}").Result;
                response.EnsureSuccessStatusCode();
                var root = JsonSerializer.Deserialize<Root<T>>(response.Content.ReadAsStream(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return root?.Data ?? new List<T>();
            }
        }

        public List<Loot> GetLoot()
        {
            return GetItems<Loot>("loot");
        }
    }
}
