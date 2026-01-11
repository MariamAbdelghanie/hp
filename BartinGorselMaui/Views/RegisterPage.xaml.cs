using BartinGorselMaui.Services.Auth;

namespace BartinGorselMaui.Views;

public partial class RegisterPage : ContentPage
{
    private readonly FirebaseAuthService _auth = new(); // Firebase servisi

    public RegisterPage()
    {
        InitializeComponent(); // XAML bileşenlerini yükler
    }

    // "Kaydol" butonuna tıklandığında çalışır
    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var loginName = LoginNameEntry.Text?.Trim(); // Kullanıcı adı (sadece görsel, kullanılmayacak)
        var email = EmailEntry.Text?.Trim();         // Email
        var parola = PasswordEntry.Text;             // Parola

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(parola))
        {
            await DisplayAlert("Uyarı", "Email ve parola doldurulmalı.", "Tamam");
            return;
        }

        // Firebase ile kayıt yap (loginName kullanılmıyor)
        var (ok, mesaj) = await _auth.RegisterAsync(loginName ?? "Kullanıcı", email, parola);

        await DisplayAlert(ok ? "Başarılı" : "Hata", mesaj, "Tamam");

        if (ok)
        {
            // Başarılı kayıt → Login sayfasına dön
            await Navigation.PopModalAsync();
        }
    }

    // "Zaten bir hesabım var" butonuna tıklandığında çalışır
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}





