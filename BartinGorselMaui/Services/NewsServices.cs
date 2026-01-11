using Newtonsoft.Json;
using BartinGorselMaui.Model;

namespace BartinGorselMaui.Services
{
    public class NewsServices
    {
        // JSON verisini doğrudan oku
        public static async Task<List<Item>> GetNewsFromJson(string url)
        {
            HttpClient client = new();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonData = await response.Content.ReadAsStringAsync();

            var root = JsonConvert.DeserializeObject<Root>(jsonData);
            return root?.items ?? new List<Item>();
        }

        // JSON formatına uygun model
        public class Root
        {
            public List<Item> items { get; set; }
        }
    }
}



