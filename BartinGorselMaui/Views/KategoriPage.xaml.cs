using BartinGorselMaui.Model;

namespace BartinGorselMaui.Views
{
    public partial class KategoriPage : ContentPage
    {
        public KategoriPage()
        {
            InitializeComponent();
        }

        // Butona tıklanınca doğrudan NewsPage açılır
        // Artık NewsPage parametre almıyor, kullanıcı kategori seçimini NewsPage içinden yapacak
        private async void OnCategoryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewsPage());
        }
    }
}

