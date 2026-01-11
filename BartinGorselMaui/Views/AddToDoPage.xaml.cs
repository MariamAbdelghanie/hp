using BartinGorselMaui.Model;
using BartinGorselMaui.Services;

namespace BartinGorselMaui.Views
{
    public partial class AddToDoPage : ContentPage
    {
        ToDoItem Todo;

        // Constructor: yeni görev veya düzenlenecek görev alınır
        public AddToDoPage(ToDoItem item)
        {
            InitializeComponent(); // XAML ile bağlantıyı kurar
            Todo = item;
            this.BindingContext = item; // Veri bağlama
        }

        // Kaydet butonuna basıldığında çalışır
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // 🔹 AddOrUpdate döndürdüğü tuple'u açıkça yakalıyoruz
            (bool success, string message) = await FirebaseServices.AddOrUpdate(Todo);

            if (success)
            {
                await DisplayAlert("Başarılı", "Görev kaydedildi.", "Tamam");
                await Navigation.PopAsync(); // Görevler listesine geri dön
            }
            else
            {
                await DisplayAlert("Hata", $"Kaydedilemedi: {message}", "Tamam");
            }
        }
    }
}



