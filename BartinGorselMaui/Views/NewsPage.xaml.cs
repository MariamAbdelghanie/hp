using BartinGorselMaui.Model;
using BartinGorselMaui.Services;

namespace BartinGorselMaui.Views
{
    public partial class NewsPage : ContentPage
    {
        public NewsPage()
        {
            InitializeComponent();
        }

        // Butona tıklanınca kategoriye göre haberleri yükle
        private async void OnCategoryClicked(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string url = btn.Text switch
                {
                    "Gündem" => "https://api.rss2json.com/v1/api.json?rss_url=https://www.trthaber.com/gundem_articles.rss",
                    "Ekonomi" => "https://api.rss2json.com/v1/api.json?rss_url=https://www.trthaber.com/ekonomi_articles.rss",
                    "Spor" => "https://api.rss2json.com/v1/api.json?rss_url=https://www.trthaber.com/spor_articles.rss",
                    "Bilim Teknoloji" => "https://api.rss2json.com/v1/api.json?rss_url=https://www.trthaber.com/bilim_teknoloji_articles.rss",
                    "Son Dakika" => "https://api.rss2json.com/v1/api.json?rss_url=https://www.trthaber.com/sondakika_articles.rss",
                    _ => ""
                };

                if (!string.IsNullOrEmpty(url))
                {
                    await LoadNewsAsync(url);
                }
            }
        }

        // Haberleri yükle
        private async Task LoadNewsAsync(string url)
        {
            try
            {
                Loader.IsRunning = Loader.IsVisible = true;
                var newsItems = await NewsServices.GetNewsFromJson(url);
                NewsList.ItemsSource = newsItems;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Haberler yüklenemedi: {ex.Message}", "Tamam");
            }
            finally
            {
                Loader.IsRunning = Loader.IsVisible = false;
            }
        }

        // Habere tıklanınca detay sayfasına git
        private async void OnNewsSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Item selectedNews)
            {
                await Navigation.PushAsync(new NewsDetailPage(selectedNews));
                NewsList.SelectedItem = null;
            }
        }
    }
}







