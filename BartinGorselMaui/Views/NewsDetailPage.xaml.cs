using BartinGorselMaui.Model;

namespace BartinGorselMaui.Views
{
    public partial class NewsDetailPage : ContentPage
    {
        private readonly Item news;

        public NewsDetailPage(Item selectedNews)
        {
            InitializeComponent();
            news = selectedNews;

            TitleLabel.Text = news.title;
            NewsWebView.Source = news.link;
        }

        private async void OnShareClicked(object sender, EventArgs e)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Uri = news.link,
                Text = news.title,
                Title = "Haberi Paylaş"
            });
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}











