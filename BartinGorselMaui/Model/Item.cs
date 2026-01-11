using Newtonsoft.Json;

namespace BartinGorselMaui.Model
{
    public class Item
    {
        public string title { get; set; }
        public string link { get; set; }
        public string pubDate { get; set; }
        public string thumbnail { get; set; } // Görsel için
        public string description { get; set; }
    }
}

