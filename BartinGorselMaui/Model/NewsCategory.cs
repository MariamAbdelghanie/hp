namespace BartinGorselMaui.Model
{
    public class NewsCategory
    {
        public string Category { get; set; }
        public string Url { get; set; }

        public NewsCategory(string category, string url)
        {
            Category = category;
            Url = url;
        }
    }
}


